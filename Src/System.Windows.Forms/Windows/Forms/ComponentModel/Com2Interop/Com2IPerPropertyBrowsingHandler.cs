using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049C RID: 1180
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IPerPropertyBrowsingHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x001439F4 File Offset: 0x00141BF4
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IPerPropertyBrowsing);
			}
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00143A00 File Offset: 0x00141C00
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetBaseAttributes;
				propDesc[i].QueryGetDisplayValue += this.OnGetDisplayValue;
				propDesc[i].QueryGetTypeConverterAndTypeEditor += this.OnGetTypeConverterAndTypeEditor;
			}
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00143A5C File Offset: 0x00141C5C
		private Guid GetPropertyPageGuid(NativeMethods.IPerPropertyBrowsing target, int dispid)
		{
			Guid guid;
			if (target.MapPropertyToPage(dispid, out guid) == 0)
			{
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00143A80 File Offset: 0x00141C80
		internal static string GetDisplayString(NativeMethods.IPerPropertyBrowsing ppb, int dispid, ref bool success)
		{
			string[] array = new string[1];
			if (ppb.GetDisplayString(dispid, array) == 0)
			{
				success = array[0] != null;
				return array[0];
			}
			success = false;
			return null;
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00143AB0 File Offset: 0x00141CB0
		private void OnGetBaseAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = sender.TargetObject as NativeMethods.IPerPropertyBrowsing;
			if (perPropertyBrowsing != null)
			{
				bool flag = !Guid.Empty.Equals(this.GetPropertyPageGuid(perPropertyBrowsing, sender.DISPID));
				if (sender.CanShow && flag && typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType))
				{
					attrEvent.Add(BrowsableAttribute.Yes);
				}
			}
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00143B18 File Offset: 0x00141D18
		private void OnGetDisplayValue(Com2PropertyDescriptor sender, GetNameItemEvent gnievent)
		{
			try
			{
				if (sender.TargetObject is NativeMethods.IPerPropertyBrowsing)
				{
					if (!(sender.Converter is Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter) && !sender.ConvertingNativeType)
					{
						bool flag = true;
						string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)sender.TargetObject, sender.DISPID, ref flag);
						if (flag)
						{
							gnievent.Name = displayString;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00143B84 File Offset: 0x00141D84
		private void OnGetTypeConverterAndTypeEditor(Com2PropertyDescriptor sender, GetTypeConverterAndTypeEditorEvent gveevent)
		{
			if (sender.TargetObject is NativeMethods.IPerPropertyBrowsing)
			{
				NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)sender.TargetObject;
				NativeMethods.CA_STRUCT ca_STRUCT = new NativeMethods.CA_STRUCT();
				NativeMethods.CA_STRUCT ca_STRUCT2 = new NativeMethods.CA_STRUCT();
				int num = 0;
				try
				{
					num = perPropertyBrowsing.GetPredefinedStrings(sender.DISPID, ca_STRUCT, ca_STRUCT2);
				}
				catch (ExternalException ex)
				{
					num = ex.ErrorCode;
				}
				if (gveevent.TypeConverter is Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter)
				{
					gveevent.TypeConverter = null;
				}
				bool flag = num == 0;
				if (flag)
				{
					OleStrCAMarshaler oleStrCAMarshaler = new OleStrCAMarshaler(ca_STRUCT);
					Int32CAMarshaler int32CAMarshaler = new Int32CAMarshaler(ca_STRUCT2);
					if (oleStrCAMarshaler.Count > 0 && int32CAMarshaler.Count > 0)
					{
						gveevent.TypeConverter = new Com2IPerPropertyBrowsingHandler.Com2IPerPropertyEnumConverter(new Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum(sender, this, oleStrCAMarshaler, int32CAMarshaler, true));
					}
				}
				if (!flag)
				{
					if (sender.ConvertingNativeType)
					{
						return;
					}
					Guid propertyPageGuid = this.GetPropertyPageGuid(perPropertyBrowsing, sender.DISPID);
					if (!Guid.Empty.Equals(propertyPageGuid))
					{
						gveevent.TypeEditor = new Com2PropertyPageUITypeEditor(sender, propertyPageGuid, (UITypeEditor)gveevent.TypeEditor);
					}
				}
			}
		}

		// Token: 0x02000850 RID: 2128
		private class Com2IPerPropertyEnumConverter : Com2EnumConverter
		{
			// Token: 0x0600705B RID: 28763 RVA: 0x0019B4ED File Offset: 0x001996ED
			public Com2IPerPropertyEnumConverter(Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum items)
				: base(items)
			{
				this.itemsEnum = items;
			}

			// Token: 0x0600705C RID: 28764 RVA: 0x0019B500 File Offset: 0x00199700
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
			{
				if (destType == typeof(string) && !this.itemsEnum.arraysFetched)
				{
					object value2 = this.itemsEnum.target.GetValue(this.itemsEnum.target.TargetObject);
					if (value2 == value || (value2 != null && value2.Equals(value)))
					{
						bool flag = false;
						string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)this.itemsEnum.target.TargetObject, this.itemsEnum.target.DISPID, ref flag);
						if (flag)
						{
							return displayString;
						}
					}
				}
				return base.ConvertTo(context, culture, value, destType);
			}

			// Token: 0x04004386 RID: 17286
			private Com2IPerPropertyBrowsingHandler.Com2IPerPropertyBrowsingEnum itemsEnum;
		}

		// Token: 0x02000851 RID: 2129
		private class Com2IPerPropertyBrowsingEnum : Com2Enum
		{
			// Token: 0x0600705D RID: 28765 RVA: 0x0019B59D File Offset: 0x0019979D
			public Com2IPerPropertyBrowsingEnum(Com2PropertyDescriptor targetObject, Com2IPerPropertyBrowsingHandler handler, OleStrCAMarshaler names, Int32CAMarshaler values, bool allowUnknowns)
				: base(new string[0], new object[0], allowUnknowns)
			{
				this.target = targetObject;
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.handler = handler;
				this.arraysFetched = false;
			}

			// Token: 0x17001885 RID: 6277
			// (get) Token: 0x0600705E RID: 28766 RVA: 0x0019B5D7 File Offset: 0x001997D7
			public override object[] Values
			{
				get
				{
					this.EnsureArrays();
					return base.Values;
				}
			}

			// Token: 0x17001886 RID: 6278
			// (get) Token: 0x0600705F RID: 28767 RVA: 0x0019B5E5 File Offset: 0x001997E5
			public override string[] Names
			{
				get
				{
					this.EnsureArrays();
					return base.Names;
				}
			}

			// Token: 0x06007060 RID: 28768 RVA: 0x0019B5F4 File Offset: 0x001997F4
			private void EnsureArrays()
			{
				if (this.arraysFetched)
				{
					return;
				}
				this.arraysFetched = true;
				try
				{
					object[] items = this.nameMarshaller.Items;
					object[] items2 = this.valueMarshaller.Items;
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)this.target.TargetObject;
					int num = 0;
					if (items.Length != 0)
					{
						object[] array = new object[items2.Length];
						NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
						Type propertyType = this.target.PropertyType;
						for (int i = items.Length - 1; i >= 0; i--)
						{
							int num2 = (int)items2[i];
							if (items[i] != null && items[i] is string)
							{
								variant.vt = 0;
								int predefinedValue = perPropertyBrowsing.GetPredefinedValue(this.target.DISPID, num2, variant);
								if (predefinedValue == 0 && variant.vt != 0)
								{
									array[i] = variant.ToObject();
									if (array[i].GetType() != propertyType)
									{
										if (propertyType.IsEnum)
										{
											array[i] = Enum.ToObject(propertyType, array[i]);
										}
										else
										{
											try
											{
												array[i] = Convert.ChangeType(array[i], propertyType, CultureInfo.InvariantCulture);
											}
											catch
											{
											}
										}
									}
								}
								variant.Clear();
								if (predefinedValue == 0)
								{
									num++;
								}
								else if (num > 0)
								{
									Array.Copy(items, i, items, i + 1, num);
									Array.Copy(array, i, array, i + 1, num);
								}
							}
						}
						string[] array2 = new string[num];
						Array.Copy(items, 0, array2, 0, num);
						base.PopulateArrays(array2, array);
					}
				}
				catch (Exception ex)
				{
					base.PopulateArrays(new string[0], new object[0]);
				}
			}

			// Token: 0x06007061 RID: 28769 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void PopulateArrays(string[] names, object[] values)
			{
			}

			// Token: 0x06007062 RID: 28770 RVA: 0x0019B7BC File Offset: 0x001999BC
			public override object FromString(string s)
			{
				this.EnsureArrays();
				return base.FromString(s);
			}

			// Token: 0x06007063 RID: 28771 RVA: 0x0019B7CC File Offset: 0x001999CC
			public override string ToString(object v)
			{
				if (this.target.IsCurrentValue(v))
				{
					bool flag = false;
					string displayString = Com2IPerPropertyBrowsingHandler.GetDisplayString((NativeMethods.IPerPropertyBrowsing)this.target.TargetObject, this.target.DISPID, ref flag);
					if (flag)
					{
						return displayString;
					}
				}
				this.EnsureArrays();
				return base.ToString(v);
			}

			// Token: 0x04004387 RID: 17287
			internal Com2PropertyDescriptor target;

			// Token: 0x04004388 RID: 17288
			private Com2IPerPropertyBrowsingHandler handler;

			// Token: 0x04004389 RID: 17289
			private OleStrCAMarshaler nameMarshaller;

			// Token: 0x0400438A RID: 17290
			private Int32CAMarshaler valueMarshaller;

			// Token: 0x0400438B RID: 17291
			internal bool arraysFetched;
		}
	}
}
