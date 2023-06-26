using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an encoded output byte sequence.</summary>
	// Token: 0x02000A6D RID: 2669
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class EncoderFallback
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06006817 RID: 26647 RVA: 0x00160C64 File Offset: 0x0015EE64
		private static object InternalSyncObject
		{
			get
			{
				if (EncoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref EncoderFallback.s_InternalSyncObject, obj, null);
				}
				return EncoderFallback.s_InternalSyncObject;
			}
		}

		/// <summary>Gets an object that outputs a substitute string in place of an input character that cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderReplacementFallback" /> object that replaces unknown input characters with the QUESTION MARK character ("?", U+003F).</returns>
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06006818 RID: 26648 RVA: 0x00160C90 File Offset: 0x0015EE90
		[__DynamicallyInvokable]
		public static EncoderFallback ReplacementFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (EncoderFallback.replacementFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.replacementFallback == null)
						{
							EncoderFallback.replacementFallback = new EncoderReplacementFallback();
						}
					}
				}
				return EncoderFallback.replacementFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input character cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderExceptionFallback" /> object.</returns>
		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06006819 RID: 26649 RVA: 0x00160CF0 File Offset: 0x0015EEF0
		[__DynamicallyInvokable]
		public static EncoderFallback ExceptionFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (EncoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.exceptionFallback == null)
						{
							EncoderFallback.exceptionFallback = new EncoderExceptionFallback();
						}
					}
				}
				return EncoderFallback.exceptionFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.EncoderFallbackBuffer" /> class.</summary>
		/// <returns>An object that provides a fallback buffer for an encoder.</returns>
		// Token: 0x0600681A RID: 26650
		[__DynamicallyInvokable]
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</returns>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x0600681B RID: 26651
		[__DynamicallyInvokable]
		public abstract int MaxCharCount
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallback" /> class.</summary>
		// Token: 0x0600681C RID: 26652 RVA: 0x00160D50 File Offset: 0x0015EF50
		[__DynamicallyInvokable]
		protected EncoderFallback()
		{
		}

		// Token: 0x04002E6E RID: 11886
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04002E6F RID: 11887
		private static volatile EncoderFallback replacementFallback;

		// Token: 0x04002E70 RID: 11888
		private static volatile EncoderFallback exceptionFallback;

		// Token: 0x04002E71 RID: 11889
		private static object s_InternalSyncObject;
	}
}
