using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A8 RID: 1704
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06003FBA RID: 16314 RVA: 0x0010C071 File Offset: 0x0010A271
		internal CompiledRegexRunner()
		{
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x0010C079 File Offset: 0x0010A279
		internal void SetDelegates(NoParamDelegate go, FindFirstCharDelegate firstChar, NoParamDelegate trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x0010C090 File Offset: 0x0010A290
		protected override void Go()
		{
			this.goMethod(this);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x0010C09E File Offset: 0x0010A29E
		protected override bool FindFirstChar()
		{
			return this.findFirstCharMethod(this);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x0010C0AC File Offset: 0x0010A2AC
		protected override void InitTrackCount()
		{
			this.initTrackCountMethod(this);
		}

		// Token: 0x04002E70 RID: 11888
		private NoParamDelegate goMethod;

		// Token: 0x04002E71 RID: 11889
		private FindFirstCharDelegate findFirstCharMethod;

		// Token: 0x04002E72 RID: 11890
		private NoParamDelegate initTrackCountMethod;
	}
}
