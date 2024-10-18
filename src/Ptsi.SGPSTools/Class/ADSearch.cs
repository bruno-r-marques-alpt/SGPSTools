using PTelecom.Intranet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using WindowsApplication1.ptsgps_spsdev011;

namespace SGPStools
{
    public class ADSearch
    {
        public ADSearch()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => { return true; };
        }

        /// <summary>
        /// This function processes an AD search request and return either a error or search responses.
        /// </summary>
        public object ProcessSearchRequest(searchRequest oRequest, IdentityCredentials oCredentials)
        {
            if (oRequest == null)
                return (object)null;
            try
            {
                var appSetting = ConfigurationManager.AppSettings["LDAPServer"];
                using (var searchRoot = new DirectoryEntry(appSetting + "/" + oRequest.dn, ConfigurationManager.AppSettings["LDAPUsername"], Encription.DecryptToken(ConfigurationManager.AppSettings["LDAPPassword"])))
                {
                    using (var directorySearcher = new DirectorySearcher(searchRoot))
                    {
                        directorySearcher.Filter = oRequest.filter;
                        switch (oRequest.scope)
                        {
                            case "baseObject":
                                directorySearcher.SearchScope = SearchScope.Base;
                                break;
                            case "singleLevel":
                                directorySearcher.SearchScope = SearchScope.OneLevel;
                                break;
                            case "wholeSubtree":
                                directorySearcher.SearchScope = SearchScope.Subtree;
                                break;
                        }
                        directorySearcher.SizeLimit = oRequest.sizeLimit;
                        directorySearcher.ServerTimeLimit = new TimeSpan(0, 0, 0, oRequest.timeLimit);
                        directorySearcher.PropertyNamesOnly = oRequest.typesOnly;
                        switch (oRequest.derefAliases)
                        {
                            case "neverDerefAliases":
                                directorySearcher.ReferralChasing = ReferralChasingOption.None;
                                break;
                            case "derefAlways":
                                directorySearcher.ReferralChasing = ReferralChasingOption.All;
                                break;
                            case "derefInSearching":
                                directorySearcher.ReferralChasing = ReferralChasingOption.External;
                                break;
                            case "derefFindingBaseObj":
                                directorySearcher.ReferralChasing = ReferralChasingOption.Subordinate;
                                break;
                        }
                        var flag1 = false;
                        if (oRequest.attributes != null)
                        {
                            for (var index = 0; index < oRequest.attributes.Length; ++index)
                            {
                                directorySearcher.PropertiesToLoad.Add(oRequest.attributes[index].name);
                                flag1 = flag1 || oRequest.attributes[index].name.ToLower() == "objectclass";
                            }
                        }
                        else
                            flag1 = true;
                        if (!flag1)
                            directorySearcher.PropertiesToLoad.Add("objectClass");
                        var all = directorySearcher.FindAll();
                        var searchResponse = new searchResponse();
                        var items = new List<object>();
                        foreach (SearchResult searchResult in all)
                        {
                            var searchResultEntry = new searchResultEntry
                            {
                                dn = searchResult.Path.Remove(0, appSetting.Length + 1)
                            };
                            var property = "";
                            foreach (string prop in (ReadOnlyCollectionBase)searchResult.Properties["objectClass"])
                            {
                                property = prop;
                                break;
                            }
                            var num2 = 0;

                            //List<attr> attrs = new List<attr>();

                            searchResultEntry.attr = new attr[searchResult.Properties.Count];

                            foreach (string propertyName in (IEnumerable)searchResult.Properties.PropertyNames)
                            {
                                var attr = new attr
                                {
                                    name = propertyName,
                                    value = new string[searchResult.Properties[propertyName].Count]
                                };
                                for (var index = 0; index < attr.value.Length; ++index)
                                    attr.value[index] = searchResult.Properties[propertyName][index].ToString();

                                //attrs.Add(attr);
                                searchResultEntry.attr[num2++] = attr;

                                items.Add(searchResultEntry);
                            }
                        }
                        searchResponse.searchResultDone = new SearchResultDone
                        {
                            resultcode = new ResultCode
                            {
                                code = 0,
                                codeSpecified = true,
                                descr = "Success"
                            }
                        };
                        searchResponse.Items = items.ToArray();
                        return (object)searchResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                return (object)new errorResponse()
                {
                    type = "gatewayInternalError",
                    message = ex.Message,
                    detail = ex.ToString()
                };
            }
        }


    }
}