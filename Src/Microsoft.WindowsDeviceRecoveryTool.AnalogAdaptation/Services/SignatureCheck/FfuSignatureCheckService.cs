using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsPhone.Imaging;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services.SignatureCheck
{
	// Token: 0x0200000A RID: 10
	internal sealed class FfuSignatureCheckService
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000451C File Offset: 0x0000271C
		private FfuSignatureCheckService(FfuFileInfoService ffuFileInfo)
		{
			bool flag = ffuFileInfo == null;
			if (flag)
			{
				throw new ArgumentNullException("ffuFileInfo");
			}
			this.ffuFileInfo = ffuFileInfo;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000454C File Offset: 0x0000274C
		public static void RunSignatureCheck(FfuFileInfoService ffuFileInfoService, CancellationToken cancellationToken)
		{
			Tracer<FfuSignatureCheckService>.LogEntry("RunSignatureCheck");
			FfuSignatureCheckService ffuSignatureCheckService = new FfuSignatureCheckService(ffuFileInfoService);
			try
			{
				ffuSignatureCheckService.ValidateComponents(cancellationToken);
				ffuSignatureCheckService.RunGetCatalog(cancellationToken);
				ffuSignatureCheckService.ValidateExtractedCatalog(cancellationToken);
				ffuSignatureCheckService.ValidateExtractedCatalogCorrespondsToTheImage(cancellationToken);
			}
			finally
			{
				ffuSignatureCheckService.Cleanup();
				Tracer<FfuSignatureCheckService>.LogExit("RunSignatureCheck");
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000045B8 File Offset: 0x000027B8
		private void Cleanup()
		{
			bool flag = File.Exists(this.extractedCatalogFilePath);
			if (flag)
			{
				File.Delete(this.extractedCatalogFilePath);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000045E4 File Offset: 0x000027E4
		private void ValidateExtractedCatalogCorrespondsToTheImage(CancellationToken cancellationToken)
		{
			try
			{
				FullFlashUpdateImage fullFlashUpdateImage = new FullFlashUpdateImage();
				fullFlashUpdateImage.Initialize(this.ffuFileInfo.FullName);
				ImageSigner imageSigner = new ImageSigner();
				imageSigner.Initialize(fullFlashUpdateImage, this.extractedCatalogFilePath, null);
				imageSigner.VerifyCatalog();
			}
			catch (Exception ex)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Verification failed: {0}", new object[] { ex });
				throw new FfuSignatureCheckException("FFU Signature check fail - Verification of extracted catalog against image failed", ex);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004660 File Offset: 0x00002860
		private void SignFfuFileWithCatalog(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			string text = string.Format("SIGN \"{0}\" \"{1}\"", this.ffuFileInfo.FullName, this.extractedCatalogFilePath);
			ProcessHelper imageSignerProcess = new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = new ProcessStartInfo(this.imageSignerPath, text)
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true,
					WorkingDirectory = this.workingDirectory
				}
			};
			try
			{
				imageSignerProcess.ErrorDataReceived += this.ImageSignerProcessOnErrorDataReceived;
				imageSignerProcess.OutputDataReceived += this.ImageSignerProcessOnOutputDataReceived;
				Task task = new Task(delegate
				{
					this.CancellationMonitor(cancellationToken, imageSignerProcess);
				});
				task.Start();
				imageSignerProcess.Start();
				Tracer<FfuSignatureCheckService>.WriteVerbose("Running process ID={0}: {1} {2}", new object[] { imageSignerProcess.Id, "imagesigner.exe", text });
				imageSignerProcess.BeginOutputReadLine();
				imageSignerProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				throw new FfuSignatureCheckException("FFU Signature fail - Signing ffu with catalog failed", ex);
			}
			finally
			{
				imageSignerProcess.ErrorDataReceived -= this.ImageSignerProcessOnErrorDataReceived;
				imageSignerProcess.OutputDataReceived -= this.ImageSignerProcessOnOutputDataReceived;
			}
			bool flag = imageSignerProcess.ExitCode != 0;
			if (flag)
			{
				throw new FfuSignatureCheckException(string.Format("FFU Signature check fail - Signing ffu with catalog failed with exit code: {0}", imageSignerProcess.ExitCode), imageSignerProcess.ExitCode);
			}
			Tracer<FfuSignatureCheckService>.WriteInformation("Signature check - signing ffu file success");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004848 File Offset: 0x00002A48
		private void ValidateExtractedCatalog(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = !File.Exists(this.extractedCatalogFilePath);
			if (flag)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - FFU extracted cat not present: {0}", new object[] { this.extractedCatalogFilePath });
				throw new FileNotFoundException("FFU Signature check fail - FFU image catalog extracted but not present on given location", this.extractedCatalogFilePath);
			}
			string text = string.Format("VERIFY /r \"{0}\" /ca {1} /u {2} \"{3}\"", new object[] { "Microsoft Root Certificate Authority 2010", "C01386A907496404F276C3C1853ABF4A5274AF88", "1.3.6.1.4.1.311.10.3.6", this.extractedCatalogFilePath });
			ProcessHelper signToolProcess = new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = new ProcessStartInfo(this.signToolPath, text)
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true,
					WorkingDirectory = this.workingDirectory
				}
			};
			try
			{
				signToolProcess.OutputDataReceived += this.SignToolProcessOnOutputDataReceived;
				signToolProcess.ErrorDataReceived += this.SignToolProcessOnErrorDataReceived;
				Task task = new Task(delegate
				{
					this.CancellationMonitor(cancellationToken, signToolProcess);
				});
				task.Start();
				signToolProcess.Start();
				Tracer<FfuSignatureCheckService>.WriteVerbose("Running process ID={0}: {1} {2}", new object[] { signToolProcess.Id, "signtool.exe", text });
				signToolProcess.BeginOutputReadLine();
				signToolProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Verification failed: {0}", new object[] { ex });
				throw new FfuSignatureCheckException("FFU Signature check fail - Verification of extracted catalog failed", ex);
			}
			finally
			{
				signToolProcess.OutputDataReceived -= this.SignToolProcessOnOutputDataReceived;
				signToolProcess.ErrorDataReceived -= this.SignToolProcessOnErrorDataReceived;
			}
			bool flag2 = signToolProcess.ExitCode != 0;
			if (flag2)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Verification failed", new object[0]);
				throw new FfuSignatureCheckException(string.Format("FFU Signature check fail - Verification of extracted catalog failed exit code: {0}", signToolProcess.ExitCode), signToolProcess.ExitCode);
			}
			Tracer<FfuSignatureCheckService>.WriteInformation("Signature check - extracted cat file verification success");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004AB0 File Offset: 0x00002CB0
		private void RunGetCatalog(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			string text = string.Format("{0}.cat", Path.GetFileNameWithoutExtension(this.ffuFileInfo.Name));
			this.extractedCatalogFilePath = Path.Combine(Path.GetTempPath(), text);
			string text2 = string.Format("GETCATALOG \"{0}\" \"{1}\"", this.ffuFileInfo.FullName, this.extractedCatalogFilePath);
			ProcessHelper imageSignerProcess = new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = new ProcessStartInfo(this.imageSignerPath, text2)
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true,
					WorkingDirectory = this.workingDirectory
				}
			};
			try
			{
				imageSignerProcess.ErrorDataReceived += this.ImageSignerProcessOnErrorDataReceived;
				imageSignerProcess.OutputDataReceived += this.ImageSignerProcessOnOutputDataReceived;
				Task task = new Task(delegate
				{
					this.CancellationMonitor(cancellationToken, imageSignerProcess);
				});
				task.Start();
				imageSignerProcess.Start();
				Tracer<FfuSignatureCheckService>.WriteVerbose("Running process ID={0}: {1} {2}", new object[] { imageSignerProcess.Id, "imagesigner.exe", text2 });
				imageSignerProcess.BeginOutputReadLine();
				imageSignerProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Extracting catalog failed: {0}", new object[] { ex });
				throw new FfuSignatureCheckException("FFU Signature check fail - Extracting ffu image catalog failed", ex);
			}
			finally
			{
				imageSignerProcess.ErrorDataReceived -= this.ImageSignerProcessOnErrorDataReceived;
				imageSignerProcess.OutputDataReceived -= this.ImageSignerProcessOnOutputDataReceived;
			}
			bool flag = imageSignerProcess.ExitCode != 0;
			if (flag)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Extracting catalog failed with exit code: {0}", new object[] { imageSignerProcess.ExitCode });
				throw new FfuSignatureCheckException(string.Format("FFU Signature check fail - Extracting ffu image catalog failed with exit code: {0}", imageSignerProcess.ExitCode), imageSignerProcess.ExitCode);
			}
			Tracer<FfuSignatureCheckService>.WriteInformation("Signature check - Extracting catalog success, file path = {0}", new object[] { this.extractedCatalogFilePath });
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004D10 File Offset: 0x00002F10
		private void ValidateComponents(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = !File.Exists(this.ffuFileInfo.FullName);
			if (flag)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Ffu file not found: {0}", new object[] { this.ffuFileInfo.FullName });
				throw new FileNotFoundException("FFU file was not found on given location", this.ffuFileInfo.FullName);
			}
			string workingDirectoryPath = this.GetWorkingDirectoryPath();
			this.signToolPath = Path.Combine(workingDirectoryPath, "signtool.exe");
			this.imageSignerPath = Path.Combine(workingDirectoryPath, "imagesigner.exe");
			bool flag2 = !File.Exists(this.signToolPath);
			if (flag2)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Signing tool not found: {0}", new object[] { this.signToolPath });
				throw new FileNotFoundException("Signing tool was not found on given location", this.signToolPath);
			}
			bool flag3 = !File.Exists(this.imageSignerPath);
			if (flag3)
			{
				Tracer<FfuSignatureCheckService>.WriteError("Signature check - Image signer not found: {0}", new object[] { this.imageSignerPath });
				throw new FileNotFoundException("Image signer was not found on given location", this.imageSignerPath);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004E18 File Offset: 0x00003018
		private string GetWorkingDirectoryPath()
		{
			bool flag = !string.IsNullOrEmpty(this.workingDirectory);
			string text;
			if (flag)
			{
				text = this.workingDirectory;
			}
			else
			{
				string directoryName = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
				bool flag2 = string.IsNullOrWhiteSpace(directoryName);
				if (flag2)
				{
					Tracer<FfuSignatureCheckService>.WriteError("Signature check - Could not read working directory", new object[0]);
					throw new Exception("Could not find working directory path");
				}
				Tracer<FfuSignatureCheckService>.WriteVerbose("Working directory set to: {0}", new object[] { directoryName });
				text = (this.workingDirectory = directoryName);
			}
			return text;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004EB0 File Offset: 0x000030B0
		private void CancellationMonitor(CancellationToken token, ProcessHelper helper)
		{
			while (!helper.HasExited)
			{
				Thread.Sleep(500);
				bool isCancellationRequested = token.IsCancellationRequested;
				if (isCancellationRequested)
				{
					bool flag = !helper.HasExited;
					if (flag)
					{
						Tracer<FfuSignatureCheckService>.WriteInformation("Cancellation requested. Process still running. Need to manually kill process.");
						helper.Kill();
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004F08 File Offset: 0x00003108
		private void SignToolProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			Tracer<FfuSignatureCheckService>.WriteError("{0}: {1}", new object[] { "signtool.exe", dataReceivedEventArgs.Data });
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004F2D File Offset: 0x0000312D
		private void SignToolProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			Tracer<FfuSignatureCheckService>.WriteVerbose("{0}: {1}", new object[] { "signtool.exe", dataReceivedEventArgs.Data });
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004F52 File Offset: 0x00003152
		private void ImageSignerProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			Tracer<FfuSignatureCheckService>.WriteVerbose("{0}: {1}", new object[] { "imagesigner.exe", dataReceivedEventArgs.Data });
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004F52 File Offset: 0x00003152
		private void ImageSignerProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			Tracer<FfuSignatureCheckService>.WriteVerbose("{0}: {1}", new object[] { "imagesigner.exe", dataReceivedEventArgs.Data });
		}

		// Token: 0x04000030 RID: 48
		private const string SignToolFileName = "signtool.exe";

		// Token: 0x04000031 RID: 49
		private const string ImageSignerFileName = "imagesigner.exe";

		// Token: 0x04000032 RID: 50
		private const string CertificateAuthorityName = "Microsoft Root Certificate Authority 2010";

		// Token: 0x04000033 RID: 51
		private const string CertificateKey = "C01386A907496404F276C3C1853ABF4A5274AF88";

		// Token: 0x04000034 RID: 52
		private const string CertificateVersion = "1.3.6.1.4.1.311.10.3.6";

		// Token: 0x04000035 RID: 53
		private readonly FfuFileInfoService ffuFileInfo;

		// Token: 0x04000036 RID: 54
		private string signToolPath;

		// Token: 0x04000037 RID: 55
		private string imageSignerPath;

		// Token: 0x04000038 RID: 56
		private string extractedCatalogFilePath;

		// Token: 0x04000039 RID: 57
		private string workingDirectory;
	}
}
