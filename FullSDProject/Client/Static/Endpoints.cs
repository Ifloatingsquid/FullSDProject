using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDProject.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string ChatsEndpoint = $"{Prefix}/chats";
        public static readonly string DatingUsersEndpoint = $"{Prefix}/datingusers";
        public static readonly string MatchesEndpoint = $"{Prefix}/matches";
        public static readonly string PersonalProfilesEndpoint = $"{Prefix}/personalprofiles";
        public static readonly string PostsEndpoint = $"{Prefix}/posts";
        public static readonly string TopicsEndpoint = $"{Prefix}/topics";
    }
}
