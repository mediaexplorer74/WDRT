using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001C7 RID: 455
	internal class SSPIAuthType : SSPIInterface
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0006044A File Offset: 0x0005E64A
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x00060453 File Offset: 0x0005E653
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPIAuthType.m_SecurityPackages;
			}
			set
			{
				SSPIAuthType.m_SecurityPackages = value;
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0006045D File Offset: 0x0005E65D
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			return SafeFreeContextBuffer.EnumeratePackages(SSPIAuthType.Library, out pkgnum, out pkgArray);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0006046B File Offset: 0x0005E66B
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0006047C File Offset: 0x0005E67C
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00060488 File Offset: 0x0005E688
		public int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(SSPIAuthType.Library, moduleName, usage, out outCredential);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00060497 File Offset: 0x0005E697
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000604A8 File Offset: 0x0005E6A8
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000604BC File Offset: 0x0005E6BC
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPIAuthType.Library, ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000604E0 File Offset: 0x0005E6E0
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPIAuthType.Library, ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00060504 File Offset: 0x0005E704
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPIAuthType.Library, ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0006052C File Offset: 0x0005E72C
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPIAuthType.Library, ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00060554 File Offset: 0x0005E754
		public int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.NativeNTSSPI.EncryptMessage(ref context._handle, 0U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000605C8 File Offset: 0x0005E7C8
		public unsafe int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			bool flag = false;
			uint num2 = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, &num2);
					context.DangerousRelease();
				}
			}
			if (num == 0 && num2 == 2147483649U)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_message_not_encrypted"));
			}
			return num;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0006065C File Offset: 0x0005E85C
		public int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.NativeNTSSPI.EncryptMessage(ref context._handle, 2147483649U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000606D4 File Offset: 0x0005E8D4
		public unsafe int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			bool flag = false;
			uint num2 = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, &num2);
					context.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0006074C File Offset: 0x0005E94C
		public int QueryContextChannelBinding(SafeDeleteContext context, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding binding)
		{
			binding = null;
			throw new NotSupportedException();
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00060758 File Offset: 0x0005E958
		public unsafe int QueryContextAttributes(SafeDeleteContext context, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle(SSPIAuthType.Library);
				}
				else
				{
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw new ArgumentException(SR.GetString("SSPIInvalidHandleType", new object[] { handleType.FullName }), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return SafeFreeContextBuffer.QueryContextAttributes(SSPIAuthType.Library, context, attribute, ptr, refHandle);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000607FF File Offset: 0x0005E9FF
		public int SetContextAttributes(SafeDeleteContext context, ContextAttribute attribute, byte[] buffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00060806 File Offset: 0x0005EA06
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken)
		{
			return SSPIAuthType.GetSecurityContextToken(phContext, out phToken);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006080F File Offset: 0x0005EA0F
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.CompleteAuthToken(SSPIAuthType.Library, ref refContext, inputBuffers);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006081D File Offset: 0x0005EA1D
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00060824 File Offset: 0x0005EA24
		private static int GetSecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle safeHandle)
		{
			int num = -2146893055;
			bool flag = false;
			safeHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = UnsafeNclNativeMethods.SafeNetHandles.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
					phContext.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0400146A RID: 5226
		private static readonly SecurDll Library;

		// Token: 0x0400146B RID: 5227
		private static volatile SecurityPackageInfoClass[] m_SecurityPackages;
	}
}
