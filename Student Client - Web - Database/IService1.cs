using System.Collections.Generic;
using System.ServiceModel;

namespace Student_Client___Web___Database
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IList<Student> GetAllStudents();


        [OperationContract]
        Student GetStudentById(int id);


        [OperationContract]
        IList<Student> GetStudentsByName(string name);


        [OperationContract]
        int AddStudent(string name, int id);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}