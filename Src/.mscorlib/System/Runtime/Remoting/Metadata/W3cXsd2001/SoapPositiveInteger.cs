using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="positiveInteger" /> type.</summary>
	// Token: 0x020007E8 RID: 2024
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0013782D File Offset: 0x00135A2D
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057A3 RID: 22435 RVA: 0x00137834 File Offset: 0x00135A34
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> class.</summary>
		// Token: 0x060057A4 RID: 22436 RVA: 0x0013783B File Offset: 0x00135A3B
		public SoapPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x060057A5 RID: 22437 RVA: 0x00137844 File Offset: 0x00135A44
		public SoapPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
			}
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> indicating the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0013789A File Offset: 0x00135A9A
		// (set) Token: 0x060057A7 RID: 22439 RVA: 0x001378A4 File Offset: 0x00135AA4
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
				}
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" />.</returns>
		// Token: 0x060057A8 RID: 22440 RVA: 0x001378F4 File Offset: 0x00135AF4
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057A9 RID: 22441 RVA: 0x00137906 File Offset: 0x00135B06
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002823 RID: 10275
		private decimal _value;
	}
}
