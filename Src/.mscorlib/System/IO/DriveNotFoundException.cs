using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when trying to access a drive or share that is not available.</summary>
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_DIRECTORYNOTFOUND.</summary>
		// Token: 0x06001794 RID: 6036 RVA: 0x0004BB09 File Offset: 0x00049D09
		public DriveNotFoundException()
			: base(Environment.GetResourceString("Arg_DriveNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified message string and the HRESULT set to COR_E_DIRECTORYNOTFOUND.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001795 RID: 6037 RVA: 0x0004BB26 File Offset: 0x00049D26
		public DriveNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001796 RID: 6038 RVA: 0x0004BB3A File Offset: 0x00049D3A
		public DriveNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the serialized object data about the exception being thrown.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the exception being thrown.</param>
		// Token: 0x06001797 RID: 6039 RVA: 0x0004BB4F File Offset: 0x00049D4F
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
