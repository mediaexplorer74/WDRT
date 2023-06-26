using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="token" /> type.</summary>
	// Token: 0x020007F0 RID: 2032
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x00137E1F File Offset: 0x0013601F
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060057EA RID: 22506 RVA: 0x00137E26 File Offset: 0x00136026
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class.</summary>
		// Token: 0x060057EB RID: 22507 RVA: 0x00137E2D File Offset: 0x0013602D
		public SoapToken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class with an XML <see langword="token" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="token" />.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:  
		///
		/// <paramref name="value" /> contains invalid characters (0xD or 0x9).  
		///
		/// <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.  
		///
		/// <paramref name="value" /> contains any spaces.</exception>
		// Token: 0x060057EC RID: 22508 RVA: 0x00137E35 File Offset: 0x00136035
		public SoapToken(string value)
		{
			this._value = this.Validate(value);
		}

		/// <summary>Gets or sets an XML <see langword="token" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="token" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).  
		///
		/// <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.  
		///
		/// <paramref name="value" /> contains any spaces.</exception>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x00137E4A File Offset: 0x0013604A
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x00137E52 File Offset: 0x00136052
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />.</returns>
		// Token: 0x060057EF RID: 22511 RVA: 0x00137E61 File Offset: 0x00136061
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057F0 RID: 22512 RVA: 0x00137E6E File Offset: 0x0013606E
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x00137E78 File Offset: 0x00136078
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] array = new char[] { '\r', '\t' };
			int num = value.LastIndexOfAny(array);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			num = value.IndexOf("  ");
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			return value;
		}

		// Token: 0x0400282D RID: 10285
		private string _value;
	}
}
