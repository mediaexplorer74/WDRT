using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	/// <summary>The exception that is thrown in a thread upon cancellation of an operation that the thread was executing.</summary>
	// Token: 0x0200011D RID: 285
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		/// <summary>Gets a token associated with the operation that was canceled.</summary>
		/// <returns>A token associated with the operation that was canceled, or a default token.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00032BEE File Offset: 0x00030DEE
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00032BF6 File Offset: 0x00030DF6
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a system-supplied error message.</summary>
		// Token: 0x060010CF RID: 4303 RVA: 0x00032BFF File Offset: 0x00030DFF
		[__DynamicallyInvokable]
		public OperationCanceledException()
			: base(Environment.GetResourceString("OperationCanceled"))
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060010D0 RID: 4304 RVA: 0x00032C1C File Offset: 0x00030E1C
		[__DynamicallyInvokable]
		public OperationCanceledException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060010D1 RID: 4305 RVA: 0x00032C30 File Offset: 0x00030E30
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a cancellation token.</summary>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060010D2 RID: 4306 RVA: 0x00032C45 File Offset: 0x00030E45
		[__DynamicallyInvokable]
		public OperationCanceledException(CancellationToken token)
			: this()
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message and a cancellation token.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060010D3 RID: 4307 RVA: 0x00032C54 File Offset: 0x00030E54
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, CancellationToken token)
			: this(message)
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message, a reference to the inner exception that is the cause of this exception, and a cancellation token.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060010D4 RID: 4308 RVA: 0x00032C64 File Offset: 0x00030E64
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException, CancellationToken token)
			: this(message, innerException)
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060010D5 RID: 4309 RVA: 0x00032C75 File Offset: 0x00030E75
		protected OperationCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040005D3 RID: 1491
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
