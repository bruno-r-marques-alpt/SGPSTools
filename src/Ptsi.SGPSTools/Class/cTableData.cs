namespace SGPStools.Class
{
    /// <summary>
    /// Summary description for cTableData.
    /// </summary>
    public class cTableData
    {
        public string columHeader;
        public string adField;

        public cTableData()
        {
            columHeader = "";
            adField = "";
        }

        public cTableData(string pColumHeader, string pAdField)
        {
            columHeader = pColumHeader;
            adField = pAdField;
        }


    }
}
