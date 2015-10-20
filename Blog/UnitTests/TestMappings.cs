using System;
using System.Linq;
using Dal;
using NHibernate.Linq;
using Xunit;

namespace UnitTests
{
    public class TestMappings
    {
        [Fact]
        public void CreateArticle()
        {
            var content = @"Winnie-the-Pooh, also called Pooh Bear, is a fictional anthropomorphic teddy bear created by English author A. A. Milne. The first collection of stories about the character was the book Winnie-the-Pooh (1926), and this was followed by The House at Pooh Corner (1928). Milne also included a poem about the bear in the children's verse book When We Were Very Young (1924) and many more in Now We Are Six (1927). All four volumes were illustrated by E. H. Shepard.

The Pooh stories have been translated into many languages, including Alexander Lenard's Latin translation, Winnie ille Pu, which was first published in 1958, and, in 1960, became the only Latin book ever to have been featured on The New York Times Best Seller list.[1]

Hyphens in the character's name were dropped by Disney when the company adapted the Pooh stories into a series of features that became one of its most successful franchises. In popular film adaptations, Pooh Bear has been voiced by actors Sterling Holloway, Hal Smith, and Jim Cummings in English and Yevgeny Leonov in Russian.

A live action adaptation of the film is currently in development with screenwriter Alex Ross Perry on board to write the screenplay. The story will focus on an adult Christopher Robin returning to the Hundred Acre Wood.[2]";
            var title = "Winnie-the-Pooh";
            var desc = @"From Wikipedia, the free encyclopedia.";

                using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var article = new Article
                {
                    Content = content,
                    Date = DateTime.UtcNow.Date,
                    Title = title,
                    Description = desc
                };
                session.Save(article);
                tx.Commit();
            }
            using (var session = SessionFactory.OpenSession())
            {
                var article = session.Query<Article>().FirstOrDefault();
                Assert.NotNull(article);
                Assert.Equal(content, article.Content);
                Assert.Equal(title, article.Title);
            }
        }
        [Fact]
        public void CreateArticleWithComment()
        {
            var content = "Content";
            var title = "Title2";
            var commentContent = "Comment Content";

            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                
                var article = new Article
                {
                    Content = content,
                    Date = DateTime.UtcNow.Date,
                    Title = title
                };
                article.Comments.Add(new Comment()
                {
                    Content = commentContent,
                    Date = DateTime.UtcNow.Date,
                });
                session.Save(article);
                tx.Commit();
            }
            using (var session = SessionFactory.OpenSession())
            {
                var article = session.Query<Article>().FirstOrDefault(x=>x.Title == title);
                Assert.NotNull(article);
                Assert.Equal(content, article.Content);

                var comment = session.Query<Comment>().FirstOrDefault();
                Assert.NotNull(comment);
                Assert.Equal(commentContent, comment.Content);
                //Assert.Equal(article.Id, comment.Article.Id);
            }
        }
    }
}