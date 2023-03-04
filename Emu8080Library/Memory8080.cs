using System;
using System.Net;

namespace Emu8080
{
	public class Memory8080
	{
        private byte[] memory;
		private int romLimit = 0x2000;
		private int ramSize = 16 * 1024;
		private int videoRamStart = 0x2400;
        private int videoRamEnd = 0x3fff;


        public Memory8080()
		{
			memory = new byte[ramSize];
		}

		public byte ReadByteFromMemoryAt(int address)
		{
			return memory[address];
		}

		public Boolean WriteByteInRamAt(int address, byte value)
		{
			if (address < romLimit)
			{
				return false;
			}

			if (address >= ramSize)
			{
				return false;
			}

			memory[address] = value;
			return true;
		}

		public Boolean InitRomFromBuffer(int address, byte[] buffer)
		{
            int memstart = address;
            foreach (byte b in buffer)
            {
                memory[memstart++] = b;
            }
            return true;
        }

		public byte[] GetVideoRam()
		{
			byte[] ret = new byte[7 * 1024];
			int retInd = 0;
			for(int i = videoRamStart; i <= videoRamEnd; i++)
			{
                ret[retInd++] = memory[i];
            }

            return ret;
		}
    }
}

