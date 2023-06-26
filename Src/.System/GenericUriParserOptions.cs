using System;

namespace System
{
	/// <summary>Specifies options for a <see cref="T:System.UriParser" />.</summary>
	// Token: 0x02000050 RID: 80
	[Flags]
	public enum GenericUriParserOptions
	{
		/// <summary>The parser:
		///
		/// Requires an authority.
		/// Converts back slashes into forward slashes.
		/// Unescapes path dots, forward slashes, and back slashes.
		/// Removes trailing dots, empty segments, and dots-only segments.</summary>
		// Token: 0x040004B7 RID: 1207
		Default = 0,
		/// <summary>The parser allows a registry-based authority.</summary>
		// Token: 0x040004B8 RID: 1208
		GenericAuthority = 1,
		/// <summary>The parser allows a URI with no authority.</summary>
		// Token: 0x040004B9 RID: 1209
		AllowEmptyAuthority = 2,
		/// <summary>The scheme does not define a user information part.</summary>
		// Token: 0x040004BA RID: 1210
		NoUserInfo = 4,
		/// <summary>The scheme does not define a port.</summary>
		// Token: 0x040004BB RID: 1211
		NoPort = 8,
		/// <summary>The scheme does not define a query part.</summary>
		// Token: 0x040004BC RID: 1212
		NoQuery = 16,
		/// <summary>The scheme does not define a fragment part.</summary>
		// Token: 0x040004BD RID: 1213
		NoFragment = 32,
		/// <summary>The parser does not convert back slashes into forward slashes.</summary>
		// Token: 0x040004BE RID: 1214
		DontConvertPathBackslashes = 64,
		/// <summary>The parser does not canonicalize the URI.</summary>
		// Token: 0x040004BF RID: 1215
		DontCompressPath = 128,
		/// <summary>The parser does not unescape path dots, forward slashes, or back slashes.</summary>
		// Token: 0x040004C0 RID: 1216
		DontUnescapePathDotsAndSlashes = 256,
		/// <summary>The parser supports Internationalized Domain Name (IDN) parsing (IDN) of host names. Whether IDN is used is dictated by configuration values.</summary>
		// Token: 0x040004C1 RID: 1217
		Idn = 512,
		/// <summary>The parser supports the parsing rules specified in RFC 3987 for International Resource Identifiers (IRI). Whether IRI is used is dictated by configuration values.</summary>
		// Token: 0x040004C2 RID: 1218
		IriParsing = 1024
	}
}
