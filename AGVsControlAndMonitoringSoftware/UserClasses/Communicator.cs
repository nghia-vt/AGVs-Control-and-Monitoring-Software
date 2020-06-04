using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AGVsControlAndMonitoringSoftware
{
    class Communicator
    {
        public const ushort AGVInfoReceivePacketSize = 21;

        private static SerialPort _serialPort = new SerialPort();
        public static SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }
    }

    /* AGV information packet (21 bytes):
     * Header		2 byte  -> 0xFFAA
     * FunctionCode	1 byte  -> 0x01
     * AGVID 		1 byte	
     * Status		1 byte  -> 'R', 'S', 'P', 'D'
     * ExitNode	    2 byte	
     * Distance	    4 byte	
     * Orient		1 byte  -> 'E', 'W', 'N', 'S'
     * Velocity	    4 byte	
     * Battery		1 byte	
     * CheckSum	    2 byte  -> sum of 17 bytes from Header to Battery
     * EndOfFrame	2 byte  -> 0x0D0A
     */
    public struct AGVInfoReceivePacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte Status;
        public ushort ExitNode;
        public float DisToExitNode;
        public byte Orient;
        public float Velocity;
        public byte Battery;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Converting Structs to Byte Arrays (method 1)
        public static AGVInfoReceivePacket FromArray(byte[] bytes)
        {
            var reader = new System.IO.BinaryReader(new System.IO.MemoryStream(bytes));

            var s = default(AGVInfoReceivePacket);

            s.Header = reader.ReadBytes(2);
            s.FunctionCode = reader.ReadByte();
            s.AGVID = reader.ReadByte();
            s.Status = reader.ReadByte();
            s.ExitNode = reader.ReadUInt16();
            s.DisToExitNode = reader.ReadSingle();
            s.Orient = reader.ReadByte();
            s.Velocity = reader.ReadSingle();
            s.Battery = reader.ReadByte();
            s.CheckSum = reader.ReadUInt16();
            s.EndOfFrame = reader.ReadBytes(2);

            return s;
        }

        // Converting Structs to Byte Arrays (method 2)
        public static AGVInfoReceivePacket GetFromArray(byte[] bytes) 
        {
            AGVInfoReceivePacket packet = new AGVInfoReceivePacket();
            packet.Header = new byte[2] { bytes[0], bytes[1] };
            packet.FunctionCode = bytes[2];
            packet.AGVID = bytes[3];
            packet.Status = bytes[4]; // use Convert.ToChar() to see the character
            packet.ExitNode = BitConverter.ToUInt16(bytes, 5);
            packet.DisToExitNode = BitConverter.ToSingle(bytes, 7);
            packet.Orient = bytes[11]; // use Convert.ToChar() to see the character
            packet.Velocity = BitConverter.ToSingle(bytes, 12);
            packet.Battery = bytes[16];
            packet.CheckSum = BitConverter.ToUInt16(bytes, 17);
            packet.EndOfFrame = new byte[2] { bytes[19], bytes[20] };

            return packet;
        }

        /* maybe won't use
        public byte[] ToArray()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.BinaryWriter(stream);

            writer.Write(this.header);
            writer.Write(this.agvID);
            writer.Write(this.status);
            writer.Write(this.exitNode);
            writer.Write(this.disToExitNode);
            writer.Write(this.orient);
            writer.Write(this.velocity);
            writer.Write(this.battery);
            writer.Write(this.checkSum);
            writer.Write(this.endOfFrame);

            return stream.ToArray();
        }
        */
    }
}
