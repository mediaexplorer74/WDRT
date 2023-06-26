using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000490 RID: 1168
	internal class Com2AboutBoxPropertyDescriptor : Com2PropertyDescriptor
	{
		// Token: 0x06004E4D RID: 20045 RVA: 0x001428BC File Offset: 0x00140ABC
		public Com2AboutBoxPropertyDescriptor()
			: base(-552, "About", new Attribute[]
			{
				new DispIdAttribute(-552),
				DesignerSerializationVisibilityAttribute.Hidden,
				new DescriptionAttribute(SR.GetString("AboutBoxDesc")),
				new ParenthesizePropertyNameAttribute(true)
			}, true, typeof(string), null, false)
		{
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x0014291C File Offset: 0x00140B1C
		public override Type ComponentType
		{
			get
			{
				return typeof(UnsafeNativeMethods.IDispatch);
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x00142928 File Offset: 0x00140B28
		public override TypeConverter Converter
		{
			get
			{
				if (this.converter == null)
				{
					this.converter = new TypeConverter();
				}
				return this.converter;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00142943 File Offset: 0x00140B43
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x0014294F File Offset: 0x00140B4F
		public override object GetEditor(Type editorBaseType)
		{
			if (editorBaseType == typeof(UITypeEditor) && this.editor == null)
			{
				this.editor = new Com2AboutBoxPropertyDescriptor.AboutBoxUITypeEditor();
			}
			return this.editor;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x000F17EC File Offset: 0x000EF9EC
		public override object GetValue(object component)
		{
			return "";
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x000070A6 File Offset: 0x000052A6
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0014297C File Offset: 0x00140B7C
		public override void SetValue(object component, object value)
		{
			throw new ArgumentException();
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x040033FB RID: 13307
		private TypeConverter converter;

		// Token: 0x040033FC RID: 13308
		private UITypeEditor editor;

		// Token: 0x0200084F RID: 2127
		public class AboutBoxUITypeEditor : UITypeEditor
		{
			// Token: 0x06007058 RID: 28760 RVA: 0x0019B490 File Offset: 0x00199690
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				object instance = context.Instance;
				if (Marshal.IsComObject(instance) && instance is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)instance;
					NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
					Guid empty = Guid.Empty;
					int num = dispatch.Invoke(-552, ref empty, SafeNativeMethods.GetThreadLCID(), 1, new NativeMethods.tagDISPPARAMS(), null, tagEXCEPINFO, null);
				}
				return value;
			}

			// Token: 0x06007059 RID: 28761 RVA: 0x00016041 File Offset: 0x00014241
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}
		}
	}
}
