using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	// Token: 0x020003D7 RID: 983
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred.</param>
		// Token: 0x060025D0 RID: 9680 RVA: 0x000AFC0E File Offset: 0x000ADE0E
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000AFC1D File Offset: 0x000ADE1D
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x0400205A RID: 8282
		private Exception exception;
	}
}
