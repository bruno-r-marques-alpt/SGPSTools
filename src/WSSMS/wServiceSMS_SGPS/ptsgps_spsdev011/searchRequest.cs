// Decompiled with JetBrains decompiler
// Type: WindowsApplication1.ptsgps_spsdev011.searchRequest
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System.Xml.Serialization;

namespace WindowsApplication1.ptsgps_spsdev011
{
    [XmlType(Namespace = "urn:oasis:names:tc:DSML:2:0:core")]
    public class searchRequest
    {
        public attribute[] attributes;

        [XmlAttribute] public string derefAliases;

        [XmlAttribute] public string dn;

        public string filter;

        [XmlAttribute] public string scope;

        [XmlAttribute] public int sizeLimit;

        [XmlIgnore] public bool sizeLimitSpecified;

        [XmlAttribute] public int timeLimit;

        [XmlIgnore] public bool timeLimitSpecified;

        [XmlAttribute] public bool typesOnly;

        [XmlIgnore] public bool typesOnlySpecified;
    }
}