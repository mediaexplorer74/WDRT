using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x020005AD RID: 1453
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to <see langword="null" />.</summary>
		// Token: 0x06004382 RID: 17282 RVA: 0x000FBF09 File Offset: 0x000FA109
		[__DynamicallyInvokable]
		public AmbiguousMatchException()
			: base(Environment.GetResourceString("RFLCT.Ambiguous"))
		{
			base.SetErrorCode(-2147475171);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to <see langword="null" />.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown.</param>
		// Token: 0x06004383 RID: 17283 RVA: 0x000FBF26 File Offset: 0x000FA126
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147475171);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06004384 RID: 17284 RVA: 0x000FBF3A File Offset: 0x000FA13A
		[__DynamicallyInvokable]
		public AmbiguousMatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000FBF4F File Offset: 0x000FA14F
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
