using System;
using System.Configuration.Internal;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Wraps the corresponding <see cref="T:System.Xml.XmlDocument" /> type and also carries the necessary information for reporting file-name and line numbers.</summary>
	// Token: 0x02000087 RID: 135
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigErrorInfo
	{
		/// <summary>Gets the configuration line number.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0002155C File Offset: 0x0001F75C
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				if (this._reader == null)
				{
					return 0;
				}
				if (this._lineOffset > 0)
				{
					return this._reader.LineNumber + this._lineOffset - 1;
				}
				return this._reader.LineNumber;
			}
		}

		/// <summary>Gets the current node line number.</summary>
		/// <returns>The line number for the current node.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00021591 File Offset: 0x0001F791
		public int LineNumber
		{
			get
			{
				return ((IConfigErrorInfo)this).LineNumber;
			}
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00021599 File Offset: 0x0001F799
		public string Filename
		{
			get
			{
				return ConfigurationException.SafeFilename(this._filename);
			}
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The file name.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000215A6 File Offset: 0x0001F7A6
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		/// <summary>Loads the configuration file.</summary>
		/// <param name="filename">The name of the file.</param>
		// Token: 0x06000533 RID: 1331 RVA: 0x000215B0 File Offset: 0x0001F7B0
		public override void Load(string filename)
		{
			this._filename = filename;
			try
			{
				this._reader = new XmlTextReader(filename);
				this._reader.XmlResolver = null;
				base.Load(this._reader);
			}
			finally
			{
				if (this._reader != null)
				{
					this._reader.Close();
					this._reader = null;
				}
			}
		}

		/// <summary>Loads a single configuration element.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <param name="sourceReader">The source for the reader.</param>
		// Token: 0x06000534 RID: 1332 RVA: 0x00021618 File Offset: 0x0001F818
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this._filename = filename;
			this._lineOffset = sourceReader.LineNumber;
			string text = sourceReader.ReadOuterXml();
			try
			{
				this._reader = new XmlTextReader(new StringReader(text), sourceReader.NameTable);
				base.Load(this._reader);
			}
			finally
			{
				if (this._reader != null)
				{
					this._reader.Close();
					this._reader = null;
				}
			}
		}

		/// <summary>Creates a configuration element attribute.</summary>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name that is used locally.</param>
		/// <param name="namespaceUri">The URL that is assigned to the namespace.</param>
		/// <returns>The <see cref="P:System.Xml.Serialization.XmlAttributes.XmlAttribute" /> attribute.</returns>
		// Token: 0x06000535 RID: 1333 RVA: 0x00021690 File Offset: 0x0001F890
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlAttribute(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		/// <summary>Creates a configuration element.</summary>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name used locally.</param>
		/// <param name="namespaceUri">The namespace for the URL.</param>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> value.</returns>
		// Token: 0x06000536 RID: 1334 RVA: 0x000216A7 File Offset: 0x0001F8A7
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlElement(this._filename, this.LineNumber, prefix, localName, namespaceUri, this);
		}

		/// <summary>Create a text node.</summary>
		/// <param name="text">The text to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlText" /> value.</returns>
		// Token: 0x06000537 RID: 1335 RVA: 0x000216BE File Offset: 0x0001F8BE
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlText(this._filename, this.LineNumber, text, this);
		}

		/// <summary>Creates an XML CData section.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlCDataSection" /> value.</returns>
		// Token: 0x06000538 RID: 1336 RVA: 0x000216D3 File Offset: 0x0001F8D3
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlCDataSection(this._filename, this.LineNumber, data, this);
		}

		/// <summary>Create an XML comment.</summary>
		/// <param name="data">The comment data.</param>
		/// <returns>The <see cref="T:System.Xml.XmlComment" /> value.</returns>
		// Token: 0x06000539 RID: 1337 RVA: 0x000216E8 File Offset: 0x0001F8E8
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlComment(this._filename, this.LineNumber, data, this);
		}

		/// <summary>Creates white spaces.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlSignificantWhitespace" /> value.</returns>
		// Token: 0x0600053A RID: 1338 RVA: 0x000216FD File Offset: 0x0001F8FD
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return new ConfigXmlSignificantWhitespace(this._filename, this.LineNumber, data, this);
		}

		/// <summary>Creates white space.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlWhitespace" /> value.</returns>
		// Token: 0x0600053B RID: 1339 RVA: 0x00021712 File Offset: 0x0001F912
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return new ConfigXmlWhitespace(this._filename, this.LineNumber, data, this);
		}

		// Token: 0x04000C1F RID: 3103
		private XmlTextReader _reader;

		// Token: 0x04000C20 RID: 3104
		private int _lineOffset;

		// Token: 0x04000C21 RID: 3105
		private string _filename;
	}
}
