using System;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200022D RID: 557
	internal class DisplayInformation
	{
		// Token: 0x06002462 RID: 9314 RVA: 0x000AC00A File Offset: 0x000AA20A
		static DisplayInformation()
		{
			SystemEvents.UserPreferenceChanging += DisplayInformation.UserPreferenceChanging;
			SystemEvents.DisplaySettingsChanging += DisplayInformation.DisplaySettingsChanging;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000AC02E File Offset: 0x000AA22E
		public static short BitsPerPixel
		{
			get
			{
				if (DisplayInformation.bitsPerPixel == 0)
				{
					DisplayInformation.bitsPerPixel = (short)Screen.PrimaryScreen.BitsPerPixel;
				}
				return DisplayInformation.bitsPerPixel;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000AC04C File Offset: 0x000AA24C
		public static bool LowResolution
		{
			get
			{
				if (DisplayInformation.lowResSettingValid && !DisplayInformation.lowRes)
				{
					return DisplayInformation.lowRes;
				}
				DisplayInformation.lowRes = DisplayInformation.BitsPerPixel <= 8;
				DisplayInformation.lowResSettingValid = true;
				return DisplayInformation.lowRes;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000AC07D File Offset: 0x000AA27D
		public static bool HighContrast
		{
			get
			{
				if (DisplayInformation.highContrastSettingValid)
				{
					return DisplayInformation.highContrast;
				}
				DisplayInformation.highContrast = SystemInformation.HighContrast;
				DisplayInformation.highContrastSettingValid = true;
				return DisplayInformation.highContrast;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000AC0A1 File Offset: 0x000AA2A1
		public static bool IsDropShadowEnabled
		{
			get
			{
				if (DisplayInformation.dropShadowSettingValid)
				{
					return DisplayInformation.dropShadowEnabled;
				}
				DisplayInformation.dropShadowEnabled = SystemInformation.IsDropShadowEnabled;
				DisplayInformation.dropShadowSettingValid = true;
				return DisplayInformation.dropShadowEnabled;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000AC0C5 File Offset: 0x000AA2C5
		public static bool TerminalServer
		{
			get
			{
				if (DisplayInformation.terminalSettingValid)
				{
					return DisplayInformation.isTerminalServerSession;
				}
				DisplayInformation.isTerminalServerSession = SystemInformation.TerminalServerSession;
				DisplayInformation.terminalSettingValid = true;
				return DisplayInformation.isTerminalServerSession;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x000AC0E9 File Offset: 0x000AA2E9
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				if (DisplayInformation.menuAccessKeysUnderlinedValid)
				{
					return DisplayInformation.menuAccessKeysUnderlined;
				}
				DisplayInformation.menuAccessKeysUnderlined = SystemInformation.MenuAccessKeysUnderlined;
				DisplayInformation.menuAccessKeysUnderlinedValid = true;
				return DisplayInformation.menuAccessKeysUnderlined;
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000AC10D File Offset: 0x000AA30D
		private static void DisplaySettingsChanging(object obj, EventArgs ea)
		{
			DisplayInformation.highContrastSettingValid = false;
			DisplayInformation.lowResSettingValid = false;
			DisplayInformation.terminalSettingValid = false;
			DisplayInformation.dropShadowSettingValid = false;
			DisplayInformation.menuAccessKeysUnderlinedValid = false;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000AC12D File Offset: 0x000AA32D
		private static void UserPreferenceChanging(object obj, UserPreferenceChangingEventArgs e)
		{
			DisplayInformation.highContrastSettingValid = false;
			DisplayInformation.lowResSettingValid = false;
			DisplayInformation.terminalSettingValid = false;
			DisplayInformation.dropShadowSettingValid = false;
			DisplayInformation.bitsPerPixel = 0;
			if (e.Category == UserPreferenceCategory.General)
			{
				DisplayInformation.menuAccessKeysUnderlinedValid = false;
			}
		}

		// Token: 0x04000EE5 RID: 3813
		private static bool highContrast;

		// Token: 0x04000EE6 RID: 3814
		private static bool lowRes;

		// Token: 0x04000EE7 RID: 3815
		private static bool isTerminalServerSession;

		// Token: 0x04000EE8 RID: 3816
		private static bool highContrastSettingValid;

		// Token: 0x04000EE9 RID: 3817
		private static bool lowResSettingValid;

		// Token: 0x04000EEA RID: 3818
		private static bool terminalSettingValid;

		// Token: 0x04000EEB RID: 3819
		private static short bitsPerPixel;

		// Token: 0x04000EEC RID: 3820
		private static bool dropShadowSettingValid;

		// Token: 0x04000EED RID: 3821
		private static bool dropShadowEnabled;

		// Token: 0x04000EEE RID: 3822
		private static bool menuAccessKeysUnderlinedValid;

		// Token: 0x04000EEF RID: 3823
		private static bool menuAccessKeysUnderlined;
	}
}
