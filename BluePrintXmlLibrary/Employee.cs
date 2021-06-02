using System;
using System.Collections.Generic;
using System.Text;

namespace BluePrintXmlLibrary
{
    public class Employee
    {
        public Guid EmpId { get; set; }
        public bool IsCSuite { get; set; }
        public string JobTitle { get; set; }
        public string EmpName { get; set; }
        public Guid UnitId { get; set; }
    }
}
