using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the encryption methods used by documents displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	// Token: 0x02000430 RID: 1072
	public enum WebBrowserEncryptionLevel
	{
		/// <summary>No security encryption.</summary>
		// Token: 0x040027E7 RID: 10215
		Insecure,
		/// <summary>Multiple security encryption methods in different Web page frames.</summary>
		// Token: 0x040027E8 RID: 10216
		Mixed,
		/// <summary>Unknown security encryption.</summary>
		// Token: 0x040027E9 RID: 10217
		Unknown,
		/// <summary>40-bit security encryption.</summary>
		// Token: 0x040027EA RID: 10218
		Bit40,
		/// <summary>56-bit security encryption.</summary>
		// Token: 0x040027EB RID: 10219
		Bit56,
		/// <summary>Fortezza security encryption.</summary>
		// Token: 0x040027EC RID: 10220
		Fortezza,
		/// <summary>128-bit security encryption.</summary>
		// Token: 0x040027ED RID: 10221
		Bit128
	}
}
