using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	// Token: 0x0200037F RID: 895
	internal class StringSource : IEnumString
	{
		// Token: 0x06003A90 RID: 14992 RVA: 0x001022B8 File Offset: 0x001004B8
		public StringSource(string[] strings)
		{
			Array.Clear(strings, 0, this.size);
			if (strings != null)
			{
				this.strings = strings;
			}
			this.current = 0;
			this.size = ((strings == null) ? 0 : strings.Length);
			Guid guid = typeof(UnsafeNativeMethods.IAutoComplete2).GUID;
			object obj = UnsafeNativeMethods.CoCreateInstance(ref StringSource.autoCompleteClsid, null, 1, ref guid);
			this.autoCompleteObject2 = (UnsafeNativeMethods.IAutoComplete2)obj;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x00102324 File Offset: 0x00100524
		public bool Bind(HandleRef edit, int options)
		{
			bool flag = false;
			if (this.autoCompleteObject2 != null)
			{
				try
				{
					this.autoCompleteObject2.SetOptions(options);
					this.autoCompleteObject2.Init(edit, this, null, null);
					flag = true;
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00102374 File Offset: 0x00100574
		public void ReleaseAutoComplete()
		{
			if (this.autoCompleteObject2 != null)
			{
				Marshal.ReleaseComObject(this.autoCompleteObject2);
				this.autoCompleteObject2 = null;
			}
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x00102394 File Offset: 0x00100594
		public void RefreshList(string[] newSource)
		{
			Array.Clear(this.strings, 0, this.size);
			if (this.strings != null)
			{
				this.strings = newSource;
			}
			this.current = 0;
			this.size = ((this.strings == null) ? 0 : this.strings.Length);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x001023E2 File Offset: 0x001005E2
		void IEnumString.Clone(out IEnumString ppenum)
		{
			ppenum = new StringSource(this.strings);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x001023F4 File Offset: 0x001005F4
		int IEnumString.Next(int celt, string[] rgelt, IntPtr pceltFetched)
		{
			if (celt < 0)
			{
				return -2147024809;
			}
			int num = 0;
			while (this.current < this.size && celt > 0)
			{
				rgelt[num] = this.strings[this.current];
				this.current++;
				num++;
				celt--;
			}
			if (pceltFetched != IntPtr.Zero)
			{
				Marshal.WriteInt32(pceltFetched, num);
			}
			if (celt != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x00102462 File Offset: 0x00100662
		void IEnumString.Reset()
		{
			this.current = 0;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x0010246B File Offset: 0x0010066B
		int IEnumString.Skip(int celt)
		{
			this.current += celt;
			if (this.current >= this.size)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04002317 RID: 8983
		private string[] strings;

		// Token: 0x04002318 RID: 8984
		private int current;

		// Token: 0x04002319 RID: 8985
		private int size;

		// Token: 0x0400231A RID: 8986
		private UnsafeNativeMethods.IAutoComplete2 autoCompleteObject2;

		// Token: 0x0400231B RID: 8987
		private static Guid autoCompleteClsid = new Guid("{00BB2763-6A77-11D0-A535-00C04FD7D062}");
	}
}
