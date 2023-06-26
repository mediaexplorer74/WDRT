using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of Windows Vista custom places for the <see cref="T:System.Windows.Forms.FileDialog" /> class.</summary>
	// Token: 0x0200024F RID: 591
	public class FileDialogCustomPlacesCollection : Collection<FileDialogCustomPlace>
	{
		// Token: 0x060025A3 RID: 9635 RVA: 0x000AF680 File Offset: 0x000AD880
		internal void Apply(FileDialogNative.IFileDialog dialog)
		{
			for (int i = base.Items.Count - 1; i >= 0; i--)
			{
				FileDialogCustomPlace fileDialogCustomPlace = base.Items[i];
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fileDialogCustomPlace.Path);
				fileIOPermission.Demand();
				try
				{
					FileDialogNative.IShellItem nativePath = fileDialogCustomPlace.GetNativePath();
					if (nativePath != null)
					{
						dialog.AddPlace(nativePath, 0);
					}
				}
				catch (FileNotFoundException)
				{
				}
			}
		}

		/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
		/// <param name="path">A folder path to the custom place.</param>
		// Token: 0x060025A4 RID: 9636 RVA: 0x000AF6EC File Offset: 0x000AD8EC
		public void Add(string path)
		{
			base.Add(new FileDialogCustomPlace(path));
		}

		/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
		/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder.</param>
		// Token: 0x060025A5 RID: 9637 RVA: 0x000AF6FA File Offset: 0x000AD8FA
		public void Add(Guid knownFolderGuid)
		{
			base.Add(new FileDialogCustomPlace(knownFolderGuid));
		}
	}
}
