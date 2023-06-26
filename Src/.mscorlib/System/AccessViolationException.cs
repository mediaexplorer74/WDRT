using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to read or write protected memory.</summary>
	// Token: 0x0200008F RID: 143
	[ComVisible(true)]
	[Serializable]
	public class AccessViolationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000769 RID: 1897 RVA: 0x00019F3F File Offset: 0x0001813F
		public AccessViolationException()
			: base(Environment.GetResourceString("Arg_AccessViolationException"))
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x0600076A RID: 1898 RVA: 0x00019F5C File Offset: 0x0001815C
		public AccessViolationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600076B RID: 1899 RVA: 0x00019F70 File Offset: 0x00018170
		public AccessViolationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600076C RID: 1900 RVA: 0x00019F85 File Offset: 0x00018185
		protected AccessViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000377 RID: 887
		private IntPtr _ip;

		// Token: 0x04000378 RID: 888
		private IntPtr _target;

		// Token: 0x04000379 RID: 889
		private int _accessType;
	}
}
