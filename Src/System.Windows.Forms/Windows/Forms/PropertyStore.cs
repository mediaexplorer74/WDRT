using System;
using System.Diagnostics;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000331 RID: 817
	internal class PropertyStore
	{
		// Token: 0x0600353C RID: 13628 RVA: 0x000F18FC File Offset: 0x000EFAFC
		public bool ContainsInteger(int key)
		{
			bool flag;
			this.GetInteger(key, out flag);
			return flag;
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000F1914 File Offset: 0x000EFB14
		public bool ContainsObject(int key)
		{
			bool flag;
			this.GetObject(key, out flag);
			return flag;
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000F192C File Offset: 0x000EFB2C
		public static int CreateKey()
		{
			return PropertyStore.currentKey++;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000F193C File Offset: 0x000EFB3C
		public Color GetColor(int key)
		{
			bool flag;
			return this.GetColor(key, out flag);
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000F1954 File Offset: 0x000EFB54
		public Color GetColor(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.ColorWrapper colorWrapper = @object as PropertyStore.ColorWrapper;
				if (colorWrapper != null)
				{
					return colorWrapper.Color;
				}
			}
			found = false;
			return Color.Empty;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000F1988 File Offset: 0x000EFB88
		public Padding GetPadding(int key)
		{
			bool flag;
			return this.GetPadding(key, out flag);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000F19A0 File Offset: 0x000EFBA0
		public Padding GetPadding(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.PaddingWrapper paddingWrapper = @object as PropertyStore.PaddingWrapper;
				if (paddingWrapper != null)
				{
					return paddingWrapper.Padding;
				}
			}
			found = false;
			return Padding.Empty;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000F19D4 File Offset: 0x000EFBD4
		public Size GetSize(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.SizeWrapper sizeWrapper = @object as PropertyStore.SizeWrapper;
				if (sizeWrapper != null)
				{
					return sizeWrapper.Size;
				}
			}
			found = false;
			return Size.Empty;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000F1A08 File Offset: 0x000EFC08
		public Rectangle GetRectangle(int key)
		{
			bool flag;
			return this.GetRectangle(key, out flag);
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000F1A20 File Offset: 0x000EFC20
		public Rectangle GetRectangle(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.RectangleWrapper rectangleWrapper = @object as PropertyStore.RectangleWrapper;
				if (rectangleWrapper != null)
				{
					return rectangleWrapper.Rectangle;
				}
			}
			found = false;
			return Rectangle.Empty;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000F1A54 File Offset: 0x000EFC54
		public int GetInteger(int key)
		{
			bool flag;
			return this.GetInteger(key, out flag);
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000F1A6C File Offset: 0x000EFC6C
		public int GetInteger(int key, out bool found)
		{
			int num = 0;
			short num3;
			short num2 = this.SplitKey(key, out num3);
			found = false;
			int num4;
			if (this.LocateIntegerEntry(num2, out num4) && ((1 << (int)num3) & (int)this.intEntries[num4].Mask) != 0)
			{
				found = true;
				switch (num3)
				{
				case 0:
					num = this.intEntries[num4].Value1;
					break;
				case 1:
					num = this.intEntries[num4].Value2;
					break;
				case 2:
					num = this.intEntries[num4].Value3;
					break;
				case 3:
					num = this.intEntries[num4].Value4;
					break;
				}
			}
			return num;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000F1B1C File Offset: 0x000EFD1C
		public object GetObject(int key)
		{
			bool flag;
			return this.GetObject(key, out flag);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000F1B34 File Offset: 0x000EFD34
		public object GetObject(int key, out bool found)
		{
			object obj = null;
			short num2;
			short num = this.SplitKey(key, out num2);
			found = false;
			int num3;
			if (this.LocateObjectEntry(num, out num3) && ((1 << (int)num2) & (int)this.objEntries[num3].Mask) != 0)
			{
				found = true;
				switch (num2)
				{
				case 0:
					obj = this.objEntries[num3].Value1;
					break;
				case 1:
					obj = this.objEntries[num3].Value2;
					break;
				case 2:
					obj = this.objEntries[num3].Value3;
					break;
				case 3:
					obj = this.objEntries[num3].Value4;
					break;
				}
			}
			return obj;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000F1BE4 File Offset: 0x000EFDE4
		private bool LocateIntegerEntry(short entryKey, out int index)
		{
			if (this.intEntries == null)
			{
				index = 0;
				return false;
			}
			int num = this.intEntries.Length;
			if (num > 16)
			{
				int num2 = num - 1;
				int num3 = 0;
				int num4;
				for (;;)
				{
					num4 = (num2 + num3) / 2;
					short key = this.intEntries[num4].Key;
					if (key == entryKey)
					{
						break;
					}
					if (entryKey < key)
					{
						num2 = num4 - 1;
					}
					else
					{
						num3 = num4 + 1;
					}
					if (num2 < num3)
					{
						goto Block_14;
					}
				}
				index = num4;
				return true;
				Block_14:
				index = num4;
				if (entryKey > this.intEntries[num4].Key)
				{
					index++;
				}
				return false;
			}
			index = 0;
			int num5 = num / 2;
			if (this.intEntries[num5].Key <= entryKey)
			{
				index = num5;
			}
			if (this.intEntries[index].Key == entryKey)
			{
				return true;
			}
			num5 = (num + 1) / 4;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 3) / 8;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 7) / 16;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			if (entryKey > this.intEntries[index].Key)
			{
				index++;
			}
			return false;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000F1D78 File Offset: 0x000EFF78
		private bool LocateObjectEntry(short entryKey, out int index)
		{
			if (this.objEntries == null)
			{
				index = 0;
				return false;
			}
			int num = this.objEntries.Length;
			if (num > 16)
			{
				int num2 = num - 1;
				int num3 = 0;
				int num4;
				for (;;)
				{
					num4 = (num2 + num3) / 2;
					short key = this.objEntries[num4].Key;
					if (key == entryKey)
					{
						break;
					}
					if (entryKey < key)
					{
						num2 = num4 - 1;
					}
					else
					{
						num3 = num4 + 1;
					}
					if (num2 < num3)
					{
						goto Block_14;
					}
				}
				index = num4;
				return true;
				Block_14:
				index = num4;
				if (entryKey > this.objEntries[num4].Key)
				{
					index++;
				}
				return false;
			}
			index = 0;
			int num5 = num / 2;
			if (this.objEntries[num5].Key <= entryKey)
			{
				index = num5;
			}
			if (this.objEntries[index].Key == entryKey)
			{
				return true;
			}
			num5 = (num + 1) / 4;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 3) / 8;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 7) / 16;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			if (entryKey > this.objEntries[index].Key)
			{
				index++;
			}
			return false;
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000F1F0C File Offset: 0x000F010C
		public void RemoveInteger(int key)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (this.LocateIntegerEntry(num, out num3))
			{
				if (((1 << (int)num2) & (int)this.intEntries[num3].Mask) == 0)
				{
					return;
				}
				PropertyStore.IntegerEntry[] array = this.intEntries;
				int num4 = num3;
				array[num4].Mask = array[num4].Mask & ~(short)(1 << (int)num2);
				if (this.intEntries[num3].Mask == 0)
				{
					PropertyStore.IntegerEntry[] array2 = new PropertyStore.IntegerEntry[this.intEntries.Length - 1];
					if (num3 > 0)
					{
						Array.Copy(this.intEntries, 0, array2, 0, num3);
					}
					if (num3 < array2.Length)
					{
						Array.Copy(this.intEntries, num3 + 1, array2, num3, this.intEntries.Length - num3 - 1);
					}
					this.intEntries = array2;
					return;
				}
				switch (num2)
				{
				case 0:
					this.intEntries[num3].Value1 = 0;
					return;
				case 1:
					this.intEntries[num3].Value2 = 0;
					return;
				case 2:
					this.intEntries[num3].Value3 = 0;
					return;
				case 3:
					this.intEntries[num3].Value4 = 0;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000F2030 File Offset: 0x000F0230
		public void RemoveObject(int key)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (this.LocateObjectEntry(num, out num3))
			{
				if (((1 << (int)num2) & (int)this.objEntries[num3].Mask) == 0)
				{
					return;
				}
				PropertyStore.ObjectEntry[] array = this.objEntries;
				int num4 = num3;
				array[num4].Mask = array[num4].Mask & ~(short)(1 << (int)num2);
				if (this.objEntries[num3].Mask == 0)
				{
					if (this.objEntries.Length == 1)
					{
						this.objEntries = null;
						return;
					}
					PropertyStore.ObjectEntry[] array2 = new PropertyStore.ObjectEntry[this.objEntries.Length - 1];
					if (num3 > 0)
					{
						Array.Copy(this.objEntries, 0, array2, 0, num3);
					}
					if (num3 < array2.Length)
					{
						Array.Copy(this.objEntries, num3 + 1, array2, num3, this.objEntries.Length - num3 - 1);
					}
					this.objEntries = array2;
					return;
				}
				else
				{
					switch (num2)
					{
					case 0:
						this.objEntries[num3].Value1 = null;
						return;
					case 1:
						this.objEntries[num3].Value2 = null;
						return;
					case 2:
						this.objEntries[num3].Value3 = null;
						return;
					case 3:
						this.objEntries[num3].Value4 = null;
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000F2164 File Offset: 0x000F0364
		public void SetColor(int key, Color value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.ColorWrapper(value));
				return;
			}
			PropertyStore.ColorWrapper colorWrapper = @object as PropertyStore.ColorWrapper;
			if (colorWrapper != null)
			{
				colorWrapper.Color = value;
				return;
			}
			this.SetObject(key, new PropertyStore.ColorWrapper(value));
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000F21AC File Offset: 0x000F03AC
		public void SetPadding(int key, Padding value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.PaddingWrapper(value));
				return;
			}
			PropertyStore.PaddingWrapper paddingWrapper = @object as PropertyStore.PaddingWrapper;
			if (paddingWrapper != null)
			{
				paddingWrapper.Padding = value;
				return;
			}
			this.SetObject(key, new PropertyStore.PaddingWrapper(value));
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000F21F4 File Offset: 0x000F03F4
		public void SetRectangle(int key, Rectangle value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.RectangleWrapper(value));
				return;
			}
			PropertyStore.RectangleWrapper rectangleWrapper = @object as PropertyStore.RectangleWrapper;
			if (rectangleWrapper != null)
			{
				rectangleWrapper.Rectangle = value;
				return;
			}
			this.SetObject(key, new PropertyStore.RectangleWrapper(value));
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000F223C File Offset: 0x000F043C
		public void SetSize(int key, Size value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.SizeWrapper(value));
				return;
			}
			PropertyStore.SizeWrapper sizeWrapper = @object as PropertyStore.SizeWrapper;
			if (sizeWrapper != null)
			{
				sizeWrapper.Size = value;
				return;
			}
			this.SetObject(key, new PropertyStore.SizeWrapper(value));
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000F2284 File Offset: 0x000F0484
		public void SetInteger(int key, int value)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (!this.LocateIntegerEntry(num, out num3))
			{
				if (this.intEntries != null)
				{
					PropertyStore.IntegerEntry[] array = new PropertyStore.IntegerEntry[this.intEntries.Length + 1];
					if (num3 > 0)
					{
						Array.Copy(this.intEntries, 0, array, 0, num3);
					}
					if (this.intEntries.Length - num3 > 0)
					{
						Array.Copy(this.intEntries, num3, array, num3 + 1, this.intEntries.Length - num3);
					}
					this.intEntries = array;
				}
				else
				{
					this.intEntries = new PropertyStore.IntegerEntry[1];
				}
				this.intEntries[num3].Key = num;
			}
			switch (num2)
			{
			case 0:
				this.intEntries[num3].Value1 = value;
				break;
			case 1:
				this.intEntries[num3].Value2 = value;
				break;
			case 2:
				this.intEntries[num3].Value3 = value;
				break;
			case 3:
				this.intEntries[num3].Value4 = value;
				break;
			}
			this.intEntries[num3].Mask = (short)((1 << (int)num2) | (int)((ushort)this.intEntries[num3].Mask));
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000F23B0 File Offset: 0x000F05B0
		public void SetObject(int key, object value)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (!this.LocateObjectEntry(num, out num3))
			{
				if (this.objEntries != null)
				{
					PropertyStore.ObjectEntry[] array = new PropertyStore.ObjectEntry[this.objEntries.Length + 1];
					if (num3 > 0)
					{
						Array.Copy(this.objEntries, 0, array, 0, num3);
					}
					if (this.objEntries.Length - num3 > 0)
					{
						Array.Copy(this.objEntries, num3, array, num3 + 1, this.objEntries.Length - num3);
					}
					this.objEntries = array;
				}
				else
				{
					this.objEntries = new PropertyStore.ObjectEntry[1];
				}
				this.objEntries[num3].Key = num;
			}
			switch (num2)
			{
			case 0:
				this.objEntries[num3].Value1 = value;
				break;
			case 1:
				this.objEntries[num3].Value2 = value;
				break;
			case 2:
				this.objEntries[num3].Value3 = value;
				break;
			case 3:
				this.objEntries[num3].Value4 = value;
				break;
			}
			this.objEntries[num3].Mask = (short)((int)((ushort)this.objEntries[num3].Mask) | (1 << (int)num2));
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000F24DC File Offset: 0x000F06DC
		private short SplitKey(int key, out short element)
		{
			element = (short)(key & 3);
			return (short)((long)key & (long)((ulong)(-4)));
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000F24EC File Offset: 0x000F06EC
		[Conditional("DEBUG_PROPERTYSTORE")]
		private void Debug_VerifyLocateIntegerEntry(int index, short entryKey, int length)
		{
			int num = length - 1;
			int num2 = 0;
			int num3;
			do
			{
				num3 = (num + num2) / 2;
				short key = this.intEntries[num3].Key;
				if (key != entryKey)
				{
					if (entryKey < key)
					{
						num = num3 - 1;
					}
					else
					{
						num2 = num3 + 1;
					}
				}
			}
			while (num >= num2);
			if (entryKey > this.intEntries[num3].Key)
			{
				num3++;
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000F2548 File Offset: 0x000F0748
		[Conditional("DEBUG_PROPERTYSTORE")]
		private void Debug_VerifyLocateObjectEntry(int index, short entryKey, int length)
		{
			int num = length - 1;
			int num2 = 0;
			int num3;
			do
			{
				num3 = (num + num2) / 2;
				short key = this.objEntries[num3].Key;
				if (key != entryKey)
				{
					if (entryKey < key)
					{
						num = num3 - 1;
					}
					else
					{
						num2 = num3 + 1;
					}
				}
			}
			while (num >= num2);
			if (entryKey > this.objEntries[num3].Key)
			{
				num3++;
			}
		}

		// Token: 0x04001F41 RID: 8001
		private static int currentKey;

		// Token: 0x04001F42 RID: 8002
		private PropertyStore.IntegerEntry[] intEntries;

		// Token: 0x04001F43 RID: 8003
		private PropertyStore.ObjectEntry[] objEntries;

		// Token: 0x020007D2 RID: 2002
		private struct IntegerEntry
		{
			// Token: 0x0400429B RID: 17051
			public short Key;

			// Token: 0x0400429C RID: 17052
			public short Mask;

			// Token: 0x0400429D RID: 17053
			public int Value1;

			// Token: 0x0400429E RID: 17054
			public int Value2;

			// Token: 0x0400429F RID: 17055
			public int Value3;

			// Token: 0x040042A0 RID: 17056
			public int Value4;
		}

		// Token: 0x020007D3 RID: 2003
		private struct ObjectEntry
		{
			// Token: 0x040042A1 RID: 17057
			public short Key;

			// Token: 0x040042A2 RID: 17058
			public short Mask;

			// Token: 0x040042A3 RID: 17059
			public object Value1;

			// Token: 0x040042A4 RID: 17060
			public object Value2;

			// Token: 0x040042A5 RID: 17061
			public object Value3;

			// Token: 0x040042A6 RID: 17062
			public object Value4;
		}

		// Token: 0x020007D4 RID: 2004
		private sealed class ColorWrapper
		{
			// Token: 0x06006D7E RID: 28030 RVA: 0x001918D9 File Offset: 0x0018FAD9
			public ColorWrapper(Color color)
			{
				this.Color = color;
			}

			// Token: 0x040042A7 RID: 17063
			public Color Color;
		}

		// Token: 0x020007D5 RID: 2005
		private sealed class PaddingWrapper
		{
			// Token: 0x06006D7F RID: 28031 RVA: 0x001918E8 File Offset: 0x0018FAE8
			public PaddingWrapper(Padding padding)
			{
				this.Padding = padding;
			}

			// Token: 0x040042A8 RID: 17064
			public Padding Padding;
		}

		// Token: 0x020007D6 RID: 2006
		private sealed class RectangleWrapper
		{
			// Token: 0x06006D80 RID: 28032 RVA: 0x001918F7 File Offset: 0x0018FAF7
			public RectangleWrapper(Rectangle rectangle)
			{
				this.Rectangle = rectangle;
			}

			// Token: 0x040042A9 RID: 17065
			public Rectangle Rectangle;
		}

		// Token: 0x020007D7 RID: 2007
		private sealed class SizeWrapper
		{
			// Token: 0x06006D81 RID: 28033 RVA: 0x00191906 File Offset: 0x0018FB06
			public SizeWrapper(Size size)
			{
				this.Size = size;
			}

			// Token: 0x040042AA RID: 17066
			public Size Size;
		}
	}
}
