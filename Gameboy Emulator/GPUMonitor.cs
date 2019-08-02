using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameboy_Emulator
{
	class GPUMonitor
	{
		private static Bitmap bitmap = new Bitmap(160, 144);

		public static void MonitorGPU(Form1 form)
		{
			bool val = false;
			while (true)
			{
				lock ("GPU Update")
				{
					if (GBCore.GPU.Updated)
						val = true;
				}
				if (val)
				{
					UpdateImage(form);
					form.GBScreen.Image = bitmap;
				}
			}
		}

		private unsafe static void UpdateImage(Form1 form)
		{
			int x = 0, y = 0, z = 0;
			for(int i = 0; i < 160; i++)
			{
				for(int j = 0; j < 144; j++)
				{
					bitmap.SetPixel(i, j, GetColour(form.cpu->gpu.tile_map[x + 8 * (y + 8 * z)]));
					if (z >= 8)
					{
						y++;
						z = 0;
					}
					if (y >= 8)
					{
						x++;
						y = 0;
					}
				}
			}
		}

		private static Color GetColour(int colour)
		{
			if (colour == 0)
				return Color.White;
			else if (colour == 1)
				return Color.Black;
			else if (colour == 2)
				return Color.LightGray;
			else if (colour == 3)
				return Color.DarkGray;
			return Color.White;
		}

	}
}
