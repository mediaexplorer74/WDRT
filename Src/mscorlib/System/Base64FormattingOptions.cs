using System;

namespace System
{
	/// <summary>Specifies whether relevant <see cref="Overload:System.Convert.ToBase64CharArray" /> and <see cref="Overload:System.Convert.ToBase64String" /> methods insert line breaks in their output.</summary>
	// Token: 0x020000CB RID: 203
	[Flags]
	public enum Base64FormattingOptions
	{
		/// <summary>Does not insert line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x04000532 RID: 1330
		None = 0,
		/// <summary>Inserts line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x04000533 RID: 1331
		InsertLineBreaks = 1
	}
}
