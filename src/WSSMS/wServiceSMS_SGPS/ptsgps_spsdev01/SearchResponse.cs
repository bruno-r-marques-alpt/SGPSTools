// Decompiled with JetBrains decompiler
// Type: WindowsApplication1.ptsgps_spsdev01.SearchResponse
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System.Xml.Serialization;

namespace WindowsApplication1.ptsgps_spsdev01
{
    [XmlType(Namespace = "urn:PTelecom/Intranet/SearchService")]
    public class SearchResponse
    {
        public Item[] List;

        [XmlAttribute] public int RangeMax;

        [XmlIgnore] public bool RangeMaxSpecified;

        [XmlAttribute] public int RangeMin;

        [XmlIgnore] public bool RangeMinSpecified;

        [XmlAttribute] public int TotalHits;

        [XmlIgnore] public bool TotalHitsSpecified;
    }
}