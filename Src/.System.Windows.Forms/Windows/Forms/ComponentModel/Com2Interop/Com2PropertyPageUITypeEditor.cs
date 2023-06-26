using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AE RID: 1198
	internal class Com2PropertyPageUITypeEditor : Com2ExtendedUITypeEditor, ICom2PropertyPageDisplayService
	{
		// Token: 0x06004F45 RID: 20293 RVA: 0x001460D6 File Offset: 0x001442D6
		public Com2PropertyPageUITypeEditor(Com2PropertyDescriptor pd, Guid guid, UITypeEditor baseEditor)
			: base(baseEditor)
		{
			this.propDesc = pd;
			this.guid = guid;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x001460F0 File Offset: 0x001442F0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			try
			{
				ICom2PropertyPageDisplayService com2PropertyPageDisplayService = (ICom2PropertyPageDisplayService)provider.GetService(typeof(ICom2PropertyPageDisplayService));
				if (com2PropertyPageDisplayService == null)
				{
					com2PropertyPageDisplayService = this;
				}
				object obj = context.Instance;
				if (!obj.GetType().IsArray)
				{
					obj = this.propDesc.TargetObject;
					if (obj is ICustomTypeDescriptor)
					{
						obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.propDesc);
					}
				}
				com2PropertyPageDisplayService.ShowPropertyPage(this.propDesc.Name, obj, this.propDesc.DISPID, this.guid, focus);
			}
			catch (Exception ex)
			{
				if (provider != null)
				{
					IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
					if (iuiservice != null)
					{
						iuiservice.ShowError(ex, SR.GetString("ErrorTypeConverterFailed"));
					}
				}
			}
			return value;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00016041 File Offset: 0x00014241
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x001461C4 File Offset: 0x001443C4
		public unsafe void ShowPropertyPage(string title, object component, int dispid, Guid pageGuid, IntPtr parentHandle)
		{
			Guid[] array = new Guid[] { pageGuid };
			IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
			object[] array2;
			if (!component.GetType().IsArray)
			{
				(array2 = new object[1])[0] = component;
			}
			else
			{
				array2 = (object[])component;
			}
			object[] array3 = array2;
			int num = array3.Length;
			IntPtr[] array4 = new IntPtr[num];
			try
			{
				for (int i = 0; i < num; i++)
				{
					array4[i] = Marshal.GetIUnknownForObject(array3[i]);
				}
				try
				{
					IntPtr[] array5;
					IntPtr* ptr;
					if ((array5 = array4) == null || array5.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array5[0];
					}
					SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(null, parentHandle), 0, 0, title, num, new HandleRef(null, (IntPtr)ptr), 1, new HandleRef(null, intPtr), SafeNativeMethods.GetThreadLCID(), 0, IntPtr.Zero);
				}
				finally
				{
					IntPtr[] array5 = null;
				}
			}
			finally
			{
				for (int j = 0; j < num; j++)
				{
					if (array4[j] != IntPtr.Zero)
					{
						Marshal.Release(array4[j]);
					}
				}
			}
		}

		// Token: 0x0400344C RID: 13388
		private Com2PropertyDescriptor propDesc;

		// Token: 0x0400344D RID: 13389
		private Guid guid;
	}
}
