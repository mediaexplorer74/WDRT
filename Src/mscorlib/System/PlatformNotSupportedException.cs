using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a feature does not run on a particular platform.</summary>
	// Token: 0x02000123 RID: 291
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with default properties.</summary>
		// Token: 0x060010EB RID: 4331 RVA: 0x00032E37 File Offset: 0x00031037
		[__DynamicallyInvokable]
		public PlatformNotSupportedException()
			: base(Environment.GetResourceString("Arg_PlatformNotSupported"))
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The text message that explains the reason for the exception.</param>
		// Token: 0x060010EC RID: 4332 RVA: 0x00032E54 File Offset: 0x00031054
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060010ED RID: 4333 RVA: 0x00032E68 File Offset: 0x00031068
		[__DynamicallyInvokable]
		public PlatformNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060010EE RID: 4334 RVA: 0x00032E7D File Offset: 0x0003107D
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
