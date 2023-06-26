using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(1)]
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06000CB4 RID: 3252
		IXmlNode CreateComment([Nullable(2)] string text);

		// Token: 0x06000CB5 RID: 3253
		IXmlNode CreateTextNode([Nullable(2)] string text);

		// Token: 0x06000CB6 RID: 3254
		IXmlNode CreateCDataSection([Nullable(2)] string data);

		// Token: 0x06000CB7 RID: 3255
		IXmlNode CreateWhitespace([Nullable(2)] string text);

		// Token: 0x06000CB8 RID: 3256
		IXmlNode CreateSignificantWhitespace([Nullable(2)] string text);

		// Token: 0x06000CB9 RID: 3257
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone);

		// Token: 0x06000CBA RID: 3258
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset);

		// Token: 0x06000CBB RID: 3259
		IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data);

		// Token: 0x06000CBC RID: 3260
		IXmlElement CreateElement(string elementName);

		// Token: 0x06000CBD RID: 3261
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x06000CBE RID: 3262
		IXmlNode CreateAttribute(string name, [Nullable(2)] string value);

		// Token: 0x06000CBF RID: 3263
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value);

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CC0 RID: 3264
		[Nullable(2)]
		IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get;
		}
	}
}
