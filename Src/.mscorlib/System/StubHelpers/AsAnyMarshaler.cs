using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x0200059F RID: 1439
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal struct AsAnyMarshaler
	{
		// Token: 0x0600432B RID: 17195 RVA: 0x000FB329 File Offset: 0x000F9529
		private static bool IsIn(int dwFlags)
		{
			return (dwFlags & 268435456) != 0;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000FB335 File Offset: 0x000F9535
		private static bool IsOut(int dwFlags)
		{
			return (dwFlags & 536870912) != 0;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000FB341 File Offset: 0x000F9541
		private static bool IsAnsi(int dwFlags)
		{
			return (dwFlags & 16711680) != 0;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000FB34D File Offset: 0x000F954D
		private static bool IsThrowOn(int dwFlags)
		{
			return (dwFlags & 65280) != 0;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000FB359 File Offset: 0x000F9559
		private static bool IsBestFit(int dwFlags)
		{
			return (dwFlags & 255) != 0;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000FB365 File Offset: 0x000F9565
		internal AsAnyMarshaler(IntPtr pvArrayMarshaler)
		{
			this.pvArrayMarshaler = pvArrayMarshaler;
			this.backPropAction = AsAnyMarshaler.BackPropAction.None;
			this.layoutType = null;
			this.cleanupWorkList = null;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000FB384 File Offset: 0x000F9584
		[SecurityCritical]
		private unsafe IntPtr ConvertArrayToNative(object pManagedHome, int dwFlags)
		{
			Type elementType = pManagedHome.GetType().GetElementType();
			VarEnum varEnum;
			switch (Type.GetTypeCode(elementType))
			{
			case TypeCode.Object:
				if (elementType == typeof(IntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_I4 : VarEnum.VT_I8);
					goto IL_10D;
				}
				if (elementType == typeof(UIntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_UI4 : VarEnum.VT_UI8);
					goto IL_10D;
				}
				break;
			case TypeCode.Boolean:
				varEnum = (VarEnum)254;
				goto IL_10D;
			case TypeCode.Char:
				varEnum = (AsAnyMarshaler.IsAnsi(dwFlags) ? ((VarEnum)253) : VarEnum.VT_UI2);
				goto IL_10D;
			case TypeCode.SByte:
				varEnum = VarEnum.VT_I1;
				goto IL_10D;
			case TypeCode.Byte:
				varEnum = VarEnum.VT_UI1;
				goto IL_10D;
			case TypeCode.Int16:
				varEnum = VarEnum.VT_I2;
				goto IL_10D;
			case TypeCode.UInt16:
				varEnum = VarEnum.VT_UI2;
				goto IL_10D;
			case TypeCode.Int32:
				varEnum = VarEnum.VT_I4;
				goto IL_10D;
			case TypeCode.UInt32:
				varEnum = VarEnum.VT_UI4;
				goto IL_10D;
			case TypeCode.Int64:
				varEnum = VarEnum.VT_I8;
				goto IL_10D;
			case TypeCode.UInt64:
				varEnum = VarEnum.VT_UI8;
				goto IL_10D;
			case TypeCode.Single:
				varEnum = VarEnum.VT_R4;
				goto IL_10D;
			case TypeCode.Double:
				varEnum = VarEnum.VT_R8;
				goto IL_10D;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
			IL_10D:
			int num = (int)varEnum;
			if (AsAnyMarshaler.IsBestFit(dwFlags))
			{
				num |= 65536;
			}
			if (AsAnyMarshaler.IsThrowOn(dwFlags))
			{
				num |= 16777216;
			}
			MngdNativeArrayMarshaler.CreateMarshaler(this.pvArrayMarshaler, IntPtr.Zero, num);
			IntPtr intPtr2;
			IntPtr intPtr = new IntPtr((void*)(&intPtr2));
			MngdNativeArrayMarshaler.ConvertSpaceToNative(this.pvArrayMarshaler, ref pManagedHome, intPtr);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				MngdNativeArrayMarshaler.ConvertContentsToNative(this.pvArrayMarshaler, ref pManagedHome, intPtr);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Array;
			}
			return intPtr2;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000FB514 File Offset: 0x000F9714
		[SecurityCritical]
		private static IntPtr ConvertStringToNative(string pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				intPtr = CSTRMarshaler.ConvertToNative(dwFlags & 65535, pManagedHome, IntPtr.Zero);
			}
			else
			{
				StubHelpers.CheckStringLength(pManagedHome.Length);
				int num = (pManagedHome.Length + 1) * 2;
				intPtr = Marshal.AllocCoTaskMem(num);
				string.InternalCopy(pManagedHome, intPtr, num);
			}
			return intPtr;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000FB564 File Offset: 0x000F9764
		[SecurityCritical]
		private unsafe IntPtr ConvertStringBuilderToNative(StringBuilder pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				StubHelpers.CheckStringLength(pManagedHome.Capacity);
				int num = pManagedHome.Capacity * Marshal.SystemMaxDBCSCharSize + 4;
				intPtr = Marshal.AllocCoTaskMem(num);
				byte* ptr = (byte*)(void*)intPtr;
				*(ptr + num - 3) = 0;
				*(ptr + num - 2) = 0;
				*(ptr + num - 1) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num2;
					byte[] array = AnsiCharMarshaler.DoAnsiConversion(pManagedHome.ToString(), AsAnyMarshaler.IsBestFit(dwFlags), AsAnyMarshaler.IsThrowOn(dwFlags), out num2);
					Buffer.Memcpy(ptr, 0, array, 0, num2);
					ptr[num2] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderAnsi;
				}
			}
			else
			{
				int num3 = pManagedHome.Capacity * 2 + 4;
				intPtr = Marshal.AllocCoTaskMem(num3);
				byte* ptr2 = (byte*)(void*)intPtr;
				*(ptr2 + num3 - 1) = 0;
				*(ptr2 + num3 - 2) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num4 = pManagedHome.Length * 2;
					pManagedHome.InternalCopy(intPtr, num4);
					ptr2[num4] = 0;
					(ptr2 + num4)[1] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderUnicode;
				}
			}
			return intPtr;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000FB668 File Offset: 0x000F9868
		[SecurityCritical]
		private unsafe IntPtr ConvertLayoutToNative(object pManagedHome, int dwFlags)
		{
			int num = Marshal.SizeOfHelper(pManagedHome.GetType(), false);
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				StubHelpers.FmtClassUpdateNativeInternal(pManagedHome, (byte*)intPtr.ToPointer(), ref this.cleanupWorkList);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Layout;
			}
			this.layoutType = pManagedHome.GetType();
			return intPtr;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000FB6C0 File Offset: 0x000F98C0
		[SecurityCritical]
		internal IntPtr ConvertToNative(object pManagedHome, int dwFlags)
		{
			if (pManagedHome == null)
			{
				return IntPtr.Zero;
			}
			if (pManagedHome is ArrayWithOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MarshalAsAnyRestriction"));
			}
			IntPtr intPtr;
			string text;
			StringBuilder stringBuilder;
			if (pManagedHome.GetType().IsArray)
			{
				intPtr = this.ConvertArrayToNative(pManagedHome, dwFlags);
			}
			else if ((text = pManagedHome as string) != null)
			{
				intPtr = AsAnyMarshaler.ConvertStringToNative(text, dwFlags);
			}
			else if ((stringBuilder = pManagedHome as StringBuilder) != null)
			{
				intPtr = this.ConvertStringBuilderToNative(stringBuilder, dwFlags);
			}
			else
			{
				if (!pManagedHome.GetType().IsLayoutSequential && !pManagedHome.GetType().IsExplicitLayout)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
				}
				intPtr = this.ConvertLayoutToNative(pManagedHome, dwFlags);
			}
			return intPtr;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000FB768 File Offset: 0x000F9968
		[SecurityCritical]
		internal unsafe void ConvertToManaged(object pManagedHome, IntPtr pNativeHome)
		{
			switch (this.backPropAction)
			{
			case AsAnyMarshaler.BackPropAction.Array:
				MngdNativeArrayMarshaler.ConvertContentsToManaged(this.pvArrayMarshaler, ref pManagedHome, new IntPtr((void*)(&pNativeHome)));
				return;
			case AsAnyMarshaler.BackPropAction.Layout:
				StubHelpers.FmtClassUpdateCLRInternal(pManagedHome, (byte*)pNativeHome.ToPointer());
				return;
			case AsAnyMarshaler.BackPropAction.StringBuilderAnsi:
			{
				sbyte* ptr = (sbyte*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferAnsiInternal(ptr, Win32Native.lstrlenA(pNativeHome));
				return;
			}
			case AsAnyMarshaler.BackPropAction.StringBuilderUnicode:
			{
				char* ptr2 = (char*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferInternal(ptr2, Win32Native.lstrlenW(pNativeHome));
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000FB7EE File Offset: 0x000F99EE
		[SecurityCritical]
		internal void ClearNative(IntPtr pNativeHome)
		{
			if (pNativeHome != IntPtr.Zero)
			{
				if (this.layoutType != null)
				{
					Marshal.DestroyStructure(pNativeHome, this.layoutType);
				}
				Win32Native.CoTaskMemFree(pNativeHome);
			}
			StubHelpers.DestroyCleanupList(ref this.cleanupWorkList);
		}

		// Token: 0x04001BE1 RID: 7137
		private const ushort VTHACK_ANSICHAR = 253;

		// Token: 0x04001BE2 RID: 7138
		private const ushort VTHACK_WINBOOL = 254;

		// Token: 0x04001BE3 RID: 7139
		private IntPtr pvArrayMarshaler;

		// Token: 0x04001BE4 RID: 7140
		private AsAnyMarshaler.BackPropAction backPropAction;

		// Token: 0x04001BE5 RID: 7141
		private Type layoutType;

		// Token: 0x04001BE6 RID: 7142
		private CleanupWorkList cleanupWorkList;

		// Token: 0x02000C30 RID: 3120
		private enum BackPropAction
		{
			// Token: 0x04003723 RID: 14115
			None,
			// Token: 0x04003724 RID: 14116
			Array,
			// Token: 0x04003725 RID: 14117
			Layout,
			// Token: 0x04003726 RID: 14118
			StringBuilderAnsi,
			// Token: 0x04003727 RID: 14119
			StringBuilderUnicode
		}
	}
}
