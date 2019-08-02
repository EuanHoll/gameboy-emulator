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
			if (targ == null)
				*targ = cpu->memoryBus.ReadByte(cpu->registers.GetHL(), cpu);
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
			switch (type)
			{
				case JumpCodes.ALWAYS:
					cpu->progCounter += 3;
					break;
				case JumpCodes.HL:
					cpu->progCounter = regs->GetHL();
					break;
				case JumpCodes.CARRY:
					if (regs->GetFlagCarry())
						cpu->progCounter = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter + 1), cpu);
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.ZERO:
					if (regs->GetFlagZero())
						cpu->progCounter = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter + 1), cpu);
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.NOTZERO:
					if (!regs->GetFlagZero())
						cpu->progCounter = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter + 1), cpu);
					else
						cpu->progCounter += 3;
					break;
				case JumpCodes.NOTCARRY:
					if (!regs->GetFlagCarry())
						cpu->progCounter = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter + 1), cpu);
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

		public static unsafe void LOAD8B16B(CPU* cpu, byte* into, ushort address)
		{
			*into = cpu->memoryBus.ReadByte(address, cpu);
			cpu->progCounter++;
		}

		public static unsafe void LOAD16B8B(CPU* cpu, ushort address, byte* from)
		{
			cpu->memoryBus.SetByte(address, *from, cpu);
			cpu->progCounter++;
		}

		public static unsafe void POP(CPU* cpu, Target target)
		{
			switch (target)
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
			switch (target)
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
			cpu->progCounter = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter - 2), cpu);
		}

		public static unsafe void RET(CPU* cpu, JumpCodes jumpcode)
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

		public static unsafe void INC(CPU* cpu, byte* val)
		{
			if (val == null)
				*val = cpu->memoryBus.ReadByte(cpu->registers.GetHL(), cpu);
			if ((((1 & 0xf) + ((*val) & 0xf)) & 0x10) == 0x10)
				cpu->registers.SetFlagHalfCarry(true);
			(*val)++;
			if ((*val) == 0)
				cpu->registers.SetFlagZero(true);
			cpu->progCounter++;
		}

		public static unsafe void DEC(CPU* cpu, byte* val)
		{
			if (val == null)
				*val = cpu->memoryBus.ReadByte(cpu->registers.GetHL(), cpu);
			if ((((*val) & 0xf) - ((1 & 0xf)) & 0x10) == 0x10)
				cpu->registers.SetFlagHalfCarry(true);
			cpu->registers.SetFlagSub(true);
			(*val)--;
			if ((*val) == 0)
				cpu->registers.SetFlagZero(true);
			cpu->progCounter++;
		}

		public static unsafe void DEC16(CPU* cpu, Target targ)
		{
			switch (targ)
			{
				case Target.SP:
					cpu->stackpointer--;
					break;
				case Target.HL:
					cpu->registers.SetHL((ushort)(cpu->registers.GetHL() - 1));
					break;
				case Target.DE:
					cpu->registers.SetDE((ushort)(cpu->registers.GetDE() - 1));
					break;
				case Target.BC:
					cpu->registers.SetBC((ushort)(cpu->registers.GetBC() - 1));
					break;
			}
			cpu->progCounter++;
		}

		public static unsafe void RST(CPU* cpu, RSTTYPE rst)
		{
			cpu->memoryBus.WriteStack(cpu->stackpointer, cpu->progCounter);
			cpu->stackpointer += 2;
			switch (rst)
			{
				case RSTTYPE.H00:
					cpu->progCounter++;
					break;
				case RSTTYPE.H08:
					cpu->progCounter = 0x08;
					break;
				case RSTTYPE.H10:
					cpu->progCounter = 0x10;
					break;
				case RSTTYPE.H18:
					cpu->progCounter = 0x18;
					break;
				case RSTTYPE.H20:
					cpu->progCounter = 0x20;
					break;
				case RSTTYPE.H28:
					cpu->progCounter = 0x28;
					break;
				case RSTTYPE.H30:
					cpu->progCounter = 0x30;
					break;
				case RSTTYPE.H38:
					cpu->progCounter = 0x38;
					break;
			}
		}

		public static unsafe void JR(CPU* cpu, JumpCodes jumpcode)
		{
			switch (jumpcode)
			{
				case JumpCodes.ALWAYS:
					cpu->progCounter = (ushort)(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu) + cpu->progCounter);
					break;
				case JumpCodes.CARRY:
					if (cpu->registers.GetFlagCarry())
						cpu->progCounter = (ushort)(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu) + cpu->progCounter);
					else
						cpu->progCounter += 2;
					break;
				case JumpCodes.ZERO:
					if (cpu->registers.GetFlagZero())
						cpu->progCounter = (ushort)(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu) + cpu->progCounter);
					else
						cpu->progCounter += 2;
					break;
				case JumpCodes.NOTZERO:
					if (!cpu->registers.GetFlagZero())
						cpu->progCounter = (ushort)(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu) + cpu->progCounter);
					else
						cpu->progCounter += 2;
					break;
				case JumpCodes.NOTCARRY:
					if (!cpu->registers.GetFlagCarry())
						cpu->progCounter = (ushort)(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu) + cpu->progCounter);
					else
						cpu->progCounter += 2;
					break;
			}
		}

		public static unsafe void OR(CPU* cpu, byte* val, Target targ)
		{
			if (val == null && targ == Target.HL)
				*val = cpu->memoryBus.ReadByte(cpu->registers.GetHL(), cpu);
			else if (val == null && targ == Target.NEXT)
			{
				cpu->progCounter++;
				*val = cpu->memoryBus.ReadByte(cpu->progCounter, cpu);
			}
			cpu->registers.a = (byte)(cpu->registers.a | (*val));
			cpu->registers.ResetFlags();
			if (cpu->registers.a == 0)
				cpu->registers.SetFlagZero(true);
			cpu->progCounter++;
		}

		public static unsafe void AND(CPU* cpu, byte* val, Target targ)
		{
			cpu->registers.ResetFlags();
			if (val == null && targ == Target.HL)
				*val = cpu->memoryBus.ReadByte(cpu->registers.GetHL(), cpu);
			else if (val == null && targ == Target.NEXT)
			{
				cpu->progCounter++;
				*val = cpu->memoryBus.ReadByte(cpu->progCounter, cpu);
			}
			if ((((*val & 0xf) & ((*val) & 0xf)) & 0x10) == 0x10)
				cpu->registers.SetFlagHalfCarry(true);
			cpu->registers.a = (byte)(cpu->registers.a & (*val));
			if (cpu->registers.a == 0)
				cpu->registers.SetFlagZero(true);
			cpu->progCounter++;
		}

		public static unsafe void CCF(CPU* cpu)
		{
			bool z = cpu->registers.GetFlagZero();
			bool c = cpu->registers.GetFlagCarry();
			cpu->registers.ResetFlags();
			cpu->registers.SetFlagZero(z);
			cpu->registers.SetFlagCarry(!c);
			cpu->progCounter++;
		}

		public static unsafe void LOAD16BN(CPU* cpu, Target targ)
		{
			ushort val = cpu->memoryBus.ReadShort((ushort)(cpu->progCounter + 1), cpu);
			switch (targ)
			{
				case Target.BC:
					cpu->registers.SetBC(val);
					break;
				case Target.DE:
					cpu->registers.SetDE(val);
					break;
				case Target.HL:
					cpu->registers.SetHL(val);
					break;
				case Target.SP:
					cpu->stackpointer = val;
					break;
			}
			cpu->progCounter += 3;
		}
	}
}
