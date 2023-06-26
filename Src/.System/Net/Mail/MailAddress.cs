using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x0200026A RID: 618
	public class MailAddress
	{
		// Token: 0x06001727 RID: 5927 RVA: 0x00076621 File Offset: 0x00074821
		internal MailAddress(string displayName, string userName, string domain)
		{
			this.host = domain;
			this.userName = userName;
			this.displayName = displayName;
			this.displayNameEncoding = Encoding.GetEncoding("utf-8");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06001728 RID: 5928 RVA: 0x0007664E File Offset: 0x0007484E
		public MailAddress(string address)
			: this(address, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.  
		/// -or-  
		/// <paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06001729 RID: 5929 RVA: 0x00076659 File Offset: 0x00074859
		public MailAddress(string address, string displayName)
			: this(address, displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="displayName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").  
		/// -or-  
		/// <paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.  
		/// -or-  
		/// <paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x0600172A RID: 5930 RVA: 0x00076664 File Offset: 0x00074864
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "address" }), "address");
			}
			this.displayNameEncoding = displayNameEncoding ?? Encoding.GetEncoding("utf-8");
			this.displayName = displayName ?? string.Empty;
			if (!string.IsNullOrEmpty(this.displayName))
			{
				this.displayName = MailAddressParser.NormalizeOrThrow(this.displayName);
				if (this.displayName.Length >= 2 && this.displayName[0] == '"' && this.displayName[this.displayName.Length - 1] == '"')
				{
					this.displayName = this.displayName.Substring(1, this.displayName.Length - 2);
				}
			}
			MailAddress mailAddress = MailAddressParser.ParseAddress(address);
			this.host = mailAddress.host;
			this.userName = mailAddress.userName;
			if (string.IsNullOrEmpty(this.displayName))
			{
				this.displayName = mailAddress.displayName;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00076789 File Offset: 0x00074989
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x00076791 File Offset: 0x00074991
		public string User
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00076799 File Offset: 0x00074999
		private string GetUser(bool allowUnicode)
		{
			if (!allowUnicode && !MimeBasePart.IsAscii(this.userName, true))
			{
				throw new SmtpException(SR.GetString("SmtpNonAsciiUserNotSupported", new object[] { this.Address }));
			}
			return this.userName;
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts email for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000767D1 File Offset: 0x000749D1
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000767DC File Offset: 0x000749DC
		private string GetHost(bool allowUnicode)
		{
			string ascii = this.host;
			if (!allowUnicode && !MimeBasePart.IsAscii(ascii, true))
			{
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					ascii = idnMapping.GetAscii(ascii);
				}
				catch (ArgumentException ex)
				{
					throw new SmtpException(SR.GetString("SmtpInvalidHostName", new object[] { this.Address }), ex);
				}
			}
			if (!ServicePointManager.AllowFullDomainLiterals && ascii.IndexOfAny(MailAddress.s_newLines) >= 0)
			{
				throw new SmtpException("SmtpInvalidHostName", this.Address);
			}
			return ascii;
		}

		/// <summary>Gets the email address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the email address.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00076864 File Offset: 0x00074A64
		public string Address
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[] { this.userName, this.host });
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0007688D File Offset: 0x00074A8D
		private string GetAddress(bool allowUnicode)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				this.GetUser(allowUnicode),
				this.GetHost(allowUnicode)
			});
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000768B8 File Offset: 0x00074AB8
		private string SmtpAddress
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[] { this.Address });
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000768D8 File Offset: 0x00074AD8
		internal string GetSmtpAddress(bool allowUnicode)
		{
			return string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[] { this.GetAddress(allowUnicode) });
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06001734 RID: 5940 RVA: 0x000768F9 File Offset: 0x00074AF9
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				return this.Address;
			}
			return string.Format("\"{0}\" {1}", this.DisplayName, this.SmtpAddress);
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the two mail addresses are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001735 RID: 5941 RVA: 0x00076925 File Offset: 0x00074B25
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06001736 RID: 5942 RVA: 0x0007693E File Offset: 0x00074B3E
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0007694C File Offset: 0x00074B4C
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.displayName))
			{
				if (MimeBasePart.IsAscii(this.displayName, false) || allowUnicode)
				{
					text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[] { this.displayName });
				}
				else
				{
					IEncodableStream encoderForHeader = MailAddress.encoderFactory.GetEncoderForHeader(this.displayNameEncoding, false, charsConsumed);
					byte[] bytes = this.displayNameEncoding.GetBytes(this.displayName);
					encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
					text = encoderForHeader.GetEncodedString();
				}
				text = text + " " + this.GetSmtpAddress(allowUnicode);
			}
			else
			{
				text = this.GetAddress(allowUnicode);
			}
			return text;
		}

		// Token: 0x04001794 RID: 6036
		private static readonly char[] s_newLines = new char[] { '\r', '\n' };

		// Token: 0x04001795 RID: 6037
		private readonly Encoding displayNameEncoding;

		// Token: 0x04001796 RID: 6038
		private readonly string displayName;

		// Token: 0x04001797 RID: 6039
		private readonly string userName;

		// Token: 0x04001798 RID: 6040
		private readonly string host;

		// Token: 0x04001799 RID: 6041
		private static EncodedStreamFactory encoderFactory = new EncodedStreamFactory();
	}
}
