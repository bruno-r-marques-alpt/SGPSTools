// Decompiled with JetBrains decompiler
// Type: wServiceSMS_SGPS.Service1
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services;
using WindowsApplication1.ptsgps_spsdev011;
using wServiceSMS_SGPS.Components;

namespace wServiceSMS_SGPS
{
    [WebService(Namespace = "http://localhost/wssms", Description = "A short description of the XML Web service.")]
    public class Service1 : WebService
    {
        private readonly IContainer components = null;
        protected DataTable objTable;
        protected string[] queryFields = new string[6];

        public Service1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private byte[] StringToByteArray(string theString)
        {
            var byteArray = new byte[theString.Length * 2];
            for (var index = 0; index < theString.Length; ++index)
            {
                byteArray[index] = (byte)(theString[index] & (uint)byte.MaxValue);
                byteArray[index + 1] = (byte)((theString[index] & 65280) >> 16);
            }

            return byteArray;
        }

        private bool verifySMSUser(string dominio)
        {
            Trace.WriteLine("WSSMS verifySMSUser dominio=" + dominio);
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand("[ADCorpPT].[ADCorpPT_SA].[adcorppt_verificaSmsUser]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    var sqlParameter = sqlCommand.Parameters.Add("@dominio", SqlDbType.VarChar, 150);
                    sqlParameter.Direction = ParameterDirection.Input;
                    sqlParameter.Value = dominio;
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (!sqlDataReader.Read())
                            return false;
                        var fieldCount = sqlDataReader.FieldCount;
                        return sqlDataReader.GetInt16(0) == 1;
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS verifySMSUser ERROR:");
                Trace.WriteLine(ex);
                return false;
            }
        }

        private DataSet privGetLog(string dominio, string dataInicio, string dataFim)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    connection.Open();
                    using (var sqlCommand =
                           new SqlCommand("[ADCorpPT].[ADCorpPT_SA].[adcorppt_listar_logsSMS]", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        var sqlParameter1 = sqlCommand.Parameters.Add("@dominio", SqlDbType.VarChar, 100);
                        sqlParameter1.Direction = ParameterDirection.Input;
                        sqlParameter1.Value = dominio;
                        var sqlParameter2 = sqlCommand.Parameters.Add("@dataInicio", SqlDbType.DateTime, 8);
                        sqlParameter2.Direction = ParameterDirection.Input;
                        sqlParameter2.Value = dataInicio;
                        var sqlParameter3 = sqlCommand.Parameters.Add("@dataFim", SqlDbType.DateTime, 8);
                        sqlParameter3.Direction = ParameterDirection.Input;
                        sqlParameter3.Value = dataFim;
                        var sqlDataReader = sqlCommand.ExecuteReader();
                        var log = new DataSet();
                        log.Tables.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        log.Tables[0].Columns.Add();
                        while (sqlDataReader.Read())
                        {
                            var objArray = new object[sqlDataReader.FieldCount];
                            objArray[0] = sqlDataReader.GetSqlString(1);
                            objArray[1] = sqlDataReader.GetSqlString(2);
                            objArray[2] = sqlDataReader.GetSqlString(3);
                            objArray[3] = sqlDataReader.GetSqlString(4);
                            objArray[4] = sqlDataReader.GetSqlDateTime(5);
                            objArray[5] = sqlDataReader.GetSqlString(6);
                            log.Tables[0].Rows.Add(objArray);
                        }

                        return log;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS privGetLog ERROR: ");
                Trace.WriteLine(ex);

                return null;
            }
        }

