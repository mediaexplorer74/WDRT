using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x02000566 RID: 1382
	public interface INestedSite : ISite, IServiceProvider
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600339D RID: 13213
		string FullName { get; }
	}
}
