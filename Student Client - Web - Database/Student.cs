using System.Runtime.Serialization;

namespace Student_Client___Web___Database
{
    [DataContract]
    public class Student
    {
        [DataMember] public int alder;

        [DataMember] public string navn;
    }
}