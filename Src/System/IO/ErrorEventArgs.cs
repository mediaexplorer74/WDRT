using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	// Token: 0x020003FB RID: 1019
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred.</param>
		// Token: 0x06002648 RID: 9800 RVA: 0x000B0906 File Offset: 0x000AEB06
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		// Token: 0x06002649 RID: 9801 RVA: 0x000B0915 File Offset: 0x000AEB15
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x040020A0 RID: 8352
		private Exception exception;
	}
}
