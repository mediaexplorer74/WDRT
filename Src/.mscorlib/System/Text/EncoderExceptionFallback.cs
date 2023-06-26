using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback throws an exception if an input character cannot be converted to an output byte sequence. This class cannot be inherited.</summary>
	// Token: 0x02000A6A RID: 2666
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</summary>
		// Token: 0x06006801 RID: 26625 RVA: 0x001609FF File Offset: 0x0015EBFF
		[__DynamicallyInvokable]
		public EncoderExceptionFallback()
		{
		}

		/// <summary>Returns an encoder fallback buffer that throws an exception if it cannot convert a character sequence to a byte sequence.</summary>
		/// <returns>An encoder fallback buffer that throws an exception when it cannot encode a character sequence.</returns>
		// Token: 0x06006802 RID: 26626 RVA: 0x00160A07 File Offset: 0x0015EC07
		[__DynamicallyInvokable]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x00160A0E File Offset: 0x0015EC0E
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.EncoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not <see langword="null" /> (<see langword="Nothing" /> in Visual Basic .NET) and is a <see cref="T:System.Text.EncoderExceptionFallback" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006804 RID: 26628 RVA: 0x00160A14 File Offset: 0x0015EC14
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance.</returns>
		// Token: 0x06006805 RID: 26629 RVA: 0x00160A2E File Offset: 0x0015EC2E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
