﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.</summary>
	// Token: 0x02000154 RID: 340
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class.</summary>
		// Token: 0x06001558 RID: 5464 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		[__DynamicallyInvokable]
		public UnauthorizedAccessException()
			: base(Environment.GetResourceString("Arg_UnauthorizedAccessException"))
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06001559 RID: 5465 RVA: 0x0003E5F1 File Offset: 0x0003C7F1
		[__DynamicallyInvokable]
		public UnauthorizedAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600155A RID: 5466 RVA: 0x0003E605 File Offset: 0x0003C805
		[__DynamicallyInvokable]
		public UnauthorizedAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600155B RID: 5467 RVA: 0x0003E61A File Offset: 0x0003C81A
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
