using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000203 RID: 515
	[SuppressUnmanagedCodeSecurity]
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00065D7A File Offset: 0x00063F7A
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00065D82 File Offset: 0x00063F82
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00065D8B File Offset: 0x00063F8B
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle(SecurDll dll)
		{
			if (dll == SecurDll.SECURITY)
			{
				return new SafeFreeContextBufferChannelBinding_SECURITY();
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "dll");
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00065DB8 File Offset: 0x00063FB8
		public unsafe static int QueryContextChannelBinding(SecurDll dll, SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			if (dll == SecurDll.SECURITY)
			{
				return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding_SECURITY(phContext, contextAttribute, buffer, refHandle);
			}
			return -1;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00065DCC File Offset: 0x00063FCC
		private unsafe static int QueryContextChannelBinding_SECURITY(SafeDeleteContext phContext, ContextAttribute contextAttribute, Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			int num = -2146893055;
			bool flag = false;
			if (contextAttribute != ContextAttribute.EndpointBindings && contextAttribute != ContextAttribute.UniqueBindings)
			{
				return num;
			}
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
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
					phContext.DangerousRelease();
				}
				if (num == 0 && refHandle != null)
				{
					refHandle.Set(buffer->pBindings);
					refHandle.size = buffer->BindingsLength;
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x04001548 RID: 5448
		private int size;
	}
}
