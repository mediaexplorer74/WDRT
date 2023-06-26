using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using FfuFileReader;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsPhone.Imaging;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000009 RID: 9
	[Export]
	public class FfuFileInfoService
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003173 File Offset: 0x00001373
		[ImportingConstructor]
		public FfuFileInfoService()
		{
			this.ffuReaderManaged = new FfuReaderManaged();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003188 File Offset: 0x00001388
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003190 File Offset: 0x00001390
		public string RootKeyHash { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003199 File Offset: 0x00001399
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000031A1 File Offset: 0x000013A1
		public string PlatformId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000031AA File Offset: 0x000013AA
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000031B2 File Offset: 0x000013B2
		public string FullName { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031BB File Offset: 0x000013BB
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000031C3 File Offset: 0x000013C3
		public string Name { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000031CC File Offset: 0x000013CC
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000031D4 File Offset: 0x000013D4
		public long Length { get; private set; }

		// Token: 0x0600006E RID: 110 RVA: 0x000031E0 File Offset: 0x000013E0
		public PlatformId ReadFfuFilePlatformId(string ffuFilePath)
		{
			PlatformId platformId = new PlatformId();
			platformId.SetPlatformId(this.ReadFfuPlatformId(ffuFilePath));
			return platformId;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003208 File Offset: 0x00001408
		public string ReadFfuPlatformId(string ffuFileName)
		{
			FileInfo fileInfo = new FileInfo(ffuFileName);
			int num = this.ffuReaderManaged.ReadPlatformId(fileInfo.FullName);
			bool flag = num != 0;
			if (flag)
			{
				throw new FfuFileInfoReadException(num, ffuFileName);
			}
			this.RootKeyHash = this.ffuReaderManaged.RootKeyHash;
			this.PlatformId = this.ffuReaderManaged.PlatformId;
			this.FullName = fileInfo.FullName;
			this.Name = fileInfo.Name;
			this.Length = fileInfo.Length;
			return this.ffuReaderManaged.PlatformId;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000329C File Offset: 0x0000149C
		public void ReadFfuFile(string ffuFileName)
		{
			FileInfo fileInfo = new FileInfo(ffuFileName);
			int num = this.ffuReaderManaged.Read(fileInfo.FullName);
			bool flag = num != 0;
			if (flag)
			{
				throw new FfuFileInfoReadException(num, ffuFileName);
			}
			this.RootKeyHash = this.ffuReaderManaged.RootKeyHash;
			this.PlatformId = this.ffuReaderManaged.PlatformId;
			this.FullName = fileInfo.FullName;
			this.Name = fileInfo.Name;
			this.Length = fileInfo.Length;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003320 File Offset: 0x00001520
		public bool TryReadFfuSoftwareVersion(string ffuFilePath, out string version)
		{
			version = null;
			bool flag;
			try
			{
				FullFlashUpdateImage orReadImageFromFile = FfuFileInfoService.GetOrReadImageFromFile(ffuFilePath);
				version = orReadImageFromFile.OSVersion;
				flag = true;
			}
			catch (Exception ex)
			{
				Tracer<FfuFileInfoService>.WriteWarning(ex, "Could not read ffu image: {0}", new object[] { ffuFilePath });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003374 File Offset: 0x00001574
		public bool TryReadAllFfuPlatformIds(string ffuFilePath, out IEnumerable<PlatformId> platformIds)
		{
			platformIds = null;
			bool flag;
			try
			{
				FullFlashUpdateImage orReadImageFromFile = FfuFileInfoService.GetOrReadImageFromFile(ffuFilePath);
				List<PlatformId> list = new List<PlatformId>();
				foreach (string text in orReadImageFromFile.DevicePlatformIDs)
				{
					PlatformId platformId = new PlatformId();
					platformId.SetPlatformId(text);
					list.Add(platformId);
				}
				platformIds = list;
				flag = true;
			}
			catch (Exception ex)
			{
				Tracer<FfuFileInfoService>.WriteWarning(ex, "Could not read ffu image: {0}", new object[] { ffuFilePath });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003404 File Offset: 0x00001604
		private static FullFlashUpdateImage GetOrReadImageFromFile(string ffuFilePath)
		{
			bool flag = !FfuFileInfoService.imagesDataCache.ContainsKey(ffuFilePath);
			if (flag)
			{
				FullFlashUpdateImage fullFlashUpdateImage = new FullFlashUpdateImage();
				fullFlashUpdateImage.Initialize(ffuFilePath);
				FfuFileInfoService.imagesDataCache.Add(ffuFilePath, fullFlashUpdateImage);
			}
			return FfuFileInfoService.imagesDataCache[ffuFilePath];
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003450 File Offset: 0x00001650
		public void ClearDataForFfuFile(string ffuFilePath)
		{
			bool flag = FfuFileInfoService.imagesDataCache.ContainsKey(ffuFilePath);
			if (flag)
			{
				FfuFileInfoService.imagesDataCache.Remove(ffuFilePath);
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly FfuReaderManaged ffuReaderManaged;

		// Token: 0x04000024 RID: 36
		private static Dictionary<string, FullFlashUpdateImage> imagesDataCache = new Dictionary<string, FullFlashUpdateImage>();
	}
}
