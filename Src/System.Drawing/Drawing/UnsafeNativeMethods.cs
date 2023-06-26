using System;
using System.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing
{
	// Token: 0x02000039 RID: 57
	[SuppressUnmanagedCodeSecurity]
	internal class UnsafeNativeMethods
	{
		// Token: 0x0600059C RID: 1436
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

		// Token: 0x0600059D RID: 1437
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x0600059E RID: 1438 RVA: 0x0001884D File Offset: 0x00016A4D
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), SafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600059F RID: 1439
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDeleteDC(HandleRef hDC);

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001885F File Offset: 0x00016A5F
		public static bool DeleteDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.GDI);
			return UnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x060005A1 RID: 1441
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true, SetLastError = true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018878 File Offset: 0x00016A78
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, SafeNativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x060005A3 RID: 1443
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

		// Token: 0x060005A4 RID: 1444 RVA: 0x00018892 File Offset: 0x00016A92
		public static IntPtr CreateCompatibleDC(HandleRef hDC)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateCompatibleDC(hDC), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060005A5 RID: 1445
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x060005A6 RID: 1446
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetSystemDefaultLCID();

		// Token: 0x060005A7 RID: 1447
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x060005A8 RID: 1448
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] NativeMethods.NONCLIENTMETRICS pvParam, int fWinIni);

		// Token: 0x060005A9 RID: 1449
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In] [Out] SafeNativeMethods.LOGFONT pvParam, int fWinIni);

		// Token: 0x060005AA RID: 1450
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x060005AB RID: 1451
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetObjectType(HandleRef hObject);

		// Token: 0x060005AC RID: 1452 RVA: 0x000188A4 File Offset: 0x00016AA4
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static object PtrToStructure(IntPtr lparam, Type cls)
		{
			return Marshal.PtrToStructure(lparam, cls);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000188AD File Offset: 0x00016AAD
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void PtrToStructure(IntPtr lparam, object data)
		{
			Marshal.PtrToStructure(lparam, data);
		}

		// Token: 0x02000103 RID: 259
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06000CBB RID: 3259
			int Read([In] IntPtr buf, [In] int len);

			// Token: 0x06000CBC RID: 3260
			int Write([In] IntPtr buf, [In] int len);

			// Token: 0x06000CBD RID: 3261
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, [In] int dwOrigin);

			// Token: 0x06000CBE RID: 3262
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x06000CBF RID: 3263
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x06000CC0 RID: 3264
			void Commit([In] int grfCommitFlags);

			// Token: 0x06000CC1 RID: 3265
			void Revert();

			// Token: 0x06000CC2 RID: 3266
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x06000CC3 RID: 3267
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [In] int dwLockType);

			// Token: 0x06000CC4 RID: 3268
			void Stat([In] IntPtr pStatstg, [In] int grfStatFlag);

			// Token: 0x06000CC5 RID: 3269
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream Clone();
		}

		// Token: 0x02000104 RID: 260
		internal class ComStreamFromDataStream : UnsafeNativeMethods.IStream
		{
			// Token: 0x06000CC6 RID: 3270 RVA: 0x0002C90E File Offset: 0x0002AB0E
			internal ComStreamFromDataStream(Stream dataStream)
			{
				if (dataStream == null)
				{
					throw new ArgumentNullException("dataStream");
				}
				this.dataStream = dataStream;
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x0002C934 File Offset: 0x0002AB34
			private void ActualizeVirtualPosition()
			{
				if (this.virtualPosition == -1L)
				{
					return;
				}
				if (this.virtualPosition > this.dataStream.Length)
				{
					this.dataStream.SetLength(this.virtualPosition);
				}
				this.dataStream.Position = this.virtualPosition;
				this.virtualPosition = -1L;
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x0002C989 File Offset: 0x0002AB89
			public virtual UnsafeNativeMethods.IStream Clone()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
				return null;
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x0002C991 File Offset: 0x0002AB91
			public virtual void Commit(int grfCommitFlags)
			{
				this.dataStream.Flush();
				this.ActualizeVirtualPosition();
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
			public virtual long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
			{
				int num = 4096;
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				long num2 = 0L;
				try
				{
					while (num2 < cb)
					{
						int num3 = num;
						if (num2 + (long)num3 > cb)
						{
							num3 = (int)(cb - num2);
						}
						int num4 = this.Read(intPtr, num3);
						if (num4 == 0)
						{
							break;
						}
						if (pstm.Write(intPtr, num4) != num4)
						{
							throw UnsafeNativeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
						}
						num2 += (long)num4;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (pcbRead != null && pcbRead.Length != 0)
				{
					pcbRead[0] = num2;
				}
				return num2;
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x0002CA3C File Offset: 0x0002AC3C
			public virtual Stream GetDataStream()
			{
				return this.dataStream;
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x00015259 File Offset: 0x00013459
			public virtual void LockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x0002B854 File Offset: 0x00029A54
			protected static ExternalException EFail(string msg)
			{
				throw new ExternalException(msg, -2147467259);
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x0002B861 File Offset: 0x00029A61
			protected static void NotImplemented()
			{
				throw new ExternalException(SR.GetString("NotImplemented"), -2147467263);
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x0002CA44 File Offset: 0x0002AC44
			public virtual int Read(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				int num = this.Read(array, length);
				Marshal.Copy(array, 0, buf, length);
				return num;
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x0002CA6B File Offset: 0x0002AC6B
			public virtual int Read(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				return this.dataStream.Read(buffer, 0, length);
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x0002CA81 File Offset: 0x0002AC81
			public virtual void Revert()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x0002CA88 File Offset: 0x0002AC88
			public virtual long Seek(long offset, int origin)
			{
				long position = this.virtualPosition;
				if (this.virtualPosition == -1L)
				{
					position = this.dataStream.Position;
				}
				long length = this.dataStream.Length;
				switch (origin)
				{
				case 0:
					if (offset <= length)
					{
						this.dataStream.Position = offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset;
					}
					break;
				case 1:
					if (offset + position <= length)
					{
						this.dataStream.Position = position + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset + position;
					}
					break;
				case 2:
					if (offset <= 0L)
					{
						this.dataStream.Position = length + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = length + offset;
					}
					break;
				}
				if (this.virtualPosition != -1L)
				{
					return this.virtualPosition;
				}
				return this.dataStream.Position;
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x0002CB60 File Offset: 0x0002AD60
			public virtual void SetSize(long value)
			{
				this.dataStream.SetLength(value);
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x0002CA81 File Offset: 0x0002AC81
			public virtual void Stat(IntPtr pstatstg, int grfStatFlag)
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x00015259 File Offset: 0x00013459
			public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x0002CB70 File Offset: 0x0002AD70
			public virtual int Write(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				Marshal.Copy(buf, array, 0, length);
				return this.Write(array, length);
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x0002CB95 File Offset: 0x0002AD95
			public virtual int Write(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				this.dataStream.Write(buffer, 0, length);
				return length;
			}

			// Token: 0x04000B2C RID: 2860
			protected Stream dataStream;

			// Token: 0x04000B2D RID: 2861
			private long virtualPosition = -1L;
		}
	}
}
