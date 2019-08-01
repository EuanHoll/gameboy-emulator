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

		public static unsafe void POP(CPU* cpu, Target target)
		{
			switch(target)
			{
				case Target.AF:
					cpu->registers.SetAF(cpu->memoryBus.ReadStack(cpu->stackpointer));
					break;
				case Target.BC:
					cpu->registers.SetBC(cpu->memoryBus.ReadStack(cpu->stackpointer));
					break;
				case Target.DE:
					cpu->registers.SetDE(cpu->memoryBus.ReadStack(cpu->stackpointer));
					break;
				case Target.HL:
					cpu->registers.SetHL(cpu->memoryBus.ReadStack(cpu->stackpointer));
					break;
			}
			cpu->stackpointer += 2;
			cpu->progCounter++;
		}

		public static unsafe void PUSH(CPU* cpu, Target target)
		{
			cpu->stackpointer--;
			switch(target)
			{
				case Target.BC:
					cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->registers.GetBC());
					break;
				case Target.DE:
					cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->registers.GetDE());
					break;
				case Target.HL:
					cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->registers.GetHL());
					break;
				case Target.AF:
					cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->registers.GetAF());
					break;
			}
			cpu->stackpointer--;
			cpu->progCounter++;
		}

		public static unsafe void CALL(CPU* cpu, JumpCodes jumpcode)
		{
			bool jump = false;
			switch (jumpcode)
			{
				case JumpCodes.ALWAYS:
					jump = true;
					break;
				case JumpCodes.CARRY:
					if (cpu->registers.GetFlagCarry())
						jump = true;
					break;
				case JumpCodes.ZERO:
					if (cpu->registers.GetFlagZero())
						jump = true;
					break;
				case JumpCodes.NOTZERO:
					if (!cpu->registers.GetFlagZero())
						jump = true;
					break;
				case JumpCodes.NOTCARRY:
					if (!cpu->registers.GetFlagCarry())
						jump = true;
					break;
			}
			cpu->progCounter += 3;
			if (!jump)
				return;
			cpu->stackpointer--;
			cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->progCounter);
			cpu->stackpointer--;
			cpu->progCounter = cpu->memoryBus.ReadShort((short)(cpu->progCounter - 2));
		}

		public static unsafe void RET(CPU *cpu, JumpCodes jumpcode)
		{
			bool jump = false;
			switch (jumpcode)
			{
				case JumpCodes.ALWAYS:
					jump = true;
					break;
				case JumpCodes.CARRY:
					if (cpu->registers.GetFlagCarry())
						jump = true;
					break;
				case JumpCodes.ZERO:
					if (cpu->registers.GetFlagZero())
						jump = true;
					break;
				case JumpCodes.NOTZERO:
					if (!cpu->registers.GetFlagZero())
						jump = true;
					break;
				case JumpCodes.NOTCARRY:
					if (!cpu->registers.GetFlagCarry())
						jump = true;
					break;
			}
			if (!jump)
			{
				cpu->progCounter++;
				return;
			}
			cpu->progCounter = cpu->memoryBus.ReadStack(cpu->stackpointer);
			cpu->stackpointer += 2;
		}

		public static unsafe void NOP(CPU* cpu)
		{
			cpu->progCounter++;
		}

		public static unsafe void HALT(CPU* cpu)
		{
			GBCPU.is_halted = true;
			cpu->progCounter++;
		}
	}
}
