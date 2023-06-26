using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="QName" /> type.</summary>
	// Token: 0x020007ED RID: 2029
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x00137C1F File Offset: 0x00135E1F
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x060057CB RID: 22475 RVA: 0x00137C26 File Offset: 0x00135E26
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class.</summary>
		// Token: 0x060057CC RID: 22476 RVA: 0x00137C2D File Offset: 0x00135E2D
		public SoapQName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the local part of a qualified name.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the local part of a qualified name.</param>
		// Token: 0x060057CD RID: 22477 RVA: 0x00137C35 File Offset: 0x00135E35
		public SoapQName(string value)
		{
			this._name = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias and the local part of a qualified name.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name.</param>
		// Token: 0x060057CE RID: 22478 RVA: 0x00137C44 File Offset: 0x00135E44
		public SoapQName(string key, string name)
		{
			this._name = name;
			this._key = key;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias, the local part of a qualified name, and the namespace that is referenced by the alias.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name.</param>
		/// <param name="namespaceValue">A <see cref="T:System.String" /> that contains the namespace that is referenced by <paramref name="key" />.</param>
		// Token: 0x060057CF RID: 22479 RVA: 0x00137C5A File Offset: 0x00135E5A
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._name = name;
			this._namespace = namespaceValue;
			this._key = key;
		}

		/// <summary>Gets or sets the name portion of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name portion of a qualified name.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x00137C77 File Offset: 0x00135E77
		// (set) Token: 0x060057D1 RID: 22481 RVA: 0x00137C7F File Offset: 0x00135E7F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x00137C88 File Offset: 0x00135E88
		// (set) Token: 0x060057D3 RID: 22483 RVA: 0x00137C90 File Offset: 0x00135E90
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		/// <summary>Gets or sets the namespace alias of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x060057D4 RID: 22484 RVA: 0x00137C99 File Offset: 0x00135E99
		// (set) Token: 0x060057D5 RID: 22485 RVA: 0x00137CA1 File Offset: 0x00135EA1
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Returns the qualified name as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> in the format " <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" /> ". If <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> is not specified, this method returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />.</returns>
		// Token: 0x060057D6 RID: 22486 RVA: 0x00137CAA File Offset: 0x00135EAA
		public override string ToString()
		{
			if (this._key == null || this._key.Length == 0)
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060057D7 RID: 22487 RVA: 0x00137CE0 File Offset: 0x00135EE0
		public static SoapQName Parse(string value)
		{
			if (value == null)
			{
				return new SoapQName();
			}
			string text = "";
			string text2 = value;
			int num = value.IndexOf(':');
			if (num > 0)
			{
				text = value.Substring(0, num);
				text2 = value.Substring(num + 1);
			}
			return new SoapQName(text, text2);
		}

		// Token: 0x04002828 RID: 10280
		private string _name;

		// Token: 0x04002829 RID: 10281
		private string _namespace;

		// Token: 0x0400282A RID: 10282
		private string _key;
	}
}
