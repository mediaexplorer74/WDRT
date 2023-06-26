using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryAccessibilityHelp" /> event.</summary>
	// Token: 0x02000336 RID: 822
	[ComVisible(true)]
	public class QueryAccessibilityHelpEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
		// Token: 0x06003566 RID: 13670 RVA: 0x0009081B File Offset: 0x0008EA1B
		public QueryAccessibilityHelpEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
		/// <param name="helpNamespace">The file containing Help for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		/// <param name="helpString">The string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		/// <param name="helpKeyword">The keyword to associate with the Help request for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		// Token: 0x06003567 RID: 13671 RVA: 0x000F25EF File Offset: 0x000F07EF
		public QueryAccessibilityHelpEventArgs(string helpNamespace, string helpString, string helpKeyword)
		{
			this.helpNamespace = helpNamespace;
			this.helpString = helpString;
			this.helpKeyword = helpKeyword;
		}

		/// <summary>Gets or sets a value specifying the name of the Help file.</summary>
		/// <returns>The name of the Help file. This name can be in the form C:\path\sample.chm or /folder/file.htm.</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000F260C File Offset: 0x000F080C
		// (set) Token: 0x06003569 RID: 13673 RVA: 0x000F2614 File Offset: 0x000F0814
		public string HelpNamespace
		{
			get
			{
				return this.helpNamespace;
			}
			set
			{
				this.helpNamespace = value;
			}
		}

		/// <summary>Gets or sets the string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
		/// <returns>The Help to retrieve for the accessible object.</returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000F261D File Offset: 0x000F081D
		// (set) Token: 0x0600356B RID: 13675 RVA: 0x000F2625 File Offset: 0x000F0825
		public string HelpString
		{
			get
			{
				return this.helpString;
			}
			set
			{
				this.helpString = value;
			}
		}

		/// <summary>Gets or sets the Help keyword for the specified control.</summary>
		/// <returns>The Help topic associated with the <see cref="T:System.Windows.Forms.AccessibleObject" /> that was queried.</returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000F262E File Offset: 0x000F082E
		// (set) Token: 0x0600356D RID: 13677 RVA: 0x000F2636 File Offset: 0x000F0836
		public string HelpKeyword
		{
			get
			{
				return this.helpKeyword;
			}
			set
			{
				this.helpKeyword = value;
			}
		}

		// Token: 0x04001F48 RID: 8008
		private string helpNamespace;

		// Token: 0x04001F49 RID: 8009
		private string helpString;

		// Token: 0x04001F4A RID: 8010
		private string helpKeyword;
	}
}
