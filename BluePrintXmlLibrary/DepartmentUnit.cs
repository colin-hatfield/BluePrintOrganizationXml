using System;
using System.Collections.Generic;
using System.Text;

namespace BluePrintXmlLibrary
{
    public class DepartmentUnit
    {
        public Guid UnitID { get; set; }
        public string UnitName { get; set; }
        public bool isOrgLevel { get; set; }
        public List<DepartmentUnit> ChildDepartments { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
