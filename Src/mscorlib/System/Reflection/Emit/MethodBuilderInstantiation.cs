using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000647 RID: 1607
	internal sealed class MethodBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004B99 RID: 19353 RVA: 0x00113170 File Offset: 0x00111370
		internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
		{
			if (!method.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return new MethodBuilderInstantiation(method, inst);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00113187 File Offset: 0x00111387
		internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
		{
			this.m_method = method;
			this.m_inst = inst;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0011319D File Offset: 0x0011139D
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x001131AA File Offset: 0x001113AA
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x001131B7 File Offset: 0x001113B7
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x001131C4 File Offset: 0x001113C4
		public override Type DeclaringType
		{
			get
			{
				return this.m_method.DeclaringType;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x001131D1 File Offset: 0x001113D1
		public override Type ReflectedType
		{
			get
			{
				return this.m_method.ReflectedType;
			}
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x001131DE File Offset: 0x001113DE
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x001131EC File Offset: 0x001113EC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x001131FB File Offset: 0x001113FB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x0011320A File Offset: 0x0011140A
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00113217 File Offset: 0x00111417
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0011321F File Offset: 0x0011141F
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00113226 File Offset: 0x00111426
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x00113233 File Offset: 0x00111433
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x00113244 File Offset: 0x00111444
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x00113251 File Offset: 0x00111451
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x00113258 File Offset: 0x00111458
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00113265 File Offset: 0x00111465
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x0011326D File Offset: 0x0011146D
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004BAD RID: 19373 RVA: 0x00113275 File Offset: 0x00111475
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004BAE RID: 19374 RVA: 0x00113278 File Offset: 0x00111478
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x001132C7 File Offset: 0x001114C7
		public override MethodInfo MakeGenericMethod(params Type[] arguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x001132D8 File Offset: 0x001114D8
		public override bool IsGenericMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x001132DB File Offset: 0x001114DB
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x001132E8 File Offset: 0x001114E8
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x001132EF File Offset: 0x001114EF
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x001132F6 File Offset: 0x001114F6
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001F41 RID: 8001
		internal MethodInfo m_method;

		// Token: 0x04001F42 RID: 8002
		private Type[] m_inst;
	}
}
