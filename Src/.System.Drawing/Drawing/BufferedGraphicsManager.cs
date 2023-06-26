using System;
using System.Runtime.ConstrainedExecution;

namespace System.Drawing
{
	/// <summary>Provides access to the main buffered graphics context object for the application domain.</summary>
	// Token: 0x02000016 RID: 22
	public sealed class BufferedGraphicsManager
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00003800 File Offset: 0x00001A00
		private BufferedGraphicsManager()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006E2F File Offset: 0x0000502F
		static BufferedGraphicsManager()
		{
			AppDomain.CurrentDomain.ProcessExit += BufferedGraphicsManager.OnShutdown;
			AppDomain.CurrentDomain.DomainUnload += BufferedGraphicsManager.OnShutdown;
			BufferedGraphicsManager.bufferedGraphicsContext = new BufferedGraphicsContext();
		}

		/// <summary>Gets the <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</summary>
		/// <returns>The <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006E67 File Offset: 0x00005067
		public static BufferedGraphicsContext Current
		{
			get
			{
				return BufferedGraphicsManager.bufferedGraphicsContext;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006E6E File Offset: 0x0000506E
		[PrePrepareMethod]
		private static void OnShutdown(object sender, EventArgs e)
		{
			BufferedGraphicsManager.Current.Invalidate();
		}

		// Token: 0x04000143 RID: 323
		private static BufferedGraphicsContext bufferedGraphicsContext;
	}
}
