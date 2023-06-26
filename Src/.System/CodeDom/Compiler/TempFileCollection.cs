using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of temporary files.</summary>
	// Token: 0x02000684 RID: 1668
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class TempFileCollection : ICollection, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class with default values.</summary>
		// Token: 0x06003D62 RID: 15714 RVA: 0x000FB9C9 File Offset: 0x000F9BC9
		public TempFileCollection()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory that is set to delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files.</param>
		// Token: 0x06003D63 RID: 15715 RVA: 0x000FB9D3 File Offset: 0x000F9BD3
		public TempFileCollection(string tempDir)
			: this(tempDir, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory and specified value indicating whether to keep or delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files.</param>
		/// <param name="keepFiles">
		///   <see langword="true" /> if the temporary files should be kept after use; <see langword="false" /> if the temporary files should be deleted.</param>
		// Token: 0x06003D64 RID: 15716 RVA: 0x000FB9E0 File Offset: 0x000F9BE0
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.keepFiles = keepFiles;
			this.tempDir = tempDir;
			this.files = new Hashtable(StringComparer.OrdinalIgnoreCase);
			WindowsImpersonationContext windowsImpersonationContext = Executor.RevertImpersonation();
			try
			{
				this.currentIdentity = WindowsIdentity.GetCurrent();
			}
			finally
			{
				Executor.ReImpersonate(windowsImpersonationContext);
			}
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		// Token: 0x06003D65 RID: 15717 RVA: 0x000FBA3C File Offset: 0x000F9C3C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003D66 RID: 15718 RVA: 0x000FBA4B File Offset: 0x000F9C4B
		protected virtual void Dispose(bool disposing)
		{
			this.Delete();
			this.DeleteHighIntegrityDirectory();
		}

		/// <summary>Attempts to delete the temporary files before this object is reclaimed by garbage collection.</summary>
		// Token: 0x06003D67 RID: 15719 RVA: 0x000FBA5C File Offset: 0x000F9C5C
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection.</summary>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection.</param>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		// Token: 0x06003D68 RID: 15720 RVA: 0x000FBA8C File Offset: 0x000F9C8C
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.keepFiles);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection, using the specified value indicating whether the file should be deleted or retained.</summary>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection.</param>
		/// <param name="keepFile">
		///   <see langword="true" /> if the file should be kept after use; <see langword="false" /> if the file should be deleted.</param>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileExtension" /> is <see langword="null" /> or an empty string.</exception>
		// Token: 0x06003D69 RID: 15721 RVA: 0x000FBA9C File Offset: 0x000F9C9C
		public string AddExtension(string fileExtension, bool keepFile)
		{
			if (fileExtension == null || fileExtension.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "fileExtension" }), "fileExtension");
			}
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		/// <summary>Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called.</summary>
		/// <param name="fileName">The name of the file to add to the collection.</param>
		/// <param name="keepFile">
		///   <see langword="true" /> if the file should be kept after use; <see langword="false" /> if the file should be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is <see langword="null" /> or an empty string.  
		/// -or-  
		/// <paramref name="fileName" /> is a duplicate.</exception>
		// Token: 0x06003D6A RID: 15722 RVA: 0x000FBAF4 File Offset: 0x000F9CF4
		public void AddFile(string fileName, bool keepFile)
		{
			if (fileName == null || fileName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "fileName" }), "fileName");
			}
			if (this.files[fileName] != null)
			{
				throw new ArgumentException(SR.GetString("DuplicateFileName", new object[] { fileName }), "fileName");
			}
			this.files.Add(fileName, keepFile);
		}

		/// <summary>Gets an enumerator that can enumerate the members of the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x06003D6B RID: 15723 RVA: 0x000FBB6E File Offset: 0x000F9D6E
		public IEnumerator GetEnumerator()
		{
			return this.files.Keys.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003D6C RID: 15724 RVA: 0x000FBB80 File Offset: 0x000F9D80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.files.Keys.GetEnumerator();
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="start">The zero-based index in array at which copying begins.</param>
		// Token: 0x06003D6D RID: 15725 RVA: 0x000FBB92 File Offset: 0x000F9D92
		void ICollection.CopyTo(Array array, int start)
		{
			this.files.Keys.CopyTo(array, start);
		}

		/// <summary>Copies the members of the collection to the specified string, beginning at the specified index.</summary>
		/// <param name="fileNames">The array of strings to copy to.</param>
		/// <param name="start">The index of the array to begin copying to.</param>
		// Token: 0x06003D6E RID: 15726 RVA: 0x000FBBA6 File Offset: 0x000F9DA6
		public void CopyTo(string[] fileNames, int start)
		{
			this.files.Keys.CopyTo(fileNames, start);
		}

		/// <summary>Gets the number of files in the collection.</summary>
		/// <returns>The number of files in the collection.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000FBBBA File Offset: 0x000F9DBA
		public int Count
		{
			get
			{
				return this.files.Count;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000FBBC7 File Offset: 0x000F9DC7
		int ICollection.Count
		{
			get
			{
				return this.files.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000FBBD4 File Offset: 0x000F9DD4
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000FBBD7 File Offset: 0x000F9DD7
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the temporary directory to store the temporary files in.</summary>
		/// <returns>The temporary directory to store the temporary files in.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x000FBBDA File Offset: 0x000F9DDA
		public string TempDir
		{
			get
			{
				if (this.tempDir != null)
				{
					return this.tempDir;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</summary>
		/// <returns>The full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</returns>
		/// <exception cref="T:System.Security.SecurityException">If the <see cref="P:System.CodeDom.Compiler.TempFileCollection.BasePath" /> property has not been set or is set to <see langword="null" />, and <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" /> is not granted for the temporary directory indicated by the <see cref="P:System.CodeDom.Compiler.TempFileCollection.TempDir" /> property.</exception>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x000FBBF0 File Offset: 0x000F9DF0
		public string BasePath
		{
			get
			{
				this.EnsureTempNameCreated();
				return this.basePath;
			}
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000FBC00 File Offset: 0x000F9E00
		private void EnsureTempNameCreated()
		{
			if (this.basePath == null)
			{
				string text = null;
				bool flag = false;
				int num = 5000;
				do
				{
					try
					{
						this.basePath = this.GetTempFileName(this.TempDir);
						string fullPath = Path.GetFullPath(this.basePath);
						new FileIOPermission(FileIOPermissionAccess.AllAccess, fullPath).Demand();
						text = this.basePath + ".tmp";
						using (new FileStream(text, FileMode.CreateNew, FileAccess.Write))
						{
						}
						flag = true;
					}
					catch (IOException ex)
					{
						num--;
						uint num2 = 2147942480U;
						if (num == 0 || (long)Marshal.GetHRForException(ex) != (long)((ulong)num2))
						{
							throw;
						}
						flag = false;
					}
				}
				while (!flag);
				this.files.Add(text, this.keepFiles);
			}
		}

		/// <summary>Gets or sets a value indicating whether to keep the files, by default, when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called or the collection is disposed.</summary>
		/// <returns>
		///   <see langword="true" /> if the files should be kept; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x000FBCDC File Offset: 0x000F9EDC
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x000FBCE4 File Offset: 0x000F9EE4
		public bool KeepFiles
		{
			get
			{
				return this.keepFiles;
			}
			set
			{
				this.keepFiles = value;
			}
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000FBCF0 File Offset: 0x000F9EF0
		private bool KeepFile(string fileName)
		{
			object obj = this.files[fileName];
			return obj != null && (bool)obj;
		}

		/// <summary>Deletes the temporary files within this collection that were not marked to be kept.</summary>
		// Token: 0x06003D79 RID: 15737 RVA: 0x000FBD18 File Offset: 0x000F9F18
		public void Delete()
		{
			if (this.files != null && this.files.Count > 0)
			{
				string[] array = new string[this.files.Count];
				this.files.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!this.KeepFile(text))
					{
						this.Delete(text);
						this.files.Remove(text);
					}
				}
			}
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000FBD90 File Offset: 0x000F9F90
		private void DeleteHighIntegrityDirectory()
		{
			try
			{
				if (this.currentIdentity != null && Directory.Exists(this.highIntegrityDirectory))
				{
					TempFileCollection.RemoveAceOnTempDirectory(this.highIntegrityDirectory, this.currentIdentity.User.ToString());
					if (Directory.GetFiles(this.highIntegrityDirectory).Length == 0)
					{
						Directory.Delete(this.highIntegrityDirectory, true);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000FBDFC File Offset: 0x000F9FFC
		internal void SafeDelete()
		{
			WindowsImpersonationContext windowsImpersonationContext = Executor.RevertImpersonation();
			try
			{
				this.Delete();
			}
			finally
			{
				Executor.ReImpersonate(windowsImpersonationContext);
			}
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000FBE30 File Offset: 0x000FA030
		private void Delete(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000FBE58 File Offset: 0x000FA058
		private string GetTempFileName(string tempDir)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
			if (string.IsNullOrEmpty(tempDir))
			{
				tempDir = Path.GetTempPath();
				if (!LocalAppContextSwitches.DisableTempFileCollectionDirectoryFeature && this.currentIdentity != null && new WindowsPrincipal(this.currentIdentity).IsInRole(WindowsBuiltInRole.Administrator))
				{
					tempDir = Path.Combine(tempDir, fileNameWithoutExtension);
					TempFileCollection.CreateTempDirectoryWithAce(tempDir, this.currentIdentity.User.ToString());
					this.highIntegrityDirectory = tempDir;
				}
			}
			string text;
			if (tempDir.EndsWith("\\", StringComparison.Ordinal))
			{
				text = tempDir + fileNameWithoutExtension;
			}
			else
			{
				text = tempDir + "\\" + fileNameWithoutExtension;
			}
			return text;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000FBEF4 File Offset: 0x000FA0F4
		private static void CreateTempDirectoryWithAce(string directory, string identity)
		{
			string text = "D:(D;OI;SD;;;" + identity + ")(A;OICI;FA;;;BA)S:(ML;OI;NW;;;HI)";
			SafeLocalMemHandle safeLocalMemHandle = null;
			SafeLocalMemHandle.ConvertStringSecurityDescriptorToSecurityDescriptor(text, 1, out safeLocalMemHandle, IntPtr.Zero);
			Microsoft.Win32.NativeMethods.CreateDirectory(directory, safeLocalMemHandle);
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000FBF2C File Offset: 0x000FA12C
		private static void RemoveAceOnTempDirectory(string directory, string identity)
		{
			string text = "D:(A;OICI;FA;;;" + identity + ")(A;OICI;FA;;;BA)";
			SafeLocalMemHandle safeLocalMemHandle = null;
			SafeLocalMemHandle.ConvertStringSecurityDescriptorToSecurityDescriptor(text, 1, out safeLocalMemHandle, IntPtr.Zero);
			Microsoft.Win32.NativeMethods.SetNamedSecurityInfo(directory, safeLocalMemHandle);
		}

		// Token: 0x04002CB2 RID: 11442
		private string basePath;

		// Token: 0x04002CB3 RID: 11443
		private string tempDir;

		// Token: 0x04002CB4 RID: 11444
		private bool keepFiles;

		// Token: 0x04002CB5 RID: 11445
		private Hashtable files;

		// Token: 0x04002CB6 RID: 11446
		[NonSerialized]
		private WindowsIdentity currentIdentity;

		// Token: 0x04002CB7 RID: 11447
		[NonSerialized]
		private string highIntegrityDirectory;
	}
}
