using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using static OpenVote.Shared.PacketType;

namespace OpenVote.Shared.Networking.Messages
{
    [Serializable]
    [PacketTypeAttribute(PacketType.Vote)]
    public class Vote
    {
        public Vote(int organization, int type, int result)
        {
            Organization = organization;
            Type = type;
            Result = result;
        }

        public int Organization { get; }
        public int Type { get; }
        public int Result { get; }
    }
}
