using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SoftwareRepository.Streaming
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal class DownloadMetadata
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					new BinaryFormatter().Serialize(memoryStream, this);
				}
				catch
				{
					return null;
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D24 File Offset: 0x00000F24
		internal static DownloadMetadata Deserialize(byte[] data)
		{
			DownloadMetadata downloadMetadata;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				try
				{
					downloadMetadata = new BinaryFormatter().Deserialize(memoryStream) as DownloadMetadata;
				}
				catch
				{
					return null;
				}
			}
			bool flag = downloadMetadata == null || !downloadMetadata.IsValid();
			DownloadMetadata downloadMetadata2;
			if (flag)
			{
				downloadMetadata2 = null;
			}
			else
			{
				downloadMetadata2 = downloadMetadata;
			}
			return downloadMetadata2;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D9C File Offset: 0x00000F9C
		private bool IsValid()
		{
			bool flag = this.ChunkStates == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num = 0;
				for (int i = 0; i < this.ChunkStates.Length; i++)
				{
					bool flag3 = this.ChunkStates[i] == ChunkState.PartiallyDownloaded;
					if (flag3)
					{
						num++;
						bool flag4 = this.PartialProgress == null || !this.PartialProgress.ContainsKey(i);
						if (flag4)
						{
							return false;
						}
					}
				}
				flag2 = this.PartialProgress == null || num == this.PartialProgress.Count;
			}
			return flag2;
		}

		// Token: 0x04000028 RID: 40
		internal ChunkState[] ChunkStates;

		// Token: 0x04000029 RID: 41
		internal Dictionary<int, long> PartialProgress;
	}
}
