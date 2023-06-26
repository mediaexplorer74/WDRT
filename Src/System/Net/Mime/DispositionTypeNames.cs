using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an email attachment.</summary>
	// Token: 0x02000242 RID: 578
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as part of the email message body.</summary>
		// Token: 0x040016F2 RID: 5874
		public const string Inline = "inline";

		/// <summary>Specifies that the attachment is to be displayed as a file attached to the email message.</summary>
		// Token: 0x040016F3 RID: 5875
		public const string Attachment = "attachment";
	}
}
