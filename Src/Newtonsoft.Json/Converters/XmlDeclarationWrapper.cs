using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F0 RID: 240
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x00032D1A File Offset: 0x00030F1A
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00032D2A File Offset: 0x00030F2A
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00032D37 File Offset: 0x00030F37
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00032D44 File Offset: 0x00030F44
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00032D52 File Offset: 0x00030F52
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00032D5F File Offset: 0x00030F5F
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x040003EA RID: 1002
		private readonly XmlDeclaration _declaration;
	}
}
