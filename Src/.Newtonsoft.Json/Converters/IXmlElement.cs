using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F6 RID: 246
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000CCA RID: 3274
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000CCB RID: 3275
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CCC RID: 3276
		bool IsEmpty { get; }
	}
}
