using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020000EE RID: 238
	internal class FtpMethodInfo
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0002C408 File Offset: 0x0002A608
		internal FtpMethodInfo(string method, FtpOperation operation, FtpMethodFlags flags, string httpCommand)
		{
			this.Method = method;
			this.Operation = operation;
			this.Flags = flags;
			this.HttpCommand = httpCommand;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002C42D File Offset: 0x0002A62D
		internal bool HasFlag(FtpMethodFlags flags)
		{
			return (this.Flags & flags) > FtpMethodFlags.None;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0002C43A File Offset: 0x0002A63A
		internal bool IsCommandOnly
		{
			get
			{
				return (this.Flags & (FtpMethodFlags.IsDownload | FtpMethodFlags.IsUpload)) == FtpMethodFlags.None;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002C447 File Offset: 0x0002A647
		internal bool IsUpload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsUpload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0002C454 File Offset: 0x0002A654
		internal bool IsDownload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsDownload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002C461 File Offset: 0x0002A661
		internal bool HasHttpCommand
		{
			get
			{
				return (this.Flags & FtpMethodFlags.HasHttpCommand) > FtpMethodFlags.None;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0002C472 File Offset: 0x0002A672
		internal bool ShouldParseForResponseUri
		{
			get
			{
				return (this.Flags & FtpMethodFlags.ShouldParseForResponseUri) > FtpMethodFlags.None;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002C480 File Offset: 0x0002A680
		internal static FtpMethodInfo GetMethodInfo(string method)
		{
			method = method.ToUpper(CultureInfo.InvariantCulture);
			foreach (FtpMethodInfo ftpMethodInfo in FtpMethodInfo.KnownMethodInfo)
			{
				if (method == ftpMethodInfo.Method)
				{
					return ftpMethodInfo;
				}
			}
			throw new ArgumentException(SR.GetString("net_ftp_unsupported_method"), "method");
		}

		// Token: 0x04000D95 RID: 3477
		internal string Method;

		// Token: 0x04000D96 RID: 3478
		internal FtpOperation Operation;

		// Token: 0x04000D97 RID: 3479
		internal FtpMethodFlags Flags;

		// Token: 0x04000D98 RID: 3480
		internal string HttpCommand;

		// Token: 0x04000D99 RID: 3481
		private static readonly FtpMethodInfo[] KnownMethodInfo = new FtpMethodInfo[]
		{
			new FtpMethodInfo("RETR", FtpOperation.DownloadFile, FtpMethodFlags.IsDownload | FtpMethodFlags.TakesParameter | FtpMethodFlags.HasHttpCommand, "GET"),
			new FtpMethodInfo("NLST", FtpOperation.ListDirectory, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("LIST", FtpOperation.ListDirectoryDetails, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("STOR", FtpOperation.UploadFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("STOU", FtpOperation.UploadFileUnique, FtpMethodFlags.IsUpload | FtpMethodFlags.DoesNotTakeParameter | FtpMethodFlags.ShouldParseForResponseUri | FtpMethodFlags.MustChangeWorkingDirectoryToPath, null),
			new FtpMethodInfo("APPE", FtpOperation.AppendFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("DELE", FtpOperation.DeleteFile, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MDTM", FtpOperation.GetDateTimestamp, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("SIZE", FtpOperation.GetFileSize, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("RENAME", FtpOperation.Rename, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MKD", FtpOperation.MakeDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("RMD", FtpOperation.RemoveDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("PWD", FtpOperation.PrintWorkingDirectory, FtpMethodFlags.DoesNotTakeParameter, null)
		};
	}
}
