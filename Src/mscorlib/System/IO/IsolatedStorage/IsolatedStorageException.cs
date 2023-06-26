using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	/// <summary>The exception that is thrown when an operation in isolated storage fails.</summary>
	// Token: 0x020001B1 RID: 433
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with default properties.</summary>
		// Token: 0x06001B4B RID: 6987 RVA: 0x0005C906 File Offset: 0x0005AB06
		public IsolatedStorageException()
			: base(Environment.GetResourceString("IsolatedStorage_Exception"))
		{
			base.SetErrorCode(-2146233264);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001B4C RID: 6988 RVA: 0x0005C923 File Offset: 0x0005AB23
		public IsolatedStorageException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233264);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001B4D RID: 6989 RVA: 0x0005C937 File Offset: 0x0005AB37
		public IsolatedStorageException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233264);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001B4E RID: 6990 RVA: 0x0005C94C File Offset: 0x0005AB4C
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
