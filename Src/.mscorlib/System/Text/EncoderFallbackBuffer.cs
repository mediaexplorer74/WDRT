using System;
using System.Security;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to an encoder when it cannot encode an input character.</summary>
	// Token: 0x02000A6E RID: 2670
	[__DynamicallyInvokable]
	public abstract class EncoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input character.</summary>
		/// <param name="charUnknown">An input character.</param>
		/// <param name="index">The index position of the character in the input buffer.</param>
		/// <returns>
		///   <see langword="true" /> if the fallback buffer can process <paramref name="charUnknown" />; <see langword="false" /> if the fallback buffer ignores <paramref name="charUnknown" />.</returns>
		// Token: 0x0600681D RID: 26653
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknown, int index);

		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified surrogate pair.</summary>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <returns>
		///   <see langword="true" /> if the fallback buffer can process <paramref name="charUnknownHigh" /> and <paramref name="charUnknownLow" />; <see langword="false" /> if the fallback buffer ignores the surrogate pair.</returns>
		// Token: 0x0600681E RID: 26654
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		// Token: 0x0600681F RID: 26655
		[__DynamicallyInvokable]
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, causes the next call to the <see cref="M:System.Text.EncoderFallbackBuffer.GetNextChar" /> method to access the data buffer character position that is prior to the current character position.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Text.EncoderFallbackBuffer.MovePrevious" /> operation was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006820 RID: 26656
		[__DynamicallyInvokable]
		public abstract bool MovePrevious();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.EncoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06006821 RID: 26657
		[__DynamicallyInvokable]
		public abstract int Remaining
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		// Token: 0x06006822 RID: 26658 RVA: 0x00160D58 File Offset: 0x0015EF58
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x00160D62 File Offset: 0x0015EF62
		[SecurityCritical]
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00160D80 File Offset: 0x0015EF80
		[SecurityCritical]
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x00160DB4 File Offset: 0x0015EFB4
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = nextChar > '\0';
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00160DE0 File Offset: 0x0015EFE0
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int num = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder.charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num2 = this.iRecursionCount;
							this.iRecursionCount = num2 + 1;
							if (num2 > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, num);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num2 = this.iRecursionCount;
				this.iRecursionCount = num2 + 1;
				if (num2 > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, num);
			return this.bFallingBack;
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00160EDE File Offset: 0x0015F0DE
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[] { charRecursive }), "chars");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackBuffer" /> class.</summary>
		// Token: 0x06006828 RID: 26664 RVA: 0x00160F03 File Offset: 0x0015F103
		[__DynamicallyInvokable]
		protected EncoderFallbackBuffer()
		{
		}

		// Token: 0x04002E72 RID: 11890
		[SecurityCritical]
		internal unsafe char* charStart;

		// Token: 0x04002E73 RID: 11891
		[SecurityCritical]
		internal unsafe char* charEnd;

		// Token: 0x04002E74 RID: 11892
		internal EncoderNLS encoder;

		// Token: 0x04002E75 RID: 11893
		internal bool setEncoder;

		// Token: 0x04002E76 RID: 11894
		internal bool bUsedEncoder;

		// Token: 0x04002E77 RID: 11895
		internal bool bFallingBack;

		// Token: 0x04002E78 RID: 11896
		internal int iRecursionCount;

		// Token: 0x04002E79 RID: 11897
		private const int iMaxRecursion = 250;
	}
}
