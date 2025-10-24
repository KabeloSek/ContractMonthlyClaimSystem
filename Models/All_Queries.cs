using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Security.Claims;

namespace ContractMonthlyClaimSystem.Models
{
    public class All_Queries
    {
        public string claimID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int sessions { get; set; }
        [Required]
        public int hoursWorked { get; set; }
        [Required]
        public int hourlyRate { get; set; }
        [Required]
        public string document { get; set; }



        //Declaration of global connectionString variable
        private string connection = @"Server=(localdb)\claim_system;Database=claims_database;";
        public string createClaimsTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    string claimsTable = @"IF OBJECT_ID('dbo.Claims','U') IS NULL
                                           BEGIN
                                                CREATE TABLE Claims(
                                                    ClaimID INT PRIMARY KEY IDENTITY(1,1),
                                                    Name Varchar(50) NOT NULL,
                                                    Sessions INT,
                                                    HoursWorked INT,
                                                    HourlyRate INT,
                                                    Document VARCHAR(100)
                                                    );               
                                           END";

                   
                    using (SqlCommand createclaimsTable = new SqlCommand(claimsTable, connect))
                    {
                        createclaimsTable.ExecuteNonQuery();
                        Console.WriteLine("Claims table created");
                    }
                   
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error creating claims table:" + error.Message);


            }
            return "Claim table created successfully";

        }//end createClaimsTable
        public string Store_claims(string name, int sessions, int hoursWorked, int hourlyRate, string document)
        {

            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    //query to insert into claims
                    string insertIntoClaims = @"INSERT INTO Claims
                                                VALUES
                                                ('" + name + "'," + sessions + "," + hoursWorked + "," + hourlyRate + ",'" + document + "');";

                    using (SqlCommand insert = new SqlCommand(insertIntoClaims, connect))
                    {
                        insert.ExecuteNonQuery();
                        Console.WriteLine("Claim stored successfully");
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error storing claim: " + error.Message);
            }
            return "Claim stored successfully";
        }//end store_claims
        public List<All_Queries> GetAllClaims()
        {
            List<All_Queries> claims = new List<All_Queries>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    string query = @"SELECT * FROM Claims";
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        All_Queries claim = new All_Queries
                        {
                            claimID = reader["ClaimID"].ToString(),
                            name = reader["Name"].ToString(),
                            sessions = Convert.ToInt32(reader["Sessions"]),
                            hoursWorked = Convert.ToInt32(reader["HoursWorked"]),
                            hourlyRate = Convert.ToInt32(reader["HourlyRate"]),
                            document = reader["Document"].ToString()
                        };
                        claims.Add(claim);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error retrieving claims: " + error.Message);
            }
            return claims;
        }//end GetAllClaims


    }
}
