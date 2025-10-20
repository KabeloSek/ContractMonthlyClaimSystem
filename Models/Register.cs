using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class Register
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string surname { get; set; }

        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }


        //Declaration of global connectionString variable
        private string connection = @"Server=(localdb)\claim_system;Database=claims_database;";

        public void Create_Lecturer_table()
        {
            //try and catch for error handling
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    //SQL query to create Lectures table
                    string LecturerTable = @"CREATE TABLE Lecturers (
                                            LecturerID INT PRIMARY KEY IDENTITY(1,1),
                                            Name VARCHAR(50) NOT NULL,
                                            Surname VARCHAR(50) NOT NULL,
                                            Email VARCHAR(100) NOT NULL UNIQUE,
                                            Password VARCHAR(75) NOT NULL
                                        );";


                    //using function to command query to execute
                    using (SqlCommand create = new SqlCommand(LecturerTable, connect))
                    {
                        //execute create
                        create.ExecuteNonQuery();
                        Console.WriteLine("Lecturer table Created");

                    }
                    connect.Close();
                }
            }
            catch (Exception error)
            {
                //error message if try fails
                Console.WriteLine("Error creating tables" + error.Message);
            }
        }//end of create_tables method

        public void Store_Lecturer(string name, string surname, string email, string password)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    //Query to insert into Lecturers table
                    string insertIntoLectuer = @"INSERT INTO Lecturers
                                                 VALUES
                                                    ('" + name + "','" + surname + "','" + email + "','" + password + "');";

                    //using command to execute query
                    using (SqlCommand insert = new SqlCommand(insertIntoLectuer, connect))
                    {
                        insert.ExecuteNonQuery();
                        Console.WriteLine("Lecturer data inserted successfully");
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Lecture could not be inserted into database" + error.Message);
            }
        }//end_ storeLecturer method

    }
}
