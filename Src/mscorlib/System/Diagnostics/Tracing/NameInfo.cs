using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000449 RID: 1097
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x06003662 RID: 13922 RVA: 0x000D4624 File Offset: 0x000D2824
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000D465C File Offset: 0x000D285C
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = tags & (EventTags)268435455;
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int num = 0;
			Statics.EncodeTags((int)this.tags, ref num, null);
			this.nameMetadata = Statics.MetadataForString(name, num, 0, typeMetadataSize);
			num = 2;
			Statics.EncodeTags((int)this.tags, ref num, this.nameMetadata);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000D46C7 File Offset: 0x000D28C7
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000D46DB File Offset: 0x000D28DB
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000D46F8 File Offset: 0x000D28F8
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? (-1) : 1);
			}
			return num;
		}

		// Token: 0x04001853 RID: 6227
		private static int lastIdentity = 184549376;

		// Token: 0x04001854 RID: 6228
		internal readonly string name;

		// Token: 0x04001855 RID: 6229
		internal readonly EventTags tags;

		// Token: 0x04001856 RID: 6230
		internal readonly int identity;

		// Token: 0x04001857 RID: 6231
		internal readonly byte[] nameMetadata;
	}
}
