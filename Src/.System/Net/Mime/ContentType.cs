using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Type header.</summary>
	// Token: 0x02000241 RID: 577
	public class ContentType
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Net.Mime.ContentType" /> class.</summary>
		// Token: 0x060015D9 RID: 5593 RVA: 0x00071120 File Offset: 0x0006F320
		public ContentType()
			: this(ContentType.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentType" /> class using the specified string.</summary>
		/// <param name="contentType">A <see cref="T:System.String" />, for example, <c>"text/plain; charset=us-ascii"</c>, that contains the MIME media type, subtype, and optional parameters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contentType" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is in a form that cannot be parsed.</exception>
		// Token: 0x060015DA RID: 5594 RVA: 0x00071130 File Offset: 0x0006F330
		public ContentType(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "contentType" }), "contentType");
			}
			this.isChanged = true;
			this.type = contentType;
			this.ParseValue();
		}

		/// <summary>Gets or sets the value of the boundary parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the boundary parameter.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00071195 File Offset: 0x0006F395
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x000711A7 File Offset: 0x0006F3A7
		public string Boundary
		{
			get
			{
				return this.Parameters["boundary"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("boundary");
					return;
				}
				this.Parameters["boundary"] = value;
			}
		}

		/// <summary>Gets or sets the value of the charset parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the charset parameter.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000711DB File Offset: 0x0006F3DB
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x000711ED File Offset: 0x0006F3ED
		public string CharSet
		{
			get
			{
				return this.Parameters["charset"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("charset");
					return;
				}
				this.Parameters["charset"] = value;
			}
		}

		/// <summary>Gets or sets the media type value included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the media type and subtype value. This value does not include the semicolon (;) separator that follows the subtype.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">The value specified for a set operation is in a form that cannot be parsed.</exception>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00071221 File Offset: 0x0006F421
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0007123C File Offset: 0x0006F43C
		public string MediaType
		{
			get
			{
				return this.mediaType + "/" + this.subType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException(SR.GetString("net_emptystringset"), "value");
				}
				int num = 0;
				this.mediaType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.mediaType.Length == 0 || num >= value.Length || value[num++] != '/')
				{
					throw new FormatException(SR.GetString("MediaTypeInvalid"));
				}
				this.subType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.subType.Length == 0 || num < value.Length)
				{
					throw new FormatException(SR.GetString("MediaTypeInvalid"));
				}
				this.isChanged = true;
				this.isPersisted = false;
			}
		}

		/// <summary>Gets or sets the value of the name parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the name parameter.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00071304 File Offset: 0x0006F504
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x00071334 File Offset: 0x0006F534
		public string Name
		{
			get
			{
				string text = this.Parameters["name"];
				Encoding encoding = MimeBasePart.DecodeEncoding(text);
				if (encoding != null)
				{
					text = MimeBasePart.DecodeHeaderValue(text);
				}
				return text;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("name");
					return;
				}
				this.Parameters["name"] = value;
			}
		}

		/// <summary>Gets the dictionary that contains the parameters included in the Content-Type header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains name and value pairs.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00071368 File Offset: 0x0006F568
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null && this.type == null)
				{
					this.parameters = new TrackingStringDictionary();
				}
				return this.parameters;
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0007138B File Offset: 0x0006F58B
		internal void Set(string contentType, HeaderCollection headers)
		{
			this.type = contentType;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000713B3 File Offset: 0x0006F5B3
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000713E6 File Offset: 0x0006F5E6
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Net.Mime.ContentType" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the current settings for this <see cref="T:System.Net.Mime.ContentType" />.</returns>
		// Token: 0x060015E7 RID: 5607 RVA: 0x00071407 File Offset: 0x0006F607
		public override string ToString()
		{
			if (this.type == null || this.IsChanged)
			{
				this.type = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.type;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00071448 File Offset: 0x0006F648
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.mediaType);
			stringBuilder.Append('/');
			stringBuilder.Append(this.subType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentType.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentType.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00071500 File Offset: 0x0006F700
		private static void EncodeToBuffer(string value, StringBuilder builder, bool allowUnicode)
		{
			Encoding encoding = MimeBasePart.DecodeEncoding(value);
			if (encoding != null)
			{
				builder.Append("\"" + value + "\"");
				return;
			}
			if ((allowUnicode && !MailBnfHelper.HasCROrLF(value)) || MimeBasePart.IsAscii(value, false))
			{
				MailBnfHelper.GetTokenOrQuotedString(value, builder, allowUnicode);
				return;
			}
			encoding = Encoding.GetEncoding("utf-8");
			builder.Append("\"" + MimeBasePart.EncodeHeaderValue(value, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding)) + "\"");
		}

		/// <summary>Determines whether the content-type header of the specified <see cref="T:System.Net.Mime.ContentType" /> object is equal to the content-type header of this object.</summary>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentType" /> object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the content-type headers are the same; otherwise <see langword="false" />.</returns>
		// Token: 0x060015EA RID: 5610 RVA: 0x0007157A File Offset: 0x0006F77A
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentType" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x060015EB RID: 5611 RVA: 0x00071596 File Offset: 0x0006F796
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000715A8 File Offset: 0x0006F7A8
		private void ParseValue()
		{
			int num = 0;
			Exception ex = null;
			this.parameters = new TrackingStringDictionary();
			try
			{
				this.mediaType = MailBnfHelper.ReadToken(this.type, ref num, null);
				if (this.mediaType == null || this.mediaType.Length == 0 || num >= this.type.Length || this.type[num++] != '/')
				{
					ex = new FormatException(SR.GetString("ContentTypeInvalid"));
				}
				if (ex == null)
				{
					this.subType = MailBnfHelper.ReadToken(this.type, ref num, null);
					if (this.subType == null || this.subType.Length == 0)
					{
						ex = new FormatException(SR.GetString("ContentTypeInvalid"));
					}
				}
				if (ex == null)
				{
					while (MailBnfHelper.SkipCFWS(this.type, ref num))
					{
						if (this.type[num++] != ';')
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							break;
						}
						string text = MailBnfHelper.ReadParameterAttribute(this.type, ref num, null);
						if (text == null || text.Length == 0)
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (num >= this.type.Length || this.type[num++] != '=')
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						string text2;
						if (this.type[num] == '"')
						{
							text2 = MailBnfHelper.ReadQuotedString(this.type, ref num, null);
						}
						else
						{
							text2 = MailBnfHelper.ReadToken(this.type, ref num, null);
						}
						if (text2 == null)
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						this.parameters.Add(text, text2);
					}
				}
				this.parameters.IsChanged = false;
			}
			catch (FormatException)
			{
				throw new FormatException(SR.GetString("ContentTypeInvalid"));
			}
			if (ex != null)
			{
				throw new FormatException(SR.GetString("ContentTypeInvalid"));
			}
		}

		// Token: 0x040016EB RID: 5867
		private string mediaType;

		// Token: 0x040016EC RID: 5868
		private string subType;

		// Token: 0x040016ED RID: 5869
		private bool isChanged;

		// Token: 0x040016EE RID: 5870
		private string type;

		// Token: 0x040016EF RID: 5871
		private bool isPersisted;

		// Token: 0x040016F0 RID: 5872
		private TrackingStringDictionary parameters;

		// Token: 0x040016F1 RID: 5873
		internal static readonly string Default = "application/octet-stream";
	}
}
