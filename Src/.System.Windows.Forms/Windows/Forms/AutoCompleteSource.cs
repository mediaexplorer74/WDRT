using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the source for <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> automatic completion functionality.</summary>
	// Token: 0x02000127 RID: 295
	public enum AutoCompleteSource
	{
		/// <summary>Specifies the file system as the source.</summary>
		// Token: 0x04000605 RID: 1541
		FileSystem = 1,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the history list.</summary>
		// Token: 0x04000606 RID: 1542
		HistoryList,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the list of those URLs most recently used.</summary>
		// Token: 0x04000607 RID: 1543
		RecentlyUsedList = 4,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.HistoryList" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.RecentlyUsedList" /> as the source.</summary>
		// Token: 0x04000608 RID: 1544
		AllUrl = 6,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.FileSystem" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.AllUrl" /> as the source. This is the default value when <see cref="T:System.Windows.Forms.AutoCompleteMode" /> has been set to a value other than the default.</summary>
		// Token: 0x04000609 RID: 1545
		AllSystemSources,
		/// <summary>Specifies that only directory names and not file names will be automatically completed.</summary>
		// Token: 0x0400060A RID: 1546
		FileSystemDirectories = 32,
		/// <summary>Specifies strings from a built-in <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> as the source.</summary>
		// Token: 0x0400060B RID: 1547
		CustomSource = 64,
		/// <summary>Specifies that no <see cref="T:System.Windows.Forms.AutoCompleteSource" /> is currently in use. This is the default value of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</summary>
		// Token: 0x0400060C RID: 1548
		None = 128,
		/// <summary>Specifies that the items of the <see cref="T:System.Windows.Forms.ComboBox" /> represent the source.</summary>
		// Token: 0x0400060D RID: 1549
		ListItems = 256
	}
}
