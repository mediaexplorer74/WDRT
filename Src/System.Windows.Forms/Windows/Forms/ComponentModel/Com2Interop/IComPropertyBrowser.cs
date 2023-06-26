using System;
using System.ComponentModel.Design;
using Microsoft.Win32;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	/// <summary>Allows Visual Studio to communicate internally with the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	// Token: 0x020004B4 RID: 1204
	public interface IComPropertyBrowser
	{
		/// <summary>Closes any open drop-down controls on the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		// Token: 0x06004F6F RID: 20335
		void DropDownDone();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004F70 RID: 20336
		bool InPropertySet { get; }

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is browsing a COM object and the user renames the object.</summary>
		// Token: 0x14000415 RID: 1045
		// (add) Token: 0x06004F71 RID: 20337
		// (remove) Token: 0x06004F72 RID: 20338
		event ComponentRenameEventHandler ComComponentNameChanged;

		/// <summary>Commits all pending changes to the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.PropertyGrid" /> successfully commits changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004F73 RID: 20339
		bool EnsurePendingChangesCommitted();

		/// <summary>Activates the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when the user chooses Properties for a control in Design view.</summary>
		// Token: 0x06004F74 RID: 20340
		void HandleF4();

		/// <summary>Loads user states from the registry into the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
		/// <param name="key">The registry key that contains the user states.</param>
		// Token: 0x06004F75 RID: 20341
		void LoadState(RegistryKey key);

		/// <summary>Saves user states from the <see cref="T:System.Windows.Forms.PropertyGrid" /> control to the registry.</summary>
		/// <param name="key">The registry key that contains the user states.</param>
		// Token: 0x06004F76 RID: 20342
		void SaveState(RegistryKey key);
	}
}
