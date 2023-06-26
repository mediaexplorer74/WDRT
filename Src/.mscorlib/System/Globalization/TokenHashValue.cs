using System;

namespace System.Globalization
{
	// Token: 0x020003AE RID: 942
	internal class TokenHashValue
	{
		// Token: 0x06002F47 RID: 12103 RVA: 0x000B68D2 File Offset: 0x000B4AD2
		internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
		{
			this.tokenString = tokenString;
			this.tokenType = tokenType;
			this.tokenValue = tokenValue;
		}

		// Token: 0x040013D7 RID: 5079
		internal string tokenString;

		// Token: 0x040013D8 RID: 5080
		internal TokenType tokenType;

		// Token: 0x040013D9 RID: 5081
		internal int tokenValue;
	}
}
