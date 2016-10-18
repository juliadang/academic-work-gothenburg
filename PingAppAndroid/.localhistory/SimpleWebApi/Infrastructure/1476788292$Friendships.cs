namespace SimpleWebApi.Infrastructure
{
    public class Friendships
    {
        public Friendships(string username1, string username2 )
        {
            Username1 = username1;
            User2 = username2;
        }
        public int FriendshipId { get; set; }
        public string Username1 { get; set; }
        public string User2 { get; set; }
    }
}