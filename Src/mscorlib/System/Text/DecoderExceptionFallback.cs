using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an input character. The fallback throws an exception instead of decoding the input byte sequence. This class cannot be inherited.</summary>
	// Token: 0x02000A5F RID: 2655
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</summary>
		// Token: 0x060067A5 RID: 26533 RVA: 0x0015F717 File Offset: 0x0015D917
		[__DynamicallyInvokable]
		public DecoderExceptionFallback()
		{
		}

		/// <summary>Returns a decoder fallback buffer that throws an exception if it cannot convert a sequence of bytes to a character.</summary>
		/// <returns>A decoder fallback buffer that throws an exception when it cannot decode a byte sequence.</returns>
		// Token: 0x060067A6 RID: 26534 RVA: 0x0015F71F File Offset: 0x0015D91F
		[__DynamicallyInvokable]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060067A7 RID: 26535 RVA: 0x0015F726 File Offset: 0x0015D926
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.DecoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not <see langword="null" /> and is a <see cref="T:System.Text.DecoderExceptionFallback" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060067A8 RID: 26536 RVA: 0x0015F72C File Offset: 0x0015D92C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance.</returns>
		// Token: 0x060067A9 RID: 26537 RVA: 0x0015F746 File Offset: 0x0015D946
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
