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
        private const ushort AckReceivePacketSize = 9;
        public static float lineTrackError; // temp value to draw graph (in AGV Monitoring Form)

        public static bool isReconnecting = false;
        private const int timeout = 300; // ms 
        private static AckReceivePacket ackReceived = new AckReceivePacket();

        public static void GetData()
        {
            int rxBufferSize = 25;
            byte[] rxBuffer = new byte[rxBufferSize];
            int rxByteCount = Communicator.SerialPort.Read(rxBuffer, 0, rxBufferSize);

            // add to a list of bytes received
            for (int i = 0; i < rxByteCount; i++) bytesReceived.Add(rxBuffer[i]);
            
            int startIndex = 0;
            byte functionCode = new byte();

            // check header
            if (bytesReceived.Count < 3) return;
            for (int i = 0; i < bytesReceived.Count - 3; i++)
            {
                if (bytesReceived[i] == 0xAA && bytesReceived[i + 1] == 0xFF)
                {
                    startIndex = i;
                    functionCode = bytesReceived[i + 2];

                    if (functionCode == (byte)FUNC_CODE.RESP_AGV_INFO) // receive AGV info except line tracking error
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
                    else if (functionCode == (byte)FUNC_CODE.RESP_LINE_TRACK_ERR) // receive Line tracking error
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
                        if (AGVMonitoringForm.selectedAGVID == receiveFrame.AGVID)
                            lineTrackError = receiveFrame.LineTrackError;
                        else lineTrackError = 0;
                    }
                    else if (functionCode == (byte)FUNC_CODE.RESP_ACK_PATH ||
                             functionCode == (byte)FUNC_CODE.RESP_ACK_AGV_INFO ||
                             functionCode == (byte)FUNC_CODE.RESP_ACK_WAITING ||
                             functionCode == (byte)FUNC_CODE.RESP_ACK_VELOCITY ||
                             functionCode == (byte)FUNC_CODE.RESP_ACK_INIT) // receive ack
                    {
                        // waitting for receive enough frame data of this function code
                        if (bytesReceived.Count - startIndex < AckReceivePacketSize) return;

                        // get data and take it out of the queue
                        byte[] data = new byte[AckReceivePacketSize];
                        for (int j = 0; j < AckReceivePacketSize; j++)
                            data[j] = bytesReceived[startIndex + j];

                        AckReceivePacket receiveFrame = AckReceivePacket.FromArray(data);

                        // check sum
                        ushort crc = 0;
                        for (int j = 0; j < AckReceivePacketSize - 4; j++)
                            crc += data[j];
                        if (crc != receiveFrame.CheckSum) continue;

                        bytesReceived.RemoveRange(0, startIndex + AckReceivePacketSize - 1);

                        // update received ack
                        ackReceived = receiveFrame;

                        // if receive ACK of Initialization, set agv.Initialization = true
                        if (functionCode == (byte)FUNC_CODE.RESP_ACK_INIT && receiveFrame.ACK == (byte)'Y')
                        {
                            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)receiveFrame.AGVID);
                            agv.IsInitialized = true;
                            agv.Status = "Initialized";
                        }

                        // Display ComStatus
                        string message = "";
                        if (receiveFrame.ACK == (byte)'Y')
                        {
                            if (functionCode == (byte)FUNC_CODE.RESP_ACK_PATH) message = "ACK (path)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_AGV_INFO) message = "ACK (request AGV info)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_WAITING) message = "ACK (waiting)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_VELOCITY) message = "ACK (velocity setting)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_INIT) message = "ACK (initialized)";
                            Display.UpdateComStatus("receive", receiveFrame.AGVID, message, System.Drawing.Color.Green);
                        }
                        else if (receiveFrame.ACK == (byte)'N')
                        {
                            if (functionCode == (byte)FUNC_CODE.RESP_ACK_PATH) message = "NACK (path)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_AGV_INFO) message = "NACK (request AGV info)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_WAITING) message = "NACK (waiting)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_VELOCITY) message = "NACK (velocity setting)";
                            else if (functionCode == (byte)FUNC_CODE.RESP_ACK_INIT) message = "NACK (initialize)";
                            Display.UpdateComStatus("receive", receiveFrame.AGVID, message, System.Drawing.Color.Red);
                        }
                    }
                }
            }

        }

        // Send AGV info request packet (InfoType = 'A' for all except line tracking error, 'L' for line tracking error)
        public static void SendAGVInfoRequest(uint agvID, char InfoType)
        {
            AGVInfoRequestPacket requestFrame = new AGVInfoRequestPacket();

            requestFrame.Header = new byte[2] { 0xAA, 0xFF };
            requestFrame.FunctionCode = (byte)FUNC_CODE.REQ_AGV_INFO;
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
            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)agvID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(requestFrame.ToArray(), 0, requestFrame.ToArray().Length); }
            catch { };

            // Display ComStatus
            Display.UpdateComStatus("send", requestFrame.AGVID, "Request AGV info", System.Drawing.Color.Blue);

            // wait ack
            System.Timers.Timer timer1 = new System.Timers.Timer(timeout);
            timer1.Elapsed += (sender, e) => timer1_Elapsed(sender, e, requestFrame);
            timer1.Start();
        }

        private static void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e, AGVInfoRequestPacket requestFrame)
        {
            AGVInfoRequestPacket resendFrame = requestFrame;

            if (ackReceived.AGVID == resendFrame.AGVID &&
                ackReceived.FunctionCode == (byte)FUNC_CODE.RESP_ACK_AGV_INFO &&
                ackReceived.ACK == (byte)'Y')
            {
                ackReceived = default(AckReceivePacket);
                ((System.Timers.Timer)sender).Dispose();
                return;
            }

            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)resendFrame.AGVID);
            if (agv == null || !agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(resendFrame.ToArray(), 0, resendFrame.ToArray().Length); }
            catch { return; }

            // Display ComStatus
            Display.UpdateComStatus("timeout", resendFrame.AGVID, "Request AGV info", System.Drawing.Color.Red);
            Display.UpdateComStatus("send", resendFrame.AGVID, "Request AGV info", System.Drawing.Color.Blue);
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
            sendFrame.FunctionCode = (byte)FUNC_CODE.WR_PATH;
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
            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)agvID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length); }
            catch { };

            // Display ComStatus
            Display.UpdateComStatus("send", sendFrame.AGVID, "Write Path", System.Drawing.Color.Blue);

            // wait ack
            System.Timers.Timer timer2 = new System.Timers.Timer(timeout);
            timer2.Elapsed += (sender, e) => timer2_Elapsed(sender, e, sendFrame);
            timer2.Start();
        }

        private static void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e, PathInfoSendPacket sendFrame)
        {
            PathInfoSendPacket resendFrame = sendFrame;

            if (ackReceived.AGVID == resendFrame.AGVID &&
                ackReceived.FunctionCode == (byte)FUNC_CODE.RESP_ACK_PATH &&
                ackReceived.ACK == (byte)'Y')
            {
                ackReceived = default(AckReceivePacket);
                ((System.Timers.Timer)sender).Dispose();
                return;
            }

            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)resendFrame.AGVID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(resendFrame.ToArray(), 0, resendFrame.ToArray().Length); }
            catch { return; }

            // Display ComStatus
            Display.UpdateComStatus("timeout", resendFrame.AGVID, "Write Path", System.Drawing.Color.Red);
            Display.UpdateComStatus("send", resendFrame.AGVID, "Write Path", System.Drawing.Color.Blue);
        }

        public static void SendInformWaiting(uint agvID, ushort waitingTime)
        {
            InformWaitingSendPacket sendFrame = new InformWaitingSendPacket();

            sendFrame.Header = new byte[2] { 0xAA, 0xFF };
            sendFrame.FunctionCode = (byte)FUNC_CODE.WR_WAITING;
            sendFrame.AGVID = Convert.ToByte(agvID);
            sendFrame.WaitingTime = waitingTime;
            // calculate check sum
            ushort crc = 0;
            crc += sendFrame.Header[0];
            crc += sendFrame.Header[1];
            crc += sendFrame.FunctionCode;
            crc += sendFrame.AGVID;
            crc += BitConverter.GetBytes(sendFrame.WaitingTime)[0];
            crc += BitConverter.GetBytes(sendFrame.WaitingTime)[1];
            sendFrame.CheckSum = crc;
            sendFrame.EndOfFrame = new byte[2] { 0x0A, 0x0D };

            // send data via serial port
            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)agvID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length); }
            catch { };

            // Display ComStatus
            Display.UpdateComStatus("send", sendFrame.AGVID, "Inform Waiting", System.Drawing.Color.Blue);

            // wait ack
            System.Timers.Timer timer3 = new System.Timers.Timer(timeout);
            timer3.Elapsed += (sender, e) => timer3_Elapsed(sender, e, sendFrame);
            timer3.Start();
        }

        private static void timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e, InformWaitingSendPacket sendFrame)
        {
            InformWaitingSendPacket resendFrame = sendFrame;

            if (ackReceived.AGVID == resendFrame.AGVID &&
                ackReceived.FunctionCode == (byte)FUNC_CODE.RESP_ACK_WAITING &&
                ackReceived.ACK == (byte)'Y')
            {
                ackReceived = default(AckReceivePacket);
                ((System.Timers.Timer)sender).Dispose();
                return;
            }

            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)resendFrame.AGVID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(resendFrame.ToArray(), 0, resendFrame.ToArray().Length); }
            catch { return; }

            // Display ComStatus
            Display.UpdateComStatus("timeout", resendFrame.AGVID, "Inform Waiting", System.Drawing.Color.Red);
            Display.UpdateComStatus("send", resendFrame.AGVID, "Inform Waiting", System.Drawing.Color.Blue);
        }

        public static void SendVelocitySetting(uint agvID, float velocity)
        {
            SetVelocityPacket sendFrame = new SetVelocityPacket();

            sendFrame.Header = new byte[2] { 0xAA, 0xFF };
            sendFrame.FunctionCode = (byte)FUNC_CODE.WR_VELOCITY;
            sendFrame.AGVID = Convert.ToByte(agvID);
            sendFrame.Velocity = velocity;
            // calculate check sum
            ushort crc = 0;
            crc += sendFrame.Header[0];
            crc += sendFrame.Header[1];
            crc += sendFrame.FunctionCode;
            crc += sendFrame.AGVID;
            crc += BitConverter.GetBytes(sendFrame.Velocity)[0];
            crc += BitConverter.GetBytes(sendFrame.Velocity)[1];
            crc += BitConverter.GetBytes(sendFrame.Velocity)[2];
            crc += BitConverter.GetBytes(sendFrame.Velocity)[3];
            sendFrame.CheckSum = crc;
            sendFrame.EndOfFrame = new byte[2] { 0x0A, 0x0D };

            // send data via serial port
            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)agvID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length); }
            catch { };

            // Display ComStatus
            Display.UpdateComStatus("send", sendFrame.AGVID, "Set Velocity", System.Drawing.Color.Blue);

            // wait ack
            System.Timers.Timer timer4 = new System.Timers.Timer(timeout);
            timer4.Elapsed += (sender, e) => timer4_Elapsed(sender, e, sendFrame);
            timer4.Start();
        }

        private static void timer4_Elapsed(object sender, System.Timers.ElapsedEventArgs e, SetVelocityPacket sendFrame)
        {
            SetVelocityPacket resendFrame = sendFrame;

            if (ackReceived.AGVID == resendFrame.AGVID &&
                ackReceived.FunctionCode == (byte)FUNC_CODE.RESP_ACK_VELOCITY &&
                ackReceived.ACK == (byte)'Y')
            {
                ackReceived = default(AckReceivePacket);
                ((System.Timers.Timer)sender).Dispose();
                return;
            }

            AGV agv = AGV.ListAGV.Find(a => a.ID == (int)resendFrame.AGVID);
            if (!agv.IsInitialized) return;
            try { Communicator.SerialPort.Write(resendFrame.ToArray(), 0, resendFrame.ToArray().Length); }
            catch { return; }

            // Display ComStatus
            Display.UpdateComStatus("timeout", resendFrame.AGVID, "Set Velocity", System.Drawing.Color.Red);
            Display.UpdateComStatus("send", resendFrame.AGVID, "Set Velocity", System.Drawing.Color.Blue);
        }

        public static void SendAGVInitRequest(uint agvID)
        {
            AGVInitPacket sendFrame = new AGVInitPacket();

            sendFrame.Header = new byte[2] { 0xAA, 0xFF };
            sendFrame.FunctionCode = (byte)FUNC_CODE.REQ_AGV_INIT;
            sendFrame.AGVID = Convert.ToByte(agvID);
            sendFrame.Initialization = (byte)'I';
            // calculate check sum
            ushort crc = 0;
            crc += sendFrame.Header[0];
            crc += sendFrame.Header[1];
            crc += sendFrame.FunctionCode;
            crc += sendFrame.AGVID;
            crc += sendFrame.Initialization;
            sendFrame.CheckSum = crc;
            sendFrame.EndOfFrame = new byte[2] { 0x0A, 0x0D };

            // send data via serial port
            try { Communicator.SerialPort.Write(sendFrame.ToArray(), 0, sendFrame.ToArray().Length); }
            catch { };

            // Display ComStatus
            Display.UpdateComStatus("send", sendFrame.AGVID, "Initialize", System.Drawing.Color.Blue);

            // wait ack
            System.Timers.Timer timer5 = new System.Timers.Timer(timeout);
            timer5.Elapsed += (sender, e) => timer5_Elapsed(sender, e, sendFrame);
            timer5.Start();
        }

        private static void timer5_Elapsed(object sender, System.Timers.ElapsedEventArgs e, AGVInitPacket sendFrame)
        {
            AGVInitPacket resendFrame = sendFrame;

            if (ackReceived.AGVID == resendFrame.AGVID &&
                ackReceived.FunctionCode == (byte)FUNC_CODE.RESP_ACK_INIT &&
                ackReceived.ACK == (byte)'Y')
            {
                ackReceived = default(AckReceivePacket);
                ((System.Timers.Timer)sender).Dispose();
                return;
            }

            var agv = AGV.ListAGV.Find(a => a.ID == (int)resendFrame.AGVID);
            if (agv == null) return;
            try { Communicator.SerialPort.Write(resendFrame.ToArray(), 0, resendFrame.ToArray().Length); }
            catch { return; }

            // Display ComStatus
            Display.UpdateComStatus("timeout", resendFrame.AGVID, "Initialize", System.Drawing.Color.Red);
            Display.UpdateComStatus("send", resendFrame.AGVID, "Initialize", System.Drawing.Color.Blue);
        }
    }

    #region Receive and Send Packet Structure

    /* AGV Initialization request packet (send):
     * Header		    2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0xA4
     * AGVID 		    1 byte
     * Initialization   1 byte
     * CheckSum	        2 byte  -> sum of bytes from Header to Initialization
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct AGVInitPacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte Initialization;
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
            writer.Write(this.Initialization);
            writer.Write(this.CheckSum);
            writer.Write(this.EndOfFrame);

            return stream.ToArray();
        }
    }

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

    /* Ack response packet (receive):
     * Header           2 byte  -> 0xFFAA
     * FunctionCode	    1 byte
     * AGVID 		    1 byte		
     * ACK              1 byte  -> 'Y' or 'N'		
     * CheckSum	        2 byte  -> sum of bytes from Header to ACK
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct AckReceivePacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public byte ACK;
        public ushort CheckSum;
        public byte[] EndOfFrame;

        // Convert Byte Arrays to Structs
        public static AckReceivePacket FromArray(byte[] bytes)
        {
            var reader = new System.IO.BinaryReader(new System.IO.MemoryStream(bytes));

            var s = default(AckReceivePacket);

            s.Header = reader.ReadBytes(2);
            s.FunctionCode = reader.ReadByte();
            s.AGVID = reader.ReadByte();
            s.ACK = reader.ReadByte();
            s.CheckSum = reader.ReadUInt16();
            s.EndOfFrame = reader.ReadBytes(2);

            return s;
        }
    }

    /* Inform waiting packet (send):
     * Header           2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0xA2
     * AGVID 		    1 byte		
     * WaitingTime      2 byte		
     * CheckSum	        2 byte  -> sum of bytes from Header to WaitingTime
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct InformWaitingSendPacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public ushort WaitingTime;
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
            writer.Write(this.WaitingTime);
            writer.Write(this.CheckSum);
            writer.Write(this.EndOfFrame);

            return stream.ToArray();
        }
    }

    /* Write velocity packet (send):
     * Header		    2 byte  -> 0xFFAA
     * FunctionCode	    1 byte  -> 0xA3
     * AGVID 		    1 byte
     * Velocity         4 byte
     * CheckSum	        2 byte  -> sum of bytes from Header to Velocity
     * EndOfFrame	    2 byte  -> 0x0D0A
     */
    public struct SetVelocityPacket
    {
        public byte[] Header;
        public byte FunctionCode;
        public byte AGVID;
        public float Velocity;
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
            writer.Write(this.Velocity);
            writer.Write(this.CheckSum);
            writer.Write(this.EndOfFrame);

            return stream.ToArray();
        }
    }

    #endregion

    public enum FUNC_CODE
    {
        REQ_AGV_INIT = 0xA4,
        RESP_ACK_INIT = 0x05,
        REQ_AGV_INFO = 0xA0,
        RESP_AGV_INFO = 0x01,
        RESP_LINE_TRACK_ERR = 0x11,
        RESP_ACK_AGV_INFO = 0x21,
        WR_PATH = 0xA1,
        RESP_ACK_PATH = 0x02,
        WR_WAITING = 0xA2,
        RESP_ACK_WAITING = 0x03,
        WR_VELOCITY = 0xA3,
        RESP_ACK_VELOCITY = 0x04
    }
}
