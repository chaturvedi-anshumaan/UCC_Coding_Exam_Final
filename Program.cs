namespace UCC_Coding_Exam
{
    public class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Please enter the full path of file with extension at end:");
            string xmlFilePath = Console.ReadLine();
            string isAnXMLFile = Path.GetExtension(xmlFilePath);
            if(isAnXMLFile != null && isAnXMLFile.Contains(".xml"))
            {
                XMLFileReader reader = new XMLFileReader();
                reader.ReturnListOfGrantors(xmlFilePath);
                reader.WarnTheUserForPastRecordDate(xmlFilePath);
                reader.Update_Required_Attribute_Of_Beneficiary_LastName(xmlFilePath);
            }
            else
            {
                throw new ArgumentException("Invalid File! Please give a full path of an xml file only.");
            }
        }
    }
}
