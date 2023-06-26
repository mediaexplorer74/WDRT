using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines an array of colors that make up a color palette. The colors are 32-bit ARGB colors. Not inheritable.</summary>
	// Token: 0x02000094 RID: 148
	public sealed class ColorPalette
	{
		/// <summary>Gets a value that specifies how to interpret the color information in the array of colors.</summary>
		/// <returns>The following flag values are valid:  
		///  0x00000001 The color values in the array contain alpha information.  
		///  0x00000002 The colors in the array are grayscale values.  
		///  0x00000004 The colors in the array are halftone values.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00022B15 File Offset: 0x00020D15
		public int Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Drawing.Color" /> structures.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Color" /> structure that make up this <see cref="T:System.Drawing.Imaging.ColorPalette" />.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00022B1D File Offset: 0x00020D1D
		public Color[] Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00022B25 File Offset: 0x00020D25
		internal ColorPalette(int count)
		{
			this.entries = new Color[count];
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00022B39 File Offset: 0x00020D39
		internal ColorPalette()
		{
			this.entries = new Color[1];
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00022B50 File Offset: 0x00020D50
		internal void ConvertFromMemory(IntPtr memory)
		{
			this.flags = Marshal.ReadInt32(memory);
			int num = Marshal.ReadInt32((IntPtr)((long)memory + 4L));
			this.entries = new Color[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = Marshal.ReadInt32((IntPtr)((long)memory + 8L + (long)(i * 4)));
				this.entries[i] = Color.FromArgb(num2);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00022BC0 File Offset: 0x00020DC0
		internal IntPtr ConvertToMemory()
		{
			int num = this.entries.Length;
			IntPtr intPtr;
			checked
			{
				intPtr = Marshal.AllocHGlobal(4 * (2 + num));
				Marshal.WriteInt32(intPtr, 0, this.flags);
				Marshal.WriteInt32((IntPtr)((long)intPtr + 4L), 0, num);
			}
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteInt32((IntPtr)((long)intPtr + (long)(4 * (i + 2))), 0, this.entries[i].ToArgb());
			}
			return intPtr;
		}

		// Token: 0x0400076D RID: 1901
		private int flags;

		// Token: 0x0400076E RID: 1902
		private Color[] entries;
	}
}
