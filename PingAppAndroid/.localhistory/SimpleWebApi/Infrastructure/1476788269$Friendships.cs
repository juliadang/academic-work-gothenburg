﻿namespace SimpleWebApi.Infrastructure
{
    public class Friendships
    {
        public Friendships(string user1, string user2 )
        {
            User1 = user1;
            User2 = user2;
        }
        public int FriendshipId { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
    }
}