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
			int[] instructs = new int[3];
			switch (code)
			{
				case 0xCB:
					instructs = GetCBInstruction(cpu->memoryBus.ReadByte((short)(cpu->progCounter + 1)), cpu);
					break;
				#region JUMP
				case 0xC2:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.NOTZERO;
					break;
				case 0xD2:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.NOTCARRY;
					break;
				case 0xC3:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.ALWAYS;
					break;
				case 0xCA:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.ZERO;
					break;
				case 0xDA:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.CARRY;
					break;
				case 0xE9:
					instructs[0] = (int)Instructs.JUMP;
					instructs[1] = (int)JumpCodes.HL;
					break;
				#endregion
				#region ADD
				case 0x80:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.B;
					break;
				case 0x81:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.C;
					break;
				case 0x82:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.D;
					break;
				case 0x83:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.E;
					break;
				case 0x84:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.H;
					break;
				case 0x85:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.L;
					break;
				case 0x87:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.A;
					break;
				#endregion
				#region LOAD
				#region Into B
				case 0x40:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.B;
					break;
				case 0x41:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.C;
					break;
				case 0x42:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.D;
					break;
				case 0x43:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.E;
					break;
				case 0x44:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.H;
					break;
				case 0x45:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.L;
					break;
				case 0x46:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.HL;
					break;
				case 0x47:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.B;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into C
				case 0x48:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.B;
					break;
				case 0x49:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.C;
					break;
				case 0x4A:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.D;
					break;
				case 0x4B:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.E;
					break;
				case 0x4C:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.H;
					break;
				case 0x4D:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.L;
					break;
				case 0x4E:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.HL;
					break;
				case 0x4F:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.C;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into D
				case 0x50:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.B;
					break;
				case 0x51:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.C;
					break;
				case 0x52:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.D;
					break;
				case 0x53:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.E;
					break;
				case 0x54:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.H;
					break;
				case 0x55:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.L;
					break;
				case 0x56:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.HL;
					break;
				case 0x57:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.D;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into E
				case 0x58:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.B;
					break;
				case 0x59:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.C;
					break;
				case 0x5A:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.D;
					break;
				case 0x5B:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.E;
					break;
				case 0x5C:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.H;
					break;
				case 0x5D:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.L;
					break;
				case 0x5E:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.HL;
					break;
				case 0x5F:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.E;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into H
				case 0x60:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.B;
					break;
				case 0x61:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.C;
					break;
				case 0x62:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.D;
					break;
				case 0x63:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.E;
					break;
				case 0x64:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.H;
					break;
				case 0x65:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.L;
					break;
				case 0x66:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.HL;
					break;
				case 0x67:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.H;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into E
				case 0x68:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.B;
					break;
				case 0x69:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.C;
					break;
				case 0x6A:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.D;
					break;
				case 0x6B:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.E;
					break;
				case 0x6C:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.H;
					break;
				case 0x6D:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.L;
					break;
				case 0x6E:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.HL;
					break;
				case 0x6F:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.L;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into HL
				case 0x70:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.B;
					break;
				case 0x71:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.C;
					break;
				case 0x72:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.D;
					break;
				case 0x73:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.E;
					break;
				case 0x74:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.H;
					break;
				case 0x75:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.L;
					break;
				case 0x77:
					instructs[0] = (int)Instructs.LOAD16B8B;
					instructs[1] = (int)Target.HL;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#region Into A
				case 0x78:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.B;
					break;
				case 0x79:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.C;
					break;
				case 0x7A:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.D;
					break;
				case 0x7B:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.E;
					break;
				case 0x7C:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.H;
					break;
				case 0x7D:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.L;
					break;
				case 0x7E:
					instructs[0] = (int)Instructs.LOAD8B16B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.HL;
					break;
				case 0x7F:
					instructs[0] = (int)Instructs.LOAD8B;
					instructs[1] = (int)Target.A;
					instructs[2] = (int)Target.A;
					break;
				#endregion
				#endregion
				default:
					instructs[0] = -1;
					break;
			}
			return (instructs);
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
