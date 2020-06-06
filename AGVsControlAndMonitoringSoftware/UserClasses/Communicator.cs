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

        private const ushort AGVInfoReceivePacketSize = 21;
        public static Queue<byte> queueRXData = new Queue<byte>();

        // Get desired data, put in struct packet, and take appropriate action
        /* (Method 1) Use Queue, so can save data after handling this event
         * Note: in case receive agv info, if we receive $77$AA$FF$01$01 $AA$FF$01$02$53....$AF$05$0A$0D (for example)
         * we will lose the $AA$FF$01$02$53$...$AF$05$0A$0D (desired information)
         */
        public static void GetDataUseQueue()
        {
            int rxBufferSize = Communicator.SerialPort.BytesToRead;
            byte[] rxBuffer = new byte[rxBufferSize];
            Communicator.SerialPort.Read(rxBuffer, 0, rxBufferSize);

            // put all received data into a queue, and a array of queue
            rxBuffer.ToList().ForEach(b => Communicator.queueRXData.Enqueue(b));
            byte[] arrQueueRXData = Communicator.queueRXData.ToArray();

            int startIndex = 0;
            byte functionCode = new byte();

            // waitting for 3 bytes to check header and get function code
            if (Communicator.queueRXData.Count < 3) return;

            // check header and get function code if header is detected
            for (int i = 0; i < arrQueueRXData.Length - 3; i++)
            {
                if (arrQueueRXData[i] == 0xAA && arrQueueRXData[i + 1] == 0xFF)
                {
                    startIndex = i;
                    functionCode = arrQueueRXData[i + 2];
                    break;
                }
            }

            if (functionCode == 0x01) // receive agv info
            {
                // waitting for receive enough frame data of this function code
                if (arrQueueRXData.Length - startIndex < AGVInfoReceivePacketSize) return;

                // get data and take it out of the queue
                byte[] data = new byte[AGVInfoReceivePacketSize];
                for (int i = 0; i < AGVInfoReceivePacketSize; i++)
                    data[i] = arrQueueRXData[startIndex + i];
                for (int i = 0; i < AGVInfoReceivePacketSize + startIndex; i++)
                    Communicator.queueRXData.Dequeue();

                // put data in a struct data of this function code
                AGVInfoReceivePacket receiveFrame = AGVInfoReceivePacket.FromArray(data);

                // check sum
                ushort crc = 0;
                for (int i = 0; i < AGVInfoReceivePacketSize - 4; i++)
                    crc += data[i];
                if (crc != receiveFrame.CheckSum) return;

                // update AGV info to lists of AGVs (real-time mode)
                var agv = AGV.ListAGV.Find(a => a.ID == receiveFrame.AGVID);
                if (agv == null) return;
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
        }

        /* (Method 2) Not use Queue
         * So data will be discarded after finish handle event
         */
        public static void GetData()
        {
            int rxBufferSize = 25;
            byte[] rxBuffer = new byte[rxBufferSize];
            int rxBufferCount = Communicator.SerialPort.Read(rxBuffer, 0, rxBufferSize); //return the number of bytes read
            // Console.WriteLine(BitConverter.ToString(rxBuffer));

            // check header
            if (rxBuffer[0] != 0xAA && rxBuffer[1] != 0xFF) return;

            if (rxBuffer[2] == 0x01) // function code
            {
                AGVInfoReceivePacket receiveFrame = AGVInfoReceivePacket.FromArray(rxBuffer);

                // check crc ------add action for this if it wrong------------
                ushort crc = 0;
                for (int i = 0; i < AGVInfoReceivePacketSize - 4; i++)
                    crc += rxBuffer[i];
                if (crc != receiveFrame.CheckSum) return;

                // update AGV info to lists of AGVs (real-time mode)
                var agv = AGV.ListAGV.Find(a => a.ID == receiveFrame.AGVID);
                if (agv == null) return;
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

                //----------for testing-------------------
                //Array.ForEach(frame.Header, b => { Console.Write(b); Console.WriteLine(); });
                //Console.WriteLine(frame.AGVID);
                //Console.WriteLine(frame.Status);
                //Console.WriteLine(frame.ExitNode);
                //Console.WriteLine(frame.DisToExitNode);
                //Console.WriteLine(frame.Orient);
                //Console.WriteLine(frame.Velocity);
                //Console.WriteLine(frame.Battery);
                //Console.WriteLine(frame.CheckSum);
                //Array.ForEach(frame.EndOfFrame, b => { Console.Write(b); Console.WriteLine(); });
                //-------------------------------------------
            }
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
            Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length);
        }
    }

    #region Receive and Send Packet Structure

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

    /* Path information packet
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

    #endregion
}
