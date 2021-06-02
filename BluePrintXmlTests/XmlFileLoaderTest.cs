using Microsoft.VisualStudio.TestTools.UnitTesting;

using BluePrintXmlLibrary;

namespace BluePrintXmlTests
{
    [TestClass]
    public class XmlFileLoaderTest
    {
        [TestMethod]
        public void LoadXmlFromFile()
        {
            string fileSource = @"C:\dump\organization.xml";
            XmlFileLoader xmlLoader = new XmlFileLoader();
            xmlLoader.SetDataSource(fileSource);
            xmlLoader.GetOrganization();
        }
    }
}
