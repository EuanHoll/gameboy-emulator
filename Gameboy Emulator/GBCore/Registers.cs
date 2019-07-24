using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator.GBCore
{
	struct Registers
	{
		public byte a;
		public byte b;
		public byte c;
		public byte d;
		public byte e;
		public byte f;
		public byte h;
		public byte l;
		public byte flags;

		public short GetBC()
		{
			return ((short)((b << 8) + c));
		}

		public void SetBC(short bc)
		{
			this.b = (byte)(bc >> 8);
			this.c = (byte)(255 & bc);
		}

		public short GetAF()
		{
			return ((short)((a << 8) + f));
		}

		public void SetAF(short af)
		{
			this.a = (byte)(af >> 8);
			this.f = (byte)(255 & af);
		}

		public short GetDE()
		{
			return ((short)((d << 8) + e));
		}

		public void SetDE(short de)
		{
			this.d = (byte)(de >> 8);
			this.e = (byte)(255 & de);
		}

		public short GetHL()
		{
			return ((short)((h << 8) + l));
		}

		public void SetHL(short hl)
		{
			this.h = (byte)(hl >> 8);
			this.l = (byte)(255 & hl);
		}

		public void SetFlagZero(bool val)
		{
			if (val)
				flags |= 1 << 7;
			else
				flags &= (0 << 7);
		}

		public void SetFlagSub(bool val)
		{
			if (val)
				flags |= 1 << 6;
			else
				flags &= (0 << 6);
		}

		public void SetFlagHalfCarry(bool val)
		{
			if (val)
				flags |= 1 << 5;
			else
				flags &= (0 << 5);
		}

		public void SetFlagCarry(bool val)
		{
			if (val)
				flags |= 1 << 4;
			else
				flags &= (0 << 4);
		}
		
		public bool GetFlagZero()
		{
			return (flags >> 7 == 1);
		}

		public bool GetFlagSub()
		{
			return (flags >> 6 == 1);
		}

		public bool GetFlagHalfCarry()
		{
			return (flags >> 5 == 1);
		}

		public bool GetFlagCarry()
		{
			return (flags >> 4 == 1);
		}

	}
}
