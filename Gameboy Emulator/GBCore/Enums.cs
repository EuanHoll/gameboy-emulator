using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
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
		HALT
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
}
