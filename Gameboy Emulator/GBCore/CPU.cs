using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{

	struct CPU
	{
		public Registers registers;
		public short progCounter;
		public MemoryBus memoryBus;

		public CPU(short progCounter)
		{
			registers = new Registers();
			this.progCounter = progCounter;
			memoryBus = new MemoryBus();
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	unsafe struct MemoryBus
	{
		[FieldOffset(0)]
		public fixed byte memory[0xFFFF];

		public void SetByte(short address, byte data)
		{
			memory[address] = data;
		}

		public byte ReadByte(short address)
		{
			return (memory[address]);
		}

		public short ReadShort(short address)
		{
			return ((short)((memory[address] << 8) + memory[address + 1]));
		}
	}

}
