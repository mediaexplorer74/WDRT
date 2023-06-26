using System;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x02000048 RID: 72
	public class Thor2Error
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000064D4 File Offset: 0x000046D4
		public static Thor2ErrorType GetErrorType(Thor2ExitCode exitCode)
		{
			bool flag = exitCode == Thor2ExitCode.Thor2AllOk;
			Thor2ErrorType thor2ErrorType;
			if (flag)
			{
				Tracer<Thor2Error>.WriteInformation("Flash process exited with no error");
				thor2ErrorType = Thor2ErrorType.NoError;
			}
			else
			{
				bool flag2 = exitCode >= Thor2ExitCode.Thor2ErrorConnectionNotFound && exitCode < (Thor2ExitCode)85000U;
				if (flag2)
				{
					Tracer<Thor2Error>.WriteError("THOR2 error occured", new object[0]);
					thor2ErrorType = Thor2ErrorType.Thor2Error;
				}
				else
				{
					bool flag3 = exitCode >= (Thor2ExitCode)131072U && exitCode < (Thor2ExitCode)196608U;
					if (flag3)
					{
						Tracer<Thor2Error>.WriteError("File error occured", new object[0]);
						thor2ErrorType = Thor2ErrorType.FileError;
					}
					else
					{
						bool flag4 = exitCode >= (Thor2ExitCode)196608U && exitCode < (Thor2ExitCode)262144U;
						if (flag4)
						{
							Tracer<Thor2Error>.WriteError("Device error occured", new object[0]);
							thor2ErrorType = Thor2ErrorType.DeviceError;
						}
						else
						{
							bool flag5 = exitCode >= (Thor2ExitCode)262144U && exitCode < (Thor2ExitCode)327680U;
							if (flag5)
							{
								Tracer<Thor2Error>.WriteError("Message error occured", new object[0]);
								thor2ErrorType = Thor2ErrorType.MessageError;
							}
							else
							{
								bool flag6 = exitCode >= (Thor2ExitCode)327680U && exitCode < Thor2ExitCode.DevReportedErrorDuringSffuProgramming;
								if (flag6)
								{
									Tracer<Thor2Error>.WriteError("Messaging error occured", new object[0]);
									thor2ErrorType = Thor2ErrorType.MessagingError;
								}
								else
								{
									bool flag7 = exitCode >= Thor2ExitCode.DevReportedErrorDuringSffuProgramming && exitCode < Thor2ExitCode.FfuParsingError;
									if (flag7)
									{
										Tracer<Thor2Error>.WriteError("Device reported ver 2 error during FFU programming", new object[0]);
										thor2ErrorType = Thor2ErrorType.FfuProgrammingVer2Error;
									}
									else
									{
										bool flag8 = exitCode >= Thor2ExitCode.FfuParsingError && exitCode < (Thor2ExitCode)2293760U;
										if (flag8)
										{
											Tracer<Thor2Error>.WriteError("FFU parsing error occured", new object[0]);
											thor2ErrorType = Thor2ErrorType.FfuParsingError;
										}
										else
										{
											bool flag9 = exitCode >= (Thor2ExitCode)4194304000U && exitCode < (Thor2ExitCode)4211081215U;
											if (flag9)
											{
												Tracer<Thor2Error>.WriteError("FlashApp error occured", new object[0]);
												thor2ErrorType = Thor2ErrorType.FlashAppError;
											}
											else
											{
												Tracer<Thor2Error>.WriteError("Unhandled error occured", new object[0]);
												thor2ErrorType = Thor2ErrorType.UnhandledError;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return thor2ErrorType;
		}
	}
}
