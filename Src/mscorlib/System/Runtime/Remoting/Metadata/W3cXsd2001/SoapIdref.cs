using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="IDREFS" /> attribute.</summary>
	// Token: 0x020007F9 RID: 2041
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x001381AA File Offset: 0x001363AA
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005833 RID: 22579 RVA: 0x001381B1 File Offset: 0x001363B1
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class.</summary>
		// Token: 0x06005834 RID: 22580 RVA: 0x001381B8 File Offset: 0x001363B8
		public SoapIdref()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class with an XML <see langword="IDREF" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="IDREF" /> attribute.</param>
		// Token: 0x06005835 RID: 22581 RVA: 0x001381C0 File Offset: 0x001363C0
		public SoapIdref(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="IDREF" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="IDREF" /> attribute.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x001381CF File Offset: 0x001363CF
		// (set) Token: 0x06005837 RID: 22583 RVA: 0x001381D7 File Offset: 0x001363D7
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.</returns>
		// Token: 0x06005838 RID: 22584 RVA: 0x001381E0 File Offset: 0x001363E0
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> obtained from <paramref name="value" />.</returns>
		// Token: 0x06005839 RID: 22585 RVA: 0x001381ED File Offset: 0x001363ED
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		// Token: 0x04002836 RID: 10294
		private string _value;
	}
}
