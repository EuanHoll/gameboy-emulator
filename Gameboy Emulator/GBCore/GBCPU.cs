using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	class GBCPU
	{
		public static bool is_halted = false;
		public unsafe void CPUStart()
		{
			CPU cpu = new CPU(0);
			while (!is_halted)
				Step(&cpu);
		}

		private unsafe bool Step(CPU* cpu)
		{
			byte instruct = cpu->memoryBus.ReadByte(cpu->progCounter);
			int[] intInstructions = Opcodes.GetInstruction(instruct, cpu);
			if (intInstructions[0] == -1)
			{
				Console.WriteLine("Could not find opcode : {0:X} {0:X}.", instruct, cpu->memoryBus.ReadByte(cpu->progCounter));
				return (false);
			}
			Execute((Instructs)intInstructions[0], intInstructions[1], cpu, intInstructions);
			return (true);
		}

		private unsafe void Execute(Instructs inst, int targ, CPU* cpu, int[] ar)
		{
			switch (inst)
			{
				case Instructs.ADD:
					byte* tbyte = GetTarget((Target)targ, &cpu->registers);
					Instructions.ADD(&cpu->registers, tbyte, cpu);
					break;
				case Instructs.JUMP:
					Instructions.JUMP(&cpu->registers, cpu, (JumpCodes)(targ));
					break;
				case Instructs.LOAD8B:
					Instructions.LOAD8B(cpu, GetTarget((Target)targ, &cpu->registers), GetTarget((Target)ar[2], &cpu->registers));
					break;
				case Instructs.LOAD8B16B:
					Instructions.LOAD8B16B(cpu, GetTarget((Target)targ, &cpu->registers), GetAddressValue((Target)ar[1], &cpu->registers));
					break;
				case Instructs.POP:
					Instructions.POP(cpu, (Target)targ);
					break;
				case Instructs.PUSH:
					Instructions.PUSH(cpu, (Target)targ);
					break;
				case Instructs.CALL:
					Instructions.CALL(cpu, (JumpCodes)targ);
					break;
				case Instructs.RET:
					Instructions.RET(cpu, (JumpCodes)targ);
					break;
				case Instructs.NOP:
					Instructions.NOP(cpu);
					break;
				case Instructs.HALT:
					Instructions.HALT(cpu);
					break;
			}
		}

		private unsafe byte* GetTarget(Target targ, Registers* regs)
		{
			switch (targ)
			{
				case Target.A:
					return &regs->a;
				case Target.B:
					return &regs->b;
				case Target.C:
					return &regs->c;
				case Target.D:
					return &regs->d;
				case Target.E:
					return &regs->e;
				case Target.F:
					return &regs->flags;
				case Target.H:
					return &regs->h;
				default:
					return &regs->l;
			}
		}

		private unsafe short GetAddressValue(Target targ, Registers* regs)
		{
			switch (targ)
			{
				case Target.HL:
					return regs->GetHL();
				case Target.BC:
					return regs->GetBC();
				default:
					return regs->GetDE();
			}
		}

	}
}
