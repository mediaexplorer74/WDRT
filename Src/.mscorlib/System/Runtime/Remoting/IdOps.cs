using System;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B0 RID: 1968
	internal struct IdOps
	{
		// Token: 0x06005586 RID: 21894 RVA: 0x00130E1E File Offset: 0x0012F01E
		internal static bool bStrongIdentity(int flags)
		{
			return (flags & 2) != 0;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00130E26 File Offset: 0x0012F026
		internal static bool bIsInitializing(int flags)
		{
			return (flags & 4) != 0;
		}

		// Token: 0x04002761 RID: 10081
		internal const int None = 0;

		// Token: 0x04002762 RID: 10082
		internal const int GenerateURI = 1;

		// Token: 0x04002763 RID: 10083
		internal const int StrongIdentity = 2;

		// Token: 0x04002764 RID: 10084
		internal const int IsInitializing = 4;
	}
}
