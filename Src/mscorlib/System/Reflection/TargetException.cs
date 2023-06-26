using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents the exception that is thrown when an attempt is made to invoke an invalid target.</summary>
	// Token: 0x0200061F RID: 1567
	[ComVisible(true)]
	[Serializable]
	public class TargetException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with an empty message and the root cause of the exception.</summary>
		// Token: 0x060048CA RID: 18634 RVA: 0x00109087 File Offset: 0x00107287
		public TargetException()
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the given message and the root cause exception.</summary>
		/// <param name="message">A <see langword="String" /> describing the reason why the exception occurred.</param>
		// Token: 0x060048CB RID: 18635 RVA: 0x0010909A File Offset: 0x0010729A
		public TargetException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060048CC RID: 18636 RVA: 0x001090AE File Offset: 0x001072AE
		public TargetException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object.</param>
		/// <param name="context">The source of and destination for the object.</param>
		// Token: 0x060048CD RID: 18637 RVA: 0x001090C3 File Offset: 0x001072C3
		protected TargetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
