using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F8 RID: 248
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0003307A File Offset: 0x0003127A
		internal XDeclaration Declaration { get; }

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00033082 File Offset: 0x00031282
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00033092 File Offset: 0x00031292
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00033096 File Offset: 0x00031296
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x000330A3 File Offset: 0x000312A3
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x000330B0 File Offset: 0x000312B0
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000330BE File Offset: 0x000312BE
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x000330CB File Offset: 0x000312CB
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
