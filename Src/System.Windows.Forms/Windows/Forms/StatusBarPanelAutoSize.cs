using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a <see cref="T:System.Windows.Forms.StatusBarPanel" /> on a <see cref="T:System.Windows.Forms.StatusBar" /> control behaves when the control resizes.</summary>
	// Token: 0x02000377 RID: 887
	public enum StatusBarPanelAutoSize
	{
		/// <summary>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> does not change size when the <see cref="T:System.Windows.Forms.StatusBar" /> control resizes.</summary>
		// Token: 0x040022F2 RID: 8946
		None = 1,
		/// <summary>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> shares the available space on the <see cref="T:System.Windows.Forms.StatusBar" /> (the space not taken up by other panels whose <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property is set to <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.None" /> or <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.Contents" />) with other panels that have their <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property set to <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.Spring" />.</summary>
		// Token: 0x040022F3 RID: 8947
		Spring,
		/// <summary>The width of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> is determined by its contents.</summary>
		// Token: 0x040022F4 RID: 8948
		Contents
	}
}
