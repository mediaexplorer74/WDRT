using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EC RID: 1004
	[Guid("e5af3542-ca67-4081-995b-709dd13792df")]
	[ComImport]
	internal interface ICommand_WinRT
	{
		// Token: 0x06002616 RID: 9750
		EventRegistrationToken add_CanExecuteChanged(EventHandler<object> value);

		// Token: 0x06002617 RID: 9751
		void remove_CanExecuteChanged(EventRegistrationToken token);

		// Token: 0x06002618 RID: 9752
		bool CanExecute(object parameter);

		// Token: 0x06002619 RID: 9753
		void Execute(object parameter);
	}
}
