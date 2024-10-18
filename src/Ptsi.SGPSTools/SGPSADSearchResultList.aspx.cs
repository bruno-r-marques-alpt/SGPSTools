using System;
using System.Configuration;
using System.Data;
using System.Web;
using WindowsApplication1.ptsgps_spsdev011;

namespace SGPStools.Templates
{
    /// <summary>
    /// Summary description for SGPSADSearchResultList.
    /// </summary>
    public class SGPSADSearchResultList : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Literal LTResult;

        protected System.Web.UI.WebControls.Literal LTTotalRegistos;
        protected System.Web.UI.WebControls.Literal LTPrevious;
        protected System.Web.UI.WebControls.Literal LTPages;
        protected System.Web.UI.WebControls.Literal LTNext;
        protected SGPStools.UserControls.ADList ADList1;

        protected string strImagePath;
        protected DataTable objTable;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtNColaborador;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.TextBox txtNomeCompleto;
        protected System.Web.UI.WebControls.Label Label5;
        protected System.Web.UI.WebControls.TextBox txtNomeAbreviado;
        protected System.Web.UI.WebControls.Label Label18;
        protected System.Web.UI.WebControls.TextBox txtDominio;
        protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.TextBox txtDepartamento;
        protected System.Web.UI.WebControls.Label Label6;
        protected System.Web.UI.WebControls.TextBox txtEmpresa;
        protected System.Web.UI.WebControls.Label Label8;
        protected System.Web.UI.WebControls.TextBox txtTelefone1;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.WebControls.TextBox txtTelefone2;
        protected System.Web.UI.WebControls.Label Label10;
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
        protected System.Web.UI.WebControls.Label Label15;
        protected System.Web.UI.WebControls.TextBox txtCV;
        protected System.Web.UI.WebControls.Label Label16;
        protected System.Web.UI.WebControls.TextBox txtHobbies;
        protected System.Web.UI.WebControls.Label Label17;
        protected System.Web.UI.WebControls.TextBox txtObservacoes;
        protected System.Web.UI.WebControls.Label Label19;
        protected System.Web.UI.WebControls.TextBox txtDataUltimaActualizacao;
        protected System.Web.UI.WebControls.Button btnActualizar;
        int intTotalHits;

        #region properties
        public string SiteImagePath
        {
            get { return strImagePath; }

        }
        #endregion properties

        private void Page_Load(object sender, System.EventArgs e)
        {

            // gets the environment type
            var environment = ConfigurationManager.AppSettings["environment"];

            if (environment.Equals("PROD"))
            {

                var countParam = 0;
                if (Request.QueryString["Name"] != null && Request.QueryString["Name"].Length > 1)
                    countParam++;
                if (Request.QueryString["email"] != null && Request.QueryString["email"].Length > 1)
                    countParam++;


                if (Request.QueryString["department"] != null && Request.QueryString["department"].Length > 1)
                    countParam++;
                if (Request.QueryString["Phone"] != null && Request.QueryString["Phone"].Length > 1)
                    countParam++;


                ADList1.createSearchFields(countParam);

                var indice = 0;
                if (Request.QueryString["Name"] != null && Request.QueryString["Name"].Length > 1)
                {
                    ADList1.searchAdField[indice].searchField = "displayName";
                    var nome = HttpUtility.UrlDecode(Request.QueryString["Name"]);

                    ADList1.searchAdField[indice].searchValue = nome;
                    indice++;
                }

                if (Request.QueryString["email"] != null && Request.QueryString["email"].Length > 1)
                {
                    ADList1.searchAdField[indice].searchField = "mail";
                    ADList1.searchAdField[indice].searchValue = Request.QueryString["email"];
                    indice++;
                }

                if (Request.QueryString["Phone"] != null && Request.QueryString["Phone"].Length > 1)
                {
                    ADList1.searchAdField[indice].searchField = "telephonenumber";
                    ADList1.searchAdField[indice].searchValue = Request.QueryString["Phone"];
                    indice++;
                }


                //if ((Request.QueryString["department"] != "" || Request.QueryString["department"] != null) && Request.QueryString["department"].Length > 1) {
                if (Request.QueryString["department"] != null && Request.QueryString["department"].Length > 1)
                {
                    ADList1.searchAdField[indice].searchField = "department";
                    ADList1.searchAdField[indice].searchValue = Request.QueryString["department"];
                    indice++;
                }

                ADList1.creatTableData(6);

                ADList1.tableData[0].columHeader = "Nome";
                ADList1.tableData[0].adField = "displayName";

                ADList1.tableData[1].columHeader = "Login";
                ADList1.tableData[1].adField = "cn";

                ADList1.tableData[2].columHeader = "E-Mail";
                ADList1.tableData[2].adField = "mail";

                ADList1.tableData[3].columHeader = "Empresa";
                ADList1.tableData[3].adField = "dNSHostName";

                ADList1.tableData[4].columHeader = "Telefone";
                ADList1.tableData[4].adField = "telephonenumber";

                ADList1.tableData[5].columHeader = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                ADList1.tableData[5].adField = "mobile";


                // FALTA PEDIR OS CAMPOS COM SO DOMINIOS!!!!!

                ADList1.creatExceptionData(1);
                ADList1.exceptionField[0].nameField = "mobile";
                ADList1.exceptionField[0].urlPage = "CRIAR PAGINA PARA ENVIAR O SMS";

                ADList1.rowClassHeader = "rowADClassHeader";
                ADList1.rowClass = "rowADClass";
                ADList1.alternateRowClass = "alternateRowClass";
                ADList1.aHiddenFields = new string[] { "cn" };

                ADList1.execute();

                //LTCriteria.Text = ADList1.strCriterio;
                LTTotalRegistos.Text = "Total de registos encontrados: " + ADList1.adReturncount;


                LTResult.Text = "Resultados para a pesquisa:";
            }
            else
            {
                // some dummy code
            }
        }

        // Function to getUsers from the specified domainPath
        private void getUsers(string strDomainPath, string strQueryName, string strQueryDepartment, string strQueryNameCriteria, string strQueryDepartmentCriteria)
        {
            string strName, strDepartment, strEmail, strContact;

            var op = new UserMng();

            //create search request
            var req = new searchRequest();

            req.dn = strDomainPath;
            req.scope = "wholeSubtree";


            req.filter = "(&((objectClass=user))" + strQueryNameCriteria + strQueryDepartmentCriteria + ")";
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
                    objTable = AddContactSearchResult(objTable, intCounter + 1, strName, strDepartment, strEmail, strContact);
                }
            }

        }

        // Searches the specified field int the array of data
        private int FindData(attr[] arrayData, string strFieldData)
        {
            for (var intCounter = 0; intCounter < arrayData.Length; intCounter++)
            {
                if (arrayData[intCounter].name.ToLower() == strFieldData.ToLower()) return intCounter;
            }
            return -1;
        }

        private DataTable AddContactSearchResult(DataTable objTable, int intNumber, string strName, string strDepartment, string strEmail, string strContact)
        {
            var objDataRow = objTable.NewRow();
            objDataRow["Number"] = Convert.ToString(intNumber);
            objDataRow["Name"] = strName;
            objDataRow["Department"] = strDepartment;
            objDataRow["Email"] = strEmail;
            objDataRow["Contact"] = strContact;
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
