using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal sealed class IWbemClassObjectFreeThreaded : IDisposable, ISerializable
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00020C21 File Offset: 0x0001EE21
		public IWbemClassObjectFreeThreaded(IntPtr pWbemClassObject)
		{
			this.pWbemClassObject = pWbemClassObject;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00020C3B File Offset: 0x0001EE3B
		public static implicit operator IntPtr(IWbemClassObjectFreeThreaded wbemClassObject)
		{
			if (wbemClassObject == null)
			{
				return IntPtr.Zero;
			}
			return wbemClassObject.pWbemClassObject;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public IWbemClassObjectFreeThreaded(SerializationInfo info, StreamingContext context)
		{
			byte[] array = info.GetValue("flatWbemClassObject", typeof(byte[])) as byte[];
			if (array == null)
			{
				throw new SerializationException();
			}
			this.DeserializeFromBlob(array);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00020C95 File Offset: 0x0001EE95
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("flatWbemClassObject", this.SerializeToBlob());
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		public void Dispose()
		{
			this.Dispose_(false);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		private void Dispose_(bool finalization)
		{
			if (this.pWbemClassObject != IntPtr.Zero)
			{
				Marshal.Release(this.pWbemClassObject);
				this.pWbemClassObject = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		~IWbemClassObjectFreeThreaded()
		{
			this.Dispose_(true);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00020D14 File Offset: 0x0001EF14
		private void DeserializeFromBlob(byte[] rg)
		{
			IntPtr intPtr = IntPtr.Zero;
			IStream stream = null;
			try
			{
				this.pWbemClassObject = IntPtr.Zero;
				intPtr = Marshal.AllocHGlobal(rg.Length);
				Marshal.Copy(rg, 0, intPtr, rg.Length);
				stream = IWbemClassObjectFreeThreaded.CreateStreamOnHGlobal(intPtr, 0);
				this.pWbemClassObject = IWbemClassObjectFreeThreaded.CoUnmarshalInterface(stream, ref IWbemClassObjectFreeThreaded.IID_IWbemClassObject);
			}
			finally
			{
				if (stream != null)
				{
					Marshal.ReleaseComObject(stream);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00020D94 File Offset: 0x0001EF94
		private byte[] SerializeToBlob()
		{
			byte[] array = null;
			IStream stream = null;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				stream = IWbemClassObjectFreeThreaded.CreateStreamOnHGlobal(IntPtr.Zero, 1);
				IWbemClassObjectFreeThreaded.CoMarshalInterface(stream, ref IWbemClassObjectFreeThreaded.IID_IWbemClassObject, this.pWbemClassObject, 2U, IntPtr.Zero, 2U);
				System.Runtime.InteropServices.ComTypes.STATSTG statstg;
				stream.Stat(out statstg, 0);
				array = new byte[statstg.cbSize];
				intPtr = IWbemClassObjectFreeThreaded.GlobalLock(IWbemClassObjectFreeThreaded.GetHGlobalFromStream(stream));
				Marshal.Copy(intPtr, array, 0, (int)statstg.cbSize);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					IWbemClassObjectFreeThreaded.GlobalUnlock(intPtr);
				}
				if (stream != null)
				{
					Marshal.ReleaseComObject(stream);
				}
			}
			GC.KeepAlive(this);
			return array;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00020E38 File Offset: 0x0001F038
		public int GetQualifierSet_(out IWbemQualifierSetFreeThreaded ppQualSet)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.GetQualifierSet_f(3, this.pWbemClassObject, out intPtr);
			if (num < 0)
			{
				ppQualSet = null;
			}
			else
			{
				ppQualSet = new IWbemQualifierSetFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00020E90 File Offset: 0x0001F090
		public int Get_(string wszName, int lFlags, ref object pVal, ref int pType, ref int plFlavor)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.Get_f(4, this.pWbemClassObject, wszName, lFlags, ref pVal, ref pType, ref plFlavor);
			if (num == -2147217393 && string.Compare(wszName, "__path", StringComparison.OrdinalIgnoreCase) == 0)
			{
				num = 0;
				pType = 8;
				plFlavor = 64;
				pVal = DBNull.Value;
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00020F04 File Offset: 0x0001F104
		public int Put_(string wszName, int lFlags, ref object pVal, int Type)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.Put_f(5, this.pWbemClassObject, wszName, lFlags, ref pVal, Type);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00020F4C File Offset: 0x0001F14C
		public int Delete_(string wszName)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.Delete_f(6, this.pWbemClassObject, wszName);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00020F90 File Offset: 0x0001F190
		public int GetNames_(string wszQualifierName, int lFlags, ref object pQualifierVal, out string[] pNames)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.GetNames_f(7, this.pWbemClassObject, wszQualifierName, lFlags, ref pQualifierVal, out pNames);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		public int BeginEnumeration_(int lEnumFlags)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.BeginEnumeration_f(8, this.pWbemClassObject, lEnumFlags);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0002101C File Offset: 0x0001F21C
		public int Next_(int lFlags, ref string strName, ref object pVal, ref int pType, ref int plFlavor)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			pVal = null;
			strName = null;
			int num = WmiNetUtilsHelper.Next_f(9, this.pWbemClassObject, lFlags, ref strName, ref pVal, ref pType, ref plFlavor);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00021070 File Offset: 0x0001F270
		public int EndEnumeration_()
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.EndEnumeration_f(10, this.pWbemClassObject);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000210B4 File Offset: 0x0001F2B4
		public int GetPropertyQualifierSet_(string wszProperty, out IWbemQualifierSetFreeThreaded ppQualSet)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.GetPropertyQualifierSet_f(11, this.pWbemClassObject, wszProperty, out intPtr);
			if (num < 0)
			{
				ppQualSet = null;
			}
			else
			{
				ppQualSet = new IWbemQualifierSetFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0002110C File Offset: 0x0001F30C
		public int Clone_(out IWbemClassObjectFreeThreaded ppCopy)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.Clone_f(12, this.pWbemClassObject, out intPtr);
			if (num < 0)
			{
				ppCopy = null;
			}
			else
			{
				ppCopy = new IWbemClassObjectFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00021164 File Offset: 0x0001F364
		public int GetObjectText_(int lFlags, out string pstrObjectText)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.GetObjectText_f(13, this.pWbemClassObject, lFlags, out pstrObjectText);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000211AC File Offset: 0x0001F3AC
		public int SpawnDerivedClass_(int lFlags, out IWbemClassObjectFreeThreaded ppNewClass)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.SpawnDerivedClass_f(14, this.pWbemClassObject, lFlags, out intPtr);
			if (num < 0)
			{
				ppNewClass = null;
			}
			else
			{
				ppNewClass = new IWbemClassObjectFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00021204 File Offset: 0x0001F404
		public int SpawnInstance_(int lFlags, out IWbemClassObjectFreeThreaded ppNewInstance)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.SpawnInstance_f(15, this.pWbemClassObject, lFlags, out intPtr);
			if (num < 0)
			{
				ppNewInstance = null;
			}
			else
			{
				ppNewInstance = new IWbemClassObjectFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0002125C File Offset: 0x0001F45C
		public int CompareTo_(int lFlags, IWbemClassObjectFreeThreaded pCompareTo)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.CompareTo_f(16, this.pWbemClassObject, lFlags, pCompareTo.pWbemClassObject);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000212A8 File Offset: 0x0001F4A8
		public int GetPropertyOrigin_(string wszName, out string pstrClassName)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.GetPropertyOrigin_f(17, this.pWbemClassObject, wszName, out pstrClassName);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000212F0 File Offset: 0x0001F4F0
		public int InheritsFrom_(string strAncestor)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.InheritsFrom_f(18, this.pWbemClassObject, strAncestor);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00021338 File Offset: 0x0001F538
		public int GetMethod_(string wszName, int lFlags, out IWbemClassObjectFreeThreaded ppInSignature, out IWbemClassObjectFreeThreaded ppOutSignature)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			int num = WmiNetUtilsHelper.GetMethod_f(19, this.pWbemClassObject, wszName, lFlags, out intPtr, out intPtr2);
			ppInSignature = null;
			ppOutSignature = null;
			if (num >= 0)
			{
				if (intPtr != IntPtr.Zero)
				{
					ppInSignature = new IWbemClassObjectFreeThreaded(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					ppOutSignature = new IWbemClassObjectFreeThreaded(intPtr2);
				}
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000213B8 File Offset: 0x0001F5B8
		public int PutMethod_(string wszName, int lFlags, IWbemClassObjectFreeThreaded pInSignature, IWbemClassObjectFreeThreaded pOutSignature)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.PutMethod_f(20, this.pWbemClassObject, wszName, lFlags, pInSignature, pOutSignature);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0002140C File Offset: 0x0001F60C
		public int DeleteMethod_(string wszName)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.DeleteMethod_f(21, this.pWbemClassObject, wszName);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00021454 File Offset: 0x0001F654
		public int BeginMethodEnumeration_(int lEnumFlags)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.BeginMethodEnumeration_f(22, this.pWbemClassObject, lEnumFlags);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0002149C File Offset: 0x0001F69C
		public int NextMethod_(int lFlags, out string pstrName, out IWbemClassObjectFreeThreaded ppInSignature, out IWbemClassObjectFreeThreaded ppOutSignature)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			int num = WmiNetUtilsHelper.NextMethod_f(23, this.pWbemClassObject, lFlags, out pstrName, out intPtr, out intPtr2);
			ppInSignature = null;
			ppOutSignature = null;
			if (num >= 0)
			{
				if (intPtr != IntPtr.Zero)
				{
					ppInSignature = new IWbemClassObjectFreeThreaded(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					ppOutSignature = new IWbemClassObjectFreeThreaded(intPtr2);
				}
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0002151C File Offset: 0x0001F71C
		public int EndMethodEnumeration_()
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.EndMethodEnumeration_f(24, this.pWbemClassObject);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00021560 File Offset: 0x0001F760
		public int GetMethodQualifierSet_(string wszMethod, out IWbemQualifierSetFreeThreaded ppQualSet)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			IntPtr intPtr;
			int num = WmiNetUtilsHelper.GetMethodQualifierSet_f(25, this.pWbemClassObject, wszMethod, out intPtr);
			if (num < 0)
			{
				ppQualSet = null;
			}
			else
			{
				ppQualSet = new IWbemQualifierSetFreeThreaded(intPtr);
			}
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public int GetMethodOrigin_(string wszMethodName, out string pstrClassName)
		{
			if (this.pWbemClassObject == IntPtr.Zero)
			{
				throw new ObjectDisposedException(IWbemClassObjectFreeThreaded.name);
			}
			int num = WmiNetUtilsHelper.GetMethodOrigin_f(26, this.pWbemClassObject, wszMethodName, out pstrClassName);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x06000373 RID: 883
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern IStream CreateStreamOnHGlobal(IntPtr hGlobal, int fDeleteOnRelease);

		// Token: 0x06000374 RID: 884
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern IntPtr GetHGlobalFromStream([In] IStream pstm);

		// Token: 0x06000375 RID: 885
		[DllImport("kernel32.dll")]
		private static extern IntPtr GlobalLock([In] IntPtr hGlobal);

		// Token: 0x06000376 RID: 886
		[DllImport("kernel32.dll")]
		private static extern int GlobalUnlock([In] IntPtr pData);

		// Token: 0x06000377 RID: 887
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void CoMarshalInterface([In] IStream pStm, [In] ref Guid riid, [In] IntPtr Unk, [In] uint dwDestContext, [In] IntPtr pvDestContext, [In] uint mshlflags);

		// Token: 0x06000378 RID: 888
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern IntPtr CoUnmarshalInterface([In] IStream pStm, [In] ref Guid riid);

		// Token: 0x04000244 RID: 580
		private const string SerializationBlobName = "flatWbemClassObject";

		// Token: 0x04000245 RID: 581
		private static readonly string name = typeof(IWbemClassObjectFreeThreaded).FullName;

		// Token: 0x04000246 RID: 582
		public static Guid IID_IWbemClassObject = new Guid("DC12A681-737F-11CF-884D-00AA004B2E24");

		// Token: 0x04000247 RID: 583
		private IntPtr pWbemClassObject = IntPtr.Zero;

		// Token: 0x020000FF RID: 255
		private enum STATFLAG
		{
			// Token: 0x04000555 RID: 1365
			STATFLAG_DEFAULT,
			// Token: 0x04000556 RID: 1366
			STATFLAG_NONAME
		}

		// Token: 0x02000100 RID: 256
		private enum MSHCTX
		{
			// Token: 0x04000558 RID: 1368
			MSHCTX_LOCAL,
			// Token: 0x04000559 RID: 1369
			MSHCTX_NOSHAREDMEM,
			// Token: 0x0400055A RID: 1370
			MSHCTX_DIFFERENTMACHINE,
			// Token: 0x0400055B RID: 1371
			MSHCTX_INPROC
		}

		// Token: 0x02000101 RID: 257
		private enum MSHLFLAGS
		{
			// Token: 0x0400055D RID: 1373
			MSHLFLAGS_NORMAL,
			// Token: 0x0400055E RID: 1374
			MSHLFLAGS_TABLESTRONG,
			// Token: 0x0400055F RID: 1375
			MSHLFLAGS_TABLEWEAK,
			// Token: 0x04000560 RID: 1376
			MSHLFLAGS_NOPING
		}
	}
}
