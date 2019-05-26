using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MaY;
using OpenVote.Shared.Networking;
using OpenVote.Shared.Networking.Messages;

namespace OpenVote.Shared
{
    public class Peer : PeerBase
    {
        private readonly Queue<IBlock> _blockChain = new Queue<IBlock>();
        private readonly Dictionary<PacketType, object> _nextBlockToCreate = new Dictionary<PacketType, object>();
        
        public const string DEFAULT_PORT = "49001";

        public Peer()
            : base(GetLocalIpAddress(), DEFAULT_PORT, SHA512.Create())
        {
        }

        public Peer(string port)
            : base(GetLocalIpAddress(), port, SHA512.Create())
        {
        }

        public Peer(string ipAddress, string port, SHA512 hasher)
            : base(ipAddress, port, hasher)
        {
        }

        /// <summary>
        /// Add vote to queue for applying
        /// </summary>
        /// <param name="vote">Vote to create</param>
        public void ApplyVote(Vote vote)
        {
            _nextBlockToCreate.Add(PacketType.Vote, vote);
        }

        /// <summary>
        /// Add post to queue for applying
        /// </summary>
        /// <param name="post">Post to create</param>
        public void ApplyPost(Post post)
        {
            _nextBlockToCreate.Add(PacketType.Post, post);
        }

        protected override byte[] PreparePeerData<T>(IPeerProxy peer, T data)
        {
            byte[] result = null;
            var packetTypeAttribute = (PacketTypeAttribute) data.GetType()
                .GetCustomAttributes(typeof(PacketTypeAttribute), true)
                .FirstOrDefault();
            if (packetTypeAttribute != null)
            {
                PeerPacket<T> votePacket = new PeerPacket<T>();
                votePacket.Header.Type = (ulong) packetTypeAttribute.PacketType;
                votePacket.Header.FromPoint = PeerId;
                votePacket.Header.ToPoint = peer.PeerId;
                votePacket.Payload = data;
                result = votePacket.SerializePeerPacket();
            }
            return result;
        }
        
        protected override async Task OnClientDataReceived(IPeerProxy client, byte[] data)
        {
            Stream stream = new MemoryStream(data); 
            PeerPacketHeader packetHeader =  stream.DeserializePeerPacketHeader();
            switch ((PacketType) packetHeader.Type)
            {
                case PacketType.System:
                {
                    HandleSystemPacket(client, stream);
                }
                    break;
                case PacketType.Post:
                {
                   HandlePostPacket(client, stream);
                }
                    break;
                case PacketType.Vote:
                {
                    HandleVotePacket(stream);
                }
                    break;
            }
        }

        private void HandleVotePacket(Stream stream)
        {
            Vote vote = stream.DeserializePeerPayload<Vote>();
//                    nextBlockToCreate.Add(PacketType.Vote, votePacket.Payload);
            OrganizationType orgType = (OrganizationType) vote.Organization;
            Console.WriteLine($"vote.Organization = {orgType}");
            switch (orgType)
            {
                case OrganizationType.Union:
                    throw new NotSupportedException("Union operation is not yet supported !!");
                case OrganizationType.Company:
                    HandleCompanyVote(vote);
                    break;
                case OrganizationType.Village:
                    HandleVillageVote(vote);
                    break;
                case OrganizationType.City:
                    HandleCityVote(vote);
                    break;
                case OrganizationType.Government:
                    HandleCountryVote(vote);
                    break;
                case OrganizationType.Country:
                    HandleCountryVote(vote);
                    break;
                case OrganizationType.Anonymous:
                    throw new NotSupportedException("Anonymous operation is not yet supported !!");
                default:
                    throw new ArgumentOutOfRangeException();
            }

//                      blockInProcess = Block<SHA512Hasher>.CreateBlock(vote);
        }

        private void HandlePostPacket(IPeerProxy client, Stream data)
        {
            PeerPacket<Post> postPacket = data.DeserializePeerPacket<Post>();
            _nextBlockToCreate.Add(PacketType.Post, postPacket.Payload);
        }

        private void HandleSystemPacket(IPeerProxy client, Stream data)
        {
            PeerPacket<Networking.Messages.System> systemPacket = data.DeserializePeerPacket<Networking.Messages.System>();
            Networking.Messages.System systemInfo = systemPacket.Payload;
            SystemType systemEvent = systemInfo.SystemEvent;
            switch (systemEvent)
            {
                case SystemType.CREATE_NEW_BLOCK:
                    break;
                case SystemType.FIND_BLOCK:
                    break;
                case SystemType.READ_BLOCK:
                    break;
            }
        }

        private void HandleCompanyVote(Vote vote)
        {
            CompanyVoteType companyVoteType = (CompanyVoteType) vote.Type;
            Console.WriteLine($"companyVoteType = {companyVoteType}");
        }

        private static void HandleCityVote(Vote vote)
        {
            CityVoteType cityVoteType = (CityVoteType) vote.Type;
            Console.WriteLine($"vote.Type = {cityVoteType}");
            Console.WriteLine($"votePacket.Result = {vote.Result}");
        }

        private static void HandleVillageVote(Vote vote)
        {
            VillageVoteType villageVoteType = (VillageVoteType) vote.Type;
            Console.WriteLine($"villageVoteType = {villageVoteType}");
        }

        private static void HandleCountryVote(Vote vote)
        {
            CountryVoteType countryVoteType = (CountryVoteType) vote.Type;
            Console.WriteLine($"countryVoteType = {countryVoteType}");
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system !!");
        }
    }
}