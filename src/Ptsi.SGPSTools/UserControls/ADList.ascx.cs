namespace SGPStools.UserControls
{
    using Class;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Text;
    using System.Web;
    using WindowsApplication1.ptsgps_spsdev011;




    /// <summary>
    ///		Summary description for ADList.
    /// </summary>
    public abstract class ADList : System.Web.UI.UserControl
    {
        #region Protected Properties
        protected System.Web.UI.WebControls.Repeater ListAD;
        protected string[] queryFields;

        #endregion

        #region Public Properties
        public string rowClass;
        public string cellClass;
        public string rowBackColor;
        public string cellBackColor;
        public string rowClassHeader;
        public string cellClassHeader;
        public string alternateRowClass;
        public cTableData[] tableData;
        public cSearchField[] searchAdField;
        public cExceptionField[] exceptionField;
        public string[] aHiddenFields;
        public string linkAnterior;
        public string currPage;
        public string linkSeguinte;
        public int MAXPERPAGE;
        public int numCols;
        public string strCriterio;
        public string adReturncount;
        public string strResultADtext;
        public int numTotalPag;
        public string DetailPageName = "ADuserDetail.aspx";
        //public string CorpptDomain = "*";

        public string strCretiriaTMP;
        #endregion

        #region Private Methods
        private void Page_Load(object sender, System.EventArgs e)
        {


            this.DataBind();
        }

        /// <summary>
        /// Validates the mobile phone number! If the number doesn't start width 96 is an
        /// invalid number!
        /// if the number as any chars besides 0..9 is an invalid number.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>
        /// True for a valid number.
        ///	False for an Invalid number.		 
        /// </returns>
        private bool validatePhone(string phone)
        {
            try
            {
                if (phone.Substring(0, 2).Equals("96"))
                {
                    var xpto = Convert.ToInt64(phone);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Gets the URL of an exceptionField.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>The URL.</returns>
        private string getExceptionUrl(string ex)
        {
            for (var i = 0; i < exceptionField.Length; i++)
            {
                if (ex.ToUpper().Equals(exceptionField[i].nameField.ToUpper()))
                {
                    return exceptionField[i].urlPage;
                }
            }
            return null;
        }


        /// <summary>
        /// Indicates if a field form the AD is hidden.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>
        /// Null if the field is not hidden.
        /// the field name if is hidden.
        /// </returns>
        private string IsHiddenColumn(string ex)
        {
            for (var i = 0; i < aHiddenFields.Length; i++)
            {

                if (ex.ToUpper().Equals(aHiddenFields[i].ToUpper()))
                {
                    return ex;
                }
            }
            return null;
        }



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

            //req.filter = "(&(|(objectClass=contact)(objectClass=user))" + strQueryNameCriteria + ")";
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
            //object[] ret = op.batchRequest(items);

            // AMM - guardar resultados da query numa variável de sessão.
            object[] ret;

            searchResponse Tempresp;

            if (HttpContext.Current.Session["strCriteria"] != null && HttpContext.Current.Session["strCriteria"].ToString() != "" && HttpContext.Current.Session["strDomain"] != null && HttpContext.Current.Session["strDomain"].ToString() != "")
            // Existe um critério armazenado
            {
                if (HttpContext.Current.Session["strCriteria"].ToString() == strQueryNameCriteria && HttpContext.Current.Session["strDomain"].ToString() == strDomainPath)
                // o critério é mantido
                {
                    Tempresp = HttpContext.Current.Session["objResult"] as searchResponse;
                    //HttpContext.Current.Response.Write("O critério n se alterou");
                }
                else
                // o critério foi alterado
                {
                    //HttpContext.Current.Response.Write("O critério alterou-se");
                    HttpContext.Current.Session["strCriteria"] = strQueryNameCriteria;
                    HttpContext.Current.Session["strDomain"] = strDomainPath;
                    ret = op.batchRequest(items);
                    Tempresp = ret[0] as searchResponse;
                    HttpContext.Current.Session["objResult"] = Tempresp as searchResponse;
                }

            }
            else
            // não existe critério - primeira vez
            {
                //HttpContext.Current.Response.Write("1ª vez");
                HttpContext.Current.Session["strCriteria"] = strQueryNameCriteria;
                HttpContext.Current.Session["strDomain"] = strDomainPath;
                ret = op.batchRequest(items);
                Tempresp = ret[0] as searchResponse;
                HttpContext.Current.Session["objResult"] = Tempresp;
            }

            //searchResponse resp = ret[0] as searchResponse;
            //errorResponse resp2 = ret[0] as errorResponse; isto nunca era utilizado!

            var resp = Tempresp as searchResponse;









            int idx;
            if (resp != null && resp.Items != null)
            {
                var valueFields = new string[queryFields.Length];

                for (var intCounter = 0; intCounter < resp.Items.Length; intCounter++)
                {

                    var sr = (searchResultEntry)resp.Items[intCounter];
                    //sr.dn
                    for (iFields = 0; iFields < queryFields.Length; iFields++)
                    {

                        idx = GetQueryFieldIdx(queryFields[iFields], sr);

                        if (idx >= 0)
                        {
                            valueFields[iFields] = sr.attr[idx].value[0].ToString();
                        }
                        else
                        {
                            valueFields[iFields] = "";
                        }
                    }
                    oTable = AddContactSearchResult(oTable, valueFields, queryFields, sr.dn);

                }
            }

        }


        private DataTable AddContactSearchResult(DataTable objTable, string[] valueFields, string[] queryFields, string dn)
        {
            // Don't add users from Intranet DC
            if (dn.IndexOf("OU=Intranet,DC=intranet", StringComparison.Ordinal) <= 0)
            {
                var objDataRow = objTable.NewRow();

                objDataRow["dn"] = dn;

                for (var i = 0; i < queryFields.Length; i++)
                    objDataRow[queryFields[i].ToString()] = valueFields[i];

                objTable.Rows.Add(objDataRow);
            }

            return objTable;
        }





        #endregion

        #region Public Methods




        /// <summary>
        /// Creates the ExceptionData width the respective size
        /// </summary>
        /// <param name="size"> Size of the Table</param>
        public void creatExceptionData(int size)
        {
            exceptionField = new cExceptionField[size];
            for (var i = 0; i < size; i++)
            {
                exceptionField[i] = new cExceptionField();
            }
        }


        /// <summary>
        /// Initializes the ExceptionData widht a pre-made array.
        /// NOTE: Erases the table created by the method "creatExceptionData(int size)"
        /// </summary>
        /// <param name="pTableData">the ExceptionData</param>
        /// <returns></returns>
        public int setExceptionData(cExceptionField[] pExceptionField)
        {
            try
            {
                exceptionField = pExceptionField;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        /// <summary>
        /// Creates the tableData width the respective size
        /// </summary>
        /// <param name="size"> Size of the Table</param>
        public void creatTableData(int size)
        {
            tableData = new cTableData[size];
            for (var i = 0; i < size; i++)
            {
                tableData[i] = new cTableData();
            }
        }

        /// <summary>
        /// Initializes the tableData widht a pre-made array.
        /// NOTE: Erases the table created by the method "createTableData(int size)"
        /// </summary>
        /// <param name="pTableData">the tableData</param>
        /// <returns></returns>
        public int setTableData(cTableData[] pTableData)
        {
            try
            {
                tableData = pTableData;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        /// <summary>
        /// Creates the searchAdField width the respective size
        /// </summary>
        /// <param name="size"> Size of the Table</param>
        public void createSearchFields(int size)
        {
            searchAdField = new cSearchField[size];
            for (var i = 0; i < size; i++)
            {
                searchAdField[i] = new cSearchField();
            }
        }

        /// <summary>
        /// Initializes the SearchFields widht a pre-made array.
        /// NOTE: Erases the table created by the method "createSearchFields(int size)"
        /// </summary>
        /// <param name="pSearchField"></param>
        /// <returns></returns>
        public int setSearchFields(cSearchField[] pSearchField)
        {
            try
            {
                searchAdField = pSearchField;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Sets the CSS classes to be used in the construction of the table rows
        /// </summary>
        /// <param name="pRowClassHeader"></param>
        /// <param name="pCellClassHeader"></param>
        /// <param name="pRowClass"></param>
        /// <param name="pRowBackColor"></param>
        /// <param name="pCellClass"></param>
        /// <param name="pCellBackColor"></param>
        public void setTableProperties(string pRowClassHeader, string pCellClassHeader, string pRowClass, string pRowBackColor, string pCellClass, string pCellBackColor)
        {
            rowClass = pRowClass;
            cellClass = pCellClass;
            rowBackColor = pRowBackColor;
            cellBackColor = pCellBackColor;
            rowClassHeader = pRowClassHeader;
            cellClassHeader = pCellClassHeader;
        }



        /// <summary>
        /// GetDomainToSearch
        /// </summary>
        /// <param name="localSearch">the domain to serch</param>
        /// <returns>the string to query the webservice (DC=xpto,DC=corppt,DC=com)</returns>
        private string getDomainToSearch(string localSearch)
        {
            var strDomain = "";

            try
            {
                var szExceptionList = ConfigurationManager.AppSettings["DomainException"].Split('|');

                var i = 0;
                while (i < szExceptionList.Length)
                {
                    var szException = szExceptionList[i].Split(';');

                    localSearch = localSearch.Replace("*", "");

                    if (localSearch.ToUpper().Equals(szException[0].ToUpper()))
                    {
                        var strTmp = szException[1].Substring(0, szException[1].IndexOf(".", StringComparison.Ordinal));
                        strDomain = "OU=" + szException[0].Substring(0, localSearch.IndexOf(".", StringComparison.Ordinal))
                            + ",DC=" + strTmp + ",DC=corppt,DC=com";
                        break;
                    }
                    i++;
                }

                if (strDomain.Equals(""))
                {
                    var strTmp = localSearch.Substring(0, localSearch.IndexOf(".", StringComparison.Ordinal));
                    strDomain = "DC=" + strTmp + ",DC=corppt,DC=com";
                }
            }
            catch (Exception)
            {
                var strTmp = localSearch.Substring(0, localSearch.IndexOf(".", StringComparison.Ordinal));
                strDomain = "DC=" + strTmp + ",DC=corppt,DC=com";
            }

            return strDomain;
        }

        /// <summary>
        /// Builds the HTML table rows width the AD search results.
        /// </summary>
        /// <returns></returns>
        public int execute()
        {
            try
            {
                MAXPERPAGE = Convert.ToInt32(ConfigurationManager.AppSettings["MAXPERPAGE"]);
                numCols = tableData.Length + 1;
                var strCretiria = "";

                var strTableData = new string[tableData.Length];
                // Put the AD searhc code HERE!!!!!!!!!!!!!!!!
                // AD Call return an object array width all the fields in each row.
                // Ex:
                var retObjTable = new DataTable();

                for (var i = 0; i < tableData.Length; i++)
                {
                    retObjTable.Columns.Add(tableData[i].adField, System.Type.GetType("System.String"));
                    strTableData[i] = tableData[i].adField;
                }
                // Coluna com o DN	
                retObjTable.Columns.Add("dn", System.Type.GetType("System.String"));

                for (var i = 0; i < searchAdField.Length; i++)
                {
                    if (searchAdField[i].searchValue.Substring(0, 1).Equals(" "))
                    {
                        searchAdField[i].searchValue = "*" + searchAdField[i].searchValue.Trim();
                    }
                    strCretiria += "(" + searchAdField[i].searchField + "=" + searchAdField[i].searchValue + ")";
                }
                strCretiriaTMP = strCretiria;
                var strDomain = ConfigurationManager.AppSettings["WebServiceADSearchDomain"];
                var localSearch = Request.QueryString["Empresa"];

                if (localSearch != null && localSearch != "" && !localSearch.Equals("*"))
                {
                    strDomain = getDomainToSearch(localSearch);
                }

                getUsers(ref retObjTable, strDomain, strCretiria, strTableData);

                //Limpa linhas com $
                var contador = retObjTable.Rows.Count;
                for (var k = 0; k < contador; k++)
                {
                    for (var h = 0; h < retObjTable.Columns.Count; h++)
                    {
                        if (retObjTable.Columns[h].ColumnName.ToUpper().Equals("displayName".ToUpper()))
                        {
                            var aaa = (string)retObjTable.Rows[k].ItemArray.GetValue(h);
                            if (retObjTable.Rows[k].ItemArray.GetValue(h).ToString().IndexOf("$", StringComparison.Ordinal) != -1)
                            {
                                retObjTable.Rows.RemoveAt(k);
                                contador--;
                                k--;
                            }
                        }
                    }
                }


                strCretiria = strCretiria.Trim();
                strCretiria = strCretiria.Replace("(", ":");
                strCretiria = strCretiria.Replace(")", ":");
                strCriterio = strCretiria.Substring(1, strCretiria.Length - 2);


                adReturncount = retObjTable.Rows.Count.ToString();

                currPage = Request.QueryString["pag"];
                if (currPage == null)
                {
                    currPage = "1";
                }

                var icurrPage = Convert.ToInt32(currPage);
                var initialCount = icurrPage * MAXPERPAGE - MAXPERPAGE;
                var finalCount = icurrPage * MAXPERPAGE;
                if (finalCount > retObjTable.Rows.Count) finalCount = retObjTable.Rows.Count;


                if (icurrPage <= 1)
                {
                    linkAnterior = Request.Url.ToString();
                }
                else
                {
                    var tmpStr = (Request.Url.ToString());
                    linkAnterior = tmpStr.Replace("pag=" + icurrPage, "pag=" + (icurrPage - 1).ToString());
                }

                var limite = retObjTable.Rows.Count / MAXPERPAGE;
                var dLimite = (double)((double)(retObjTable.Rows.Count) / (double)(MAXPERPAGE));
                numTotalPag = retObjTable.Rows.Count / MAXPERPAGE;
                if ((double)retObjTable.Rows.Count % MAXPERPAGE != 0) { numTotalPag++; }

                if (icurrPage >= dLimite)
                {
                    linkSeguinte = Request.Url.ToString(); ;
                }
                else
                {
                    var tmpStr1 = (Request.Url.ToString());

                    linkSeguinte = tmpStr1.Replace("pag=" + icurrPage, "pag=" + (icurrPage + 1).ToString());

                }

                if (icurrPage > numTotalPag)
                {

                    currPage = Convert.ToString(numTotalPag);
                    linkAnterior = "#";
                    linkSeguinte = "#";
                }

                // creates the data table for the pages information
                DataTable objTable;
                objTable = new DataTable();

                // Contains de HTML table row
                objTable.Columns.Add("strRow", System.Type.GetType("System.String"));

                //Builds the Table Header.
                DataRow objDataRowHeader;
                // creates a new data table row
                objDataRowHeader = objTable.NewRow();
                objDataRowHeader["strRow"] = "<tr class='" + rowClassHeader + "'>";
                for (var i = 0; i < tableData.Length; i++)
                {
                    if (IsHiddenColumn(tableData[i].adField) == null)
                    {
                        objDataRowHeader["strRow"] += "<td class='" + cellClassHeader + "' nowrap>&nbsp;";
                        objDataRowHeader["strRow"] += tableData[i].columHeader;
                        objDataRowHeader["strRow"] += "</td>";
                    }
                }


                objDataRowHeader["strRow"] += "</tr>";

                objTable.Rows.Add(objDataRowHeader);


                //Put AD return Values Iteration HERE!!!!
                // WHILE adReturnArray AS VALUES DO 
                for (var k = initialCount; k < finalCount; k++)
                {

                    DataRow objDataRow;
                    // creates a new data table row
                    objDataRow = objTable.NewRow();


                    // Search all the returned AD fiels for a match in 
                    // the searchField propertie
                    var cnNameTmp = "";
                    var localDNShostName = "";
                    var dominio = "";
                    var localDN = "";
                    var mailTmp = "";
                    var tmpClass = "";
                    if (k % 2 == 0)
                        tmpClass = rowClass;
                    else
                        tmpClass = alternateRowClass;

                    objDataRow["strRow"] = "<tr class='" + tmpClass + "'>";

                    for (var h = 0; h < retObjTable.Columns.Count; h++)
                    {
                        if (retObjTable.Columns[h].ColumnName.ToUpper().Equals("cn".ToUpper()))
                        {
                            cnNameTmp = (string)retObjTable.Rows[k].ItemArray.GetValue(h);
                        }

                        if (retObjTable.Columns[h].ColumnName.ToUpper().Equals("dn".ToUpper()))
                        {
                            localDN = (string)retObjTable.Rows[k].ItemArray.GetValue(h);
                        }
                    }

                    // Changed by SMP
                    dominio = getCompanyName(localDN);

                    localDN = Convert.ToBase64String(Encoding.Unicode.GetBytes(localDN));

                    for (var j = 0; j < retObjTable.Columns.Count; j++)
                    {

                        if (retObjTable.Columns[j].ColumnName.ToUpper().Equals("mail".ToUpper()))
                        {
                            mailTmp = (string)retObjTable.Rows[k].ItemArray.GetValue(j);
                        }

                        if (IsHiddenColumn(retObjTable.Columns[j].ColumnName) == null)
                        {

                            string tmpStr;
                            if ((tmpStr = getExceptionUrl((string)retObjTable.Columns[j].ColumnName)) != null)
                            {
                                if (validatePhone((string)retObjTable.Rows[k].ItemArray.GetValue(j)))
                                {
                                    objDataRow["strRow"] += "<td align='center' class='" + cellClass + "' nowrap>";
                                    //objDataRow["strRow"] += "<input type=button class='smsButton' value='sms' onclick='JavaScript:document.location=\"" + "SMSTemplate.aspx?warningFalse=1&amp;" + "?cn=" + cnNameTmp + "&email=" + mailTmp + "&dominioTo=&dominoFrom=" + localDN + "&nr_tel=" + (string)retObjTable.Rows[k].ItemArray.GetValue(j) + "\";'>";

                                    var tempURL = "SMSTemplate.aspx?warningFalse=1&amp;" + "?cn=" + cnNameTmp + "&email=" + mailTmp + "&dominioTo=&dominoFrom=" + localDN + "&nr_tel=" + (string)retObjTable.Rows[k].ItemArray.GetValue(j);
                                    objDataRow["strRow"] += "<input type=button class='smsButton' value='sms' onclick=\"AbrirJanela('" + tempURL + "', '_blank', '630','220', 'no'); return false;\">";

                                    objDataRow["strRow"] += "</td>";
                                }
                                else
                                {
                                    objDataRow["strRow"] += "<td align='center' class='" + cellClass + "' nowrap>";
                                    objDataRow["strRow"] += (string)retObjTable.Rows[k].ItemArray.GetValue(j);
                                    objDataRow["strRow"] += "</td>";
                                }


                            }
                            else if (retObjTable.Columns[j].ColumnName.ToUpper().Equals("mail".ToUpper()))
                            {
                                var displayEmail = "";
                                displayEmail = ((string)retObjTable.Rows[k].ItemArray.GetValue(j));
                                if (displayEmail.Length >= 25)
                                    displayEmail = displayEmail.Substring(0, 25) + "...";
                                objDataRow["strRow"] += "<td class='" + cellClass + "' nowrap>";
                                objDataRow["strRow"] += "<a class='linksverde' alt='Enviar Email.' href='mailto:" + (string)retObjTable.Rows[k].ItemArray.GetValue(j) + "'>" + displayEmail + "</a>";
                                objDataRow["strRow"] += "</td>";
                            }
                            else if (retObjTable.Columns[j].ColumnName.ToUpper().Equals("displayName".ToUpper()))
                            {

                                var localName = (string)retObjTable.Rows[k].ItemArray.GetValue(j);
                                //localName
                                if (localName.Length >= 40)
                                    localName = localName.Substring(0, 40) + "...";
                                objDataRow["strRow"] += "<td class='" + cellClass + "' nowrap>";
                                //objDataRow["strRow"] += "<a class='linksverde' alt='Detalhe.' href=\"ADuserDetail.aspx?cn=" + cnNameTmp + "&host=" + localDNShostName +"&dn=" + localDN + "\">" + localName + "</a>";
                                objDataRow["strRow"] += "<a class='linksverde' alt='Detalhe.' href=\"" + DetailPageName + "?cn=" + cnNameTmp + "&host=" + localDNShostName + "&dn=" + localDN + "\">" + localName + "</a>";
                                objDataRow["strRow"] += "</td>";
                            }
                            else if (retObjTable.Columns[j].ColumnName.ToUpper().Equals("dNSHostName".ToUpper()))
                            {
                                objDataRow["strRow"] += "<td class='" + cellClass + "' nowrap>";
                                objDataRow["strRow"] += dominio;
                                objDataRow["strRow"] += "</td>";
                            }
                            else
                            {
                                if (!retObjTable.Columns[j].ColumnName.ToUpper().Equals("dn".ToUpper()))
                                {
                                    objDataRow["strRow"] += "<td class='" + cellClass + "' nowrap>";
                                    objDataRow["strRow"] += (string)retObjTable.Rows[k].ItemArray.GetValue(j);
                                    objDataRow["strRow"] += "</td>";
                                }
                            }

                        }
                    }
                    objDataRow["strRow"] += "</tr>";
                    objTable.Rows.Add(objDataRow);
                }
                // END AD RETURN ITERATION
                //


                // sets the data to the repeater object
                ListAD.DataSource = objTable.DefaultView;
                ListAD.DataBind();

                // sets the data to page object

                this.DataBind();
                return 0;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
                return -1;
            }

        }

        private string getCompanyName(string localDN)
        {
            var upperStr = localDN.ToUpper();

            var i = upperStr.IndexOf("DC=", StringComparison.Ordinal);
            var str2 = upperStr.Substring(i + 3);
            var endInt = str2.IndexOf("DC=", StringComparison.Ordinal) - 1;

            var companyName = str2.Substring(0, endInt);

            if (isException(upperStr))
            {
                var str3 = upperStr.Substring(0, i - 1);
                var beginInt = str3.LastIndexOf("OU=") + 3;
                companyName = str3.Substring(beginInt);
            }

            return companyName;
        }

        private bool isException(string cn)
        {
            var szExceptionList = ConfigurationManager.AppSettings["DomainException"].Split('|');

            var i = 0;
            while (i < szExceptionList.Length)
            {
                var szException = szExceptionList[i].Split(';');

                var strTmp = szException[0].Substring(0, szException[0].IndexOf(".", StringComparison.Ordinal));
                if (cn.IndexOf("OU=" + strTmp.ToUpper(), StringComparison.Ordinal) > 0)
                {
                    return true;
                }
                i++;
            }
            return false;


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
        #endregion

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
