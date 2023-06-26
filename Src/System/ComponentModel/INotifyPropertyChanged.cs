using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000569 RID: 1385
	[global::__DynamicallyInvokable]
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060033A4 RID: 13220
		// (remove) Token: 0x060033A5 RID: 13221
		[global::__DynamicallyInvokable]
		event PropertyChangedEventHandler PropertyChanged;
	}
}
