using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000783 RID: 1923
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x060053EF RID: 21487 RVA: 0x0012883F File Offset: 0x00126A3F
		internal BinaryObject()
		{
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00128847 File Offset: 0x00126A47
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00128857 File Offset: 0x00126A57
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x00128878 File Offset: 0x00126A78
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00128892 File Offset: 0x00126A92
		public void Dump()
		{
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x00128894 File Offset: 0x00126A94
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040025CD RID: 9677
		internal int objectId;

		// Token: 0x040025CE RID: 9678
		internal int mapId;
	}
}
