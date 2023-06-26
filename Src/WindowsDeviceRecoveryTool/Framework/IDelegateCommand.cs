using System;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x0200008E RID: 142
	public interface IDelegateCommand : ICommand
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004B6 RID: 1206
		KeyGesture KeyGesture { get; }

		// Token: 0x060004B7 RID: 1207
		void RaiseCanExecuteChanged();
	}
}
