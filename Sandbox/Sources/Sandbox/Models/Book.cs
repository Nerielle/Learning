using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sandbox.Models
{
    public class Book
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Isbn { get; set; }

        public string Synopsis { get; set; }

        public string Description { get; set; }

        [Required]
        public virtual Author Author { get; set; }
    }
}