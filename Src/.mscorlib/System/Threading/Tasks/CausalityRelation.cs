using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200057C RID: 1404
	internal enum CausalityRelation
	{
		// Token: 0x04001B8B RID: 7051
		AssignDelegate,
		// Token: 0x04001B8C RID: 7052
		Join,
		// Token: 0x04001B8D RID: 7053
		Choice,
		// Token: 0x04001B8E RID: 7054
		Cancel,
		// Token: 0x04001B8F RID: 7055
		Error
	}
}
