using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000666 RID: 1638
	internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004ED5 RID: 20181 RVA: 0x0011D41E File Offset: 0x0011B61E
		internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
		{
			return new MethodOnTypeBuilderInstantiation(method, type);
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x0011D427 File Offset: 0x0011B627
		internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
		{
			this.m_method = method;
			this.m_type = type;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0011D43D File Offset: 0x0011B63D
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x0011D44A File Offset: 0x0011B64A
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0011D457 File Offset: 0x0011B657
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0011D464 File Offset: 0x0011B664
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x0011D46C File Offset: 0x0011B66C
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0011D474 File Offset: 0x0011B674
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0011D482 File Offset: 0x0011B682
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0011D491 File Offset: 0x0011B691
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x0011D4A0 File Offset: 0x0011B6A0
		internal int MetadataTokenInternal
		{
			get
			{
				MethodBuilder methodBuilder = this.m_method as MethodBuilder;
				if (methodBuilder != null)
				{
					return methodBuilder.MetadataTokenInternal;
				}
				return this.m_method.MetadataToken;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x0011D4D4 File Offset: 0x0011B6D4
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0011D4E1 File Offset: 0x0011B6E1
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0011D4E9 File Offset: 0x0011B6E9
		public override ParameterInfo[] GetParameters()
		{
			return this.m_method.GetParameters();
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0011D4F6 File Offset: 0x0011B6F6
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x0011D503 File Offset: 0x0011B703
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_method.MethodHandle;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x0011D510 File Offset: 0x0011B710
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0011D51D File Offset: 0x0011B71D
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0011D524 File Offset: 0x0011B724
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0011D531 File Offset: 0x0011B731
		public override Type[] GetGenericArguments()
		{
			return this.m_method.GetGenericArguments();
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0011D53E File Offset: 0x0011B73E
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x0011D546 File Offset: 0x0011B746
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x0011D553 File Offset: 0x0011B753
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_method.ContainsGenericParameters;
			}
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0011D560 File Offset: 0x0011B760
		public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
			}
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArgs);
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x0011D581 File Offset: 0x0011B781
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_method.IsGenericMethod;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x0011D58E File Offset: 0x0011B78E
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x0011D59B File Offset: 0x0011B79B
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0011D5A2 File Offset: 0x0011B7A2
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0011D5A9 File Offset: 0x0011B7A9
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040021DD RID: 8669
		internal MethodInfo m_method;

		// Token: 0x040021DE RID: 8670
		private TypeBuilderInstantiation m_type;
	}
}
