using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="gMonthDay" /> type.</summary>
	// Token: 0x020007E2 RID: 2018
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600576D RID: 22381 RVA: 0x00137384 File Offset: 0x00135584
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600576E RID: 22382 RVA: 0x0013738B File Offset: 0x0013558B
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class.</summary>
		// Token: 0x0600576F RID: 22383 RVA: 0x00137392 File Offset: 0x00135592
		public SoapMonthDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x06005770 RID: 22384 RVA: 0x001373A5 File Offset: 0x001355A5
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x001373BF File Offset: 0x001355BF
		// (set) Token: 0x06005772 RID: 22386 RVA: 0x001373C7 File Offset: 0x001355C7
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> in the format "'--'MM'-'dd".</returns>
		// Token: 0x06005773 RID: 22387 RVA: 0x001373D0 File Offset: 0x001355D0
		public override string ToString()
		{
			return this._value.ToString("'--'MM'-'dd", CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x06005774 RID: 22388 RVA: 0x001373E7 File Offset: 0x001355E7
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x04002819 RID: 10265
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400281A RID: 10266
		private static string[] formats = new string[] { "--MM-dd", "--MM-ddzzz" };
	}
}
