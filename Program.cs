using System;
using System.IO;

namespace UCC_Coding_Exam
{
 
    public class Program
    {
        private static readonly string xmlFilePath = "..\\..\\sampleXML\\SampleXML.xml";
        public static void Main(string[] args)
        {
            
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
