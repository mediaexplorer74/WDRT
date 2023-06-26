using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides access to the designer options located on the Tools menu under the Options command in the Visual Studio development environment.</summary>
	// Token: 0x020005EA RID: 1514
	public interface IDesignerOptionService
	{
		/// <summary>Gets the value of the specified Windows Forms Designer option.</summary>
		/// <param name="pageName">The name of the page that defines the option.</param>
		/// <param name="valueName">The name of the option property.</param>
		/// <returns>The value of the specified option.</returns>
		// Token: 0x0600380B RID: 14347
		object GetOptionValue(string pageName, string valueName);

		/// <summary>Sets the value of the specified Windows Forms Designer option.</summary>
		/// <param name="pageName">The name of the page that defines the option.</param>
		/// <param name="valueName">The name of the option property.</param>
		/// <param name="value">The new value.</param>
		// Token: 0x0600380C RID: 14348
		void SetOptionValue(string pageName, string valueName, object value);
	}
}
