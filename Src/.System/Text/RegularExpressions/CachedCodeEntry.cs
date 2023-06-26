using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000687 RID: 1671
	internal sealed class CachedCodeEntry
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x000FCD44 File Offset: 0x000FAF44
		internal CachedCodeEntry(string key, Hashtable capnames, string[] capslist, RegexCode code, Hashtable caps, int capsize, ExclusiveReference runner, SharedReference repl)
		{
			this._key = key;
			this._capnames = capnames;
			this._capslist = capslist;
			this._code = code;
			this._caps = caps;
			this._capsize = capsize;
			this._runnerref = runner;
			this._replref = repl;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000FCD94 File Offset: 0x000FAF94
		internal void AddCompiled(RegexRunnerFactory factory)
		{
			this._factory = factory;
			this._code = null;
		}

		// Token: 0x04002CCC RID: 11468
		internal string _key;

		// Token: 0x04002CCD RID: 11469
		internal RegexCode _code;

		// Token: 0x04002CCE RID: 11470
		internal Hashtable _caps;

		// Token: 0x04002CCF RID: 11471
		internal Hashtable _capnames;

		// Token: 0x04002CD0 RID: 11472
		internal string[] _capslist;

		// Token: 0x04002CD1 RID: 11473
		internal int _capsize;

		// Token: 0x04002CD2 RID: 11474
		internal RegexRunnerFactory _factory;

		// Token: 0x04002CD3 RID: 11475
		internal ExclusiveReference _runnerref;

		// Token: 0x04002CD4 RID: 11476
		internal SharedReference _replref;
	}
}
