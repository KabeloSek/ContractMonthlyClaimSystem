using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Models
{
    public class Login
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

        public bool searchLecturer(string name, string surname, string email, string password, string role)
        {
            bool found = false;

            try{
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    //open connection
                    connect.Open();

                    string query = "@SELECT * FROM '"+role+"' WHERE Email='"+email+"' AND Password='"+password+"' AND Role='"+role+"'";

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
                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine();
            }
            return found;
        }
    }
}
