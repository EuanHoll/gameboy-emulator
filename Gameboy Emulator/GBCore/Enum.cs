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
		JUMP,
		LOAD8B,
		LOAD8B16B,
		LOAD16B8B,
		BIT //https://www.reddit.com/r/EmuDev/comments/501dek/til_do_not_trust_anybody/
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
		SP,
		D8
	}

	enum JumpCodes
	{
		NOTZERO,
		ZERO,
		NOTCARRY,
		CARRY,
		ALWAYS,
		HL
	}

}
