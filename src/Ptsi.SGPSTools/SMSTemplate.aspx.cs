using PTelecom.Intranet;
using PTIntranetSGPS.ptsgps_spsdev01;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WindowsApplication1.ptsgps_spsdev011;



namespace SGPStools.Templates
{
    /// <summary>
    /// Summary description for SMSTemplate.
    /// </summary>
    public class SMSTemplate : System.Web.UI.Page
    {

        public int statePage = 0;
        public string warningRow;
        public string numtoSend = "";
        public string smsURL;
        public string CN_Name;
        public string nr_tel;
        public string email;
        public string dominioFrom;
        public string dominioTo;
        public string cn;
        public string MAXSIZE;
        public int hasSent = 0;
        public string USERDN;

        public string senderCN_Name;
        public string senderEmail;
        public string senderNr_tel;
        protected DataTable objTable;
        protected string[] queryFields = new String[14];

        /*
         * CREATE PROCEDURE adcorppt_getLargeAccount
         *		@dominio varchar(150)
         * AS
         *		select largeAccount ADCorpPT.ADCorpPT_Domains where descricao = @dominio
         * GO
         * 
         * 
         * 
         */



        private string getLargeAccount(string dominio)
        {
            //			try {
            var oConn = new SqlConnection(Encription.DecryptToken(ConfigurationManager.AppSettings["ConnectionString"]));

            oConn.Open();
            var sc = new SqlCommand("[ADCorpPT].[ADCorpPT_SA].[adcorppt_getLargeAccount]", oConn);

            sc.Connection = oConn;
            sc.CommandType = CommandType.StoredProcedure;

            var oParamDomino = sc.Parameters.Add("@dominio", SqlDbType.VarChar, 150);
            oParamDomino.Direction = ParameterDirection.Input;
            oParamDomino.Value = dominio;

            var myReader = sc.ExecuteReader();

            if (myReader.Read())
            {
                return myReader.GetString(0);
            }
            return null;

            // 			return "2020";

            //			} catch(Exception) {
            //				return null;
            //			}				
        }



        private int GetQueryFieldIdx(string key, searchResultEntry sr)
        {
            for (var intCounter = 0; intCounter < sr.attr.Length; intCounter++)
                if (sr.attr[intCounter].name.ToLower() == key.ToLower())
                    return intCounter;

            return -1;
        }


        private DataTable AddContactSearchResult(DataTable objTable, string[] valueFields)
        {
            var objDataRow = objTable.NewRow();

            for (var i = 0; i < queryFields.Length; i++)
                objDataRow[queryFields[i].ToString()] = valueFields[i];

            objTable.Rows.Add(objDataRow);

            return objTable;
        }


        // Function to getUsers from the specified domainPath
        private void getUsers(ref DataTable oTable, string strDomainPath, string strQueryNameCriteria, string[] queryFields)
        {
            var op = new UserMng();

            //create search request
            var req = new searchRequest();

            req.dn = strDomainPath;
            req.scope = "wholeSubtree";

            req.filter = "(&(objectClass=user)" + strQueryNameCriteria + ")";
            req.derefAliases = "derefAlways";

            //Atributos para Pesquisar
            int iFields;

            req.attributes = (attribute[])Array.CreateInstance(typeof(attribute), queryFields.Length);
            for (iFields = 0; iFields < queryFields.Length; iFields++)
            {
                req.attributes[iFields] = new attribute();
                req.attributes[iFields].name = queryFields[iFields];
            }

            //set SOAP headers
            op.IdentityCredentialsValue = new IdentityCredentials();
            op.IdentityCredentialsValue.IdentityName = ConfigurationManager.AppSettings["ADSearchUserName"];
            op.IdentityCredentialsValue.IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"];

            op.UserCredentialsValue = new UserCredentials();
            op.UserCredentialsValue.Username = "a";
            op.UserCredentialsValue.Password = "a";
            op.UserCredentialsValue.Domain = "a";

            var items = new object[] { req };
            var ret = op.batchRequest(items);

            var resp = ret[0] as searchResponse;
            var resp2 = ret[0] as errorResponse;

            int idx;
            if (resp != null && resp.Items != null)
            {
                var valueFields = new string[queryFields.Length];

                for (var intCounter = 0; intCounter < resp.Items.Length; intCounter++)
                {
                    var sr = (searchResultEntry)resp.Items[intCounter];


                    USERDN = sr.dn;
                    for (iFields = 0; iFields < queryFields.Length; iFields++)
                    {
                        idx = GetQueryFieldIdx(queryFields[iFields], sr);

                        if (idx >= 0)
                        {
                            if (queryFields[iFields].Equals("mail"))
                            {
                                if (sr.attr[idx].value[0].ToString() != null && !sr.attr[idx].value[0].ToString().Equals(""))
                                {
                                    var upperStr = sr.dn.ToUpper();
                                    var str2 = upperStr.Substring(upperStr.IndexOf("DC=", StringComparison.Ordinal) + 3);
                                    var endInt = str2.IndexOf("DC=", StringComparison.Ordinal) - 1;
                                    dominioTo = str2.Substring(0, endInt) + ".corppt.com";
                                }
                            }
                            valueFields[iFields] = sr.attr[idx].value[0].ToString();
                        }
                        else
                        {
                            valueFields[iFields] = "";
                        }
                    }

                    oTable = AddContactSearchResult(oTable, valueFields);
                }
            }
        }


