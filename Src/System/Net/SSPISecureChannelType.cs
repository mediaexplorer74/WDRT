using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001C6 RID: 454
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0006013C File Offset: 0x0005E33C
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x00060145 File Offset: 0x0005E345
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPISecureChannelType.m_SecurityPackages;
			}
			set
			{
				SSPISecureChannelType.m_SecurityPackages = value;
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0006014F File Offset: 0x0005E34F
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			return SafeFreeContextBuffer.EnumeratePackages(SSPISecureChannelType.Library, out pkgnum, out pkgArray);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0006015D File Offset: 0x0005E35D
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0006016E File Offset: 0x0005E36E
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0006017A File Offset: 0x0005E37A
		public int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(SSPISecureChannelType.Library, moduleName, usage, out outCredential);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00060189 File Offset: 0x0005E389
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0006019A File Offset: 0x0005E39A
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000601AC File Offset: 0x0005E3AC
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000601D0 File Offset: 0x0005E3D0
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000601F4 File Offset: 0x0005E3F4
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0006021C File Offset: 0x0005E41C
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00060244 File Offset: 0x0005E444
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

		// Token: 0x060011F2 RID: 4594 RVA: 0x000602B8 File Offset: 0x0005E4B8
		public int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
					num = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, null);
					context.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0006032C File Offset: 0x0005E52C
		public int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00060333 File Offset: 0x0005E533
		public int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0006033C File Offset: 0x0005E53C
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle(SSPISecureChannelType.Library);
			Bindings bindings = default(Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(SSPISecureChannelType.Library, phContext, attribute, &bindings, refHandle);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00060370 File Offset: 0x0005E570
		public unsafe int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle(SSPISecureChannelType.Library);
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
			return SafeFreeContextBuffer.QueryContextAttributes(SSPISecureChannelType.Library, phContext, attribute, ptr, refHandle);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00060417 File Offset: 0x0005E617
		public int SetContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer)
		{
			return SafeFreeContextBuffer.SetContextAttributes(SSPISecureChannelType.Library, phContext, attribute, buffer);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00060426 File Offset: 0x0005E626
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0006042D File Offset: 0x0005E62D
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00060434 File Offset: 0x0005E634
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.ApplyControlToken(SSPISecureChannelType.Library, ref refContext, inputBuffers);
		}

		// Token: 0x04001468 RID: 5224
		private static readonly SecurDll Library;

		// Token: 0x04001469 RID: 5225
		private static volatile SecurityPackageInfoClass[] m_SecurityPackages;
	}
}
