using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies characteristics of the X.500 distinguished name.</summary>
	// Token: 0x02000461 RID: 1121
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		/// <summary>The distinguished name has no special characteristics.</summary>
		// Token: 0x0400258A RID: 9610
		None = 0,
		/// <summary>The distinguished name is reversed.</summary>
		// Token: 0x0400258B RID: 9611
		Reversed = 1,
		/// <summary>The distinguished name uses semicolons.</summary>
		// Token: 0x0400258C RID: 9612
		UseSemicolons = 16,
		/// <summary>The distinguished name does not use the plus sign.</summary>
		// Token: 0x0400258D RID: 9613
		DoNotUsePlusSign = 32,
		/// <summary>The distinguished name does not use quotation marks.</summary>
		// Token: 0x0400258E RID: 9614
		DoNotUseQuotes = 64,
		/// <summary>The distinguished name uses commas.</summary>
		// Token: 0x0400258F RID: 9615
		UseCommas = 128,
		/// <summary>The distinguished name uses the new line character.</summary>
		// Token: 0x04002590 RID: 9616
		UseNewLines = 256,
		/// <summary>The distinguished name uses UTF8 encoding instead of Unicode character encoding.</summary>
		// Token: 0x04002591 RID: 9617
		UseUTF8Encoding = 4096,
		/// <summary>The distinguished name uses T61 encoding.</summary>
		// Token: 0x04002592 RID: 9618
		UseT61Encoding = 8192,
		/// <summary>Forces the distinguished name to encode specific X.500 keys as UTF-8 strings rather than printable Unicode strings. For more information and the list of X.500 keys affected, see the X500NameFlags enumeration.</summary>
		// Token: 0x04002593 RID: 9619
		ForceUTF8Encoding = 16384
	}
}
