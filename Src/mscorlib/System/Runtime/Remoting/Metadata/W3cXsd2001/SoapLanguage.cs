using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="language" /> type.</summary>
	// Token: 0x020007F1 RID: 2033
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x00137F52 File Offset: 0x00136152
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057F3 RID: 22515 RVA: 0x00137F59 File Offset: 0x00136159
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class.</summary>
		// Token: 0x060057F4 RID: 22516 RVA: 0x00137F60 File Offset: 0x00136160
		public SoapLanguage()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class with the language identifier value of <see langword="language" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the language identifier value of a <see langword="language" /> attribute.</param>
		// Token: 0x060057F5 RID: 22517 RVA: 0x00137F68 File Offset: 0x00136168
		public SoapLanguage(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the language identifier of a <see langword="language" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the language identifier of a <see langword="language" /> attribute.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x00137F77 File Offset: 0x00136177
		// (set) Token: 0x060057F7 RID: 22519 RVA: 0x00137F7F File Offset: 0x0013617F
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" />.</returns>
		// Token: 0x060057F8 RID: 22520 RVA: 0x00137F88 File Offset: 0x00136188
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057F9 RID: 22521 RVA: 0x00137F95 File Offset: 0x00136195
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		// Token: 0x0400282E RID: 10286
		private string _value;
	}
}
