using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers that indicate special features of a language.</summary>
	// Token: 0x02000682 RID: 1666
	[Flags]
	[Serializable]
	public enum LanguageOptions
	{
		/// <summary>The language has default characteristics.</summary>
		// Token: 0x04002CA8 RID: 11432
		None = 0,
		/// <summary>The language is case-insensitive.</summary>
		// Token: 0x04002CA9 RID: 11433
		CaseInsensitive = 1
	}
}
