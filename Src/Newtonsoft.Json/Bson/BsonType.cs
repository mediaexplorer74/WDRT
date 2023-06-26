using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000111 RID: 273
	internal enum BsonType : sbyte
	{
		// Token: 0x0400042F RID: 1071
		Number = 1,
		// Token: 0x04000430 RID: 1072
		String,
		// Token: 0x04000431 RID: 1073
		Object,
		// Token: 0x04000432 RID: 1074
		Array,
		// Token: 0x04000433 RID: 1075
		Binary,
		// Token: 0x04000434 RID: 1076
		Undefined,
		// Token: 0x04000435 RID: 1077
		Oid,
		// Token: 0x04000436 RID: 1078
		Boolean,
		// Token: 0x04000437 RID: 1079
		Date,
		// Token: 0x04000438 RID: 1080
		Null,
		// Token: 0x04000439 RID: 1081
		Regex,
		// Token: 0x0400043A RID: 1082
		Reference,
		// Token: 0x0400043B RID: 1083
		Code,
		// Token: 0x0400043C RID: 1084
		Symbol,
		// Token: 0x0400043D RID: 1085
		CodeWScope,
		// Token: 0x0400043E RID: 1086
		Integer,
		// Token: 0x0400043F RID: 1087
		TimeStamp,
		// Token: 0x04000440 RID: 1088
		Long,
		// Token: 0x04000441 RID: 1089
		MinKey = -1,
		// Token: 0x04000442 RID: 1090
		MaxKey = 127
	}
}
