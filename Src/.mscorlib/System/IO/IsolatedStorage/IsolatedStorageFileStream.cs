using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	/// <summary>Exposes a file within isolated storage.</summary>
	// Token: 0x020001B0 RID: 432
	[ComVisible(true)]
	public class IsolatedStorageFileStream : FileStream
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x0005C00C File Offset: 0x0005A20C
		private IsolatedStorageFileStream()
		{
		}

		/// <summary>Initializes a new instance of an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object giving access to the file designated by <paramref name="path" /> in the specified <paramref name="mode" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" /></exception>
		// Token: 0x06001B28 RID: 6952 RVA: 0x0005C014 File Offset: 0x0005A214
		public IsolatedStorageFileStream(string path, FileMode mode)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, and in the context of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> specified by <paramref name="isf" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="isf">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> in which to open the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
		///   <paramref name="isf" /> does not have a quota.</exception>
		// Token: 0x06001B29 RID: 6953 RVA: 0x0005C028 File Offset: 0x0005A228
		public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, isf)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, with the kind of <paramref name="access" /> requested.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		// Token: 0x06001B2A RID: 6954 RVA: 0x0005C03C File Offset: 0x0005A23C
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access)
			: this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" /> in the specified <paramref name="mode" />, with the specified file <paramref name="access" />, and in the context of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> specified by <paramref name="isf" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <param name="isf">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> in which to open the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
		///   <paramref name="isf" /> does not have a quota.</exception>
		// Token: 0x06001B2B RID: 6955 RVA: 0x0005C055 File Offset: 0x0005A255
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
			: this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, isf)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, with the specified file <paramref name="access" />, using the file sharing mode specified by <paramref name="share" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <param name="share">A bitwise combination of the <see cref="T:System.IO.FileShare" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		// Token: 0x06001B2C RID: 6956 RVA: 0x0005C06F File Offset: 0x0005A26F
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: this(path, mode, access, share, 4096, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, with the specified file <paramref name="access" />, using the file sharing mode specified by <paramref name="share" />, and in the context of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> specified by <paramref name="isf" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <param name="share">A bitwise combination of the <see cref="T:System.IO.FileShare" /> values.</param>
		/// <param name="isf">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> in which to open the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
		///   <paramref name="isf" /> does not have a quota.</exception>
		// Token: 0x06001B2D RID: 6957 RVA: 0x0005C082 File Offset: 0x0005A282
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
			: this(path, mode, access, share, 4096, isf)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, with the specified file <paramref name="access" />, using the file sharing mode specified by <paramref name="share" />, with the <paramref name="buffersize" /> specified.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <param name="share">A bitwise combination of the <see cref="T:System.IO.FileShare" /> values.</param>
		/// <param name="bufferSize">The <see cref="T:System.IO.FileStream" /> buffer size.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		// Token: 0x06001B2E RID: 6958 RVA: 0x0005C096 File Offset: 0x0005A296
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: this(path, mode, access, share, bufferSize, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> class giving access to the file designated by <paramref name="path" />, in the specified <paramref name="mode" />, with the specified file <paramref name="access" />, using the file sharing mode specified by <paramref name="share" />, with the <paramref name="buffersize" /> specified, and in the context of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> specified by <paramref name="isf" />.</summary>
		/// <param name="path">The relative path of the file within isolated storage.</param>
		/// <param name="mode">One of the <see cref="T:System.IO.FileMode" /> values.</param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <param name="share">A bitwise combination of the <see cref="T:System.IO.FileShare" /> values</param>
		/// <param name="bufferSize">The <see cref="T:System.IO.FileStream" /> buffer size.</param>
		/// <param name="isf">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> in which to open the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> is badly formed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">
		///   <paramref name="isf" /> does not have a quota.</exception>
		// Token: 0x06001B2F RID: 6959 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		[SecuritySafeCritical]
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0 || path.Equals("\\"))
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
			}
			if (isf == null)
			{
				this.m_OwnedStore = true;
				isf = IsolatedStorageFile.GetUserStoreForDomain();
			}
			if (isf.Disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (mode - FileMode.CreateNew > 5)
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
			}
			this.m_isf = isf;
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
			fileIOPermission.Assert();
			fileIOPermission.PermitOnly();
			this.m_GivenPath = path;
			this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
			ulong num = 0UL;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				switch (mode)
				{
				case FileMode.CreateNew:
					flag = true;
					break;
				case FileMode.Create:
				case FileMode.OpenOrCreate:
				case FileMode.Truncate:
				case FileMode.Append:
					this.m_isf.Lock(ref flag2);
					try
					{
						num = IsolatedStorageFile.RoundToBlockSize((ulong)LongPathFile.GetLength(this.m_FullPath));
					}
					catch (FileNotFoundException)
					{
						flag = true;
					}
					catch
					{
					}
					break;
				}
				if (flag)
				{
					this.m_isf.ReserveOneBlock();
				}
				try
				{
					this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true, true);
				}
				catch
				{
					if (flag)
					{
						this.m_isf.UnreserveOneBlock();
					}
					throw;
				}
				if (!flag && (mode == FileMode.Truncate || mode == FileMode.Create))
				{
					ulong num2 = IsolatedStorageFile.RoundToBlockSize((ulong)this.m_fs.Length);
					if (num > num2)
					{
						this.m_isf.Unreserve(num - num2);
					}
					else if (num2 > num)
					{
						this.m_isf.Reserve(num2 - num);
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_isf.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		/// <summary>Gets a Boolean value indicating whether the file can be read.</summary>
		/// <returns>
		///   <see langword="true" /> if an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object can be read; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0005C2A0 File Offset: 0x0005A4A0
		public override bool CanRead
		{
			get
			{
				return this.m_fs.CanRead;
			}
		}

		/// <summary>Gets a Boolean value indicating whether you can write to the file.</summary>
		/// <returns>
		///   <see langword="true" /> if an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object can be written; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0005C2AD File Offset: 0x0005A4AD
		public override bool CanWrite
		{
			get
			{
				return this.m_fs.CanWrite;
			}
		}

		/// <summary>Gets a Boolean value indicating whether seek operations are supported.</summary>
		/// <returns>
		///   <see langword="true" /> if an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object supports seek operations; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0005C2BA File Offset: 0x0005A4BA
		public override bool CanSeek
		{
			get
			{
				return this.m_fs.CanSeek;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object was opened asynchronously or synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object supports asynchronous access; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0005C2C7 File Offset: 0x0005A4C7
		public override bool IsAsync
		{
			get
			{
				return this.m_fs.IsAsync;
			}
		}

		/// <summary>Gets the length of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</summary>
		/// <returns>The length of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object in bytes.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
		public override long Length
		{
			get
			{
				return this.m_fs.Length;
			}
		}

		/// <summary>Gets or sets the current position of the current <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</summary>
		/// <returns>The current position of this <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position cannot be set to a negative number.</exception>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0005C2E1 File Offset: 0x0005A4E1
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x0005C2EE File Offset: 0x0005A4EE
		public override long Position
		{
			get
			{
				return this.m_fs.Position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources</param>
		// Token: 0x06001B37 RID: 6967 RVA: 0x0005C314 File Offset: 0x0005A514
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (this.m_fs != null)
						{
							this.m_fs.Close();
						}
					}
					finally
					{
						if (this.m_OwnedStore && this.m_isf != null)
						{
							this.m_isf.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file.</summary>
		// Token: 0x06001B38 RID: 6968 RVA: 0x0005C37C File Offset: 0x0005A57C
		public override void Flush()
		{
			this.m_fs.Flush();
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file, and also clears all intermediate file buffers.</summary>
		/// <param name="flushToDisk">
		///   <see langword="true" /> to flush all intermediate file buffers; otherwise, <see langword="false" />.</param>
		// Token: 0x06001B39 RID: 6969 RVA: 0x0005C389 File Offset: 0x0005A589
		public override void Flush(bool flushToDisk)
		{
			this.m_fs.Flush(flushToDisk);
		}

		/// <summary>Gets the file handle for the file that the current <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object encapsulates. Accessing this property is not permitted on an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object, and throws an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" />.</summary>
		/// <returns>The file handle for the file that the current <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object encapsulates.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.Handle" /> property always generates this exception.</exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0005C397 File Offset: 0x0005A597
		[Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override IntPtr Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object encapsulates.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object encapsulates.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFileStream.SafeFileHandle" /> property always generates this exception.</exception>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		public override SafeFileHandle SafeFileHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return null;
			}
		}

		/// <summary>Sets the length of this <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object to the specified <paramref name="value" />.</summary>
		/// <param name="value">The new length of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is a negative number.</exception>
		// Token: 0x06001B3C RID: 6972 RVA: 0x0005C3B0 File Offset: 0x0005A5B0
		[SecuritySafeCritical]
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				this.m_isf.Reserve(length, (ulong)value);
				try
				{
					this.ZeroInit(length, (ulong)value);
					this.m_fs.SetLength(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, (ulong)value);
					throw;
				}
				if (length > (ulong)value)
				{
					this.m_isf.UndoReserveOperation((ulong)value, length);
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		/// <summary>Prevents other processes from reading from or writing to the stream.</summary>
		/// <param name="position">The starting position of the range to lock. The value of this parameter must be equal to or greater than 0 (zero).</param>
		/// <param name="length">The number of bytes to lock.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The process cannot access the file because another process has locked a portion of the file.</exception>
		// Token: 0x06001B3D RID: 6973 RVA: 0x0005C46C File Offset: 0x0005A66C
		public override void Lock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Lock(position, length);
		}

		/// <summary>Allows other processes to access all or part of a file that was previously locked.</summary>
		/// <param name="position">The starting position of the range to unlock. The value of this parameter must be equal to or greater than 0 (zero).</param>
		/// <param name="length">The number of bytes to unlock.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative.</exception>
		// Token: 0x06001B3E RID: 6974 RVA: 0x0005C4A6 File Offset: 0x0005A6A6
		public override void Unlock(long position, long length)
		{
			if (position < 0L || length < 0L)
			{
				throw new ArgumentOutOfRangeException((position < 0L) ? "position" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_fs.Unlock(position, length);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		private void ZeroInit(ulong oldLen, ulong newLen)
		{
			if (oldLen >= newLen)
			{
				return;
			}
			ulong num = newLen - oldLen;
			byte[] array = new byte[1024];
			long position = this.m_fs.Position;
			this.m_fs.Seek((long)oldLen, SeekOrigin.Begin);
			if (num <= 1024UL)
			{
				this.m_fs.Write(array, 0, (int)num);
				this.m_fs.Position = position;
				return;
			}
			int num2 = 1024 - (int)(oldLen & 1023UL);
			this.m_fs.Write(array, 0, num2);
			num -= (ulong)((long)num2);
			int num3 = (int)(num / 1024UL);
			for (int i = 0; i < num3; i++)
			{
				this.m_fs.Write(array, 0, 1024);
			}
			this.m_fs.Write(array, 0, (int)(num & 1023UL));
			this.m_fs.Position = position;
		}

		/// <summary>Copies bytes from the current buffered <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object to an array.</summary>
		/// <param name="buffer">The buffer to read.</param>
		/// <param name="offset">The offset in the buffer at which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns>The total number of bytes read into the <paramref name="buffer" />. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream is reached.</returns>
		// Token: 0x06001B40 RID: 6976 RVA: 0x0005C5B3 File Offset: 0x0005A7B3
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_fs.Read(buffer, offset, count);
		}

		/// <summary>Reads a single byte from the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object in isolated storage.</summary>
		/// <returns>The 8-bit unsigned integer value read from the isolated storage file.</returns>
		// Token: 0x06001B41 RID: 6977 RVA: 0x0005C5C3 File Offset: 0x0005A7C3
		public override int ReadByte()
		{
			return this.m_fs.ReadByte();
		}

		/// <summary>Sets the current position of this <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object to the specified value.</summary>
		/// <param name="offset">The new position of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <returns>The new position in the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="origin" /> must be one of the <see cref="T:System.IO.SeekOrigin" /> values.</exception>
		// Token: 0x06001B42 RID: 6978 RVA: 0x0005C5D0 File Offset: 0x0005A7D0
		[SecuritySafeCritical]
		public override long Seek(long offset, SeekOrigin origin)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num2;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = (ulong)((offset < 0L) ? 0L : offset);
					break;
				case SeekOrigin.Current:
					num = (ulong)((this.m_fs.Position + offset < 0L) ? 0L : (this.m_fs.Position + offset));
					break;
				case SeekOrigin.End:
					num = (ulong)((this.m_fs.Length + offset < 0L) ? 0L : (this.m_fs.Length + offset));
					break;
				default:
					throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
				}
				this.m_isf.Reserve(length, num);
				try
				{
					this.ZeroInit(length, num);
					num2 = this.m_fs.Seek(offset, origin);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return num2;
		}

		/// <summary>Writes a block of bytes to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object using data read from a byte array.</summary>
		/// <param name="buffer">The buffer to write.</param>
		/// <param name="offset">The byte offset in buffer from which to begin.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The write attempt exceeds the quota for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</exception>
		// Token: 0x06001B43 RID: 6979 RVA: 0x0005C6DC File Offset: 0x0005A8DC
		[SecuritySafeCritical]
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + (long)count);
				this.m_isf.Reserve(length, num);
				try
				{
					this.m_fs.Write(buffer, offset, count);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		/// <summary>Writes a single byte to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</summary>
		/// <param name="value">The byte value to write to the isolated storage file.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The write attempt exceeds the quota for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> object.</exception>
		// Token: 0x06001B44 RID: 6980 RVA: 0x0005C770 File Offset: 0x0005A970
		[SecuritySafeCritical]
		public override void WriteByte(byte value)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + 1L);
				this.m_isf.Reserve(length, num);
				try
				{
					this.m_fs.WriteByte(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
		}

		/// <summary>Begins an asynchronous read.</summary>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin reading.</param>
		/// <param name="numBytes">The maximum number of bytes to read.</param>
		/// <param name="userCallback">The method to call when the asynchronous read operation is completed. This parameter is optional.</param>
		/// <param name="stateObject">The status of the asynchronous read.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that represents the asynchronous read, which is possibly still pending. This <see cref="T:System.IAsyncResult" /> must be passed to this stream's <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.EndRead(System.IAsyncResult)" /> method to determine how many bytes were read. This can be done either by the same code that called <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> or in a callback passed to <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</returns>
		/// <exception cref="T:System.IO.IOException">An asynchronous read was attempted past the end of the file.</exception>
		// Token: 0x06001B45 RID: 6981 RVA: 0x0005C804 File Offset: 0x0005AA04
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Ends a pending asynchronous read request.</summary>
		/// <param name="asyncResult">The pending asynchronous request.</param>
		/// <returns>The number of bytes read from the stream, between zero and the number of requested bytes. Streams will only return zero at the end of the stream. Otherwise, they will block until at least one byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		// Token: 0x06001B46 RID: 6982 RVA: 0x0005C818 File Offset: 0x0005AA18
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return this.m_fs.EndRead(asyncResult);
		}

		/// <summary>Begins an asynchronous write.</summary>
		/// <param name="buffer">The buffer to write data to.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="numBytes">The maximum number of bytes to write.</param>
		/// <param name="userCallback">The method to call when the asynchronous write operation is completed. This parameter is optional.</param>
		/// <param name="stateObject">The status of the asynchronous write.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous write, which is possibly still pending. This <see cref="T:System.IAsyncResult" /> must be passed to this stream's <see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" /> method to ensure that the write is complete, then frees resources appropriately. This can be done either by the same code that called <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> or in a callback passed to <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</returns>
		/// <exception cref="T:System.IO.IOException">An asynchronous write was attempted past the end of the file.</exception>
		// Token: 0x06001B47 RID: 6983 RVA: 0x0005C834 File Offset: 0x0005AA34
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IAsyncResult asyncResult;
			try
			{
				this.m_isf.Lock(ref flag);
				ulong length = (ulong)this.m_fs.Length;
				ulong num = (ulong)(this.m_fs.Position + (long)numBytes);
				this.m_isf.Reserve(length, num);
				try
				{
					asyncResult = this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, num);
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_isf.Unlock();
				}
			}
			return asyncResult;
		}

		/// <summary>Ends an asynchronous write.</summary>
		/// <param name="asyncResult">The pending asynchronous I/O request to end.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001B48 RID: 6984 RVA: 0x0005C8D0 File Offset: 0x0005AAD0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0005C8EC File Offset: 0x0005AAEC
		internal void NotPermittedError(string str)
		{
			throw new IsolatedStorageException(str);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0005C8F4 File Offset: 0x0005AAF4
		internal void NotPermittedError()
		{
			this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation_ISFS"));
		}

		// Token: 0x0400096D RID: 2413
		private const int s_BlockSize = 1024;

		// Token: 0x0400096E RID: 2414
		private const string s_BackSlash = "\\";

		// Token: 0x0400096F RID: 2415
		private FileStream m_fs;

		// Token: 0x04000970 RID: 2416
		private IsolatedStorageFile m_isf;

		// Token: 0x04000971 RID: 2417
		private string m_GivenPath;

		// Token: 0x04000972 RID: 2418
		private string m_FullPath;

		// Token: 0x04000973 RID: 2419
		private bool m_OwnedStore;
	}
}
