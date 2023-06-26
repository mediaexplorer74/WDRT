using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200048F RID: 1167
	internal abstract class BaseCAMarshaler
	{
		// Token: 0x06004E44 RID: 20036 RVA: 0x00142758 File Offset: 0x00140958
		protected BaseCAMarshaler(NativeMethods.CA_STRUCT caStruct)
		{
			if (caStruct == null)
			{
				this.count = 0;
			}
			this.count = caStruct.cElems;
			this.caArrayAddress = caStruct.pElems;
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00142784 File Offset: 0x00140984
		protected override void Finalize()
		{
			try
			{
				if (this.itemArray == null && this.caArrayAddress != IntPtr.Zero)
				{
					object[] items = this.Items;
				}
			}
			catch
			{
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06004E46 RID: 20038
		protected abstract Array CreateArray();

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004E47 RID: 20039
		public abstract Type ItemType { get; }

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x001427DC File Offset: 0x001409DC
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004E49 RID: 20041 RVA: 0x001427E4 File Offset: 0x001409E4
		public virtual object[] Items
		{
			get
			{
				try
				{
					if (this.itemArray == null)
					{
						this.itemArray = this.Get_Items();
					}
				}
				catch (Exception ex)
				{
				}
				return this.itemArray;
			}
		}

		// Token: 0x06004E4A RID: 20042
		protected abstract object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex);

		// Token: 0x06004E4B RID: 20043 RVA: 0x00142820 File Offset: 0x00140A20
		private object[] Get_Items()
		{
			Array array = new object[this.Count];
			for (int i = 0; i < this.count; i++)
			{
				try
				{
					object obj = this.UnmarshalAndFreeOneItem(this.caArrayAddress, i);
					if (obj != null && this.ItemType.IsInstanceOfType(obj))
					{
						array.SetValue(obj, i);
					}
				}
				catch (Exception ex)
				{
				}
			}
			Marshal.FreeCoTaskMem(this.caArrayAddress);
			this.caArrayAddress = IntPtr.Zero;
			return (object[])array;
		}

		// Token: 0x040033F7 RID: 13303
		private static TraceSwitch CAMarshalSwitch = new TraceSwitch("CAMarshal", "BaseCAMarshaler: Debug CA* struct marshaling");

		// Token: 0x040033F8 RID: 13304
		private IntPtr caArrayAddress;

		// Token: 0x040033F9 RID: 13305
		private int count;

		// Token: 0x040033FA RID: 13306
		private object[] itemArray;
	}
}
