using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000668 RID: 1640
	internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
	{
		// Token: 0x06004F0B RID: 20235 RVA: 0x0011D6EC File Offset: 0x0011B8EC
		internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
		{
			FieldInfo fieldInfo;
			if (type.m_hashtable.Contains(Field))
			{
				fieldInfo = type.m_hashtable[Field] as FieldInfo;
			}
			else
			{
				fieldInfo = new FieldOnTypeBuilderInstantiation(Field, type);
				type.m_hashtable[Field] = fieldInfo;
			}
			return fieldInfo;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0011D733 File Offset: 0x0011B933
		internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
		{
			this.m_field = field;
			this.m_type = type;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0011D749 File Offset: 0x0011B949
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0011D751 File Offset: 0x0011B951
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0011D754 File Offset: 0x0011B954
		public override string Name
		{
			get
			{
				return this.m_field.Name;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x0011D761 File Offset: 0x0011B961
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0011D769 File Offset: 0x0011B969
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0011D771 File Offset: 0x0011B971
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0011D77F File Offset: 0x0011B97F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0011D78E File Offset: 0x0011B98E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0011D7A0 File Offset: 0x0011B9A0
		internal int MetadataTokenInternal
		{
			get
			{
				FieldBuilder fieldBuilder = this.m_field as FieldBuilder;
				if (fieldBuilder != null)
				{
					return fieldBuilder.MetadataTokenInternal;
				}
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0011D7D4 File Offset: 0x0011B9D4
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x0011D7E1 File Offset: 0x0011B9E1
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x0011D7E9 File Offset: 0x0011B9E9
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_field.GetRequiredCustomModifiers();
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0011D7F6 File Offset: 0x0011B9F6
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_field.GetOptionalCustomModifiers();
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0011D803 File Offset: 0x0011BA03
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0011D80A File Offset: 0x0011BA0A
		public override object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0011D811 File Offset: 0x0011BA11
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x0011D818 File Offset: 0x0011BA18
		public override Type FieldType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0011D81F File Offset: 0x0011BA1F
		public override object GetValue(object obj)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0011D826 File Offset: 0x0011BA26
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0011D82D File Offset: 0x0011BA2D
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x040021E1 RID: 8673
		private FieldInfo m_field;

		// Token: 0x040021E2 RID: 8674
		private TypeBuilderInstantiation m_type;
	}
}
