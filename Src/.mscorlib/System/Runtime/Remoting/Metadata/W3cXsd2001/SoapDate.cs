using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="date" /> type.</summary>
	// Token: 0x020007DF RID: 2015
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005749 RID: 22345 RVA: 0x00136FD2 File Offset: 0x001351D2
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600574A RID: 22346 RVA: 0x00136FD9 File Offset: 0x001351D9
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class.</summary>
		// Token: 0x0600574B RID: 22347 RVA: 0x00136FE0 File Offset: 0x001351E0
		public SoapDate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x0600574C RID: 22348 RVA: 0x00137008 File Offset: 0x00135208
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive.</param>
		// Token: 0x0600574D RID: 22349 RVA: 0x00137038 File Offset: 0x00135238
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600574E RID: 22350 RVA: 0x0013706C File Offset: 0x0013526C
		// (set) Token: 0x0600574F RID: 22351 RVA: 0x00137074 File Offset: 0x00135274
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value.Date;
			}
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is positive or negative.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x00137083 File Offset: 0x00135283
		// (set) Token: 0x06005751 RID: 22353 RVA: 0x0013708B File Offset: 0x0013528B
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> in the format "yyyy-MM-dd" or "'-'yyyy-MM-dd" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Sign" /> is negative.</returns>
		// Token: 0x06005752 RID: 22354 RVA: 0x00137094 File Offset: 0x00135294
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x06005753 RID: 22355 RVA: 0x001370CC File Offset: 0x001352CC
		public static SoapDate Parse(string value)
		{
			int num = 0;
			if (value[0] == '-')
			{
				num = -1;
			}
			return new SoapDate(DateTime.ParseExact(value, SoapDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), num);
		}

		// Token: 0x04002810 RID: 10256
		private DateTime _value = DateTime.MinValue.Date;

		// Token: 0x04002811 RID: 10257
		private int _sign;

		// Token: 0x04002812 RID: 10258
		private static string[] formats = new string[] { "yyyy-MM-dd", "'+'yyyy-MM-dd", "'-'yyyy-MM-dd", "yyyy-MM-ddzzz", "'+'yyyy-MM-ddzzz", "'-'yyyy-MM-ddzzz" };
	}
}
