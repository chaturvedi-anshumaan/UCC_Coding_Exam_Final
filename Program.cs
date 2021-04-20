namespace UCC_Coding_Exam
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string XML_FILE_PATH = "D:\\Practice-Dev\\UCC_Coding_Exam\\sampleXML\\SampleXML.xml";
            XMLFileReader reader = new XMLFileReader();
            reader.ReturnListOFGrantors(XML_FILE_PATH);
            reader.WarnTheUserForPastRecordDate(XML_FILE_PATH);
            reader.Update_Required_Attribute_Of_Beneficiary_LastName(XML_FILE_PATH);
        }
    }
}
