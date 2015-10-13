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
            var content = "Content";
            var title = "Title";
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var article = new Article
                {
                    Content = content,
                    Date = DateTime.UtcNow.Date,
                    Title = title
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
                var comment = new Comment()
                {
                    Content = commentContent,
                    Date = DateTime.UtcNow.Date,
                    //Article = article
                };
                article.Comments.Add(comment);
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