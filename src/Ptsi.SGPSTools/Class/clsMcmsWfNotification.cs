// API MCMS 2002
//using Microsoft.ContentManagement.Publishing;
//using Microsoft.ContentManagement.Publishing.Events;

namespace SGPStools.Class
{
    /// <summary>
    /// Summary description for clsMcmsWfNotification.
    /// </summary>
    public class clsMcmsWfNotification
    {
        /*		static public void SendNotificationEmail(Posting pPosting, User userAccount, string strSubjectStatus) 
                {
                    // purpose: Send email Notification to sendTo users array

                    string strCriteria = GenerateUsersCriteria(userAccount);

                    // creates the data table for the search result information
                    DataTable objTable;
                    objTable = new DataTable();
                    objTable.Columns.Add("Email", System.Type.GetType("System.String"));

                    // Get each Domain , this probably won't be needed ??
                    string strSeparator = ";";
                    string []arrDomains;

                    arrDomains = ConfigurationManager.AppSettings["WebServiceADSearchDomain"].Split(strSeparator.ToCharArray());

                    // get a data table with the user's emails to send notification
                    for(int intCountArr = 0; intCountArr < arrDomains.Length; intCountArr++) 
                    {
                        GetUsersDataTable(arrDomains[intCountArr], objTable, strCriteria);
                    }

                    // if there are mail addresses to send mails to
                    if (objTable.Rows.Count > 0 ) {

                        // gets the posting data
                        string strMailBody = GetPostingDataToMailBody(pPosting);

                        // generates the mail message
                        MailMessage myMail = new MailMessage();
                        myMail.From ="IntranetSGPS"; // should be a valid return mail...
                        myMail.Subject = strSubjectStatus + " : " + pPosting.DisplayName;
                        myMail.Body = strMailBody;
                        myMail.BodyFormat = MailFormat.Html;
                        SmtpMail.SmtpServer = ConfigurationManager.AppSettings["MailServer"];

                        foreach(DataRow rowItem in objTable.Rows) {					

                            // sets the TO mail message data
                            myMail.To = rowItem["Email"].ToString();

                            try 
                            {
                                // sends the mail message to SMTP Server
                                SmtpMail.Send(myMail);
                            }
                            catch (Exception) {
                                return;
                            }

                        }
                    }		
                }


                static public void SendNotificationEmail(Posting pPosting, UserCollection sendToUsers, string strSubjectStatus) {
                    // purpose: Send email Notification to sendTo users array

                    string strCriteria = GenerateUsersCriteria(sendToUsers);

                    // is there a criteria to perform a LDAP search for email addresses
                    if (strCriteria.Length > 0) {

                        // creates the data table for the search result information
                        DataTable objTable;
                        objTable = new DataTable();
                        objTable.Columns.Add("Email", System.Type.GetType("System.String"));

                        // Get each Domain , this probably won't be needed ??
                        string strSeparator = ";";
                        string []arrDomains;

                        arrDomains = ConfigurationManager.AppSettings["WebServiceADSearchDomain"].Split(strSeparator.ToCharArray());


                        // get a data table with the user's emails to send notification
                        for(int intCountArr = 0; intCountArr < arrDomains.Length; intCountArr++) 
                        {
                            GetUsersDataTable(arrDomains[intCountArr], objTable, strCriteria);
                        }

                        // if there are mail addresses to send mails to
                        if (objTable.Rows.Count > 0 ) {

                            // gets the posting data
                            string strMailBody = GetPostingDataToMailBody(pPosting);

                            // generates the mail message
                            MailMessage myMail = new MailMessage();
                            myMail.From ="IntranetSGPS"; // should be a valid return mail...
                            myMail.Subject = strSubjectStatus + " : " + pPosting.DisplayName;
                            myMail.Body = strMailBody;
                            myMail.BodyFormat = MailFormat.Html;
                            SmtpMail.SmtpServer = ConfigurationManager.AppSettings["MailServer"];

                            foreach(DataRow rowItem in objTable.Rows) {					

                                // sets the TO mail message data
                                myMail.To = rowItem["Email"].ToString();

                                try 
                                {
                                    // sends the mail message to SMTP Server
                                    SmtpMail.Send(myMail);
                                }
                                catch (Exception) {
                                    return;
                                }

                            }
                        }
                    }			
                }


                static private string GetPostingDataToMailBody(Posting pPosting) {

                    // searches the posting with the given guid
                    string strCurrentUser = "";
                    strCurrentUser = CmsHttpContext.Current.User.ClientAccountName;

                    // creates the placeholder objects
                    PlaceholderCollection pPlaceholders;	

                    // gets the placeholdercollection for the current page
                    pPlaceholders = pPosting.Placeholders;

                    string urlHomePage = ConfigurationManager.AppSettings["DefaultAuthoringHomepage"];

                    string strMailBody = "";
                    strMailBody = strMailBody + "<table border='0' width='100%' cellspacing='2' cellpadding='2'><tr><td width='30%' align='right' bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Estado actual do conteúdo:</font></b></td>";
                    strMailBody = strMailBody + "<td width=70% valign=top>" + pPosting.State + "</td>";
                    strMailBody = strMailBody + "</tr><tr><td width='30%' align='right' valign=top bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Acção efectuada por:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top>"+strCurrentUser+"</td>";
                    strMailBody = strMailBody + "</tr><tr><td width='30%' align='right' valign=top bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Autor inicial do conteúdo:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top >"+pPosting.CreatedBy.ClientAccountName+"</td>";			
                    strMailBody = strMailBody + "</tr><tr><td width='30%' align='right' valign=top bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Última alteração por:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top >"+pPosting.LastModifiedBy.ClientAccountName +"</td>";
                    strMailBody = strMailBody + "</tr><tr><td width='100%' align='right' colspan='2' valign=top><hr noshade size='1' color='#000000'></td>";
                    strMailBody = strMailBody + "</tr><tr><td width='100%' align='right' colspan='2' valign=top bgcolor='#808080' ><p align='center'><b><font face='Arial' color='#FFFFFF'>CONTEÚDO</font></b></td></tr>";
                    strMailBody = strMailBody + "<tr><td valign=top width='30%' align='right'  bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Título:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top>"+pPlaceholders["strTitle"].Datasource.RawContent.ToString()+"</td>";
                    strMailBody = strMailBody + "</tr><tr><td width='30%' align='right' valign=top bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Sinopse:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top>"+pPlaceholders["strSynopsis"].Datasource.RawContent.ToString()+"</td>";
                    strMailBody = strMailBody + "</tr><tr><td width='30%' align='right' valign=top  bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Comentários:</font></b></td>";
                    strMailBody = strMailBody + "<td width='70%' valign=top>"+pPlaceholders["strComments"].Datasource.RawContent.ToString()+"</td></tr>";
                    if (pPosting.State != PostingState.Deleted) {
                        strMailBody = strMailBody + "<tr><td width='30%' align='right' valign=top  bgcolor='#C0C0C0'><b><font face='Arial' size='2'>Link:</font></b></td>";
                        strMailBody = strMailBody + "<td width='70%' valign=top><a href='"+ urlHomePage + pPosting.Url +"'>Clique aqui</a></td></tr>";
                    }
                    strMailBody = strMailBody + "</table>";
                    return strMailBody;
                }

                */
        /*
                static private void GetUsersDataTable(string strDomainPath, DataTable objTable, string strQueryUserCriteria) {
                    // Function to getUsers from the specified domainPath

                    // gets the environment type
                    string environment = ConfigurationManager.AppSettings["environment"];

                    int intTotalHits = 0;

                    // only if PROD env
                    if (environment.Equals("PROD")) {

                        UserMng op = new UserMng();

                        //create search request
                        searchRequest req = new searchRequest();

                        req.dn =  strDomainPath;
                        req.scope = "wholeSubtree";

                        req.filter = strQueryUserCriteria;
                        req.derefAliases = "derefAlways";

                        // FALTAM ATRIBUTOS, FALAR COM O PPL DA MS
                        req.attributes = (attribute[])Array.CreateInstance(typeof(attribute),1);
                        req.attributes[0] = new attribute();
                        req.attributes[0].name = "mail";

                        //set SOAP headers
                        op.IdentityCredentialsValue=new IdentityCredentials();
                        op.IdentityCredentialsValue.IdentityName= ConfigurationManager.AppSettings["ADSearchUserName"]; // "SGPSWebService";
                        op.IdentityCredentialsValue.IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"]; //"4ssWVfZg9mWVZBDp0nKb6w==";

                        op.UserCredentialsValue = new UserCredentials();
                        op.UserCredentialsValue.Username="a";
                        op.UserCredentialsValue.Password="a";
                        op.UserCredentialsValue.Domain = "a";

                        object[] items = new object[] {req};
                        object[] ret = op.batchRequest(items);

                        searchResponse resp = ret[0] as searchResponse;
                        errorResponse resp2 = ret[0] as errorResponse;

                        if (resp != null && resp.Items != null) 
                        {					

                            // Add to total hits in the search result
                            intTotalHits = intTotalHits + resp.Items.Length;						

                            string strEmail = "";

                            for (int intCounter=0; intCounter < resp.Items.Length; intCounter++)
                            {
                                searchResultEntry sr= (searchResultEntry) resp.Items[intCounter];

                                if (FindData(sr.attr, "mail") >= 0) 
                                { 
                                    strEmail = sr.attr[FindData(sr.attr, "mail")].value[0].ToString();
                                } 
                                else 
                                {
                                    strEmail = "";
                                }

                                // inserts data
                                objTable = AddContactSearchResult(objTable, strEmail);
                            }
                        }
                    }
                    else {
                        // not the production, its dev env.
                        // do some dummy code

                        // inserts data
                        objTable = AddContactSearchResult(objTable, ConfigurationManager.AppSettings["WFNotificationMailDevEnv"].ToString());

                    }
                }


                static private DataTable AddContactSearchResult(DataTable objTable, string strEmail) 
                {
                    DataRow objDataRow = objTable.NewRow();
                    objDataRow["Email"] = strEmail;
                    objTable.Rows.Add(objDataRow);
                    return objTable;
                }
        */
        /*
		static private string GenerateUsersCriteria(UserCollection sendToUsers) {
			// used to generate the user's criteria string to the LDAP query

			string strCriteria = "";
			
			// iterates every user and adds to the LDAP query
			foreach(User userAccount in sendToUsers) {
				strCriteria = strCriteria + "(cn=" + userAccount.ClientAccountName + ")";
			}

//			This is a dummy teste code 
//			strCriteria = strCriteria + "(cn=XOLI003)";
//			strCriteria = strCriteria + "(cn=XPTS093)";
//			strCriteria = strCriteria + "(cn=Q023187)";

			// adds the remain code to the LDAP query
			if (strCriteria.Length > 0) {
				strCriteria = "(&(objectClass=user)(|" + strCriteria + "))";
			} 
			
			return strCriteria;
		}

		
		static private string GenerateUsersCriteria(User userAccount) {
			// used to generate the user's criteria string to the LDAP query

			string strCriteria = "";		
			strCriteria = "(cn=" + userAccount.ClientAccountName + ")";

//			This is a dummy teste code 
//			strCriteria = strCriteria + "(cn=XOLI003)";

			// adds the remain code to the LDAP query
			strCriteria = "(&(objectClass=user)" + strCriteria + ")";
			
			return strCriteria;
		}

		*/
        /*
		static private int FindData(attr[] arrayData, string strFieldData) {
			// Searches the specified field int the array of data

			for(int intCounter = 0; intCounter < arrayData.Length; intCounter++) 
			{
				if (arrayData[intCounter].name.ToLower() == strFieldData.ToLower()) return intCounter;
			}
			return -1;
		}
		*/
    }
}
