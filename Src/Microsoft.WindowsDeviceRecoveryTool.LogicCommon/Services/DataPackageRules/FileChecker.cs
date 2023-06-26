using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services.DataPackageRules
{
	// Token: 0x02000013 RID: 19
	[Export]
	public class FileChecker
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005390 File Offset: 0x00003590
		[ImportingConstructor]
		public FileChecker(Crc32Service crc32Service, Md5Sevice md5Sevice, Sha256Service sha256Service)
		{
			this.crc32Service = crc32Service;
			this.crc32Service.Crc32ProgressEvent += this.Progress;
			this.checksumServices = new List<IChecksumService> { md5Sevice, sha256Service };
			this.md5Sevice = md5Sevice;
			this.sha256Service = sha256Service;
			this.md5Sevice.ProgressEvent += this.Progress;
			this.sha256Service.ProgressEvent += this.Progress;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000541D File Offset: 0x0000361D
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005425 File Offset: 0x00003625
		public double CrcCurrentProgress { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000542E File Offset: 0x0000362E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005436 File Offset: 0x00003636
		public double ProgressModificator { get; set; }

		// Token: 0x060000EC RID: 236 RVA: 0x00005440 File Offset: 0x00003640
		public static void ValidateSpaceAvailability(string path, long sizeNeeded)
		{
			string pathRoot = Path.GetPathRoot(path);
			try
			{
				bool flag = !string.IsNullOrEmpty(pathRoot);
				if (!flag)
				{
					throw new CannotAccessDirectoryException(path);
				}
				long availableFreeSpace = new DriveInfo(pathRoot).AvailableFreeSpace;
				bool flag2 = availableFreeSpace < sizeNeeded;
				if (flag2)
				{
					throw new NotEnoughSpaceException
					{
						Available = availableFreeSpace,
						Needed = sizeNeeded,
						Disk = pathRoot
					};
				}
			}
			catch (Exception ex)
			{
				bool flag3 = ex is NotEnoughSpaceException;
				if (flag3)
				{
					throw;
				}
				throw new CannotAccessDirectoryException(path);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000054D4 File Offset: 0x000036D4
		public void CheckFilesCorrectness(string destinationPath, IEnumerable<File4> files, CancellationToken token)
		{
			IList<File4> list = (files as IList<File4>) ?? files.ToList<File4>();
			long num = list.Sum((File4 file) => this.GetFileLength(Path.Combine(destinationPath, file.FileName)));
			this.CrcCurrentProgress = 0.0;
			foreach (File4 file2 in list)
			{
				long fileLength = this.GetFileLength(Path.Combine(destinationPath, file2.FileName));
				this.ProgressModificator = (double)fileLength / (double)num;
				token.ThrowIfCancellationRequested();
				bool flag = !this.CheckSumEqual(Path.Combine(destinationPath, file2.FileName), file2.Checksum, token);
				if (flag)
				{
					throw new Crc32Exception(file2.FileName);
				}
				this.CrcCurrentProgress += this.ProgressModificator * 100.0;
			}
			this.Progress(100);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000055F8 File Offset: 0x000037F8
		public long GetFileLength(string file)
		{
			FileInfo fileInfo = new FileInfo(file);
			return fileInfo.Length;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005618 File Offset: 0x00003818
		public byte[] CheckFile(string checksumTypeName, string filePath, CancellationToken cancellationToken)
		{
			IChecksumService checksumService = this.checksumServices.FirstOrDefault((IChecksumService ch) => ch.IsOfType(checksumTypeName));
			bool flag = checksumService == null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("No checksum service found for checksum: {0}", checksumTypeName));
			}
			return checksumService.CalculateChecksum(filePath, cancellationToken);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005678 File Offset: 0x00003878
		public bool CheckSumEqual(string fileName, string crc, CancellationToken token)
		{
			uint num = this.crc32Service.CalculateCrc32(fileName, token);
			return num == Convert.ToUInt32(crc);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000056A4 File Offset: 0x000038A4
		public ReadOnlyCollection<string> FindLocalVplFilePaths(string productType, string productCode, string searchPath)
		{
			LocalDataPackageAccess localDataPackageAccess = new LocalDataPackageAccess();
			bool flag = string.IsNullOrEmpty(productCode);
			ReadOnlyCollection<string> readOnlyCollection;
			if (flag)
			{
				readOnlyCollection = localDataPackageAccess.GetVplPathList(productType, searchPath);
			}
			else
			{
				readOnlyCollection = localDataPackageAccess.GetVplPathList(productType, productCode, searchPath);
			}
			return readOnlyCollection;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000056DB File Offset: 0x000038DB
		public void SetProgressHandler(Action<double> action)
		{
			this.progressHandler = action;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000056E8 File Offset: 0x000038E8
		public void CheckFiles(List<FileCrcInfo> filesToCheck, CancellationToken cancellationToken)
		{
			Tracer<FileChecker>.LogEntry("CheckFiles");
			long num = filesToCheck.Sum((FileCrcInfo file) => this.GetFileLength(file.FilePath));
			this.CrcCurrentProgress = 0.0;
			foreach (FileCrcInfo fileCrcInfo in filesToCheck)
			{
				long fileLength = this.GetFileLength(fileCrcInfo.FilePath);
				this.ProgressModificator = (double)fileLength / (double)num;
				uint num2 = this.crc32Service.CalculateCrc32(fileCrcInfo.FilePath, cancellationToken);
				bool flag = num2 != uint.Parse(fileCrcInfo.Crc, NumberStyles.HexNumber);
				if (flag)
				{
					Tracer<FileChecker>.WriteInformation("Crc check failed: {0} crc: {1}", new object[]
					{
						fileCrcInfo.FileName,
						num2.ToString("X8")
					});
					throw new Crc32Exception(fileCrcInfo.FileName);
				}
				this.CrcCurrentProgress += this.ProgressModificator * 100.0;
			}
			this.Progress(100);
			Tracer<FileChecker>.LogExit("CheckFiles");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000581C File Offset: 0x00003A1C
		private void Progress(int progress)
		{
			double progressToShow = this.CrcCurrentProgress + (double)progress * this.ProgressModificator;
			bool flag = this.progressHandler != null;
			if (flag)
			{
				AppDispatcher.Execute(delegate
				{
					this.progressHandler((double)((int)progressToShow));
				}, false);
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly Crc32Service crc32Service;

		// Token: 0x0400004B RID: 75
		private readonly Md5Sevice md5Sevice;

		// Token: 0x0400004C RID: 76
		private readonly Sha256Service sha256Service;

		// Token: 0x0400004D RID: 77
		private List<IChecksumService> checksumServices;

		// Token: 0x0400004E RID: 78
		private Action<double> progressHandler;
	}
}
