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
		public GPU gpu;
		public MemoryBus memoryBus;
		public ushort progCounter;
		public ushort stackpointer;

		public CPU(ushort progCounter)
		{
			registers = new Registers();
			this.progCounter = progCounter;
			memoryBus = new MemoryBus();
			stackpointer = 0;
			gpu = new GPU();
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	unsafe struct MemoryBus
	{
		[FieldOffset(0)]
		public fixed byte memory[0xFFFF];

		public void SetByte(ushort address, byte data, CPU* cpu)
		{
			if (address >= Refs.VRAM_START && address < Refs.VRAM_END)
				cpu->gpu.WriteMemory(address, data);
			memory[address] = data;
		}

		public byte ReadByte(ushort address, CPU* cpu)
		{
			if (address >= Refs.VRAM_START && address < Refs.VRAM_END)
				return (cpu->gpu.ReadMemory(address));
			return (memory[address]);
		}

		public ushort ReadShort(ushort address, CPU* cpu)
		{
			if (address >= Refs.VRAM_START && address < Refs.VRAM_END)
				return (cpu->gpu.ReadMemoryS(address));
			return ((ushort)(memory[address] + (memory[address + 1] << 8)));
		}

		public ushort ReadStack(ushort address)
		{
			return ((ushort)(memory[address] + (memory[address + 1] << 8)));
		}

		public void WriteStack(ushort address, ushort data)
		{
			memory[address] = (byte)((data & 0xFF00) >> 8);
			memory[address + 1] = (byte)(data & 0x00FF);
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	unsafe struct GPU
	{
		[FieldOffset(0)]
		public fixed byte memory[Refs.VRAM_SIZE];
		[FieldOffset(1)]
		public fixed int tile_map[512];

		public byte ReadMemory(ushort address)
		{
			address -= Refs.VRAM_START;
			return (memory[address]);
		}

		public ushort ReadMemoryS(ushort address)
		{
			address -= Refs.VRAM_START;
			return ((ushort)(memory[address] + (memory[address + 1] << 8)));
		}

		public void WriteMemory(ushort address, byte data)
		{
			address -= Refs.VRAM_START;
			memory[address] = data;
			if (address == 0x1800)
				return;
			ushort n_in = (ushort)(address & 0xFFFE);

			byte b1 = memory[n_in];
			byte b2 = memory[n_in + 1];

			ushort x_in = (ushort)(n_in / 16);
			ushort y_in = (ushort)((n_in % 16) / 2);
			for (byte i = 0; i < 8; i++)
			{
				byte mask = (byte)(1 << (7 - i));
				byte leastsigbyte = (byte)(b1 & mask);
				byte mostsigbyte = (byte)(b2 & mask);
				Colour colour = Colour.BLACK;
				if (leastsigbyte == 1 && mostsigbyte == 1)
					colour = Colour.WHITE;
				else if (leastsigbyte == 0 && mostsigbyte == 0)
					colour = Colour.BLACK;
				else if (leastsigbyte == 1 && mostsigbyte == 0)
					colour = Colour.DARK_GREY;
				else if (leastsigbyte == 0 && mostsigbyte == 1)
					colour = Colour.LIGHT_GREY;
				tile_map[x_in + 8 * (y_in + 8 * i)] = (int)colour;
			}
		}
	}
}
