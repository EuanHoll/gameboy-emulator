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
		public short stackpointer;

		public CPU(short progCounter)
		{
			registers = new Registers();
			this.progCounter = progCounter;
			memoryBus = new MemoryBus();
			stackpointer = 0;
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

		public short ReadStack(short address)
		{
			return ((short)(memory[address] + (memory[address + 1] << 8)));
		}

		public void WriteStack(short address, short data)
		{
			memory[address] = (byte)(data >> 8);
			memory[address - 1] = (byte)(data & 0x00FF);
		}
	}

}
