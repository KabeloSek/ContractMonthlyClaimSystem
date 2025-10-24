using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ContractMonthlyClaimSystem.Models
{
    public class Login
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string role { get; set; }

        //Declaration of global connectionString variable
        private string connection = @"Server=(localdb)\claim_system;Database=claims_database;";

        public bool searchLogin(string email, string password, string role)
        {
            bool found = false;

            try{
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    //open connection
                    connect.Open();

                    //searching based on specific role you chose 
                    string tableName = "";
                    switch(role.Trim().ToLower())
                    {
                        case "lecturer":
                            tableName = "Lecturer";
                            break;
                        case "program_coordinator":
                            tableName = "Program_Coordinator";
                            break;
                        case "program_manager":
                            tableName = "Program_Manager";
                            break;
                        default:
                            throw new Exception("Invalid role specified.");
                    }

                    string query = @"SELECT * FROM "+tableName+" WHERE Email='"+email+"' AND Password='"+password+"' AND Role='"+role+"'";

                    if (tableName == "Lecturer")
                    {
                        try
                        {
                            using (SqlCommand insert = new SqlCommand(query, connect))
                            {
                                using (SqlDataReader find = insert.ExecuteReader())
                                {
                                    while (find.Read())
                                    {
                                        found = true;
                                        Console.WriteLine(find["LecturerID"]);
                                        Console.WriteLine(find["Name"]);
                                        Console.WriteLine(find["Surname"]);
                                    }
                                }// end datareader
                                Console.WriteLine("Lecturer Found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error during lecturer search: " + ex.Message);
                        }
                    }
                    else if (tableName == "Program_Coordinator")
                    {
                        try
                        {
                            using (SqlCommand insert = new SqlCommand(query, connect))
                            {
                                using (SqlDataReader find = insert.ExecuteReader())
                                {
                                    while (find.Read())
                                    {
                                        found = true;
                                        Console.WriteLine(find["CoordinatorID"]);
                                        Console.WriteLine(find["Name"]);
                                        Console.WriteLine(find["Surname"]);
                                    }
                                }// end datareader
                                Console.WriteLine("Program Coordinator Found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error during program coordinator search: " + ex.Message);
                        }
                    }
                    else if (tableName == "Program_Manager")
                    {
                        try
                        {
                            using (SqlCommand insert = new SqlCommand(query, connect))
                            {
                                using (SqlDataReader find = insert.ExecuteReader())
                                {
                                    while (find.Read())
                                    {
                                        found = true;
                                        Console.WriteLine(find["ManagerID"]);
                                        Console.WriteLine(find["Name"]);
                                        Console.WriteLine(find["Surname"]);
                                    }
                                }// end datareader
                                Console.WriteLine("Program Manager Found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error during program manager search: " + ex.Message);
                        }
                    }
                        connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("User not found" + error.Message);
            }
            return found;
        }//end searchLogin
        public int GetLecturerID(string email, string password, string role)
        {
            int id = 0;
            string tableName = role.ToLower() switch
            {
                "lecturer" => "Lecturer",
                "program_coordinator" => "Program_Coordinator",
                "program_manager" => "Program_Manager",
                _ => throw new Exception("Invalid role specified.")
            }; try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    string query = $"Select * from {tableName} WHERE Email=@Email AND Password=@Password";
                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password",password);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = role.ToLower() == "lecturer"
                                    ? (int)reader["LecturerID"]
                                    : role.ToLower() == "program_coordinator"
                                    ? (int)reader["CoordinatorID"]
                                    : (int)reader["ManagerID"];
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error fetching ID" + error.Message);
            }
            return id;
        }
    }
}
