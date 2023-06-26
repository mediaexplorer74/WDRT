using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A6 RID: 1702
	internal sealed class RegexTree
	{
		// Token: 0x06003FAA RID: 16298 RVA: 0x0010B526 File Offset: 0x00109726
		internal RegexTree(RegexNode root, Hashtable caps, int[] capnumlist, int captop, Hashtable capnames, string[] capslist, RegexOptions opts)
		{
			this._root = root;
			this._caps = caps;
			this._capnumlist = capnumlist;
			this._capnames = capnames;
			this._capslist = capslist;
			this._captop = captop;
			this._options = opts;
		}

		// Token: 0x04002E5D RID: 11869
		internal RegexNode _root;

		// Token: 0x04002E5E RID: 11870
		internal Hashtable _caps;

		// Token: 0x04002E5F RID: 11871
		internal int[] _capnumlist;

		// Token: 0x04002E60 RID: 11872
		internal Hashtable _capnames;

		// Token: 0x04002E61 RID: 11873
		internal string[] _capslist;

		// Token: 0x04002E62 RID: 11874
		internal RegexOptions _options;

		// Token: 0x04002E63 RID: 11875
		internal int _captop;
	}
}
