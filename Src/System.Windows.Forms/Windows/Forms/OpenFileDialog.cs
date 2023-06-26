using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Displays a standard dialog box that prompts the user to open a file. This class cannot be inherited.</summary>
	// Token: 0x02000310 RID: 784
	[SRDescription("DescriptionOpenFileDialog")]
	public sealed class OpenFileDialog : FileDialog
	{
		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays a warning when the user specifies a file name that does not exist; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000E1383 File Offset: 0x000DF583
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000E138B File Offset: 0x000DF58B
		[DefaultValue(true)]
		[SRDescription("OFDcheckFileExistsDescr")]
		public override bool CheckFileExists
		{
			get
			{
				return base.CheckFileExists;
			}
			set
			{
				base.CheckFileExists = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows multiple files to be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box allows multiple files to be selected together or concurrently; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000E1394 File Offset: 0x000DF594
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000E13A1 File Offset: 0x000DF5A1
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDmultiSelectDescr")]
		public bool Multiselect
		{
			get
			{
				return base.GetOption(512);
			}
			set
			{
				base.SetOption(512, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the read-only check box is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the read-only check box is selected; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000E13AF File Offset: 0x000DF5AF
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000E13B8 File Offset: 0x000DF5B8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDreadOnlyCheckedDescr")]
		public bool ReadOnlyChecked
		{
			get
			{
				return base.GetOption(1);
			}
			set
			{
				base.SetOption(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains a read-only check box.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box contains a read-only check box; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000E13C2 File Offset: 0x000DF5C2
		// (set) Token: 0x0600320C RID: 12812 RVA: 0x000E13CE File Offset: 0x000DF5CE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("OFDshowReadOnlyDescr")]
		public bool ShowReadOnly
		{
			get
			{
				return !base.GetOption(4);
			}
			set
			{
				base.SetOption(4, !value);
			}
		}

		/// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
		/// <exception cref="T:System.ArgumentNullException">The file name is <see langword="null" />.</exception>
		// Token: 0x0600320D RID: 12813 RVA: 0x000E13DC File Offset: 0x000DF5DC
		public Stream OpenFile()
		{
			IntSecurity.FileDialogOpenFile.Demand();
			string text = base.FileNamesInternal[0];
			if (text == null || text.Length == 0)
			{
				throw new ArgumentNullException("FileName");
			}
			Stream stream = null;
			new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(text)).Assert();
			try
			{
				stream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return stream;
		}

		/// <summary>Resets all properties to their default values.</summary>
		// Token: 0x0600320E RID: 12814 RVA: 0x000E1448 File Offset: 0x000DF648
		public override void Reset()
		{
			base.Reset();
			base.SetOption(4096, true);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000E145C File Offset: 0x000DF65C
		internal override void EnsureFileDialogPermission()
		{
			IntSecurity.FileDialogOpenFile.Demand();
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000E1468 File Offset: 0x000DF668
		internal override bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn)
		{
			IntSecurity.FileDialogOpenFile.Demand();
			bool openFileName = UnsafeNativeMethods.GetOpenFileName(ofn);
			if (!openFileName)
			{
				switch (SafeNativeMethods.CommDlgExtendedError())
				{
				case 12289:
					throw new InvalidOperationException(SR.GetString("FileDialogSubLassFailure"));
				case 12290:
					throw new InvalidOperationException(SR.GetString("FileDialogInvalidFileName", new object[] { base.FileName }));
				case 12291:
					throw new InvalidOperationException(SR.GetString("FileDialogBufferTooSmall"));
				}
			}
			return openFileName;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000E14EC File Offset: 0x000DF6EC
		internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.IFileOpenDialog fileOpenDialog = (FileDialogNative.IFileOpenDialog)dialog;
			if (this.Multiselect)
			{
				FileDialogNative.IShellItemArray shellItemArray;
				fileOpenDialog.GetResults(out shellItemArray);
				uint num;
				shellItemArray.GetCount(out num);
				string[] array = new string[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					FileDialogNative.IShellItem shellItem;
					shellItemArray.GetItemAt(num2, out shellItem);
					array[(int)num2] = FileDialog.GetFilePathFromShellItem(shellItem);
				}
				return array;
			}
			FileDialogNative.IShellItem shellItem2;
			fileOpenDialog.GetResult(out shellItem2);
			return new string[] { FileDialog.GetFilePathFromShellItem(shellItem2) };
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000E155E File Offset: 0x000DF75E
		internal override FileDialogNative.IFileDialog CreateVistaDialog()
		{
			return (FileDialogNative.NativeFileOpenDialog)new FileDialogNative.FileOpenDialogRCW();
		}

		/// <summary>Gets the file name and extension for the file selected in the dialog box. The file name does not include the path.</summary>
		/// <returns>The file name and extension for the file selected in the dialog box. The file name does not include the path. The default value is an empty string.</returns>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000E156C File Offset: 0x000DF76C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SafeFileName
		{
			get
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string fileName = base.FileName;
				CodeAccessPermission.RevertAssert();
				if (string.IsNullOrEmpty(fileName))
				{
					return "";
				}
				return OpenFileDialog.RemoveSensitivePathInformation(fileName);
			}
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000E15A6 File Offset: 0x000DF7A6
		private static string RemoveSensitivePathInformation(string fullPath)
		{
			return Path.GetFileName(fullPath);
		}

		/// <summary>Gets an array of file names and extensions for all the selected files in the dialog box. The file names do not include the path.</summary>
		/// <returns>An array of file names and extensions for all the selected files in the dialog box. The file names do not include the path. If no files are selected, an empty array is returned.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000E15B0 File Offset: 0x000DF7B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] SafeFileNames
		{
			get
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string[] fileNames = base.FileNames;
				CodeAccessPermission.RevertAssert();
				if (fileNames == null || fileNames.Length == 0)
				{
					return new string[0];
				}
				string[] array = new string[fileNames.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = OpenFileDialog.RemoveSensitivePathInformation(fileNames[i]);
				}
				return array;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000E1605 File Offset: 0x000DF805
		internal override bool SettingsSupportVistaDialog
		{
			get
			{
				return base.SettingsSupportVistaDialog && !this.ShowReadOnly;
			}
		}
	}
}
