using System;

namespace System.Net
{
	/// <summary>Provides the base interface to load and execute scripts for automatic proxy detection.</summary>
	// Token: 0x02000193 RID: 403
	public interface IWebProxyScript
	{
		/// <summary>Loads a script.</summary>
		/// <param name="scriptLocation">Internal only.</param>
		/// <param name="script">Internal only.</param>
		/// <param name="helperType">Internal only.</param>
		/// <returns>A <see cref="T:System.Boolean" /> indicating whether the script was successfully loaded.</returns>
		// Token: 0x06000F94 RID: 3988
		bool Load(Uri scriptLocation, string script, Type helperType);

		/// <summary>Runs a script.</summary>
		/// <param name="url">Internal only.</param>
		/// <param name="host">Internal only.</param>
		/// <returns>A <see cref="T:System.String" />.  
		///  An internal-only value returned.</returns>
		// Token: 0x06000F95 RID: 3989
		string Run(string url, string host);

		/// <summary>Closes a script.</summary>
		// Token: 0x06000F96 RID: 3990
		void Close();
	}
}
