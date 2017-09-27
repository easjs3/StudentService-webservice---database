using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Student_Client___Web___Database
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


//så vi kan læse fra vores WebConfig og få database string
      private static string GetConnectionString()
        {
            ConnectionStringSettingsCollection connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;
            ConnectionStringSettings connStringSettings = connectionStringSettingsCollection["MikDatabaseAzure"];
            return connStringSettings.ConnectionString;
        }



        

  

        public int AddStudent(string navn, int alder)
        {
            const string Addstudent = "insert into student (navn, alder) values (@navn, @alder)";
            using (SqlConnection DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (SqlCommand addstuCommand = new SqlCommand(Addstudent, DBconnection))
                {
                    addstuCommand.Parameters.AddWithValue("@alder", alder);
                    addstuCommand.Parameters.AddWithValue("@navn", navn);
                    
                    int rowsaffected = addstuCommand.ExecuteNonQuery();
                    return rowsaffected;
                }


            }
           
        }


        public IList<Student> GetAllStudents()
        {
            const string SelectAllStudents = "select * from student order by navn";

            using (SqlConnection dabConnection = new SqlConnection(GetConnectionString()))
            {
                dabConnection.Open();
                using (SqlCommand selecSqlCommand = new SqlCommand(SelectAllStudents, dabConnection))
                {
                    using (SqlDataReader reader = selecSqlCommand.ExecuteReader())
                    {
                        List<Student> StudentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = Readstudent(reader);
                            StudentList.Add(student);
                        }
                        return StudentList;
                    }
                }
            }

        }

        public Student GetStudentById(int alder)
        {
            const string SelectStudentByID = "select * from student where alder=@alder";

            using (SqlConnection DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(SelectStudentByID,DBconnection))
                {
                    selectCommand.Parameters.AddWithValue("@alder", alder);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();
                        Student student = Readstudent(reader);
                        return student;
                    }
                }
            }
        }








        public IList<Student> GetStudentsByName(string navn)
        {
            const string SelectByName = "select * from student where navn LIKE @navn";
            using (SqlConnection DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(SelectByName, DBconnection))
                {
                    selectCommand.Parameters.AddWithValue("@navn", navn);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Student> StudentListe = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = Readstudent(reader);
                            StudentListe.Add(student);
                        }
                        return StudentListe;
                    }
                }
            }
        }



        //vi laver en read student reader så vi kan fortælle hvordan man læser en student (vi opretter en og retunere den)
        private static Student Readstudent(IDataRecord reader)
        {
            int alder = reader.GetInt32(0);
            string navn = reader.GetString(1);

            Student student = new Student() {alder = alder, navn = navn};

            return student;
        }


    }
}
