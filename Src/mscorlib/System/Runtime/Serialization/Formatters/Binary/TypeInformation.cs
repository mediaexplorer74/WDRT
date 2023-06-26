using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A3 RID: 1955
	internal sealed class TypeInformation
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x0012F7C2 File Offset: 0x0012D9C2
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x0012F7CA File Offset: 0x0012D9CA
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x0012F7D2 File Offset: 0x0012D9D2
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0012F7DA File Offset: 0x0012D9DA
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x04002716 RID: 10006
		private string fullTypeName;

		// Token: 0x04002717 RID: 10007
		private string assemblyString;

		// Token: 0x04002718 RID: 10008
		private bool hasTypeForwardedFrom;
	}
}
