using PTelecom.Intranet;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using WindowsApplication1.ptsgps_spsdev011;
namespace PTIntranetSGPS.UserManagent
{
    /// <summary>
    /// Summary description for ChangeUserData.
    /// </summary>
    public class sss2 : System.Web.UI.Page
    {
        protected string logonName, domain;
        protected string host;
        protected string dn;
        public int isError;
        public int isSuccess;
        protected DataTable objTable;
        protected System.Web.UI.HtmlControls.HtmlGenericControl btnACT;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.TextBox txtNomeCompleto;
        protected System.Web.UI.WebControls.Label Label18;
        protected System.Web.UI.WebControls.TextBox txtDominio;


        protected System.Web.UI.WebControls.Label Label6;
        protected System.Web.UI.WebControls.TextBox txtEmpresa;
        protected System.Web.UI.WebControls.Label Label8;
        protected System.Web.UI.WebControls.TextBox txtTelefone1;
        protected System.Web.UI.WebControls.TextBox txtTelefone2;
        protected System.Web.UI.WebControls.Label Label10;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.WebControls.TextBox txtTelemovel;
        protected System.Web.UI.WebControls.Label Label9;
        protected System.Web.UI.WebControls.TextBox txtFax;
        protected System.Web.UI.WebControls.Label Label11;
        protected System.Web.UI.WebControls.TextBox txtEmail;
        protected System.Web.UI.WebControls.Label Label12;
        protected System.Web.UI.WebControls.TextBox txtCategoria;
        protected System.Web.UI.WebControls.Label Label13;
        protected System.Web.UI.WebControls.TextBox txtLocalTrabalho;
        protected System.Web.UI.WebControls.Label Label14;
        protected System.Web.UI.WebControls.TextBox txtMorada;
        protected System.Web.UI.WebControls.Label Label17;
        protected System.Web.UI.WebControls.TextBox txtObservacoes;
        protected System.Web.UI.WebControls.Label Label19;
        protected System.Web.UI.WebControls.TextBox txtDataUltimaActualizacao;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtDepartamento;
        protected string[] queryFields = new String[14];

        private int GetQueryFieldIdx(string key, searchResultEntry sr)
        {
            for (var intCounter = 0; intCounter < sr.attr.Length; intCounter++)
                if (sr.attr[intCounter].name.ToLower() == key.ToLower())
                    return intCounter;

            return -1;
        }

