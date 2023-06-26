using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ENTITY" /> attribute.</summary>
	// Token: 0x020007FA RID: 2042
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600583A RID: 22586 RVA: 0x001381F5 File Offset: 0x001363F5
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600583B RID: 22587 RVA: 0x001381FC File Offset: 0x001363FC
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class.</summary>
		// Token: 0x0600583C RID: 22588 RVA: 0x00138203 File Offset: 0x00136403
		public SoapEntity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class with an XML <see langword="ENTITY" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ENTITY" /> attribute.</param>
		// Token: 0x0600583D RID: 22589 RVA: 0x0013820B File Offset: 0x0013640B
		public SoapEntity(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="ENTITY" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ENTITY" /> attribute.</returns>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x0013821A File Offset: 0x0013641A
		// (set) Token: 0x0600583F RID: 22591 RVA: 0x00138222 File Offset: 0x00136422
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" />.</returns>
		// Token: 0x06005840 RID: 22592 RVA: 0x0013822B File Offset: 0x0013642B
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06005841 RID: 22593 RVA: 0x00138238 File Offset: 0x00136438
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		// Token: 0x04002837 RID: 10295
		private string _value;
	}
}
