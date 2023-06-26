using System;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200064A RID: 1610
	internal sealed class SymbolMethod : MethodInfo
	{
		// Token: 0x06004BEC RID: 19436 RVA: 0x00113A4C File Offset: 0x00111C4C
		[SecurityCritical]
		internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.m_mdMethod = token;
			this.m_returnType = returnType;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			this.m_module = mod;
			this.m_containingType = arrayClass;
			this.m_name = methodName;
			this.m_callingConvention = callingConvention;
			this.m_signature = SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00113AD3 File Offset: 0x00111CD3
		internal override Type[] GetParameterTypes()
		{
			return this.m_parameterTypes;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00113ADB File Offset: 0x00111CDB
		internal MethodToken GetToken(ModuleBuilder mod)
		{
			return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x00113B01 File Offset: 0x00111D01
		public override Module Module
		{
			get
			{
				return this.m_module;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x00113B09 File Offset: 0x00111D09
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x00113B11 File Offset: 0x00111D11
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x00113B19 File Offset: 0x00111D19
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00113B21 File Offset: 0x00111D21
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00113B32 File Offset: 0x00111D32
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00113B43 File Offset: 0x00111D43
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x00113B54 File Offset: 0x00111D54
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x00113B5C File Offset: 0x00111D5C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x00113B6D File Offset: 0x00111D6D
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004BF9 RID: 19449 RVA: 0x00113B75 File Offset: 0x00111D75
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x00113B78 File Offset: 0x00111D78
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x00113B89 File Offset: 0x00111D89
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00113B8C File Offset: 0x00111D8C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x00113B9D File Offset: 0x00111D9D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x00113BAE File Offset: 0x00111DAE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x00113BBF File Offset: 0x00111DBF
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00113BC7 File Offset: 0x00111DC7
		public MethodToken GetToken()
		{
			return this.m_mdMethod;
		}

		// Token: 0x04001F4E RID: 8014
		private ModuleBuilder m_module;

		// Token: 0x04001F4F RID: 8015
		private Type m_containingType;

		// Token: 0x04001F50 RID: 8016
		private string m_name;

		// Token: 0x04001F51 RID: 8017
		private CallingConventions m_callingConvention;

		// Token: 0x04001F52 RID: 8018
		private Type m_returnType;

		// Token: 0x04001F53 RID: 8019
		private MethodToken m_mdMethod;

		// Token: 0x04001F54 RID: 8020
		private Type[] m_parameterTypes;

		// Token: 0x04001F55 RID: 8021
		private SignatureHelper m_signature;
	}
}
