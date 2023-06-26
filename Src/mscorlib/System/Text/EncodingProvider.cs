using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Provides the base class for an encoding provider, which supplies encodings that are unavailable on a particular platform.</summary>
	// Token: 0x02000A74 RID: 2676
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public abstract class EncodingProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncodingProvider" /> class.</summary>
		// Token: 0x060068A5 RID: 26789 RVA: 0x0016294E File Offset: 0x00160B4E
		[__DynamicallyInvokable]
		public EncodingProvider()
		{
		}

		/// <summary>Returns the encoding with the specified name.</summary>
		/// <param name="name">The name of the requested encoding.</param>
		/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
		// Token: 0x060068A6 RID: 26790
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(string name);

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <param name="codepage">The code page identifier of the requested encoding.</param>
		/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
		// Token: 0x060068A7 RID: 26791
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(int codepage);

		/// <summary>Returns the encoding associated with the specified name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="name">The name of the preferred encoding.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
		/// <returns>The encoding that is associated with the specified name, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="name" />.</returns>
		// Token: 0x060068A8 RID: 26792 RVA: 0x00162958 File Offset: 0x00160B58
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="codepage">The code page identifier of the requested encoding.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with this encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with this encoding.</param>
		/// <returns>The encoding that is associated with the specified code page, or <see langword="null" /> if this <see cref="T:System.Text.EncodingProvider" /> cannot return a valid encoding that corresponds to <paramref name="codepage" />.</returns>
		// Token: 0x060068A9 RID: 26793 RVA: 0x00162994 File Offset: 0x00160B94
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x001629D0 File Offset: 0x00160BD0
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[] { provider };
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x00162A7C File Offset: 0x00160C7C
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x00162AC4 File Offset: 0x00160CC4
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x00162B0C File Offset: 0x00160D0C
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x00162B54 File Offset: 0x00160D54
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x04002EC0 RID: 11968
		private static object s_InternalSyncObject = new object();

		// Token: 0x04002EC1 RID: 11969
		private static volatile EncodingProvider[] s_providers;
	}
}
