using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to either the standard output or the standard error stream.</summary>
	// Token: 0x02000493 RID: 1171
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with trace output written to the standard output stream.</summary>
		// Token: 0x06002B51 RID: 11089 RVA: 0x000C4CAC File Offset: 0x000C2EAC
		public ConsoleTraceListener()
			: base(Console.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with an option to write trace output to the standard output stream or the standard error stream.</summary>
		/// <param name="useErrorStream">
		///   <see langword="true" /> to write tracing and debugging output to the standard error stream; <see langword="false" /> to write tracing and debugging output to the standard output stream.</param>
		// Token: 0x06002B52 RID: 11090 RVA: 0x000C4CB9 File Offset: 0x000C2EB9
		public ConsoleTraceListener(bool useErrorStream)
			: base(useErrorStream ? Console.Error : Console.Out)
		{
		}

		/// <summary>Closes the output to the stream specified for this trace listener.</summary>
		// Token: 0x06002B53 RID: 11091 RVA: 0x000C4CD0 File Offset: 0x000C2ED0
		public override void Close()
		{
		}
	}
}
