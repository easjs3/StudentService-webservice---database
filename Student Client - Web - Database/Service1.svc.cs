using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Student_Client___Web___Database
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public int AddStudent(string navn, int alder)
        {
            const string Addstudent = "insert into student (navn, alder) values (@navn, @alder)";
            using (var DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (var addstuCommand = new SqlCommand(Addstudent, DBconnection))
                {
                    addstuCommand.Parameters.AddWithValue("@alder", alder);
                    addstuCommand.Parameters.AddWithValue("@navn", navn);

                    var rowsaffected = addstuCommand.ExecuteNonQuery();
                    return rowsaffected;
                }
            }
        }


        public IList<Student> GetAllStudents()
        {
            const string SelectAllStudents = "select * from student order by navn";

            using (var dabConnection = new SqlConnection(GetConnectionString()))
            {
                dabConnection.Open();
                using (var selecSqlCommand = new SqlCommand(SelectAllStudents, dabConnection))
                {
                    using (var reader = selecSqlCommand.ExecuteReader())
                    {
                        var StudentList = new List<Student>();
                        while (reader.Read())
                        {
                            var student = Readstudent(reader);
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

            using (var DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (var selectCommand = new SqlCommand(SelectStudentByID, DBconnection))
                {
                    selectCommand.Parameters.AddWithValue("@alder", alder);
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;
                        reader.Read();
                        var student = Readstudent(reader);
                        return student;
                    }
                }
            }
        }


        public IList<Student> GetStudentsByName(string navn)
        {
            const string SelectByName = "select * from student where navn LIKE @navn";
            using (var DBconnection = new SqlConnection(GetConnectionString()))
            {
                DBconnection.Open();
                using (var selectCommand = new SqlCommand(SelectByName, DBconnection))
                {
                    selectCommand.Parameters.AddWithValue("@navn", navn);
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        var StudentListe = new List<Student>();
                        while (reader.Read())
                        {
                            var student = Readstudent(reader);
                            StudentListe.Add(student);
                        }
                        return StudentListe;
                    }
                }
            }
        }


//så vi kan læse fra vores WebConfig og få database string
        private static string GetConnectionString()
        {
            var connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;
            var connStringSettings = connectionStringSettingsCollection["MikDatabaseAzure"];
            return connStringSettings.ConnectionString;
        }


        //vi laver en read student reader så vi kan fortælle hvordan man læser en student (vi opretter en og retunere den)
        private static Student Readstudent(IDataRecord reader)
        {
            var alder = reader.GetInt32(0);
            var navn = reader.GetString(1);

            var student = new Student {alder = alder, navn = navn};

            return student;
        }
    }
}