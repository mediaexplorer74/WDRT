using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	/// <summary>Defines a method that shows the property page for an ActiveX control.</summary>
	// Token: 0x020004B3 RID: 1203
	public interface ICom2PropertyPageDisplayService
	{
		/// <summary>Shows a property page for the specified object.</summary>
		/// <param name="title">A string that is the title of the property page.</param>
		/// <param name="component">The object for which the property page is created.</param>
		/// <param name="dispid">The DispID of the property that is highlighted when the property page is created.</param>
		/// <param name="pageGuid">The GUID for the property page.</param>
		/// <param name="parentHandle">A IntPtr that is the handle of the parent control of the property page.</param>
		// Token: 0x06004F6E RID: 20334
		void ShowPropertyPage(string title, object component, int dispid, Guid pageGuid, IntPtr parentHandle);
	}
}
