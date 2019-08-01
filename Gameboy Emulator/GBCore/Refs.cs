using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	class Refs
	{
		public const ushort VRAM_START = 0x8000;
		public const ushort VRAM_END = 0x9FFF;
		public const ushort VRAM_SIZE = (VRAM_END - VRAM_START) + 1;
	}

	enum Instructs
	{
		ADD,
		LOAD8B16B,
		LOAD8B,
		JUMP,
		LOAD16B8B,
		PUSH,
		POP,
		CALL,
		RET,
		NOP,
		HALT,
		INC,
		DEC,
		RST
	}

	enum Target
	{
		A,
		B,
		C,
		D,
		E,
		F,
		H,
		L,
		HL,
		BC,
		DE,
		AF,
		SP
	}

	enum JumpCodes
	{
		ALWAYS,
		HL,
		CARRY,
		ZERO,
		NOTZERO,
		NOTCARRY
	}

	enum Colour
	{
		WHITE,
		BLACK,
		LIGHT_GREY,
		DARK_GREY
	}

	enum RSTTYPE
	{
		H00,
		H08,
		H10,
		H18,
		H20,
		H28,
		H30,
		H38
	}
}
