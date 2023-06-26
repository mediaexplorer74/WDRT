using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A1 RID: 1185
	internal class Com2PropertyBuilderUITypeEditor : Com2ExtendedUITypeEditor
	{
		// Token: 0x06004ED6 RID: 20182 RVA: 0x00144A18 File Offset: 0x00142C18
		public Com2PropertyBuilderUITypeEditor(Com2PropertyDescriptor pd, string guidString, int type, UITypeEditor baseEditor)
			: base(baseEditor)
		{
			this.propDesc = pd;
			this.guidString = guidString;
			this.bldrType = type;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00144A38 File Offset: 0x00142C38
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IntPtr intPtr = UnsafeNativeMethods.GetFocus();
			IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				IWin32Window dialogOwnerWindow = iuiservice.GetDialogOwnerWindow();
				if (dialogOwnerWindow != null)
				{
					intPtr = dialogOwnerWindow.Handle;
				}
			}
			bool flag = false;
			object obj = value;
			try
			{
				object obj2 = this.propDesc.TargetObject;
				if (obj2 is ICustomTypeDescriptor)
				{
					obj2 = ((ICustomTypeDescriptor)obj2).GetPropertyOwner(this.propDesc);
				}
				NativeMethods.IProvidePropertyBuilder providePropertyBuilder = (NativeMethods.IProvidePropertyBuilder)obj2;
				if (NativeMethods.Failed(providePropertyBuilder.ExecuteBuilder(this.propDesc.DISPID, this.guidString, null, new HandleRef(null, intPtr), ref obj, ref flag)))
				{
					flag = false;
				}
			}
			catch (ExternalException ex)
			{
			}
			if (flag && (this.bldrType & 4) == 0)
			{
				return obj;
			}
			return value;
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00016041 File Offset: 0x00014241
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x0400341B RID: 13339
		private Com2PropertyDescriptor propDesc;

		// Token: 0x0400341C RID: 13340
		private string guidString;

		// Token: 0x0400341D RID: 13341
		private int bldrType;
	}
}
