using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x02000364 RID: 868
	[global::__DynamicallyInvokable]
	[Serializable]
	public class SocketException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06001FBA RID: 8122 RVA: 0x00094B77 File Offset: 0x00092D77
		[global::__DynamicallyInvokable]
		public SocketException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00094B84 File Offset: 0x00092D84
		internal SocketException(EndPoint endPoint)
			: base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		// Token: 0x06001FBC RID: 8124 RVA: 0x00094B98 File Offset: 0x00092D98
		[global::__DynamicallyInvokable]
		public SocketException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00094BA1 File Offset: 0x00092DA1
		internal SocketException(int errorCode, EndPoint endPoint)
			: base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00094BB1 File Offset: 0x00092DB1
		internal SocketException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance.</param>
		// Token: 0x06001FBF RID: 8127 RVA: 0x00094BBA File Offset: 0x00092DBA
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x00094BC4 File Offset: 0x00092DC4
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message.</returns>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x00094BCC File Offset: 0x00092DCC
		[global::__DynamicallyInvokable]
		public override string Message
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x00094BF8 File Offset: 0x00092DF8
		[global::__DynamicallyInvokable]
		public SocketError SocketErrorCode
		{
			[global::__DynamicallyInvokable]
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x04001D61 RID: 7521
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
