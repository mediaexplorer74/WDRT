using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000008 RID: 8
	[Export]
	public class Crc32Service : IDisposable
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000027B8 File Offset: 0x000009B8
		[ImportingConstructor]
		public Crc32Service()
		{
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00002F1C File Offset: 0x0000111C
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x00002F54 File Offset: 0x00001154
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> Crc32ProgressEvent;

		// Token: 0x0600005E RID: 94 RVA: 0x00002F89 File Offset: 0x00001189
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F9C File Offset: 0x0000119C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FC0 File Offset: 0x000011C0
		public uint CalculateCrc32(string filePath, CancellationToken cancellationToken)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			bool flag = !fileInfo.Exists;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("File '{0}' not found.", filePath));
			}
			uint num;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				num = this.CalculateCrc32(fileStream, cancellationToken);
			}
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003028 File Offset: 0x00001228
		private uint CalculateCrc32(Stream fileStream, CancellationToken cancellationToken)
		{
			uint[] array = new uint[256];
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num;
				for (int i = 8; i > 0; i--)
				{
					bool flag = (num2 & 1U) == 1U;
					if (flag)
					{
						num2 = (num2 >> 1) ^ 3988292384U;
					}
					else
					{
						num2 >>= 1;
					}
				}
				array[(int)num] = num2;
			}
			uint num3 = uint.MaxValue;
			long num4 = 0L;
			for (;;)
			{
				byte[] array2 = new byte[65536];
				int num5 = fileStream.Read(array2, 0, array2.Length);
				num4 += (long)num5;
				bool flag2 = num5 == 0;
				if (flag2)
				{
					break;
				}
				for (int j = 0; j < num5; j++)
				{
					num3 = (num3 >> 8) ^ array[(int)((uint)array2[j] ^ (num3 & 255U))];
				}
				bool flag3 = num4 % 65536000L == 0L;
				if (flag3)
				{
					this.RaiseProgressEvent((int)(num4 * 100L / fileStream.Length % 101L));
				}
				cancellationToken.ThrowIfCancellationRequested();
			}
			return ~num3;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000314C File Offset: 0x0000134C
		private void RaiseProgressEvent(int progress)
		{
			Action<int> crc32ProgressEvent = this.Crc32ProgressEvent;
			bool flag = crc32ProgressEvent != null;
			if (flag)
			{
				crc32ProgressEvent(progress);
			}
		}

		// Token: 0x04000021 RID: 33
		private bool disposed;
	}
}