        private string setLogEntrie(
            string conta,
            string dominoFrom,
            string dominoTo,
            string to,
            string msg,
            string company,
            string user,
            DateTime smsDate,
            string cgiError,
            string generalError,
            string email)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    connection.Open();
                    using (var sqlCommand =
                           new SqlCommand("[ADCorpPT].[ADCorpPT_SA].[adcorppt_criarlogSMS]", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        var sqlParameter1 = sqlCommand.Parameters.Add("@username", SqlDbType.VarChar, 50);
                        sqlParameter1.Direction = ParameterDirection.Input;
                        sqlParameter1.Value = user;
                        var sqlParameter2 = sqlCommand.Parameters.Add("@dominio", SqlDbType.VarChar, 100);
                        sqlParameter2.Direction = ParameterDirection.Input;
                        sqlParameter2.Value = dominoFrom;
                        var sqlParameter3 = sqlCommand.Parameters.Add("@destinoTelefone", SqlDbType.VarChar, 50);
                        sqlParameter3.Direction = ParameterDirection.Input;
                        sqlParameter3.Value = to;
                        var sqlParameter4 = sqlCommand.Parameters.Add("@destinoDominio", SqlDbType.VarChar, 100);
                        sqlParameter4.Direction = ParameterDirection.Input;
                        sqlParameter4.Value = dominoTo;
                        var sqlParameter5 = sqlCommand.Parameters.Add("@data", SqlDbType.DateTime, 8);
                        sqlParameter5.Direction = ParameterDirection.Input;
                        sqlParameter5.Value = smsDate;
                        var sqlParameter6 = sqlCommand.Parameters.Add("@conta", SqlDbType.VarChar, 50);
                        sqlParameter6.Direction = ParameterDirection.Input;
                        sqlParameter6.Value = conta;
                        var sqlParameter7 = sqlCommand.Parameters.Add("@cgiError", SqlDbType.VarChar, 200);
                        sqlParameter7.Direction = ParameterDirection.Input;
                        sqlParameter7.Value = cgiError;
                        var sqlParameter8 = sqlCommand.Parameters.Add("@generalError", SqlDbType.VarChar, 200);
                        sqlParameter8.Direction = ParameterDirection.Input;
                        sqlParameter8.Value = generalError;
                        var sqlParameter9 = sqlCommand.Parameters.Add("@email", SqlDbType.VarChar, 100);
                        sqlParameter9.Direction = ParameterDirection.Input;
                        sqlParameter9.Value = email;
                        sqlCommand.ExecuteNonQuery();
                        return "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS sendSMSV2 setLogEntrie ERROR:");
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }

        private int GetQueryFieldIdx(string key, searchResultEntry sr)
        {
            for (var queryFieldIdx = 0; queryFieldIdx < sr.attr.Length; ++queryFieldIdx)
                if (sr.attr[queryFieldIdx].name.ToLower() == key.ToLower())
                    return queryFieldIdx;
            return -1;
        }

        private void getUsers(
            ref DataTable oTable,
            string strDomainPath,
            string strQueryNameCriteria,
            string[] queryFields)
        {
            Trace.WriteLine("WSSMS getUsers strDomainPath=" + strDomainPath);
            Trace.WriteLine("WSSMS getUsers strQueryNameCriteria=" + strQueryNameCriteria);

            var userMng = new UserMng();
            var searchRequest = new searchRequest
            {
                dn = strDomainPath,
                scope = "wholeSubtree",
                filter = "(&(objectClass=user)" + strQueryNameCriteria + ")",
                derefAliases = "derefAlways",
                attributes = (attribute[])Array.CreateInstance(typeof(attribute), queryFields.Length)
            };
            for (var index = 0; index < queryFields.Length; ++index)
            {
                searchRequest.attributes[index] = new attribute
                {
                    name = queryFields[index]
                };
            }

            userMng.IdentityCredentialsValue = new IdentityCredentials
            {
                IdentityName = ConfigurationManager.AppSettings["ADSearchUserName"],
                IdentityPassword = ConfigurationManager.AppSettings["ADSearchUserPass"]
            };
            userMng.UserCredentialsValue = new UserCredentials
            {
                Username = "a",
                Password = "a",
                Domain = "a"
            };
            var Items = new object[1]
            {
                searchRequest
            };
            var objArray = userMng.batchRequest(Items);
            var searchResponse = objArray[0] as searchResponse;
            var errorResponse = objArray[0] as errorResponse;
            if (searchResponse == null || searchResponse.Items == null)
                return;
            var valueFields = new string[queryFields.Length];
            for (var index1 = 0; index1 < searchResponse.Items.Length; ++index1)
            {
                var sr = (searchResultEntry)searchResponse.Items[index1];
                for (var index2 = 0; index2 < queryFields.Length; ++index2)
                {
                    var queryFieldIdx = GetQueryFieldIdx(queryFields[index2], sr);
                    valueFields[index2] = queryFieldIdx < 0 ? "" : sr.attr[queryFieldIdx].value[0];
                }

                oTable = AddContactSearchResult(oTable, valueFields);
            }
        }

