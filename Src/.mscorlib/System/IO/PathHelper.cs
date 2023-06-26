using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x0200019B RID: 411
	internal struct PathHelper
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x000538E1 File Offset: 0x00051AE1
		[SecurityCritical]
		internal unsafe PathHelper(char* charArrayPtr, int length)
		{
			this.m_length = 0;
			this.m_sb = null;
			this.m_arrayPtr = charArrayPtr;
			this.m_capacity = length;
			this.m_maxPath = Path.MaxPath;
			this.useStackAlloc = true;
			this.doNotTryExpandShortFileName = false;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00053918 File Offset: 0x00051B18
		[SecurityCritical]
		internal PathHelper(int capacity, int maxPath)
		{
			this.m_length = 0;
			this.m_arrayPtr = null;
			this.useStackAlloc = false;
			this.m_sb = new StringBuilder(capacity);
			this.m_capacity = capacity;
			this.m_maxPath = maxPath;
			this.doNotTryExpandShortFileName = false;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00053951 File Offset: 0x00051B51
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0005396D File Offset: 0x00051B6D
		internal int Length
		{
			get
			{
				if (this.useStackAlloc)
				{
					return this.m_length;
				}
				return this.m_sb.Length;
			}
			set
			{
				if (this.useStackAlloc)
				{
					this.m_length = value;
					return;
				}
				this.m_sb.Length = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0005398B File Offset: 0x00051B8B
		internal int Capacity
		{
			get
			{
				return this.m_capacity;
			}
		}

		// Token: 0x170002C8 RID: 712
		internal unsafe char this[int index]
		{
			[SecurityCritical]
			get
			{
				if (this.useStackAlloc)
				{
					return this.m_arrayPtr[index];
				}
				return this.m_sb[index];
			}
			[SecurityCritical]
			set
			{
				if (this.useStackAlloc)
				{
					this.m_arrayPtr[index] = value;
					return;
				}
				this.m_sb[index] = value;
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000539DC File Offset: 0x00051BDC
		[SecurityCritical]
		internal unsafe void Append(char value)
		{
			if (this.Length + 1 >= this.m_capacity)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			if (this.useStackAlloc)
			{
				this.m_arrayPtr[this.Length] = value;
				this.m_length++;
				return;
			}
			this.m_sb.Append(value);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00053A40 File Offset: 0x00051C40
		[SecurityCritical]
		internal unsafe int GetFullPathName()
		{
			if (this.useStackAlloc)
			{
				char* ptr;
				int num;
				checked
				{
					ptr = stackalloc char[unchecked((UIntPtr)(Path.MaxPath + 1)) * 2];
					num = Win32Native.GetFullPathName(this.m_arrayPtr, unchecked(Path.MaxPath + 1), ptr, IntPtr.Zero);
					if (num > Path.MaxPath)
					{
						char* ptr2 = stackalloc char[unchecked((UIntPtr)num) * 2];
						ptr = ptr2;
						num = Win32Native.GetFullPathName(this.m_arrayPtr, num, ptr, IntPtr.Zero);
					}
					if (num >= Path.MaxPath)
					{
						throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
					}
				}
				if (num == 0 && *this.m_arrayPtr != '\0')
				{
					__Error.WinIOError();
				}
				else if (num < Path.MaxPath)
				{
					ptr[num] = '\0';
				}
				this.doNotTryExpandShortFileName = false;
				string.wstrcpy(this.m_arrayPtr, ptr, num);
				this.Length = num;
				return num;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(this.m_capacity + 1);
				int num2 = Win32Native.GetFullPathName(this.m_sb.ToString(), this.m_capacity + 1, stringBuilder, IntPtr.Zero);
				if (num2 > this.m_maxPath)
				{
					stringBuilder.Length = num2;
					num2 = Win32Native.GetFullPathName(this.m_sb.ToString(), num2, stringBuilder, IntPtr.Zero);
				}
				if (num2 >= this.m_maxPath)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				if (num2 == 0 && this.m_sb[0] != '\0')
				{
					if (this.Length >= this.m_maxPath)
					{
						throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
					}
					__Error.WinIOError();
				}
				this.doNotTryExpandShortFileName = false;
				this.m_sb = stringBuilder;
				return num2;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00053BB4 File Offset: 0x00051DB4
		[SecurityCritical]
		internal unsafe bool TryExpandShortFileName()
		{
			if (this.doNotTryExpandShortFileName)
			{
				return false;
			}
			if (this.useStackAlloc)
			{
				this.NullTerminate();
				char* ptr = this.UnsafeGetArrayPtr();
				checked
				{
					char* ptr2 = stackalloc char[unchecked((UIntPtr)(Path.MaxPath + 1)) * 2];
					int longPathName = Win32Native.GetLongPathName(ptr, ptr2, Path.MaxPath);
					if (longPathName >= Path.MaxPath)
					{
						throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
					}
					if (longPathName == 0)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 2 || lastWin32Error == 3)
						{
							this.doNotTryExpandShortFileName = true;
						}
						return false;
					}
					string.wstrcpy(ptr, ptr2, longPathName);
					this.Length = longPathName;
					this.NullTerminate();
					return true;
				}
			}
			else
			{
				StringBuilder stringBuilder = this.GetStringBuilder();
				string text = stringBuilder.ToString();
				string text2 = text;
				bool flag = false;
				if (text2.Length > Path.MaxPath)
				{
					text2 = Path.AddLongPathPrefix(text2);
					flag = true;
				}
				stringBuilder.Capacity = this.m_capacity;
				stringBuilder.Length = 0;
				int num = Win32Native.GetLongPathName(text2, stringBuilder, this.m_capacity);
				if (num == 0)
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					if (2 == lastWin32Error2 || 3 == lastWin32Error2)
					{
						this.doNotTryExpandShortFileName = true;
					}
					stringBuilder.Length = 0;
					stringBuilder.Append(text);
					return false;
				}
				if (flag)
				{
					num -= 4;
				}
				if (num >= this.m_maxPath)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				stringBuilder = Path.RemoveLongPathPrefix(stringBuilder);
				this.Length = stringBuilder.Length;
				return true;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00053D08 File Offset: 0x00051F08
		[SecurityCritical]
		internal unsafe void Fixup(int lenSavedName, int lastSlash)
		{
			if (this.useStackAlloc)
			{
				char* ptr;
				checked
				{
					ptr = stackalloc char[unchecked((UIntPtr)lenSavedName) * 2];
				}
				string.wstrcpy(ptr, this.m_arrayPtr + lastSlash + 1, lenSavedName);
				this.Length = lastSlash;
				this.NullTerminate();
				this.doNotTryExpandShortFileName = false;
				bool flag = this.TryExpandShortFileName();
				this.Append(Path.DirectorySeparatorChar);
				if (this.Length + lenSavedName >= Path.MaxPath)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				string.wstrcpy(this.m_arrayPtr + this.Length, ptr, lenSavedName);
				this.Length += lenSavedName;
				return;
			}
			else
			{
				string text = this.m_sb.ToString(lastSlash + 1, lenSavedName);
				this.Length = lastSlash;
				this.doNotTryExpandShortFileName = false;
				bool flag2 = this.TryExpandShortFileName();
				this.Append(Path.DirectorySeparatorChar);
				if (this.Length + lenSavedName >= this.m_maxPath)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				this.m_sb.Append(text);
				return;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00053E04 File Offset: 0x00052004
		[SecurityCritical]
		internal unsafe bool OrdinalStartsWith(string compareTo, bool ignoreCase)
		{
			if (this.Length < compareTo.Length)
			{
				return false;
			}
			if (this.useStackAlloc)
			{
				this.NullTerminate();
				if (ignoreCase)
				{
					string text = new string(this.m_arrayPtr, 0, compareTo.Length);
					return compareTo.Equals(text, StringComparison.OrdinalIgnoreCase);
				}
				for (int i = 0; i < compareTo.Length; i++)
				{
					if (this.m_arrayPtr[i] != compareTo[i])
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				if (ignoreCase)
				{
					return this.m_sb.ToString().StartsWith(compareTo, StringComparison.OrdinalIgnoreCase);
				}
				return this.m_sb.ToString().StartsWith(compareTo, StringComparison.Ordinal);
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00053EA0 File Offset: 0x000520A0
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this.useStackAlloc)
			{
				return new string(this.m_arrayPtr, 0, this.Length);
			}
			return this.m_sb.ToString();
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00053EC8 File Offset: 0x000520C8
		[SecurityCritical]
		private unsafe char* UnsafeGetArrayPtr()
		{
			return this.m_arrayPtr;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00053ED0 File Offset: 0x000520D0
		private StringBuilder GetStringBuilder()
		{
			return this.m_sb;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00053ED8 File Offset: 0x000520D8
		[SecurityCritical]
		private unsafe void NullTerminate()
		{
			this.m_arrayPtr[this.m_length] = '\0';
		}

		// Token: 0x040008D8 RID: 2264
		private int m_capacity;

		// Token: 0x040008D9 RID: 2265
		private int m_length;

		// Token: 0x040008DA RID: 2266
		private int m_maxPath;

		// Token: 0x040008DB RID: 2267
		[SecurityCritical]
		private unsafe char* m_arrayPtr;

		// Token: 0x040008DC RID: 2268
		private StringBuilder m_sb;

		// Token: 0x040008DD RID: 2269
		private bool useStackAlloc;

		// Token: 0x040008DE RID: 2270
		private bool doNotTryExpandShortFileName;
	}
}
