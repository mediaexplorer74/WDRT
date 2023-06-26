using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000262 RID: 610
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("70b51430-b6ca-11d0-b9b9-00a0c922e750")]
	[ComImport]
	internal interface IMSAdminBase
	{
		// Token: 0x060016F1 RID: 5873
		[PreserveSig]
		int AddKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x060016F2 RID: 5874
		[PreserveSig]
		int DeleteKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x060016F3 RID: 5875
		void DeleteChildKeys(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x060016F4 RID: 5876
		[PreserveSig]
		int EnumKeys(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, StringBuilder Buffer, int EnumKeyIndex);

		// Token: 0x060016F5 RID: 5877
		void CopyKey(IntPtr source, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, IntPtr dest, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, bool OverwriteFlag, bool CopyFlag);

		// Token: 0x060016F6 RID: 5878
		void RenameKey(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.LPWStr)] string newName);

		// Token: 0x060016F7 RID: 5879
		[PreserveSig]
		int SetData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data);

		// Token: 0x060016F8 RID: 5880
		[PreserveSig]
		int GetData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data, [In] [Out] ref uint RequiredDataLen);

		// Token: 0x060016F9 RID: 5881
		[PreserveSig]
		int DeleteData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, uint Identifier, uint DataType);

		// Token: 0x060016FA RID: 5882
		[PreserveSig]
		int EnumData(IntPtr key, [MarshalAs(UnmanagedType.LPWStr)] string path, ref MetadataRecord data, int EnumDataIndex, [In] [Out] ref uint RequiredDataLen);

		// Token: 0x060016FB RID: 5883
		[PreserveSig]
		int GetAllData(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, uint Attributes, uint UserType, uint DataType, [In] [Out] ref uint NumDataEntries, [In] [Out] ref uint DataSetNumber, uint BufferSize, IntPtr buffer, [In] [Out] ref uint RequiredBufferSize);

		// Token: 0x060016FC RID: 5884
		void DeleteAllData(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, uint UserType, uint DataType);

		// Token: 0x060016FD RID: 5885
		[PreserveSig]
		int CopyData(IntPtr sourcehandle, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, IntPtr desthandle, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, int Attributes, int UserType, int DataType, [MarshalAs(UnmanagedType.Bool)] bool CopyFlag);

		// Token: 0x060016FE RID: 5886
		[PreserveSig]
		void GetDataPaths(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, int Identifier, int DataType, int BufferSize, [MarshalAs(UnmanagedType.LPWStr)] out char[] Buffer, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int RequiredBufferSize);

		// Token: 0x060016FF RID: 5887
		[PreserveSig]
		int OpenKey(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested, int TimeOut, [In] [Out] ref IntPtr NewHandle);

		// Token: 0x06001700 RID: 5888
		[PreserveSig]
		int CloseKey(IntPtr handle);

		// Token: 0x06001701 RID: 5889
		void ChangePermissions(IntPtr handle, int TimeOut, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested);

		// Token: 0x06001702 RID: 5890
		void SaveData();

		// Token: 0x06001703 RID: 5891
		[PreserveSig]
		void GetHandleInfo(IntPtr handle, [In] [Out] ref _METADATA_HANDLE_INFO Info);

		// Token: 0x06001704 RID: 5892
		[PreserveSig]
		void GetSystemChangeNumber([MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint SystemChangeNumber);

		// Token: 0x06001705 RID: 5893
		[PreserveSig]
		void GetDataSetNumber(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [In] [Out] ref uint DataSetNumber);

		// Token: 0x06001706 RID: 5894
		[PreserveSig]
		void SetLastChangeTime(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);

		// Token: 0x06001707 RID: 5895
		[PreserveSig]
		int GetLastChangeTime(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [In] [Out] ref System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);

		// Token: 0x06001708 RID: 5896
		[PreserveSig]
		int KeyExchangePhase1();

		// Token: 0x06001709 RID: 5897
		[PreserveSig]
		int KeyExchangePhase2();

		// Token: 0x0600170A RID: 5898
		[PreserveSig]
		int Backup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x0600170B RID: 5899
		[PreserveSig]
		int Restore([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x0600170C RID: 5900
		[PreserveSig]
		void EnumBackups([MarshalAs(UnmanagedType.LPWStr)] out string Location, [MarshalAs(UnmanagedType.U4)] out uint Version, out System.Runtime.InteropServices.ComTypes.FILETIME BackupTime, uint EnumIndex);

		// Token: 0x0600170D RID: 5901
		[PreserveSig]
		void DeleteBackup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version);

		// Token: 0x0600170E RID: 5902
		[PreserveSig]
		int UnmarshalInterface([MarshalAs(UnmanagedType.Interface)] out IMSAdminBase interf);

		// Token: 0x0600170F RID: 5903
		[PreserveSig]
		int GetServerGuid();
	}
}
