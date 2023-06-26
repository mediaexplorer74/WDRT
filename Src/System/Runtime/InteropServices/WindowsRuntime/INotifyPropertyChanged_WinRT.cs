using System;
using System.ComponentModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EB RID: 1003
	[Guid("cf75d69c-f2f4-486b-b302-bb4c09baebfa")]
	[ComImport]
	internal interface INotifyPropertyChanged_WinRT
	{
		// Token: 0x06002614 RID: 9748
		EventRegistrationToken add_PropertyChanged(PropertyChangedEventHandler value);

		// Token: 0x06002615 RID: 9749
		void remove_PropertyChanged(EventRegistrationToken token);
	}
}
