using ITS.Snap7.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITSOPCCourseCode.Snap7.Example.Services
{
    public class PlcCommunication
    {
        private const int WAIT_AS_COMPLETION_DELAY = 1000; // in milliseconds
        internal enum WordLenght : int
        {
            Bit = 0x01,
            Byte = 0x02,
            Word = 0x04,
            DWord = 0x06,
            Real = 0x08,
            Counter = 0x1C,
            Timer = 0x1D
        }
        private string _plcAddress { get; }
        private S7Client _client { get; }

        public PlcCommunication(string plcAddress)
        {
            _plcAddress = plcAddress;
            _client = new S7Client();
        }

        public async Task<bool> ReadBitAsync(int dbNumber, uint start, ushort bitOffset) =>
            new BitArray(await ReadAsync(dbNumber, start, 1).ConfigureAwait(false)).Get(bitOffset);

        public async Task<bool> WriteBitAsync(bool data, int dbNumber, uint start, ushort bitOffset)
        { 
            ConnectIfNotConnected();

            var buffer = new byte[1];
            var bitArray = new BitArray(new bool[] { data });
            bitArray.CopyTo(buffer, 0);

            var writeResult = _client.AsWriteArea(S7Client.S7AreaDB, dbNumber, (int)start * 8 + bitOffset, 1, (int)WordLenght.Bit, buffer);
            if (writeResult != 0)
            {
                throw new Exception($"An error occurred while writing bit to PLC {_plcAddress}. Code: {writeResult}");
            }
            var waitResult = _client.WaitAsCompletion(WAIT_AS_COMPLETION_DELAY);
            if (waitResult != 0)
            {
                throw new Exception($"An error occurred while waiting bit write to PLC {_plcAddress}. Code: {waitResult}");
            }
            return true;
        }

        public async Task<byte[]> ReadAsync(int dbNumber, uint start, uint amount)
        {
            ConnectIfNotConnected();

            // Prepare the buffers and instruct the client to read.
            var buffer = new byte[amount];
            var readResult = _client.AsReadArea(S7Client.S7AreaDB, dbNumber, (int)start, (int)amount, (int)WordLenght.Byte, buffer);
            if (readResult != 0)
            {
                throw new Exception($"An error occurred while reading area from PLC {_plcAddress}. Code: {readResult}");
            }
            var waitResult = _client.WaitAsCompletion(WAIT_AS_COMPLETION_DELAY);
            if (waitResult != 0)
            {
                throw new Exception($"An error occurred while waiting area read from PLC {_plcAddress}. Code: {waitResult}");
            }

            return buffer;
        }

        public async Task<bool> WriteAsync(byte[] data, int dbNumber, uint start, uint amount)
        {
            ConnectIfNotConnected();

            var writeResult = _client.AsWriteArea(S7Client.S7AreaDB, dbNumber, (int)start, (int)amount, (int)WordLenght.Byte, data);
            if (writeResult != 0)
            {
                throw new Exception($"An error occurred while writing area to PLC {_plcAddress}. Code: {writeResult}");
            }
            var waitResult = _client.WaitAsCompletion(WAIT_AS_COMPLETION_DELAY);
            if (waitResult != 0)
            {
                throw new Exception($"An error occurred while waiting area write to PLC {_plcAddress}. Code: {waitResult}");
            }

            return true;
        }

        private void ConnectIfNotConnected()
        {
            var connectionResult = _client.ConnectTo(_plcAddress, 0, 1);
            if (connectionResult != 0)
            {
                throw new Exception($"Connection to plc {_plcAddress} failed: [{connectionResult}]");
            }
            return;
        }
    }
}
