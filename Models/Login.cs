using System.ComponentModel.DataAnnotations;

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


    }
}
