using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BluePrintXmlLibrary
{
    public class XmlFileLoader : IOrganizationLoader
    {
        private string _fileSource = String.Empty;
        private DepartmentUnit _org = new DepartmentUnit();
        public DepartmentUnit GetOrganization()
        {
            XDocument xdoc = XDocument.Load(this._fileSource);

            DepartmentUnit orgroot = new DepartmentUnit();
            orgroot.Employees = new List<Employee>();
            orgroot.ChildDepartments = new List<DepartmentUnit>();

            orgroot.isOrgLevel = true;
            orgroot.UnitID = Guid.NewGuid();

            IEnumerable<XElement> employees = xdoc.Root.Elements("Employee");
            foreach (XElement xe in employees)
            {
                orgroot.Employees.Add(GetEmployee(xe, orgroot.UnitID, true));
            }

            IEnumerable<XElement> departments = xdoc.Root.Elements("Units");
            foreach (XElement xe in departments)
            {
                List<DepartmentUnit> departmentUnits = (GetDepartmentUnits(xe));
                foreach (DepartmentUnit deptUnit in departmentUnits)
                {
                    orgroot.ChildDepartments.Add(deptUnit);
                }
            }
            return orgroot;
        }

        public void SetDataSource(string source)
        {
            this._fileSource = source;
        }

        List<DepartmentUnit> GetDepartmentUnits(XElement xe)
        {
            List<DepartmentUnit> depts = new List<DepartmentUnit>();
            IEnumerable<XElement> departments = xe.Elements("Unit");
            foreach (XElement deptunit in departments)
            {
                depts.Add(GetDepartmentUnit(deptunit));
            }
            return depts;
        }

        DepartmentUnit GetDepartmentUnit(XElement xe)
        {
            DepartmentUnit dept = new DepartmentUnit();
            dept.Employees = new List<Employee>();
            dept.ChildDepartments = new List<DepartmentUnit>();

            dept.UnitID = Guid.NewGuid();
            dept.UnitName = xe.Attribute("Name").Value;

            IEnumerable<XElement> employees = xe.Elements("Employee");
            foreach (XElement emp in employees)
            {
                dept.Employees.Add(GetEmployee(emp, dept.UnitID, true));
            }

            IEnumerable<XElement> departments = xe.Elements("Units");
            foreach (XElement depts in departments)
            {
                List<DepartmentUnit> departmentUnits = (GetDepartmentUnits(depts));
                foreach (DepartmentUnit deptUnit in departmentUnits)
                {
                    dept.ChildDepartments.Add(deptUnit);
                }
            }
            return dept;
        }

        Employee GetEmployee(XElement xe, Guid unitID, bool isCSuite = false)
        {
            Employee emp = new Employee();
            emp.IsCSuite = isCSuite;
            emp.EmpName = xe.Value.ToString();
            emp.JobTitle = xe.Attribute("Title").Value.ToString();
            emp.UnitId = unitID;
            return emp;
        }
    }
}
