using System;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event.</summary>
	// Token: 0x0200006F RID: 111
	public class QueryPageSettingsEventArgs : PrintEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> class.</summary>
		/// <param name="pageSettings">The page settings for the page to be printed.</param>
		// Token: 0x06000816 RID: 2070 RVA: 0x00020BA7 File Offset: 0x0001EDA7
		public QueryPageSettingsEventArgs(PageSettings pageSettings)
		{
			this.pageSettings = pageSettings;
		}

		/// <summary>Gets or sets the page settings for the page to be printed.</summary>
		/// <returns>The page settings for the page to be printed.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00020BB6 File Offset: 0x0001EDB6
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00020BC5 File Offset: 0x0001EDC5
		public PageSettings PageSettings
		{
			get
			{
				this.PageSettingsChanged = true;
				return this.pageSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PageSettings();
				}
				this.pageSettings = value;
				this.PageSettingsChanged = true;
			}
		}

		// Token: 0x040006FE RID: 1790
		private PageSettings pageSettings;

		// Token: 0x040006FF RID: 1791
		internal bool PageSettingsChanged;
	}
}
