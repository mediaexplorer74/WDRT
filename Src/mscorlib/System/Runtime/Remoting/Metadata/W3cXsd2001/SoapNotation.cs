using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NOTATION" /> attribute type.</summary>
	// Token: 0x020007EE RID: 2030
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x060057D8 RID: 22488 RVA: 0x00137D25 File Offset: 0x00135F25
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057D9 RID: 22489 RVA: 0x00137D2C File Offset: 0x00135F2C
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class.</summary>
		// Token: 0x060057DA RID: 22490 RVA: 0x00137D33 File Offset: 0x00135F33
		public SoapNotation()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class with an XML <see langword="NOTATION" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NOTATION" /> attribute.</param>
		// Token: 0x060057DB RID: 22491 RVA: 0x00137D3B File Offset: 0x00135F3B
		public SoapNotation(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="NOTATION" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NOTATION" /> attribute.</returns>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x00137D4A File Offset: 0x00135F4A
		// (set) Token: 0x060057DD RID: 22493 RVA: 0x00137D52 File Offset: 0x00135F52
		public string Value
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" />.</returns>
		// Token: 0x060057DE RID: 22494 RVA: 0x00137D5B File Offset: 0x00135F5B
		public override string ToString()
		{
			return this._value;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057DF RID: 22495 RVA: 0x00137D63 File Offset: 0x00135F63
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		// Token: 0x0400282B RID: 10283
		private string _value;
	}
}
