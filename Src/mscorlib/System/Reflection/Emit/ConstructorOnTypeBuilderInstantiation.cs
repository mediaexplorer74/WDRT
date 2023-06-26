using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000667 RID: 1639
	internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
	{
		// Token: 0x06004EF2 RID: 20210 RVA: 0x0011D5B0 File Offset: 0x0011B7B0
		internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
		{
			return new ConstructorOnTypeBuilderInstantiation(Constructor, type);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0011D5B9 File Offset: 0x0011B7B9
		internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
		{
			this.m_ctor = constructor;
			this.m_type = type;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x0011D5CF File Offset: 0x0011B7CF
		internal override Type[] GetParameterTypes()
		{
			return this.m_ctor.GetParameterTypes();
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0011D5DC File Offset: 0x0011B7DC
		internal override Type GetReturnType()
		{
			return this.DeclaringType;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x0011D5E4 File Offset: 0x0011B7E4
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_ctor.MemberType;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0011D5F1 File Offset: 0x0011B7F1
		public override string Name
		{
			get
			{
				return this.m_ctor.Name;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0011D5FE File Offset: 0x0011B7FE
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x0011D606 File Offset: 0x0011B806
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x0011D60E File Offset: 0x0011B80E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(inherit);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0011D61C File Offset: 0x0011B81C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0011D62B File Offset: 0x0011B82B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_ctor.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0011D63C File Offset: 0x0011B83C
		internal int MetadataTokenInternal
		{
			get
			{
				ConstructorBuilder constructorBuilder = this.m_ctor as ConstructorBuilder;
				if (constructorBuilder != null)
				{
					return constructorBuilder.MetadataTokenInternal;
				}
				return this.m_ctor.MetadataToken;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0011D670 File Offset: 0x0011B870
		public override Module Module
		{
			get
			{
				return this.m_ctor.Module;
			}
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x0011D67D File Offset: 0x0011B87D
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0011D685 File Offset: 0x0011B885
		public override ParameterInfo[] GetParameters()
		{
			return this.m_ctor.GetParameters();
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0011D692 File Offset: 0x0011B892
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_ctor.GetMethodImplementationFlags();
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0011D69F File Offset: 0x0011B89F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_ctor.MethodHandle;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x0011D6AC File Offset: 0x0011B8AC
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_ctor.Attributes;
			}
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0011D6B9 File Offset: 0x0011B8B9
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0011D6C0 File Offset: 0x0011B8C0
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_ctor.CallingConvention;
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0011D6CD File Offset: 0x0011B8CD
		public override Type[] GetGenericArguments()
		{
			return this.m_ctor.GetGenericArguments();
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0011D6DA File Offset: 0x0011B8DA
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x0011D6DD File Offset: 0x0011B8DD
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0011D6E0 File Offset: 0x0011B8E0
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0011D6E3 File Offset: 0x0011B8E3
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040021DF RID: 8671
		internal ConstructorInfo m_ctor;

		// Token: 0x040021E0 RID: 8672
		private TypeBuilderInstantiation m_type;
	}
}
