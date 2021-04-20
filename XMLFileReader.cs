using System;
using System.Linq;
using System.Xml.Linq;

namespace UCC_Coding_Exam
{
    public class XMLFileReader
    {
        private readonly XNamespace ermlNameSpace = "http://www.ingeo.com/2001/v2/documents";

        private readonly string saveToPath = @"D:\Practice-Dev\UCC_Coding_Exam\UpdatedXML\";
        public void ReturnListOFGrantors(string xmlFilePath)
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
            Console.WriteLine("Enter the file name with extension .xml");
            var fileName = Console.ReadLine();
            var query = (from e in doc.Descendants(ermlNameSpace + "LastName")
                         where e.Attribute("LabelKey")?.Value == "OriginalBeneficiary"
                         select e).First();

            if (query != null)
            {
                query.Attribute(@"required")?.SetValue("true");
                doc.Save(saveToPath + fileName);
            }

            Console.WriteLine("File saved to : " + saveToPath);
            Console.WriteLine("End of Application");
            Console.ReadLine();
        }
    }
}