        // Function to getUsers from the specified domainPath
        private void getUsers(ref DataTable oTable, string strDomainPath, string strQueryNameCriteria, string[] queryFields)
        {
            var op = new UserMng();

            //create search request
            var req = new searchRequest();

            req.dn = strDomainPath;
            req.scope = "wholeSubtree";

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

                    for (iFields = 0; iFields < queryFields.Length; iFields++)
                    {
                        idx = GetQueryFieldIdx(queryFields[iFields], sr);
                        if (idx >= 0)
                            valueFields[iFields] = sr.attr[idx].value[0].ToString();
                        else
                            valueFields[iFields] = "";
                    }

                    oTable = AddContactSearchResult(oTable, valueFields);
                }
            }
        }

        private bool IsAuthenticated()
        {
            return (User.Identity.IsAuthenticated);
        }

        private bool IsUser(string cn)
        {
            if (User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) != -1)
            {
                var windowsUser = User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) + 1);
                if (cn.ToUpper().Equals(windowsUser.ToUpper()))
                    return true;
                else
                    return false;
            }
            else
            {
                var windowsUser = User.Identity.Name;
                if (cn.ToUpper().Equals(windowsUser.ToUpper()))
                    return true;
                else
                    return false;
            }
        }

        private bool SaveUserData(Page psPage, string psLogonName, string psDomain, string dn)
        {
            /*try
			{*/
            //Trying to put User Data - Web Service Invocation
            //Get User Access
            var op = new UserMng();

            var encriptedPass = Encription.EncryptToken(txtPassword.Text);

            op.IdentityCredentialsValue = new IdentityCredentials();
            op.IdentityCredentialsValue.IdentityName = ConfigurationManager.AppSettings["ADSearchUserName"];
            op.IdentityCredentialsValue.IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"];



            op.UserCredentialsValue = new UserCredentials();
            op.UserCredentialsValue.Username = User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\", StringComparison.Ordinal) + 1); //nSettings.AppSettings["ADAdminUserPass"];
            op.UserCredentialsValue.Password = encriptedPass;
            op.UserCredentialsValue.Domain = psDomain;//User.Identity.Name.Substring( 0,User.Identity.Name.IndexOf("\\") );

            //Get Request Access
            var req = new modifyRequest();
            req.modification = new Modification[8];
            req.dn = dn;

            //Para cada campo
            req.modification[0] = new Modification();
            req.modification[0].name = "telephoneNumber";
            req.modification[0].operation = "replace";
            if (txtTelefone1.Text.Equals("") || txtTelefone1.Text.Equals(null))
                txtTelefone1.Text = " ";
            req.modification[0].value = new string[] { txtTelefone1.Text };

            req.modification[1] = new Modification();
            req.modification[1].name = "otherTelephone";
            req.modification[1].operation = "replace";
            if (txtTelefone2.Text.Equals("") || txtTelefone2.Text.Equals(null))
                txtTelefone2.Text = " ";
            req.modification[1].value = new string[] { txtTelefone2.Text };

            req.modification[2] = new Modification();
            req.modification[2].name = "department";
            req.modification[2].operation = "replace";
            if (txtDepartamento.Text.Equals("") || txtDepartamento.Text.Equals(null))
                txtDepartamento.Text = " ";
            req.modification[2].value = new string[] { txtDepartamento.Text };

            req.modification[3] = new Modification();
            req.modification[3].name = "facsimileTelephoneNumber";
            req.modification[3].operation = "replace";
            if (txtFax.Text.Equals("") || txtFax.Text.Equals(null))
                txtFax.Text = " ";
            req.modification[3].value = new string[] { txtFax.Text };

            req.modification[4] = new Modification();
            req.modification[4].name = "title";
            req.modification[4].operation = "replace";
            if (txtCategoria.Text.Equals("") || txtCategoria.Text.Equals(null))
                txtCategoria.Text = " ";
            req.modification[4].value = new string[] { txtCategoria.Text };

            req.modification[5] = new Modification();
            req.modification[5].name = "street";
            req.modification[5].operation = "replace";
            if (txtLocalTrabalho.Text.Equals("") || txtLocalTrabalho.Text.Equals(null))
                txtLocalTrabalho.Text = " ";
            req.modification[5].value = new string[] { txtLocalTrabalho.Text };

            req.modification[6] = new Modification();
            req.modification[6].name = "streetAddress";
            req.modification[6].operation = "replace";
            if (txtMorada.Text.Equals("") || txtMorada.Text.Equals(null))
                txtMorada.Text = " ";
            req.modification[6].value = new string[] { txtMorada.Text };

            req.modification[7] = new Modification();
            req.modification[7].name = "mobile";
            req.modification[7].operation = "replace";
            if (txtTelemovel.Text.Equals("") || txtTelemovel.Text.Equals(null))
                txtTelemovel.Text = " ";
            req.modification[7].value = new string[] { txtTelemovel.Text };

            /*req.modification[8]				= new Modification();
            req.modification[8].name		= "notes";
            req.modification[8].operation	= "replace";
            if (txtDataUltimaActualizacao.Text.Equals("") || txtDataUltimaActualizacao.Text.Equals(null))
                txtTelemovel.Text = " ";
            req.modification[8].value		= new string[] {txtDataUltimaActualizacao.Text};*/

            var items = new object[] { req };
            var ret = op.batchRequest(items);

            var resp = ret[0] as modifyResponse;
            var resp2 = ret[0] as errorResponse;

            if (resp == null)
            {
                isError = 2;
                //					Response.Write(resp2.message);
                //					Response.Write(resp2.detail);
                //					Response.Write("<br><br>CN=" + psLogonName + ",CN=Users," + psDomain);
                //					Response.Write("<br><br>" + txtTelefone1.Text);
                return false;
            }
            else
            {
                Response.Write("<!--XXX " + resp.resultCode.descr + "XXX-->");
                isSuccess = 1;
            }

            return true;
            /*}
			catch( Exception ){ return false; }*/
        }

        private bool LoadUserData(Page psPage, string psLogonName, string host, string dn, string dominio, string companyName)
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
            objTable.Columns.Add("dNSHostName", System.Type.GetType("System.String"));

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
            queryFields[5] = "dNSHostName"; //Domínio

            queryFields[6] = "company"; //Companhia
            queryFields[7] = "otherTelephone"; //Telefone 2
            queryFields[8] = "mobile"; //Telemovel
            queryFields[9] = "facsimileTelephoneNumber"; //Fax
            queryFields[10] = "title"; //Titulo
            queryFields[11] = "street"; //Rua
            queryFields[12] = "streetAddress"; //Morada de Rua.
            queryFields[13] = "notes"; //Notas.

            if (dn == null)
                dn = ConfigurationManager.AppSettings["WebServiceADSearchDomain"];

            var strQueryNameCriteria = "(cn=" + psLogonName + ")";//(dNSHostName=" + host + ")";
            getUsers(ref objTable, dn, strQueryNameCriteria, queryFields);

            if (objTable.Rows.Count >= 1)
            {
                var oColumns = objTable.Rows[0].ItemArray;
                txtNomeCompleto.Text = oColumns.GetValue(1).ToString();
                txtDepartamento.Text = oColumns.GetValue(2).ToString();
                txtEmail.Text = oColumns.GetValue(3).ToString();
                txtTelefone1.Text = oColumns.GetValue(4).ToString();
                txtDominio.Text = dominio;
                txtEmpresa.Text = companyName;//oColumns.GetValue(6).ToString();
                txtTelefone2.Text = oColumns.GetValue(7).ToString();
                txtTelemovel.Text = oColumns.GetValue(8).ToString();
                txtFax.Text = oColumns.GetValue(9).ToString();
                txtCategoria.Text = oColumns.GetValue(10).ToString();
                txtLocalTrabalho.Text = oColumns.GetValue(11).ToString();
                txtMorada.Text = oColumns.GetValue(12).ToString();
                txtObservacoes.Text = oColumns.GetValue(13).ToString();
            }
            else
            {
                isError = 1;
            }


            return true;
            /*}
			catch( Exception ){ return false; }*/
        }

        /// <summary>
        /// Get the Domain and CompanyName
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="domain">return domain</param>
        /// <param name="companyName">return companyName</param>
        private void getADInfo(string cn, out string domain, out string companyName)
        {

            //string strCompanyName = "";
            domain = "";
            companyName = "";

            try
            {
                var szExceptionList = ConfigurationManager.AppSettings["DomainException"].Split('|');

                var i = 0;
                cn = cn.ToUpper();

                while (i < szExceptionList.Length)
                {
                    var szException = szExceptionList[i].Split(';');

                    var strTmp = szException[0].Substring(0, szException[0].IndexOf(".", StringComparison.Ordinal));
                    if (cn.IndexOf("OU=" + strTmp.ToUpper(), StringComparison.Ordinal) > 0)
                    {
                        companyName = strTmp;
                        break;
                    }
                    i++;
                }

                var upperStr = dn.ToUpper();
                var str2 = upperStr.Substring(upperStr.IndexOf("DC=", StringComparison.Ordinal) + 3);
                var endInt = str2.IndexOf("DC=", StringComparison.Ordinal) - 1;
                domain = str2.Substring(0, endInt);

                if (companyName.Equals(""))
                {
                    companyName = domain;
                }
            }

            catch (Exception)
            {
                var upperStr = dn.ToUpper();
                var str2 = upperStr.Substring(upperStr.IndexOf("DC=", StringComparison.Ordinal) + 3);
                var endInt = str2.IndexOf("DC=", StringComparison.Ordinal) - 1;
                companyName = str2.Substring(0, endInt);
                domain = companyName;
            }
            domain = domain.ToUpper();
            companyName = companyName.ToUpper();
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            //btnActualizar.Attributes.Add("onclick", "Enter()();");
            isError = 0;
            isSuccess = 0;
            logonName = this.Request.QueryString["cn"];
            host = this.Request.QueryString["host"];
            dn = this.Request.QueryString["dn"];
            dn = Encoding.Unicode.GetString(Convert.FromBase64String(dn));

            // CHANGED BY SMP (22-01-2004)
            //			string upperStr = dn.ToUpper();
            //			string str2 = upperStr.Substring(upperStr.IndexOf("DC=")+ 3);
            //			int endInt = str2.IndexOf("DC=") - 1;
            //
            //			string dominio = str2.Substring(0,endInt);
            string dominio;
            string companyName;
            getADInfo(dn, out dominio, out companyName);


            Label2.CssClass = "texto";
            Label3.CssClass = "texto";

            Label6.CssClass = "texto";
            Label7.CssClass = "texto";
            Label8.CssClass = "texto";
            Label9.CssClass = "texto";
            Label10.CssClass = "texto";
            Label11.CssClass = "texto";
            Label12.CssClass = "texto";
            Label13.CssClass = "texto";
            Label14.CssClass = "texto";
            Label17.CssClass = "texto";
            Label18.CssClass = "texto";
            Label19.CssClass = "texto";
            Label1.CssClass = "texto";


            txtCategoria.CssClass = "textField";
            txtDataUltimaActualizacao.CssClass = "textfieldDisabled";

            txtDominio.CssClass = "textfieldDisabled";
            txtEmail.CssClass = "textfieldDisabled";
            txtEmpresa.CssClass = "textfieldDisabled";
            txtFax.CssClass = "textField";
            txtLocalTrabalho.CssClass = "textField";
            txtMorada.CssClass = "textField";
            txtDepartamento.CssClass = "textField";
            txtNomeCompleto.CssClass = "textfieldDisabled";
            txtObservacoes.CssClass = "textfieldDisabled";
            txtPassword.CssClass = "textField";
            txtTelefone1.CssClass = "textField";
            txtTelefone2.CssClass = "textField";//"textField";
            txtTelemovel.CssClass = "textField";



            if (!this.IsPostBack)
            {
                if (LoadUserData(this.Page, logonName, host, dn, dominio, companyName))
                {
                    if (IsUser(logonName) == false)
                    {
                        txtTelefone1.Enabled = false;
                        txtTelefone2.Enabled = false;
                        txtTelemovel.Enabled = false;
                        txtObservacoes.Enabled = false;
                        txtCategoria.Enabled = false;
                        txtDepartamento.Enabled = false;
                        txtFax.Enabled = false;
                        txtLocalTrabalho.Enabled = false;
                        txtMorada.Enabled = false;
                        txtDataUltimaActualizacao.Enabled = false;
                        btnACT.Visible = false;
                    }
                    else
                    {
                        txtTelefone1.Enabled = true;
                        txtTelefone2.Enabled = true;
                        txtTelemovel.Enabled = true;
                        txtObservacoes.Enabled = true;
                        txtCategoria.Enabled = true;
                        txtDepartamento.Enabled = true;
                        txtFax.Enabled = true;
                        txtLocalTrabalho.Enabled = true;
                        txtMorada.Enabled = true;
                        txtDataUltimaActualizacao.Enabled = true;
                        btnACT.Visible = true;

                    }
                }
            }
            else
            {
                //Save the user data
                SaveUserData(this.Page, logonName, dominio, dn);
            }
            this.DataBind();
        }

        private DataTable AddContactSearchResult(DataTable objTable, string[] valueFields)
        {
            var objDataRow = objTable.NewRow();

            for (var i = 0; i < queryFields.Length; i++)
                objDataRow[queryFields[i].ToString()] = valueFields[i];

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
            this.ID = "sss";
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void TextBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
