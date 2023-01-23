using System;
namespace Emu8080
{
	public class Memory8080
	{
        private byte[] memory;
		private int romLimit = 0x2000;
		private int ramSize = 16 * 1024;

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


    }
}

