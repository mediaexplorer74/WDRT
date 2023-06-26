using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000012 RID: 18
	[Export]
	public class Sha256Service : IDisposable, IChecksumService
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000DE RID: 222 RVA: 0x00005144 File Offset: 0x00003344
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x0000517C File Offset: 0x0000337C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> ProgressEvent;

		// Token: 0x060000E0 RID: 224 RVA: 0x000027B8 File Offset: 0x000009B8
		[ImportingConstructor]
		public Sha256Service()
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000051B1 File Offset: 0x000033B1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000051C4 File Offset: 0x000033C4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000051E8 File Offset: 0x000033E8
		private void RaiseProgressEvent(int progress)
		{
			Action<int> progressEvent = this.ProgressEvent;
			bool flag = progressEvent != null;
			if (flag)
			{
				progressEvent(progress);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005210 File Offset: 0x00003410
		public bool IsOfType(string checksumTypeName)
		{
			bool flag = string.Equals(checksumTypeName, "sha-256", StringComparison.InvariantCultureIgnoreCase);
			bool flag2 = !flag;
			if (flag2)
			{
				flag = string.Equals(checksumTypeName, "sha256", StringComparison.InvariantCultureIgnoreCase);
			}
			return flag;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005248 File Offset: 0x00003448
		public byte[] CalculateChecksum(string filePath, CancellationToken cancellationToken)
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
				array = this.CalculateChecksum(fileStream, cancellationToken);
			}
			return array;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000052B0 File Offset: 0x000034B0
		public byte[] CalculateChecksum(FileStream fileStream, CancellationToken cancellationToken)
		{
			bool flag = fileStream.Length == 0L;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				using (SHA256Managed sha256Managed = new SHA256Managed())
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
							sha256Managed.TransformBlock(array2, 0, num2, array2, 0);
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
					sha256Managed.TransformFinalBlock(array2, 0, num2);
					array = sha256Managed.Hash;
				}
			}
			return array;
		}

		// Token: 0x04000046 RID: 70
		private const string MsrChecksumTypeName = "sha-256";

		// Token: 0x04000047 RID: 71
		private const string MsrChecksumTypeNameOther = "sha256";

		// Token: 0x04000048 RID: 72
		private bool disposed;
	}
}
