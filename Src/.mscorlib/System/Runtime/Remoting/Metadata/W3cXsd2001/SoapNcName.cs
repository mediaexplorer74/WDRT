using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NcName" /> type.</summary>
	// Token: 0x020007F7 RID: 2039
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x00138114 File Offset: 0x00136314
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005823 RID: 22563 RVA: 0x0013811B File Offset: 0x0013631B
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class.</summary>
		// Token: 0x06005824 RID: 22564 RVA: 0x00138122 File Offset: 0x00136322
		public SoapNcName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class with an XML <see langword="NcName" /> type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</param>
		// Token: 0x06005825 RID: 22565 RVA: 0x0013812A File Offset: 0x0013632A
		public SoapNcName(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="NcName" /> type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x00138139 File Offset: 0x00136339
		// (set) Token: 0x06005827 RID: 22567 RVA: 0x00138141 File Offset: 0x00136341
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" />.</returns>
		// Token: 0x06005828 RID: 22568 RVA: 0x0013814A File Offset: 0x0013634A
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005829 RID: 22569 RVA: 0x00138157 File Offset: 0x00136357
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		// Token: 0x04002834 RID: 10292
		private string _value;
	}
}
