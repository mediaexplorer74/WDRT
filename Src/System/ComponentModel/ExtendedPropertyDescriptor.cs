using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000552 RID: 1362
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06003330 RID: 13104 RVA: 0x000E33D4 File Offset: 0x000E15D4
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes)
			: base(extenderInfo, attributes)
		{
			ArrayList arrayList = new ArrayList(this.AttributeArray);
			arrayList.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				arrayList.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			this.AttributeArray = array;
			this.extenderInfo = extenderInfo;
			this.provider = provider;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000E3444 File Offset: 0x000E1644
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes)
			: base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor reflectPropertyDescriptor = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this.extenderInfo = reflectPropertyDescriptor;
			this.provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000E3493 File Offset: 0x000E1693
		public override bool CanResetValue(object comp)
		{
			return this.extenderInfo.ExtenderCanResetValue(this.provider, comp);
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000E34A7 File Offset: 0x000E16A7
		public override Type ComponentType
		{
			get
			{
				return this.extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000E34B4 File Offset: 0x000E16B4
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000E34D5 File Offset: 0x000E16D5
		public override Type PropertyType
		{
			get
			{
				return this.extenderInfo.ExtenderGetType(this.provider);
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000E34E8 File Offset: 0x000E16E8
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this.provider);
					if (site != null)
					{
						string name = site.Name;
						if (name != null && name.Length > 0)
						{
							text = SR.GetString("MetaExtenderName", new object[] { text, name });
						}
					}
				}
				return text;
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000E355E File Offset: 0x000E175E
		public override object GetValue(object comp)
		{
			return this.extenderInfo.ExtenderGetValue(this.provider, comp);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000E3572 File Offset: 0x000E1772
		public override void ResetValue(object comp)
		{
			this.extenderInfo.ExtenderResetValue(this.provider, comp, this);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000E3587 File Offset: 0x000E1787
		public override void SetValue(object component, object value)
		{
			this.extenderInfo.ExtenderSetValue(this.provider, component, value, this);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000E359D File Offset: 0x000E179D
		public override bool ShouldSerializeValue(object comp)
		{
			return this.extenderInfo.ExtenderShouldSerializeValue(this.provider, comp);
		}

		// Token: 0x040029A0 RID: 10656
		private readonly ReflectPropertyDescriptor extenderInfo;

		// Token: 0x040029A1 RID: 10657
		private readonly IExtenderProvider provider;
	}
}
