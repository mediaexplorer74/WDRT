using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000693 RID: 1683
	internal class RegexLWCGCompiler : RegexCompiler
	{
		// Token: 0x06003E97 RID: 16023 RVA: 0x001042CF File Offset: 0x001024CF
		internal RegexLWCGCompiler()
		{
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x001042D8 File Offset: 0x001024D8
		internal RegexRunnerFactory FactoryInstanceFromCode(RegexCode code, RegexOptions options)
		{
			this._code = code;
			this._codes = code._codes;
			this._strings = code._strings;
			this._fcPrefix = code._fcPrefix;
			this._bmPrefix = code._bmPrefix;
			this._anchors = code._anchors;
			this._trackcount = code._trackcount;
			this._options = options;
			string text = Interlocked.Increment(ref RegexLWCGCompiler._regexCount).ToString(CultureInfo.InvariantCulture);
			DynamicMethod dynamicMethod = this.DefineDynamicMethod("Go" + text, null, typeof(CompiledRegexRunner));
			base.GenerateGo();
			DynamicMethod dynamicMethod2 = this.DefineDynamicMethod("FindFirstChar" + text, typeof(bool), typeof(CompiledRegexRunner));
			base.GenerateFindFirstChar();
			DynamicMethod dynamicMethod3 = this.DefineDynamicMethod("InitTrackCount" + text, null, typeof(CompiledRegexRunner));
			base.GenerateInitTrackCount();
			return new CompiledRegexRunnerFactory(dynamicMethod, dynamicMethod2, dynamicMethod3);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x001043D0 File Offset: 0x001025D0
		internal DynamicMethod DefineDynamicMethod(string methname, Type returntype, Type hostType)
		{
			MethodAttributes methodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static;
			CallingConventions callingConventions = CallingConventions.Standard;
			DynamicMethod dynamicMethod = new DynamicMethod(methname, methodAttributes, callingConventions, returntype, RegexLWCGCompiler._paramTypes, hostType, false);
			this._ilg = dynamicMethod.GetILGenerator();
			return dynamicMethod;
		}

		// Token: 0x04002DA7 RID: 11687
		private static int _regexCount = 0;

		// Token: 0x04002DA8 RID: 11688
		private static Type[] _paramTypes = new Type[] { typeof(RegexRunner) };
	}
}
