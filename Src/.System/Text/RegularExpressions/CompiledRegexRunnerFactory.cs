using System;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006AB RID: 1707
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x06003FC7 RID: 16327 RVA: 0x0010C0BA File Offset: 0x0010A2BA
		internal CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0010C0D8 File Offset: 0x0010A2D8
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			compiledRegexRunner.SetDelegates((NoParamDelegate)this.goMethod.CreateDelegate(typeof(NoParamDelegate)), (FindFirstCharDelegate)this.findFirstCharMethod.CreateDelegate(typeof(FindFirstCharDelegate)), (NoParamDelegate)this.initTrackCountMethod.CreateDelegate(typeof(NoParamDelegate)));
			return compiledRegexRunner;
		}

		// Token: 0x04002E73 RID: 11891
		private DynamicMethod goMethod;

		// Token: 0x04002E74 RID: 11892
		private DynamicMethod findFirstCharMethod;

		// Token: 0x04002E75 RID: 11893
		private DynamicMethod initTrackCountMethod;
	}
}
