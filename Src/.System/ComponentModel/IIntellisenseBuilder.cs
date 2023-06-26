using System;

namespace System.ComponentModel
{
	/// <summary>Provides an interface to facilitate the retrieval of the builder's name and to display the builder.</summary>
	// Token: 0x02000562 RID: 1378
	public interface IIntellisenseBuilder
	{
		/// <summary>Gets a localized name.</summary>
		/// <returns>A localized name.</returns>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003392 RID: 13202
		string Name { get; }

		/// <summary>Shows the builder.</summary>
		/// <param name="language">The language service that is calling the builder.</param>
		/// <param name="value">The expression being edited.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns>
		///   <see langword="true" /> if the value should be replaced with <paramref name="newValue" />; otherwise, <see langword="false" /> (if the user cancels, for example).</returns>
		// Token: 0x06003393 RID: 13203
		bool Show(string language, string value, ref string newValue);
	}
}
