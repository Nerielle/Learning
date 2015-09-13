using System.ComponentModel.DataAnnotations;

namespace Sandbox.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Isbn { get; set; }

        public string Synopsis { get; set; }

        public string Description { get; set; }


        public virtual Author Author { get; set; }
    }
}