using System;
using System.Threading;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000081 RID: 129
	public interface IAsyncDelegateCommand : IDelegateCommand, ICommand
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600046C RID: 1132
		CancellationTokenSource CancellationTokenSource { get; }

		// Token: 0x0600046D RID: 1133
		void Cancel();

		// Token: 0x0600046E RID: 1134
		void Wait();
	}
}
