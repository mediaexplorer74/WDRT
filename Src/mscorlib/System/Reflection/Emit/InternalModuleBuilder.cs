using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200064E RID: 1614
	internal sealed class InternalModuleBuilder : RuntimeModule
	{
		// Token: 0x06004C20 RID: 19488 RVA: 0x00114971 File Offset: 0x00112B71
		private InternalModuleBuilder()
		{
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x00114979 File Offset: 0x00112B79
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalModuleBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x00114994 File Offset: 0x00112B94
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
