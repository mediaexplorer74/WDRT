using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ENTITIES" /> attribute.</summary>
	// Token: 0x020007F4 RID: 2036
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x00138033 File Offset: 0x00136233
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600580B RID: 22539 RVA: 0x0013803A File Offset: 0x0013623A
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class.</summary>
		// Token: 0x0600580C RID: 22540 RVA: 0x00138041 File Offset: 0x00136241
		public SoapEntities()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class with an XML <see langword="ENTITIES" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ENTITIES" /> attribute.</param>
		// Token: 0x0600580D RID: 22541 RVA: 0x00138049 File Offset: 0x00136249
		public SoapEntities(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="ENTITIES" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ENTITIES" /> attribute.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x00138058 File Offset: 0x00136258
		// (set) Token: 0x0600580F RID: 22543 RVA: 0x00138060 File Offset: 0x00136260
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" />.</returns>
		// Token: 0x06005810 RID: 22544 RVA: 0x00138069 File Offset: 0x00136269
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005811 RID: 22545 RVA: 0x00138076 File Offset: 0x00136276
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		// Token: 0x04002831 RID: 10289
		private string _value;
	}
}
