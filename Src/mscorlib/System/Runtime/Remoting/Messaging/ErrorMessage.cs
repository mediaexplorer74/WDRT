using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086F RID: 2159
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005C02 RID: 23554 RVA: 0x00144281 File Offset: 0x00142481
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x00144284 File Offset: 0x00142484
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this.m_URI;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005C04 RID: 23556 RVA: 0x0014428C File Offset: 0x0014248C
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00144294 File Offset: 0x00142494
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.m_TypeName;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005C06 RID: 23558 RVA: 0x0014429C File Offset: 0x0014249C
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodSignature;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x001442A4 File Offset: 0x001424A4
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005C08 RID: 23560 RVA: 0x001442A7 File Offset: 0x001424A7
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x001442AF File Offset: 0x001424AF
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this.m_ArgName;
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x001442B7 File Offset: 0x001424B7
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x001442BA File Offset: 0x001424BA
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005C0C RID: 23564 RVA: 0x001442BD File Offset: 0x001424BD
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x001442C0 File Offset: 0x001424C0
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x001442C8 File Offset: 0x001424C8
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x001442CB File Offset: 0x001424CB
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005C10 RID: 23568 RVA: 0x001442CE File Offset: 0x001424CE
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x001442D1 File Offset: 0x001424D1
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x04002995 RID: 10645
		private string m_URI = "Exception";

		// Token: 0x04002996 RID: 10646
		private string m_MethodName = "Unknown";

		// Token: 0x04002997 RID: 10647
		private string m_TypeName = "Unknown";

		// Token: 0x04002998 RID: 10648
		private object m_MethodSignature;

		// Token: 0x04002999 RID: 10649
		private int m_ArgCount;

		// Token: 0x0400299A RID: 10650
		private string m_ArgName = "Unknown";
	}
}
