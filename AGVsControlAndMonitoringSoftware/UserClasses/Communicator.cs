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
        private static SerialPort _serialPort = new SerialPort();
        public static SerialPort SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        private static List<byte> bytesReceived = new List<byte>();
        private const ushort AGVInfoReceivePacketSize = 21;
        private const ushort AGVLineTrackErrorReceivePacketSize = 12;
        public static float lineTrackError; // temp value to draw graph (in AGV Monitoring Form)

        public static void GetData()
        {
            byte[] rxOneByte = new byte[1];
            Communicator.SerialPort.Read(rxOneByte, 0, 1); //return the number of bytes read
            bytesReceived.Add(rxOneByte[0]);

            int startIndex = 0;
            byte functionCode = new byte();

            if (bytesReceived.Count < 3) return;
            for (int i = 0; i < bytesReceived.Count - 3; i++)
            {
                if (bytesReceived[i] == 0xAA && bytesReceived[i + 1] == 0xFF)
                {
                    startIndex = i;
                    functionCode = bytesReceived[i + 2];

                    if (functionCode == 0x01) // receive AGV info except line tracking error
                    {
                        // waitting for receive enough frame data of this function code
                        if (bytesReceived.Count - startIndex < AGVInfoReceivePacketSize) return;

                        // put data in an array
                        byte[] data = new byte[AGVInfoReceivePacketSize];
                        for (int j = 0; j < AGVInfoReceivePacketSize; j++)
                            data[j] = bytesReceived[startIndex + j];

                        AGVInfoReceivePacket receiveFrame = AGVInfoReceivePacket.FromArray(data);

                        // check sum
                        ushort crc = 0;
                        for (int j = 0; j < AGVInfoReceivePacketSize - 4; j++)
                            crc += data[j];
                        if (crc != receiveFrame.CheckSum) continue;

                        bytesReceived.RemoveRange(0, startIndex + AGVInfoReceivePacketSize - 1);

                        // update AGV info to lists of AGVs (real-time mode)
                        var agv = AGV.ListAGV.Find(a => a.ID == receiveFrame.AGVID);
                        if (agv == null) continue;
                        switch (Convert.ToChar(receiveFrame.Status).ToString())
                        {
                            case "R": agv.Status = "Running"; break;
                            case "S": agv.Status = "Stop"; break;
                            case "P": agv.Status = "Picking"; break;
                            case "D": agv.Status = "Dropping"; break;
                        }
                        switch (Convert.ToChar(receiveFrame.Orient).ToString())
                        {
                            case "E": agv.Orientation = 'E'; break;
                            case "W": agv.Orientation = 'W'; break;
                            case "S": agv.Orientation = 'S'; break;
                            case "N": agv.Orientation = 'N'; break;
                        }
                        agv.ExitNode = receiveFrame.ExitNode;
                        agv.DistanceToExitNode = receiveFrame.DisToExitNode;
                        agv.Velocity = receiveFrame.Velocity;
                        agv.Battery = receiveFrame.Battery;
                    }
                    else if (functionCode == 0x11) // receive Line tracking error
                    {
                        // waitting for receive enough frame data of this function code
                        if (bytesReceived.Count - startIndex < AGVLineTrackErrorReceivePacketSize) return;

                        // get data and take it out of the queue
                        byte[] data = new byte[AGVLineTrackErrorReceivePacketSize];
                        for (int j = 0; j < AGVLineTrackErrorReceivePacketSize; j++)
                            data[j] = bytesReceived[startIndex + j];

                        AGVLineTrackErrorReceivePacket receiveFrame = AGVLineTrackErrorReceivePacket.FromArray(data);

                        // check sum
                        ushort crc = 0;
                        for (int j = 0; j < AGVLineTrackErrorReceivePacketSize - 4; j++)
                            crc += data[j];
                        if (crc != receiveFrame.CheckSum) continue;

                        bytesReceived.RemoveRange(0, startIndex + AGVLineTrackErrorReceivePacketSize - 1);

                        // update Line tracking error value
                        lineTrackError = receiveFrame.LineTrackError;
                    }
                }
            }

        }

        // Send AGV info request packet (InfoType = 'A' for all except line tracking error, 'L' for line tracking error)
        public static void SendAGVInfoRequest(uint agvID, char InfoType)
        {
            AGVInfoRequestPacket requestFrame = new AGVInfoRequestPacket();

            requestFrame.Header = new byte[2] { 0xAA, 0xFF };
            requestFrame.FunctionCode = 0xA0;
            requestFrame.AGVID = Convert.ToByte(agvID);
            requestFrame.InformationType = (byte)InfoType;
            // calculate check sum
            ushort crc = 0;
            crc += requestFrame.Header[0];
            crc += requestFrame.Header[1];
            crc += requestFrame.FunctionCode;
            crc += requestFrame.AGVID;
            crc += requestFrame.InformationType;
            requestFrame.CheckSum = crc;
            requestFrame.EndOfFrame = new byte[2] { 0x0A, 0x0D };

            // send data via serial port
            if (!Communicator.SerialPort.IsOpen) return;
            Communicator.SerialPort.Write(requestFrame.ToArray(), 0, requestFrame.ToArray().Length);
        }

        // Send path information packet
        public static void SendPathData(uint agvID, bool isPick, int pickLevel, string strNavigationFrame, bool isDrop, int dropLevel)
        {
            PathInfoSendPacket sendFrame = new PathInfoSendPacket();

            // get path info (to byte array)
            string[] arrNavigationFrame = strNavigationFrame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] arrPathFrame = new byte[arrNavigationFrame.Length + 4]; // 4 bytes of pick/drop info

            if (isPick == true) arrPathFrame[0] = (byte)'P';
            else arrPathFrame[0] = (byte)'N';
            arrPathFrame[1] = Convert.ToByte(pickLevel);

            if (isDrop == true) arrPathFrame[arrNavigationFrame.Length + 2] = (byte)'D';
            else arrPathFrame[arrNavigationFrame.Length + 2] = (byte)'N';
            arrPathFrame[arrNavigationFrame.Length + 3] = Convert.ToByte(dropLevel);

            for (int i = 0; i < arrNavigationFrame.Length; i++)
            {
                if (i % 2 == 0) arrPathFrame[i + 2] = (byte)(arrNavigationFrame[i][0]);
                else arrPathFrame[i + 2] = Convert.ToByte(arrNavigationFrame[i]);
            }

            // set frame to send as struct
            // note: send reversed Header and End-of-frame because of Intel processors (in my laptop) use little endian
            sendFrame.Header = new byte[2] { 0xAA, 0xFF };
            sendFrame.FunctionCode = 0xA1;
            sendFrame.AGVID = Convert.ToByte(agvID);
            sendFrame.PathByteCount = Convert.ToByte(arrPathFrame.Length);
            sendFrame.Path = arrPathFrame;
            // calculate check sum
            ushort crc = 0;
            crc += sendFrame.Header[0];
            crc += sendFrame.Header[1];
            crc += sendFrame.FunctionCode;
            crc += sendFrame.AGVID;
            crc += sendFrame.PathByteCount;
            Array.ForEach(arrPathFrame, x => crc += x);
            sendFrame.CheckSum = crc;
            sendFrame.EndOfFrame = new byte[2] { 0x0A, 0x0D };

            // send data via serial port
            if (!Communicator.SerialPort.IsOpen) return;
            Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length);
        }
    }

    #region Receive and Send Packet Structure

    /* AGV information request packet (send):
     * Header		    2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0xA0
     * AGVID 		    1 byte
     * Information type 1 byte  -> 'A' for all except Line tracking error, 'L' for Line tracking error
     * CheckSum	        2 byte  -> sum of bytes from Header to Information type
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct AGVInfoRequestPacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte InformationType;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Convert Structs to Byte Arrays
        public byte[] ToArray()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.BinaryWriter(stream);

            writer.Write(this.Header);
            writer.Write(this.FunctionCode);
            writer.Write(this.AGVID);
            writer.Write(this.InformationType);
            writer.Write(this.CheckSum);
            writer.Write(this.EndOfFrame);

            return stream.ToArray();
        }
    }

    /* AGV information packet (receive):
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

        // Convert Byte Arrays to Structs (method 1)
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

        // Convert Byte Arrays to Structs (method 2)
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
    }

    /* AGV line tracking error packet (receive):
     * Header		        2 byte  -> 0xFFAA
     * FunctionCode	        1 byte  -> 0x11
     * AGVID 		        1 byte		
     * Line tracking error  4 byte		
     * CheckSum	            2 byte  -> sum of bytes from Header to Line tracking error
     * EndOfFrame	        2 byte  -> 0x0D0A
     */
    public struct AGVLineTrackErrorReceivePacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public float LineTrackError;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Convert Byte Arrays to Structs
        public static AGVLineTrackErrorReceivePacket FromArray(byte[] bytes)
        {
            var reader = new System.IO.BinaryReader(new System.IO.MemoryStream(bytes));

            var s = default(AGVLineTrackErrorReceivePacket);

            s.Header = reader.ReadBytes(2);
            s.FunctionCode = reader.ReadByte();
            s.AGVID = reader.ReadByte();
            s.LineTrackError = reader.ReadSingle();
            s.CheckSum = reader.ReadUInt16();
            s.EndOfFrame = reader.ReadBytes(2);

            return s;
        }
    }

    /* Path information packet (send):
     * Header		    2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0xA1
     * AGVID 		    1 byte
     * Path Byte Count  1 byte
     * Path             (dynamic)
     * CheckSum	        2 byte  -> sum of bytes from Header to Path
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct PathInfoSendPacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte PathByteCount;
        public byte[] Path;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Convert Structs to Byte Arrays
        public byte[] ToArray()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.BinaryWriter(stream);

            writer.Write(this.Header);
            writer.Write(this.FunctionCode);
            writer.Write(this.AGVID);
            writer.Write(this.PathByteCount);
            writer.Write(this.Path);
            writer.Write(this.CheckSum);
            writer.Write(this.EndOfFrame);

            return stream.ToArray();
        }
    }

    /* Path information response packet (receive):
     * Header           2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0x02
     * AGVID 		    1 byte		
     * ACK              1 byte  -> 'Y' or 'N'		
     * CheckSum	        2 byte  -> sum of bytes from Header to ACK
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct PathInfoACKReceivePacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte ACK;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Convert Byte Arrays to Structs
        public static PathInfoACKReceivePacket FromArray(byte[] bytes)
        {
            var reader = new System.IO.BinaryReader(new System.IO.MemoryStream(bytes));

            var s = default(PathInfoACKReceivePacket);

            s.Header = reader.ReadBytes(2);
            s.FunctionCode = reader.ReadByte();
            s.AGVID = reader.ReadByte();
            s.ACK = reader.ReadByte();
            s.CheckSum = reader.ReadUInt16();
            s.EndOfFrame = reader.ReadBytes(2);

            return s;
        }
    }

    #endregion
}
