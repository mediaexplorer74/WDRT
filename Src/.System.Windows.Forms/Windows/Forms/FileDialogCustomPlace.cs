using System;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents an entry in a <see cref="T:System.Windows.Forms.FileDialog" /> custom place collection for Windows Vista.</summary>
	// Token: 0x0200024E RID: 590
	public class FileDialogCustomPlace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class. with a specified folder path to a custom place.</summary>
		/// <param name="path">A folder path to the custom place.</param>
		// Token: 0x0600259A RID: 9626 RVA: 0x000AF528 File Offset: 0x000AD728
		public FileDialogCustomPlace(string path)
		{
			this.Path = path;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class with a custom place identified by a Windows Vista Known Folder GUID.</summary>
		/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder.</param>
		// Token: 0x0600259B RID: 9627 RVA: 0x000AF54D File Offset: 0x000AD74D
		public FileDialogCustomPlace(Guid knownFolderGuid)
		{
			this.KnownFolderGuid = knownFolderGuid;
		}

		/// <summary>Gets or sets the folder path to the custom place.</summary>
		/// <returns>A folder path to the custom place. If the custom place was specified with a Windows Vista Known Folder GUID, then an empty string is returned.</returns>
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000AF572 File Offset: 0x000AD772
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x000AF58D File Offset: 0x000AD78D
		public string Path
		{
			get
			{
				if (string.IsNullOrEmpty(this._path))
				{
					return string.Empty;
				}
				return this._path;
			}
			set
			{
				this._path = value ?? "";
				this._knownFolderGuid = Guid.Empty;
			}
		}

		/// <summary>Gets or sets the Windows Vista Known Folder GUID for the custom place.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that indicates the Windows Vista Known Folder for the custom place. If the custom place was specified with a folder path, then an empty GUID is returned. For a list of the available Windows Vista Known Folders, see Known Folder GUIDs for File Dialog Custom Places or the KnownFolders.h file in the Windows SDK.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x000AF5AA File Offset: 0x000AD7AA
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x000AF5B2 File Offset: 0x000AD7B2
		public Guid KnownFolderGuid
		{
			get
			{
				return this._knownFolderGuid;
			}
			set
			{
				this._path = string.Empty;
				this._knownFolderGuid = value;
			}
		}

		/// <summary>Returns a string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</summary>
		/// <returns>A string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</returns>
		// Token: 0x060025A0 RID: 9632 RVA: 0x000AF5C6 File Offset: 0x000AD7C6
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} Path: {1} KnownFolderGuid: {2}", new object[]
			{
				base.ToString(),
				this.Path,
				this.KnownFolderGuid
			});
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000AF600 File Offset: 0x000AD800
		internal FileDialogNative.IShellItem GetNativePath()
		{
			string text;
			if (!string.IsNullOrEmpty(this._path))
			{
				text = this._path;
			}
			else
			{
				text = FileDialogCustomPlace.GetFolderLocation(this._knownFolderGuid);
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return FileDialog.GetShellItemForPath(text);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000AF648 File Offset: 0x000AD848
		private static string GetFolderLocation(Guid folderGuid)
		{
			if (!UnsafeNativeMethods.IsVista)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (UnsafeNativeMethods.Shell32.SHGetFolderPathEx(ref folderGuid, 0U, IntPtr.Zero, stringBuilder) == 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x04000F95 RID: 3989
		private string _path = "";

		// Token: 0x04000F96 RID: 3990
		private Guid _knownFolderGuid = Guid.Empty;
	}
}
