using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs while accessing the network through a pluggable protocol.</summary>
	// Token: 0x0200017D RID: 381
	[global::__DynamicallyInvokable]
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class.</summary>
		// Token: 0x06000E04 RID: 3588 RVA: 0x0004989A File Offset: 0x00047A9A
		[global::__DynamicallyInvokable]
		public WebException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message.</summary>
		/// <param name="message">The text of the error message.</param>
		// Token: 0x06000E05 RID: 3589 RVA: 0x000498AA File Offset: 0x00047AAA
		[global::__DynamicallyInvokable]
		public WebException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and nested exception.</summary>
		/// <param name="message">The text of the error message.</param>
		/// <param name="innerException">A nested exception.</param>
		// Token: 0x06000E06 RID: 3590 RVA: 0x000498B4 File Offset: 0x00047AB4
		[global::__DynamicallyInvokable]
		public WebException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and status.</summary>
		/// <param name="message">The text of the error message.</param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</param>
		// Token: 0x06000E07 RID: 3591 RVA: 0x000498C6 File Offset: 0x00047AC6
		[global::__DynamicallyInvokable]
		public WebException(string message, WebExceptionStatus status)
			: this(message, null, status, null)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000498D2 File Offset: 0x00047AD2
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException)
			: this(message, innerException, status, null, internalStatus)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message, nested exception, status, and response.</summary>
		/// <param name="message">The text of the error message.</param>
		/// <param name="innerException">A nested exception.</param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</param>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> instance that contains the response from the remote host.</param>
		// Token: 0x06000E09 RID: 3593 RVA: 0x000498E0 File Offset: 0x00047AE0
		[global::__DynamicallyInvokable]
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response)
			: this(message, null, innerException, status, response)
		{
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000498F0 File Offset: 0x00047AF0
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0004993C File Offset: 0x00047B3C
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0004994C File Offset: 0x00047B4C
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebException" />.</param>
		// Token: 0x06000E0D RID: 3597 RVA: 0x000499A0 File Offset: 0x00047BA0
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebException" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x06000E0E RID: 3598 RVA: 0x000499B2 File Offset: 0x00047BB2
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used.</param>
		// Token: 0x06000E0F RID: 3599 RVA: 0x000499BC File Offset: 0x00047BBC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000499C6 File Offset: 0x00047BC6
		[global::__DynamicallyInvokable]
		public WebExceptionStatus Status
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Status;
			}
		}

		/// <summary>Gets the response that the remote host returned.</summary>
		/// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse" /> instance that contains the error response from an Internet resource; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000499CE File Offset: 0x00047BCE
		[global::__DynamicallyInvokable]
		public WebResponse Response
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000499D6 File Offset: 0x00047BD6
		internal WebExceptionInternalStatus InternalStatus
		{
			get
			{
				return this.m_InternalStatus;
			}
		}

		// Token: 0x04001214 RID: 4628
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x04001215 RID: 4629
		private WebResponse m_Response;

		// Token: 0x04001216 RID: 4630
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}
