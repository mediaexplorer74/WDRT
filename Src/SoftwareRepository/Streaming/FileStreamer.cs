using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000014 RID: 20
	public class FileStreamer : Streamer
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036A0 File Offset: 0x000018A0
		private static HashSet<char> InvalidFileCharsSet
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = FileStreamer.invalidFileCharsSet) == null)
				{
					hashSet = (FileStreamer.invalidFileCharsSet = new HashSet<char>(Path.GetInvalidFileNameChars()));
				}
				return hashSet;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000036CC File Offset: 0x000018CC
		public static string DefaultResumeFolder
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SoftwareRepositoryResume");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000036EF File Offset: 0x000018EF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000036F7 File Offset: 0x000018F7
		public string ResumeFileName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003700 File Offset: 0x00001900
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003708 File Offset: 0x00001908
		public string FileName { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003711 File Offset: 0x00001911
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi")]
		public FileStreamer(string downloadPath, string packageId, bool multiPath = false)
			: this(downloadPath, packageId, FileStreamer.DefaultResumeFolder, multiPath)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003724 File Offset: 0x00001924
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi")]
		public FileStreamer(string downloadPath, string packageId, string resumeFolder, bool multiPath = false)
		{
			bool flag = string.IsNullOrEmpty(downloadPath);
			if (flag)
			{
				throw new ArgumentNullException("downloadPath");
			}
			bool flag2 = string.IsNullOrEmpty(packageId);
			if (flag2)
			{
				throw new ArgumentNullException("packageId");
			}
			bool flag3 = string.IsNullOrEmpty(resumeFolder);
			if (flag3)
			{
				throw new ArgumentNullException("resumeFolder");
			}
			this.FileName = downloadPath;
			this.DownloadPath = downloadPath;
			this.ResumeFileName = FileStreamer.GetResumePath(downloadPath, packageId, resumeFolder, multiPath);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000379C File Offset: 0x0000199C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi")]
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public static string GetResumePath(string downloadPath, string packageId, string resumeFolder = null, bool multiPath = false)
		{
			bool flag = string.IsNullOrEmpty(downloadPath);
			if (flag)
			{
				throw new ArgumentNullException("downloadPath");
			}
			bool flag2 = string.IsNullOrEmpty(packageId);
			if (flag2)
			{
				throw new ArgumentNullException("packageId");
			}
			resumeFolder = resumeFolder ?? FileStreamer.DefaultResumeFolder;
			string text;
			if (multiPath)
			{
				using (SHA256 sha = SHA256.Create())
				{
					byte[] array = sha.ComputeHash(Encoding.Unicode.GetBytes(downloadPath));
					text = BitConverter.ToString(array.Take(4).Concat(array.Skip(28)).ToArray<byte>()).Replace("-", string.Empty).ToLowerInvariant();
				}
			}
			else
			{
				text = Path.GetFileName(downloadPath);
			}
			return Path.Combine(resumeFolder, new string(packageId.Select((char c) => FileStreamer.InvalidFileCharsSet.Contains(c) ? '-' : c).ToArray<char>()) + "_" + text + ".resume");
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000038B0 File Offset: 0x00001AB0
		public override void SetMetadata(byte[] metadata)
		{
			bool flag = metadata != null;
			if (flag)
			{
				string directoryName = Path.GetDirectoryName(this.ResumeFileName);
				bool flag2 = !string.IsNullOrEmpty(directoryName);
				if (flag2)
				{
					Directory.CreateDirectory(directoryName);
				}
				using (FileStream fileStream = new FileStream(this.ResumeFileName, FileMode.Create))
				{
					fileStream.Write(metadata, 0, metadata.Length);
				}
			}
			else
			{
				this.ClearMetadata();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003930 File Offset: 0x00001B30
		public override byte[] GetMetadata()
		{
			bool flag = File.Exists(this.ResumeFileName);
			if (flag)
			{
				bool flag2 = File.Exists(this.FileName);
				if (flag2)
				{
					using (FileStream fileStream = new FileStream(this.ResumeFileName, FileMode.Open))
					{
						fileStream.Seek(0L, SeekOrigin.Begin);
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						return array;
					}
				}
				this.ClearMetadata();
			}
			return null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000039C0 File Offset: 0x00001BC0
		public override void ClearMetadata()
		{
			bool flag = File.Exists(this.ResumeFileName);
			if (flag)
			{
				File.Delete(this.ResumeFileName);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000039EC File Offset: 0x00001BEC
		protected override Stream GetStreamInternal()
		{
			string directoryName = Path.GetDirectoryName(this.FileName);
			bool flag = !string.IsNullOrEmpty(directoryName);
			if (flag)
			{
				Directory.CreateDirectory(directoryName);
			}
			return new FileStream(this.FileName, FileMode.OpenOrCreate);
		}

		// Token: 0x0400004A RID: 74
		private static HashSet<char> invalidFileCharsSet;

		// Token: 0x0400004B RID: 75
		public const string ResumeExtension = ".resume";

		// Token: 0x0400004C RID: 76
		public const string AppDataSubFolder = "SoftwareRepositoryResume";
	}
}
