using System.ComponentModel.DataAnnotations;

namespace PRY20232084.Models.DTOs
{
    public class UserModel
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public bool UserType { get; set; }

        [Required]
        public string Enterprise { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
