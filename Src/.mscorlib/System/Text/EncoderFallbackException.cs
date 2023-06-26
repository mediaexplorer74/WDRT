using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when an encoder fallback operation fails. This class cannot be inherited.</summary>
	// Token: 0x02000A6C RID: 2668
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class.</summary>
		// Token: 0x0600680C RID: 26636 RVA: 0x00160B2A File Offset: 0x0015ED2A
		[__DynamicallyInvokable]
		public EncoderFallbackException()
			: base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x0600680D RID: 26637 RVA: 0x00160B47 File Offset: 0x0015ED47
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x0600680E RID: 26638 RVA: 0x00160B5B File Offset: 0x0015ED5B
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x00160B70 File Offset: 0x0015ED70
		internal EncoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x00160B7A File Offset: 0x0015ED7A
		internal EncoderFallbackException(string message, char charUnknown, int index)
			: base(message)
		{
			this.charUnknown = charUnknown;
			this.index = index;
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x00160B94 File Offset: 0x0015ED94
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
			: base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 56320, 57343 }));
			}
			this.charUnknownHigh = charUnknownHigh;
			this.charUnknownLow = charUnknownLow;
			this.index = index;
		}

		/// <summary>Gets the input character that caused the exception.</summary>
		/// <returns>The character that cannot be encoded.</returns>
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06006812 RID: 26642 RVA: 0x00160C38 File Offset: 0x0015EE38
		[__DynamicallyInvokable]
		public char CharUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknown;
			}
		}

		/// <summary>Gets the high component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The high component character of the surrogate pair that cannot be encoded.</returns>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06006813 RID: 26643 RVA: 0x00160C40 File Offset: 0x0015EE40
		[__DynamicallyInvokable]
		public char CharUnknownHigh
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownHigh;
			}
		}

		/// <summary>Gets the low component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The low component character of the surrogate pair that cannot be encoded.</returns>
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06006814 RID: 26644 RVA: 0x00160C48 File Offset: 0x0015EE48
		[__DynamicallyInvokable]
		public char CharUnknownLow
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownLow;
			}
		}

		/// <summary>Gets the index position in the input buffer of the character that caused the exception.</summary>
		/// <returns>The index position in the input buffer of the character that cannot be encoded.</returns>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06006815 RID: 26645 RVA: 0x00160C50 File Offset: 0x0015EE50
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		/// <summary>Indicates whether the input that caused the exception is a surrogate pair.</summary>
		/// <returns>
		///   <see langword="true" /> if the input was a surrogate pair; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006816 RID: 26646 RVA: 0x00160C58 File Offset: 0x0015EE58
		[__DynamicallyInvokable]
		public bool IsUnknownSurrogate()
		{
			return this.charUnknownHigh > '\0';
		}

		// Token: 0x04002E6A RID: 11882
		private char charUnknown;

		// Token: 0x04002E6B RID: 11883
		private char charUnknownHigh;

		// Token: 0x04002E6C RID: 11884
		private char charUnknownLow;

		// Token: 0x04002E6D RID: 11885
		private int index;
	}
}
