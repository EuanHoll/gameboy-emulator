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

		public unsafe void CPUStart()
		{
			CPU cpu = new CPU(0);
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
			Execute((Instructs)intInstructions[0], (Target)intInstructions[1], cpu);
			return (true);
		}

		private unsafe void Execute(Instructs inst, Target targ, CPU* cpu)
		{
			byte* tbyte = GetTarget(targ, &cpu->registers);
			switch (inst)
			{
				case Instructs.ADD:
					Instructions.ADD(&cpu->registers, tbyte);
					break;
			}
			cpu->progCounter++;
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
					return &regs->f;
				case Target.H:
					return &regs->h;
				default:
					return &regs->l;
			}
		}

	}
}
