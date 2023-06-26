using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="Name" /> type.</summary>
	// Token: 0x020007F2 RID: 2034
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x00137F9D File Offset: 0x0013619D
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057FB RID: 22523 RVA: 0x00137FA4 File Offset: 0x001361A4
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class.</summary>
		// Token: 0x060057FC RID: 22524 RVA: 0x00137FAB File Offset: 0x001361AB
		public SoapName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class with an XML <see langword="Name" /> type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="Name" /> type.</param>
		// Token: 0x060057FD RID: 22525 RVA: 0x00137FB3 File Offset: 0x001361B3
		public SoapName(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="Name" /> type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="Name" /> type.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x060057FE RID: 22526 RVA: 0x00137FC2 File Offset: 0x001361C2
		// (set) Token: 0x060057FF RID: 22527 RVA: 0x00137FCA File Offset: 0x001361CA
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" />.</returns>
		// Token: 0x06005800 RID: 22528 RVA: 0x00137FD3 File Offset: 0x001361D3
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005801 RID: 22529 RVA: 0x00137FE0 File Offset: 0x001361E0
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		// Token: 0x0400282F RID: 10287
		private string _value;
	}
}
