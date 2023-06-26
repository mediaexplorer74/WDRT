using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method message interface.</summary>
	// Token: 0x02000858 RID: 2136
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		/// <summary>Gets the URI of the specific object that the call is destined for.</summary>
		/// <returns>The URI of the remote object that contains the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005A99 RID: 23193
		string Uri
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>The name of the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005A9A RID: 23194
		string MethodName
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</summary>
		/// <returns>The full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005A9B RID: 23195
		string TypeName
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets an object containing the method signature.</summary>
		/// <returns>An object containing the method signature.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005A9C RID: 23196
		object MethodSignature
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>The number of arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005A9D RID: 23197
		int ArgCount
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the name of the argument passed to the method.</summary>
		/// <param name="index">The number of the requested argument.</param>
		/// <returns>The name of the specified argument passed to the method, or <see langword="null" /> if the current method is not implemented.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005A9E RID: 23198
		[SecurityCritical]
		string GetArgName(int index);

		/// <summary>Gets a specific argument as an <see cref="T:System.Object" />.</summary>
		/// <param name="argNum">The number of the requested argument.</param>
		/// <returns>The argument passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005A9F RID: 23199
		[SecurityCritical]
		object GetArg(int argNum);

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing the arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005AA0 RID: 23200
		object[] Args
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets a value indicating whether the message has variable arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005AA1 RID: 23201
		bool HasVarArgs
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005AA2 RID: 23202
		LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005AA3 RID: 23203
		MethodBase MethodBase
		{
			[SecurityCritical]
			get;
		}
	}
}
