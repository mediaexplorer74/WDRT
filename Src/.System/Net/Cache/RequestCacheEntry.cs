using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x0200030E RID: 782
	internal class RequestCacheEntry
	{
		// Token: 0x06001BE0 RID: 7136 RVA: 0x00085260 File Offset: 0x00083460
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0008529C File Offset: 0x0008349C
		internal RequestCacheEntry(_WinInetCache.Entry entry, bool isPrivateEntry)
		{
			this.m_IsPrivateEntry = isPrivateEntry;
			this.m_StreamSize = ((long)entry.Info.SizeHigh << 32) | (long)entry.Info.SizeLow;
			this.m_ExpiresUtc = (entry.Info.ExpireTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.ExpireTime.ToLong()));
			this.m_HitCount = entry.Info.HitRate;
			this.m_LastAccessedUtc = (entry.Info.LastAccessTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastAccessTime.ToLong()));
			this.m_LastModifiedUtc = (entry.Info.LastModifiedTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastModifiedTime.ToLong()));
			this.m_LastSynchronizedUtc = (entry.Info.LastSyncTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastSyncTime.ToLong()));
			this.m_MaxStale = TimeSpan.FromSeconds((double)entry.Info.U.ExemptDelta);
			if (this.m_MaxStale == WinInetCache.s_MaxTimeSpanForInt32)
			{
				this.m_MaxStale = TimeSpan.MaxValue;
			}
			this.m_UsageCount = entry.Info.UseCount;
			this.m_IsPartialEntry = (entry.Info.EntryType & _WinInetCache.EntryType.Sparse) > (_WinInetCache.EntryType)0;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0008541D File Offset: 0x0008361D
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x00085425 File Offset: 0x00083625
		internal bool IsPrivateEntry
		{
			get
			{
				return this.m_IsPrivateEntry;
			}
			set
			{
				this.m_IsPrivateEntry = value;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0008542E File Offset: 0x0008362E
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00085436 File Offset: 0x00083636
		internal long StreamSize
		{
			get
			{
				return this.m_StreamSize;
			}
			set
			{
				this.m_StreamSize = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0008543F File Offset: 0x0008363F
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00085447 File Offset: 0x00083647
		internal DateTime ExpiresUtc
		{
			get
			{
				return this.m_ExpiresUtc;
			}
			set
			{
				this.m_ExpiresUtc = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00085450 File Offset: 0x00083650
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00085458 File Offset: 0x00083658
		internal DateTime LastAccessedUtc
		{
			get
			{
				return this.m_LastAccessedUtc;
			}
			set
			{
				this.m_LastAccessedUtc = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00085461 File Offset: 0x00083661
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x00085469 File Offset: 0x00083669
		internal DateTime LastModifiedUtc
		{
			get
			{
				return this.m_LastModifiedUtc;
			}
			set
			{
				this.m_LastModifiedUtc = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00085472 File Offset: 0x00083672
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0008547A File Offset: 0x0008367A
		internal DateTime LastSynchronizedUtc
		{
			get
			{
				return this.m_LastSynchronizedUtc;
			}
			set
			{
				this.m_LastSynchronizedUtc = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00085483 File Offset: 0x00083683
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0008548B File Offset: 0x0008368B
		internal TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
			set
			{
				this.m_MaxStale = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00085494 File Offset: 0x00083694
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0008549C File Offset: 0x0008369C
		internal int HitCount
		{
			get
			{
				return this.m_HitCount;
			}
			set
			{
				this.m_HitCount = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000854A5 File Offset: 0x000836A5
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x000854AD File Offset: 0x000836AD
		internal int UsageCount
		{
			get
			{
				return this.m_UsageCount;
			}
			set
			{
				this.m_UsageCount = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000854B6 File Offset: 0x000836B6
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x000854BE File Offset: 0x000836BE
		internal bool IsPartialEntry
		{
			get
			{
				return this.m_IsPartialEntry;
			}
			set
			{
				this.m_IsPartialEntry = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x000854C7 File Offset: 0x000836C7
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x000854CF File Offset: 0x000836CF
		internal StringCollection EntryMetadata
		{
			get
			{
				return this.m_EntryMetadata;
			}
			set
			{
				this.m_EntryMetadata = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000854D8 File Offset: 0x000836D8
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x000854E0 File Offset: 0x000836E0
		internal StringCollection SystemMetadata
		{
			get
			{
				return this.m_SystemMetadata;
			}
			set
			{
				this.m_SystemMetadata = value;
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000854EC File Offset: 0x000836EC
		internal virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("\r\nIsPrivateEntry   = ").Append(this.IsPrivateEntry);
			stringBuilder.Append("\r\nIsPartialEntry   = ").Append(this.IsPartialEntry);
			stringBuilder.Append("\r\nStreamSize       = ").Append(this.StreamSize);
			stringBuilder.Append("\r\nExpires          = ").Append((this.ExpiresUtc == DateTime.MinValue) ? "" : this.ExpiresUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastAccessed     = ").Append((this.LastAccessedUtc == DateTime.MinValue) ? "" : this.LastAccessedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastModified     = ").Append((this.LastModifiedUtc == DateTime.MinValue) ? "" : this.LastModifiedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastSynchronized = ").Append((this.LastSynchronizedUtc == DateTime.MinValue) ? "" : this.LastSynchronizedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nMaxStale(sec)    = ").Append((this.MaxStale == TimeSpan.MinValue) ? "" : ((int)this.MaxStale.TotalSeconds).ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nHitCount         = ").Append(this.HitCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nUsageCount       = ").Append(this.UsageCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\n");
			if (verbose)
			{
				stringBuilder.Append("EntryMetadata:\r\n");
				if (this.m_EntryMetadata != null)
				{
					foreach (string text in this.m_EntryMetadata)
					{
						stringBuilder.Append(text).Append("\r\n");
					}
				}
				stringBuilder.Append("---\r\nSystemMetadata:\r\n");
				if (this.m_SystemMetadata != null)
				{
					foreach (string text2 in this.m_SystemMetadata)
					{
						stringBuilder.Append(text2).Append("\r\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B29 RID: 6953
		private bool m_IsPrivateEntry;

		// Token: 0x04001B2A RID: 6954
		private long m_StreamSize;

		// Token: 0x04001B2B RID: 6955
		private DateTime m_ExpiresUtc;

		// Token: 0x04001B2C RID: 6956
		private int m_HitCount;

		// Token: 0x04001B2D RID: 6957
		private DateTime m_LastAccessedUtc;

		// Token: 0x04001B2E RID: 6958
		private DateTime m_LastModifiedUtc;

		// Token: 0x04001B2F RID: 6959
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x04001B30 RID: 6960
		private TimeSpan m_MaxStale;

		// Token: 0x04001B31 RID: 6961
		private int m_UsageCount;

		// Token: 0x04001B32 RID: 6962
		private bool m_IsPartialEntry;

		// Token: 0x04001B33 RID: 6963
		private StringCollection m_EntryMetadata;

		// Token: 0x04001B34 RID: 6964
		private StringCollection m_SystemMetadata;
	}
}
