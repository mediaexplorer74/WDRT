using System;

namespace System
{
	// Token: 0x0200004C RID: 76
	internal enum ParsingError
	{
		// Token: 0x04000496 RID: 1174
		None,
		// Token: 0x04000497 RID: 1175
		BadFormat,
		// Token: 0x04000498 RID: 1176
		BadScheme,
		// Token: 0x04000499 RID: 1177
		BadAuthority,
		// Token: 0x0400049A RID: 1178
		EmptyUriString,
		// Token: 0x0400049B RID: 1179
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x0400049C RID: 1180
		SchemeLimit,
		// Token: 0x0400049D RID: 1181
		SizeLimit,
		// Token: 0x0400049E RID: 1182
		MustRootedPath,
		// Token: 0x0400049F RID: 1183
		BadHostName,
		// Token: 0x040004A0 RID: 1184
		NonEmptyHost,
		// Token: 0x040004A1 RID: 1185
		BadPort,
		// Token: 0x040004A2 RID: 1186
		BadAuthorityTerminator,
		// Token: 0x040004A3 RID: 1187
		CannotCreateRelative
	}
}
