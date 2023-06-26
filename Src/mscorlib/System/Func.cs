using System;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Encapsulates a method that has no parameters and returns a value of the type specified by the <typeparamref name="TResult" /> parameter.</summary>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	// Token: 0x02000045 RID: 69
	// (Invoke) Token: 0x0600024C RID: 588
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	public delegate TResult Func<out TResult>();
}
