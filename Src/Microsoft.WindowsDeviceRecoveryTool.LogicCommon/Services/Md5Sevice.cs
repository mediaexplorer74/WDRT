using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000E RID: 14
	[Export]
	public class Md5Sevice : IDisposable, IChecksumService
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000027B8 File Offset: 0x000009B8
		[ImportingConstructor]
		public Md5Sevice()
		{
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600009A RID: 154 RVA: 0x000038EC File Offset: 0x00001AEC
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x00003924 File Offset: 0x00001B24
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> ProgressEvent;

		// Token: 0x0600009C RID: 156 RVA: 0x00003959 File Offset: 0x00001B59
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000396C File Offset: 0x00001B6C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003990 File Offset: 0x00001B90
		public byte[] CalculateMd5(string filePath, CancellationToken cancellationToken)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			bool flag = !fileInfo.Exists;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("File '{0}' not found.", filePath));
			}
			byte[] array;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = this.CalculateMd5(fileStream, cancellationToken);
			}
			return array;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000039F8 File Offset: 0x00001BF8
		private byte[] CalculateMd5(Stream fileStream, CancellationToken cancellationToken)
		{
			bool flag = fileStream.Length == 0L;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				using (MD5 md = MD5.Create())
				{
					byte[] array2 = new byte[4096];
					long num = 0L;
					int num2;
					do
					{
						num2 = fileStream.Read(array2, 0, 4096);
						bool flag2 = num2 > 0;
						if (flag2)
						{
							md.TransformBlock(array2, 0, num2, array2, 0);
						}
						num += (long)num2;
						bool flag3 = num % 4096000L == 0L;
						if (flag3)
						{
							this.RaiseProgressEvent((int)(num * 100L / fileStream.Length % 101L));
						}
						cancellationToken.ThrowIfCancellationRequested();
					}
					while (num2 > 0);
					md.TransformFinalBlock(array2, 0, num2);
					array = md.Hash;
				}
			}
			return array;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003AD8 File Offset: 0x00001CD8
		private void RaiseProgressEvent(int progress)
		{
			Action<int> progressEvent = this.ProgressEvent;
			bool flag = progressEvent != null;
			if (flag)
			{
				progressEvent(progress);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B00 File Offset: 0x00001D00
		public bool IsOfType(string checksumTypeName)
		{
			return string.Equals(checksumTypeName, "md5", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B20 File Offset: 0x00001D20
		public byte[] CalculateChecksum(string filePath, CancellationToken cancellationToken)
		{
			return this.CalculateMd5(filePath, cancellationToken);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B3C File Offset: 0x00001D3C
		public byte[] CalculateChecksum(FileStream fileStream, CancellationToken cancellationToken)
		{
			return this.CalculateMd5(fileStream, cancellationToken);
		}

		// Token: 0x04000033 RID: 51
		private const string MsrChecksumTypeName = "md5";

		// Token: 0x04000034 RID: 52
		private bool disposed;
	}
}
