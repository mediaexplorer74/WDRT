using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a DLL specified in a DLL import cannot be found.</summary>
	// Token: 0x020000DC RID: 220
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with default properties.</summary>
		// Token: 0x06000E2C RID: 3628 RVA: 0x0002BC8B File Offset: 0x00029E8B
		[__DynamicallyInvokable]
		public DllNotFoundException()
			: base(Environment.GetResourceString("Arg_DllNotFoundException"))
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000E2D RID: 3629 RVA: 0x0002BCA8 File Offset: 0x00029EA8
		[__DynamicallyInvokable]
		public DllNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E2E RID: 3630 RVA: 0x0002BCBC File Offset: 0x00029EBC
		[__DynamicallyInvokable]
		public DllNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000E2F RID: 3631 RVA: 0x0002BCD1 File Offset: 0x00029ED1
		protected DllNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
