using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	enum Instructs
	{
		ADD
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
		SP
	}
}
