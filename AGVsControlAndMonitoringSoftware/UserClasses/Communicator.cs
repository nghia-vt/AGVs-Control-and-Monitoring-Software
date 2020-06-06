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
                AGVInfoReceivePacket frame = AGVInfoReceivePacket.FromArray(data);

                // check sum
                ushort crc = 0;
                for (int i = 0; i < AGVInfoReceivePacketSize - 4; i++)
                    crc += data[i];
                if (crc != frame.CheckSum) return;

                // update AGV info to lists of AGVs (real-time mode)
                var agv = AGV.ListAGV.Find(a => a.ID == frame.AGVID);
                if (agv == null) return;
                switch (Convert.ToChar(frame.Status).ToString())
                {
                    case "R": agv.Status = "Running"; break;
                    case "S": agv.Status = "Stop"; break;
                    case "P": agv.Status = "Picking"; break;
                    case "D": agv.Status = "Dropping"; break;
                }
                switch (Convert.ToChar(frame.Orient).ToString())
                {
                    case "E": agv.Orientation = 'E'; break;
                    case "W": agv.Orientation = 'W'; break;
                    case "S": agv.Orientation = 'S'; break;
                    case "N": agv.Orientation = 'N'; break;
                }
                agv.ExitNode = frame.ExitNode;
                agv.DistanceToExitNode = frame.DisToExitNode;
                agv.Velocity = frame.Velocity;
                agv.Battery = frame.Battery;
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
                AGVInfoReceivePacket frame = AGVInfoReceivePacket.FromArray(rxBuffer);

                // check crc ------add action for this if it wrong------------
                ushort crc = 0;
                for (int i = 0; i < AGVInfoReceivePacketSize - 4; i++)
                    crc += rxBuffer[i];
                if (crc != frame.CheckSum) return;

                // update AGV info to lists of AGVs (real-time mode)
                var agv = AGV.ListAGV.Find(a => a.ID == frame.AGVID);
                if (agv == null) return;
                switch (Convert.ToChar(frame.Status).ToString())
                {
                    case "R": agv.Status = "Running"; break;
                    case "S": agv.Status = "Stop"; break;
                    case "P": agv.Status = "Picking"; break;
                    case "D": agv.Status = "Dropping"; break;
                }
                switch (Convert.ToChar(frame.Orient).ToString())
                {
                    case "E": agv.Orientation = 'E'; break;
                    case "W": agv.Orientation = 'W'; break;
                    case "S": agv.Orientation = 'S'; break;
                    case "N": agv.Orientation = 'N'; break;
                }
                agv.ExitNode = frame.ExitNode;
                agv.DistanceToExitNode = frame.DisToExitNode;
                agv.Velocity = frame.Velocity;
                agv.Battery = frame.Battery;

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
