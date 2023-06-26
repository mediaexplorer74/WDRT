using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001BB19 File Offset: 0x00019D19
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0001BB21 File Offset: 0x00019D21
		internal bool Traced { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001BB2A File Offset: 0x00019D2A
		public Exception Error { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001BB32 File Offset: 0x00019D32
		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001BB3A File Offset: 0x00019D3A
		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001BB42 File Offset: 0x00019D42
		public string Path { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BB4A File Offset: 0x00019D4A
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001BB52 File Offset: 0x00019D52
		public bool Handled { get; set; }
	}
}
