using System;
using System.Drawing;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049F RID: 1183
	internal class Com2PictureConverter : Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x06004EBD RID: 20157 RVA: 0x00144230 File Offset: 0x00142430
		public Com2PictureConverter(Com2PropertyDescriptor pd)
		{
			if (pd.DISPID == -522 || pd.Name.IndexOf("Icon") != -1)
			{
				this.pictureType = typeof(Icon);
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x0014428E File Offset: 0x0014248E
		public override Type ManagedType
		{
			get
			{
				return this.pictureType;
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00144298 File Offset: 0x00142498
		public override object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd)
		{
			if (nativeValue == null)
			{
				return null;
			}
			UnsafeNativeMethods.IPicture picture = (UnsafeNativeMethods.IPicture)nativeValue;
			IntPtr handle = picture.GetHandle();
			if (this.lastManaged != null && handle == this.lastNativeHandle)
			{
				return this.lastManaged;
			}
			this.lastNativeHandle = handle;
			if (handle != IntPtr.Zero)
			{
				short num = picture.GetPictureType();
				if (num != 1)
				{
					if (num == 3)
					{
						this.pictureType = typeof(Icon);
						this.lastManaged = Icon.FromHandle(handle);
					}
				}
				else
				{
					this.pictureType = typeof(Bitmap);
					this.lastManaged = Image.FromHbitmap(handle);
				}
				this.pictureRef = new WeakReference(picture);
			}
			else
			{
				this.lastManaged = null;
				this.pictureRef = null;
			}
			return this.lastManaged;
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00144354 File Offset: 0x00142554
		public override object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet)
		{
			cancelSet = false;
			if (this.lastManaged != null && this.lastManaged.Equals(managedValue) && this.pictureRef != null && this.pictureRef.IsAlive)
			{
				return this.pictureRef.Target;
			}
			this.lastManaged = managedValue;
			if (managedValue != null)
			{
				Guid guid = typeof(UnsafeNativeMethods.IPicture).GUID;
				NativeMethods.PICTDESC pictdesc = null;
				bool flag = false;
				if (this.lastManaged is Icon)
				{
					pictdesc = NativeMethods.PICTDESC.CreateIconPICTDESC(((Icon)this.lastManaged).Handle);
				}
				else if (this.lastManaged is Bitmap)
				{
					pictdesc = NativeMethods.PICTDESC.CreateBitmapPICTDESC(((Bitmap)this.lastManaged).GetHbitmap(), this.lastPalette);
					flag = true;
				}
				UnsafeNativeMethods.IPicture picture = UnsafeNativeMethods.OleCreatePictureIndirect(pictdesc, ref guid, flag);
				this.lastNativeHandle = picture.GetHandle();
				this.pictureRef = new WeakReference(picture);
				return picture;
			}
			this.lastManaged = null;
			this.lastNativeHandle = (this.lastPalette = IntPtr.Zero);
			this.pictureRef = null;
			return null;
		}

		// Token: 0x04003409 RID: 13321
		private object lastManaged;

		// Token: 0x0400340A RID: 13322
		private IntPtr lastNativeHandle;

		// Token: 0x0400340B RID: 13323
		private WeakReference pictureRef;

		// Token: 0x0400340C RID: 13324
		private IntPtr lastPalette = IntPtr.Zero;

		// Token: 0x0400340D RID: 13325
		private Type pictureType = typeof(Bitmap);
	}
}
