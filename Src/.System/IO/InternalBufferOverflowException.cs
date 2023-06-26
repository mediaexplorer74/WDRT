using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	// Token: 0x02000401 RID: 1025
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x0600268B RID: 9867 RVA: 0x000B1860 File Offset: 0x000AFA60
		public InternalBufferOverflowException()
		{
			base.HResult = -2146232059;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception.</param>
		// Token: 0x0600268C RID: 9868 RVA: 0x000B1873 File Offset: 0x000AFA73
		public InternalBufferOverflowException(string message)
			: base(message)
		{
			base.HResult = -2146232059;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception.</param>
		/// <param name="inner">The inner exception.</param>
		// Token: 0x0600268D RID: 9869 RVA: 0x000B1887 File Offset: 0x000AFA87
		public InternalBufferOverflowException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232059;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x0600268E RID: 9870 RVA: 0x000B189C File Offset: 0x000AFA9C
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
