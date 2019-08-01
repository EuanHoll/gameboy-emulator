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
					instructs = GetCBInstruction(cpu->memoryBus.ReadByte((ushort)(cpu->progCounter + 1), cpu), cpu);
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
				case 0x86:
					instructs[0] = (int)Instructs.ADD;
					instructs[1] = (int)Target.HL;
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
				#region POP
				case 0xC1:
					instructs[0] = (int)Instructs.POP;
					instructs[1] = (int)Target.BC;
					break;
				case 0xD1:
					instructs[0] = (int)Instructs.POP;
					instructs[1] = (int)Target.DE;
					break;
				case 0xE1:
					instructs[0] = (int)Instructs.POP;
					instructs[1] = (int)Target.HL;
					break;
				case 0xF1:
					instructs[0] = (int)Instructs.POP;
					instructs[1] = (int)Target.AF;
					break;
				#endregion
				#region PUSH
				case 0xC5:
					instructs[0] = (int)Instructs.PUSH;
					instructs[1] = (int)Target.BC;
					break;
				case 0xD5:
					instructs[0] = (int)Instructs.PUSH;
					instructs[1] = (int)Target.DE;
					break;
				case 0xE5:
					instructs[0] = (int)Instructs.PUSH;
					instructs[1] = (int)Target.HL;
					break;
				case 0xF5:
					instructs[0] = (int)Instructs.PUSH;
					instructs[1] = (int)Target.AF;
					break;
				#endregion
				#region RET
				case 0xC0:
					instructs[0] = (int)Instructs.RET;
					instructs[1] = (int)JumpCodes.NOTZERO;
					break;
				case 0xD0:
					instructs[0] = (int)Instructs.RET;
					instructs[1] = (int)JumpCodes.NOTCARRY;
					break;
				case 0xC8:
					instructs[0] = (int)Instructs.RET;
					instructs[1] = (int)JumpCodes.ZERO;
					break;
				case 0xD8:
					instructs[0] = (int)Instructs.RET;
					instructs[1] = (int)JumpCodes.CARRY;
					break;
				case 0xC9:
					instructs[0] = (int)Instructs.RET;
					instructs[1] = (int)JumpCodes.ALWAYS;
					break;
				#endregion
				#region CALL
				case 0xC4:
					instructs[0] = (int)Instructs.CALL;
					instructs[1] = (int)JumpCodes.NOTZERO;
					break;
				case 0xD4:
					instructs[0] = (int)Instructs.CALL;
					instructs[1] = (int)JumpCodes.NOTCARRY;
					break;
				case 0xCC:
					instructs[0] = (int)Instructs.CALL;
					instructs[1] = (int)JumpCodes.ZERO;
					break;
				case 0xDC:
					instructs[0] = (int)Instructs.CALL;
					instructs[1] = (int)JumpCodes.CARRY;
					break;
				case 0xCD:
					instructs[0] = (int)Instructs.CALL;
					instructs[1] = (int)JumpCodes.ALWAYS;
					break;
				#endregion
				#region INC
				case 0x04:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.B;
					break;
				case 0x14:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.D;
					break;
				case 0x24:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.H;
					break;
				case 0x34:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.HL;
					break;
				case 0x0C:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.C;
					break;
				case 0x1C:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.E;
					break;
				case 0x2C:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.L;
					break;
				case 0x3C:
					instructs[0] = (int)Instructs.INC;
					instructs[1] = (int)Target.A;
					break;
				#endregion
				#region DEC
				case 0x05:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.B;
					break;
				case 0x15:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.D;
					break;
				case 0x25:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.H;
					break;
				case 0x35:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.HL;
					break;
				case 0x0D:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.C;
					break;
				case 0x1D:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.E;
					break;
				case 0x2D:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.L;
					break;
				case 0x3D:
					instructs[0] = (int)Instructs.DEC;
					instructs[1] = (int)Target.A;
					break;
				#endregion
				#region NOP
				case 0x00:
					instructs[0] = (int)Instructs.NOP;
					break;
				#endregion
				#region RST
				case 0xC7:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H00;
					break;
				case 0xD7:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H10;
					break;
				case 0xE7:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H20;
					break;
				case 0xF7:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H30;
					break;
				case 0xCF:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H08;
					break;
				case 0xDF:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H18;
					break;
				case 0xEF:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H28;
					break;
				case 0xFF:
					instructs[0] = (int)Instructs.RST;
					instructs[1] = (int)RSTTYPE.H38;
					break;
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
