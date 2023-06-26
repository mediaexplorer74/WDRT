using System;

namespace System
{
	/// <summary>Encapsulates a method that has a single parameter and does not return a value.</summary>
	/// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
	/// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
	// Token: 0x02000040 RID: 64
	// (Invoke) Token: 0x06000238 RID: 568
	[__DynamicallyInvokable]
	public delegate void Action<in T>(T obj);
}
