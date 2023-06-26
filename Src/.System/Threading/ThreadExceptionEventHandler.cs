using System;

namespace System.Threading
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event of an <see cref="T:System.Windows.Forms.Application" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Threading.ThreadExceptionEventArgs" /> that contains the event data.</param>
	// Token: 0x020003D8 RID: 984
	// (Invoke) Token: 0x060025D3 RID: 9683
	public delegate void ThreadExceptionEventHandler(object sender, ThreadExceptionEventArgs e);
}
