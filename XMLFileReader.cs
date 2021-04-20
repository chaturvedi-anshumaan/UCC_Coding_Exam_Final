using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace UCC_Coding_Exam
{
    public class XMLFileReader
    {
        private readonly XNamespace ermlNameSpace = "http://www.ingeo.com/2001/v2/documents";


        public void ReturnListOfGrantors(string xmlFilePath)
        {
            XDocument xml = XDocument.Load(xmlFilePath);
            var query = from c in xml.Descendants(ermlNameSpace + "Multiple")
                        where (string)c.Attribute("tooltip") == "Grantor"
                        select (new
                                    {
                                        Title = c.Descendants(ermlNameSpace + "Title")?.Select(x => x.Value),
                                        FirstName = c.Descendants(ermlNameSpace + "FirstName")?.Select(x => x.Value),
                                        MiddleName = c.Descendants(ermlNameSpace + "MI")?.Select(x => x.Value),
                                        LastName = c.Descendants(ermlNameSpace + "LastName")?.Select(x => x.Value),
                                        Suffix = c.Descendants(ermlNameSpace + "Suffix")?.Select(x => x.Value)
                                    });

            Console.WriteLine("List of all grantors:\n");
            foreach (var nameList in query)
            {
                foreach (var fullName in nameList.Title.ZipToSignatoryName(
                    nameList.FirstName,
                    nameList.MiddleName,
                    nameList.LastName,
                    nameList.Suffix,
                    (fn, mn, ln, sfix) => fn + " " + mn + " " + ln + " " + sfix))
                {
                    Console.WriteLine(fullName);
                }
            }
        }

        public void WarnTheUserForPastRecordDate(string xmlFilePath)
        {
            XDocument xDocument = XDocument.Load(xmlFilePath);

            if (xDocument.Root != null)
            {
                var queryRecordingDate = from c in xDocument.Root.Descendants(ermlNameSpace + "Date")
                                         where (int)c.Attribute("id") == 0702
                                         select c?.Value.ToString();
                var recordedDate = DateTime.Parse(queryRecordingDate.First());

                if (recordedDate < DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n WARNING ! .Recorded date is in past.");
                }
                Console.ResetColor();
            }
        }

        public void Update_Required_Attribute_Of_Beneficiary_LastName(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);
            var query = (from e in doc.Descendants(ermlNameSpace + "LastName")
                         where e.Attribute("LabelKey")?.Value == "OriginalBeneficiary"
                         select e).First();

            string relativePath = "..\\..\\UpdatedXML\\";
            string filePath = relativePath + "UpdatedXML.xml";
            if (query != null && Directory.Exists(relativePath))
            {
                query.Attribute(@"required")?.SetValue("true");
                doc.Save(filePath);
            }
            else
            {
                Directory.CreateDirectory(relativePath);
                query.Attribute(@"required")?.SetValue("true");
                doc.Save(filePath);
            }

            Console.WriteLine("File saved to : " + filePath);
            Console.WriteLine("End of Application");
            Console.ReadLine();
        }
    }
}
