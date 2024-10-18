namespace SGPStools.Class
{
    /// <summary>
    /// Summary description for cSearchField.
    /// </summary>
    public class cSearchField
    {

        public string searchField;
        public string searchValue;

        public cSearchField()
        {
            searchField = "";
            searchValue = "";
        }

        public cSearchField(string pSearchField, string oSearchValue)
        {
            searchField = pSearchField;
            searchValue = oSearchValue;
        }
    }
}
