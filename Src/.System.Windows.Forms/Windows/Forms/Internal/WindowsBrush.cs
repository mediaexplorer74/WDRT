using System;
using System.Drawing;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E3 RID: 1251
	internal abstract class WindowsBrush : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x060051B3 RID: 20915
		public abstract object Clone();

		// Token: 0x060051B4 RID: 20916
		protected abstract void CreateBrush();

		// Token: 0x060051B5 RID: 20917 RVA: 0x0015385E File Offset: 0x00151A5E
		public WindowsBrush(DeviceContext dc)
		{
			this.dc = dc;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00153878 File Offset: 0x00151A78
		public WindowsBrush(DeviceContext dc, Color color)
		{
			this.dc = dc;
			this.color = color;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0015389C File Offset: 0x00151A9C
		~WindowsBrush()
		{
			this.Dispose(false);
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x001538CC File Offset: 0x00151ACC
		protected DeviceContext DC
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x001538D4 File Offset: 0x00151AD4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x001538E0 File Offset: 0x00151AE0
		protected virtual void Dispose(bool disposing)
		{
			if (this.dc != null && this.nativeHandle != IntPtr.Zero)
			{
				this.dc.DeleteObject(this.nativeHandle, GdiObjectType.Brush);
				this.nativeHandle = IntPtr.Zero;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x0015392D File Offset: 0x00151B2D
		public Color Color
		{
			get
			{
				return this.color;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x00153935 File Offset: 0x00151B35
		// (set) Token: 0x060051BD RID: 20925 RVA: 0x00153955 File Offset: 0x00151B55
		protected IntPtr NativeHandle
		{
			get
			{
				if (this.nativeHandle == IntPtr.Zero)
				{
					this.CreateBrush();
				}
				return this.nativeHandle;
			}
			set
			{
				this.nativeHandle = value;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0015395E File Offset: 0x00151B5E
		public IntPtr HBrush
		{
			get
			{
				return this.NativeHandle;
			}
		}

		// Token: 0x040035CC RID: 13772
		private DeviceContext dc;

		// Token: 0x040035CD RID: 13773
		private IntPtr nativeHandle;

		// Token: 0x040035CE RID: 13774
		private Color color = Color.White;
	}
}
