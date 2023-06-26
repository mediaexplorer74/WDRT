using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Net
{
	// Token: 0x0200020E RID: 526
	internal static class SSPIWrapper
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x00066E9C File Offset: 0x0006509C
		internal static SecurityPackageInfoClass[] EnumerateSecurityPackages(SSPIInterface SecModule)
		{
			if (SecModule.SecurityPackages == null)
			{
				lock (SecModule)
				{
					if (SecModule.SecurityPackages == null)
					{
						int num = 0;
						SafeFreeContextBuffer safeFreeContextBuffer = null;
						try
						{
							int num2 = SecModule.EnumerateSecurityPackages(out num, out safeFreeContextBuffer);
							if (num2 != 0)
							{
								throw new Win32Exception(num2);
							}
							SecurityPackageInfoClass[] array = new SecurityPackageInfoClass[num];
							if (Logging.On)
							{
								Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_enumerating_security_packages"));
							}
							for (int i = 0; i < num; i++)
							{
								array[i] = new SecurityPackageInfoClass(safeFreeContextBuffer, i);
								if (Logging.On)
								{
									Logging.PrintInfo(Logging.Web, "    " + array[i].Name);
								}
							}
							SecModule.SecurityPackages = array;
						}
						finally
						{
							if (safeFreeContextBuffer != null)
							{
								safeFreeContextBuffer.Close();
							}
						}
					}
				}
			}
			return SecModule.SecurityPackages;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00066F90 File Offset: 0x00065190
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName)
		{
			return SSPIWrapper.GetVerifyPackageInfo(secModule, packageName, false);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00066F9C File Offset: 0x0006519C
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName, bool throwIfMissing)
		{
			SecurityPackageInfoClass[] array = SSPIWrapper.EnumerateSecurityPackages(secModule);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i].Name, packageName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return array[i];
					}
				}
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_package_not_found", new object[] { packageName }));
			}
			if (throwIfMissing)
			{
				throw new NotSupportedException(SR.GetString("net_securitypackagesupport"));
			}
			return null;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0006700F File Offset: 0x0006520F
		private static ConcurrentDictionary<string, SafeFreeCredentials> InitDefaultCredentialsHandleCache()
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Format("{0}: {1} = {2}", "InitDefaultCredentialsHandleCache", "defaultCredentialsHandleCacheSize", SSPIWrapper.s_DefaultCredentialsHandleCacheSize));
			}
			return new ConcurrentDictionary<string, SafeFreeCredentials>(Environment.ProcessorCount, SSPIWrapper.s_DefaultCredentialsHandleCacheSize);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00067050 File Offset: 0x00065250
		public static SafeFreeCredentials AcquireDefaultCredential(SSPIInterface SecModule, string package, CredentialUse intent)
		{
			SafeFreeCredentials safeFreeCredentials = null;
			string text = null;
			bool flag;
			if (SSPIWrapper.s_DefaultCredentialsHandleCacheEnabled)
			{
				text = string.Format("{0}_{1}_{2}", package, intent.ToString(), WindowsIdentity.GetCurrent().Name);
				flag = SSPIWrapper.s_DefaultCredentialsHandleCache.Value.TryGetValue(text, out safeFreeCredentials);
			}
			else
			{
				flag = false;
			}
			if (Logging.On)
			{
				if (text == null)
				{
					text = string.Format("{0}_{1}_{2}", package, intent.ToString(), WindowsIdentity.GetCurrent().Name);
				}
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireDefaultCredential(package = ",
					package,
					", intent = ",
					intent.ToString(),
					", identity = ",
					text,
					", cached = ",
					flag.ToString(),
					")"
				}));
			}
			if (!flag)
			{
				int num = SecModule.AcquireDefaultCredential(package, intent, out safeFreeCredentials);
				if (num != 0)
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
						{
							"AcquireDefaultCredential()",
							string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num })
						}));
					}
					throw new Win32Exception(num);
				}
				if (SSPIWrapper.s_DefaultCredentialsHandleCacheEnabled && SSPIWrapper.s_DefaultCredentialsHandleCache.Value.Count < SSPIWrapper.s_DefaultCredentialsHandleCacheSize)
				{
					try
					{
						SSPIWrapper.s_DefaultCredentialsHandleCache.Value.TryAdd(text, safeFreeCredentials);
					}
					catch (OverflowException)
					{
					}
				}
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000671DC File Offset: 0x000653DC
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, ref AuthIdentity authdata)
		{
			if (Logging.On)
			{
				TraceSource web = Logging.Web;
				string[] array = new string[7];
				array[0] = "AcquireCredentialsHandle(package  = ";
				array[1] = package;
				array[2] = ", intent   = ";
				array[3] = intent.ToString();
				array[4] = ", authdata = ";
				int num = 5;
				AuthIdentity authIdentity = authdata;
				array[num] = authIdentity.ToString();
				array[6] = ")";
				Logging.PrintInfo(web, string.Concat(array));
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num2 = SecModule.AcquireCredentialsHandle(package, intent, ref authdata, out safeFreeCredentials);
			if (num2 != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num2 })
					}));
				}
				throw new Win32Exception(num2);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000672B4 File Offset: 0x000654B4
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, ref SafeSspiAuthDataHandle authdata)
		{
			if (Logging.On)
			{
				TraceSource web = Logging.Web;
				string[] array = new string[7];
				array[0] = "AcquireCredentialsHandle(package  = ";
				array[1] = package;
				array[2] = ", intent   = ";
				array[3] = intent.ToString();
				array[4] = ", authdata = ";
				int num = 5;
				SafeSspiAuthDataHandle safeSspiAuthDataHandle = authdata;
				array[num] = ((safeSspiAuthDataHandle != null) ? safeSspiAuthDataHandle.ToString() : null);
				array[6] = ")";
				Logging.PrintInfo(web, string.Concat(array));
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num2 = SecModule.AcquireCredentialsHandle(package, intent, ref authdata, out safeFreeCredentials);
			if (num2 != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num2 })
					}));
				}
				throw new Win32Exception(num2);
			}
			return safeFreeCredentials;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00067388 File Offset: 0x00065588
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, SecureCredential scc)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireCredentialsHandle(package = ",
					package,
					", intent  = ",
					intent.ToString(),
					", scc     = ",
					scc.ToString(),
					")"
				}));
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = SecModule.AcquireCredentialsHandle(package, intent, ref scc, out safeFreeCredentials);
			if (num != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num })
					}));
				}
				throw new Win32Exception(num);
			}
			return safeFreeCredentials;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0006745C File Offset: 0x0006565C
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, SecureCredential2 scc)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireCredentialsHandle(package = ",
					package,
					", intent  = ",
					intent.ToString(),
					", scc     = ",
					scc.ToString(),
					")"
				}));
			}
			SafeFreeCredentials safeFreeCredentials = null;
			int num = SecModule.AcquireCredentialsHandle(package, intent, ref scc, out safeFreeCredentials);
			if (num != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num })
					}));
				}
				throw new Win32Exception(num);
			}
			return safeFreeCredentials;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00067530 File Offset: 0x00065730
		internal static int InitializeSecurityContext(SSPIInterface SecModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"InitializeSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", targetName = ",
					targetName,
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, datarep, inputBuffer, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffer", new object[]
				{
					"InitializeSecurityContext",
					(inputBuffer == null) ? 0 : inputBuffer.size,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00067618 File Offset: 0x00065818
		internal static int InitializeSecurityContext(SSPIInterface SecModule, SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"InitializeSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", targetName = ",
					targetName,
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, inputBuffers, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffers", new object[]
				{
					"InitializeSecurityContext",
					(inputBuffers == null) ? 0 : inputBuffers.Length,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000676FC File Offset: 0x000658FC
		internal static int AcceptSecurityContext(SSPIInterface SecModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, ContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcceptSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.AcceptSecurityContext(ref credential, ref context, inputBuffer, inFlags, datarep, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffer", new object[]
				{
					"AcceptSecurityContext",
					(inputBuffer == null) ? 0 : inputBuffer.size,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000677D4 File Offset: 0x000659D4
		internal static int AcceptSecurityContext(SSPIInterface SecModule, SafeFreeCredentials credential, ref SafeDeleteContext context, ContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcceptSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.AcceptSecurityContext(credential, ref context, inputBuffers, inFlags, datarep, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffers", new object[]
				{
					"AcceptSecurityContext",
					(inputBuffers == null) ? 0 : inputBuffers.Length,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000678A8 File Offset: 0x00065AA8
		internal static int CompleteAuthToken(SSPIInterface SecModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = SecModule.CompleteAuthToken(ref context, inputBuffers);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[]
				{
					"CompleteAuthToken()",
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000678F4 File Offset: 0x00065AF4
		internal static int ApplyControlToken(SSPIInterface SecModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = SecModule.ApplyControlToken(ref context, inputBuffers);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[]
				{
					"ApplyControlToken()",
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0006793D File Offset: 0x00065B3D
		public static int QuerySecurityContextToken(SSPIInterface SecModule, SafeDeleteContext context, out SafeCloseHandle token)
		{
			return SecModule.QuerySecurityContextToken(context, out token);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00067947 File Offset: 0x00065B47
		public static int EncryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Encrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00067953 File Offset: 0x00065B53
		public static int DecryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Decrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00067960 File Offset: 0x00065B60
		public static int ApplyAlertToken(SSPIInterface secModule, ref SafeFreeCredentials credentialsHandle, SafeDeleteContext securityContext, TlsAlertType alertType, TlsAlertMessage alertMessage)
		{
			Interop.SChannel.SCHANNEL_ALERT_TOKEN schannel_ALERT_TOKEN;
			schannel_ALERT_TOKEN.dwTokenType = 2U;
			schannel_ALERT_TOKEN.dwAlertType = (uint)alertType;
			schannel_ALERT_TOKEN.dwAlertNumber = (uint)alertMessage;
			SecurityBuffer[] array = new SecurityBuffer[1];
			int num = Marshal.SizeOf(typeof(Interop.SChannel.SCHANNEL_ALERT_TOKEN));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			int num2;
			try
			{
				byte[] array2 = new byte[num];
				Marshal.StructureToPtr(schannel_ALERT_TOKEN, intPtr, false);
				Marshal.Copy(intPtr, array2, 0, num);
				array[0] = new SecurityBuffer(array2, BufferType.Token);
				num2 = SSPIWrapper.ApplyControlToken(secModule, ref securityContext, array);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return num2;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000679F4 File Offset: 0x00065BF4
		public static int ApplyShutdownToken(SSPIInterface secModule, ref SafeFreeCredentials credentialsHandle, SafeDeleteContext securityContext)
		{
			int num = 1;
			SecurityBuffer[] array = new SecurityBuffer[1];
			byte[] bytes = BitConverter.GetBytes(num);
			array[0] = new SecurityBuffer(bytes, BufferType.Token);
			return SSPIWrapper.ApplyControlToken(secModule, ref securityContext, array);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00067A24 File Offset: 0x00065C24
		internal static int MakeSignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.MakeSignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00067A30 File Offset: 0x00065C30
		public static int VerifySignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.VerifySignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00067A3C File Offset: 0x00065C3C
		private unsafe static int EncryptDecryptHelper(SSPIWrapper.OP op, SSPIInterface SecModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(input.Length);
			SecurityBufferStruct[] array = new SecurityBufferStruct[input.Length];
			SecurityBufferStruct[] array2;
			SecurityBufferStruct* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
			GCHandle[] array3 = new GCHandle[input.Length];
			byte[][] array4 = new byte[input.Length][];
			int num2;
			try
			{
				for (int i = 0; i < input.Length; i++)
				{
					SecurityBuffer securityBuffer = input[i];
					array[i].count = securityBuffer.size;
					array[i].type = securityBuffer.type;
					if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array[i].token = IntPtr.Zero;
					}
					else
					{
						array3[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
						array4[i] = securityBuffer.token;
					}
				}
				int num;
				switch (op)
				{
				case SSPIWrapper.OP.Encrypt:
					num = SecModule.EncryptMessage(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.Decrypt:
					num = SecModule.DecryptMessage(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.MakeSignature:
					num = SecModule.MakeSignature(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.VerifySignature:
					num = SecModule.VerifySignature(context, securityBufferDescriptor, sequenceNumber);
					break;
				default:
					throw ExceptionHelper.MethodNotImplementedException;
				}
				for (int j = 0; j < input.Length; j++)
				{
					SecurityBuffer securityBuffer2 = input[j];
					securityBuffer2.size = array[j].count;
					securityBuffer2.type = array[j].type;
					checked
					{
						if (securityBuffer2.size == 0)
						{
							securityBuffer2.offset = 0;
							securityBuffer2.token = null;
						}
						else
						{
							int k;
							for (k = 0; k < input.Length; k++)
							{
								if (array4[k] != null)
								{
									byte* ptr2 = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array4[k], 0);
									if ((void*)array[j].token >= (void*)ptr2 && (byte*)(void*)array[j].token + securityBuffer2.size == ptr2 + array4[k].Length)
									{
										securityBuffer2.offset = (int)(unchecked((long)((byte*)(void*)array[j].token - (byte*)ptr2)));
										securityBuffer2.token = array4[k];
										break;
									}
								}
							}
							if (k >= input.Length)
							{
								securityBuffer2.size = 0;
								securityBuffer2.offset = 0;
								securityBuffer2.token = null;
							}
						}
					}
				}
				if (num != 0 && Logging.On)
				{
					if (num == 590625)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[] { op, "SEC_I_RENEGOTIATE" }));
					}
					else
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
						{
							op,
							string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[] { num })
						}));
					}
				}
				num2 = num;
			}
			finally
			{
				for (int l = 0; l < array3.Length; l++)
				{
					if (array3[l].IsAllocated)
					{
						array3[l].Free();
					}
				}
			}
			return num2;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00067D94 File Offset: 0x00065F94
		public static SafeFreeContextBufferChannelBinding QueryContextChannelBinding(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute)
		{
			SafeFreeContextBufferChannelBinding safeFreeContextBufferChannelBinding;
			int num = SecModule.QueryContextChannelBinding(securityContext, contextAttribute, out safeFreeContextBufferChannelBinding);
			if (num != 0)
			{
				return null;
			}
			return safeFreeContextBufferChannelBinding;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00067DB4 File Offset: 0x00065FB4
		public static object QueryContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute)
		{
			int num;
			return SSPIWrapper.QueryContextAttributes(SecModule, securityContext, contextAttribute, out num);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00067DCC File Offset: 0x00065FCC
		public unsafe static object QueryContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute, out int errorCode)
		{
			int num = IntPtr.Size;
			Type type = null;
			if (contextAttribute <= ContextAttribute.ClientSpecifiedSpn)
			{
				if (contextAttribute <= ContextAttribute.PackageInfo)
				{
					switch (contextAttribute)
					{
					case ContextAttribute.Sizes:
						num = SecSizes.SizeOf;
						goto IL_143;
					case ContextAttribute.Names:
						type = typeof(SafeFreeContextBuffer);
						goto IL_143;
					case ContextAttribute.Lifespan:
					case ContextAttribute.DceInfo:
						break;
					case ContextAttribute.StreamSizes:
						num = StreamSizes.SizeOf;
						goto IL_143;
					default:
						if (contextAttribute == ContextAttribute.PackageInfo)
						{
							type = typeof(SafeFreeContextBuffer);
							goto IL_143;
						}
						break;
					}
				}
				else
				{
					if (contextAttribute == ContextAttribute.NegotiationInfo)
					{
						type = typeof(SafeFreeContextBuffer);
						num = Marshal.SizeOf(typeof(NegotiationInfo));
						goto IL_143;
					}
					if (contextAttribute == ContextAttribute.ClientSpecifiedSpn)
					{
						type = typeof(SafeFreeContextBuffer);
						goto IL_143;
					}
				}
			}
			else if (contextAttribute <= ContextAttribute.LocalCertificate)
			{
				if (contextAttribute == ContextAttribute.RemoteCertificate)
				{
					type = typeof(SafeFreeCertContext);
					goto IL_143;
				}
				if (contextAttribute == ContextAttribute.LocalCertificate)
				{
					type = typeof(SafeFreeCertContext);
					goto IL_143;
				}
			}
			else
			{
				if (contextAttribute == ContextAttribute.IssuerListInfoEx)
				{
					num = Marshal.SizeOf(typeof(IssuerListInfoEx));
					type = typeof(SafeFreeContextBuffer);
					goto IL_143;
				}
				if (contextAttribute == ContextAttribute.ConnectionInfo)
				{
					num = Marshal.SizeOf(typeof(SslConnectionInfo));
					goto IL_143;
				}
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "ContextAttribute" }), "contextAttribute");
			IL_143:
			SafeHandle safeHandle = null;
			object obj = null;
			try
			{
				byte[] array = new byte[num];
				errorCode = SecModule.QueryContextAttributes(securityContext, contextAttribute, array, type, out safeHandle);
				if (errorCode != 0)
				{
					return null;
				}
				if (contextAttribute <= ContextAttribute.ClientSpecifiedSpn)
				{
					if (contextAttribute <= ContextAttribute.PackageInfo)
					{
						switch (contextAttribute)
						{
						case ContextAttribute.Sizes:
							obj = new SecSizes(array);
							break;
						case ContextAttribute.Names:
							obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
							break;
						case ContextAttribute.Lifespan:
						case ContextAttribute.DceInfo:
							break;
						case ContextAttribute.StreamSizes:
							obj = new StreamSizes(array);
							break;
						default:
							if (contextAttribute == ContextAttribute.PackageInfo)
							{
								obj = new SecurityPackageInfoClass(safeHandle, 0);
							}
							break;
						}
					}
					else
					{
						if (contextAttribute != ContextAttribute.NegotiationInfo)
						{
							if (contextAttribute != ContextAttribute.ClientSpecifiedSpn)
							{
								goto IL_279;
							}
						}
						else
						{
							try
							{
								byte[] array2;
								void* ptr;
								if ((array2 = array) == null || array2.Length == 0)
								{
									ptr = null;
								}
								else
								{
									ptr = (void*)(&array2[0]);
								}
								return new NegotiationInfoClass(safeHandle, Marshal.ReadInt32(new IntPtr(ptr), NegotiationInfo.NegotiationStateOffest));
							}
							finally
							{
								byte[] array2 = null;
							}
						}
						obj = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
					}
				}
				else if (contextAttribute <= ContextAttribute.LocalCertificate)
				{
					if (contextAttribute == ContextAttribute.RemoteCertificate || contextAttribute == ContextAttribute.LocalCertificate)
					{
						obj = safeHandle;
						safeHandle = null;
					}
				}
				else if (contextAttribute != ContextAttribute.IssuerListInfoEx)
				{
					if (contextAttribute == ContextAttribute.ConnectionInfo)
					{
						obj = new SslConnectionInfo(array);
					}
				}
				else
				{
					obj = new IssuerListInfoEx(safeHandle, array);
					safeHandle = null;
				}
				IL_279:;
			}
			finally
			{
				if (safeHandle != null)
				{
					safeHandle.Close();
				}
			}
			return obj;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00068098 File Offset: 0x00066298
		public static int SetContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute, object value)
		{
			if (contextAttribute == ContextAttribute.UiInfo)
			{
				IntPtr intPtr = (IntPtr)value;
				byte[] array = new byte[IntPtr.Size];
				if (IntPtr.Size == 4)
				{
					int num = intPtr.ToInt32();
					array[0] = (byte)num;
					array[1] = (byte)(num >> 8);
					array[2] = (byte)(num >> 16);
					array[3] = (byte)(num >> 24);
				}
				else
				{
					long num2 = intPtr.ToInt64();
					array[0] = (byte)num2;
					array[1] = (byte)(num2 >> 8);
					array[2] = (byte)(num2 >> 16);
					array[3] = (byte)(num2 >> 24);
					array[4] = (byte)(num2 >> 32);
					array[5] = (byte)(num2 >> 40);
					array[6] = (byte)(num2 >> 48);
					array[7] = (byte)(num2 >> 56);
				}
				return SecModule.SetContextAttributes(securityContext, contextAttribute, array);
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "ContextAttribute" }), "contextAttribute");
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00068160 File Offset: 0x00066360
		public static string ErrorDescription(int errorCode)
		{
			if (errorCode == -1)
			{
				return "An exception when invoking Win32 API";
			}
			SecurityStatus securityStatus = (SecurityStatus)errorCode;
			if (securityStatus <= SecurityStatus.MessageAltered)
			{
				switch (securityStatus)
				{
				case SecurityStatus.InvalidHandle:
					return "Invalid handle";
				case SecurityStatus.Unsupported:
				case SecurityStatus.InternalError:
					break;
				case SecurityStatus.TargetUnknown:
					return "Target unknown";
				case SecurityStatus.PackageNotFound:
					return "Package not found";
				default:
					if (securityStatus == SecurityStatus.InvalidToken)
					{
						return "Invalid token";
					}
					if (securityStatus == SecurityStatus.MessageAltered)
					{
						return "Message altered";
					}
					break;
				}
			}
			else
			{
				if (securityStatus == SecurityStatus.IncompleteMessage)
				{
					return "Message incomplete";
				}
				switch (securityStatus)
				{
				case SecurityStatus.BufferNotEnough:
					return "Buffer not enough";
				case SecurityStatus.WrongPrincipal:
					return "Wrong principal";
				case (SecurityStatus)(-2146893021):
				case SecurityStatus.TimeSkew:
					break;
				case SecurityStatus.UntrustedRoot:
					return "Untrusted root";
				default:
					if (securityStatus == SecurityStatus.ContinueNeeded)
					{
						return "Continue needed";
					}
					break;
				}
			}
			return "0x" + errorCode.ToString("x", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04001558 RID: 5464
		private static int s_DefaultCredentialsHandleCacheSize = SettingsSectionInternal.Section.DefaultCredentialsHandleCacheSize;

		// Token: 0x04001559 RID: 5465
		private static bool s_DefaultCredentialsHandleCacheEnabled = SSPIWrapper.s_DefaultCredentialsHandleCacheSize > 0;

		// Token: 0x0400155A RID: 5466
		private static readonly Lazy<ConcurrentDictionary<string, SafeFreeCredentials>> s_DefaultCredentialsHandleCache = new Lazy<ConcurrentDictionary<string, SafeFreeCredentials>>(new Func<ConcurrentDictionary<string, SafeFreeCredentials>>(SSPIWrapper.InitDefaultCredentialsHandleCache));

		// Token: 0x0200075B RID: 1883
		private enum OP
		{
			// Token: 0x04003211 RID: 12817
			Encrypt = 1,
			// Token: 0x04003212 RID: 12818
			Decrypt,
			// Token: 0x04003213 RID: 12819
			MakeSignature,
			// Token: 0x04003214 RID: 12820
			VerifySignature
		}
	}
}
