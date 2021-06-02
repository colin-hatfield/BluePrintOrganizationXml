using System;
using System.Collections.Generic;
using System.Text;

namespace BluePrintXmlLibrary
{
    public interface IOrganizationLoader
    {
        public DepartmentUnit GetOrganization();
        public void SetDataSource(string source);
    }
}
