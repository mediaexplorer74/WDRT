using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000632 RID: 1586
	internal class DynamicScope
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x0010DC0F File Offset: 0x0010BE0F
		internal DynamicScope()
		{
			this.m_tokens = new List<object>();
			this.m_tokens.Add(null);
		}

		// Token: 0x17000B90 RID: 2960
		internal object this[int token]
		{
			get
			{
				token &= 16777215;
				if (token < 0 || token > this.m_tokens.Count)
				{
					return null;
				}
				return this.m_tokens[token];
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0010DC59 File Offset: 0x0010BE59
		internal int GetTokenFor(VarArgMethod varArgMethod)
		{
			this.m_tokens.Add(varArgMethod);
			return (this.m_tokens.Count - 1) | 167772160;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0010DC7A File Offset: 0x0010BE7A
		internal string GetString(int token)
		{
			return this[token] as string;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0010DC88 File Offset: 0x0010BE88
		internal byte[] ResolveSignature(int token, int fromMethod)
		{
			if (fromMethod == 0)
			{
				return (byte[])this[token];
			}
			VarArgMethod varArgMethod = this[token] as VarArgMethod;
			if (varArgMethod == null)
			{
				return null;
			}
			return varArgMethod.m_signature.GetSignature(true);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0010DCC4 File Offset: 0x0010BEC4
		[SecuritySafeCritical]
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
			RuntimeMethodHandleInternal value = methodInfo.Value;
			if (methodInfo != null && !RuntimeMethodHandle.IsDynamicMethod(value))
			{
				RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(value);
				if (declaringType != null && RuntimeTypeHandle.HasInstantiation(declaringType))
				{
					MethodBase methodBase = RuntimeType.GetMethodBase(methodInfo);
					Type genericTypeDefinition = methodBase.DeclaringType.GetGenericTypeDefinition();
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGenericLcg"), methodBase, genericTypeDefinition));
				}
			}
			this.m_tokens.Add(method);
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0010DD58 File Offset: 0x0010BF58
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericMethodInfo(method, typeContext));
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0010DD7F File Offset: 0x0010BF7F
		public int GetTokenFor(DynamicMethod method)
		{
			this.m_tokens.Add(method);
			return (this.m_tokens.Count - 1) | 100663296;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0010DDA0 File Offset: 0x0010BFA0
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			this.m_tokens.Add(field);
			return (this.m_tokens.Count - 1) | 67108864;
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0010DDC6 File Offset: 0x0010BFC6
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericFieldInfo(field, typeContext));
			return (this.m_tokens.Count - 1) | 67108864;
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0010DDED File Offset: 0x0010BFED
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			this.m_tokens.Add(type);
			return (this.m_tokens.Count - 1) | 33554432;
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0010DE13 File Offset: 0x0010C013
		public int GetTokenFor(string literal)
		{
			this.m_tokens.Add(literal);
			return (this.m_tokens.Count - 1) | 1879048192;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0010DE34 File Offset: 0x0010C034
		public int GetTokenFor(byte[] signature)
		{
			this.m_tokens.Add(signature);
			return (this.m_tokens.Count - 1) | 285212672;
		}

		// Token: 0x04001E9D RID: 7837
		internal List<object> m_tokens;
	}
}
