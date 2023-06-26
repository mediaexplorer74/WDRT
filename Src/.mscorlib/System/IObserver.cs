using System;

namespace System
{
	/// <summary>Provides a mechanism for receiving push-based notifications.</summary>
	/// <typeparam name="T">The object that provides notification information.</typeparam>
	// Token: 0x020000F3 RID: 243
	[__DynamicallyInvokable]
	public interface IObserver<in T>
	{
		/// <summary>Provides the observer with new data.</summary>
		/// <param name="value">The current notification information.</param>
		// Token: 0x06000F05 RID: 3845
		[__DynamicallyInvokable]
		void OnNext(T value);

		/// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
		/// <param name="error">An object that provides additional information about the error.</param>
		// Token: 0x06000F06 RID: 3846
		[__DynamicallyInvokable]
		void OnError(Exception error);

		/// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
		// Token: 0x06000F07 RID: 3847
		[__DynamicallyInvokable]
		void OnCompleted();
	}
}
