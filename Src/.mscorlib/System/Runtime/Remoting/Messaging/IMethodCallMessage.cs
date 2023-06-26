using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call message interface.</summary>
	// Token: 0x02000859 RID: 2137
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the number of arguments in the call that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>The number of arguments in the call that are not marked as <see langword="out" /> parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005AA4 RID: 23204
		int InArgCount
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Returns the name of the specified argument that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="index">The number of the requested <see langword="in" /> argument.</param>
		/// <returns>The name of a specific argument that is not marked as an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005AA5 RID: 23205
		[SecurityCritical]
		string GetInArgName(int index);

		/// <summary>Returns the specified argument that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The number of the requested <see langword="in" /> argument.</param>
		/// <returns>The requested argument that is not marked as an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005AA6 RID: 23206
		[SecurityCritical]
		object GetInArg(int argNum);

		/// <summary>Gets an array of arguments that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>An array of arguments that are not marked as <see langword="out" /> parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06005AA7 RID: 23207
		object[] InArgs
		{
			[SecurityCritical]
			get;
		}
	}
}
