#pragma warning disable CS8618

namespace MotivationalQuotes.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        // public List<FavoriteQuote> Likes { get; set; } = new List<FavoriteQuote>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int PostedByUserId { get; set; }

        public User? PostedByUser { get; set; }

    }

    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }

    public class FavoriteQuote
    {
        public int FavoriteQuoteId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }

    }
    public class Like
    {
        public int LikeId { get; set; }
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
