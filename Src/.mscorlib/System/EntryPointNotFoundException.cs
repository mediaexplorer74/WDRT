using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to load a class fails due to the absence of an entry method.</summary>
	// Token: 0x020000DB RID: 219
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class.</summary>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0002BC3B File Offset: 0x00029E3B
		public EntryPointNotFoundException()
			: base(Environment.GetResourceString("Arg_EntryPointNotFoundException"))
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000E29 RID: 3625 RVA: 0x0002BC58 File Offset: 0x00029E58
		public EntryPointNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0002BC6C File Offset: 0x00029E6C
		public EntryPointNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000E2B RID: 3627 RVA: 0x0002BC81 File Offset: 0x00029E81
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
