using System;
using System.Collections.Generic;
using System.Text;

namespace OpenVote.Shared.Networking.Messages
{
    [Serializable]
    public class Post
    {
        public OrganizationType Ogranization { get; }
        public string Message { get; }
    }
}
