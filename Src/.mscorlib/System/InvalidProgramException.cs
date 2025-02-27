﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.</summary>
	// Token: 0x02000101 RID: 257
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with default properties.</summary>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00030271 File Offset: 0x0002E471
		[__DynamicallyInvokable]
		public InvalidProgramException()
			: base(Environment.GetResourceString("InvalidProgram_Default"))
		{
			base.SetErrorCode(-2146233030);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003028E File Offset: 0x0002E48E
		[__DynamicallyInvokable]
		public InvalidProgramException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233030);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x000302A2 File Offset: 0x0002E4A2
		[__DynamicallyInvokable]
		public InvalidProgramException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233030);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000302B7 File Offset: 0x0002E4B7
		internal InvalidProgramException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
