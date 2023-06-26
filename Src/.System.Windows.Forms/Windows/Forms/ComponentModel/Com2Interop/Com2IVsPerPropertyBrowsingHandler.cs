using System;
using System.ComponentModel;
using System.Globalization;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049E RID: 1182
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IVsPerPropertyBrowsingHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x00143DFD File Offset: 0x00141FFD
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IVsPerPropertyBrowsing);
			}
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00143E0C File Offset: 0x0014200C
		public static bool AllowChildProperties(Com2PropertyDescriptor propDesc)
		{
			if (propDesc.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				bool flag = false;
				int num = ((NativeMethods.IVsPerPropertyBrowsing)propDesc.TargetObject).DisplayChildProperties(propDesc.DISPID, ref flag);
				return num == 0 && flag;
			}
			return false;
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00143E4C File Offset: 0x0014204C
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetDynamicAttributes += this.OnGetDynamicAttributes;
				propDesc[i].QueryGetBaseAttributes += this.OnGetBaseAttributes;
				propDesc[i].QueryGetDisplayName += this.OnGetDisplayName;
				propDesc[i].QueryGetIsReadOnly += this.OnGetIsReadOnly;
				propDesc[i].QueryShouldSerializeValue += this.OnShouldSerializeValue;
				propDesc[i].QueryCanResetValue += this.OnCanResetPropertyValue;
				propDesc[i].QueryResetValue += this.OnResetPropertyValue;
				propDesc[i].QueryGetTypeConverterAndTypeEditor += this.OnGetTypeConverterAndTypeEditor;
			}
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00143F14 File Offset: 0x00142114
		private void OnGetBaseAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = sender.TargetObject as NativeMethods.IVsPerPropertyBrowsing;
			if (vsPerPropertyBrowsing == null)
			{
				return;
			}
			string[] array = new string[1];
			if (vsPerPropertyBrowsing.GetLocalizedPropertyInfo(sender.DISPID, CultureInfo.CurrentCulture.LCID, null, array) == 0 && array[0] != null)
			{
				attrEvent.Add(new DescriptionAttribute(array[0]));
			}
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x00143F68 File Offset: 0x00142168
		private void OnGetDynamicAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				if (sender.CanShow)
				{
					bool flag = sender.Attributes[typeof(BrowsableAttribute)].Equals(BrowsableAttribute.No);
					if (vsPerPropertyBrowsing.HideProperty(sender.DISPID, ref flag) == 0)
					{
						attrEvent.Add(flag ? BrowsableAttribute.No : BrowsableAttribute.Yes);
					}
				}
				if (typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType) && sender.CanShow)
				{
					bool flag2 = false;
					int num = vsPerPropertyBrowsing.DisplayChildProperties(sender.DISPID, ref flag2);
					if (num == 0 && flag2)
					{
						attrEvent.Add(BrowsableAttribute.Yes);
					}
				}
			}
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x00144028 File Offset: 0x00142228
		private void OnCanResetPropertyValue(Com2PropertyDescriptor sender, GetBoolValueEvent boolEvent)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				bool value = boolEvent.Value;
				int num = vsPerPropertyBrowsing.CanResetPropertyValue(sender.DISPID, ref value);
				if (NativeMethods.Succeeded(num))
				{
					boolEvent.Value = value;
				}
			}
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00144074 File Offset: 0x00142274
		private void OnGetDisplayName(Com2PropertyDescriptor sender, GetNameItemEvent nameItem)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				string[] array = new string[1];
				if (vsPerPropertyBrowsing.GetLocalizedPropertyInfo(sender.DISPID, CultureInfo.CurrentCulture.LCID, array, null) == 0 && array[0] != null)
				{
					nameItem.Name = array[0];
				}
			}
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x001440CC File Offset: 0x001422CC
		private void OnGetIsReadOnly(Com2PropertyDescriptor sender, GetBoolValueEvent gbvevent)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				bool flag = false;
				if (vsPerPropertyBrowsing.IsPropertyReadOnly(sender.DISPID, ref flag) == 0)
				{
					gbvevent.Value = flag;
				}
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00144110 File Offset: 0x00142310
		private void OnGetTypeConverterAndTypeEditor(Com2PropertyDescriptor sender, GetTypeConverterAndTypeEditorEvent gveevent)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing && sender.CanShow && typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType))
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				bool flag = false;
				int num = vsPerPropertyBrowsing.DisplayChildProperties(sender.DISPID, ref flag);
				if (gveevent.TypeConverter is Com2IDispatchConverter)
				{
					gveevent.TypeConverter = new Com2IDispatchConverter(sender, num == 0 && flag);
					return;
				}
				gveevent.TypeConverter = new Com2IDispatchConverter(sender, num == 0 && flag, gveevent.TypeConverter);
			}
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x001441A0 File Offset: 0x001423A0
		private void OnResetPropertyValue(Com2PropertyDescriptor sender, EventArgs e)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				int dispid = sender.DISPID;
				bool flag = false;
				int num = vsPerPropertyBrowsing.CanResetPropertyValue(dispid, ref flag);
				if (NativeMethods.Succeeded(num))
				{
					vsPerPropertyBrowsing.ResetPropertyValue(dispid);
				}
			}
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x001441EC File Offset: 0x001423EC
		private void OnShouldSerializeValue(Com2PropertyDescriptor sender, GetBoolValueEvent gbvevent)
		{
			if (sender.TargetObject is NativeMethods.IVsPerPropertyBrowsing)
			{
				NativeMethods.IVsPerPropertyBrowsing vsPerPropertyBrowsing = (NativeMethods.IVsPerPropertyBrowsing)sender.TargetObject;
				bool flag = true;
				if (vsPerPropertyBrowsing.HasDefaultValue(sender.DISPID, ref flag) == 0 && !flag)
				{
					gbvevent.Value = true;
				}
			}
		}
	}
}
