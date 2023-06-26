using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	internal class EnvironmentStringExpressionSet : StringExpressionSet
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x0008AE0B File Offset: 0x0008900B
		public EnvironmentStringExpressionSet()
			: base(true, null, false)
		{
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0008AE16 File Offset: 0x00089016
		public EnvironmentStringExpressionSet(string str)
			: base(true, str, false)
		{
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0008AE21 File Offset: 0x00089021
		protected override StringExpressionSet CreateNewEmpty()
		{
			return new EnvironmentStringExpressionSet();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008AE28 File Offset: 0x00089028
		protected override bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return string.Compare(left, right, StringComparison.Ordinal) == 0;
			}
			return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0008AE44 File Offset: 0x00089044
		protected override string ProcessWholeString(string str)
		{
			return str;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0008AE47 File Offset: 0x00089047
		protected override string ProcessSingleString(string str)
		{
			return str;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0008AE4A File Offset: 0x0008904A
		[SecuritySafeCritical]
		public override string ToString()
		{
			return base.UnsafeToString();
		}
	}
}
