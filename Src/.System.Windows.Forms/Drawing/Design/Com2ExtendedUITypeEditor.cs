using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	// Token: 0x020000FD RID: 253
	internal class Com2ExtendedUITypeEditor : UITypeEditor
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public Com2ExtendedUITypeEditor(UITypeEditor baseTypeEditor)
		{
			this.innerEditor = baseTypeEditor;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
		public Com2ExtendedUITypeEditor(Type baseType)
		{
			this.innerEditor = (UITypeEditor)TypeDescriptor.GetEditor(baseType, typeof(UITypeEditor));
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		public UITypeEditor InnerEditor
		{
			get
			{
				return this.innerEditor;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.EditValue(context, provider, value);
			}
			return base.EditValue(context, provider, value);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.GetPaintValueSupported(context);
			}
			return base.GetPaintValueSupported(context);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000CC32 File Offset: 0x0000AE32
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.GetEditStyle(context);
			}
			return base.GetEditStyle(context);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public override void PaintValue(PaintValueEventArgs e)
		{
			if (this.innerEditor != null)
			{
				this.innerEditor.PaintValue(e);
			}
			base.PaintValue(e);
		}

		// Token: 0x04000432 RID: 1074
		private UITypeEditor innerEditor;
	}
}
