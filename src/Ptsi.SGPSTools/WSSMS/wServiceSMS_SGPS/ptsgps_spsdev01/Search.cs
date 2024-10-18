// Decompiled with JetBrains decompiler
// Type: WindowsApplication1.ptsgps_spsdev01.Search
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace WindowsApplication1.ptsgps_spsdev01
{
    [WebServiceBinding(Name = "SearchSoap", Namespace = "urn:PTelecom/Intranet/SearchService")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public class Search : SoapHttpClientProtocol
    {
        public IdentityCredentials IdentityCredentialsValue;

        public Search()
        {
            Url = ConfigurationManager.AppSettings["WebServiceSearchURL"];
        }

        [SoapDocumentMethod("urn:PTelecom/Intranet/SearchService/getProperties",
            RequestNamespace = "urn:PTelecom/Intranet/SearchService",
            ResponseNamespace = "urn:PTelecom/Intranet/SearchService", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        [SoapHeader("IdentityCredentialsValue")]
        public GetPropertiesResponse getProperties()
        {
            return (GetPropertiesResponse)Invoke("getProperties", new object[0])[0];
        }

        public IAsyncResult BegingetProperties(AsyncCallback callback, object asyncState)
        {
            return BeginInvoke("getProperties", new object[0], callback, asyncState);
        }

        public GetPropertiesResponse EndgetProperties(IAsyncResult asyncResult)
        {
            return (GetPropertiesResponse)EndInvoke(asyncResult)[0];
        }

        [SoapDocumentMethod("urn:PTelecom/Intranet/SearchService/search",
            RequestNamespace = "urn:PTelecom/Intranet/SearchService",
            ResponseNamespace = "urn:PTelecom/Intranet/SearchService", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        [SoapHeader("IdentityCredentialsValue")]
        public SearchResponse search(
            string PropertyFilter,
            string FreeTextFilter,
            string OrderBy,
            int PageNumber,
            int PageSize)
        {
            return (SearchResponse)Invoke("search", new object[5]
            {
                PropertyFilter,
                FreeTextFilter,
                OrderBy,
                PageNumber,
                PageSize
            })[0];
        }

        public IAsyncResult Beginsearch(
            string PropertyFilter,
            string FreeTextFilter,
            string OrderBy,
            int PageNumber,
            int PageSize,
            AsyncCallback callback,
            object asyncState)
        {
            return BeginInvoke("search", new object[5]
            {
                PropertyFilter,
                FreeTextFilter,
                OrderBy,
                PageNumber,
                PageSize
            }, callback, asyncState);
        }

        public SearchResponse Endsearch(IAsyncResult asyncResult)
        {
            return (SearchResponse)EndInvoke(asyncResult)[0];
        }
    }
}