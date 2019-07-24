using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	class Opcodes
	{
		public unsafe static int[] GetInstruction(byte code, CPU* cpu)
		{
			int[] instucts = new int[2];
			switch (code)
			{
				case 0xCB:
					instucts = GetCBInstruction(cpu->memoryBus.ReadByte((short)(cpu->progCounter + 1)), cpu);
					break;
				case 0x80:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.B;
					break;
				case 0x81:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.C;
					break;
				case 0x82:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.D;
					break;
				case 0x83:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.E;
					break;
				case 0x84:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.H;
					break;
				case 0x85:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.L;
					break;
				case 0x87:
					instucts[0] = (int)Instructs.ADD;
					instucts[1] = (int)Target.A;
					break;
				default:
					instucts[0] = -1;
					break;
			}
			return (instucts);
		}

		public unsafe static int[] GetCBInstruction(byte code, CPU* cpu)
		{
			int[] ins = new int[2];
			cpu->progCounter++;
			switch (code)
			{
				default:
					ins[0] = -1;
					break;
			}
			return (ins);
		}

	}
}
