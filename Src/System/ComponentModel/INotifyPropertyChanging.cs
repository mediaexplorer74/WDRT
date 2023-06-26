using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value is changing.</summary>
	// Token: 0x0200056A RID: 1386
	[global::__DynamicallyInvokable]
	public interface INotifyPropertyChanging
	{
		/// <summary>Occurs when a property value is changing.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060033A6 RID: 13222
		// (remove) Token: 0x060033A7 RID: 13223
		[global::__DynamicallyInvokable]
		event PropertyChangingEventHandler PropertyChanging;
	}
}
