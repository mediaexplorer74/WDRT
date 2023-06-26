using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B2 RID: 1202
	internal class ComNativeDescriptor : TypeDescriptionProvider
	{
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x001476EC File Offset: 0x001458EC
		internal static ComNativeDescriptor Instance
		{
			get
			{
				if (ComNativeDescriptor.handler == null)
				{
					ComNativeDescriptor.handler = new ComNativeDescriptor();
				}
				return ComNativeDescriptor.handler;
			}
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x00147704 File Offset: 0x00145904
		public static object GetNativePropertyValue(object component, string propertyName, ref bool succeeded)
		{
			return ComNativeDescriptor.Instance.GetPropertyValue(component, propertyName, ref succeeded);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00147713 File Offset: 0x00145913
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return new ComNativeDescriptor.ComTypeDescriptor(this, instance);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0014771C File Offset: 0x0014591C
		internal string GetClassName(object component)
		{
			string text = null;
			if (component is NativeMethods.IVsPerPropertyBrowsing)
			{
				int className = ((NativeMethods.IVsPerPropertyBrowsing)component).GetClassName(ref text);
				if (NativeMethods.Succeeded(className) && text != null)
				{
					return text;
				}
			}
			UnsafeNativeMethods.ITypeInfo typeInfo = Com2TypeInfoProcessor.FindTypeInfo(component, true);
			if (typeInfo == null)
			{
				return "";
			}
			if (typeInfo != null)
			{
				string text2 = null;
				try
				{
					typeInfo.GetDocumentation(-1, ref text, ref text2, null, null);
					while (text != null && text.Length > 0 && text[0] == '_')
					{
						text = text.Substring(1);
					}
					return text;
				}
				catch
				{
				}
			}
			return "";
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x001477B4 File Offset: 0x001459B4
		internal TypeConverter GetConverter(object component)
		{
			return TypeDescriptor.GetConverter(typeof(IComponent));
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x001477C5 File Offset: 0x001459C5
		internal object GetEditor(object component, Type baseEditorType)
		{
			return TypeDescriptor.GetEditor(component.GetType(), baseEditorType);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x001477D4 File Offset: 0x001459D4
		internal string GetName(object component)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return "";
			}
			int nameDispId = Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)component);
			if (nameDispId != -1)
			{
				bool flag = false;
				object propertyValue = this.GetPropertyValue(component, nameDispId, ref flag);
				if (flag && propertyValue != null)
				{
					return propertyValue.ToString();
				}
			}
			return "";
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00147820 File Offset: 0x00145A20
		internal object GetPropertyValue(object component, string propertyName, ref bool succeeded)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return null;
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)component;
			string[] array = new string[] { propertyName };
			int[] array2 = new int[] { -1 };
			Guid empty = Guid.Empty;
			try
			{
				int idsOfNames = dispatch.GetIDsOfNames(ref empty, array, 1, SafeNativeMethods.GetThreadLCID(), array2);
				if (array2[0] == -1 || NativeMethods.Failed(idsOfNames))
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
			return this.GetPropertyValue(component, array2[0], ref succeeded);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x001478A8 File Offset: 0x00145AA8
		internal object GetPropertyValue(object component, int dispid, ref bool succeeded)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return null;
			}
			object[] array = new object[1];
			if (this.GetPropertyValue(component, dispid, array) == 0)
			{
				succeeded = true;
				return array[0];
			}
			succeeded = false;
			return null;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x001478DC File Offset: 0x00145ADC
		internal int GetPropertyValue(object component, int dispid, object[] retval)
		{
			if (!(component is UnsafeNativeMethods.IDispatch))
			{
				return -2147467262;
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)component;
			try
			{
				Guid empty = Guid.Empty;
				NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
				int num;
				try
				{
					num = dispatch.Invoke(dispid, ref empty, SafeNativeMethods.GetThreadLCID(), 2, new NativeMethods.tagDISPPARAMS(), retval, tagEXCEPINFO, null);
					if (num == -2147352567)
					{
						num = tagEXCEPINFO.scode;
					}
				}
				catch (ExternalException ex)
				{
					num = ex.ErrorCode;
				}
				return num;
			}
			catch
			{
			}
			return -2147467259;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0014796C File Offset: 0x00145B6C
		internal bool IsNameDispId(object obj, int dispid)
		{
			return obj != null && obj.GetType().IsCOMObject && dispid == Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)obj);
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00147990 File Offset: 0x00145B90
		private void CheckClear(object component)
		{
			int num = this.clearCount + 1;
			this.clearCount = num;
			if (num % 25 == 0)
			{
				WeakHashtable weakHashtable = this.nativeProps;
				lock (weakHashtable)
				{
					this.clearCount = 0;
					List<object> list = null;
					foreach (object obj in this.nativeProps)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						Com2Properties com2Properties = dictionaryEntry.Value as Com2Properties;
						if (com2Properties != null && com2Properties.TooOld)
						{
							if (list == null)
							{
								list = new List<object>(3);
							}
							list.Add(dictionaryEntry.Key);
						}
					}
					if (list != null)
					{
						for (int i = list.Count - 1; i >= 0; i--)
						{
							object obj2 = list[i];
							Com2Properties com2Properties = this.nativeProps[obj2] as Com2Properties;
							if (com2Properties != null)
							{
								com2Properties.Disposed -= this.OnPropsInfoDisposed;
								com2Properties.Dispose();
								this.nativeProps.Remove(obj2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00147ACC File Offset: 0x00145CCC
		private Com2Properties GetPropsInfo(object component)
		{
			this.CheckClear(component);
			Com2Properties com2Properties = (Com2Properties)this.nativeProps[component];
			if (com2Properties == null || !com2Properties.CheckValid())
			{
				com2Properties = Com2TypeInfoProcessor.GetProperties(component);
				if (com2Properties != null)
				{
					com2Properties.Disposed += this.OnPropsInfoDisposed;
					this.nativeProps.SetWeak(component, com2Properties);
					com2Properties.AddExtendedBrowsingHandlers(this.extendedBrowsingHandlers);
				}
			}
			return com2Properties;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00147B34 File Offset: 0x00145D34
		internal AttributeCollection GetAttributes(object component)
		{
			ArrayList arrayList = new ArrayList();
			if (component is NativeMethods.IManagedPerPropertyBrowsing)
			{
				object[] componentAttributes = Com2IManagedPerPropertyBrowsingHandler.GetComponentAttributes((NativeMethods.IManagedPerPropertyBrowsing)component, -1);
				object[] array = componentAttributes;
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(array[i]);
				}
			}
			if (Com2ComponentEditor.NeedsComponentEditor(component))
			{
				EditorAttribute editorAttribute = new EditorAttribute(typeof(Com2ComponentEditor), typeof(ComponentEditor));
				arrayList.Add(editorAttribute);
			}
			if (arrayList == null || arrayList.Count == 0)
			{
				return this.staticAttrs;
			}
			Attribute[] array2 = new Attribute[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			return new AttributeCollection(array2);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00147BD4 File Offset: 0x00145DD4
		internal PropertyDescriptor GetDefaultProperty(object component)
		{
			this.CheckClear(component);
			Com2Properties propsInfo = this.GetPropsInfo(component);
			if (propsInfo != null)
			{
				return propsInfo.DefaultProperty;
			}
			return null;
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x00147BFB File Offset: 0x00145DFB
		internal EventDescriptorCollection GetEvents(object component)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00147BFB File Offset: 0x00145DFB
		internal EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00015C90 File Offset: 0x00013E90
		internal EventDescriptor GetDefaultEvent(object component)
		{
			return null;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00147C04 File Offset: 0x00145E04
		internal PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			Com2Properties propsInfo = this.GetPropsInfo(component);
			if (propsInfo == null)
			{
				return PropertyDescriptorCollection.Empty;
			}
			PropertyDescriptorCollection propertyDescriptorCollection;
			try
			{
				propsInfo.AlwaysValid = true;
				PropertyDescriptor[] properties = propsInfo.Properties;
				PropertyDescriptor[] array = properties;
				propertyDescriptorCollection = new PropertyDescriptorCollection(array);
			}
			finally
			{
				propsInfo.AlwaysValid = false;
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x00147C58 File Offset: 0x00145E58
		private void OnPropsInfoDisposed(object sender, EventArgs e)
		{
			Com2Properties com2Properties = sender as Com2Properties;
			if (com2Properties != null)
			{
				com2Properties.Disposed -= this.OnPropsInfoDisposed;
				WeakHashtable weakHashtable = this.nativeProps;
				lock (weakHashtable)
				{
					object obj = com2Properties.TargetObject;
					if (obj == null && this.nativeProps.ContainsValue(com2Properties))
					{
						foreach (object obj2 in this.nativeProps)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
							if (dictionaryEntry.Value == com2Properties)
							{
								obj = dictionaryEntry.Key;
								break;
							}
						}
						if (obj == null)
						{
							return;
						}
					}
					this.nativeProps.Remove(obj);
				}
			}
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00147D38 File Offset: 0x00145F38
		internal static void ResolveVariantTypeConverterAndTypeEditor(object propertyValue, ref TypeConverter currentConverter, Type editorType, ref object currentEditor)
		{
			if (propertyValue != null && propertyValue != null && !Convert.IsDBNull(propertyValue))
			{
				Type type = propertyValue.GetType();
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null && converter.GetType() != typeof(TypeConverter))
				{
					currentConverter = converter;
				}
				object editor = TypeDescriptor.GetEditor(type, editorType);
				if (editor != null)
				{
					currentEditor = editor;
				}
			}
		}

		// Token: 0x04003458 RID: 13400
		private static ComNativeDescriptor handler;

		// Token: 0x04003459 RID: 13401
		private AttributeCollection staticAttrs = new AttributeCollection(new Attribute[]
		{
			BrowsableAttribute.Yes,
			DesignTimeVisibleAttribute.No
		});

		// Token: 0x0400345A RID: 13402
		private WeakHashtable nativeProps = new WeakHashtable();

		// Token: 0x0400345B RID: 13403
		private Hashtable extendedBrowsingHandlers = new Hashtable();

		// Token: 0x0400345C RID: 13404
		private int clearCount;

		// Token: 0x0400345D RID: 13405
		private const int CLEAR_INTERVAL = 25;

		// Token: 0x02000856 RID: 2134
		private sealed class ComTypeDescriptor : ICustomTypeDescriptor
		{
			// Token: 0x06007083 RID: 28803 RVA: 0x0019BB5C File Offset: 0x00199D5C
			internal ComTypeDescriptor(ComNativeDescriptor handler, object instance)
			{
				this._handler = handler;
				this._instance = instance;
			}

			// Token: 0x06007084 RID: 28804 RVA: 0x0019BB72 File Offset: 0x00199D72
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				return this._handler.GetAttributes(this._instance);
			}

			// Token: 0x06007085 RID: 28805 RVA: 0x0019BB85 File Offset: 0x00199D85
			string ICustomTypeDescriptor.GetClassName()
			{
				return this._handler.GetClassName(this._instance);
			}

			// Token: 0x06007086 RID: 28806 RVA: 0x0019BB98 File Offset: 0x00199D98
			string ICustomTypeDescriptor.GetComponentName()
			{
				return this._handler.GetName(this._instance);
			}

			// Token: 0x06007087 RID: 28807 RVA: 0x0019BBAB File Offset: 0x00199DAB
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				return this._handler.GetConverter(this._instance);
			}

			// Token: 0x06007088 RID: 28808 RVA: 0x0019BBBE File Offset: 0x00199DBE
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				return this._handler.GetDefaultEvent(this._instance);
			}

			// Token: 0x06007089 RID: 28809 RVA: 0x0019BBD1 File Offset: 0x00199DD1
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				return this._handler.GetDefaultProperty(this._instance);
			}

			// Token: 0x0600708A RID: 28810 RVA: 0x0019BBE4 File Offset: 0x00199DE4
			object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
			{
				return this._handler.GetEditor(this._instance, editorBaseType);
			}

			// Token: 0x0600708B RID: 28811 RVA: 0x0019BBF8 File Offset: 0x00199DF8
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				return this._handler.GetEvents(this._instance);
			}

			// Token: 0x0600708C RID: 28812 RVA: 0x0019BC0B File Offset: 0x00199E0B
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
			{
				return this._handler.GetEvents(this._instance, attributes);
			}

			// Token: 0x0600708D RID: 28813 RVA: 0x0019BC1F File Offset: 0x00199E1F
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				return this._handler.GetProperties(this._instance, null);
			}

			// Token: 0x0600708E RID: 28814 RVA: 0x0019BC33 File Offset: 0x00199E33
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
			{
				return this._handler.GetProperties(this._instance, attributes);
			}

			// Token: 0x0600708F RID: 28815 RVA: 0x0019BC47 File Offset: 0x00199E47
			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
			{
				return this._instance;
			}

			// Token: 0x040043A2 RID: 17314
			private ComNativeDescriptor _handler;

			// Token: 0x040043A3 RID: 17315
			private object _instance;
		}
	}
}
