using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BluePrintXmlLibrary;
using Newtonsoft.Json;

namespace BluePrintTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string xmlFilePath = @"C:\dump\bad.xml";

            //Implementation 1.0
            ScriptyHackyWay(xmlFilePath);

            //Implementation 2.0
            EnterpriseyWay(xmlFilePath);

        }

        public static void EnterpriseyWay(string xmlFilePath)
        {
            IOrganizationLoader orgLoad = new XmlFileLoader();
            orgLoad.SetDataSource(xmlFilePath);

            DepartmentUnit org = orgLoad.GetOrganization();
        }

        public static void ScriptyHackyWay(string xmlFilePath)
        {
            if (File.Exists(xmlFilePath))
            {
                string destPath = @"C:\dump\organization.json";

                DisplayOrganizationsXmlToConsole(xmlFilePath);

                XDocument xdoc = SwitchDepartments(xmlFilePath, "Maintenance Team", "Platform Team");

                if (xdoc != null)
                {
                    string jsonOrg = ConvertXdocToJson(xdoc);
                    using (StreamWriter destfile = File.CreateText(destPath))
                    {
                        using (JsonTextWriter jtw = new JsonTextWriter(destfile))
                        {
                            jtw.WriteRaw(jsonOrg);
                        }
                    }
                }
                Console.WriteLine("Completed");
            } else
            {
                Console.Write("File {0} is not found. Exiting", xmlFilePath);
            }
        }

        static string ConvertXdocToJson(XDocument xdoc)
        {
            return JsonConvert.SerializeXNode(xdoc);
        }
        static XDocument SwitchDepartments(string xmlFilePath, string department1, string department2)
        {
            string deptName = String.Empty;
            XDocument xdoc = null;

            try
            {
                xdoc = XDocument.Load(xmlFilePath);
                foreach (XElement element in xdoc.Descendants())
                {
                    if (element.Name.LocalName.Equals("Unit"))
                    {
                        XAttribute unitName = (element.Attribute("Name"));
                        deptName = unitName.Value;

                        if (unitName.Value.Equals(department1))
                        {
                            unitName.SetValue(department2);
                            continue;   //if this isn't set, overwrites itself
                        }

                        if (unitName.Value.Equals(department2))
                        {
                            unitName.SetValue(department1);
                            continue;
                        }
                    }
                }
            }
            catch (XmlException xmlEx)
            {
                Console.Write("Xml Exception: {0}", xmlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            return xdoc;
        }

        static void DisplayOrganizationsXmlToConsole(string xmlFilePath)
        {
            string deptName = String.Empty;

            try
            {
                XDocument xdoc = XDocument.Load(xmlFilePath);
                foreach (XElement element in xdoc.Descendants())
                {
                    if (element.Name.LocalName.Equals("Organization"))
                    {
                        deptName = "C-Suite";
                    }
                    if (element.Name.LocalName.Equals("Unit"))
                    {
                        XAttribute unitName = (element.Attribute("Name"));
                        deptName = unitName.Value;
                    }
                    if (element.Name.LocalName.Equals("Employee"))
                    {
                        XAttribute jobTitle = (element.Attribute("Title"));
                        WriteEmployeeToConsole(element.Value, jobTitle.Value, deptName);
                    }
                }
            } catch (XmlException xmlEx)
            {
                Console.Write("Xml Exception: {0}", xmlEx.Message);
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
        static void WriteEmployeeToConsole(string employeeName, string positutionTitle, string unitName)
        {
            Console.WriteLine(String.Format("EmployeeName: {0}, Job Title: {1}, Unit {2}", employeeName, positutionTitle, unitName));
        }
    }
}