using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="normalizedString" /> type.</summary>
	// Token: 0x020007EF RID: 2031
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x060057E0 RID: 22496 RVA: 0x00137D6B File Offset: 0x00135F6B
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057E1 RID: 22497 RVA: 0x00137D72 File Offset: 0x00135F72
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> class.</summary>
		// Token: 0x060057E2 RID: 22498 RVA: 0x00137D79 File Offset: 0x00135F79
		public SoapNormalizedString()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> class with a normalized string.</summary>
		/// <param name="value">A <see cref="T:System.String" /> object that contains a normalized string.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9).</exception>
		// Token: 0x060057E3 RID: 22499 RVA: 0x00137D81 File Offset: 0x00135F81
		public SoapNormalizedString(string value)
		{
			this._value = this.Validate(value);
		}

		/// <summary>Gets or sets a normalized string.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains a normalized string.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9).</exception>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x060057E4 RID: 22500 RVA: 0x00137D96 File Offset: 0x00135F96
		// (set) Token: 0x060057E5 RID: 22501 RVA: 0x00137D9E File Offset: 0x00135F9E
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = this.Validate(value);
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> in the format "&lt;![CDATA[" + <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString.Value" /> + "]]&gt;".</returns>
		// Token: 0x060057E6 RID: 22502 RVA: 0x00137DAD File Offset: 0x00135FAD
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNormalizedString" /> object obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> contains invalid characters (0xD, 0xA, or 0x9).</exception>
		// Token: 0x060057E7 RID: 22503 RVA: 0x00137DBA File Offset: 0x00135FBA
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00137DC4 File Offset: 0x00135FC4
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] array = new char[] { '\r', '\n', '\t' };
			int num = value.LastIndexOfAny(array);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:normalizedString", value }));
			}
			return value;
		}

		// Token: 0x0400282C RID: 10284
		private string _value;
	}
}
