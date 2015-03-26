using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Net;


namespace InvestAzure.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SService.svc or SService.svc.cs at the Solution Explorer and start debugging.
    public class SService : ISService
    {
        Dep_LinqDataContext depContext = new Dep_LinqDataContext();
        Emp_LinqDataContext empContext = new Emp_LinqDataContext();

        public bool insertDep(String name, int enc, String ubic)
        {
           
            Department dep = new Department
            {
                
                nbrDepto = name,
                idEncargado = enc,
                ubicacion = ubic
            };
          
              depContext.Departments.InsertOnSubmit(dep);
              depContext.SubmitChanges();
              return true;
            
            
          

        }


        public bool insertEmp(String name, int idDep, String fechaIngreso, String foto)
        {

            try
            {
             DateTime saveNow = DateTime.Now;
            Employee ord = new Employee
            {
                
                nbrEmpleado = name,
                IdDepto = idDep,
                fechaIngreso = saveNow.ToShortDateString(),
                fotoStr = foto
                
            };
            
                empContext.Employees.InsertOnSubmit(ord);
                empContext.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
           
           
        }

        public List<Employee> GetAllEmp()
        {
            List<Employee> employ = new List<Employee>();
            var employees = from Employee in empContext.Employees
                            orderby Employee.IdEmpleado
                            select Employee;
            try
            {
                 employ = employees.ToList<Employee>();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error", "Mensaje System", MessageBoxButton.OK);
            }
           return employ;

        }

        public List<Department> GetAllDep()
        {
            var departs = from Departments in depContext.Departments
                          orderby Departments.IdDepto
                          select Departments;
            return departs.ToList<Department>();

        }

        public bool DelEmp(int id)
        {
            var listToRemove =  from Employee  in empContext.Employees
                                where Employee.IdEmpleado.Equals(id)
                                select Employee;

            if(listToRemove==null) {
                return false;
            }
            else
            {

            empContext.Employees.DeleteAllOnSubmit(listToRemove);
            empContext.SubmitChanges();
            return true;
             }
        }

        public bool DelDep(int id)
        {
            var listToRemove = from Department in depContext.Departments
                               where Department.IdDepto.Equals(id)
                               select Department;

            if (listToRemove == null)
            {
                return false;
            }
            else
            {

                depContext.Departments.DeleteAllOnSubmit(listToRemove);
                depContext.SubmitChanges();
                return true;
            }
        }


        public List<String> ExecProcedure()
        {

            depContext.Connection.Open();
            List<String> miList = new List<string>();
            var listofProced = depContext.allProcedures();

            foreach (var r in listofProced)
            {
                miList.Add(r.SPECIFIC_NAME);
            
            }
            depContext.Connection.Close();
            return miList;


        }


        public List<Employee> getEmployees(int name)
        {
           // empContext.Connection.Open();
            List<Employee> employ = new List<Employee>();
            var employees = from Employee in empContext.Employees
                            where Employee.IdDepto == name
                            orderby Employee.IdEmpleado
                           select Employee;
            try
            {
                employ = employees.ToList<Employee>();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error", "Mensaje System", MessageBoxButton.OK);
            }
           // empContext.Connection.Close();
            return employ;

        }



        public Department GetDepIdbyName(String name)
        
        {

            depContext.Connection.Open();
            Department ret = new Department();
            var departs = from Departments in depContext.Departments
                          where Departments.nbrDepto == name
                          
                          select Departments;
         
                foreach (var r in departs)
                {
                    if (r == null)
                    {
                        return null;
                    }
                    else
                    {
                        ret = r;
                        break;
                    }
                }
            
            depContext.Connection.Close();
            return ret;
            

        }

        public Employee GetEmpByName(String name)
        {

            empContext.Connection.Open();
            Employee ret = new Employee();
            var departs = from Employees in empContext.Employees
                          where Employees.nbrEmpleado == name

                          select Employees;

            foreach (var r in departs)
            {
                if (r == null)
                {
                    return null;
                }
                else
                {
                    ret = r;
                    break;
                }
            }

            depContext.Connection.Close();
            return ret;


        }
        public bool updateDep(int iD, String name, int enc, String ubic)
        {

            var cust =
           (from c in depContext.Departments
            where c.IdDepto == iD
              select c).First();

            // Change the name of the contact.
            cust.nbrDepto = name;
            cust.ubicacion = ubic;

            depContext.SubmitChanges();
            return true;
        }

        public bool updateEmp( int iD,String name, int dep, String dir,String fecha)
        {
           // empContext.Connection.Open();
            
                var cust =
                    (from c in empContext.Employees
                     where c.IdEmpleado == iD
                     select c).First();

                // Change the name of the contact.
                cust.nbrEmpleado = name;
                cust.fechaIngreso = fecha;
                cust.fotoStr = dir;
                cust.IdDepto = dep;
                

                empContext.SubmitChanges();
               // empContext.Connection.Open();
                return true;
           
           
        }

     


    }
}
