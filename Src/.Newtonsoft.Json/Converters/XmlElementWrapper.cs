using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EF RID: 239
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x00032CC1 File Offset: 0x00030EC1
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00032CD4 File Offset: 0x00030ED4
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00032CFF File Offset: 0x00030EFF
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00032D0D File Offset: 0x00030F0D
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private readonly XmlElement _element;
	}
}
