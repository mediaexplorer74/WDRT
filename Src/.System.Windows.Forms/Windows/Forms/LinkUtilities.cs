using System;
using System.Drawing;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x020002C8 RID: 712
	internal class LinkUtilities
	{
		// Token: 0x06002BAE RID: 11182 RVA: 0x000C47CC File Offset: 0x000C29CC
		private static Color GetIEColor(string name)
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			Color color;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Settings");
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue(name);
					registryKey.Close();
					if (text != null)
					{
						string[] array = text.Split(new char[] { ',' });
						int[] array2 = new int[3];
						int num = Math.Min(array2.Length, array.Length);
						for (int i = 0; i < num; i++)
						{
							int.TryParse(array[i], out array2[i]);
						}
						return Color.FromArgb(array2[0], array2[1], array2[2]);
					}
				}
				if (string.Equals(name, "Anchor Color", StringComparison.OrdinalIgnoreCase))
				{
					color = Color.Blue;
				}
				else if (string.Equals(name, "Anchor Color Visited", StringComparison.OrdinalIgnoreCase))
				{
					color = Color.Purple;
				}
				else
				{
					string.Equals(name, "Anchor Color Hover", StringComparison.OrdinalIgnoreCase);
					color = Color.Red;
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return color;
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000C48C4 File Offset: 0x000C2AC4
		public static Color IELinkColor
		{
			get
			{
				if (LinkUtilities.ielinkColor.IsEmpty)
				{
					LinkUtilities.ielinkColor = LinkUtilities.GetIEColor("Anchor Color");
				}
				return LinkUtilities.ielinkColor;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000C48E6 File Offset: 0x000C2AE6
		public static Color IEActiveLinkColor
		{
			get
			{
				if (LinkUtilities.ieactiveLinkColor.IsEmpty)
				{
					LinkUtilities.ieactiveLinkColor = LinkUtilities.GetIEColor("Anchor Color Hover");
				}
				return LinkUtilities.ieactiveLinkColor;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000C4908 File Offset: 0x000C2B08
		public static Color IEVisitedLinkColor
		{
			get
			{
				if (LinkUtilities.ievisitedLinkColor.IsEmpty)
				{
					LinkUtilities.ievisitedLinkColor = LinkUtilities.GetIEColor("Anchor Color Visited");
				}
				return LinkUtilities.ievisitedLinkColor;
			}
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000C492C File Offset: 0x000C2B2C
		public static Color GetVisitedLinkColor()
		{
			int num = (int)((SystemColors.Window.R + SystemColors.WindowText.R + 1) / 2);
			int g = (int)SystemColors.WindowText.G;
			int num2 = (int)((SystemColors.Window.B + SystemColors.WindowText.B + 1) / 2);
			return Color.FromArgb(num, g, num2);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000C4990 File Offset: 0x000C2B90
		public static LinkBehavior GetIELinkBehavior()
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main");
				}
				catch (SecurityException)
				{
				}
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("Anchor Underline");
					registryKey.Close();
					if (text != null && string.Compare(text, "no", true, CultureInfo.InvariantCulture) == 0)
					{
						return LinkBehavior.NeverUnderline;
					}
					if (text != null && string.Compare(text, "hover", true, CultureInfo.InvariantCulture) == 0)
					{
						return LinkBehavior.HoverUnderline;
					}
					return LinkBehavior.AlwaysUnderline;
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return LinkBehavior.AlwaysUnderline;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000C4A38 File Offset: 0x000C2C38
		public static void EnsureLinkFonts(Font baseFont, LinkBehavior link, ref Font linkFont, ref Font hoverLinkFont)
		{
			LinkUtilities.EnsureLinkFontsInternal(baseFont, link, ref linkFont, ref hoverLinkFont, false);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000C4A44 File Offset: 0x000C2C44
		internal static void EnsureLinkFontsInternal(Font baseFont, LinkBehavior link, ref Font linkFont, ref Font hoverLinkFont, bool isActive)
		{
			if (linkFont != null && hoverLinkFont != null)
			{
				return;
			}
			bool flag = true;
			bool flag2 = true;
			if (link == LinkBehavior.SystemDefault)
			{
				link = LinkUtilities.GetIELinkBehavior();
			}
			switch (link)
			{
			case LinkBehavior.AlwaysUnderline:
				flag = true;
				flag2 = true;
				break;
			case LinkBehavior.HoverUnderline:
				flag = false;
				flag2 = true;
				break;
			case LinkBehavior.NeverUnderline:
				flag = false;
				flag2 = false;
				break;
			}
			if (flag2 == flag)
			{
				FontStyle fontStyle = baseFont.Style;
				if (flag2)
				{
					fontStyle |= FontStyle.Underline;
				}
				else
				{
					fontStyle &= ~FontStyle.Underline;
				}
				if (AccessibilityImprovements.Level5)
				{
					if (isActive)
					{
						fontStyle |= FontStyle.Bold;
					}
					else
					{
						fontStyle &= ~FontStyle.Bold;
					}
				}
				hoverLinkFont = new Font(baseFont, fontStyle);
				linkFont = hoverLinkFont;
				return;
			}
			FontStyle fontStyle2 = baseFont.Style;
			if (flag2)
			{
				fontStyle2 |= FontStyle.Underline;
			}
			else
			{
				fontStyle2 &= ~FontStyle.Underline;
			}
			hoverLinkFont = new Font(baseFont, fontStyle2);
			FontStyle fontStyle3 = baseFont.Style;
			if (flag)
			{
				fontStyle3 |= FontStyle.Underline;
			}
			else
			{
				fontStyle3 &= ~FontStyle.Underline;
			}
			linkFont = new Font(baseFont, fontStyle3);
		}

		// Token: 0x04001243 RID: 4675
		private static Color ielinkColor = Color.Empty;

		// Token: 0x04001244 RID: 4676
		private static Color ieactiveLinkColor = Color.Empty;

		// Token: 0x04001245 RID: 4677
		private static Color ievisitedLinkColor = Color.Empty;

		// Token: 0x04001246 RID: 4678
		private const string IESettingsRegPath = "Software\\Microsoft\\Internet Explorer\\Settings";

		// Token: 0x04001247 RID: 4679
		public const string IEMainRegPath = "Software\\Microsoft\\Internet Explorer\\Main";

		// Token: 0x04001248 RID: 4680
		private const string IEAnchorColor = "Anchor Color";

		// Token: 0x04001249 RID: 4681
		private const string IEAnchorColorVisited = "Anchor Color Visited";

		// Token: 0x0400124A RID: 4682
		private const string IEAnchorColorHover = "Anchor Color Hover";
	}
}
