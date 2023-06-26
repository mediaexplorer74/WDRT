using System;
using System.Text.RegularExpressions;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000006 RID: 6
	public sealed class VidPidPair : IEquatable<VidPidPair>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000020F8 File Offset: 0x000002F8
		public VidPidPair(string vid, string pid)
		{
			this.Vid = vid;
			this.Pid = pid;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000210E File Offset: 0x0000030E
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002116 File Offset: 0x00000316
		public string Vid { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000211F File Offset: 0x0000031F
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002127 File Offset: 0x00000327
		public string Pid { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002130 File Offset: 0x00000330
		public static VidPidPair Parse(string devicePath)
		{
			VidPidPair vidPidPair;
			if (!VidPidPair.TryParse(devicePath, out vidPidPair))
			{
				throw new FormatException();
			}
			return vidPidPair;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000214E File Offset: 0x0000034E
		public bool Equals(VidPidPair other)
		{
			return !(other == null) && new VidPidPairEqualityComparer().Equals(this, other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002168 File Offset: 0x00000368
		public override bool Equals(object obj)
		{
			VidPidPair vidPidPair = obj as VidPidPair;
			return !(vidPidPair == null) && this.Equals(vidPidPair);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000218E File Offset: 0x0000038E
		public override int GetHashCode()
		{
			return new VidPidPairEqualityComparer().GetHashCode(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000219B File Offset: 0x0000039B
		public static bool operator ==(VidPidPair a, VidPidPair b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021B2 File Offset: 0x000003B2
		public static bool operator !=(VidPidPair a, VidPidPair b)
		{
			return !(a == b);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021C0 File Offset: 0x000003C0
		public static bool TryParse(string value, out VidPidPair result)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text;
			string text2;
			if (!VidPidPair.TryParse(value, out text, out text2))
			{
				result = null;
				return false;
			}
			result = new VidPidPair(text, text2);
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000021F8 File Offset: 0x000003F8
		private static bool TryParse(string value, out string vid, out string pid)
		{
			Match match = VidPidPair.Pattern.Match(value);
			if (!match.Success)
			{
				vid = null;
				pid = null;
				return false;
			}
			vid = match.Groups["Vid"].Value;
			pid = match.Groups["Pid"].Value;
			return true;
		}

		// Token: 0x04000008 RID: 8
		private static readonly Regex Pattern = new Regex("\\bVID_(?<Vid>[0-9A-Z]{4})&PID_(?<Pid>[0-9A-Z]{4})(?:&MI_(?<Mi>\\d{2}))?.*(?<Guid>{[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}})", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
