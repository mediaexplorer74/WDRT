using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x020002DF RID: 735
	[global::__DynamicallyInvokable]
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x060019DE RID: 6622 RVA: 0x0007E085 File Offset: 0x0007C285
		[global::__DynamicallyInvokable]
		public NetworkInformationException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A <see langword="Win32" /> error code.</param>
		// Token: 0x060019DF RID: 6623 RVA: 0x0007E092 File Offset: 0x0007C292
		[global::__DynamicallyInvokable]
		public NetworkInformationException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0007E09B File Offset: 0x0007C29B
		internal NetworkInformationException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data.</param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception.</param>
		// Token: 0x060019E1 RID: 6625 RVA: 0x0007E0A4 File Offset: 0x0007C2A4
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the <see langword="Win32" /> error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the <see langword="Win32" /> error code.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0007E0AE File Offset: 0x0007C2AE
		[global::__DynamicallyInvokable]
		public override int ErrorCode
		{
			[global::__DynamicallyInvokable]
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
