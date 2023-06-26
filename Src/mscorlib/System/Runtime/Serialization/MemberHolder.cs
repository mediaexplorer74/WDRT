using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000736 RID: 1846
	[Serializable]
	internal class MemberHolder
	{
		// Token: 0x060051DE RID: 20958 RVA: 0x001214E5 File Offset: 0x0011F6E5
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this.memberType = type;
			this.context = ctx;
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x001214FB File Offset: 0x0011F6FB
		public override int GetHashCode()
		{
			return this.memberType.GetHashCode();
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00121508 File Offset: 0x0011F708
		public override bool Equals(object obj)
		{
			if (!(obj is MemberHolder))
			{
				return false;
			}
			MemberHolder memberHolder = (MemberHolder)obj;
			return memberHolder.memberType == this.memberType && memberHolder.context.State == this.context.State;
		}

		// Token: 0x04002448 RID: 9288
		internal MemberInfo[] members;

		// Token: 0x04002449 RID: 9289
		internal Type memberType;

		// Token: 0x0400244A RID: 9290
		internal StreamingContext context;
	}
}
