﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>Provides random access to unmanaged blocks of memory from managed code.</summary>
	// Token: 0x020001A8 RID: 424
	public class UnmanagedMemoryAccessor : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class.</summary>
		// Token: 0x06001A76 RID: 6774 RVA: 0x0005822E File Offset: 0x0005642E
		protected UnmanagedMemoryAccessor()
		{
			this._isOpen = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class with a specified buffer, offset, and capacity.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06001A77 RID: 6775 RVA: 0x0005823D File Offset: 0x0005643D
		[SecuritySafeCritical]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
		{
			this.Initialize(buffer, offset, capacity, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class with a specified buffer, offset, capacity, and access right.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <param name="access">The type of access allowed to the memory. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06001A78 RID: 6776 RVA: 0x0005824F File Offset: 0x0005644F
		[SecuritySafeCritical]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			this.Initialize(buffer, offset, capacity, access);
		}

		/// <summary>Sets the initial values for the accessor.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <param name="access">The type of access allowed to the memory. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06001A79 RID: 6777 RVA: 0x00058264 File Offset: 0x00056464
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.ByteLength < (ulong)(offset + capacity))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndCapacityOutOfBounds"));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + capacity < ptr)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_UnmanagedMemAccessorWrapAround"));
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
			this._canRead = (this._access & FileAccess.Read) > (FileAccess)0;
			this._canWrite = (this._access & FileAccess.Write) > (FileAccess)0;
		}

		/// <summary>Gets the capacity of the accessor.</summary>
		/// <returns>The capacity of the accessor.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0005838C File Offset: 0x0005658C
		public long Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		/// <summary>Determines whether the accessor is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if the accessor is readable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x00058394 File Offset: 0x00056594
		public bool CanRead
		{
			get
			{
				return this._isOpen && this._canRead;
			}
		}

		/// <summary>Determines whether the accessory is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if the accessor is writable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x000583A6 File Offset: 0x000565A6
		public bool CanWrite
		{
			get
			{
				return this._isOpen && this._canWrite;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001A7D RID: 6781 RVA: 0x000583B8 File Offset: 0x000565B8
		protected virtual void Dispose(bool disposing)
		{
			this._isOpen = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.UnmanagedMemoryAccessor" />.</summary>
		// Token: 0x06001A7E RID: 6782 RVA: 0x000583C1 File Offset: 0x000565C1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Determines whether the accessor is currently open by a process.</summary>
		/// <returns>
		///   <see langword="true" /> if the accessor is open; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000583D0 File Offset: 0x000565D0
		protected bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Reads a Boolean value from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A80 RID: 6784 RVA: 0x000583D8 File Offset: 0x000565D8
		public bool ReadBoolean(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			byte b = this.InternalReadByte(position);
			return b > 0;
		}

		/// <summary>Reads a byte value from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A81 RID: 6785 RVA: 0x000583FC File Offset: 0x000565FC
		public byte ReadByte(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			return this.InternalReadByte(position);
		}

		/// <summary>Reads a character from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A82 RID: 6786 RVA: 0x0005841C File Offset: 0x0005661C
		[SecuritySafeCritical]
		public unsafe char ReadChar(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			char c;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					c = (char)(*(ushort*)ptr);
				}
				else
				{
					c = (char)((int)(*ptr) | ((int)ptr[1] << 8));
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return c;
		}

		/// <summary>Reads a 16-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A83 RID: 6787 RVA: 0x00058490 File Offset: 0x00056690
		[SecuritySafeCritical]
		public unsafe short ReadInt16(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(short*)ptr;
				}
				else
				{
					num2 = (short)((int)(*ptr) | ((int)ptr[1] << 8));
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a 32-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A84 RID: 6788 RVA: 0x00058504 File Offset: 0x00056704
		[SecuritySafeCritical]
		public unsafe int ReadInt32(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(int*)ptr;
				}
				else
				{
					num2 = (int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a 64-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A85 RID: 6789 RVA: 0x00058588 File Offset: 0x00056788
		[SecuritySafeCritical]
		public unsafe long ReadInt64(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(long*)ptr;
				}
				else
				{
					int num3 = (int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24);
					int num4 = (int)ptr[4] | ((int)ptr[5] << 8) | ((int)ptr[6] << 16) | ((int)ptr[7] << 24);
					num2 = ((long)num4 << 32) | (long)((ulong)num3);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a decimal value from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.  
		///  -or-  
		///  The decimal to read is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00058634 File Offset: 0x00056834
		[SecuritySafeCritical]
		public decimal ReadDecimal(long position)
		{
			int num = 16;
			this.EnsureSafeToRead(position, num);
			int[] array = new int[4];
			this.ReadArray<int>(position, array, 0, array.Length);
			return new decimal(array);
		}

		/// <summary>Reads a single-precision floating-point value from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A87 RID: 6791 RVA: 0x00058668 File Offset: 0x00056868
		[SecuritySafeCritical]
		public unsafe float ReadSingle(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			float num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(float*)ptr;
				}
				else
				{
					uint num3 = (uint)((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24));
					num2 = *(float*)(&num3);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a double-precision floating-point value from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A88 RID: 6792 RVA: 0x000586F0 File Offset: 0x000568F0
		[SecuritySafeCritical]
		public unsafe double ReadDouble(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			double num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(double*)ptr;
				}
				else
				{
					uint num3 = (uint)((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24));
					uint num4 = (uint)((int)ptr[4] | ((int)ptr[5] << 8) | ((int)ptr[6] << 16) | ((int)ptr[7] << 24));
					ulong num5 = ((ulong)num4 << 32) | (ulong)num3;
					num2 = *(double*)(&num5);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an 8-bit signed integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A89 RID: 6793 RVA: 0x000587A0 File Offset: 0x000569A0
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe sbyte ReadSByte(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			sbyte b;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				b = *(sbyte*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return b;
		}

		/// <summary>Reads an unsigned 16-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8A RID: 6794 RVA: 0x00058800 File Offset: 0x00056A00
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe ushort ReadUInt16(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			ushort num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(ushort*)ptr;
				}
				else
				{
					num2 = (ushort)((int)(*ptr) | ((int)ptr[1] << 8));
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an unsigned 32-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8B RID: 6795 RVA: 0x00058874 File Offset: 0x00056A74
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe uint ReadUInt32(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			uint num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = *(uint*)ptr;
				}
				else
				{
					num2 = (uint)((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24));
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an unsigned 64-bit integer from the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <returns>The value that was read.</returns>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8C RID: 6796 RVA: 0x000588F8 File Offset: 0x00056AF8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe ulong ReadUInt64(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			ulong num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					num2 = (ulong)(*(long*)ptr);
				}
				else
				{
					uint num3 = (uint)((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24));
					uint num4 = (uint)((int)ptr[4] | ((int)ptr[5] << 8) | ((int)ptr[6] << 16) | ((int)ptr[7] << 24));
					num2 = ((ulong)num4 << 32) | (ulong)num3;
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a structure of type <paramref name="T" /> from the accessor into a provided reference.</summary>
		/// <param name="position">The position in the accessor at which to begin reading.</param>
		/// <param name="structure">The structure to contain the read data.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read in a structure of type <paramref name="T" />.  
		///  -or-  
		///  <see langword="T" /> is a value type that contains one or more reference types.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8D RID: 6797 RVA: 0x000589A4 File Offset: 0x00056BA4
		[SecurityCritical]
		public void Read<T>(long position, out T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
			}
			uint num = Marshal.SizeOfType(typeof(T));
			if (position <= this._capacity - (long)((ulong)num))
			{
				structure = this._buffer.Read<T>((ulong)(this._offset + position));
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead", new object[] { typeof(T).FullName }), "position");
		}

		/// <summary>Reads structures of type <paramref name="T" /> from the accessor into an array of type <paramref name="T" />.</summary>
		/// <param name="position">The number of bytes in the accessor at which to begin reading.</param>
		/// <param name="array">The array to contain the structures read from the accessor.</param>
		/// <param name="offset">The index in <paramref name="array" /> in which to place the first copied structure.</param>
		/// <param name="count">The number of structures of type T to read from the accessor.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <returns>The number of structures read into <paramref name="array" />. This value can be less than <paramref name="count" /> if there are fewer structures available, or zero if the end of the accessor is reached.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to contain <paramref name="count" /> of structures (starting from <paramref name="position" />).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8E RID: 6798 RVA: 0x00058A80 File Offset: 0x00056C80
		[SecurityCritical]
		public int ReadArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
			}
			if (!this.CanRead)
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
			}
			else
			{
				if (position < 0L)
				{
					throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				uint num = Marshal.AlignedSizeOf<T>();
				if (position >= this._capacity)
				{
					throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
				}
				int num2 = count;
				long num3 = this._capacity - position;
				if (num3 < 0L)
				{
					num2 = 0;
				}
				else
				{
					ulong num4 = (ulong)num * (ulong)((long)count);
					if (num3 < (long)num4)
					{
						num2 = (int)(num3 / (long)((ulong)num));
					}
				}
				this._buffer.ReadArray<T>((ulong)(this._offset + position), array, offset, num2);
				return num2;
			}
		}

		/// <summary>Writes a Boolean value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A8F RID: 6799 RVA: 0x00058B9C File Offset: 0x00056D9C
		public void Write(long position, bool value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			byte b = (value ? 1 : 0);
			this.InternalWrite(position, b);
		}

		/// <summary>Writes a byte value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A90 RID: 6800 RVA: 0x00058BC4 File Offset: 0x00056DC4
		public void Write(long position, byte value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			this.InternalWrite(position, value);
		}

		/// <summary>Writes a character into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A91 RID: 6801 RVA: 0x00058BE4 File Offset: 0x00056DE4
		[SecuritySafeCritical]
		public unsafe void Write(long position, char value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(short*)ptr = (short)value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 16-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A92 RID: 6802 RVA: 0x00058C58 File Offset: 0x00056E58
		[SecuritySafeCritical]
		public unsafe void Write(long position, short value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(short*)ptr = value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 32-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A93 RID: 6803 RVA: 0x00058CCC File Offset: 0x00056ECC
		[SecuritySafeCritical]
		public unsafe void Write(long position, int value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(int*)ptr = value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
					ptr[2] = (byte)(value >> 16);
					ptr[3] = (byte)(value >> 24);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 64-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after position to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A94 RID: 6804 RVA: 0x00058D54 File Offset: 0x00056F54
		[SecuritySafeCritical]
		public unsafe void Write(long position, long value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(long*)ptr = value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
					ptr[2] = (byte)(value >> 16);
					ptr[3] = (byte)(value >> 24);
					ptr[4] = (byte)(value >> 32);
					ptr[5] = (byte)(value >> 40);
					ptr[6] = (byte)(value >> 48);
					ptr[7] = (byte)(value >> 56);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a decimal value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.  
		///  -or-  
		///  The decimal is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A95 RID: 6805 RVA: 0x00058E00 File Offset: 0x00057000
		[SecuritySafeCritical]
		public void Write(long position, decimal value)
		{
			int num = 16;
			this.EnsureSafeToWrite(position, num);
			byte[] array = new byte[16];
			decimal.GetBytes(value, array);
			int[] array2 = new int[4];
			int num2 = (int)array[12] | ((int)array[13] << 8) | ((int)array[14] << 16) | ((int)array[15] << 24);
			int num3 = (int)array[0] | ((int)array[1] << 8) | ((int)array[2] << 16) | ((int)array[3] << 24);
			int num4 = (int)array[4] | ((int)array[5] << 8) | ((int)array[6] << 16) | ((int)array[7] << 24);
			int num5 = (int)array[8] | ((int)array[9] << 8) | ((int)array[10] << 16) | ((int)array[11] << 24);
			array2[0] = num3;
			array2[1] = num4;
			array2[2] = num5;
			array2[3] = num2;
			this.WriteArray<int>(position, array2, 0, array2.Length);
		}

		/// <summary>Writes a <see langword="Single" /> into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A96 RID: 6806 RVA: 0x00058EB8 File Offset: 0x000570B8
		[SecuritySafeCritical]
		public unsafe void Write(long position, float value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(float*)ptr = value;
				}
				else
				{
					uint num2 = *(uint*)(&value);
					*ptr = (byte)num2;
					ptr[1] = (byte)(num2 >> 8);
					ptr[2] = (byte)(num2 >> 16);
					ptr[3] = (byte)(num2 >> 24);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a <see langword="Double" /> value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A97 RID: 6807 RVA: 0x00058F44 File Offset: 0x00057144
		[SecuritySafeCritical]
		public unsafe void Write(long position, double value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(double*)ptr = value;
				}
				else
				{
					ulong num2 = (ulong)(*(long*)(&value));
					*ptr = (byte)num2;
					ptr[1] = (byte)(num2 >> 8);
					ptr[2] = (byte)(num2 >> 16);
					ptr[3] = (byte)(num2 >> 24);
					ptr[4] = (byte)(num2 >> 32);
					ptr[5] = (byte)(num2 >> 40);
					ptr[6] = (byte)(num2 >> 48);
					ptr[7] = (byte)(num2 >> 56);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an 8-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A98 RID: 6808 RVA: 0x00058FF4 File Offset: 0x000571F4
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, sbyte value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*ptr = (byte)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 16-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A99 RID: 6809 RVA: 0x00059054 File Offset: 0x00057254
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, ushort value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(short*)ptr = (short)value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 32-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A9A RID: 6810 RVA: 0x000590C8 File Offset: 0x000572C8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, uint value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(int*)ptr = (int)value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
					ptr[2] = (byte)(value >> 16);
					ptr[3] = (byte)(value >> 24);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 64-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A9B RID: 6811 RVA: 0x00059150 File Offset: 0x00057350
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, ulong value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				if ((ptr & (num - 1)) == 0)
				{
					*(long*)ptr = (long)value;
				}
				else
				{
					*ptr = (byte)value;
					ptr[1] = (byte)(value >> 8);
					ptr[2] = (byte)(value >> 16);
					ptr[3] = (byte)(value >> 24);
					ptr[4] = (byte)(value >> 32);
					ptr[5] = (byte)(value >> 40);
					ptr[6] = (byte)(value >> 48);
					ptr[7] = (byte)(value >> 56);
				}
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a structure into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="structure">The structure to write.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes in the accessor after <paramref name="position" /> to write a structure of type <paramref name="T" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A9C RID: 6812 RVA: 0x000591FC File Offset: 0x000573FC
		[SecurityCritical]
		public void Write<T>(long position, ref T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
			}
			uint num = Marshal.SizeOfType(typeof(T));
			if (position <= this._capacity - (long)((ulong)num))
			{
				this._buffer.Write<T>((ulong)(this._offset + position), structure);
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", new object[] { typeof(T).FullName }), "position");
		}

		/// <summary>Writes structures from an array of type <paramref name="T" /> into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="array">The array to write into the accessor.</param>
		/// <param name="offset">The index in <paramref name="array" /> to start writing from.</param>
		/// <param name="count">The number of structures in <paramref name="array" /> to write.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes in the accessor after <paramref name="position" /> to write the number of structures specified by <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.  
		/// -or-  
		/// <paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06001A9D RID: 6813 RVA: 0x000592D8 File Offset: 0x000574D8
		[SecurityCritical]
		public void WriteArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetAndLengthOutOfBounds"));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (position >= this.Capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
			}
			this._buffer.WriteArray<T>((ulong)(this._offset + position), array, offset, count);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000593C8 File Offset: 0x000575C8
		[SecuritySafeCritical]
		private unsafe byte InternalReadByte(long position)
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				b = (ptr + this._offset)[position];
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return b;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0005941C File Offset: 0x0005761C
		[SecuritySafeCritical]
		private unsafe void InternalWrite(long position, byte value)
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				(ptr + this._offset)[position] = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00059470 File Offset: 0x00057670
		private void EnsureSafeToRead(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Reading"));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToRead"), "position");
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0005950C File Offset: 0x0005770C
		private void EnsureSafeToWrite(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_Writing"));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired"));
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NotEnoughBytesToWrite", new object[] { "Byte" }), "position");
		}

		// Token: 0x04000930 RID: 2352
		[SecurityCritical]
		private SafeBuffer _buffer;

		// Token: 0x04000931 RID: 2353
		private long _offset;

		// Token: 0x04000932 RID: 2354
		private long _capacity;

		// Token: 0x04000933 RID: 2355
		private FileAccess _access;

		// Token: 0x04000934 RID: 2356
		private bool _isOpen;

		// Token: 0x04000935 RID: 2357
		private bool _canRead;

		// Token: 0x04000936 RID: 2358
		private bool _canWrite;
	}
}