        private bool LoadSenderData(string cnName)
        {
            /*try
			{*/
            //Trying to get User Data - Web Service Invocation
            objTable = new DataTable();
            objTable.Columns.Add("CN", System.Type.GetType("System.String"));
            objTable.Columns.Add("displayName", System.Type.GetType("System.String"));
            objTable.Columns.Add("Department", System.Type.GetType("System.String"));
            objTable.Columns.Add("mail", System.Type.GetType("System.String"));
            objTable.Columns.Add("telephoneNumber", System.Type.GetType("System.String"));
            objTable.Columns.Add("dn", System.Type.GetType("System.String"));

            objTable.Columns.Add("company", System.Type.GetType("System.String"));
            objTable.Columns.Add("otherTelephone", System.Type.GetType("System.String"));
            objTable.Columns.Add("mobile", System.Type.GetType("System.String"));
            objTable.Columns.Add("facsimileTelephoneNumber", System.Type.GetType("System.String"));
            objTable.Columns.Add("title", System.Type.GetType("System.String"));
            objTable.Columns.Add("street", System.Type.GetType("System.String"));
            objTable.Columns.Add("streetAddress", System.Type.GetType("System.String"));
            objTable.Columns.Add("notes", System.Type.GetType("System.String"));



            queryFields[0] = "cn"; //Logon Name
            queryFields[1] = "displayName"; //Nome Completo
            queryFields[2] = "department"; //Departamento
            queryFields[3] = "mail"; //E-mail
            queryFields[4] = "telephoneNumber"; //Telefone 1
            queryFields[5] = "dn"; //Domínio

            queryFields[6] = "company"; //Companhia
            queryFields[7] = "otherTelephone"; //Telefone 2
            queryFields[8] = "mobile"; //Telemovel
            queryFields[9] = "facsimileTelephoneNumber"; //Fax
            queryFields[10] = "title"; //Titulo
            queryFields[11] = "street"; //Rua
            queryFields[12] = "streetAddress"; //Morada de Rua.
            queryFields[13] = "notes"; //Notas.


            var strQueryNameCriteria = "(cn=" + cnName + ")";
            getUsers(ref objTable, ConfigurationManager.AppSettings["WebServiceADSearchDomain"], strQueryNameCriteria, queryFields);


            if (objTable.Rows.Count >= 1)
            {
                for (var i = 0; i < objTable.Rows.Count; i++)
                {
                    var oColumns = objTable.Rows[i].ItemArray;
                    if (oColumns.GetValue(0).ToString() != null)
                    {
                        senderCN_Name = oColumns.GetValue(0).ToString();
                        senderEmail = oColumns.GetValue(3).ToString();
                        //dominioFrom = oColumns.GetValue(5).ToString();
                        senderNr_tel = oColumns.GetValue(8).ToString();
                    }
                    if (!senderEmail.Equals("") && senderEmail != null)
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }


            /*}
			catch( Exception ){ return false; }*/
        }


        private bool LoadUserData(string psPhone)
        {
            var blnDBCheck = true;

            //Check mobile number in PhneList DataBase
            try
            {
                var spName = ConfigurationManager.AppSettings["sp_CheckPhoneNumber"];
                var ConnStrPhoneListDB = ConfigurationManager.AppSettings["ConnectionStrPhoneListDB"];
                var oConn = new SqlConnection(Encription.DecryptToken(ConnStrPhoneListDB));

                oConn.Open();
                var sc = new SqlCommand(spName, oConn);

                sc.CommandType = CommandType.StoredProcedure;

                var oParamPhoneNumber = sc.Parameters.Add("@strNumber", SqlDbType.VarChar, 50);
                oParamPhoneNumber.Direction = ParameterDirection.Input;
                oParamPhoneNumber.Value = psPhone;

                var myReader = sc.ExecuteReader();
                if (myReader.Read())
                    // OK - the number is on DB
                    return true;
                else
                {
                    // the number is NOT on DB - go check the AD
                    blnDBCheck = false;
                }

            }
            catch (Exception e)
            {
                blnDBCheck = false;
            }


            if (!blnDBCheck)
            {
                /*try
				{*/
                //Trying to get User Data - Web Service Invocation
                objTable = new DataTable();
                objTable.Columns.Add("CN", System.Type.GetType("System.String"));
                objTable.Columns.Add("displayName", System.Type.GetType("System.String"));
                objTable.Columns.Add("Department", System.Type.GetType("System.String"));
                objTable.Columns.Add("mail", System.Type.GetType("System.String"));
                objTable.Columns.Add("telephoneNumber", System.Type.GetType("System.String"));
                objTable.Columns.Add("dn", System.Type.GetType("System.String"));

                objTable.Columns.Add("company", System.Type.GetType("System.String"));
                objTable.Columns.Add("otherTelephone", System.Type.GetType("System.String"));
                objTable.Columns.Add("mobile", System.Type.GetType("System.String"));
                objTable.Columns.Add("facsimileTelephoneNumber", System.Type.GetType("System.String"));
                objTable.Columns.Add("title", System.Type.GetType("System.String"));
                objTable.Columns.Add("street", System.Type.GetType("System.String"));
                objTable.Columns.Add("streetAddress", System.Type.GetType("System.String"));
                objTable.Columns.Add("notes", System.Type.GetType("System.String"));



                queryFields[0] = "cn"; //Logon Name
                queryFields[1] = "displayName"; //Nome Completo
                queryFields[2] = "department"; //Departamento
                queryFields[3] = "mail"; //E-mail
                queryFields[4] = "telephoneNumber"; //Telefone 1
                queryFields[5] = "dn"; //Domínio

                queryFields[6] = "company"; //Companhia
                queryFields[7] = "otherTelephone"; //Telefone 2
                queryFields[8] = "mobile"; //Telemovel
                queryFields[9] = "facsimileTelephoneNumber"; //Fax
                queryFields[10] = "title"; //Titulo
                queryFields[11] = "street"; //Rua
                queryFields[12] = "streetAddress"; //Morada de Rua.
                queryFields[13] = "notes"; //Notas.



                string strDN;
                var strQueryNameCriteria = "";
                if (Request.QueryString["dn"] == null || Request.QueryString["dn"].Equals(""))
                {
                    strDN = ConfigurationManager.AppSettings["WebServiceADSearchDomain"];
                    strQueryNameCriteria = "(mobile=" + psPhone + ")";
                }
                else
                {
                    strDN = Request.QueryString["dn"];
                    strDN = Encoding.Unicode.GetString(Convert.FromBase64String(strDN));
                    strQueryNameCriteria = "";
                }

                getUsers(ref objTable, strDN, strQueryNameCriteria, queryFields);

                if (objTable.Rows.Count >= 1)
                {
                    var oColumns = objTable.Rows[0].ItemArray;
                    if (oColumns.GetValue(0).ToString() != null)
                    {

                        CN_Name = oColumns.GetValue(0).ToString();
                        email = oColumns.GetValue(3).ToString();
                        //dominioTo = oColumns.GetValue(5).ToString();
                        nr_tel = oColumns.GetValue(8).ToString();

                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }


                /*}
				catch( Exception ){ return false; }*/
            }
            return false;
        }



        private void Page_Load(object sender, System.EventArgs e)
        {







            smsURL = ConfigurationManager.AppSettings["SMSPage"];
            MAXSIZE = ConfigurationManager.AppSettings["smsMaxSize"];
            // Put user code to initialize the page here
            nr_tel = Request.QueryString["nr_tel"];





            if (User.Identity.IsAuthenticated)
            {
                email = Request.QueryString["mail"];
                //dominioFrom = User.Identity.Name.Substring( 0,User.Identity.Name.IndexOf("\\")) + ".corppt.com";

                dominioTo = Request.QueryString["dominioTo"];
                var company = User.Identity.Name.Substring(0, User.Identity.Name.IndexOf("\\", StringComparison.Ordinal));
                if (User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) != -1)
                    cn = User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) + 1);
                else
                    cn = User.Identity.Name;



                if (Request.QueryString["isOut"] == null || Request.QueryString["isOut"] == "")
                {
                    Response.Redirect("SMSTemplate.aspx?" + Request.QueryString.ToString() + "&isOut=1");
                }



                if (hasSent == 0)
                {
                    statePage = 0;

                    if ((nr_tel == null || nr_tel.Equals("")) && (Request.QueryString["warningFalse"] == null || Request.QueryString["warningFalse"].Equals("")))
                    {
                        statePage = 5;
                    }

                    if (this.IsPostBack)
                    {
                        statePage = 1;
                        var smsObj = new Service1();
                        numtoSend = Request.Form["numDestino"];
                        //string retStr = smsObj.sendSMS("CONTA",dominioFrom, dominioTo,Request.Form["numDestino"],Request.Form["mensagem"]," ",cn);

                        var retStr = "";

                        if (LoadSenderData(cn))
                        {
                            //Preenchimento do CN e do DominioFrom
                            var dn = USERDN;

                            var upperStr = dn.ToUpper();
                            var str2 = upperStr.Substring(upperStr.IndexOf("DC=", StringComparison.Ordinal) + 3);
                            var endInt = str2.IndexOf("DC=", StringComparison.Ordinal) - 1;
                            dominioFrom = str2.Substring(0, endInt) + ".corppt.com";

                            //dominioFrom = dominioFrom.ToLower();



                            if (senderEmail.Equals("") || senderEmail == null)
                            {
                                statePage = 4;
                                retStr = "-1";
                            }
                            else
                            {
                                if (LoadUserData(Request.Form["numDestino"]))
                                {

#if DEBUG
									var la = "2525";
#else
                                    string la = getLargeAccount(dominioFrom);
#endif
                                    var body = Request.Form["msg"];
                                    body = body.Replace(Convert.ToString((char)13) + Convert.ToString((char)10), " ");

                                    // retStr = smsObj.sendSMS(la,la, dominioFrom, dominioTo, Request.Form["numDestino"], body, company, senderCN_Name,senderEmail);

                                    // mendes 11-08-2008 ---------------------------------------------
                                    var destinatario = Request.Form["numDestino"].Replace(" ", "");

                                    if (destinatario.StartsWith("+351"))
                                        destinatario = destinatario.Substring(4);

                                    retStr = smsObj.sendSMS(la, la, dominioFrom, dominioTo, destinatario, body, company, senderCN_Name, senderEmail);
                                    // -------------------------------------------------------------------

                                }
                                else
                                {
                                    statePage = 3;
                                    retStr = "-1";
                                }
                            }
                        }
                        else
                        {
                            statePage = 4;
                            retStr = "-1";
                        }
                        hasSent = 1;

                        if (retStr.IndexOf("0", StringComparison.Ordinal) == -1 && statePage == 1)
                        {
                            statePage = 2;
                        }
                    }
                }
            }

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
