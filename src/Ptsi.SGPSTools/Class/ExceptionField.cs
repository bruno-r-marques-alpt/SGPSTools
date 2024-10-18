namespace SGPStools.Class
{
    /// <summary>
    /// Summary description for ExceptionField.
    /// </summary>
    public class cExceptionField
    {

        public string nameField;
        public string urlPage;

        public cExceptionField()
        {
            nameField = "";
            urlPage = "";
        }

        public cExceptionField(string pNameField, string pUrlPage)
        {
            nameField = pNameField;
            urlPage = pUrlPage;
        }

    }
}
