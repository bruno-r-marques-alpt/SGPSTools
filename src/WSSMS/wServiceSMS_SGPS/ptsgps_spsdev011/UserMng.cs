// Decompiled with JetBrains decompiler
// Type: WindowsApplication1.ptsgps_spsdev011.UserMng
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
using System.Xml.Serialization;

namespace WindowsApplication1.ptsgps_spsdev011
{
    [DesignerCategory("code")]
    [WebServiceBinding(Name = "UserMngSoap", Namespace = "urn:PTelecom/Intranet/UserMngService")]
    [DebuggerStepThrough]
    public class UserMng : SoapHttpClientProtocol
    {
        public IdentityCredentials IdentityCredentialsValue;
        public UserCredentials UserCredentialsValue;

        public UserMng()
        {
            Url = ConfigurationManager.AppSettings["WebServiceADSearchURL"];
        }

        [SoapHeader("IdentityCredentialsValue")]
        [SoapDocumentMethod("urn:PTelecom/Intranet/UserMngService/batchRequest",
            RequestNamespace = "urn:oasis:names:tc:DSML:2:0:core", ResponseElementName = "batchResponse",
            ResponseNamespace = "urn:oasis:names:tc:DSML:2:0:core", Use = SoapBindingUse.Literal,
            ParameterStyle = SoapParameterStyle.Wrapped)]
        [SoapHeader("UserCredentialsValue")]
        [return: XmlElement("modifyResponse", typeof(modifyResponse))]
        [return: XmlElement("delResponse", typeof(delResponse))]
        [return: XmlElement("searchResponse", typeof(searchResponse))]
        [return: XmlElement("addResponse", typeof(addResponse))]
        [return: XmlElement("errorResponse", typeof(errorResponse))]
        public object[] batchRequest(
            [XmlElement("modifyRequest", typeof(modifyRequest))]
            [XmlElement("searchRequest", typeof(searchRequest))]
            [XmlElement("delRequest", typeof(delRequest))]
            [XmlElement("addRequest", typeof(addRequest))]
            object[] Items)
        {
            return (object[])Invoke("batchRequest", new object[1]
            {
                Items
            })[0];
        }

        public IAsyncResult BeginbatchRequest(
            object[] Items,
            AsyncCallback callback,
            object asyncState)
        {
            return BeginInvoke("batchRequest", new object[1]
            {
                Items
            }, callback, asyncState);
        }

        public object[] EndbatchRequest(IAsyncResult asyncResult)
        {
            return (object[])EndInvoke(asyncResult)[0];
        }
    }
}