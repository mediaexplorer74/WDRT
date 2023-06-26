using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000221 RID: 545
	public sealed class AtomTextConstruct : ODataAnnotatable
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0003FC86 File Offset: 0x0003DE86
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0003FC8E File Offset: 0x0003DE8E
		public AtomTextConstructKind Kind { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0003FC97 File Offset: 0x0003DE97
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0003FC9F File Offset: 0x0003DE9F
		public string Text { get; set; }

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003FCA8 File Offset: 0x0003DEA8
		public static AtomTextConstruct ToTextConstruct(string text)
		{
			return new AtomTextConstruct
			{
				Text = text
			};
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003FCC3 File Offset: 0x0003DEC3
		public static implicit operator AtomTextConstruct(string text)
		{
			return AtomTextConstruct.ToTextConstruct(text);
		}
	}
}
