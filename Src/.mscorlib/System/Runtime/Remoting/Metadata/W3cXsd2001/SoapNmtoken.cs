using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NMTOKEN" /> attribute.</summary>
	// Token: 0x020007F5 RID: 2037
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06005812 RID: 22546 RVA: 0x0013807E File Offset: 0x0013627E
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005813 RID: 22547 RVA: 0x00138085 File Offset: 0x00136285
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class.</summary>
		// Token: 0x06005814 RID: 22548 RVA: 0x0013808C File Offset: 0x0013628C
		public SoapNmtoken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class with an XML <see langword="NMTOKEN" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> containing an XML <see langword="NMTOKEN" /> attribute.</param>
		// Token: 0x06005815 RID: 22549 RVA: 0x00138094 File Offset: 0x00136294
		public SoapNmtoken(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="NMTOKEN" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKEN" /> attribute.</returns>
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005816 RID: 22550 RVA: 0x001380A3 File Offset: 0x001362A3
		// (set) Token: 0x06005817 RID: 22551 RVA: 0x001380AB File Offset: 0x001362AB
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" />.</returns>
		// Token: 0x06005818 RID: 22552 RVA: 0x001380B4 File Offset: 0x001362B4
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005819 RID: 22553 RVA: 0x001380C1 File Offset: 0x001362C1
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		// Token: 0x04002832 RID: 10290
		private string _value;
	}
}
