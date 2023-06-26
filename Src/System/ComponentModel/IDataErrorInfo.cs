using System;

namespace System.ComponentModel
{
	/// <summary>Provides the functionality to offer custom error information that a user interface can bind to.</summary>
	// Token: 0x0200055F RID: 1375
	public interface IDataErrorInfo
	{
		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <param name="columnName">The name of the property whose error message to get.</param>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		// Token: 0x17000C98 RID: 3224
		string this[string columnName] { get; }

		/// <summary>Gets an error message indicating what is wrong with this object.</summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600338D RID: 13197
		string Error { get; }
	}
}
