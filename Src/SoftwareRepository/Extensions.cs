using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace SoftwareRepository
{
	// Token: 0x02000005 RID: 5
	public static class Extensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
		public static string ToJson(this object o)
		{
			return JsonConvert.SerializeObject(o);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public static string ToSpeedFormat(this double speed)
		{
			return string.Format("{0}/s", Extensions.ByteSizeConverter((long)Math.Round(speed)));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		private static string ByteSizeConverter(long size)
		{
			bool flag = size < 1024L;
			string text;
			if (flag)
			{
				text = string.Format("{0} B", size);
			}
			else
			{
				bool flag2 = size < 1048576L;
				if (flag2)
				{
					text = string.Format("{0:F2} KiB", 1.0 * (double)size / 1024.0);
				}
				else
				{
					bool flag3 = size < 1073741824L;
					if (flag3)
					{
						text = string.Format("{0:F2} MiB", 1.0 * (double)size / 1048576.0);
					}
					else
					{
						bool flag4 = size < 1099511627776L;
						if (flag4)
						{
							text = string.Format("{0:F2} GiB", 1.0 * (double)size / 1073741824.0);
						}
						else
						{
							text = string.Format("{0:F2} TiB", 1.0 * (double)size / 1099511627776.0);
						}
					}
				}
			}
			bool flag5 = size >= 1024L;
			string text2;
			if (flag5)
			{
				text2 = string.Format("{0} ({1} B)", text, size);
			}
			else
			{
				text2 = text;
			}
			return text2;
		}
	}
}
