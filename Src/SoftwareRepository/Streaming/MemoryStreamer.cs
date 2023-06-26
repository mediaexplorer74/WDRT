using System;
using System.IO;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000019 RID: 25
	public class MemoryStreamer : Streamer
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003EAC File Offset: 0x000020AC
		protected override Stream GetStreamInternal()
		{
			return new MemoryStream();
		}
	}
}
