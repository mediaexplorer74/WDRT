using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" />, <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" />, <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" />, or <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> event of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
	// Token: 0x02000419 RID: 1049
	// (Invoke) Token: 0x060049BE RID: 18878
	public delegate void TreeViewCancelEventHandler(object sender, TreeViewCancelEventArgs e);
}
