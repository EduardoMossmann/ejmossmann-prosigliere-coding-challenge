using BloggingPlatform.Application.Models;

namespace BloggingPlatform.Tests.Factories
{
    public static class CommentFactory
    {
        public static CommentRequest GenerateCommentRequest()
        {
            return new CommentRequest()
            {
                Title = "Comment Title 1",
                Content = "Comment Content 1"
            };
        }
    }
}
