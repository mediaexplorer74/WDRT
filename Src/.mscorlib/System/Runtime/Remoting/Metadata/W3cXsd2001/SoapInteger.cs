using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="integer" /> type.</summary>
	// Token: 0x020007E7 RID: 2023
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x001377C8 File Offset: 0x001359C8
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600579B RID: 22427 RVA: 0x001377CF File Offset: 0x001359CF
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class.</summary>
		// Token: 0x0600579C RID: 22428 RVA: 0x001377D6 File Offset: 0x001359D6
		public SoapInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		// Token: 0x0600579D RID: 22429 RVA: 0x001377DE File Offset: 0x001359DE
		public SoapInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x001377F2 File Offset: 0x001359F2
		// (set) Token: 0x0600579F RID: 22431 RVA: 0x001377FA File Offset: 0x001359FA
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" />.</returns>
		// Token: 0x060057A0 RID: 22432 RVA: 0x00137808 File Offset: 0x00135A08
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057A1 RID: 22433 RVA: 0x0013781A File Offset: 0x00135A1A
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002822 RID: 10274
		private decimal _value;
	}
}
