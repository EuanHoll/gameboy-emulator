using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	class Instructions
	{

		public static unsafe void ADD(Registers* regs, byte* targ)
		{
			byte val = *targ;
			if ((((val & 0xf) + (regs->a & 0xf)) & 0x10) == 0x10)
				regs->SetFlagHalfCarry(true);
			else
				regs->SetFlagHalfCarry(false);
			if ((((val & 0x80) + (regs->a & 0x80)) & 0x80) == 0x00)
				regs->SetFlagCarry(true);
			else
				regs->SetFlagCarry(false);
			regs->a += val;
		}

	}
}
