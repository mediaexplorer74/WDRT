using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character.</summary>
	// Token: 0x02000A62 RID: 2658
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060067B7 RID: 26551 RVA: 0x0015F878 File Offset: 0x0015DA78
		private static object InternalSyncObject
		{
			get
			{
				if (DecoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, obj, null);
				}
				return DecoderFallback.s_InternalSyncObject;
			}
		}

		/// <summary>Gets an object that outputs a substitute string in place of an input byte sequence that cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderReplacementFallback" /> object that emits the QUESTION MARK character ("?", U+003F) in place of unknown byte sequences.</returns>
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060067B8 RID: 26552 RVA: 0x0015F8A4 File Offset: 0x0015DAA4
		[__DynamicallyInvokable]
		public static DecoderFallback ReplacementFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.replacementFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.replacementFallback == null)
						{
							DecoderFallback.replacementFallback = new DecoderReplacementFallback();
						}
					}
				}
				return DecoderFallback.replacementFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input byte sequence cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderExceptionFallback" /> object.</returns>
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x0015F904 File Offset: 0x0015DB04
		[__DynamicallyInvokable]
		public static DecoderFallback ExceptionFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.exceptionFallback == null)
						{
							DecoderFallback.exceptionFallback = new DecoderExceptionFallback();
						}
					}
				}
				return DecoderFallback.exceptionFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class.</summary>
		/// <returns>An object that provides a fallback buffer for a decoder.</returns>
		// Token: 0x060067BA RID: 26554
		[__DynamicallyInvokable]
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</returns>
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060067BB RID: 26555
		[__DynamicallyInvokable]
		public abstract int MaxCharCount
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060067BC RID: 26556 RVA: 0x0015F964 File Offset: 0x0015DB64
		internal bool IsMicrosoftBestFitFallback
		{
			get
			{
				return this.bIsMicrosoftBestFitFallback;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallback" /> class.</summary>
		// Token: 0x060067BD RID: 26557 RVA: 0x0015F96C File Offset: 0x0015DB6C
		[__DynamicallyInvokable]
		protected DecoderFallback()
		{
		}

		// Token: 0x04002E52 RID: 11858
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04002E53 RID: 11859
		private static volatile DecoderFallback replacementFallback;

		// Token: 0x04002E54 RID: 11860
		private static volatile DecoderFallback exceptionFallback;

		// Token: 0x04002E55 RID: 11861
		private static object s_InternalSyncObject;
	}
}
