using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Disposition header.</summary>
	// Token: 0x0200023F RID: 575
	public class ContentDisposition
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x00070A58 File Offset: 0x0006EC58
		static ContentDisposition()
		{
			ContentDisposition.validators.Add("creation-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("modification-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("read-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("size", ContentDisposition.longParser);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with a <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> of <see cref="F:System.Net.Mime.DispositionTypeNames.Attachment" />.</summary>
		// Token: 0x060015BE RID: 5566 RVA: 0x00070AE9 File Offset: 0x0006ECE9
		public ContentDisposition()
		{
			this.isChanged = true;
			this.dispositionType = "attachment";
			this.disposition = this.dispositionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with the specified disposition information.</summary>
		/// <param name="disposition">A <see cref="T:System.Net.Mime.DispositionTypeNames" /> value that contains the disposition.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="disposition" /> is <see langword="null" /> or equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x060015BF RID: 5567 RVA: 0x00070B0F File Offset: 0x0006ED0F
		public ContentDisposition(string disposition)
		{
			if (disposition == null)
			{
				throw new ArgumentNullException("disposition");
			}
			this.isChanged = true;
			this.disposition = disposition;
			this.ParseValue();
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00070B3C File Offset: 0x0006ED3C
		internal DateTime GetDateParameter(string parameterName)
		{
			SmtpDateTime smtpDateTime = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet(parameterName) as SmtpDateTime;
			if (smtpDateTime == null)
			{
				return DateTime.MinValue;
			}
			return smtpDateTime.Date;
		}

		/// <summary>Gets or sets the disposition type for an email attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the disposition type. The value is not restricted but is typically one of the <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00070B6F File Offset: 0x0006ED6F
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00070B77 File Offset: 0x0006ED77
		public string DispositionType
		{
			get
			{
				return this.dispositionType;
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
				this.isChanged = true;
				this.dispositionType = value;
			}
		}

		/// <summary>Gets the parameters included in the Content-Disposition header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains parameter name/value pairs.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00070BB7 File Offset: 0x0006EDB7
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the suggested file name for an email attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the file name.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00070BD7 File Offset: 0x0006EDD7
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00070BE9 File Offset: 0x0006EDE9
		public string FileName
		{
			get
			{
				return this.Parameters["filename"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Parameters.Remove("filename");
					return;
				}
				this.Parameters["filename"] = value;
			}
		}

		/// <summary>Gets or sets the creation date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file creation date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00070C15 File Offset: 0x0006EE15
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00070C24 File Offset: 0x0006EE24
		public DateTime CreationDate
		{
			get
			{
				return this.GetDateParameter("creation-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("creation-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the modification date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file modification date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00070C4E File Offset: 0x0006EE4E
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00070C5C File Offset: 0x0006EE5C
		public DateTime ModificationDate
		{
			get
			{
				return this.GetDateParameter("modification-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("modification-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines the disposition type (Inline or Attachment) for an email attachment.</summary>
		/// <returns>
		///   <see langword="true" /> if content in the attachment is presented inline as part of the email body; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00070C86 File Offset: 0x0006EE86
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00070C98 File Offset: 0x0006EE98
		public bool Inline
		{
			get
			{
				return this.dispositionType == "inline";
			}
			set
			{
				this.isChanged = true;
				if (value)
				{
					this.dispositionType = "inline";
					return;
				}
				this.dispositionType = "attachment";
			}
		}

		/// <summary>Gets or sets the read date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file read date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00070CBB File Offset: 0x0006EEBB
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x00070CC8 File Offset: 0x0006EEC8
		public DateTime ReadDate
		{
			get
			{
				return this.GetDateParameter("read-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("read-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the size of a file attachment.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of bytes in the file attachment. The default value is -1, which indicates that the file size is unknown.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00070CF4 File Offset: 0x0006EEF4
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00070D23 File Offset: 0x0006EF23
		public long Size
		{
			get
			{
				object obj = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet("size");
				if (obj == null)
				{
					return -1L;
				}
				return (long)obj;
			}
			set
			{
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("size", value);
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00070D40 File Offset: 0x0006EF40
		internal void Set(string contentDisposition, HeaderCollection headers)
		{
			this.disposition = contentDisposition;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00070D68 File Offset: 0x0006EF68
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00070D9B File Offset: 0x0006EF9B
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the property values for this instance.</returns>
		// Token: 0x060015D3 RID: 5587 RVA: 0x00070DBC File Offset: 0x0006EFBC
		public override string ToString()
		{
			if (this.disposition == null || this.isChanged || (this.parameters != null && this.parameters.IsChanged))
			{
				this.disposition = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.disposition;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00070E1C File Offset: 0x0006F01C
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.dispositionType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentDisposition.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentDisposition.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00070EBC File Offset: 0x0006F0BC
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

		/// <summary>Determines whether the content-disposition header of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object is equal to the content-disposition header of this object.</summary>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentDisposition" /> object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the content-disposition headers are the same; otherwise <see langword="false" />.</returns>
		// Token: 0x060015D6 RID: 5590 RVA: 0x00070F36 File Offset: 0x0006F136
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x060015D7 RID: 5591 RVA: 0x00070F52 File Offset: 0x0006F152
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00070F64 File Offset: 0x0006F164
		private void ParseValue()
		{
			int num = 0;
			try
			{
				this.dispositionType = MailBnfHelper.ReadToken(this.disposition, ref num, null);
				if (string.IsNullOrEmpty(this.dispositionType))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldMalformedHeader"));
				}
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				else
				{
					this.parameters.Clear();
				}
				while (MailBnfHelper.SkipCFWS(this.disposition, ref num))
				{
					if (this.disposition[num++] != ';')
					{
						throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { this.disposition[num - 1] }));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						break;
					}
					string text = MailBnfHelper.ReadParameterAttribute(this.disposition, ref num, null);
					if (this.disposition[num++] != '=')
					{
						throw new FormatException(SR.GetString("MailHeaderFieldMalformedHeader"));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						throw new FormatException(SR.GetString("ContentDispositionInvalid"));
					}
					string text2;
					if (this.disposition[num] == '"')
					{
						text2 = MailBnfHelper.ReadQuotedString(this.disposition, ref num, null);
					}
					else
					{
						text2 = MailBnfHelper.ReadToken(this.disposition, ref num, null);
					}
					if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
					{
						throw new FormatException(SR.GetString("ContentDispositionInvalid"));
					}
					this.Parameters.Add(text, text2);
				}
			}
			catch (FormatException ex)
			{
				throw new FormatException(SR.GetString("ContentDispositionInvalid"), ex);
			}
			this.parameters.IsChanged = false;
		}

		// Token: 0x040016D5 RID: 5845
		private string dispositionType;

		// Token: 0x040016D6 RID: 5846
		private TrackingValidationObjectDictionary parameters;

		// Token: 0x040016D7 RID: 5847
		private bool isChanged;

		// Token: 0x040016D8 RID: 5848
		private bool isPersisted;

		// Token: 0x040016D9 RID: 5849
		private string disposition;

		// Token: 0x040016DA RID: 5850
		private const string creationDate = "creation-date";

		// Token: 0x040016DB RID: 5851
		private const string readDate = "read-date";

		// Token: 0x040016DC RID: 5852
		private const string modificationDate = "modification-date";

		// Token: 0x040016DD RID: 5853
		private const string size = "size";

		// Token: 0x040016DE RID: 5854
		private const string fileName = "filename";

		// Token: 0x040016DF RID: 5855
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue dateParser = (object value) => new SmtpDateTime(value.ToString());

		// Token: 0x040016E0 RID: 5856
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue longParser = delegate(object value)
		{
			long num;
			if (!long.TryParse(value.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				throw new FormatException(SR.GetString("ContentDispositionInvalid"));
			}
			return num;
		};

		// Token: 0x040016E1 RID: 5857
		private static readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators = new Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue>();
	}
}
