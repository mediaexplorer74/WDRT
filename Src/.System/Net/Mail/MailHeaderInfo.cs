using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x0200026E RID: 622
	internal static class MailHeaderInfo
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x00076F5C File Offset: 0x0007515C
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.m_HeaderInfo.Length; i++)
			{
				MailHeaderInfo.m_HeaderDictionary.Add(MailHeaderInfo.m_HeaderInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00077298 File Offset: 0x00075498
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.m_HeaderInfo[(int)id].NormalizedName;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000772B8 File Offset: 0x000754B8
		internal static MailHeaderID GetID(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return (MailHeaderID)num;
			}
			return MailHeaderID.Unknown;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000772D8 File Offset: 0x000754D8
		internal static bool IsWellKnown(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000772F4 File Offset: 0x000754F4
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].IsUserSettable;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00077324 File Offset: 0x00075524
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && MailHeaderInfo.m_HeaderInfo[num].IsSingleton;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00077354 File Offset: 0x00075554
		internal static string NormalizeCase(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return MailHeaderInfo.m_HeaderInfo[num].NormalizedName;
			}
			return name;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00077384 File Offset: 0x00075584
		internal static bool IsMatch(string name, MailHeaderID header)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && num == (int)header;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000773A8 File Offset: 0x000755A8
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) || MailHeaderInfo.m_HeaderInfo[num].AllowsUnicode;
		}

		// Token: 0x040017BE RID: 6078
		private static readonly MailHeaderInfo.HeaderInfo[] m_HeaderInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x040017BF RID: 6079
		private static readonly Dictionary<string, int> m_HeaderDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200079C RID: 1948
		private struct HeaderInfo
		{
			// Token: 0x060042CF RID: 17103 RVA: 0x00117724 File Offset: 0x00115924
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x0400338A RID: 13194
			public readonly string NormalizedName;

			// Token: 0x0400338B RID: 13195
			public readonly bool IsSingleton;

			// Token: 0x0400338C RID: 13196
			public readonly MailHeaderID ID;

			// Token: 0x0400338D RID: 13197
			public readonly bool IsUserSettable;

			// Token: 0x0400338E RID: 13198
			public readonly bool AllowsUnicode;
		}
	}
}
