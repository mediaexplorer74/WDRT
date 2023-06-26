using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Text
{
	/// <summary>Represents a character encoding.</summary>
	// Token: 0x02000A71 RID: 2673
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class.</summary>
		// Token: 0x06006837 RID: 26679 RVA: 0x00161228 File Offset: 0x0015F428
		[__DynamicallyInvokable]
		protected Encoding()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class that corresponds to the specified code page.</summary>
		/// <param name="codePage">The code page identifier of the preferred encoding.  
		///  -or-  
		///  0, to use the default encoding.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codePage" /> is less than zero.</exception>
		// Token: 0x06006838 RID: 26680 RVA: 0x00161231 File Offset: 0x0015F431
		[__DynamicallyInvokable]
		protected Encoding(int codePage)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.SetDefaultFallbacks();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class that corresponds to the specified code page with the specified encoder and decoder fallback strategies.</summary>
		/// <param name="codePage">The encoding code page identifier.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codePage" /> is less than zero.</exception>
		// Token: 0x06006839 RID: 26681 RVA: 0x0016125C File Offset: 0x0015F45C
		[__DynamicallyInvokable]
		protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.encoderFallback = encoderFallback ?? new InternalEncoderBestFitFallback(this);
			this.decoderFallback = decoderFallback ?? new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x001612AE File Offset: 0x0015F4AE
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = new InternalEncoderBestFitFallback(this);
			this.decoderFallback = new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x001612C8 File Offset: 0x0015F4C8
		internal void OnDeserializing()
		{
			this.encoderFallback = null;
			this.decoderFallback = null;
			this.m_isReadOnly = true;
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x001612DF File Offset: 0x0015F4DF
		internal void OnDeserialized()
		{
			if (this.encoderFallback == null || this.decoderFallback == null)
			{
				this.m_deserializedFromEverett = true;
				this.SetDefaultFallbacks();
			}
			this.dataItem = null;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x00161305 File Offset: 0x0015F505
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.OnDeserializing();
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x0016130D File Offset: 0x0015F50D
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x00161315 File Offset: 0x0015F515
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.dataItem = null;
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x00161320 File Offset: 0x0015F520
		internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			this.dataItem = null;
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x001613EC File Offset: 0x0015F5EC
		internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_isReadOnly", this.m_isReadOnly);
			info.AddValue("encoderFallback", this.EncoderFallback);
			info.AddValue("decoderFallback", this.DecoderFallback);
			info.AddValue("m_codePage", this.m_codePage);
			info.AddValue("dataItem", null);
			info.AddValue("Encoding+m_codePage", this.m_codePage);
			info.AddValue("Encoding+dataItem", null);
		}

		/// <summary>Converts an entire byte array from one encoding to another.</summary>
		/// <param name="srcEncoding">The encoding format of <paramref name="bytes" />.</param>
		/// <param name="dstEncoding">The target encoding format.</param>
		/// <param name="bytes">The bytes to convert.</param>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the results of converting <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="dstEncoding" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  srcEncoding. <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  dstEncoding. <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006842 RID: 26690 RVA: 0x00161474 File Offset: 0x0015F674
		[__DynamicallyInvokable]
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
		}

		/// <summary>Converts a range of bytes in a byte array from one encoding to another.</summary>
		/// <param name="srcEncoding">The encoding of the source array, <paramref name="bytes" />.</param>
		/// <param name="dstEncoding">The encoding of the output array.</param>
		/// <param name="bytes">The array of bytes to convert.</param>
		/// <param name="index">The index of the first element of <paramref name="bytes" /> to convert.</param>
		/// <param name="count">The number of bytes to convert.</param>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the result of converting a range of bytes in <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="dstEncoding" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the byte array.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  srcEncoding. <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  dstEncoding. <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006843 RID: 26691 RVA: 0x00161490 File Offset: 0x0015F690
		[__DynamicallyInvokable]
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null || dstEncoding == null)
			{
				throw new ArgumentNullException((srcEncoding == null) ? "srcEncoding" : "dstEncoding", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x001614EC File Offset: 0x0015F6EC
		private static object InternalSyncObject
		{
			get
			{
				if (Encoding.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, null);
				}
				return Encoding.s_InternalSyncObject;
			}
		}

		/// <summary>Registers an encoding provider.</summary>
		/// <param name="provider">A subclass of <see cref="T:System.Text.EncodingProvider" /> that provides access to additional character encodings.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is <see langword="null" />.</exception>
		// Token: 0x06006845 RID: 26693 RVA: 0x00161518 File Offset: 0x0015F718
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void RegisterProvider(EncodingProvider provider)
		{
			EncodingProvider.AddProvider(provider);
		}

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.  
		///  -or-  
		///  0 (zero), to use the default encoding.</param>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform.</exception>
		// Token: 0x06006846 RID: 26694 RVA: 0x00161520 File Offset: 0x0015F720
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 65535 }));
			}
			if (Encoding.encodings != null)
			{
				encoding = (Encoding)Encoding.encodings[codepage];
			}
			if (encoding == null)
			{
				object internalSyncObject = Encoding.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Encoding.encodings == null)
					{
						Encoding.encodings = new Hashtable();
					}
					if ((encoding = (Encoding)Encoding.encodings[codepage]) != null)
					{
						return encoding;
					}
					if (codepage <= 1200)
					{
						if (codepage <= 3)
						{
							if (codepage == 0)
							{
								encoding = Encoding.Default;
								goto IL_185;
							}
							if (codepage - 1 > 2)
							{
								goto IL_174;
							}
						}
						else if (codepage != 42)
						{
							if (codepage != 1200)
							{
								goto IL_174;
							}
							encoding = Encoding.Unicode;
							goto IL_185;
						}
						throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", new object[] { codepage }), "codepage");
					}
					if (codepage <= 1252)
					{
						if (codepage == 1201)
						{
							encoding = Encoding.BigEndianUnicode;
							goto IL_185;
						}
						if (codepage == 1252)
						{
							encoding = new SBCSCodePageEncoding(codepage);
							goto IL_185;
						}
					}
					else
					{
						if (codepage == 20127)
						{
							encoding = Encoding.ASCII;
							goto IL_185;
						}
						if (codepage == 28591)
						{
							encoding = Encoding.Latin1;
							goto IL_185;
						}
						if (codepage == 65001)
						{
							encoding = Encoding.UTF8;
							goto IL_185;
						}
					}
					IL_174:
					encoding = Encoding.GetEncodingCodePage(codepage);
					if (encoding == null)
					{
						encoding = Encoding.GetEncodingRare(codepage);
					}
					IL_185:
					Encoding.encodings.Add(codepage, encoding);
				}
				return encoding;
			}
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.  
		///  -or-  
		///  0 (zero), to use the default encoding.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform.</exception>
		// Token: 0x06006847 RID: 26695 RVA: 0x001616F0 File Offset: 0x0015F8F0
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x00161730 File Offset: 0x0015F930
		[SecurityCritical]
		private static Encoding GetEncodingRare(int codepage)
		{
			if (codepage <= 50229)
			{
				if (codepage <= 12000)
				{
					if (codepage == 10003)
					{
						return new DBCSCodePageEncoding(10003, 20949);
					}
					if (codepage == 10008)
					{
						return new DBCSCodePageEncoding(10008, 20936);
					}
					if (codepage != 12000)
					{
						goto IL_192;
					}
					return Encoding.UTF32;
				}
				else
				{
					if (codepage == 12001)
					{
						return new UTF32Encoding(true, true);
					}
					if (codepage == 38598)
					{
						return new SBCSCodePageEncoding(codepage, 28598);
					}
					switch (codepage)
					{
					case 50220:
					case 50221:
					case 50222:
					case 50225:
						break;
					case 50223:
					case 50224:
					case 50226:
					case 50228:
						goto IL_192;
					case 50227:
						goto IL_150;
					case 50229:
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_CodePage50229"));
					default:
						goto IL_192;
					}
				}
			}
			else if (codepage <= 51949)
			{
				if (codepage == 51932)
				{
					return new EUCJPEncoding();
				}
				if (codepage == 51936)
				{
					goto IL_150;
				}
				if (codepage != 51949)
				{
					goto IL_192;
				}
				return new DBCSCodePageEncoding(codepage, 20949);
			}
			else if (codepage <= 54936)
			{
				if (codepage != 52936)
				{
					if (codepage != 54936)
					{
						goto IL_192;
					}
					return new GB18030Encoding();
				}
			}
			else
			{
				if (codepage - 57002 <= 9)
				{
					return new ISCIIEncoding(codepage);
				}
				if (codepage == 65000)
				{
					return Encoding.UTF7;
				}
				goto IL_192;
			}
			return new ISO2022Encoding(codepage);
			IL_150:
			return new DBCSCodePageEncoding(codepage, 936);
			IL_192:
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { codepage }));
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x001618F0 File Offset: 0x0015FAF0
		[SecurityCritical]
		private static Encoding GetEncodingCodePage(int CodePage)
		{
			int codePageByteSize = BaseCodePageEncoding.GetCodePageByteSize(CodePage);
			if (codePageByteSize == 1)
			{
				return new SBCSCodePageEncoding(CodePage);
			}
			if (codePageByteSize == 2)
			{
				return new DBCSCodePageEncoding(CodePage);
			}
			return null;
		}

		/// <summary>Returns the encoding associated with the specified code page name.</summary>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by the <see cref="P:System.Text.Encoding.WebName" /> property is valid. Possible values are listed in the Name column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.</param>
		/// <returns>The encoding  associated with the specified code page.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.  
		/// -or-  
		/// The code page indicated by <paramref name="name" /> is not supported by the underlying platform.</exception>
		// Token: 0x0600684A RID: 26698 RVA: 0x0016191C File Offset: 0x0015FB1C
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(string name)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		/// <summary>Returns the encoding associated with the specified code page name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by the <see cref="P:System.Text.Encoding.WebName" /> property is valid. Possible values are listed in the Name column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding.</param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding.</param>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.  
		/// -or-  
		/// The code page indicated by <paramref name="name" /> is not supported by the underlying platform.</exception>
		// Token: 0x0600684B RID: 26699 RVA: 0x00161940 File Offset: 0x0015FB40
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
		}

		/// <summary>Returns an array that contains all encodings.</summary>
		/// <returns>An array that contains all encodings.</returns>
		// Token: 0x0600684C RID: 26700 RVA: 0x00161968 File Offset: 0x0015FB68
		public static EncodingInfo[] GetEncodings()
		{
			return EncodingTable.GetEncodings();
		}

		/// <summary>When overridden in a derived class, returns a sequence of bytes that specifies the encoding used.</summary>
		/// <returns>A byte array containing a sequence of bytes that specifies the encoding used.  
		///  -or-  
		///  A byte array of length zero, if a preamble is not required.</returns>
		// Token: 0x0600684D RID: 26701 RVA: 0x0016196F File Offset: 0x0015FB6F
		[__DynamicallyInvokable]
		public virtual byte[] GetPreamble()
		{
			return EmptyArray<byte>.Value;
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x00161978 File Offset: 0x0015FB78
		private void GetDataItem()
		{
			if (this.dataItem == null)
			{
				this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
				if (this.dataItem == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { this.m_codePage }));
				}
			}
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent body tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> that can be used with mail agent body tags.  
		///  -or-  
		///  An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600684F RID: 26703 RVA: 0x001619CA File Offset: 0x0015FBCA
		public virtual string BodyName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.BodyName;
			}
		}

		/// <summary>When overridden in a derived class, gets the human-readable description of the current encoding.</summary>
		/// <returns>The human-readable description of the current <see cref="T:System.Text.Encoding" />.</returns>
		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x001619E5 File Offset: 0x0015FBE5
		[__DynamicallyInvokable]
		public virtual string EncodingName
		{
			[__DynamicallyInvokable]
			get
			{
				return Environment.GetResourceString("Globalization.cp_" + this.m_codePage.ToString());
			}
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent header tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> to use with mail agent header tags.  
		///  -or-  
		///  An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06006851 RID: 26705 RVA: 0x00161A01 File Offset: 0x0015FC01
		public virtual string HeaderName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.HeaderName;
			}
		}

		/// <summary>When overridden in a derived class, gets the name registered with the Internet Assigned Numbers Authority (IANA) for the current encoding.</summary>
		/// <returns>The IANA name for the current <see cref="T:System.Text.Encoding" />.</returns>
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06006852 RID: 26706 RVA: 0x00161A1C File Offset: 0x0015FC1C
		[__DynamicallyInvokable]
		public virtual string WebName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.WebName;
			}
		}

		/// <summary>When overridden in a derived class, gets the Windows operating system code page that most closely corresponds to the current encoding.</summary>
		/// <returns>The Windows operating system code page that most closely corresponds to the current <see cref="T:System.Text.Encoding" />.</returns>
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06006853 RID: 26707 RVA: 0x00161A37 File Offset: 0x0015FC37
		public virtual int WindowsCodePage
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.UIFamilyCodePage;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for displaying content.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for displaying content; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x00161A52 File Offset: 0x0015FC52
		public virtual bool IsBrowserDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 2U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for saving content.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for saving content; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06006855 RID: 26709 RVA: 0x00161A72 File Offset: 0x0015FC72
		public virtual bool IsBrowserSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 512U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for displaying content.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for displaying content; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06006856 RID: 26710 RVA: 0x00161A96 File Offset: 0x0015FC96
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 1U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for saving content.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for saving content; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006857 RID: 26711 RVA: 0x00161AB6 File Offset: 0x0015FCB6
		public virtual bool IsMailNewsSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 256U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding uses single-byte code points.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> uses single-byte code points; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006858 RID: 26712 RVA: 0x00161ADA File Offset: 0x0015FCDA
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual bool IsSingleByte
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The encoder fallback object for the current <see cref="T:System.Text.Encoding" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06006859 RID: 26713 RVA: 0x00161ADD File Offset: 0x0015FCDD
		// (set) Token: 0x0600685A RID: 26714 RVA: 0x00161AE5 File Offset: 0x0015FCE5
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallback EncoderFallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The decoder fallback object for the current <see cref="T:System.Text.Encoding" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600685B RID: 26715 RVA: 0x00161B14 File Offset: 0x0015FD14
		// (set) Token: 0x0600685C RID: 26716 RVA: 0x00161B1C File Offset: 0x0015FD1C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallback DecoderFallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		/// <summary>When overridden in a derived class, creates a shallow copy of the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Text.Encoding" /> object.</returns>
		// Token: 0x0600685D RID: 26717 RVA: 0x00161B4C File Offset: 0x0015FD4C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.m_isReadOnly = false;
			return encoding;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600685E RID: 26718 RVA: 0x00161B6D File Offset: 0x0015FD6D
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an encoding for the ASCII (7-bit) character set.</summary>
		/// <returns>An  encoding for the ASCII (7-bit) character set.</returns>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600685F RID: 26719 RVA: 0x00161B75 File Offset: 0x0015FD75
		[__DynamicallyInvokable]
		public static Encoding ASCII
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					Encoding.asciiEncoding = new ASCIIEncoding();
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06006860 RID: 26720 RVA: 0x00161B93 File Offset: 0x0015FD93
		private static Encoding Latin1
		{
			get
			{
				if (Encoding.latin1Encoding == null)
				{
					Encoding.latin1Encoding = new Latin1Encoding();
				}
				return Encoding.latin1Encoding;
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding all the characters in the specified character array.</summary>
		/// <param name="chars">The character array containing the characters to encode.</param>
		/// <returns>The number of bytes produced by encoding all the characters in the specified character array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006861 RID: 26721 RVA: 0x00161BB1 File Offset: 0x0015FDB1
		[__DynamicallyInvokable]
		public virtual int GetByteCount(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetByteCount(chars, 0, chars.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding the characters in the specified string.</summary>
		/// <param name="s">The string containing the set of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006862 RID: 26722 RVA: 0x00161BD8 File Offset: 0x0015FDD8
		[__DynamicallyInvokable]
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="index">The index of the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006863 RID: 26723
		[__DynamicallyInvokable]
		public abstract int GetByteCount(char[] chars, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006864 RID: 26724 RVA: 0x00161C08 File Offset: 0x0015FE08
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count);
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x00161C6E File Offset: 0x0015FE6E
		[SecurityCritical]
		internal unsafe virtual int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			return this.GetByteCount(chars, count);
		}

		/// <summary>When overridden in a derived class, encodes all the characters in the specified character array into a sequence of bytes.</summary>
		/// <param name="chars">The character array containing the characters to encode.</param>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006866 RID: 26726 RVA: 0x00161C78 File Offset: 0x0015FE78
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into a sequence of bytes.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="index">The index of the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006867 RID: 26727 RVA: 0x00161CA0 File Offset: 0x0015FEA0
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.  
		/// -or-  
		/// <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006868 RID: 26728
		[__DynamicallyInvokable]
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		/// <summary>When overridden in a derived class, encodes all the characters in the specified string into a sequence of bytes.</summary>
		/// <param name="s">The string containing the characters to encode.</param>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x06006869 RID: 26729 RVA: 0x00161CCC File Offset: 0x0015FECC
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", Environment.GetResourceString("ArgumentNull_String"));
			}
			int byteCount = this.GetByteCount(s);
			byte[] array = new byte[byteCount];
			int bytes = this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified string into the specified byte array.</summary>
		/// <param name="s">The string containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.  
		/// -or-  
		/// <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x0600686A RID: 26730 RVA: 0x00161D12 File Offset: 0x0015FF12
		[__DynamicallyInvokable]
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x00161D34 File Offset: 0x0015FF34
		[SecurityCritical]
		internal unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			return this.GetBytes(chars, charCount, bytes, byteCount);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <param name="chars">A pointer to the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes.</param>
		/// <param name="byteCount">The maximum number of bytes to write.</param>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x0600686C RID: 26732 RVA: 0x00161D44 File Offset: 0x0015FF44
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding all the bytes in the specified byte array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600686D RID: 26733 RVA: 0x00161DF4 File Offset: 0x0015FFF4
		[__DynamicallyInvokable]
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600686E RID: 26734
		[__DynamicallyInvokable]
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600686F RID: 26735 RVA: 0x00161E1C File Offset: 0x0016001C
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x00161E7F File Offset: 0x0016007F
		[SecurityCritical]
		internal unsafe virtual int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return this.GetCharCount(bytes, count);
		}

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a set of characters.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006871 RID: 26737 RVA: 0x00161E89 File Offset: 0x00160089
		[__DynamicallyInvokable]
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a set of characters.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006872 RID: 26738 RVA: 0x00161EB0 File Offset: 0x001600B0
		[__DynamicallyInvokable]
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="byteIndex">The index of the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">The character array to contain the resulting set of characters.</param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters.</param>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.  
		/// -or-  
		/// <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006873 RID: 26739
		[__DynamicallyInvokable]
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		/// <summary>When overridden in a derived class, decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <param name="bytes">A pointer to the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters.</param>
		/// <param name="charCount">The maximum number of characters to write.</param>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006874 RID: 26740 RVA: 0x00161EDC File Offset: 0x001600DC
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x00161F8C File Offset: 0x0016018C
		[SecurityCritical]
		internal unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			return this.GetChars(bytes, byteCount, chars, charCount);
		}

		/// <summary>When overridden in a derived class, decodes a specified number of bytes starting at a specified address into a string.</summary>
		/// <param name="bytes">A pointer to a byte array.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is a null pointer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A   fallback occurred (see Character Encoding in .NET for a complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006876 RID: 26742 RVA: 0x00161F99 File Offset: 0x00160199
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		/// <summary>When overridden in a derived class, gets the code page identifier of the current <see cref="T:System.Text.Encoding" />.</summary>
		/// <returns>The code page identifier of the current <see cref="T:System.Text.Encoding" />.</returns>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06006877 RID: 26743 RVA: 0x00161FD6 File Offset: 0x001601D6
		[__DynamicallyInvokable]
		public virtual int CodePage
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_codePage;
			}
		}

		/// <summary>Gets a value indicating whether the current encoding is always normalized, using the default normalization form.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> is always normalized; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x06006878 RID: 26744 RVA: 0x00161FDE File Offset: 0x001601DE
		[ComVisible(false)]
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is always normalized, using the specified normalization form.</summary>
		/// <param name="form">One of the <see cref="T:System.Text.NormalizationForm" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Text.Encoding" /> object is always normalized using the specified <see cref="T:System.Text.NormalizationForm" /> value; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x06006879 RID: 26745 RVA: 0x00161FE7 File Offset: 0x001601E7
		[ComVisible(false)]
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return false;
		}

		/// <summary>When overridden in a derived class, obtains a decoder that converts an encoded sequence of bytes into a sequence of characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts an encoded sequence of bytes into a sequence of characters.</returns>
		// Token: 0x0600687A RID: 26746 RVA: 0x00161FEA File Offset: 0x001601EA
		[__DynamicallyInvokable]
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x00161FF4 File Offset: 0x001601F4
		[SecurityCritical]
		private static Encoding CreateDefaultEncoding()
		{
			int acp = Win32Native.GetACP();
			Encoding encoding;
			if (acp == 1252)
			{
				encoding = new SBCSCodePageEncoding(acp);
			}
			else if (acp == 65001)
			{
				encoding = Encoding.s_defaultUtf8EncodingNoBom;
			}
			else
			{
				encoding = Encoding.GetEncoding(acp);
			}
			return encoding;
		}

		/// <summary>Gets the default encoding for this .NET implementation.</summary>
		/// <returns>The default encoding for this .NET implementation.</returns>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x00162030 File Offset: 0x00160230
		public static Encoding Default
		{
			[SecuritySafeCritical]
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
				}
				return Encoding.defaultEncoding;
			}
		}

		/// <summary>When overridden in a derived class, obtains an encoder that converts a sequence of Unicode characters into an encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into an encoded sequence of bytes.</returns>
		// Token: 0x0600687D RID: 26749 RVA: 0x0016204E File Offset: 0x0016024E
		[__DynamicallyInvokable]
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		/// <summary>When overridden in a derived class, calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		// Token: 0x0600687E RID: 26750
		[__DynamicallyInvokable]
		public abstract int GetMaxByteCount(int charCount);

		/// <summary>When overridden in a derived class, calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x0600687F RID: 26751
		[__DynamicallyInvokable]
		public abstract int GetMaxCharCount(int byteCount);

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a string.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006880 RID: 26752 RVA: 0x00162056 File Offset: 0x00160256
		[__DynamicallyInvokable]
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a string.</summary>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />.</exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in .NET for complete explanation)  
		///  -and-  
		///  <see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		// Token: 0x06006881 RID: 26753 RVA: 0x0016207B File Offset: 0x0016027B
		[__DynamicallyInvokable]
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		/// <summary>Gets an encoding for the UTF-16 format using the little endian byte order.</summary>
		/// <returns>An encoding for the UTF-16 format using the little endian byte order.</returns>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0016208B File Offset: 0x0016028B
		[__DynamicallyInvokable]
		public static Encoding Unicode
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
				}
				return Encoding.unicodeEncoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-16 format that uses the big endian byte order.</summary>
		/// <returns>An encoding object for the UTF-16 format that uses the big endian byte order.</returns>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06006883 RID: 26755 RVA: 0x001620AB File Offset: 0x001602AB
		[__DynamicallyInvokable]
		public static Encoding BigEndianUnicode
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.bigEndianUnicode == null)
				{
					Encoding.bigEndianUnicode = new UnicodeEncoding(true, true);
				}
				return Encoding.bigEndianUnicode;
			}
		}

		/// <summary>Gets an encoding for the UTF-7 format.</summary>
		/// <returns>An encoding for the UTF-7 format.</returns>
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x001620CB File Offset: 0x001602CB
		[__DynamicallyInvokable]
		public static Encoding UTF7
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					Encoding.utf7Encoding = new UTF7Encoding();
				}
				return Encoding.utf7Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-8 format.</summary>
		/// <returns>An encoding for the UTF-8 format.</returns>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06006885 RID: 26757 RVA: 0x001620E9 File Offset: 0x001602E9
		[__DynamicallyInvokable]
		public static Encoding UTF8
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf8Encoding == null)
				{
					Encoding.utf8Encoding = new UTF8Encoding(true);
				}
				return Encoding.utf8Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-32 format using the little endian byte order.</summary>
		/// <returns>An  encoding object for the UTF-32 format using the little endian byte order.</returns>
		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x00162108 File Offset: 0x00160308
		[__DynamicallyInvokable]
		public static Encoding UTF32
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					Encoding.utf32Encoding = new UTF32Encoding(false, true);
				}
				return Encoding.utf32Encoding;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current instance.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is an instance of <see cref="T:System.Text.Encoding" /> and is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006887 RID: 26759 RVA: 0x00162128 File Offset: 0x00160328
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals(encoding.EncoderFallback)) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x06006888 RID: 26760 RVA: 0x00162175 File Offset: 0x00160375
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x00162195 File Offset: 0x00160395
		internal virtual char[] GetBestFitUnicodeToBytesData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x0016219C File Offset: 0x0016039C
		internal virtual char[] GetBestFitBytesToUnicodeData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x001621A3 File Offset: 0x001603A3
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowBytes", new object[]
			{
				this.EncodingName,
				this.EncoderFallback.GetType()
			}), "bytes");
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x001621D6 File Offset: 0x001603D6
		[SecurityCritical]
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder.m_throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0016220A File Offset: 0x0016040A
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowChars", new object[]
			{
				this.EncodingName,
				this.DecoderFallback.GetType()
			}), "chars");
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0016223D File Offset: 0x0016043D
		[SecurityCritical]
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder.m_throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x04002E7E RID: 11902
		private static readonly UTF8Encoding.UTF8EncodingSealed s_defaultUtf8EncodingNoBom = new UTF8Encoding.UTF8EncodingSealed(false);

		// Token: 0x04002E7F RID: 11903
		private static volatile Encoding defaultEncoding;

		// Token: 0x04002E80 RID: 11904
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04002E81 RID: 11905
		private static volatile Encoding bigEndianUnicode;

		// Token: 0x04002E82 RID: 11906
		private static volatile Encoding utf7Encoding;

		// Token: 0x04002E83 RID: 11907
		private static volatile Encoding utf8Encoding;

		// Token: 0x04002E84 RID: 11908
		private static volatile Encoding utf32Encoding;

		// Token: 0x04002E85 RID: 11909
		private static volatile Encoding asciiEncoding;

		// Token: 0x04002E86 RID: 11910
		private static volatile Encoding latin1Encoding;

		// Token: 0x04002E87 RID: 11911
		private static volatile Hashtable encodings;

		// Token: 0x04002E88 RID: 11912
		private const int MIMECONTF_MAILNEWS = 1;

		// Token: 0x04002E89 RID: 11913
		private const int MIMECONTF_BROWSER = 2;

		// Token: 0x04002E8A RID: 11914
		private const int MIMECONTF_SAVABLE_MAILNEWS = 256;

		// Token: 0x04002E8B RID: 11915
		private const int MIMECONTF_SAVABLE_BROWSER = 512;

		// Token: 0x04002E8C RID: 11916
		private const int CodePageDefault = 0;

		// Token: 0x04002E8D RID: 11917
		private const int CodePageNoOEM = 1;

		// Token: 0x04002E8E RID: 11918
		private const int CodePageNoMac = 2;

		// Token: 0x04002E8F RID: 11919
		private const int CodePageNoThread = 3;

		// Token: 0x04002E90 RID: 11920
		private const int CodePageNoSymbol = 42;

		// Token: 0x04002E91 RID: 11921
		private const int CodePageUnicode = 1200;

		// Token: 0x04002E92 RID: 11922
		private const int CodePageBigEndian = 1201;

		// Token: 0x04002E93 RID: 11923
		private const int CodePageWindows1252 = 1252;

		// Token: 0x04002E94 RID: 11924
		private const int CodePageMacGB2312 = 10008;

		// Token: 0x04002E95 RID: 11925
		private const int CodePageGB2312 = 20936;

		// Token: 0x04002E96 RID: 11926
		private const int CodePageMacKorean = 10003;

		// Token: 0x04002E97 RID: 11927
		private const int CodePageDLLKorean = 20949;

		// Token: 0x04002E98 RID: 11928
		private const int ISO2022JP = 50220;

		// Token: 0x04002E99 RID: 11929
		private const int ISO2022JPESC = 50221;

		// Token: 0x04002E9A RID: 11930
		private const int ISO2022JPSISO = 50222;

		// Token: 0x04002E9B RID: 11931
		private const int ISOKorean = 50225;

		// Token: 0x04002E9C RID: 11932
		private const int ISOSimplifiedCN = 50227;

		// Token: 0x04002E9D RID: 11933
		private const int EUCJP = 51932;

		// Token: 0x04002E9E RID: 11934
		private const int ChineseHZ = 52936;

		// Token: 0x04002E9F RID: 11935
		private const int DuplicateEUCCN = 51936;

		// Token: 0x04002EA0 RID: 11936
		private const int EUCCN = 936;

		// Token: 0x04002EA1 RID: 11937
		private const int EUCKR = 51949;

		// Token: 0x04002EA2 RID: 11938
		internal const int CodePageASCII = 20127;

		// Token: 0x04002EA3 RID: 11939
		internal const int ISO_8859_1 = 28591;

		// Token: 0x04002EA4 RID: 11940
		private const int ISCIIAssemese = 57006;

		// Token: 0x04002EA5 RID: 11941
		private const int ISCIIBengali = 57003;

		// Token: 0x04002EA6 RID: 11942
		private const int ISCIIDevanagari = 57002;

		// Token: 0x04002EA7 RID: 11943
		private const int ISCIIGujarathi = 57010;

		// Token: 0x04002EA8 RID: 11944
		private const int ISCIIKannada = 57008;

		// Token: 0x04002EA9 RID: 11945
		private const int ISCIIMalayalam = 57009;

		// Token: 0x04002EAA RID: 11946
		private const int ISCIIOriya = 57007;

		// Token: 0x04002EAB RID: 11947
		private const int ISCIIPanjabi = 57011;

		// Token: 0x04002EAC RID: 11948
		private const int ISCIITamil = 57004;

		// Token: 0x04002EAD RID: 11949
		private const int ISCIITelugu = 57005;

		// Token: 0x04002EAE RID: 11950
		private const int GB18030 = 54936;

		// Token: 0x04002EAF RID: 11951
		private const int ISO_8859_8I = 38598;

		// Token: 0x04002EB0 RID: 11952
		private const int ISO_8859_8_Visual = 28598;

		// Token: 0x04002EB1 RID: 11953
		private const int ENC50229 = 50229;

		// Token: 0x04002EB2 RID: 11954
		private const int CodePageUTF7 = 65000;

		// Token: 0x04002EB3 RID: 11955
		private const int CodePageUTF8 = 65001;

		// Token: 0x04002EB4 RID: 11956
		private const int CodePageUTF32 = 12000;

		// Token: 0x04002EB5 RID: 11957
		private const int CodePageUTF32BE = 12001;

		// Token: 0x04002EB6 RID: 11958
		internal int m_codePage;

		// Token: 0x04002EB7 RID: 11959
		internal CodePageDataItem dataItem;

		// Token: 0x04002EB8 RID: 11960
		[NonSerialized]
		internal bool m_deserializedFromEverett;

		// Token: 0x04002EB9 RID: 11961
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04002EBA RID: 11962
		[OptionalField(VersionAdded = 2)]
		internal EncoderFallback encoderFallback;

		// Token: 0x04002EBB RID: 11963
		[OptionalField(VersionAdded = 2)]
		internal DecoderFallback decoderFallback;

		// Token: 0x04002EBC RID: 11964
		private static object s_InternalSyncObject;

		// Token: 0x02000CAA RID: 3242
		[Serializable]
		internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
		{
			// Token: 0x0600716F RID: 29039 RVA: 0x0018778F File Offset: 0x0018598F
			public DefaultEncoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06007170 RID: 29040 RVA: 0x001877A8 File Offset: 0x001859A8
			internal DefaultEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (EncoderFallback)info.GetValue("m_fallback", typeof(EncoderFallback));
					this.charLeftOver = (char)info.GetValue("charLeftOver", typeof(char));
				}
				catch (SerializationException)
				{
				}
			}

			// Token: 0x06007171 RID: 29041 RVA: 0x00187840 File Offset: 0x00185A40
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Encoder encoder = this.m_encoding.GetEncoder();
				if (this.m_fallback != null)
				{
					encoder.m_fallback = this.m_fallback;
				}
				if (this.charLeftOver != '\0')
				{
					EncoderNLS encoderNLS = encoder as EncoderNLS;
					if (encoderNLS != null)
					{
						encoderNLS.charLeftOver = this.charLeftOver;
					}
				}
				return encoder;
			}

			// Token: 0x06007172 RID: 29042 RVA: 0x00187896 File Offset: 0x00185A96
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06007173 RID: 29043 RVA: 0x001878B7 File Offset: 0x00185AB7
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06007174 RID: 29044 RVA: 0x001878C7 File Offset: 0x00185AC7
			[SecurityCritical]
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, count);
			}

			// Token: 0x06007175 RID: 29045 RVA: 0x001878D6 File Offset: 0x00185AD6
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x06007176 RID: 29046 RVA: 0x001878EA File Offset: 0x00185AEA
			[SecurityCritical]
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x040038A0 RID: 14496
			private Encoding m_encoding;

			// Token: 0x040038A1 RID: 14497
			[NonSerialized]
			private bool m_hasInitializedEncoding;

			// Token: 0x040038A2 RID: 14498
			[NonSerialized]
			internal char charLeftOver;
		}

		// Token: 0x02000CAB RID: 3243
		[Serializable]
		internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
		{
			// Token: 0x06007177 RID: 29047 RVA: 0x001878FC File Offset: 0x00185AFC
			public DefaultDecoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06007178 RID: 29048 RVA: 0x00187914 File Offset: 0x00185B14
			internal DefaultDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this.m_fallback = null;
				}
			}

			// Token: 0x06007179 RID: 29049 RVA: 0x00187994 File Offset: 0x00185B94
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Decoder decoder = this.m_encoding.GetDecoder();
				if (this.m_fallback != null)
				{
					decoder.m_fallback = this.m_fallback;
				}
				return decoder;
			}

			// Token: 0x0600717A RID: 29050 RVA: 0x001879CC File Offset: 0x00185BCC
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x0600717B RID: 29051 RVA: 0x001879ED File Offset: 0x00185BED
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x0600717C RID: 29052 RVA: 0x001879F9 File Offset: 0x00185BF9
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x0600717D RID: 29053 RVA: 0x00187A09 File Offset: 0x00185C09
			[SecurityCritical]
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, count);
			}

			// Token: 0x0600717E RID: 29054 RVA: 0x00187A18 File Offset: 0x00185C18
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x0600717F RID: 29055 RVA: 0x00187A28 File Offset: 0x00185C28
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x06007180 RID: 29056 RVA: 0x00187A3C File Offset: 0x00185C3C
			[SecurityCritical]
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x040038A3 RID: 14499
			private Encoding m_encoding;

			// Token: 0x040038A4 RID: 14500
			[NonSerialized]
			private bool m_hasInitializedEncoding;
		}

		// Token: 0x02000CAC RID: 3244
		internal class EncodingCharBuffer
		{
			// Token: 0x06007181 RID: 29057 RVA: 0x00187A50 File Offset: 0x00185C50
			[SecurityCritical]
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this.enc = enc;
				this.decoder = decoder;
				this.chars = charStart;
				this.charStart = charStart;
				this.charEnd = charStart + charCount;
				this.byteStart = byteStart;
				this.bytes = byteStart;
				this.byteEnd = byteStart + byteCount;
				if (this.decoder == null)
				{
					this.fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.decoder.FallbackBuffer;
				}
				this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
			}

			// Token: 0x06007182 RID: 29058 RVA: 0x00187AEC File Offset: 0x00185CEC
			[SecurityCritical]
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this.chars != null)
				{
					if (this.chars >= this.charEnd)
					{
						this.bytes -= numBytes;
						this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
						return false;
					}
					char* ptr = this.chars;
					this.chars = ptr + 1;
					*ptr = ch;
				}
				this.charCountResult++;
				return true;
			}

			// Token: 0x06007183 RID: 29059 RVA: 0x00187B65 File Offset: 0x00185D65
			[SecurityCritical]
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x06007184 RID: 29060 RVA: 0x00187B70 File Offset: 0x00185D70
			[SecurityCritical]
			internal bool AddChar(char ch1, char ch2, int numBytes)
			{
				if (this.chars >= this.charEnd - 1)
				{
					this.bytes -= numBytes;
					this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
					return false;
				}
				return this.AddChar(ch1, numBytes) && this.AddChar(ch2, numBytes);
			}

			// Token: 0x06007185 RID: 29061 RVA: 0x00187BD3 File Offset: 0x00185DD3
			[SecurityCritical]
			internal void AdjustBytes(int count)
			{
				this.bytes += count;
			}

			// Token: 0x1700136F RID: 4975
			// (get) Token: 0x06007186 RID: 29062 RVA: 0x00187BE3 File Offset: 0x00185DE3
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.bytes < this.byteEnd;
				}
			}

			// Token: 0x06007187 RID: 29063 RVA: 0x00187BF3 File Offset: 0x00185DF3
			[SecurityCritical]
			internal bool EvenMoreData(int count)
			{
				return this.bytes == this.byteEnd - count;
			}

			// Token: 0x06007188 RID: 29064 RVA: 0x00187C08 File Offset: 0x00185E08
			[SecurityCritical]
			internal unsafe byte GetNextByte()
			{
				if (this.bytes >= this.byteEnd)
				{
					return 0;
				}
				byte* ptr = this.bytes;
				this.bytes = ptr + 1;
				return *ptr;
			}

			// Token: 0x17001370 RID: 4976
			// (get) Token: 0x06007189 RID: 29065 RVA: 0x00187C37 File Offset: 0x00185E37
			internal int BytesUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.bytes - this.byteStart));
				}
			}

			// Token: 0x0600718A RID: 29066 RVA: 0x00187C4C File Offset: 0x00185E4C
			[SecurityCritical]
			internal bool Fallback(byte fallbackByte)
			{
				byte[] array = new byte[] { fallbackByte };
				return this.Fallback(array);
			}

			// Token: 0x0600718B RID: 29067 RVA: 0x00187C6C File Offset: 0x00185E6C
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2)
			{
				byte[] array = new byte[] { byte1, byte2 };
				return this.Fallback(array);
			}

			// Token: 0x0600718C RID: 29068 RVA: 0x00187C90 File Offset: 0x00185E90
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
			{
				byte[] array = new byte[] { byte1, byte2, byte3, byte4 };
				return this.Fallback(array);
			}

			// Token: 0x0600718D RID: 29069 RVA: 0x00187CBC File Offset: 0x00185EBC
			[SecurityCritical]
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this.chars != null)
				{
					char* ptr = this.chars;
					if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
					{
						this.bytes -= byteBuffer.Length;
						this.fallbackBuffer.InternalReset();
						this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
						return false;
					}
					this.charCountResult += (int)((long)(this.chars - ptr));
				}
				else
				{
					this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
				}
				return true;
			}

			// Token: 0x17001371 RID: 4977
			// (get) Token: 0x0600718E RID: 29070 RVA: 0x00187D6B File Offset: 0x00185F6B
			internal int Count
			{
				get
				{
					return this.charCountResult;
				}
			}

			// Token: 0x040038A5 RID: 14501
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x040038A6 RID: 14502
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x040038A7 RID: 14503
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x040038A8 RID: 14504
			private int charCountResult;

			// Token: 0x040038A9 RID: 14505
			private Encoding enc;

			// Token: 0x040038AA RID: 14506
			private DecoderNLS decoder;

			// Token: 0x040038AB RID: 14507
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x040038AC RID: 14508
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x040038AD RID: 14509
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x040038AE RID: 14510
			private DecoderFallbackBuffer fallbackBuffer;
		}

		// Token: 0x02000CAD RID: 3245
		internal class EncodingByteBuffer
		{
			// Token: 0x0600718F RID: 29071 RVA: 0x00187D74 File Offset: 0x00185F74
			[SecurityCritical]
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this.enc = inEncoding;
				this.encoder = inEncoder;
				this.charStart = inCharStart;
				this.chars = inCharStart;
				this.charEnd = inCharStart + inCharCount;
				this.bytes = inByteStart;
				this.byteStart = inByteStart;
				this.byteEnd = inByteStart + inByteCount;
				if (this.encoder == null)
				{
					this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.encoder.FallbackBuffer;
					if (this.encoder.m_throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.encoder.Encoding.EncodingName,
							this.encoder.Fallback.GetType()
						}));
					}
				}
				this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, this.bytes != null);
			}

			// Token: 0x06007190 RID: 29072 RVA: 0x00187E8C File Offset: 0x0018608C
			[SecurityCritical]
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this.bytes != null)
				{
					if (this.bytes >= this.byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* ptr = this.bytes;
					this.bytes = ptr + 1;
					*ptr = b;
				}
				this.byteCountResult++;
				return true;
			}

			// Token: 0x06007191 RID: 29073 RVA: 0x00187EDE File Offset: 0x001860DE
			[SecurityCritical]
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x06007192 RID: 29074 RVA: 0x00187EE8 File Offset: 0x001860E8
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x06007193 RID: 29075 RVA: 0x00187EF3 File Offset: 0x001860F3
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x06007194 RID: 29076 RVA: 0x00187F0B File Offset: 0x0018610B
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3)
			{
				return this.AddByte(b1, b2, b3, 0);
			}

			// Token: 0x06007195 RID: 29077 RVA: 0x00187F17 File Offset: 0x00186117
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
			{
				return this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected) && this.AddByte(b3, moreBytesExpected);
			}

			// Token: 0x06007196 RID: 29078 RVA: 0x00187F3E File Offset: 0x0018613E
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
			{
				return this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1) && this.AddByte(b4, 0);
			}

			// Token: 0x06007197 RID: 29079 RVA: 0x00187F6C File Offset: 0x0018616C
			[SecurityCritical]
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this.chars != this.charStart)
				{
					this.chars--;
				}
				if (bThrow)
				{
					this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
				}
			}

			// Token: 0x06007198 RID: 29080 RVA: 0x00187FD2 File Offset: 0x001861D2
			[SecurityCritical]
			internal bool Fallback(char charFallback)
			{
				return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
			}

			// Token: 0x17001372 RID: 4978
			// (get) Token: 0x06007199 RID: 29081 RVA: 0x00187FE6 File Offset: 0x001861E6
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this.chars < this.charEnd;
				}
			}

			// Token: 0x0600719A RID: 29082 RVA: 0x00188008 File Offset: 0x00186208
			[SecurityCritical]
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this.chars < this.charEnd)
				{
					char* ptr = this.chars;
					this.chars = ptr + 1;
					c = *ptr;
				}
				return c;
			}

			// Token: 0x17001373 RID: 4979
			// (get) Token: 0x0600719B RID: 29083 RVA: 0x00188046 File Offset: 0x00186246
			internal int CharsUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.chars - this.charStart));
				}
			}

			// Token: 0x17001374 RID: 4980
			// (get) Token: 0x0600719C RID: 29084 RVA: 0x00188059 File Offset: 0x00186259
			internal int Count
			{
				get
				{
					return this.byteCountResult;
				}
			}

			// Token: 0x040038AF RID: 14511
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x040038B0 RID: 14512
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x040038B1 RID: 14513
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x040038B2 RID: 14514
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x040038B3 RID: 14515
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x040038B4 RID: 14516
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x040038B5 RID: 14517
			private int byteCountResult;

			// Token: 0x040038B6 RID: 14518
			private Encoding enc;

			// Token: 0x040038B7 RID: 14519
			private EncoderNLS encoder;

			// Token: 0x040038B8 RID: 14520
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
