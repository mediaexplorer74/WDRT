using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace System.Security
{
	/// <summary>Represents text that should be kept confidential, such as by deleting it from computer memory when no longer needed. This class cannot be inherited.</summary>
	// Token: 0x020001E8 RID: 488
	public sealed class SecureString : IDisposable
	{
		// Token: 0x06001D88 RID: 7560 RVA: 0x00066DB8 File Offset: 0x00064FB8
		[SecurityCritical]
		private static bool EncryptionSupported()
		{
			bool flag = true;
			try
			{
				Win32Native.SystemFunction041(SafeBSTRHandle.Allocate(null, 16U), 16U, 0U);
			}
			catch (EntryPointNotFoundException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00066DF0 File Offset: 0x00064FF0
		[SecurityCritical]
		internal SecureString(SecureString str)
		{
			this.AllocateBuffer(str.BufferLength);
			SafeBSTRHandle.Copy(str.m_buffer, this.m_buffer);
			this.m_length = str.m_length;
			this.m_encrypted = str.m_encrypted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this instance.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06001D8A RID: 7562 RVA: 0x00066E2D File Offset: 0x0006502D
		[SecuritySafeCritical]
		public SecureString()
		{
			this.CheckSupportedOnCurrentPlatform();
			this.AllocateBuffer(8);
			this.m_length = 0;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00066E4C File Offset: 0x0006504C
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		private unsafe void InitializeSecureString(char* value, int length)
		{
			this.CheckSupportedOnCurrentPlatform();
			this.AllocateBuffer(length);
			this.m_length = length;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.Memcpy(ptr, (byte*)value, length * 2);
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			this.ProtectMemory();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class from a subarray of <see cref="T:System.Char" /> objects.  
		///  This constructor is not CLS-compliant. The CLS-compliant alternative is <see cref="M:System.Security.SecureString.#ctor" />.</summary>
		/// <param name="value">A pointer to an array of <see cref="T:System.Char" /> objects.</param>
		/// <param name="length">The number of elements of <paramref name="value" /> to include in the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than 65,536.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06001D8C RID: 7564 RVA: 0x00066ECC File Offset: 0x000650CC
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Length"));
			}
			this.InitializeSecureString(value, length);
		}

		/// <summary>Gets the number of characters in the current secure string.</summary>
		/// <returns>The number of <see cref="T:System.Char" /> objects in this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00066F2D File Offset: 0x0006512D
		public int Length
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				this.EnsureNotDisposed();
				return this.m_length;
			}
		}

		/// <summary>Appends a character to the end of the current secure string.</summary>
		/// <param name="c">A character to append to this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06001D8E RID: 7566 RVA: 0x00066F3C File Offset: 0x0006513C
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void AppendChar(char c)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			this.EnsureCapacity(this.m_length + 1);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.Write<char>((ulong)(this.m_length * 2), c);
				this.m_length++;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
			}
		}

		/// <summary>Deletes the value of the current secure string.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		// Token: 0x06001D8F RID: 7567 RVA: 0x00066FC0 File Offset: 0x000651C0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Clear()
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			this.m_length = 0;
			this.m_buffer.ClearBuffer();
			this.m_encrypted = false;
		}

		/// <summary>Creates a copy of the current secure string.</summary>
		/// <returns>A duplicate of this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06001D90 RID: 7568 RVA: 0x00066FE7 File Offset: 0x000651E7
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public SecureString Copy()
		{
			this.EnsureNotDisposed();
			return new SecureString(this);
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Security.SecureString" /> object.</summary>
		// Token: 0x06001D91 RID: 7569 RVA: 0x00066FF5 File Offset: 0x000651F5
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Dispose()
		{
			if (this.m_buffer != null && !this.m_buffer.IsInvalid)
			{
				this.m_buffer.Close();
				this.m_buffer = null;
			}
		}

		/// <summary>Inserts a character in this secure string at the specified index position.</summary>
		/// <param name="index">The index position where parameter <paramref name="c" /> is inserted.</param>
		/// <param name="c">The character to insert.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than the length of this secure string.  
		/// -or-  
		/// Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06001D92 RID: 7570 RVA: 0x00067020 File Offset: 0x00065220
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public unsafe void InsertAt(int index, char c)
		{
			if (index < 0 || index > this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			this.EnsureCapacity(this.m_length + 1);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				char* ptr2 = (char*)ptr;
				for (int i = this.m_length; i > index; i--)
				{
					ptr2[i] = ptr2[i - 1];
				}
				ptr2[index] = c;
				this.m_length++;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Indicates whether this secure string is marked read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this secure string is marked read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06001D93 RID: 7571 RVA: 0x000670FC File Offset: 0x000652FC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public bool IsReadOnly()
		{
			this.EnsureNotDisposed();
			return this.m_readOnly;
		}

		/// <summary>Makes the text value of this secure string read-only.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06001D94 RID: 7572 RVA: 0x0006710A File Offset: 0x0006530A
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void MakeReadOnly()
		{
			this.EnsureNotDisposed();
			this.m_readOnly = true;
		}

		/// <summary>Removes the character at the specified index position from this secure string.</summary>
		/// <param name="index">The index position of a character in this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06001D95 RID: 7573 RVA: 0x0006711C File Offset: 0x0006531C
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public unsafe void RemoveAt(int index)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				char* ptr2 = (char*)ptr;
				for (int i = index; i < this.m_length - 1; i++)
				{
					ptr2[i] = ptr2[i + 1];
				}
				ref short ptr3 = ref *(short*)ptr2;
				int num = this.m_length - 1;
				this.m_length = num;
				*((ref ptr3) + (IntPtr)num * 2) = 0;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Replaces the existing character at the specified index position with another character.</summary>
		/// <param name="index">The index position of an existing character in this secure string</param>
		/// <param name="c">A character that replaces the existing character.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06001D96 RID: 7574 RVA: 0x000671F0 File Offset: 0x000653F0
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void SetAt(int index, char c)
		{
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.Write<char>((ulong)(index * 2), c);
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00067274 File Offset: 0x00065474
		private int BufferLength
		{
			[SecurityCritical]
			get
			{
				return this.m_buffer.Length;
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00067284 File Offset: 0x00065484
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void AllocateBuffer(int size)
		{
			uint alignedSize = SecureString.GetAlignedSize(size);
			this.m_buffer = SafeBSTRHandle.Allocate(null, alignedSize);
			if (this.m_buffer.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000672B8 File Offset: 0x000654B8
		private void CheckSupportedOnCurrentPlatform()
		{
			if (!SecureString.supportedOnCurrentPlatform)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_PlatformSecureString"));
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000672D4 File Offset: 0x000654D4
		[SecurityCritical]
		private void EnsureCapacity(int capacity)
		{
			if (capacity > 65536)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
			}
			if (capacity <= this.m_buffer.Length)
			{
				return;
			}
			SafeBSTRHandle safeBSTRHandle = SafeBSTRHandle.Allocate(null, SecureString.GetAlignedSize(capacity));
			if (safeBSTRHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			SafeBSTRHandle.Copy(this.m_buffer, safeBSTRHandle);
			this.m_buffer.Close();
			this.m_buffer = safeBSTRHandle;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00067346 File Offset: 0x00065546
		[SecurityCritical]
		private void EnsureNotDisposed()
		{
			if (this.m_buffer == null)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00067357 File Offset: 0x00065557
		private void EnsureNotReadOnly()
		{
			if (this.m_readOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00067374 File Offset: 0x00065574
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static uint GetAlignedSize(int size)
		{
			uint num = (uint)(size / 8 * 8);
			if (size % 8 != 0 || size == 0)
			{
				num += 8U;
			}
			return num;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00067394 File Offset: 0x00065594
		[SecurityCritical]
		private unsafe int GetAnsiByteCount()
		{
			uint num = 1024U;
			uint num2 = 63U;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num3;
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				num3 = Win32Native.WideCharToMultiByte(0U, num, (char*)ptr, this.m_length, null, 0, IntPtr.Zero, new IntPtr((void*)(&num2)));
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return num3;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00067404 File Offset: 0x00065604
		[SecurityCritical]
		private unsafe void GetAnsiBytes(byte* ansiStrPtr, int byteCount)
		{
			uint num = 1024U;
			uint num2 = 63U;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				Win32Native.WideCharToMultiByte(0U, num, (char*)ptr, this.m_length, ansiStrPtr, byteCount - 1, IntPtr.Zero, new IntPtr((void*)(&num2)));
				*(ansiStrPtr + byteCount - 1) = 0;
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0006747C File Offset: 0x0006567C
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		private void ProtectMemory()
		{
			if (this.m_length == 0 || this.m_encrypted)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				int num = Win32Native.SystemFunction040(this.m_buffer, (uint)(this.m_buffer.Length * 2), 0U);
				if (num < 0)
				{
					throw new CryptographicException(Win32Native.LsaNtStatusToWinError(num));
				}
				this.m_encrypted = true;
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000674E4 File Offset: 0x000656E4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToBSTR()
		{
			this.EnsureNotDisposed();
			int length = this.m_length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Win32Native.SysAllocStringLen(null, length);
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.Memcpy((byte*)intPtr.ToPointer(), ptr, length * 2);
				intPtr2 = intPtr;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (UIntPtr)((ulong)((long)(length * 2))));
					Win32Native.SysFreeString(intPtr);
				}
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return intPtr2;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000675D4 File Offset: 0x000657D4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToUniStr(bool allocateFromHeap)
		{
			this.EnsureNotDisposed();
			int length = this.m_length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (allocateFromHeap)
					{
						intPtr = Marshal.AllocHGlobal((length + 1) * 2);
					}
					else
					{
						intPtr = Marshal.AllocCoTaskMem((length + 1) * 2);
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.Memcpy((byte*)intPtr.ToPointer(), ptr, length * 2);
				char* ptr2 = (char*)intPtr.ToPointer();
				ptr2[length] = '\0';
				intPtr2 = intPtr;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (UIntPtr)((ulong)((long)(length * 2))));
					if (allocateFromHeap)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					else
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return intPtr2;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000676F4 File Offset: 0x000658F4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToAnsiStr(bool allocateFromHeap)
		{
			this.EnsureNotDisposed();
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				num = this.GetAnsiByteCount() + 1;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (allocateFromHeap)
					{
						intPtr = Marshal.AllocHGlobal(num);
					}
					else
					{
						intPtr = Marshal.AllocCoTaskMem(num);
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.GetAnsiBytes((byte*)intPtr.ToPointer(), num);
				intPtr2 = intPtr;
			}
			catch (Exception)
			{
				this.ProtectMemory();
				throw;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (UIntPtr)((ulong)((long)num)));
					if (allocateFromHeap)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					else
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
			return intPtr2;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000677DC File Offset: 0x000659DC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void UnProtectMemory()
		{
			if (this.m_length == 0)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (this.m_encrypted)
				{
					int num = Win32Native.SystemFunction041(this.m_buffer, (uint)(this.m_buffer.Length * 2), 0U);
					if (num < 0)
					{
						throw new CryptographicException(Win32Native.LsaNtStatusToWinError(num));
					}
					this.m_encrypted = false;
				}
			}
		}

		// Token: 0x04000A49 RID: 2633
		[SecurityCritical]
		private SafeBSTRHandle m_buffer;

		// Token: 0x04000A4A RID: 2634
		private int m_length;

		// Token: 0x04000A4B RID: 2635
		private bool m_readOnly;

		// Token: 0x04000A4C RID: 2636
		private bool m_encrypted;

		// Token: 0x04000A4D RID: 2637
		private static bool supportedOnCurrentPlatform = SecureString.EncryptionSupported();

		// Token: 0x04000A4E RID: 2638
		private const int BlockSize = 8;

		// Token: 0x04000A4F RID: 2639
		private const int MaxLength = 65536;

		// Token: 0x04000A50 RID: 2640
		private const uint ProtectionScope = 0U;
	}
}
