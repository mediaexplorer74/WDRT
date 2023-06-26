using System;
using System.ComponentModel;

namespace System.Text.RegularExpressions
{
	/// <summary>Creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> class for a compiled regular expression.</summary>
	// Token: 0x020006A5 RID: 1701
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class RegexRunnerFactory
	{
		/// <summary>When overridden in a derived class, creates a <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object for a specific compiled regular expression.</summary>
		/// <returns>A <see cref="T:System.Text.RegularExpressions.RegexRunner" /> object designed to execute a specific compiled regular expression.</returns>
		// Token: 0x06003FA9 RID: 16297
		protected internal abstract RegexRunner CreateInstance();
	}
}
