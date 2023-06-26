using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x02000935 RID: 2357
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported.</param>
		// Token: 0x06006060 RID: 24672 RVA: 0x0014D5FD File Offset: 0x0014B7FD
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		/// <summary>Gets the alias for the field or parameter as found in the type library when it was imported.</summary>
		/// <returns>The alias for the field or parameter as found in the type library when it was imported.</returns>
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x0014D60C File Offset: 0x0014B80C
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002B29 RID: 11049
		internal string _val;
	}
}
