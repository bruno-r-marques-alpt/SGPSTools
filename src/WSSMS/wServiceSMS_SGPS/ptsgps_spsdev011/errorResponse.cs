// Decompiled with JetBrains decompiler
// Type: WindowsApplication1.ptsgps_spsdev011.errorResponse
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System.Xml.Serialization;

namespace WindowsApplication1.ptsgps_spsdev011
{
    [XmlType(Namespace = "urn:oasis:names:tc:DSML:2:0:core")]
    public class errorResponse
    {
        public string detail;
        public string message;

        [XmlAttribute] public string type;
    }
}