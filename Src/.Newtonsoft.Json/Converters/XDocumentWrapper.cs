using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FA RID: 250
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00033124 File Offset: 0x00031324
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00033131 File Offset: 0x00031331
		public XDocumentWrapper(XDocument document)
			: base(document)
		{
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003313C File Offset: 0x0003133C
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0003318D File Offset: 0x0003138D
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000331A7 File Offset: 0x000313A7
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000331B4 File Offset: 0x000313B4
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000331C1 File Offset: 0x000313C1
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000331CE File Offset: 0x000313CE
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000331DB File Offset: 0x000313DB
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000331E8 File Offset: 0x000313E8
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000331F7 File Offset: 0x000313F7
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00033208 File Offset: 0x00031408
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00033216 File Offset: 0x00031416
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00033228 File Offset: 0x00031428
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00033240 File Offset: 0x00031440
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00033253 File Offset: 0x00031453
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0003326C File Offset: 0x0003146C
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00033290 File Offset: 0x00031490
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
