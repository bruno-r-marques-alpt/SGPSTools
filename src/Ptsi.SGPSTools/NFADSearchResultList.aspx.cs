using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using WindowsApplication1.ptsgps_spsdev011;

namespace SGPStools.Templates
{
    /// <summary>
    /// Summary description for NFADSearchResultList.
    /// </summary>
    public class NFADSearchResultList : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.Repeater ListADListResults;
        protected System.Web.UI.WebControls.Literal LTResult;
        protected System.Web.UI.WebControls.Literal LTCriteria;
        protected System.Web.UI.WebControls.Literal LTTotalRegistos;
        protected System.Web.UI.WebControls.Literal LTPrevious;
        protected System.Web.UI.WebControls.Literal LTNext;
        protected System.Web.UI.WebControls.Literal LTPages;

        protected string strImagePath;
        protected DataTable objTable;
        string strSearchURL;
        int intMaxNumberResults;
        int intTotalHits;

        public string ImagePath
        {
            get { return strImagePath; }

        }

        // Function to getUsers from the specified domainPath
        private void getUsers(string strDomainPath, string strQueryName, string strQueryDepartment, string strQueryNameCriteria, string strQueryDepartmentCriteria, string strQueryEmailCriteria, string strQueryContactoCriteria)
        {
            try
            {
                string strName, strDepartment, strEmail, strContact;

                var op = new UserMng();

                //create search request
                var req = new searchRequest();

                req.dn = strDomainPath;
                req.scope = "wholeSubtree";

                //				(&(|(objectClass=contact)(objectClass=user))(name=balbla*)(department=blabla*))
                //				(&(objectClass=person)(name=balbla*)(department=blabla*))

                //req.filter = "(&(|(objectClass=contact)(objectClass=user))" + strQueryNameCriteria + strQueryDepartmentCriteria + ")";
                req.filter = "(&(|(objectClass=contact)(objectClass=user))" + strQueryNameCriteria + strQueryDepartmentCriteria + strQueryEmailCriteria + strQueryContactoCriteria + ")";
                req.derefAliases = "derefAlways";

                // FALTAM ATRIBUTOS, FALAR COM O PPL DA MS
                req.attributes = (attribute[])Array.CreateInstance(typeof(attribute), 4);
                req.attributes[0] = new attribute();
                req.attributes[0].name = "displayName";
                req.attributes[1] = new attribute();
                req.attributes[1].name = "department";
                req.attributes[2] = new attribute();
                req.attributes[2].name = "mail";
                req.attributes[3] = new attribute();
                req.attributes[3].name = "telephoneNumber";

                //set SOAP headers
                op.IdentityCredentialsValue = new IdentityCredentials();
                op.IdentityCredentialsValue.IdentityName = ConfigurationManager.AppSettings["ADSearchUserName"]; // "SGPSWebService";
                op.IdentityCredentialsValue.IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"]; //"4ssWVfZg9mWVZBDp0nKb6w==";

                // VERIFICAR COMO É ISTO COM O PPL DA MS
                op.UserCredentialsValue = new UserCredentials();
                op.UserCredentialsValue.Username = "a";
                op.UserCredentialsValue.Password = "a";
                op.UserCredentialsValue.Domain = "a";

                var items = new object[] { req };
                var ret = op.batchRequest(items);

                var resp = ret[0] as searchResponse;
                var resp2 = ret[0] as errorResponse;

                if (resp != null && resp.Items != null)
                {

                    // Add to total hits in the search result
                    intTotalHits = intTotalHits + resp.Items.Length;


                    // DE-comment in production END

                    for (var intCounter = 0; intCounter < resp.Items.Length; intCounter++)
                    {
                        var sr = (searchResultEntry)resp.Items[intCounter];

                        if (FindData(sr.attr, "displayName") >= 0)
                        {
                            strName = sr.attr[FindData(sr.attr, "displayName")].value[0].ToString();
                        }
                        else
                        {
                            strName = "";
                        };
                        if (FindData(sr.attr, "department") >= 0)
                        {
                            strDepartment = sr.attr[FindData(sr.attr, "department")].value[0].ToString();
                        }
                        else
                        {
                            strDepartment = "";
                        }
                        if (FindData(sr.attr, "telephoneNumber") >= 0)
                        {
                            strContact = sr.attr[FindData(sr.attr, "telephoneNumber")].value[0].ToString();
                        }
                        else
                        {
                            strContact = "";
                        }
                        if (FindData(sr.attr, "mail") >= 0)
                        {
                            strEmail = sr.attr[FindData(sr.attr, "mail")].value[0].ToString();
                        }
                        else
                        {
                            strEmail = "";
                        }

                        // inserts data
                        objTable = AddContactSearchResult(objTable, intCounter + 1, strName, strDepartment, strEmail, strContact, sr.dn);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            //	try {
            // checks the type of enviroment
            var strEnvironment = ConfigurationManager.AppSettings["environment"];

            // initiate the image path
            strImagePath = ConfigurationManager.AppSettings["imagePath"];

            string strQueryName = "", strQueryDepartment = "", strQueryNameCriteria = "", strQueryDepartmentCriteria = "";
            string strQueryEmail = "", strQueryContacto = "", strQueryEmailCriteria = "", strQueryContactoCriteria = "";
            int intCounter;
            int intFirstRecord;
            int intLastRecord;

            intTotalHits = 0;

            intMaxNumberResults = Convert.ToInt32(ConfigurationManager.AppSettings["ADSearchResultsMax"]);

            strSearchURL = "NFADSearchResultList.aspx";//rationSettings.AppSettings["ADURLSearchResultsPage"];

            // If DEV Env
            if (strEnvironment.Equals("DEV"))
            {
                intTotalHits = 24;
            }
            //string dn;
            //string cn = dn.ToUpper().Substring(dn.IndexOf("CN",0),dn.IndexOf(",",0));

            // default value;
            LTTotalRegistos.Text = "";

            // if any data to fetch
            if (Request.QueryString["Name"] != null || Request.QueryString["Department"] != null || Request.QueryString["Email"] != null || Request.QueryString["Contacto"] != null)
            {
                // creates the data table for the search result information
                objTable = new DataTable();
                objTable.Columns.Add("Number", System.Type.GetType("System.String"));
                objTable.Columns.Add("Name", System.Type.GetType("System.String"));
                objTable.Columns.Add("Department", System.Type.GetType("System.String"));
                objTable.Columns.Add("Email", System.Type.GetType("System.String"));
                objTable.Columns.Add("Contact", System.Type.GetType("System.String"));


                // Coluna com o DN	
                objTable.Columns.Add("dn", System.Type.GetType("System.String"));

                if (Request.QueryString["Name"] != null)
                {
                    strQueryName = Request.QueryString["Name"].ToString();
                }

                if (Request.QueryString["Department"] != null)
                {
                    strQueryDepartment = Request.QueryString["Department"].ToString();
                }

                if (Request.QueryString["Email"] != null)
                {
                    strQueryEmail = Request.QueryString["Email"].ToString();
                }

                if (Request.QueryString["Contacto"] != null)
                {
                    strQueryContacto = Request.QueryString["Contacto"].ToString();
                }

                // if lenght is one, them the string contains an *
                if (strQueryName.Trim().Length == 1)
                {
                    strQueryNameCriteria = "";
                }
                else
                {
                    strQueryNameCriteria = "(displayName=" + strQueryName + ")";
                }

                // if lenght is one, them the string contains an *
                if (strQueryDepartment.Trim().Length == 1)
                {
                    strQueryDepartmentCriteria = "";
                }
                else
                {
                    strQueryDepartmentCriteria = "(department=" + strQueryDepartment + ")";
                }

                if (strQueryEmail.Trim().Length == 1)
                {
                    strQueryEmailCriteria = "";
                }
                else
                {
                    strQueryEmailCriteria = "(mail=" + strQueryEmail + ")";
                }

                if (strQueryContacto.Trim().Length == 1)
                {
                    strQueryContactoCriteria = "";
                }
                else
                {
                    strQueryContactoCriteria = "(telephoneNumber=" + strQueryContacto + ")";
                }

                // ----------------------------------------

                // Get users for each Domain
                //string strDomainPath = "";
                var strSeparator = ";";
                string[] arrDomains;

                arrDomains = ConfigurationManager.AppSettings["WebServiceADSearchDomainZoom"].Split(strSeparator.ToCharArray());
                for (var intCountArr = 0; intCountArr < arrDomains.Length; intCountArr++)
                {

                    // If PROD Env
                    if (strEnvironment.Equals("PROD"))
                    {
                        //ConfigurationManager.AppSettings["WebServiceADSearchDomainZoom"]
                        getUsers(arrDomains[intCountArr], strQueryName, strQueryDepartment, strQueryNameCriteria, strQueryDepartmentCriteria, strQueryEmailCriteria, strQueryContactoCriteria);
                    }
                    else
                    {
                        // If DEV Env
                        string strName, strDepartment, strEmail, strContact;
                        strName = "Nome da pessoa";
                        strDepartment = "Departmento";
                        strEmail = "Email@domain";
                        strContact = "21 xxxx";

                        for (intCounter = 0; intCounter < 12; intCounter++)
                        {
                            //objTable = AddContactSearchResult(objTable, Convert.ToInt32(intCounter), strName + Convert.ToString(intCounter), strDepartment, strEmail, strContact);
                        }
                    }
                }

                // **************************************************************
                // Navigation BEGIN 
                // **************************************************************

                // gets the current page to display
                var intPageNo = 0;
                if (Request.QueryString["PageNo"] != null)
                {
                    intPageNo = Convert.ToInt32(Request.QueryString["PageNo"].ToString());
                }
                else
                {
                    intPageNo = 1;
                }

                LTTotalRegistos.Text = "<span class='text'>Total de registos encontrados: <b>" + intTotalHits + "</b></span>";

                // calculates the previous navigation
                if (intPageNo > 1)
                {
                    LTPrevious.Text = "<a class='linkstext' href='" + strSearchURL + "?Name=" + strQueryName + "&Department=" + strQueryDepartment + "&Email=" + strQueryEmail + "&Contacto=" + strQueryContacto;
                    LTPrevious.Text += "&PageNo=" + (intPageNo - 1) + "'><< Anterior</a>";
                }
                else
                {
                    LTPrevious.Text = "";
                }


                LTResult.Text = "<span class='tittext'>Resultados para a pesquisa de contactos com o seguinte critério:</span>";
                LTCriteria.Text = "<span class='text'><b>Nome:</b> " + strQueryName + " : <b>Departmento:</b> " + strQueryDepartment;
                LTCriteria.Text += "<br><b>E-mail:</b> " + strQueryEmail + " : <b>Contacto:</b> " + strQueryContacto + "</span>";
                // handles the number of pages in the current result set
                var intTotalPages = 0;
                if (intTotalHits > intMaxNumberResults)
                {
                    intTotalPages = Convert.ToInt32(intTotalHits / intMaxNumberResults);
                    if ((intTotalHits % intMaxNumberResults) != 0)
                    {
                        intTotalPages = intTotalPages + 1;
                    }
                }
                else
                {
                    intTotalPages = 1;
                }

                // handles the pages navegation
                LTPages.Text = "<span class='linkstext'>| </span>";
                for (intCounter = 1; intCounter <= intTotalPages; intCounter++)
                {
                    // if not the current page
                    if (intCounter != intPageNo)
                    {
                        LTPages.Text = LTPages.Text + "<a class='linkstext' href='" + strSearchURL + "?Name=" + strQueryName + "&Department=" + strQueryDepartment + "&Email=" + strQueryEmail + "&Contacto=" + strQueryContacto;
                        LTPages.Text += "&PageNo=" + intCounter + "'>" + intCounter + "</a><span class='linkstext'> | </span>";
                    }
                    else
                    {
                        // if the current page
                        LTPages.Text = LTPages.Text + "<span class='text' ><b>" + intCounter + "</b> | </span>";
                    }
                }
                // calculates the next navigation
                if (intPageNo < intTotalPages)
                {
                    LTNext.Text = "<a class='linkstext' href='" + strSearchURL + "?Name=" + strQueryName + "&Department=" + strQueryDepartment + "&Email=" + strQueryEmail + "&Contacto=" + strQueryContacto;
                    LTNext.Text += "&PageNo=" + (intPageNo + 1) + "'>Próximo >></a>";
                }
                else
                {
                    LTNext.Text = "";
                }

                intFirstRecord = (intPageNo * intMaxNumberResults) - intMaxNumberResults;
                intLastRecord = intPageNo * intMaxNumberResults;

                // **************************************************************
                // END OF Navigation 
                // **************************************************************



                DataView myDataView;
                myDataView = objTable.DefaultView;
                myDataView.Sort = "Name ASC";

                var objFinalTable = new DataTable();
                objFinalTable.Columns.Add("Number", System.Type.GetType("System.String"));
                objFinalTable.Columns.Add("Name", System.Type.GetType("System.String"));
                objFinalTable.Columns.Add("Department", System.Type.GetType("System.String"));
                objFinalTable.Columns.Add("Email", System.Type.GetType("System.String"));
                objFinalTable.Columns.Add("Contact", System.Type.GetType("System.String"));
                objFinalTable.Columns.Add("dn", System.Type.GetType("System.String"));


                intCounter = 0;
                foreach (DataRow myrow in myDataView.Table.Rows)
                {
                    if (intCounter >= intLastRecord) { break; } // ????
                    if (intCounter >= intFirstRecord && intCounter < myDataView.Table.Rows.Count)
                    {
                        objFinalTable = AddContactSearchResult(objFinalTable, Convert.ToInt32(myrow["Number"]), myrow["Name"].ToString(), myrow["Department"].ToString(), myrow["Email"].ToString(), myrow["Contact"].ToString(), myrow["dn"].ToString());
                    }
                    intCounter = intCounter + 1;
                }

                // sets the data to the repeater object
                ListADListResults.DataSource = objFinalTable.DefaultView;
                ListADListResults.DataBind();
            }

            // Binds the data
            Page.DataBind();
            //		} catch (Exception ex) {
            //		
            //		}
        }

        // Searches the specified field int the array of data
        private int FindData(attr[] arrayData, string strFieldData)
        {
            if (arrayData != null)
            {
                for (var intCounter = 0; intCounter < arrayData.Length; intCounter++)
                {
                    if (arrayData[intCounter].name.ToLower() == strFieldData.ToLower()) return intCounter;
                }
            }
            return -1;
        }

        private DataTable AddContactSearchResult(DataTable objTable, int intNumber, string strName, string strDepartment, string strEmail, string strContact, string dn)
        {
            var objDataRow = objTable.NewRow();
            objDataRow["Number"] = Convert.ToString(intNumber);
            var cn = dn.ToUpper().Substring(dn.IndexOf("CN=", 0) + 3, dn.IndexOf(",", 0) - 3);
            var tmpDN = Convert.ToBase64String(Encoding.Unicode.GetBytes(dn));
            var anchor = "<a class='linkstext' href='ADuserDetail.aspx?cn=" + cn + "&host=&dn=" + tmpDN + "'>" + strName + "</a>";

            objDataRow["Name"] = anchor;//strName;
            objDataRow["Department"] = strDepartment;
            objDataRow["Email"] = strEmail;
            objDataRow["Contact"] = strContact;

            objDataRow["dn"] = dn;

            objTable.Rows.Add(objDataRow);
            return objTable;
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
