using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="IDREFS" /> attribute.</summary>
	// Token: 0x020007F3 RID: 2035
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06005802 RID: 22530 RVA: 0x00137FE8 File Offset: 0x001361E8
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005803 RID: 22531 RVA: 0x00137FEF File Offset: 0x001361EF
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class.</summary>
		// Token: 0x06005804 RID: 22532 RVA: 0x00137FF6 File Offset: 0x001361F6
		public SoapIdrefs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class with an XML <see langword="IDREFS" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="IDREFS" /> attribute.</param>
		// Token: 0x06005805 RID: 22533 RVA: 0x00137FFE File Offset: 0x001361FE
		public SoapIdrefs(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="IDREFS" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="IDREFS" /> attribute.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x0013800D File Offset: 0x0013620D
		// (set) Token: 0x06005807 RID: 22535 RVA: 0x00138015 File Offset: 0x00136215
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" />.</returns>
		// Token: 0x06005808 RID: 22536 RVA: 0x0013801E File Offset: 0x0013621E
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005809 RID: 22537 RVA: 0x0013802B File Offset: 0x0013622B
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		// Token: 0x04002830 RID: 10288
		private string _value;
	}
}
