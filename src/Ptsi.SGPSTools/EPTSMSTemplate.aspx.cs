using PTelecom.Intranet;
using PTIntranetSGPS.ptsgps_spsdev01;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WindowsApplication1.ptsgps_spsdev011;

namespace SGPStools.Templates
{


    /// <summary>
    /// Summary description for SMSTemplate.
    /// </summary>
    public class EPTSMSTemplate : Page
    {
        public const int ERR_NONE = 0;
        public const int SUCCESS = 1;
        public const int ERR_PROBLEM_SENDING_SMS_TO_GATEWAY = 2;
        public const int ERR_RECEIVER_NOT_REGISTERED_IN_AD = 3;
        public const int ERR_SENDER_NOT_REGISTERED_IN_AD = 4;
        public const int ERR_REGISTER_IN_CONTACT_DIRECTORY_FIRST = 5;


        public string sAlertMsg = "";
        public int statePage = ERR_NONE;
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
        public bool hasSent = false;
        public string USERDN;

        public string senderCN_Name;
        public string senderEmail;
        public string senderNr_tel;
        protected DataTable objTable;
        protected RequiredFieldValidator rfvNumDestino;
        protected RegularExpressionValidator regexNumDestino1;
        protected RegularExpressionValidator regexNumDestino2;
        protected HtmlInputText numDestino;
        protected RequiredFieldValidator rfvMsg;
        protected HtmlTextArea msg;
        protected LinkButton SearchUsersButton;
        protected ValidationSummary ValidationSum;
        protected CheckBox chkMail;
        protected string[] queryFields = new String[14];

        public string AlertMsg
        {
            get
            {
                return sAlertMsg;
            }
            set
            {
                sAlertMsg = value;
            }
        }

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
            //SqlCommand sc = new SqlCommand("[ADCorpPT].[ADCorpPT_SA].[adcorppt_getLargeAccount]", oConn);
            //SqlCommand sc = new SqlCommand("[ADCorpPT].[dbo].[adcorppt_getLargeAccount]", oConn);

            var sc = new SqlCommand("adcorppt_getLargeAccount", oConn);

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
                if (string.Equals(sr.attr[intCounter].name, key, StringComparison.CurrentCultureIgnoreCase))
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
            var req = new searchRequest
            {
                dn = strDomainPath,
                scope = "wholeSubtree",

                filter = "(&(objectClass=user)" + strQueryNameCriteria + ")",
                derefAliases = "derefAlways"
            };

            //Atributos para Pesquisar
            int iFields;

            req.attributes = (attribute[])Array.CreateInstance(typeof(attribute), queryFields.Length);
            for (iFields = 0; iFields < queryFields.Length; iFields++)
            {
                req.attributes[iFields] = new attribute
                {
                    name = queryFields[iFields]
                };
            }

            //set SOAP headers
            op.IdentityCredentialsValue = new IdentityCredentials
            {
                IdentityName = ConfigurationManager.AppSettings["ADSearchUserName"],
                IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"]
            };

            op.UserCredentialsValue = new UserCredentials
            {
                Username = "a",
                Password = "a",
                Domain = "a"
            };

            //object[] items = new object[] {req};
            //object[] ret = op.batchRequest(items);

            var ret = new ADSearch().ProcessSearchRequest(req, op.IdentityCredentialsValue);

            var resp = ret as searchResponse;
            var resp2 = ret as errorResponse;

            if (resp == null || resp.Items == null) return;

            var valueFields = new string[queryFields.Length];

