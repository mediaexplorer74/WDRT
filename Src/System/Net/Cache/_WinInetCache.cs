using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x02000319 RID: 793
	internal static class _WinInetCache
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x00085F1C File Offset: 0x0008411C
		internal unsafe static _WinInetCache.Status LookupInfo(_WinInetCache.Entry entry)
		{
			byte[] array = new byte[2048];
			int num = array.Length;
			byte[] array2 = array;
			for (int i = 0; i < 64; i++)
			{
				try
				{
					byte[] array3;
					byte* ptr;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array3[0];
					}
					bool urlCacheEntryInfoW = UnsafeNclNativeMethods.UnsafeWinInetCache.GetUrlCacheEntryInfoW(entry.Key, ptr, ref num);
					if (urlCacheEntryInfoW)
					{
						array = array2;
						entry.MaxBufferBytes = num;
						_WinInetCache.EntryFixup(entry, (_WinInetCache.EntryBuffer*)ptr, array2);
						entry.Error = _WinInetCache.Status.Success;
						return entry.Error;
					}
					entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
					if (entry.Error != _WinInetCache.Status.InsufficientBuffer || array2 != array || num > entry.MaxBufferBytes)
					{
						break;
					}
					array2 = new byte[num];
				}
				finally
				{
					byte[] array3 = null;
				}
			}
			return entry.Error;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00085FF0 File Offset: 0x000841F0
		internal unsafe static SafeUnlockUrlCacheEntryFile LookupFile(_WinInetCache.Entry entry)
		{
			byte[] array = new byte[2048];
			int num = array.Length;
			SafeUnlockUrlCacheEntryFile safeUnlockUrlCacheEntryFile = null;
			try
			{
				for (;;)
				{
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						entry.Error = SafeUnlockUrlCacheEntryFile.GetAndLockFile(entry.Key, ptr, ref num, out safeUnlockUrlCacheEntryFile);
						if (entry.Error == _WinInetCache.Status.Success)
						{
							entry.MaxBufferBytes = num;
							_WinInetCache.EntryFixup(entry, (_WinInetCache.EntryBuffer*)ptr, array);
							return safeUnlockUrlCacheEntryFile;
						}
						if (entry.Error == _WinInetCache.Status.InsufficientBuffer && num <= entry.MaxBufferBytes)
						{
							array = new byte[num];
							continue;
						}
					}
					finally
					{
						byte[] array2 = null;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				if (safeUnlockUrlCacheEntryFile != null)
				{
					safeUnlockUrlCacheEntryFile.Close();
				}
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (entry.Error == _WinInetCache.Status.Success)
				{
					entry.Error = _WinInetCache.Status.InternalError;
				}
			}
			return null;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000860E0 File Offset: 0x000842E0
		private unsafe static _WinInetCache.Status EntryFixup(_WinInetCache.Entry entry, _WinInetCache.EntryBuffer* bufferPtr, byte[] buffer)
		{
			bufferPtr->_OffsetExtension = ((bufferPtr->_OffsetExtension == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)(void*)bufferPtr->_OffsetExtension - (byte*)bufferPtr))));
			bufferPtr->_OffsetFileName = ((bufferPtr->_OffsetFileName == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)(void*)bufferPtr->_OffsetFileName - (byte*)bufferPtr))));
			bufferPtr->_OffsetHeaderInfo = ((bufferPtr->_OffsetHeaderInfo == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)(void*)bufferPtr->_OffsetHeaderInfo - (byte*)bufferPtr))));
			bufferPtr->_OffsetSourceUrlName = ((bufferPtr->_OffsetSourceUrlName == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)(void*)bufferPtr->_OffsetSourceUrlName - (byte*)bufferPtr))));
			entry.Info = *bufferPtr;
			entry.OriginalUrl = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetSourceUrlName);
			entry.Filename = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetFileName);
			entry.FileExt = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetExtension);
			return _WinInetCache.GetEntryHeaders(entry, bufferPtr, buffer);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00086218 File Offset: 0x00084418
		internal static _WinInetCache.Status CreateFileName(_WinInetCache.Entry entry)
		{
			entry.Error = _WinInetCache.Status.Success;
			StringBuilder stringBuilder = new StringBuilder(260);
			if (UnsafeNclNativeMethods.UnsafeWinInetCache.CreateUrlCacheEntryW(entry.Key, entry.OptionalLength, entry.FileExt, stringBuilder, 0))
			{
				entry.Filename = stringBuilder.ToString();
				return _WinInetCache.Status.Success;
			}
			entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			return entry.Error;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00086274 File Offset: 0x00084474
		internal unsafe static _WinInetCache.Status Commit(_WinInetCache.Entry entry)
		{
			string text = entry.MetaInfo;
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.Length + entry.Key.Length + entry.Filename.Length + ((entry.OriginalUrl == null) ? 0 : entry.OriginalUrl.Length) > entry.MaxBufferBytes / 2)
			{
				entry.Error = _WinInetCache.Status.InsufficientBuffer;
				return entry.Error;
			}
			entry.Error = _WinInetCache.Status.Success;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte* ptr2 = (byte*)((text.Length == 0) ? null : ptr);
				if (!UnsafeNclNativeMethods.UnsafeWinInetCache.CommitUrlCacheEntryW(entry.Key, entry.Filename, entry.Info.ExpireTime, entry.Info.LastModifiedTime, entry.Info.EntryType, ptr2, text.Length, null, entry.OriginalUrl))
				{
					entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
				}
			}
			return entry.Error;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0008635C File Offset: 0x0008455C
		internal unsafe static _WinInetCache.Status Update(_WinInetCache.Entry newEntry, _WinInetCache.Entry_FC attributes)
		{
			byte[] array = new byte[_WinInetCache.EntryBuffer.MarshalSize];
			newEntry.Error = _WinInetCache.Status.Success;
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			_WinInetCache.EntryBuffer* ptr2 = (_WinInetCache.EntryBuffer*)ptr;
			*ptr2 = newEntry.Info;
			ptr2->StructSize = _WinInetCache.EntryBuffer.MarshalSize;
			if ((attributes & _WinInetCache.Entry_FC.Headerinfo) != _WinInetCache.Entry_FC.None)
			{
				_WinInetCache.Entry entry = new _WinInetCache.Entry(newEntry.Key, newEntry.MaxBufferBytes);
				SafeUnlockUrlCacheEntryFile safeUnlockUrlCacheEntryFile = null;
				bool flag = false;
				try
				{
					safeUnlockUrlCacheEntryFile = _WinInetCache.LookupFile(entry);
					if (safeUnlockUrlCacheEntryFile == null)
					{
						newEntry.Error = entry.Error;
						return newEntry.Error;
					}
					newEntry.Filename = entry.Filename;
					newEntry.OriginalUrl = entry.OriginalUrl;
					newEntry.FileExt = entry.FileExt;
					attributes &= ~_WinInetCache.Entry_FC.Headerinfo;
					if ((attributes & _WinInetCache.Entry_FC.Exptime) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.ExpireTime = entry.Info.ExpireTime;
					}
					if ((attributes & _WinInetCache.Entry_FC.Modtime) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.LastModifiedTime = entry.Info.LastModifiedTime;
					}
					if ((attributes & _WinInetCache.Entry_FC.Attribute) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.EntryType = entry.Info.EntryType;
						newEntry.Info.U.ExemptDelta = entry.Info.U.ExemptDelta;
						if ((entry.Info.EntryType & _WinInetCache.EntryType.StickyEntry) == _WinInetCache.EntryType.StickyEntry)
						{
							attributes |= _WinInetCache.Entry_FC.Attribute | _WinInetCache.Entry_FC.ExemptDelta;
						}
					}
					attributes &= ~(_WinInetCache.Entry_FC.Modtime | _WinInetCache.Entry_FC.Exptime);
					flag = (entry.Info.EntryType & _WinInetCache.EntryType.Edited) > (_WinInetCache.EntryType)0;
					if (!flag)
					{
						_WinInetCache.Entry entry2 = entry;
						entry2.Info.EntryType = entry2.Info.EntryType | _WinInetCache.EntryType.Edited;
						if (_WinInetCache.Update(entry, _WinInetCache.Entry_FC.Attribute) != _WinInetCache.Status.Success)
						{
							newEntry.Error = entry.Error;
							return newEntry.Error;
						}
					}
				}
				finally
				{
					if (safeUnlockUrlCacheEntryFile != null)
					{
						safeUnlockUrlCacheEntryFile.Close();
					}
				}
				_WinInetCache.Remove(entry);
				if (_WinInetCache.Commit(newEntry) != _WinInetCache.Status.Success)
				{
					if (!flag)
					{
						_WinInetCache.Entry entry3 = entry;
						entry3.Info.EntryType = entry3.Info.EntryType & ~_WinInetCache.EntryType.Edited;
						_WinInetCache.Update(entry, _WinInetCache.Entry_FC.Attribute);
					}
					return newEntry.Error;
				}
				if (attributes != _WinInetCache.Entry_FC.None)
				{
					_WinInetCache.Update(newEntry, attributes);
				}
				goto IL_215;
			}
			if (!UnsafeNclNativeMethods.UnsafeWinInetCache.SetUrlCacheEntryInfoW(newEntry.Key, ptr, attributes))
			{
				newEntry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			}
			IL_215:
			array2 = null;
			return newEntry.Error;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000865A8 File Offset: 0x000847A8
		internal static _WinInetCache.Status Remove(_WinInetCache.Entry entry)
		{
			entry.Error = _WinInetCache.Status.Success;
			if (!UnsafeNclNativeMethods.UnsafeWinInetCache.DeleteUrlCacheEntryW(entry.Key))
			{
				entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			}
			return entry.Error;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000865D0 File Offset: 0x000847D0
		private unsafe static string GetEntryBufferString(void* bufferPtr, int offset)
		{
			if (offset == 0)
			{
				return null;
			}
			IntPtr intPtr = new IntPtr((void*)((byte*)bufferPtr + offset));
			return Marshal.PtrToStringUni(intPtr);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000865F4 File Offset: 0x000847F4
		private unsafe static _WinInetCache.Status GetEntryHeaders(_WinInetCache.Entry entry, _WinInetCache.EntryBuffer* bufferPtr, byte[] buffer)
		{
			entry.Error = _WinInetCache.Status.Success;
			entry.MetaInfo = null;
			if (bufferPtr->_OffsetHeaderInfo == IntPtr.Zero || bufferPtr->HeaderInfoChars == 0 || (bufferPtr->EntryType & _WinInetCache.EntryType.UrlHistory) != (_WinInetCache.EntryType)0)
			{
				return _WinInetCache.Status.Success;
			}
			int num = bufferPtr->HeaderInfoChars + (int)bufferPtr->_OffsetHeaderInfo / 2;
			if (num * 2 > entry.MaxBufferBytes)
			{
				num = entry.MaxBufferBytes / 2;
			}
			while (*(ushort*)(bufferPtr + (IntPtr)(num - 1) * 2 / (IntPtr)sizeof(_WinInetCache.EntryBuffer)) == 0)
			{
				num--;
			}
			entry.MetaInfo = Encoding.Unicode.GetString(buffer, (int)bufferPtr->_OffsetHeaderInfo, (num - (int)bufferPtr->_OffsetHeaderInfo / 2) * 2);
			return entry.Error;
		}

		// Token: 0x04001B7C RID: 7036
		private const int c_CharSz = 2;

		// Token: 0x020007B2 RID: 1970
		[Flags]
		internal enum EntryType
		{
			// Token: 0x040033F9 RID: 13305
			NormalEntry = 65,
			// Token: 0x040033FA RID: 13306
			StickyEntry = 68,
			// Token: 0x040033FB RID: 13307
			Edited = 8,
			// Token: 0x040033FC RID: 13308
			TrackOffline = 16,
			// Token: 0x040033FD RID: 13309
			TrackOnline = 32,
			// Token: 0x040033FE RID: 13310
			Sparse = 65536,
			// Token: 0x040033FF RID: 13311
			Cookie = 1048576,
			// Token: 0x04003400 RID: 13312
			UrlHistory = 2097152
		}

		// Token: 0x020007B3 RID: 1971
		[Flags]
		internal enum Entry_FC
		{
			// Token: 0x04003402 RID: 13314
			None = 0,
			// Token: 0x04003403 RID: 13315
			Attribute = 4,
			// Token: 0x04003404 RID: 13316
			Hitrate = 16,
			// Token: 0x04003405 RID: 13317
			Modtime = 64,
			// Token: 0x04003406 RID: 13318
			Exptime = 128,
			// Token: 0x04003407 RID: 13319
			Acctime = 256,
			// Token: 0x04003408 RID: 13320
			Synctime = 512,
			// Token: 0x04003409 RID: 13321
			Headerinfo = 1024,
			// Token: 0x0400340A RID: 13322
			ExemptDelta = 2048
		}

		// Token: 0x020007B4 RID: 1972
		internal enum Status
		{
			// Token: 0x0400340C RID: 13324
			Success,
			// Token: 0x0400340D RID: 13325
			InsufficientBuffer = 122,
			// Token: 0x0400340E RID: 13326
			FileNotFound = 2,
			// Token: 0x0400340F RID: 13327
			NoMoreItems = 259,
			// Token: 0x04003410 RID: 13328
			NotEnoughStorage = 8,
			// Token: 0x04003411 RID: 13329
			SharingViolation = 32,
			// Token: 0x04003412 RID: 13330
			InvalidParameter = 87,
			// Token: 0x04003413 RID: 13331
			Warnings = 16777216,
			// Token: 0x04003414 RID: 13332
			FatalErrors = 16781312,
			// Token: 0x04003415 RID: 13333
			CorruptedHeaders,
			// Token: 0x04003416 RID: 13334
			InternalError
		}

		// Token: 0x020007B5 RID: 1973
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct FILETIME
		{
			// Token: 0x06004311 RID: 17169 RVA: 0x001190D2 File Offset: 0x001172D2
			public FILETIME(long time)
			{
				this.Low = (uint)time;
				this.High = (uint)(time >> 32);
			}

			// Token: 0x06004312 RID: 17170 RVA: 0x001190E7 File Offset: 0x001172E7
			public long ToLong()
			{
				return (long)(((ulong)this.High << 32) | (ulong)this.Low);
			}

			// Token: 0x17000F3B RID: 3899
			// (get) Token: 0x06004313 RID: 17171 RVA: 0x001190FB File Offset: 0x001172FB
			public bool IsNull
			{
				get
				{
					return this.Low == 0U && this.High == 0U;
				}
			}

			// Token: 0x04003417 RID: 13335
			public uint Low;

			// Token: 0x04003418 RID: 13336
			public uint High;

			// Token: 0x04003419 RID: 13337
			public static readonly _WinInetCache.FILETIME Zero = new _WinInetCache.FILETIME(0L);
		}

		// Token: 0x020007B6 RID: 1974
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct EntryBuffer
		{
			// Token: 0x0400341A RID: 13338
			public static int MarshalSize = Marshal.SizeOf(typeof(_WinInetCache.EntryBuffer));

			// Token: 0x0400341B RID: 13339
			public int StructSize;

			// Token: 0x0400341C RID: 13340
			public IntPtr _OffsetSourceUrlName;

			// Token: 0x0400341D RID: 13341
			public IntPtr _OffsetFileName;

			// Token: 0x0400341E RID: 13342
			public _WinInetCache.EntryType EntryType;

			// Token: 0x0400341F RID: 13343
			public int UseCount;

			// Token: 0x04003420 RID: 13344
			public int HitRate;

			// Token: 0x04003421 RID: 13345
			public int SizeLow;

			// Token: 0x04003422 RID: 13346
			public int SizeHigh;

			// Token: 0x04003423 RID: 13347
			public _WinInetCache.FILETIME LastModifiedTime;

			// Token: 0x04003424 RID: 13348
			public _WinInetCache.FILETIME ExpireTime;

			// Token: 0x04003425 RID: 13349
			public _WinInetCache.FILETIME LastAccessTime;

			// Token: 0x04003426 RID: 13350
			public _WinInetCache.FILETIME LastSyncTime;

			// Token: 0x04003427 RID: 13351
			public IntPtr _OffsetHeaderInfo;

			// Token: 0x04003428 RID: 13352
			public int HeaderInfoChars;

			// Token: 0x04003429 RID: 13353
			public IntPtr _OffsetExtension;

			// Token: 0x0400342A RID: 13354
			public _WinInetCache.EntryBuffer.Rsv U;

			// Token: 0x0200091E RID: 2334
			[StructLayout(LayoutKind.Explicit)]
			public struct Rsv
			{
				// Token: 0x04003D86 RID: 15750
				[FieldOffset(0)]
				public int ExemptDelta;

				// Token: 0x04003D87 RID: 15751
				[FieldOffset(0)]
				public int Reserved;
			}
		}

		// Token: 0x020007B7 RID: 1975
		internal class Entry
		{
			// Token: 0x06004316 RID: 17174 RVA: 0x00119134 File Offset: 0x00117334
			public Entry(string key, int maxHeadersSize)
			{
				this.Key = key;
				this.MaxBufferBytes = maxHeadersSize;
				if (maxHeadersSize != 2147483647 && 2147483647 - (key.Length + _WinInetCache.EntryBuffer.MarshalSize + 1024) * 2 > maxHeadersSize)
				{
					this.MaxBufferBytes += (key.Length + _WinInetCache.EntryBuffer.MarshalSize + 1024) * 2;
				}
				this.Info.EntryType = _WinInetCache.EntryType.NormalEntry;
			}

			// Token: 0x0400342B RID: 13355
			public const int DefaultBufferSize = 2048;

			// Token: 0x0400342C RID: 13356
			public _WinInetCache.Status Error;

			// Token: 0x0400342D RID: 13357
			public string Key;

			// Token: 0x0400342E RID: 13358
			public string Filename;

			// Token: 0x0400342F RID: 13359
			public string FileExt;

			// Token: 0x04003430 RID: 13360
			public int OptionalLength;

			// Token: 0x04003431 RID: 13361
			public string OriginalUrl;

			// Token: 0x04003432 RID: 13362
			public string MetaInfo;

			// Token: 0x04003433 RID: 13363
			public int MaxBufferBytes;

			// Token: 0x04003434 RID: 13364
			public _WinInetCache.EntryBuffer Info;
		}
	}
}
