using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Student_Client___Web___Database
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int alder;

        [DataMember]
        public string navn;


    }
}