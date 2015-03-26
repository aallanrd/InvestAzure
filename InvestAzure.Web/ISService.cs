using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InvestAzure.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISService" in both code and config file together.
    [ServiceContract]
    public interface ISService
    {
        [OperationContract]
        List<Employee> GetAllEmp();

        [OperationContract]
        List<Department> GetAllDep();

        [OperationContract]
        bool DelEmp(int id);

        [OperationContract]
        bool DelDep(int id);

        [OperationContract]
        List<String> ExecProcedure();

        [OperationContract]
        List<Employee> getEmployees(int name);

        [OperationContract]
        Department GetDepIdbyName(String name);


        [OperationContract]
        Employee GetEmpByName(String name);

         [OperationContract]
         bool insertDep(String name, int enc, String ubic);

         [OperationContract]
         bool insertEmp(String name, int idDep, String fechaIngreso, String foto);

         [OperationContract]
         bool updateDep(int iD, String name, int enc, String ubic);

         [OperationContract]
         bool updateEmp(int iD, String name, int dep, String dir, String fecha);
         
    }
}
