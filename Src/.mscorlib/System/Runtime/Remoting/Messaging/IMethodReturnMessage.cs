using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call return message interface.</summary>
	// Token: 0x0200085A RID: 2138
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</summary>
		/// <returns>The number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06005AA8 RID: 23208
		int OutArgCount
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Returns the name of the specified argument marked as a <see langword="ref" /> or an <see langword="out" /> parameter.</summary>
		/// <param name="index">The number of the requested argument name.</param>
		/// <returns>The argument name, or <see langword="null" /> if the current method is not implemented.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005AA9 RID: 23209
		[SecurityCritical]
		string GetOutArgName(int index);

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> or an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The number of the requested argument.</param>
		/// <returns>The specified argument marked as a <see langword="ref" /> or an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005AAA RID: 23210
		[SecurityCritical]
		object GetOutArg(int argNum);

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> or an <see langword="out" /> parameter.</summary>
		/// <returns>The specified argument marked as a <see langword="ref" /> or an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06005AAB RID: 23211
		object[] OutArgs
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the exception thrown during the method call.</summary>
		/// <returns>The exception object for the method call, or <see langword="null" /> if the method did not throw an exception.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005AAC RID: 23212
		Exception Exception
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>The return value of the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005AAD RID: 23213
		object ReturnValue
		{
			[SecurityCritical]
			get;
		}
	}
}
