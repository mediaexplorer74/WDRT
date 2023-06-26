using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the Content-Transfer-Encoding header information for an email message attachment.</summary>
	// Token: 0x02000252 RID: 594
	public enum TransferEncoding
	{
		/// <summary>Encodes data that consists of printable characters in the US-ASCII character set. See RFC 2406 Section 6.7.</summary>
		// Token: 0x0400174A RID: 5962
		QuotedPrintable,
		/// <summary>Encodes stream-based data. See RFC 2406 Section 6.8.</summary>
		// Token: 0x0400174B RID: 5963
		Base64,
		/// <summary>Used for data that is not encoded. The data is in 7-bit US-ASCII characters with a total line length of no longer than 1000 characters. See RFC2406 Section 2.7.</summary>
		// Token: 0x0400174C RID: 5964
		SevenBit,
		/// <summary>The data is in 8-bit characters that may represent international characters with a total line length of no longer than 1000 8-bit characters. For more information about this 8-bit MIME transport extension, see IETF RFC 6152.</summary>
		// Token: 0x0400174D RID: 5965
		EightBit,
		/// <summary>Indicates that the transfer encoding is unknown.</summary>
		// Token: 0x0400174E RID: 5966
		Unknown = -1
	}
}
