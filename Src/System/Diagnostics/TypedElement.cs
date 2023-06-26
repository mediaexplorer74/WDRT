using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004B9 RID: 1209
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x06002D21 RID: 11553 RVA: 0x000CB444 File Offset: 0x000C9644
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000CB47E File Offset: 0x000C967E
		// (set) Token: 0x06002D23 RID: 11555 RVA: 0x000CB490 File Offset: 0x000C9690
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
			set
			{
				base[TypedElement._propInitData] = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000CB49E File Offset: 0x000C969E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000CB4A6 File Offset: 0x000C96A6
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000CB4B8 File Offset: 0x000C96B8
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000CB4C6 File Offset: 0x000C96C6
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x040026F4 RID: 9972
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x040026F5 RID: 9973
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x040026F6 RID: 9974
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x040026F7 RID: 9975
		protected object _runtimeObject;

		// Token: 0x040026F8 RID: 9976
		private Type _baseType;
	}
}