        private DataTable AddContactSearchResult(DataTable objTable, string[] valueFields)
        {
            var row = objTable.NewRow();
            for (var index = 0; index < queryFields.Length; ++index)
                row[queryFields[index]] = valueFields[index];
            objTable.Rows.Add(row);
            return objTable;
        }

        private string LoadUserDataPhone(string psPhoneNumber)
        {
            try
            {
                objTable = new DataTable();
                objTable.Columns.Add("CN", Type.GetType("System.String"));
                objTable.Columns.Add("displayName", Type.GetType("System.String"));
                objTable.Columns.Add("Department", Type.GetType("System.String"));
                objTable.Columns.Add("mail", Type.GetType("System.String"));
                objTable.Columns.Add("telephoneNumber", Type.GetType("System.String"));
                objTable.Columns.Add("dn", Type.GetType("System.String"));
                queryFields[0] = "cn";
                queryFields[1] = "displayName";
                queryFields[2] = "department";
                queryFields[3] = "mail";
                queryFields[4] = "telephoneNumber";
                queryFields[5] = "dn";
                getUsers(ref objTable, ConfigurationManager.AppSettings["WebServiceADSearchDomain"],
                    "(telephoneNumber=" + psPhoneNumber + ")", queryFields);
                return objTable.Rows[0].ItemArray.GetValue(4).ToString();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS LoadUserDataPhone ERROR: psPhoneNumber=" + psPhoneNumber);
                Trace.WriteLine(ex);
                return null;
            }
        }

        [WebMethod]
        public int sendEmail(string fromName, string from, string to, string subject, string body)
        {
            Trace.WriteLine("WSSMS sendEmail: fromName=" + fromName + ", from=" + from + ",to=" + to);
            try
            {
                new CASPEmail
                {
                    SMTPService = ConfigurationManager.AppSettings["SMTPService"]
                }.SendEmailMessage(to, fromName, from, subject, body);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS sendEmail ERROR: fromName=" + fromName + ", from=" + from + ",to=" + to);
                Trace.WriteLine(ex);
                return -1;
            }

            return 0;
        }

        [WebMethod]
        public string sendSMS(
            string conta,
            string largeAccount,
            string dominioFrom,
            string dominioTo,
            string to,
            string msg,
            string company,
            string user,
            string emailFrom)
        {
            if (ConfigurationManager.AppSettings["protocol"].ToString().ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                                  SecurityProtocolType.Tls11 |
                                                                  SecurityProtocolType.Tls12 |
                                                                  SecurityProtocolType.Tls13;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.Expect100Continue = true;
            }
            Trace.WriteLine("WSSMS sendSMS conta=" + conta + ", to=" + to + ",from=" + emailFrom + ",user=" + user);
            string cgiError = null;
            try
            {
                var int32 = Convert.ToInt32(ConfigurationManager.AppSettings["maxSMSbody"]);
                Convert.ToInt32(ConfigurationManager.AppSettings["maxSMSHeader"]);
                msg = ReplaceExceptionChars(msg);
                if (msg.Length > int32)
                    return "-1";
                var str = emailFrom;
                if (str.Length >= 35)
                    str = str.Substring(0, str.Length - 4) + "...";
                msg = str + " " + msg;

                var url = ConfigurationManager.AppSettings["protocol"] + "://" +
                             ConfigurationManager.AppSettings["cgiHost"] + ":" +
                             ConfigurationManager.AppSettings["cgiPort"] + "/" +
                             ConfigurationManager.AppSettings["cgiLocalization"] + "/" +
                             ConfigurationManager.AppSettings["CGIname"];
                Trace.WriteLine("WSSMS creating WebRequest url=" + url);
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";

                webRequest.UserAgent = "DC_SMS_SENDER";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                var byteArray =
                    Encoding.UTF8.GetBytes("nr_tel=" + to + "&msg=" + msg.Replace(" ", "+") + "&la=" + largeAccount);
                webRequest.ContentLength = byteArray.Length;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    cgiError = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"))
                        .ReadToEnd();
                }

                setLogEntrie(conta, dominioFrom, dominioTo, to, msg, company, user, DateTime.Now, cgiError, "",
                    emailFrom);
                if (!cgiError.Equals("0"))
                {
                    Trace.WriteLine("WSSMS sendSMSV2 returned cgiError: " + cgiError);
                    return cgiError;
                }

                try
                {
                    var body = "SMS para o número " + to + " enviado com sucesso";
                    sendEmail(user, "inSapo", emailFrom, "SMS enviado através de InSapo", body);
                    return cgiError;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("WSSMS sendSMS error -2");
                    Trace.WriteLine(ex);
                    return "-2";
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS sendSMS error -1");
                Trace.WriteLine(ex);
                if (cgiError == null)
                    cgiError = "";
                setLogEntrie(conta, dominioFrom, dominioTo, to, msg, company, user, DateTime.Now, cgiError, ex.Message,
                    emailFrom);
                return "-1";
            }
        }

