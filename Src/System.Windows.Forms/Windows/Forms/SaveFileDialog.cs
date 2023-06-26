using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a location for saving a file. This class cannot be inherited.</summary>
	// Token: 0x02000351 RID: 849
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionSaveFileDialog")]
	public sealed class SaveFileDialog : FileDialog
	{
		/// <summary>Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box prompts the user before creating a file if the user specifies a file name that does not exist; <see langword="false" /> if the dialog box automatically creates the new file without prompting the user for permission. The default value is <see langword="false" />.</returns>
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000F7808 File Offset: 0x000F5A08
		// (set) Token: 0x06003784 RID: 14212 RVA: 0x000F7815 File Offset: 0x000F5A15
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("SaveFileDialogCreatePrompt")]
		public bool CreatePrompt
		{
			get
			{
				return base.GetOption(8192);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				base.SetOption(8192, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see langword="Save As" /> dialog box displays a warning if the user specifies a file name that already exists.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box prompts the user before overwriting an existing file if the user specifies a file name that already exists; <see langword="false" /> if the dialog box automatically overwrites the existing file without prompting the user for permission. The default value is <see langword="true" />.</returns>
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000F782D File Offset: 0x000F5A2D
		// (set) Token: 0x06003786 RID: 14214 RVA: 0x000F7836 File Offset: 0x000F5A36
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("SaveFileDialogOverWritePrompt")]
		public bool OverwritePrompt
		{
			get
			{
				return base.GetOption(2);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				base.SetOption(2, value);
			}
		}

		/// <summary>Opens the file with read/write permission selected by the user.</summary>
		/// <returns>The read/write file selected by the user.</returns>
		// Token: 0x06003787 RID: 14215 RVA: 0x000F784C File Offset: 0x000F5A4C
		public Stream OpenFile()
		{
			IntSecurity.FileDialogSaveFile.Demand();
			string text = base.FileNamesInternal[0];
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("FileName");
			}
			Stream stream = null;
			new FileIOPermission(FileIOPermissionAccess.AllAccess, IntSecurity.UnsafeGetFullPath(text)).Assert();
			try
			{
				stream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return stream;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000F78B8 File Offset: 0x000F5AB8
		private bool PromptFileCreate(string fileName)
		{
			return base.MessageBoxWithFocusRestore(SR.GetString("FileDialogCreatePrompt", new object[] { fileName }), base.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000F78DD File Offset: 0x000F5ADD
		private bool PromptFileOverwrite(string fileName)
		{
			return base.MessageBoxWithFocusRestore(SR.GetString("FileDialogOverwritePrompt", new object[] { fileName }), base.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000F7904 File Offset: 0x000F5B04
		internal override bool PromptUserIfAppropriate(string fileName)
		{
			return base.PromptUserIfAppropriate(fileName) && ((this.options & 2) == 0 || !FileDialog.FileExists(fileName) || base.UseVistaDialogInternal || this.PromptFileOverwrite(fileName)) && ((this.options & 8192) == 0 || FileDialog.FileExists(fileName) || this.PromptFileCreate(fileName));
		}

		/// <summary>Resets all dialog box options to their default values.</summary>
		// Token: 0x0600378B RID: 14219 RVA: 0x000F7963 File Offset: 0x000F5B63
		public override void Reset()
		{
			base.Reset();
			base.SetOption(2, true);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000F7973 File Offset: 0x000F5B73
		internal override void EnsureFileDialogPermission()
		{
			IntSecurity.FileDialogSaveFile.Demand();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000F7980 File Offset: 0x000F5B80
		internal override bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn)
		{
			IntSecurity.FileDialogSaveFile.Demand();
			bool saveFileName = UnsafeNativeMethods.GetSaveFileName(ofn);
			if (!saveFileName)
			{
				int num = SafeNativeMethods.CommDlgExtendedError();
				if (num == 12290)
				{
					throw new InvalidOperationException(SR.GetString("FileDialogInvalidFileName", new object[] { base.FileName }));
				}
			}
			return saveFileName;
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000F79D0 File Offset: 0x000F5BD0
		internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.IFileSaveDialog fileSaveDialog = (FileDialogNative.IFileSaveDialog)dialog;
			FileDialogNative.IShellItem shellItem;
			dialog.GetResult(out shellItem);
			return new string[] { FileDialog.GetFilePathFromShellItem(shellItem) };
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000F79FB File Offset: 0x000F5BFB
		internal override FileDialogNative.IFileDialog CreateVistaDialog()
		{
			return (FileDialogNative.NativeFileSaveDialog)new FileDialogNative.FileSaveDialogRCW();
		}
	}
}
