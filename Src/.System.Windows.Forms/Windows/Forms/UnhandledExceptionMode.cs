using System;

namespace System.Windows.Forms
{
	/// <summary>Defines where a Windows Forms application should send unhandled exceptions.</summary>
	// Token: 0x02000305 RID: 773
	public enum UnhandledExceptionMode
	{
		/// <summary>Route all exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler, unless the application's configuration file specifies otherwise.</summary>
		// Token: 0x04001E1B RID: 7707
		Automatic,
		/// <summary>Never route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
		// Token: 0x04001E1C RID: 7708
		ThrowException,
		/// <summary>Always route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
		// Token: 0x04001E1D RID: 7709
		CatchException
	}
}
