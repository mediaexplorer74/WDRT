using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate how a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed.</summary>
	// Token: 0x020001C8 RID: 456
	public enum DataGridViewComboBoxDisplayStyle
	{
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> mimics the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		// Token: 0x04000D84 RID: 3460
		ComboBox,
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed with a drop-down button but does not otherwise mimic the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		// Token: 0x04000D85 RID: 3461
		DropDownButton,
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed without a drop-down button.</summary>
		// Token: 0x04000D86 RID: 3462
		Nothing
	}
}
