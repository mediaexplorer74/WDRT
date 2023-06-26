using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an object appears more than once in an array of synchronization objects.</summary>
	// Token: 0x020000D8 RID: 216
	[ComVisible(true)]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0002A8B3 File Offset: 0x00028AB3
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Arg_DuplicateWaitObjectException");
				}
				return DuplicateWaitObjectException._duplicateWaitObjectMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class.</summary>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002A8D6 File Offset: 0x00028AD6
		public DuplicateWaitObjectException()
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception.</param>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002A8EE File Offset: 0x00028AEE
		public DuplicateWaitObjectException(string parameterName)
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception.</param>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002A907 File Offset: 0x00028B07
		public DuplicateWaitObjectException(string parameterName, string message)
			: base(message, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002A91C File Offset: 0x00028B1C
		public DuplicateWaitObjectException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002A931 File Offset: 0x00028B31
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000567 RID: 1383
		private static volatile string _duplicateWaitObjectMessage;
	}
}