        [WebMethod]
        public string sendSMSV2(
            string conta,
            string largeAccount,
            string dominioFrom,
            string dominioTo,
            string to,
            string from,
            string msg,
            string company,
            string user,
            string emailFrom)
        {
            if (ConfigurationManager.AppSettings["protocol"].ToString().ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                                  SecurityProtocolType.Tls11 |
                                                                  SecurityProtocolType.Tls12 |
                                                                  SecurityProtocolType.Tls13;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.Expect100Continue = true;
            }

            Trace.WriteLine("WSSMS sendSMSV2 conta=" + conta + ", to=" + to + ",from=" + from + ",user=" + user);

            string cgiError = null;
            try
            {
                var int32 = Convert.ToInt32(ConfigurationManager.AppSettings["maxSMSbody"]);
                Convert.ToInt32(ConfigurationManager.AppSettings["maxSMSHeader"]);
                msg = ReplaceExceptionChars(msg);
                if (msg.Length > int32)
                    return "-1";
                var str = emailFrom;
                if (str.Length >= 35)
                    str = str.Substring(0, str.Length - 4) + "...";
                msg = str + " " + msg;

                var url = ConfigurationManager.AppSettings["protocol"] + "://" +
                          ConfigurationManager.AppSettings["cgiHost"] + ":" +
                          ConfigurationManager.AppSettings["cgiPort"] + "/" +
                          ConfigurationManager.AppSettings["cgiLocalization"] + "/" +
                          ConfigurationManager.AppSettings["CGInameV2"];

                Trace.WriteLine("WSSMS creating WebRequest url=" + url);

                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";

                webRequest.UserAgent = "DC_SMS_SENDER";

                webRequest.ContentType = "application/x-www-form-urlencoded";
                var byteArray = Encoding.UTF8.GetBytes("la=" + largeAccount + "&nr_orig=" + from + "&nr_tel=" + to +
                                                  "&msg=" + msg.Replace(" ", "+"));
                webRequest.ContentLength = byteArray.Length;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    cgiError = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"))
                        .ReadToEnd();
                }

                setLogEntrie(conta, dominioFrom, dominioTo, to, msg, company, user, DateTime.Now, cgiError, "",
                    emailFrom);

                if (!cgiError.Equals("0"))
                {
                    Trace.WriteLine("WSSMS sendSMSV2 returned cgiError: " + cgiError);
                    return cgiError;
                }

                try
                {
                    var body = "SMS para o número " + to + " enviado com sucesso";
                    sendEmail(user, "inSapo", emailFrom, "SMS enviado através de InSapo", body);
                    return cgiError;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("WSSMS sendSMSV2 error -2");
                    Trace.WriteLine(ex);
                    return "-2";
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS sendSMSV2 error -1");
                Trace.WriteLine(ex);
                if (cgiError == null)
                    cgiError = "";
                setLogEntrie(conta, dominioFrom, dominioTo, to, msg, company, user, DateTime.Now, cgiError, ex.Message,
                    emailFrom);
                return "-1";
            }
        }

        [WebMethod]
        public DataSet getLog(string dominio, string initialDate, string finalDate)
        {
            try
            {
                return privGetLog(dominio, initialDate, finalDate);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS getLog ERROR: ");
                Trace.WriteLine(ex);

                return null;
            }
        }

        private string ReplaceExceptionChars(string msgOriginal)
        {
            var str = msgOriginal;
            try
            {
                var appSetting = ConfigurationManager.AppSettings["CharsException"];
                var chArray = new char[1] { ';' };
                foreach (var oldValue in appSetting.Split(chArray))
                    str = str.Replace(oldValue, "");
                return str;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("WSSMS ReplaceExceptionChars ERROR: ");
                Trace.WriteLine(ex);

                return msgOriginal;
            }
        }
    }
}