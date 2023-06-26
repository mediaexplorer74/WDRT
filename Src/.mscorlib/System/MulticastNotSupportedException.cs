using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to combine two delegates based on the <see cref="T:System.Delegate" /> type instead of the <see cref="T:System.MulticastDelegate" /> type. This class cannot be inherited.</summary>
	// Token: 0x02000112 RID: 274
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class.</summary>
		// Token: 0x0600106E RID: 4206 RVA: 0x00031460 File Offset: 0x0002F660
		public MulticastNotSupportedException()
			: base(Environment.GetResourceString("Arg_MulticastNotSupportedException"))
		{
			base.SetErrorCode(-2146233068);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x0600106F RID: 4207 RVA: 0x0003147D File Offset: 0x0002F67D
		public MulticastNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233068);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001070 RID: 4208 RVA: 0x00031491 File Offset: 0x0002F691
		public MulticastNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000314A6 File Offset: 0x0002F6A6
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
