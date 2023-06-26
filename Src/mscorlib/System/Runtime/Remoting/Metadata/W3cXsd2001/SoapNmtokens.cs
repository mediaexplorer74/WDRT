using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NMTOKENS" /> attribute.</summary>
	// Token: 0x020007F6 RID: 2038
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x001380C9 File Offset: 0x001362C9
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600581B RID: 22555 RVA: 0x001380D0 File Offset: 0x001362D0
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class.</summary>
		// Token: 0x0600581C RID: 22556 RVA: 0x001380D7 File Offset: 0x001362D7
		public SoapNmtokens()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class with an XML <see langword="NMTOKENS" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</param>
		// Token: 0x0600581D RID: 22557 RVA: 0x001380DF File Offset: 0x001362DF
		public SoapNmtokens(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="NMTOKENS" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600581E RID: 22558 RVA: 0x001380EE File Offset: 0x001362EE
		// (set) Token: 0x0600581F RID: 22559 RVA: 0x001380F6 File Offset: 0x001362F6
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />.</returns>
		// Token: 0x06005820 RID: 22560 RVA: 0x001380FF File Offset: 0x001362FF
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005821 RID: 22561 RVA: 0x0013810C File Offset: 0x0013630C
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		// Token: 0x04002833 RID: 10291
		private string _value;
	}
}