            foreach (var t in resp.Items)
            {
                var sr = (searchResultEntry)t;


                USERDN = sr.dn;
                for (iFields = 0; iFields < queryFields.Length; iFields++)
                {
                    var idx = GetQueryFieldIdx(queryFields[iFields], sr);

                    if (idx >= 0)
                    {
                        if (queryFields[iFields].Equals("mail"))
                        {
                            if (!String.IsNullOrEmpty(sr.attr[idx].value[0]))
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

        private bool LoadSenderData(string cnName)
        {
            /*try
			{*/
            //Trying to get User Data - Web Service Invocation
            PopulateQueryFieldsAndDataTableFields();

            var strQueryNameCriteria = "(cn=" + cnName + ")";
            getUsers(ref objTable, ConfigurationManager.AppSettings["WebServiceADSearchDomain"], strQueryNameCriteria, queryFields);


            if (objTable.Rows.Count >= 1)
            {
                for (var i = 0; i < objTable.Rows.Count; i++)
                {
                    var oColumns = objTable.Rows[i];
                    if (oColumns?["CN"] != null)
                    {
                        senderCN_Name = oColumns["CN"]?.ToString();
                        senderEmail = oColumns["mail"]?.ToString();
                        //dominioFrom = oColumns["dn"]?.ToString();
                        senderNr_tel = oColumns["mobile"]?.ToString();
                    }
                    if (!String.IsNullOrEmpty(senderEmail))
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }


        }

        void PopulateQueryFieldsAndDataTableFields()
        {
            objTable = new DataTable();
            objTable.Columns.Add("CN", Type.GetType("System.String"));
            objTable.Columns.Add("displayName", Type.GetType("System.String"));
            objTable.Columns.Add("Department", Type.GetType("System.String"));
            objTable.Columns.Add("mail", Type.GetType("System.String"));
            objTable.Columns.Add("telephoneNumber", Type.GetType("System.String"));
            objTable.Columns.Add("dn", Type.GetType("System.String"));

            objTable.Columns.Add("company", Type.GetType("System.String"));
            objTable.Columns.Add("otherTelephone", Type.GetType("System.String"));
            objTable.Columns.Add("mobile", Type.GetType("System.String"));
            objTable.Columns.Add("facsimileTelephoneNumber", Type.GetType("System.String"));
            objTable.Columns.Add("title", Type.GetType("System.String"));
            objTable.Columns.Add("street", Type.GetType("System.String"));
            objTable.Columns.Add("streetAddress", Type.GetType("System.String"));
            objTable.Columns.Add("notes", Type.GetType("System.String"));



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
        }

        private bool LoadUserData(string psPhone)
        {
            var blnDBCheck = true;

            //Check mobile number in PhneList DataBase
            try
            {
                var spName = ConfigurationManager.AppSettings["EPTsp_CheckPhoneNumber"];
                var connStrPhoneListDb = ConfigurationManager.AppSettings["EPTConnectionStrPhoneListDB"];
                var oConn = new SqlConnection(Encription.DecryptToken(connStrPhoneListDb));

                oConn.Open();
                var sc = new SqlCommand(spName, oConn);

                sc.CommandType = CommandType.StoredProcedure;

                var oParamPhoneNumber = sc.Parameters.Add("@intPhoneNumber", SqlDbType.Int, 4);
                oParamPhoneNumber.Direction = ParameterDirection.Input;
                oParamPhoneNumber.Value = psPhone;

                var result = sc.ExecuteScalar();

                if (result == DBNull.Value)
                    return true;
                else
                {
                    blnDBCheck = false;
                }

            }
            catch
            {
                blnDBCheck = false;
            }

            if (blnDBCheck) return false;

            /*try
				{*/
            //Trying to get User Data - Web Service Invocation
            PopulateQueryFieldsAndDataTableFields();


            string strDN;
            var strQueryNameCriteria = "";
            if (String.IsNullOrEmpty(Request.QueryString["dn"]))
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
                var oColumns = objTable.Rows[0];

                if (oColumns?["CN"] != null)
                {

                    CN_Name = oColumns["CN"]?.ToString();
                    email = oColumns["mail"]?.ToString();
                    //dominioFrom = oColumns["dn"]?.ToString();
                    nr_tel = oColumns["mobile"]?.ToString();

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

        public string CreateErrorImage(string imagePath, string altText, string width, string height)
        {
            return String.Format("<img src='{0}' title='{1}' witdh='{2}' height='{3}'></img>", imagePath, altText, width, height);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            sAlertMsg = "";
            rfvNumDestino.Text = CreateErrorImage("/SGPSTools/img/icon_bug.gif", "Não inseriu nº de telemóvel de destino", "11", "11");
            regexNumDestino1.Text = CreateErrorImage("/SGPSTools/img/icon_bug.gif", "O telemóvel que inseriu não é válido", "11", "11");
            regexNumDestino2.Text = CreateErrorImage("/SGPSTools/img/icon_bug.gif", "O telemóvel que inseriu não é da MEO", "11", "11");
            rfvMsg.Text = CreateErrorImage("/SGPSTools/img/icon_bug.gif", "Por favor insira a mensagem que pretende enviar", "11", "11");

            smsURL = ConfigurationManager.AppSettings["SMSPageEPT"];
            MAXSIZE = ConfigurationManager.AppSettings["smsMaxSize"];

            nr_tel = Request.QueryString["nr_tel"];

            if (User.Identity.IsAuthenticated)
            {
                email = Request.QueryString["mail"];
                //dominioFrom = User.Identity.Name.Substring( 0,User.Identity.Name.IndexOf("\\")) + ".corppt.com";

                dominioTo = Request.QueryString["dominioTo"];
                var company = User.Identity.Name.Substring(0, User.Identity.Name.IndexOf("\\", StringComparison.Ordinal));
                cn = User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) != -1 ? User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) + 1) : User.Identity.Name;

                if (String.IsNullOrEmpty(Request.QueryString["isOut"]))
                {
                    Response.Redirect("EPTSMSTemplate.aspx?" + Request.QueryString + "&isOut=1");
                }

                if (!hasSent)
                {
                    statePage = ERR_NONE;

                    if (String.IsNullOrEmpty(nr_tel) && String.IsNullOrEmpty(Request.QueryString["warningFalse"])) {
                        statePage = ERR_REGISTER_IN_CONTACT_DIRECTORY_FIRST;
                    }

                    if (IsPostBack)
                    {
                        Page.Validate();
                        if (!Page.IsValid) return;

                        statePage = SUCCESS;
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

                            if (String.IsNullOrEmpty(senderEmail))
                            {
                                statePage = ERR_SENDER_NOT_REGISTERED_IN_AD;
                                retStr = "-1";
                            }
                            else
                            {
                                if (LoadUserData(Request.Form["numDestino"]))
                                {
                                    var la = getLargeAccount(dominioFrom);
                                    var body = Request.Form["msg"];
                                    body = body.Replace(Convert.ToString((char)13) + Convert.ToString((char)10), " ");

                                    //									//se o senderNt_tel for vazio então o valor terá de ser o da large account
                                    //									if (senderNr_tel.Trim() == string.Empty)
                                    //										senderNr_tel = la;
                                    //
                                    //									//novo método para enviar sms's com nr de origem
                                    //									//retStr = smsObj.sendSMS(la,la, dominioFrom, dominioTo, Request.Form["numDestino"], body, company, senderCN_Name,senderEmail);
                                    //									if (chkMail.Checked)
                                    //										retStr = smsObj.sendSMSV2(la,la, dominioFrom, dominioTo, Request.Form["numDestino"], senderNr_tel, body, company, senderCN_Name,senderEmail);
                                    //									else
                                    //										retStr = smsObj.sendSMSV2(la,la, dominioFrom, dominioTo, Request.Form["numDestino"], senderNr_tel, body, company, senderCN_Name,"");

                                    // mendes 11-08-2008 ---------------------------------------------
                                    var remetente = senderNr_tel.Replace(" ", "");
                                    var destinatario = Request.Form["numDestino"].Replace(" ", "");

                                    //se o senderNt_tel for vazio então o valor terá de ser o da large account
                                    if ((remetente == string.Empty) || (remetente == "-"))
                                        remetente = la;

                                    if (remetente.StartsWith("+351"))
                                        remetente = remetente.Substring(4);

                                    if (destinatario.StartsWith("+351"))
                                        destinatario = destinatario.Substring(4);

                                    //novo método para enviar sms's com nr de origem
                                    //retStr = smsObj.sendSMS(la,la, dominioFrom, dominioTo, Request.Form["numDestino"], body, company, senderCN_Name,senderEmail);
                                    smsObj.UseDefaultCredentials = true;
                                    if (chkMail.Checked)
                                        retStr = smsObj.sendSMSV2(la, la, dominioFrom, dominioTo, destinatario, remetente, body, company, senderCN_Name, senderEmail);
                                    else
                                        retStr = smsObj.sendSMSV2(la, la, dominioFrom, dominioTo, destinatario, remetente, body, company, senderCN_Name, "");
                                    //------------------------------------------------------------------

                                }
                                else
                                {
                                    statePage = ERR_RECEIVER_NOT_REGISTERED_IN_AD;
                                    retStr = "-1";
                                }
                            }
                        }
                        else
                        {
                            statePage = ERR_SENDER_NOT_REGISTERED_IN_AD;
                            retStr = "-1";
                        }
                        hasSent = true;

                        if (!String.IsNullOrEmpty(retStr) && retStr.IndexOf("0", StringComparison.Ordinal) == -1 && statePage == SUCCESS)
                        {
                            statePage = ERR_PROBLEM_SENDING_SMS_TO_GATEWAY;
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
            this.SearchUsersButton.Click += new System.EventHandler(this.SearchUsersButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void SearchUsersButton_Click(object sender, EventArgs e)
        {
        }
    }
}
