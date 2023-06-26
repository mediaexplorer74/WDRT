using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049D RID: 1181
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IProvidePropertyBuilderHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004EAB RID: 20139 RVA: 0x00143C90 File Offset: 0x00141E90
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IProvidePropertyBuilder);
			}
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00143C9C File Offset: 0x00141E9C
		private bool GetBuilderGuidString(NativeMethods.IProvidePropertyBuilder target, int dispid, ref string strGuidBldr, int[] bldrType)
		{
			bool flag = false;
			string[] array = new string[1];
			if (NativeMethods.Failed(target.MapPropertyToBuilder(dispid, bldrType, array, ref flag)))
			{
				flag = false;
			}
			if (flag && (bldrType[0] & 2) == 0)
			{
				flag = false;
			}
			if (!flag)
			{
				return false;
			}
			if (array[0] == null)
			{
				strGuidBldr = Guid.Empty.ToString();
			}
			else
			{
				strGuidBldr = array[0];
			}
			return true;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00143CFC File Offset: 0x00141EFC
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetBaseAttributes;
				propDesc[i].QueryGetTypeConverterAndTypeEditor += this.OnGetTypeConverterAndTypeEditor;
			}
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00143D44 File Offset: 0x00141F44
		private void OnGetBaseAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			NativeMethods.IProvidePropertyBuilder providePropertyBuilder = sender.TargetObject as NativeMethods.IProvidePropertyBuilder;
			if (providePropertyBuilder != null)
			{
				string text = null;
				bool builderGuidString = this.GetBuilderGuidString(providePropertyBuilder, sender.DISPID, ref text, new int[1]);
				if (sender.CanShow && builderGuidString && typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType))
				{
					attrEvent.Add(BrowsableAttribute.Yes);
				}
			}
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00143DA4 File Offset: 0x00141FA4
		private void OnGetTypeConverterAndTypeEditor(Com2PropertyDescriptor sender, GetTypeConverterAndTypeEditorEvent gveevent)
		{
			object targetObject = sender.TargetObject;
			if (targetObject is NativeMethods.IProvidePropertyBuilder)
			{
				NativeMethods.IProvidePropertyBuilder providePropertyBuilder = (NativeMethods.IProvidePropertyBuilder)targetObject;
				int[] array = new int[1];
				string text = null;
				if (this.GetBuilderGuidString(providePropertyBuilder, sender.DISPID, ref text, array))
				{
					gveevent.TypeEditor = new Com2PropertyBuilderUITypeEditor(sender, text, array[0], (UITypeEditor)gveevent.TypeEditor);
				}
			}
		}
	}
}
