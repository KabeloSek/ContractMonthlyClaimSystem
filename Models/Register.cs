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

        [Required]
        public string role { get; set; }


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
                    string LecturerTable = @"IF OBJECT_ID('dbo.Lecturer','U') IS NULL
                                            BEGIN
                                            CREATE TABLE Lecturer (
                                            LecturerID INT PRIMARY KEY IDENTITY(1,1),
                                            Name VARCHAR(50) NOT NULL,
                                            Surname VARCHAR(50) NOT NULL,
                                            Email VARCHAR(100) NOT NULL UNIQUE,
                                            Password VARCHAR(75) NOT NULL,
                                            Role VARCHAR(50) NOT NULL
                                           );
                                            END";

                    
                    string ProgramCoordinatorTable = @"IF OBJECT_ID('dbo.Program_Coordinator','U') IS NULL
                                            BEGIN
                                            CREATE TABLE Program_Coordinator (
                                            CoordinatorID INT PRIMARY KEY IDENTITY(1,1),
                                            Name VARCHAR(50) NOT NULL,
                                            Surname VARCHAR(50) NOT NULL,
                                            Email VARCHAR(100) NOT NULL UNIQUE,
                                            Password VARCHAR(75) NOT NULL,
                                            Role VARCHAR(50) NOT NULL
                                           );
                                            END";

                    string ProgramManagerTable = @"IF OBJECT_ID('dbo.Program_Manager','U') IS NULL
                                            BEGIN
                                            CREATE TABLE Program_Manager (
                                            ManagerID INT PRIMARY KEY IDENTITY(1,1),
                                            Name VARCHAR(50) NOT NULL,
                                            Surname VARCHAR(50) NOT NULL,
                                            Email VARCHAR(100) NOT NULL UNIQUE,
                                            Password VARCHAR(75) NOT NULL,
                                            Role VARCHAR(50) NOT NULL
                                           );
                                            END";
                    //using function to command query to execute
                    using (SqlCommand createLecturer = new SqlCommand(LecturerTable, connect))
                    {
                        //execute create
                        createLecturer.ExecuteNonQuery();
                        Console.WriteLine("Lecturer table Created");

                    }
                    using (SqlCommand createProgCoordinator = new SqlCommand(ProgramCoordinatorTable, connect))
                    {
                        //execute create
                        createProgCoordinator.ExecuteNonQuery();
                        Console.WriteLine("Program Coordinator table Created");
                    }
                    using(SqlCommand createProgManager = new SqlCommand(ProgramManagerTable, connect))
                    {
                        //execute create
                        createProgManager.ExecuteNonQuery();
                        Console.WriteLine("Program Manager table Created");
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

        public void Store_Lecturer(string name, string surname, string email, string password, string role)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    //Query to insert into Lecturers table
                    string insertIntoLectuer = @"INSERT INTO Lecturer
                                                 VALUES
                                                    ('" + name + "','" + surname + "','" + email + "','" + password + "','"+role+"');";

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
        public void Store_ProgramCoordinator(string name, string surname, string email, string password, string role)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    //Query to insert into Program Coordinator table
                    string insertIntoProgCoordinator = @"INSERT INTO Program_Coordinator
                                                 VALUES
                                                    ('" + name + "','" + surname + "','" + email + "','" + password + "','" + role + "');";
                    //using command to execute query
                    using (SqlCommand insert = new SqlCommand(insertIntoProgCoordinator, connect))
                    {
                        insert.ExecuteNonQuery();
                        Console.WriteLine("Program Coordinator data inserted successfully");
                    }
                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Program Coordinator could not be inserted into database" + error.Message);
            }
        }//end_ storeProgramCoordinator method

        public void Store_ProgramManager(string name, string surname, string email, string password, string role)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    //Query to insert into Program Manager table
                    string insertIntoProgManager = @"INSERT INTO Program_Manager
                                                 VALUES
                                                    ('" + name + "','" + surname + "','" + email + "','" + password + "','" + role + "');";
                    //using command to execute query
                    using (SqlCommand insert = new SqlCommand(insertIntoProgManager, connect))
                    {
                        insert.ExecuteNonQuery();
                        Console.WriteLine("Program Manager data inserted successfully");
                    }
                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Program Manager could not be inserted into database" + error.Message);
            }
        }//end_ storeProgramManager method
    }
}
