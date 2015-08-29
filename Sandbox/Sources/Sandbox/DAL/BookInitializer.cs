using Sandbox.DAL;
using Sandbox.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Sandbox.DAL
{
    public class BookInitializer : DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            var authorProfessor = new Author
            {
                Biography = "...",
                FirstName = "John",
                LastName = "Tolkien",
                Books = new List<Book>
                             {
                               new Book {
                               Description = "...",
                               Isbn = "1491914319",
                               Synopsis = "...",
                               Title = "Two Towers"
                               },
                               new Book {
                               Description = "...",
                               Isbn = "1449319548",
                               Synopsis = "...",
                               Title = "The Fellowship of the Ring"
                               },
                               new Book {
                               Description = "...",
                               Isbn = "1449309860",
                               Synopsis = "...",
                               Title = "The return of the King"
                               },
                               new Book {
                               Description = "...",
                               Isbn = "1460954394",
                               Synopsis = "...",
                               Title = "The Hobbit: An Unexpected Journey"
                                }
                              }
            };

            var authorHarry = new Author
            {
                FirstName = "Harry",
                LastName = "Harrison",
                Biography = "American science fiction (SF) author",
                Books = new List<Book> { new Book { Title = "The Stainless Steel Rat", Isbn = "123456789" } }
            };
            context.Authors.Add(authorProfessor);
            context.Authors.Add(authorHarry);
            context.SaveChanges();
        }
    }
}