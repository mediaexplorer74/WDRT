using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200059D RID: 1437
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdHiddenLengthArrayMarshaler
	{
		// Token: 0x06004315 RID: 17173
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, IntPtr cbElementSize, ushort vt);

		// Token: 0x06004316 RID: 17174
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x06004317 RID: 17175
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x06004318 RID: 17176 RVA: 0x000FB070 File Offset: 0x000F9270
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToNative(ref managedArray[i], out ptr[i]);
				}
			}
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000FB0B0 File Offset: 0x000F92B0
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToNative(managedArray[i], ptr + i);
				}
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000FB0F0 File Offset: 0x000F92F0
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = HResultExceptionMarshaler.ConvertToNative(managedArray[i]);
				}
			}
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000FB128 File Offset: 0x000F9328
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = NullableMarshaler.ConvertToNative<T>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000FB16C File Offset: 0x000F936C
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = KeyValuePairMarshaler.ConvertToNative<K, V>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x0600431D RID: 17181
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int elementCount);

		// Token: 0x0600431E RID: 17182
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x0600431F RID: 17183 RVA: 0x000FB1B0 File Offset: 0x000F93B0
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToManaged(out managedArray[i], ref ptr[i]);
				}
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000FB1F0 File Offset: 0x000F93F0
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToManaged(ptr + i, ref managedArray[i]);
				}
			}
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000FB234 File Offset: 0x000F9434
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = HResultExceptionMarshaler.ConvertToManaged(ptr[i]);
				}
			}
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000FB26C File Offset: 0x000F946C
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = NullableMarshaler.ConvertToManaged<T>(ptr[i]);
				}
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000FB2B0 File Offset: 0x000F94B0
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = KeyValuePairMarshaler.ConvertToManaged<K, V>(ptr[i]);
				}
			}
		}

		// Token: 0x06004324 RID: 17188
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNativeContents(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);

		// Token: 0x06004325 RID: 17189 RVA: 0x000FB2F4 File Offset: 0x000F94F4
		[SecurityCritical]
		internal unsafe static void ClearNativeContents_Type(IntPtr pNativeHome, int cElements)
		{
			TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
			if (ptr != null)
			{
				for (int i = 0; i < cElements; i++)
				{
					SystemTypeMarshaler.ClearNative(ptr);
					ptr++;
				}
			}
		}
	}
}
