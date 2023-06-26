using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ID" /> attribute.</summary>
	// Token: 0x020007F8 RID: 2040
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x0013815F File Offset: 0x0013635F
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600582B RID: 22571 RVA: 0x00138166 File Offset: 0x00136366
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class.</summary>
		// Token: 0x0600582C RID: 22572 RVA: 0x0013816D File Offset: 0x0013636D
		public SoapId()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class with an XML <see langword="ID" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ID" /> attribute.</param>
		// Token: 0x0600582D RID: 22573 RVA: 0x00138175 File Offset: 0x00136375
		public SoapId(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="ID" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ID" /> attribute.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x00138184 File Offset: 0x00136384
		// (set) Token: 0x0600582F RID: 22575 RVA: 0x0013818C File Offset: 0x0013638C
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" />.</returns>
		// Token: 0x06005830 RID: 22576 RVA: 0x00138195 File Offset: 0x00136395
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005831 RID: 22577 RVA: 0x001381A2 File Offset: 0x001363A2
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		// Token: 0x04002835 RID: 10293
		private string _value;
	}
}
