using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x0200033B RID: 827
	internal static class Win32
	{
		// Token: 0x0600295F RID: 10591 RVA: 0x0009A1B8 File Offset: 0x000983B8
		[SecuritySafeCritical]
		static Win32()
		{
			Win32Native.OSVERSIONINFO osversioninfo = new Win32Native.OSVERSIONINFO();
			if (!Environment.GetVersion(osversioninfo))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfo.MajorVersion > 5 || osversioninfo.MinorVersion > 0)
			{
				Win32._LsaLookupNames2Supported = true;
				Win32._WellKnownSidApisSupported = true;
				return;
			}
			Win32._LsaLookupNames2Supported = false;
			Win32Native.OSVERSIONINFOEX osversioninfoex = new Win32Native.OSVERSIONINFOEX();
			if (!Environment.GetVersionEx(osversioninfoex))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfoex.ServicePackMajor < 3)
			{
				Win32._WellKnownSidApisSupported = false;
				return;
			}
			Win32._WellKnownSidApisSupported = true;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x0009A240 File Offset: 0x00098440
		internal static bool LsaLookupNames2Supported
		{
			get
			{
				return Win32._LsaLookupNames2Supported;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x0009A247 File Offset: 0x00098447
		internal static bool WellKnownSidApisSupported
		{
			get
			{
				return Win32._WellKnownSidApisSupported;
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0009A250 File Offset: 0x00098450
		[SecurityCritical]
		internal static SafeLsaPolicyHandle LsaOpenPolicy(string systemName, PolicyRights rights)
		{
			Win32Native.LSA_OBJECT_ATTRIBUTES lsa_OBJECT_ATTRIBUTES;
			lsa_OBJECT_ATTRIBUTES.Length = Marshal.SizeOf(typeof(Win32Native.LSA_OBJECT_ATTRIBUTES));
			lsa_OBJECT_ATTRIBUTES.RootDirectory = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.ObjectName = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.Attributes = 0;
			lsa_OBJECT_ATTRIBUTES.SecurityDescriptor = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.SecurityQualityOfService = IntPtr.Zero;
			SafeLsaPolicyHandle safeLsaPolicyHandle;
			uint num;
			if ((num = Win32Native.LsaOpenPolicy(systemName, ref lsa_OBJECT_ATTRIBUTES, (int)rights, out safeLsaPolicyHandle)) == 0U)
			{
				return safeLsaPolicyHandle;
			}
			if (num == 3221225506U)
			{
				throw new UnauthorizedAccessException();
			}
			if (num == 3221225626U || num == 3221225495U)
			{
				throw new OutOfMemoryException();
			}
			int num2 = Win32Native.LsaNtStatusToWinError((int)num);
			throw new SystemException(Win32Native.GetMessage(num2));
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0009A2F4 File Offset: 0x000984F4
		[SecurityCritical]
		internal static byte[] ConvertIntPtrSidToByteArraySid(IntPtr binaryForm)
		{
			byte b = Marshal.ReadByte(binaryForm, 0);
			if (b != SecurityIdentifier.Revision)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), "binaryForm");
			}
			byte b2 = Marshal.ReadByte(binaryForm, 1);
			if (b2 < 0 || b2 > SecurityIdentifier.MaxSubAuthorities)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", new object[] { SecurityIdentifier.MaxSubAuthorities }), "binaryForm");
			}
			int num = (int)(8 + b2 * 4);
			byte[] array = new byte[num];
			Marshal.Copy(binaryForm, array, 0, num);
			return array;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0009A37C File Offset: 0x0009857C
		[SecurityCritical]
		internal static int CreateSidFromString(string stringSid, out byte[] resultSid)
		{
			IntPtr zero = IntPtr.Zero;
			int lastWin32Error;
			try
			{
				if (1 != Win32Native.ConvertStringSidToSid(stringSid, out zero))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					goto IL_2D;
				}
				resultSid = Win32.ConvertIntPtrSidToByteArraySid(zero);
			}
			finally
			{
				Win32Native.LocalFree(zero);
			}
			return 0;
			IL_2D:
			resultSid = null;
			return lastWin32Error;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0009A3CC File Offset: 0x000985CC
		[SecurityCritical]
		internal static int CreateWellKnownSid(WellKnownSidType sidType, SecurityIdentifier domainSid, out byte[] resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			resultSid = new byte[maxBinaryLength];
			if (Win32Native.CreateWellKnownSid((int)sidType, (domainSid == null) ? null : domainSid.BinaryForm, resultSid, ref maxBinaryLength) != 0)
			{
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x0009A428 File Offset: 0x00098628
		[SecurityCritical]
		internal static bool IsEqualDomainSid(SecurityIdentifier sid1, SecurityIdentifier sid2)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			if (sid1 == null || sid2 == null)
			{
				return false;
			}
			byte[] array = new byte[sid1.BinaryLength];
			sid1.GetBinaryForm(array, 0);
			byte[] array2 = new byte[sid2.BinaryLength];
			sid2.GetBinaryForm(array2, 0);
			bool flag;
			return Win32Native.IsEqualDomainSid(array, array2, out flag) != 0 && flag;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0009A498 File Offset: 0x00098698
		[SecurityCritical]
		internal unsafe static void InitializeReferencedDomainsPointer(SafeLsaMemoryHandle referencedDomains)
		{
			referencedDomains.Initialize((ulong)Marshal.SizeOf(typeof(Win32Native.LSA_REFERENCED_DOMAIN_LIST)));
			Win32Native.LSA_REFERENCED_DOMAIN_LIST lsa_REFERENCED_DOMAIN_LIST = referencedDomains.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				referencedDomains.AcquirePointer(ref ptr);
				if (!lsa_REFERENCED_DOMAIN_LIST.Domains.IsNull())
				{
					Win32Native.LSA_TRUST_INFORMATION* ptr2 = (Win32Native.LSA_TRUST_INFORMATION*)(void*)lsa_REFERENCED_DOMAIN_LIST.Domains;
					ptr2 += lsa_REFERENCED_DOMAIN_LIST.Entries;
					long num = (long)((byte*)ptr2 - (byte*)ptr);
					referencedDomains.Initialize((ulong)num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					referencedDomains.ReleasePointer();
				}
			}
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0009A52C File Offset: 0x0009872C
		[SecurityCritical]
		internal static int GetWindowsAccountDomainSid(SecurityIdentifier sid, out SecurityIdentifier resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			byte[] array2 = new byte[maxBinaryLength];
			if (Win32Native.GetWindowsAccountDomainSid(array, array2, ref maxBinaryLength) != 0)
			{
				resultSid = new SecurityIdentifier(array2, 0);
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0009A590 File Offset: 0x00098790
		[SecurityCritical]
		internal static bool IsWellKnownSid(SecurityIdentifier sid, WellKnownSidType type)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			return Win32Native.IsWellKnownSid(array, (int)type) != 0;
		}

		// Token: 0x0600296A RID: 10602
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int ImpersonateLoggedOnUser(SafeAccessTokenHandle hToken);

		// Token: 0x0600296B RID: 10603
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int OpenThreadToken(TokenAccessLevels dwDesiredAccess, WinSecurityContext OpenAs, out SafeAccessTokenHandle phThreadToken);

		// Token: 0x0600296C RID: 10604
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int RevertToSelf();

		// Token: 0x0600296D RID: 10605
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SetThreadToken(SafeAccessTokenHandle hToken);

		// Token: 0x0400110A RID: 4362
		internal const int FALSE = 0;

		// Token: 0x0400110B RID: 4363
		internal const int TRUE = 1;

		// Token: 0x0400110C RID: 4364
		private static bool _LsaLookupNames2Supported;

		// Token: 0x0400110D RID: 4365
		private static bool _WellKnownSidApisSupported;
	}
}
