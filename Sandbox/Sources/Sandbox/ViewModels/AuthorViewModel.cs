using System.ComponentModel.DataAnnotations;

namespace Sandbox.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Biography { get; set; }
    }
}