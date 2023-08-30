using SocialApi_NET7.Models;

namespace SocialApi_NET7.Services.CommentService
{
    public interface ICommentService
    {
        public List<Comment> GetComents();
        public Comment GetComment(int id);
        public Comment CreateComment(Comment comment);
    }
}
