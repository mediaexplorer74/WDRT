using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="anyURI" /> type.</summary>
	// Token: 0x020007EC RID: 2028
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x00137BD9 File Offset: 0x00135DD9
		public static string XsdType
		{
			get
			{
				return "anyURI";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057C3 RID: 22467 RVA: 0x00137BE0 File Offset: 0x00135DE0
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class.</summary>
		// Token: 0x060057C4 RID: 22468 RVA: 0x00137BE7 File Offset: 0x00135DE7
		public SoapAnyUri()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class with the specified URI.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains a URI.</param>
		// Token: 0x060057C5 RID: 22469 RVA: 0x00137BEF File Offset: 0x00135DEF
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets a URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a URI.</returns>
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x00137BFE File Offset: 0x00135DFE
		// (set) Token: 0x060057C7 RID: 22471 RVA: 0x00137C06 File Offset: 0x00135E06
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />.</returns>
		// Token: 0x060057C8 RID: 22472 RVA: 0x00137C0F File Offset: 0x00135E0F
		public override string ToString()
		{
			return this._value;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057C9 RID: 22473 RVA: 0x00137C17 File Offset: 0x00135E17
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		// Token: 0x04002827 RID: 10279
		private string _value;
	}
}
