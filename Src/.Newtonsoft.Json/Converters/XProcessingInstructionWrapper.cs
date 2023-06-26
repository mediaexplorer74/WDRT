using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FD RID: 253
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00033365 File Offset: 0x00031565
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00033372 File Offset: 0x00031572
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0003337B File Offset: 0x0003157B
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00033388 File Offset: 0x00031588
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00033395 File Offset: 0x00031595
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
