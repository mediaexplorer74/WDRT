using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>The exception that is thrown when recursive entry into a lock is not compatible with the recursion policy for the lock.</summary>
	// Token: 0x020004FD RID: 1277
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class LockRecursionException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06003C73 RID: 15475 RVA: 0x000E5A6D File Offset: 0x000E3C6D
		[__DynamicallyInvokable]
		public LockRecursionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must make sure that this string has been localized for the current system culture.</param>
		// Token: 0x06003C74 RID: 15476 RVA: 0x000E5A75 File Offset: 0x000E3C75
		[__DynamicallyInvokable]
		public LockRecursionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06003C75 RID: 15477 RVA: 0x000E5A7E File Offset: 0x000E3C7E
		protected LockRecursionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must make sure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that caused the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003C76 RID: 15478 RVA: 0x000E5A88 File Offset: 0x000E3C88
		[__DynamicallyInvokable]
		public LockRecursionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
