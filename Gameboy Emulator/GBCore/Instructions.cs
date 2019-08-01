using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	class Instructions
	{

		public static unsafe void ADD(Registers* regs, byte* targ, CPU* cpu)
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
			cpu->progCounter++;
		}

		public static unsafe void JUMP(Registers* regs, CPU* cpu, JumpCodes type)
		{
			switch(type)
			{
				case JumpCodes.ALWAYS:
					cpu->progCounter += 3;
					break;
				case JumpCodes.HL:
					cpu->progCounter = regs->GetHL();
					break;
				case JumpCodes.CARRY:
					if (regs->GetFlagCarry())
						cpu->progCounter = cpu->memoryBus.ReadShort((short)(cpu->progCounter + 1));
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.ZERO:
					if (regs->GetFlagZero())
						cpu->progCounter = cpu->memoryBus.ReadShort((short)(cpu->progCounter + 1));
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.NOTZERO:
					if (!regs->GetFlagZero())
						cpu->progCounter = cpu->memoryBus.ReadShort((short)(cpu->progCounter + 1));
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.NOTCARRY:
					if (!regs->GetFlagCarry())
						cpu->progCounter = cpu->memoryBus.ReadShort((short)(cpu->progCounter + 1));
					else
						cpu->progCounter += 3;
					break;
			}
		}

		public static unsafe void LOAD8B(CPU* cpu, byte* into, byte* from)
		{
			(*into) = (*from);
			cpu->progCounter++;
		}
		
		public static unsafe void LOAD8B16B(CPU* cpu, byte*into, short address)
		{
			*into = cpu->memoryBus.ReadByte(address);
			cpu->progCounter++;
		}

		public static unsafe void LOAD16B8B(CPU* cpu, short address, byte *from)
		{
			cpu->memoryBus.SetByte(address, *from);
			cpu->progCounter++;
		}
	}
}
