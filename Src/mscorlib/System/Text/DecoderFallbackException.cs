using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	// Token: 0x02000A61 RID: 2657
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class.</summary>
		// Token: 0x060067B0 RID: 26544 RVA: 0x0015F801 File Offset: 0x0015DA01
		[__DynamicallyInvokable]
		public DecoderFallbackException()
			: base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x060067B1 RID: 26545 RVA: 0x0015F81E File Offset: 0x0015DA1E
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x060067B2 RID: 26546 RVA: 0x0015F832 File Offset: 0x0015DA32
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x0015F847 File Offset: 0x0015DA47
		internal DecoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x060067B4 RID: 26548 RVA: 0x0015F851 File Offset: 0x0015DA51
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this.bytesUnknown = bytesUnknown;
			this.index = index;
		}

		/// <summary>Gets the input byte sequence that caused the exception.</summary>
		/// <returns>The input byte array that cannot be decoded.</returns>
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x0015F868 File Offset: 0x0015DA68
		[__DynamicallyInvokable]
		public byte[] BytesUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bytesUnknown;
			}
		}

		/// <summary>Gets the index position in the input byte sequence of the byte that caused the exception.</summary>
		/// <returns>The index position in the input byte array of the byte that cannot be decoded. The index position is zero-based.</returns>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060067B6 RID: 26550 RVA: 0x0015F870 File Offset: 0x0015DA70
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		// Token: 0x04002E50 RID: 11856
		private byte[] bytesUnknown;

		// Token: 0x04002E51 RID: 11857
		private int index;
	}
}
