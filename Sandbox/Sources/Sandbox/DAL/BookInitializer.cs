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
            var author = new Author
            {
                Biography = "...",
                FirstName = "John",
                LastName = "Tolkien"
            };

            var books = new List<Book>
     {
       new Book {
       Author = author,
       Description = "...",
       Isbn = "1491914319",
       Synopsis = "...",
       Title = "Two Towers"
       },
       new Book {
       Author = author,
       Description = "...",
       Isbn = "1449319548",
       Synopsis = "...",
       Title = "The Fellowship of the Ring"
       },
       new Book {
       Author = author,
       Description = "...",
       Isbn = "1449309860",
       Synopsis = "...",
       Title = "The return of the King"
       },
       new Book {
       Author = author,
       Description = "...",
       Isbn = "1460954394",
       Synopsis = "...",
       Title = "The Hobbit: An Unexpected Journey"
        }
     };

            books.ForEach(b => context.Books.Add(b));

            context.SaveChanges();
        }
    }
}