using ICCSharp.Networking;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;

namespace MaY
{
    public static class PeerPacketExtensions
    {
        public static Stream SerializePeerPacket<T>(this byte[] buffer,
                                                    ulong type,
                                                    BigInteger startPoint,
                                                    BigInteger endPoint)
        {
            PeerPacket<T> packet = new PeerPacket<T>();
            packet.Header.Type = type;
            packet.Header.FromPoint = startPoint;
            packet.Header.ToPoint = endPoint;
            Stream stream = new MemoryStream();
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            formatter.Serialize(stream, packet);
            return stream;
        }
        
        public static BinaryFormatter SerializePeerPacketHeader(this in PeerPacketHeader packetHeader)
        {
            Stream stream = new MemoryStream();
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            formatter.Serialize(stream, packetHeader);
            return formatter;
        }
        
        public static byte[] SerializePeerPacket<T>(this in PeerPacket<T> packet)
        {
            MemoryStream stream = new MemoryStream();
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            formatter.Serialize(stream, packet.Header);
            formatter.Serialize(stream, packet.Payload);
            return stream.ToArray();
        }

        public static PeerPacketHeader DeserializePeerPacketHeader(this List<byte> data)
        {
            return DeserializePeerPacketHeader(data, out _);
        }

        public static PeerPacketHeader DeserializePeerPacketHeader(this List<byte> data, out int headerSize)
        {
            Stream stream = new MemoryStream(data.ToArray());
            int origBuffer = (int)stream.Length;
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            PeerPacketHeader header = (PeerPacketHeader)formatter.Deserialize(stream);
            headerSize = origBuffer - (int)stream.Length;
            return header;
        }

        public static PeerPacketHeader DeserializePeerPacketHeader(this byte[] buffer)
        {
            return DeserializePeerPacketHeader(buffer, out _);
        }

        public static PeerPacketHeader DeserializePeerPacketHeader(this Stream stream)
        {
            int origBuffer = (int) stream.Length;
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            return (PeerPacketHeader) formatter.Deserialize(stream);
        }
        
        public static PeerPacketHeader DeserializePeerPacketHeader(this byte[] buffer, out int headerSize)
        {
            Stream stream = new MemoryStream(buffer);
            int origBuffer = (int) stream.Length;
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            PeerPacketHeader header = (PeerPacketHeader) formatter.Deserialize(stream);
            headerSize = origBuffer - (int) stream.Length;
            return header;
        }

        public static T DeserializePeerPayload<T>(this Stream stream)
        {
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            T payload = (T) formatter.Deserialize(stream);
            return payload;
        }

        public static PeerPacket<T> DeserializePeerPacket<T>(this Stream stream)
        {
            return DeserializePeerPacket<T>(stream, out _);
        }

        public static PeerPacket<T> DeserializePeerPacket<T>(this Stream stream, out int headerSize)
        {
            int origBuffer = (int) stream.Length;
            // Serialize an object into the storage medium referenced by 'stream' object.
            BinaryFormatter formatter = new BinaryFormatter();
            // Serialize multiple objects into the stream
            PeerPacket<T> packet = (PeerPacket<T>)formatter.Deserialize(stream);
            headerSize = origBuffer - (int)stream.Length;
            return packet;
        }
    }
}