using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace MaY
{
    [Serializable]
    public struct PeerPacketHeader
    {
        public ulong Type;
        public BigInteger FromPoint;
        public BigInteger ToPoint;
    }

    public struct PeerPacket<T>
    {
        public PeerPacketHeader Header;
        public T Payload;
    }
}
