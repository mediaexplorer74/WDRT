using System;
using System.Drawing;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000498 RID: 1176
	internal class Com2FontConverter : Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool AllowExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x001431B9 File Offset: 0x001413B9
		public override Type ManagedType
		{
			get
			{
				return typeof(Font);
			}
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x001431C8 File Offset: 0x001413C8
		public override object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd)
		{
			UnsafeNativeMethods.IFont font = nativeValue as UnsafeNativeMethods.IFont;
			if (font == null)
			{
				this.lastHandle = IntPtr.Zero;
				this.lastFont = Control.DefaultFont;
				return this.lastFont;
			}
			IntPtr hfont = font.GetHFont();
			if (hfont == this.lastHandle && this.lastFont != null)
			{
				return this.lastFont;
			}
			this.lastHandle = hfont;
			try
			{
				Font font2 = Font.FromHfont(this.lastHandle);
				try
				{
					this.lastFont = ControlPaint.FontInPoints(font2);
				}
				finally
				{
					font2.Dispose();
				}
			}
			catch (ArgumentException)
			{
				this.lastFont = Control.DefaultFont;
			}
			return this.lastFont;
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0014327C File Offset: 0x0014147C
		public override object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet)
		{
			if (managedValue == null)
			{
				managedValue = Control.DefaultFont;
			}
			cancelSet = true;
			if (this.lastFont != null && this.lastFont.Equals(managedValue))
			{
				return null;
			}
			this.lastFont = (Font)managedValue;
			UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)pd.GetNativeValue(pd.TargetObject);
			if (font != null)
			{
				bool flag = ControlPaint.FontToIFont(this.lastFont, font);
				if (flag)
				{
					this.lastFont = null;
					this.ConvertNativeToManaged(font, pd);
				}
			}
			return null;
		}

		// Token: 0x04003404 RID: 13316
		private IntPtr lastHandle = IntPtr.Zero;

		// Token: 0x04003405 RID: 13317
		private Font lastFont;
	}
}
