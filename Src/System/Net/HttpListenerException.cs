using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs processing an HTTP request.</summary>
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class.</summary>
		// Token: 0x060008F4 RID: 2292 RVA: 0x00032A60 File Offset: 0x00030C60
		public HttpListenerException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		// Token: 0x060008F5 RID: 2293 RVA: 0x00032A6D File Offset: 0x00030C6D
		public HttpListenerException(int errorCode)
			: base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code and message.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x060008F6 RID: 2294 RVA: 0x00032A76 File Offset: 0x00030C76
		public HttpListenerException(int errorCode, string message)
			: base(errorCode, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to deserialize the new <see cref="T:System.Net.HttpListenerException" /> object.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060008F7 RID: 2295 RVA: 0x00032A80 File Offset: 0x00030C80
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets a value that identifies the error that occurred.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00032A8A File Offset: 0x00030C8A
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
