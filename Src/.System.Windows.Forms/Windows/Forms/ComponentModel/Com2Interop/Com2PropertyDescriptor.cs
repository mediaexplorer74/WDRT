using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A2 RID: 1186
	internal class Com2PropertyDescriptor : PropertyDescriptor, ICloneable
	{
		// Token: 0x06004ED9 RID: 20185 RVA: 0x00144B04 File Offset: 0x00142D04
		static Com2PropertyDescriptor()
		{
			Com2PropertyDescriptor.oleConverters[Com2PropertyDescriptor.GUID_COLOR] = typeof(Com2ColorConverter);
			Com2PropertyDescriptor.oleConverters[typeof(SafeNativeMethods.IFontDisp).GUID] = typeof(Com2FontConverter);
			Com2PropertyDescriptor.oleConverters[typeof(UnsafeNativeMethods.IFont).GUID] = typeof(Com2FontConverter);
			Com2PropertyDescriptor.oleConverters[typeof(UnsafeNativeMethods.IPictureDisp).GUID] = typeof(Com2PictureConverter);
			Com2PropertyDescriptor.oleConverters[typeof(UnsafeNativeMethods.IPicture).GUID] = typeof(Com2PictureConverter);
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00144C4C File Offset: 0x00142E4C
		public Com2PropertyDescriptor(int dispid, string name, Attribute[] attrs, bool readOnly, Type propType, object typeData, bool hrHidden)
			: base(name, attrs)
		{
			this.baseReadOnly = readOnly;
			this.readOnly = readOnly;
			this.baseAttrs = attrs;
			this.SetNeedsRefresh(32768, true);
			this.hrHidden = hrHidden;
			this.SetNeedsRefresh(4, readOnly);
			this.propertyType = propType;
			this.dispid = dispid;
			if (typeData != null)
			{
				this.typeData = typeData;
				if (typeData is Com2Enum)
				{
					this.converter = new Com2EnumConverter((Com2Enum)typeData);
				}
				else if (typeData is Guid)
				{
					this.valueConverter = this.CreateOleTypeConverter((Type)Com2PropertyDescriptor.oleConverters[(Guid)typeData]);
				}
			}
			this.canShow = true;
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					if (attrs[i].Equals(BrowsableAttribute.No) && !hrHidden)
					{
						this.canShow = false;
						break;
					}
				}
			}
			if (this.canShow && (propType == typeof(object) || (this.valueConverter == null && propType == typeof(UnsafeNativeMethods.IDispatch))))
			{
				this.typeHide = true;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00144D70 File Offset: 0x00142F70
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x00144E0E File Offset: 0x0014300E
		protected Attribute[] BaseAttributes
		{
			get
			{
				if (this.GetNeedsRefresh(32768))
				{
					this.SetNeedsRefresh(32768, false);
					int num = ((this.baseAttrs == null) ? 0 : this.baseAttrs.Length);
					ArrayList arrayList = new ArrayList();
					if (num != 0)
					{
						arrayList.AddRange(this.baseAttrs);
					}
					this.OnGetBaseAttributes(new GetAttributesEvent(arrayList));
					if (arrayList.Count != num)
					{
						this.baseAttrs = new Attribute[arrayList.Count];
					}
					if (this.baseAttrs != null)
					{
						arrayList.CopyTo(this.baseAttrs, 0);
					}
					else
					{
						this.baseAttrs = new Attribute[0];
					}
				}
				return this.baseAttrs;
			}
			set
			{
				this.baseAttrs = value;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x00144E18 File Offset: 0x00143018
		public override AttributeCollection Attributes
		{
			get
			{
				if (this.AttributesValid || this.InAttrQuery)
				{
					return base.Attributes;
				}
				this.AttributeArray = this.BaseAttributes;
				ArrayList arrayList = null;
				if (this.typeHide && this.canShow)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList(this.AttributeArray);
					}
					arrayList.Add(new BrowsableAttribute(false));
				}
				else if (this.hrHidden)
				{
					object targetObject = this.TargetObject;
					if (targetObject != null)
					{
						int propertyValue = new ComNativeDescriptor().GetPropertyValue(targetObject, this.dispid, new object[1]);
						if (NativeMethods.Succeeded(propertyValue))
						{
							if (arrayList == null)
							{
								arrayList = new ArrayList(this.AttributeArray);
							}
							arrayList.Add(new BrowsableAttribute(true));
							this.hrHidden = false;
						}
					}
				}
				this.inAttrQuery = true;
				try
				{
					ArrayList arrayList2 = new ArrayList();
					this.OnGetDynamicAttributes(new GetAttributesEvent(arrayList2));
					if (arrayList2.Count != 0 && arrayList == null)
					{
						arrayList = new ArrayList(this.AttributeArray);
					}
					for (int i = 0; i < arrayList2.Count; i++)
					{
						Attribute attribute = (Attribute)arrayList2[i];
						arrayList.Add(attribute);
					}
				}
				finally
				{
					this.inAttrQuery = false;
				}
				this.SetNeedsRefresh(1, false);
				if (arrayList != null)
				{
					Attribute[] array = new Attribute[arrayList.Count];
					arrayList.CopyTo(array, 0);
					this.AttributeArray = array;
				}
				return base.Attributes;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x00144F74 File Offset: 0x00143174
		protected bool AttributesValid
		{
			get
			{
				bool flag = !this.GetNeedsRefresh(1);
				if (this.queryRefresh)
				{
					GetRefreshStateEvent getRefreshStateEvent = new GetRefreshStateEvent(Com2ShouldRefreshTypes.Attributes, !flag);
					this.OnShouldRefresh(getRefreshStateEvent);
					flag = !getRefreshStateEvent.Value;
					this.SetNeedsRefresh(1, getRefreshStateEvent.Value);
				}
				return flag;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x00144FBE File Offset: 0x001431BE
		public bool CanShow
		{
			get
			{
				return this.canShow;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x0014291C File Offset: 0x00140B1C
		public override Type ComponentType
		{
			get
			{
				return typeof(UnsafeNativeMethods.IDispatch);
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x00144FC8 File Offset: 0x001431C8
		public override TypeConverter Converter
		{
			get
			{
				if (this.TypeConverterValid)
				{
					return this.converter;
				}
				object obj = null;
				this.GetTypeConverterAndTypeEditor(ref this.converter, typeof(UITypeEditor), ref obj);
				if (!this.TypeEditorValid)
				{
					this.editor = obj;
					this.SetNeedsRefresh(64, false);
				}
				this.SetNeedsRefresh(32, false);
				return this.converter;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x00145025 File Offset: 0x00143225
		public bool ConvertingNativeType
		{
			get
			{
				return this.valueConverter != null;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00015C90 File Offset: 0x00013E90
		protected virtual object DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x00145030 File Offset: 0x00143230
		public int DISPID
		{
			get
			{
				return this.dispid;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x00145038 File Offset: 0x00143238
		public override string DisplayName
		{
			get
			{
				if (!this.DisplayNameValid)
				{
					GetNameItemEvent getNameItemEvent = new GetNameItemEvent(base.DisplayName);
					this.OnGetDisplayName(getNameItemEvent);
					this.displayName = getNameItemEvent.NameString;
					this.SetNeedsRefresh(2, false);
				}
				return this.displayName;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x0014507C File Offset: 0x0014327C
		protected bool DisplayNameValid
		{
			get
			{
				bool flag = this.displayName != null && !this.GetNeedsRefresh(2);
				if (this.queryRefresh)
				{
					GetRefreshStateEvent getRefreshStateEvent = new GetRefreshStateEvent(Com2ShouldRefreshTypes.DisplayName, !flag);
					this.OnShouldRefresh(getRefreshStateEvent);
					this.SetNeedsRefresh(2, getRefreshStateEvent.Value);
					flag = !getRefreshStateEvent.Value;
				}
				return flag;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x001450D1 File Offset: 0x001432D1
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x001450EC File Offset: 0x001432EC
		protected bool InAttrQuery
		{
			get
			{
				return this.inAttrQuery;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x001450F4 File Offset: 0x001432F4
		public override bool IsReadOnly
		{
			get
			{
				if (!this.ReadOnlyValid)
				{
					this.readOnly |= this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
					GetBoolValueEvent getBoolValueEvent = new GetBoolValueEvent(this.readOnly);
					this.OnGetIsReadOnly(getBoolValueEvent);
					this.readOnly = getBoolValueEvent.Value;
					this.SetNeedsRefresh(4, false);
				}
				return this.readOnly;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x0014516B File Offset: 0x0014336B
		// (set) Token: 0x06004EEA RID: 20202 RVA: 0x00145162 File Offset: 0x00143362
		internal Com2Properties PropertyManager
		{
			get
			{
				return this.com2props;
			}
			set
			{
				this.com2props = value;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x00145173 File Offset: 0x00143373
		public override Type PropertyType
		{
			get
			{
				if (this.valueConverter != null)
				{
					return this.valueConverter.ManagedType;
				}
				return this.propertyType;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x00145190 File Offset: 0x00143390
		protected bool ReadOnlyValid
		{
			get
			{
				if (this.baseReadOnly)
				{
					return true;
				}
				bool flag = !this.GetNeedsRefresh(4);
				if (this.queryRefresh)
				{
					GetRefreshStateEvent getRefreshStateEvent = new GetRefreshStateEvent(Com2ShouldRefreshTypes.ReadOnly, !flag);
					this.OnShouldRefresh(getRefreshStateEvent);
					this.SetNeedsRefresh(4, getRefreshStateEvent.Value);
					flag = !getRefreshStateEvent.Value;
				}
				return flag;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x001451E4 File Offset: 0x001433E4
		public virtual object TargetObject
		{
			get
			{
				if (this.com2props != null)
				{
					return this.com2props.TargetObject;
				}
				return null;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x001451FC File Offset: 0x001433FC
		protected bool TypeConverterValid
		{
			get
			{
				bool flag = this.converter != null && !this.GetNeedsRefresh(32);
				if (this.queryRefresh)
				{
					GetRefreshStateEvent getRefreshStateEvent = new GetRefreshStateEvent(Com2ShouldRefreshTypes.TypeConverter, !flag);
					this.OnShouldRefresh(getRefreshStateEvent);
					this.SetNeedsRefresh(32, getRefreshStateEvent.Value);
					flag = !getRefreshStateEvent.Value;
				}
				return flag;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x00145254 File Offset: 0x00143454
		protected bool TypeEditorValid
		{
			get
			{
				bool flag = this.editor != null && !this.GetNeedsRefresh(64);
				if (this.queryRefresh)
				{
					GetRefreshStateEvent getRefreshStateEvent = new GetRefreshStateEvent(Com2ShouldRefreshTypes.TypeEditor, !flag);
					this.OnShouldRefresh(getRefreshStateEvent);
					this.SetNeedsRefresh(64, getRefreshStateEvent.Value);
					flag = !getRefreshStateEvent.Value;
				}
				return flag;
			}
		}

		// Token: 0x1400040C RID: 1036
		// (add) Token: 0x06004EF1 RID: 20209 RVA: 0x001452AB File Offset: 0x001434AB
		// (remove) Token: 0x06004EF2 RID: 20210 RVA: 0x001452BE File Offset: 0x001434BE
		public event GetBoolValueEventHandler QueryCanResetValue
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventCanResetValue, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventCanResetValue, value);
			}
		}

		// Token: 0x1400040D RID: 1037
		// (add) Token: 0x06004EF3 RID: 20211 RVA: 0x001452D1 File Offset: 0x001434D1
		// (remove) Token: 0x06004EF4 RID: 20212 RVA: 0x001452E4 File Offset: 0x001434E4
		public event GetAttributesEventHandler QueryGetBaseAttributes
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetBaseAttributes, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetBaseAttributes, value);
			}
		}

		// Token: 0x1400040E RID: 1038
		// (add) Token: 0x06004EF5 RID: 20213 RVA: 0x001452F7 File Offset: 0x001434F7
		// (remove) Token: 0x06004EF6 RID: 20214 RVA: 0x0014530A File Offset: 0x0014350A
		public event GetAttributesEventHandler QueryGetDynamicAttributes
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetDynamicAttributes, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetDynamicAttributes, value);
			}
		}

		// Token: 0x1400040F RID: 1039
		// (add) Token: 0x06004EF7 RID: 20215 RVA: 0x0014531D File Offset: 0x0014351D
		// (remove) Token: 0x06004EF8 RID: 20216 RVA: 0x00145330 File Offset: 0x00143530
		public event GetNameItemEventHandler QueryGetDisplayName
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetDisplayName, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetDisplayName, value);
			}
		}

		// Token: 0x14000410 RID: 1040
		// (add) Token: 0x06004EF9 RID: 20217 RVA: 0x00145343 File Offset: 0x00143543
		// (remove) Token: 0x06004EFA RID: 20218 RVA: 0x00145356 File Offset: 0x00143556
		public event GetNameItemEventHandler QueryGetDisplayValue
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetDisplayValue, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetDisplayValue, value);
			}
		}

		// Token: 0x14000411 RID: 1041
		// (add) Token: 0x06004EFB RID: 20219 RVA: 0x00145369 File Offset: 0x00143569
		// (remove) Token: 0x06004EFC RID: 20220 RVA: 0x0014537C File Offset: 0x0014357C
		public event GetBoolValueEventHandler QueryGetIsReadOnly
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetIsReadOnly, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetIsReadOnly, value);
			}
		}

		// Token: 0x14000412 RID: 1042
		// (add) Token: 0x06004EFD RID: 20221 RVA: 0x0014538F File Offset: 0x0014358F
		// (remove) Token: 0x06004EFE RID: 20222 RVA: 0x001453A2 File Offset: 0x001435A2
		public event GetTypeConverterAndTypeEditorEventHandler QueryGetTypeConverterAndTypeEditor
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventGetTypeConverterAndTypeEditor, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventGetTypeConverterAndTypeEditor, value);
			}
		}

		// Token: 0x14000413 RID: 1043
		// (add) Token: 0x06004EFF RID: 20223 RVA: 0x001453B5 File Offset: 0x001435B5
		// (remove) Token: 0x06004F00 RID: 20224 RVA: 0x001453C8 File Offset: 0x001435C8
		public event Com2EventHandler QueryResetValue
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventResetValue, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventResetValue, value);
			}
		}

		// Token: 0x14000414 RID: 1044
		// (add) Token: 0x06004F01 RID: 20225 RVA: 0x001453DB File Offset: 0x001435DB
		// (remove) Token: 0x06004F02 RID: 20226 RVA: 0x001453EE File Offset: 0x001435EE
		public event GetBoolValueEventHandler QueryShouldSerializeValue
		{
			add
			{
				this.Events.AddHandler(Com2PropertyDescriptor.EventShouldSerializeValue, value);
			}
			remove
			{
				this.Events.RemoveHandler(Com2PropertyDescriptor.EventShouldSerializeValue, value);
			}
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x00145404 File Offset: 0x00143604
		public override bool CanResetValue(object component)
		{
			if (component is ICustomTypeDescriptor)
			{
				component = ((ICustomTypeDescriptor)component).GetPropertyOwner(this);
			}
			if (component == this.TargetObject)
			{
				GetBoolValueEvent getBoolValueEvent = new GetBoolValueEvent(false);
				this.OnCanResetValue(getBoolValueEvent);
				return getBoolValueEvent.Value;
			}
			return false;
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x00145446 File Offset: 0x00143646
		public object Clone()
		{
			return new Com2PropertyDescriptor(this.dispid, this.Name, (Attribute[])this.baseAttrs.Clone(), this.readOnly, this.propertyType, this.typeData, this.hrHidden);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x00145484 File Offset: 0x00143684
		private Com2DataTypeToManagedDataTypeConverter CreateOleTypeConverter(Type t)
		{
			if (t == null)
			{
				return null;
			}
			ConstructorInfo constructor = t.GetConstructor(new Type[] { typeof(Com2PropertyDescriptor) });
			Com2DataTypeToManagedDataTypeConverter com2DataTypeToManagedDataTypeConverter;
			if (constructor != null)
			{
				com2DataTypeToManagedDataTypeConverter = (Com2DataTypeToManagedDataTypeConverter)constructor.Invoke(new object[] { this });
			}
			else
			{
				com2DataTypeToManagedDataTypeConverter = (Com2DataTypeToManagedDataTypeConverter)Activator.CreateInstance(t);
			}
			return com2DataTypeToManagedDataTypeConverter;
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x001454E4 File Offset: 0x001436E4
		protected override AttributeCollection CreateAttributeCollection()
		{
			return new AttributeCollection(this.AttributeArray);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x001454F4 File Offset: 0x001436F4
		private TypeConverter GetBaseTypeConverter()
		{
			if (this.PropertyType == null)
			{
				return new TypeConverter();
			}
			TypeConverter typeConverter = null;
			TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)this.Attributes[typeof(TypeConverterAttribute)];
			if (typeConverterAttribute != null)
			{
				string converterTypeName = typeConverterAttribute.ConverterTypeName;
				if (converterTypeName != null && converterTypeName.Length > 0)
				{
					Type type = Type.GetType(converterTypeName);
					if (type != null && typeof(TypeConverter).IsAssignableFrom(type))
					{
						try
						{
							typeConverter = (TypeConverter)Activator.CreateInstance(type);
							if (typeConverter != null)
							{
								this.refreshState |= 8192;
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
			}
			if (typeConverter == null)
			{
				if (!typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(this.PropertyType))
				{
					typeConverter = base.Converter;
				}
				else
				{
					typeConverter = new Com2IDispatchConverter(this, false);
				}
			}
			if (typeConverter == null)
			{
				typeConverter = new TypeConverter();
			}
			return typeConverter;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x001455D8 File Offset: 0x001437D8
		private object GetBaseTypeEditor(Type editorBaseType)
		{
			if (this.PropertyType == null)
			{
				return null;
			}
			object obj = null;
			EditorAttribute editorAttribute = (EditorAttribute)this.Attributes[typeof(EditorAttribute)];
			if (editorAttribute != null)
			{
				string editorBaseTypeName = editorAttribute.EditorBaseTypeName;
				if (editorBaseTypeName != null && editorBaseTypeName.Length > 0)
				{
					Type type = Type.GetType(editorBaseTypeName);
					if (type != null && type == editorBaseType)
					{
						Type type2 = Type.GetType(editorAttribute.EditorTypeName);
						if (type2 != null)
						{
							try
							{
								obj = Activator.CreateInstance(type2);
								if (obj != null)
								{
									this.refreshState |= 16384;
								}
							}
							catch (Exception ex)
							{
							}
						}
					}
				}
			}
			if (obj == null)
			{
				obj = base.GetEditor(editorBaseType);
			}
			return obj;
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x00145698 File Offset: 0x00143898
		public virtual string GetDisplayValue(string defaultValue)
		{
			GetNameItemEvent getNameItemEvent = new GetNameItemEvent(defaultValue);
			this.OnGetDisplayValue(getNameItemEvent);
			return (getNameItemEvent.Name == null) ? null : getNameItemEvent.Name.ToString();
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x001456CC File Offset: 0x001438CC
		public override object GetEditor(Type editorBaseType)
		{
			if (this.TypeEditorValid)
			{
				return this.editor;
			}
			if (this.PropertyType == null)
			{
				return null;
			}
			if (editorBaseType == typeof(UITypeEditor))
			{
				TypeConverter typeConverter = null;
				this.GetTypeConverterAndTypeEditor(ref typeConverter, editorBaseType, ref this.editor);
				if (!this.TypeConverterValid)
				{
					this.converter = typeConverter;
					this.SetNeedsRefresh(32, false);
				}
				this.SetNeedsRefresh(64, false);
			}
			else
			{
				this.editor = base.GetEditor(editorBaseType);
			}
			return this.editor;
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00145754 File Offset: 0x00143954
		public object GetNativeValue(object component)
		{
			if (component == null)
			{
				return null;
			}
			if (component is ICustomTypeDescriptor)
			{
				component = ((ICustomTypeDescriptor)component).GetPropertyOwner(this);
			}
			if (component == null || !Marshal.IsComObject(component) || !(component is UnsafeNativeMethods.IDispatch))
			{
				return null;
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)component;
			object[] array = new object[1];
			NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
			Guid empty = Guid.Empty;
			int num = dispatch.Invoke(this.dispid, ref empty, SafeNativeMethods.GetThreadLCID(), 2, new NativeMethods.tagDISPPARAMS(), array, tagEXCEPINFO, null);
			if (num == -2147352567)
			{
				return null;
			}
			if (num <= 1)
			{
				if (array[0] == null || Convert.IsDBNull(array[0]))
				{
					this.lastValue = null;
				}
				else
				{
					this.lastValue = array[0];
				}
				return this.lastValue;
			}
			throw new ExternalException(SR.GetString("DispInvokeFailed", new object[] { "GetValue", num }), num);
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0014582A File Offset: 0x00143A2A
		private bool GetNeedsRefresh(int mask)
		{
			return (this.refreshState & mask) != 0;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00145838 File Offset: 0x00143A38
		public override object GetValue(object component)
		{
			this.lastValue = this.GetNativeValue(component);
			if (this.ConvertingNativeType && this.lastValue != null)
			{
				this.lastValue = this.valueConverter.ConvertNativeToManaged(this.lastValue, this);
			}
			else if (this.lastValue != null && this.propertyType != null && this.propertyType.IsEnum && this.lastValue.GetType().IsPrimitive)
			{
				try
				{
					this.lastValue = Enum.ToObject(this.propertyType, this.lastValue);
				}
				catch
				{
				}
			}
			return this.lastValue;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x001458E4 File Offset: 0x00143AE4
		public void GetTypeConverterAndTypeEditor(ref TypeConverter typeConverter, Type editorBaseType, ref object typeEditor)
		{
			TypeConverter typeConverter2 = typeConverter;
			object obj = typeEditor;
			if (typeConverter2 == null)
			{
				typeConverter2 = this.GetBaseTypeConverter();
			}
			if (obj == null)
			{
				obj = this.GetBaseTypeEditor(editorBaseType);
			}
			if ((this.refreshState & 8192) == 0 && this.PropertyType == typeof(Com2Variant))
			{
				Type type = this.PropertyType;
				object value = this.GetValue(this.TargetObject);
				if (value != null)
				{
					type = value.GetType();
				}
				ComNativeDescriptor.ResolveVariantTypeConverterAndTypeEditor(value, ref typeConverter2, editorBaseType, ref obj);
			}
			if (typeConverter2 is Com2PropertyDescriptor.Com2PropDescMainConverter)
			{
				typeConverter2 = ((Com2PropertyDescriptor.Com2PropDescMainConverter)typeConverter2).InnerConverter;
			}
			GetTypeConverterAndTypeEditorEvent getTypeConverterAndTypeEditorEvent = new GetTypeConverterAndTypeEditorEvent(typeConverter2, obj);
			this.OnGetTypeConverterAndTypeEditor(getTypeConverterAndTypeEditorEvent);
			typeConverter2 = getTypeConverterAndTypeEditorEvent.TypeConverter;
			obj = getTypeConverterAndTypeEditorEvent.TypeEditor;
			if (typeConverter2 == null)
			{
				typeConverter2 = this.GetBaseTypeConverter();
			}
			if (obj == null)
			{
				obj = this.GetBaseTypeEditor(editorBaseType);
			}
			Type type2 = typeConverter2.GetType();
			if (type2 != typeof(TypeConverter) && type2 != typeof(Com2PropertyDescriptor.Com2PropDescMainConverter))
			{
				typeConverter2 = new Com2PropertyDescriptor.Com2PropDescMainConverter(this, typeConverter2);
			}
			typeConverter = typeConverter2;
			typeEditor = obj;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x001459DF File Offset: 0x00143BDF
		public bool IsCurrentValue(object value)
		{
			return value == this.lastValue || (this.lastValue != null && this.lastValue.Equals(value));
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x00145A02 File Offset: 0x00143C02
		protected void OnCanResetValue(GetBoolValueEvent gvbe)
		{
			this.RaiseGetBoolValueEvent(Com2PropertyDescriptor.EventCanResetValue, gvbe);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x00145A10 File Offset: 0x00143C10
		protected void OnGetBaseAttributes(GetAttributesEvent e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				GetAttributesEventHandler getAttributesEventHandler = (GetAttributesEventHandler)this.Events[Com2PropertyDescriptor.EventGetBaseAttributes];
				if (getAttributesEventHandler != null)
				{
					getAttributesEventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x00145A74 File Offset: 0x00143C74
		protected void OnGetDisplayName(GetNameItemEvent gnie)
		{
			this.RaiseGetNameItemEvent(Com2PropertyDescriptor.EventGetDisplayName, gnie);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00145A82 File Offset: 0x00143C82
		protected void OnGetDisplayValue(GetNameItemEvent gnie)
		{
			this.RaiseGetNameItemEvent(Com2PropertyDescriptor.EventGetDisplayValue, gnie);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x00145A90 File Offset: 0x00143C90
		protected void OnGetDynamicAttributes(GetAttributesEvent e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				GetAttributesEventHandler getAttributesEventHandler = (GetAttributesEventHandler)this.Events[Com2PropertyDescriptor.EventGetDynamicAttributes];
				if (getAttributesEventHandler != null)
				{
					getAttributesEventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x00145AF4 File Offset: 0x00143CF4
		protected void OnGetIsReadOnly(GetBoolValueEvent gvbe)
		{
			this.RaiseGetBoolValueEvent(Com2PropertyDescriptor.EventGetIsReadOnly, gvbe);
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00145B04 File Offset: 0x00143D04
		protected void OnGetTypeConverterAndTypeEditor(GetTypeConverterAndTypeEditorEvent e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				GetTypeConverterAndTypeEditorEventHandler getTypeConverterAndTypeEditorEventHandler = (GetTypeConverterAndTypeEditorEventHandler)this.Events[Com2PropertyDescriptor.EventGetTypeConverterAndTypeEditor];
				if (getTypeConverterAndTypeEditorEventHandler != null)
				{
					getTypeConverterAndTypeEditorEventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00145B68 File Offset: 0x00143D68
		protected void OnResetValue(EventArgs e)
		{
			this.RaiseCom2Event(Com2PropertyDescriptor.EventResetValue, e);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00145B76 File Offset: 0x00143D76
		protected void OnShouldSerializeValue(GetBoolValueEvent gvbe)
		{
			this.RaiseGetBoolValueEvent(Com2PropertyDescriptor.EventShouldSerializeValue, gvbe);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00145B84 File Offset: 0x00143D84
		protected void OnShouldRefresh(GetRefreshStateEvent gvbe)
		{
			this.RaiseGetBoolValueEvent(Com2PropertyDescriptor.EventShouldRefresh, gvbe);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00145B94 File Offset: 0x00143D94
		private void RaiseGetBoolValueEvent(object key, GetBoolValueEvent e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				GetBoolValueEventHandler getBoolValueEventHandler = (GetBoolValueEventHandler)this.Events[key];
				if (getBoolValueEventHandler != null)
				{
					getBoolValueEventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x00145BF4 File Offset: 0x00143DF4
		private void RaiseCom2Event(object key, EventArgs e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				Com2EventHandler com2EventHandler = (Com2EventHandler)this.Events[key];
				if (com2EventHandler != null)
				{
					com2EventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x00145C54 File Offset: 0x00143E54
		private void RaiseGetNameItemEvent(object key, GetNameItemEvent e)
		{
			try
			{
				this.com2props.AlwaysValid = this.com2props.CheckValid();
				GetNameItemEventHandler getNameItemEventHandler = (GetNameItemEventHandler)this.Events[key];
				if (getNameItemEventHandler != null)
				{
					getNameItemEventHandler(this, e);
				}
			}
			finally
			{
				this.com2props.AlwaysValid = false;
			}
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00145CB4 File Offset: 0x00143EB4
		public override void ResetValue(object component)
		{
			if (component is ICustomTypeDescriptor)
			{
				component = ((ICustomTypeDescriptor)component).GetPropertyOwner(this);
			}
			if (component == this.TargetObject)
			{
				this.OnResetValue(EventArgs.Empty);
			}
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00145CE0 File Offset: 0x00143EE0
		internal void SetNeedsRefresh(int mask, bool value)
		{
			if (value)
			{
				this.refreshState |= mask;
				return;
			}
			this.refreshState &= ~mask;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x00145D04 File Offset: 0x00143F04
		public override void SetValue(object component, object value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException(SR.GetString("COM2ReadonlyProperty", new object[] { this.Name }));
			}
			object obj = component;
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this);
			}
			if (obj == null || !Marshal.IsComObject(obj) || !(obj is UnsafeNativeMethods.IDispatch))
			{
				return;
			}
			if (this.valueConverter != null)
			{
				bool flag = false;
				value = this.valueConverter.ConvertManagedToNative(value, this, ref flag);
				if (flag)
				{
					return;
				}
			}
			UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)obj;
			NativeMethods.tagDISPPARAMS tagDISPPARAMS = new NativeMethods.tagDISPPARAMS();
			NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
			tagDISPPARAMS.cArgs = 1;
			tagDISPPARAMS.cNamedArgs = 1;
			int[] array = new int[] { -3 };
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				tagDISPPARAMS.rgdispidNamedArgs = Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
				IntPtr intPtr = Marshal.AllocCoTaskMem(16);
				SafeNativeMethods.VariantInit(new HandleRef(null, intPtr));
				Marshal.GetNativeVariantForObject(value, intPtr);
				tagDISPPARAMS.rgvarg = intPtr;
				try
				{
					Guid guid = Guid.Empty;
					int num = dispatch.Invoke(this.dispid, ref guid, SafeNativeMethods.GetThreadLCID(), 4, tagDISPPARAMS, null, tagEXCEPINFO, new IntPtr[1]);
					string text = null;
					if (num == -2147352567 && tagEXCEPINFO.scode != 0)
					{
						num = tagEXCEPINFO.scode;
						text = tagEXCEPINFO.bstrDescription;
					}
					if (num != -2147467260 && num != -2147221492)
					{
						if (num > 1)
						{
							if (dispatch is UnsafeNativeMethods.ISupportErrorInfo)
							{
								guid = typeof(UnsafeNativeMethods.IDispatch).GUID;
								if (NativeMethods.Succeeded(((UnsafeNativeMethods.ISupportErrorInfo)dispatch).InterfaceSupportsErrorInfo(ref guid)))
								{
									UnsafeNativeMethods.IErrorInfo errorInfo = null;
									UnsafeNativeMethods.GetErrorInfo(0, ref errorInfo);
									string text2 = null;
									if (errorInfo != null && NativeMethods.Succeeded(errorInfo.GetDescription(ref text2)))
									{
										text = text2;
									}
								}
							}
							else if (text == null)
							{
								StringBuilder stringBuilder = new StringBuilder(256);
								if (SafeNativeMethods.FormatMessage(4608, NativeMethods.NullHandleRef, num, CultureInfo.CurrentCulture.LCID, stringBuilder, 255, NativeMethods.NullHandleRef) == 0)
								{
									text = string.Format(CultureInfo.CurrentCulture, SR.GetString("DispInvokeFailed", new object[] { "SetValue", num }), new object[0]);
								}
								else
								{
									text = stringBuilder.ToString();
									while ((text.Length > 0 && text[text.Length - 1] == '\n') || text[text.Length - 1] == '\r')
									{
										text = text.Substring(0, text.Length - 1);
									}
								}
							}
							throw new ExternalException(text, num);
						}
						this.OnValueChanged(component, EventArgs.Empty);
						this.lastValue = value;
					}
				}
				finally
				{
					SafeNativeMethods.VariantClear(new HandleRef(null, intPtr));
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			finally
			{
				gchandle.Free();
			}
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x00145FF4 File Offset: 0x001441F4
		public override bool ShouldSerializeValue(object component)
		{
			GetBoolValueEvent getBoolValueEvent = new GetBoolValueEvent(false);
			this.OnShouldSerializeValue(getBoolValueEvent);
			return getBoolValueEvent.Value;
		}

		// Token: 0x0400341E RID: 13342
		private EventHandlerList events;

		// Token: 0x0400341F RID: 13343
		private bool baseReadOnly;

		// Token: 0x04003420 RID: 13344
		private bool readOnly;

		// Token: 0x04003421 RID: 13345
		private Type propertyType;

		// Token: 0x04003422 RID: 13346
		private int dispid;

		// Token: 0x04003423 RID: 13347
		private TypeConverter converter;

		// Token: 0x04003424 RID: 13348
		private object editor;

		// Token: 0x04003425 RID: 13349
		private string displayName;

		// Token: 0x04003426 RID: 13350
		private object typeData;

		// Token: 0x04003427 RID: 13351
		private int refreshState;

		// Token: 0x04003428 RID: 13352
		private bool queryRefresh;

		// Token: 0x04003429 RID: 13353
		private Com2Properties com2props;

		// Token: 0x0400342A RID: 13354
		private Attribute[] baseAttrs;

		// Token: 0x0400342B RID: 13355
		private object lastValue;

		// Token: 0x0400342C RID: 13356
		private bool typeHide;

		// Token: 0x0400342D RID: 13357
		private bool canShow;

		// Token: 0x0400342E RID: 13358
		private bool hrHidden;

		// Token: 0x0400342F RID: 13359
		private bool inAttrQuery;

		// Token: 0x04003430 RID: 13360
		private static readonly object EventGetBaseAttributes = new object();

		// Token: 0x04003431 RID: 13361
		private static readonly object EventGetDynamicAttributes = new object();

		// Token: 0x04003432 RID: 13362
		private static readonly object EventShouldRefresh = new object();

		// Token: 0x04003433 RID: 13363
		private static readonly object EventGetDisplayName = new object();

		// Token: 0x04003434 RID: 13364
		private static readonly object EventGetDisplayValue = new object();

		// Token: 0x04003435 RID: 13365
		private static readonly object EventGetIsReadOnly = new object();

		// Token: 0x04003436 RID: 13366
		private static readonly object EventGetTypeConverterAndTypeEditor = new object();

		// Token: 0x04003437 RID: 13367
		private static readonly object EventShouldSerializeValue = new object();

		// Token: 0x04003438 RID: 13368
		private static readonly object EventCanResetValue = new object();

		// Token: 0x04003439 RID: 13369
		private static readonly object EventResetValue = new object();

		// Token: 0x0400343A RID: 13370
		private static readonly Guid GUID_COLOR = new Guid("{66504301-BE0F-101A-8BBB-00AA00300CAB}");

		// Token: 0x0400343B RID: 13371
		private static IDictionary oleConverters = new SortedList();

		// Token: 0x0400343C RID: 13372
		private Com2DataTypeToManagedDataTypeConverter valueConverter;

		// Token: 0x02000852 RID: 2130
		private class Com2PropDescMainConverter : Com2ExtendedTypeConverter
		{
			// Token: 0x06007064 RID: 28772 RVA: 0x0019B81E File Offset: 0x00199A1E
			public Com2PropDescMainConverter(Com2PropertyDescriptor pd, TypeConverter baseConverter)
				: base(baseConverter)
			{
				this.pd = pd;
			}

			// Token: 0x06007065 RID: 28773 RVA: 0x0019B830 File Offset: 0x00199A30
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				object obj = base.ConvertTo(context, culture, value, destinationType);
				if (!(destinationType == typeof(string)) || !this.pd.IsCurrentValue(value) || this.pd.PropertyType.IsEnum)
				{
					return obj;
				}
				Com2EnumConverter com2EnumConverter = (Com2EnumConverter)base.GetWrappedConverter(typeof(Com2EnumConverter));
				if (com2EnumConverter == null)
				{
					return this.pd.GetDisplayValue((string)obj);
				}
				return com2EnumConverter.ConvertTo(value, destinationType);
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x0019B8B4 File Offset: 0x00199AB4
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(value, attributes);
				if (propertyDescriptorCollection != null && propertyDescriptorCollection.Count > 0)
				{
					propertyDescriptorCollection = propertyDescriptorCollection.Sort();
					PropertyDescriptor[] array = new PropertyDescriptor[propertyDescriptorCollection.Count];
					propertyDescriptorCollection.CopyTo(array, 0);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array, true);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06007067 RID: 28775 RVA: 0x0019B8FC File Offset: 0x00199AFC
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				if (this.subprops == 0)
				{
					if (!base.GetPropertiesSupported(context))
					{
						this.subprops = 2;
					}
					else if ((this.pd.valueConverter != null && this.pd.valueConverter.AllowExpand) || Com2IVsPerPropertyBrowsingHandler.AllowChildProperties(this.pd))
					{
						this.subprops = 1;
					}
				}
				return this.subprops == 1;
			}

			// Token: 0x0400438C RID: 17292
			private Com2PropertyDescriptor pd;

			// Token: 0x0400438D RID: 17293
			private const int CheckSubprops = 0;

			// Token: 0x0400438E RID: 17294
			private const int AllowSubprops = 1;

			// Token: 0x0400438F RID: 17295
			private const int SupressSubprops = 2;

			// Token: 0x04004390 RID: 17296
			private int subprops;
		}
	}
}
