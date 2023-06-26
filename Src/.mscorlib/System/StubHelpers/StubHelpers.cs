using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005AA RID: 1450
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SuppressUnmanagedCodeSecurity]
	internal static class StubHelpers
	{
		// Token: 0x06004349 RID: 17225
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsQCall(IntPtr pMD);

		// Token: 0x0600434A RID: 17226
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitDeclaringType(IntPtr pMD);

		// Token: 0x0600434B RID: 17227
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetNDirectTarget(IntPtr pMD);

		// Token: 0x0600434C RID: 17228
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetDelegateTarget(Delegate pThis, ref IntPtr pStubArg);

		// Token: 0x0600434D RID: 17229
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DemandPermission(IntPtr pNMD);

		// Token: 0x0600434E RID: 17230
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastError();

		// Token: 0x0600434F RID: 17231
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ThrowInteropParamException(int resID, int paramIdx);

		// Token: 0x06004350 RID: 17232 RVA: 0x000FBB40 File Offset: 0x000F9D40
		[SecurityCritical]
		internal static IntPtr AddToCleanupList(ref CleanupWorkList pCleanupWorkList, SafeHandle handle)
		{
			if (pCleanupWorkList == null)
			{
				pCleanupWorkList = new CleanupWorkList();
			}
			CleanupWorkListElement cleanupWorkListElement = new CleanupWorkListElement(handle);
			pCleanupWorkList.Add(cleanupWorkListElement);
			return StubHelpers.SafeHandleAddRef(handle, ref cleanupWorkListElement.m_owned);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000FBB73 File Offset: 0x000F9D73
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void DestroyCleanupList(ref CleanupWorkList pCleanupWorkList)
		{
			if (pCleanupWorkList != null)
			{
				pCleanupWorkList.Destroy();
				pCleanupWorkList = null;
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000FBB84 File Offset: 0x000F9D84
		internal static Exception GetHRExceptionObject(int hr)
		{
			Exception ex = StubHelpers.InternalGetHRExceptionObject(hr);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x06004353 RID: 17235
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetHRExceptionObject(int hr);

		// Token: 0x06004354 RID: 17236 RVA: 0x000FBBA0 File Offset: 0x000F9DA0
		internal static Exception GetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis)
		{
			Exception ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, false);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000FBBC0 File Offset: 0x000F9DC0
		internal static Exception GetCOMHRExceptionObject_WinRT(int hr, IntPtr pCPCMD, object pThis)
		{
			Exception ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, true);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x06004356 RID: 17238
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis, bool fForWinRT);

		// Token: 0x06004357 RID: 17239
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateCustomMarshalerHelper(IntPtr pMD, int paramToken, IntPtr hndManagedType);

		// Token: 0x06004358 RID: 17240 RVA: 0x000FBBDE File Offset: 0x000F9DDE
		[SecurityCritical]
		internal static IntPtr SafeHandleAddRef(SafeHandle pHandle, ref bool success)
		{
			if (pHandle == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
			}
			pHandle.DangerousAddRef(ref success);
			if (!success)
			{
				return IntPtr.Zero;
			}
			return pHandle.DangerousGetHandle();
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000FBC0C File Offset: 0x000F9E0C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void SafeHandleRelease(SafeHandle pHandle)
		{
			if (pHandle == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
			}
			try
			{
				pHandle.DangerousRelease();
			}
			catch (Exception ex)
			{
				Mda.ReportErrorSafeHandleRelease(ex);
			}
		}

		// Token: 0x0600435A RID: 17242
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget, out bool pfNeedsRelease);

		// Token: 0x0600435B RID: 17243
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRT(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x0600435C RID: 17244
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRTSharedGeneric(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x0600435D RID: 17245
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRTDelegate(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x0600435E RID: 17246
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ShouldCallWinRTInterface(object objSrc, IntPtr pCPCMD);

		// Token: 0x0600435F RID: 17247
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate GetTargetForAmbiguousVariantCall(object objSrc, IntPtr pMT, out bool fUseString);

		// Token: 0x06004360 RID: 17248
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StubRegisterRCW(object pThis);

		// Token: 0x06004361 RID: 17249
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StubUnregisterRCW(object pThis);

		// Token: 0x06004362 RID: 17250
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetDelegateInvokeMethod(Delegate pThis);

		// Token: 0x06004363 RID: 17251
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetWinRTFactoryObject(IntPtr pCPCMD);

		// Token: 0x06004364 RID: 17252
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetWinRTFactoryReturnValue(object pThis, IntPtr pCtorEntry);

		// Token: 0x06004365 RID: 17253
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetOuterInspectable(object pThis, IntPtr pCtorMD);

		// Token: 0x06004366 RID: 17254
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception TriggerExceptionSwallowedMDA(Exception ex, IntPtr pManagedTarget);

		// Token: 0x06004367 RID: 17255
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CheckCollectedDelegateMDA(IntPtr pEntryThunk);

		// Token: 0x06004368 RID: 17256
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ProfilerBeginTransitionCallback(IntPtr pSecretParam, IntPtr pThread, object pThis);

		// Token: 0x06004369 RID: 17257
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProfilerEndTransitionCallback(IntPtr pMD, IntPtr pThread);

		// Token: 0x0600436A RID: 17258 RVA: 0x000FBC50 File Offset: 0x000F9E50
		internal static void CheckStringLength(int length)
		{
			StubHelpers.CheckStringLength((uint)length);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000FBC58 File Offset: 0x000F9E58
		internal static void CheckStringLength(uint length)
		{
			if (length > 2147483632U)
			{
				throw new MarshalDirectiveException(Environment.GetResourceString("Marshaler_StringTooLong"));
			}
		}

		// Token: 0x0600436C RID: 17260
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int strlen(sbyte* ptr);

		// Token: 0x0600436D RID: 17261
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DecimalCanonicalizeInternal(ref decimal dec);

		// Token: 0x0600436E RID: 17262
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateNativeInternal(object obj, byte* pNative, ref CleanupWorkList pCleanupWorkList);

		// Token: 0x0600436F RID: 17263
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateCLRInternal(object obj, byte* pNative);

		// Token: 0x06004370 RID: 17264
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void LayoutDestroyNativeInternal(byte* pNative, IntPtr pMT);

		// Token: 0x06004371 RID: 17265
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AllocateInternal(IntPtr typeHandle);

		// Token: 0x06004372 RID: 17266
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToUnmanagedVaListInternal(IntPtr va_list, uint vaListSize, IntPtr pArgIterator);

		// Token: 0x06004373 RID: 17267
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToManagedVaListInternal(IntPtr va_list, IntPtr pArgIterator);

		// Token: 0x06004374 RID: 17268
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint CalcVaListSize(IntPtr va_list);

		// Token: 0x06004375 RID: 17269
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateObject(object obj, IntPtr pMD, object pThis);

		// Token: 0x06004376 RID: 17270
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogPinnedArgument(IntPtr localDesc, IntPtr nativeArg);

		// Token: 0x06004377 RID: 17271
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateByref(IntPtr byref, IntPtr pMD, object pThis);

		// Token: 0x06004378 RID: 17272
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContext();

		// Token: 0x06004379 RID: 17273
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContextAddr();

		// Token: 0x0600437A RID: 17274
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void TriggerGCForMDA();
	}
}
