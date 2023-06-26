using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	// Token: 0x020002E3 RID: 739
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000D31A6 File Offset: 0x000D13A6
		public static bool DontSupportReentrantFilterMessage
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DontSupportReentrantFilterMessage", ref LocalAppContextSwitches._dontSupportReentrantFilterMessage);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000D31B7 File Offset: 0x000D13B7
		public static bool DoNotSupportSelectAllShortcutInMultilineTextBox
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DoNotSupportSelectAllShortcutInMultilineTextBox", ref LocalAppContextSwitches._doNotSupportSelectAllShortcutInMultilineTextBox);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000D31C8 File Offset: 0x000D13C8
		public static bool DoNotLoadLatestRichEditControl
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DoNotLoadLatestRichEditControl", ref LocalAppContextSwitches._doNotLoadLatestRichEditControl);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000D31D9 File Offset: 0x000D13D9
		public static bool UseLegacyContextMenuStripSourceControlValue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.UseLegacyContextMenuStripSourceControlValue", ref LocalAppContextSwitches._useLegacyContextMenuStripSourceControlValue);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000D31EA File Offset: 0x000D13EA
		public static bool UseLegacyDomainUpDownControlScrolling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DomainUpDown.UseLegacyScrolling", ref LocalAppContextSwitches._useLegacyDomainUpDownScrolling);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000D31FB File Offset: 0x000D13FB
		public static bool AllowUpdateChildControlIndexForTabControls
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.AllowUpdateChildControlIndexForTabControls", ref LocalAppContextSwitches._allowUpdateChildControlIndexForTabControls);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000D320C File Offset: 0x000D140C
		public static bool UseLegacyImages
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.UseLegacyImages", ref LocalAppContextSwitches._useLegacyImages);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000D321D File Offset: 0x000D141D
		public static bool EnableVisualStyleValidation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableVisualStyleValidation", ref LocalAppContextSwitches._enableVisualStyleValidation);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000D322E File Offset: 0x000D142E
		public static bool EnableLegacyDangerousClipboardDeserializationMode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode < 0)
				{
					return false;
				}
				if (LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode > 0)
				{
					return true;
				}
				if (UnsafeNativeMethods.IsDynamicCodePolicyEnabled())
				{
					LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode = -1;
				}
				else
				{
					LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyDangerousClipboardDeserializationMode", ref LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode);
				}
				return LocalAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode > 0;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000D326B File Offset: 0x000D146B
		public static bool EnableLegacyChineseIMEIndicator
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyChineseIMEIndicator", ref LocalAppContextSwitches._enableLegacyChineseIMEIndicator);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000D327C File Offset: 0x000D147C
		public static bool EnableLegacyIMEFocusInComboBox
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.EnableLegacyIMEFocusInComboBox", ref LocalAppContextSwitches._enableLegacyIMEFocusInComboBox);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000D328D File Offset: 0x000D148D
		public static bool DisconnectUiaProvidersOnWmDestroy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.DisconnectUiaProvidersOnWmDestroy", ref LocalAppContextSwitches._disconnectUiaProvidersOnWmDestroy);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000D329E File Offset: 0x000D149E
		public static bool NoClientNotifications
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.AccessibleObject.NoClientNotifications", ref LocalAppContextSwitches._noClientNotifications);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000D32AF File Offset: 0x000D14AF
		public static bool FreeControlsForRefCountedAccessibleObjectsInLevel5
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Forms.FreeControlsForRefCountedAccessibleObjectsInLevel5", ref LocalAppContextSwitches._freeControlsForRefCountedAccessibleObjectsInLevel5);
			}
		}

		// Token: 0x04001342 RID: 4930
		internal const string DontSupportReentrantFilterMessageSwitchName = "Switch.System.Windows.Forms.DontSupportReentrantFilterMessage";

		// Token: 0x04001343 RID: 4931
		internal const string DoNotSupportSelectAllShortcutInMultilineTextBoxSwitchName = "Switch.System.Windows.Forms.DoNotSupportSelectAllShortcutInMultilineTextBox";

		// Token: 0x04001344 RID: 4932
		internal const string DoNotLoadLatestRichEditControlSwitchName = "Switch.System.Windows.Forms.DoNotLoadLatestRichEditControl";

		// Token: 0x04001345 RID: 4933
		internal const string UseLegacyContextMenuStripSourceControlValueSwitchName = "Switch.System.Windows.Forms.UseLegacyContextMenuStripSourceControlValue";

		// Token: 0x04001346 RID: 4934
		internal const string DomainUpDownUseLegacyScrollingSwitchName = "Switch.System.Windows.Forms.DomainUpDown.UseLegacyScrolling";

		// Token: 0x04001347 RID: 4935
		internal const string AllowUpdateChildControlIndexForTabControlsSwitchName = "Switch.System.Windows.Forms.AllowUpdateChildControlIndexForTabControls";

		// Token: 0x04001348 RID: 4936
		internal const string UseLegacyImagesSwitchName = "Switch.System.Windows.Forms.UseLegacyImages";

		// Token: 0x04001349 RID: 4937
		internal const string EnableVisualStyleValidationSwitchName = "Switch.System.Windows.Forms.EnableVisualStyleValidation";

		// Token: 0x0400134A RID: 4938
		internal const string EnableLegacyDangerousClipboardDeserializationModeSwitchName = "Switch.System.Windows.Forms.EnableLegacyDangerousClipboardDeserializationMode";

		// Token: 0x0400134B RID: 4939
		internal const string EnableLegacyChineseIMEIndicatorSwitchName = "Switch.System.Windows.Forms.EnableLegacyChineseIMEIndicator";

		// Token: 0x0400134C RID: 4940
		internal const string EnableLegacyIMEFocusInComboBoxSwitchName = "Switch.System.Windows.Forms.EnableLegacyIMEFocusInComboBox";

		// Token: 0x0400134D RID: 4941
		internal const string DisconnectUiaProvidersOnWmDestroySwitchName = "Switch.System.Windows.Forms.DisconnectUiaProvidersOnWmDestroy";

		// Token: 0x0400134E RID: 4942
		internal const string NoClientNotificationsSwitchName = "Switch.System.Windows.Forms.AccessibleObject.NoClientNotifications";

		// Token: 0x0400134F RID: 4943
		internal const string FreeControlsForRefCountedAccessibleObjectsInLevel5SwitchName = "Switch.System.Windows.Forms.FreeControlsForRefCountedAccessibleObjectsInLevel5";

		// Token: 0x04001350 RID: 4944
		private static int _dontSupportReentrantFilterMessage;

		// Token: 0x04001351 RID: 4945
		private static int _doNotSupportSelectAllShortcutInMultilineTextBox;

		// Token: 0x04001352 RID: 4946
		private static int _doNotLoadLatestRichEditControl;

		// Token: 0x04001353 RID: 4947
		private static int _useLegacyContextMenuStripSourceControlValue;

		// Token: 0x04001354 RID: 4948
		private static int _useLegacyDomainUpDownScrolling;

		// Token: 0x04001355 RID: 4949
		private static int _allowUpdateChildControlIndexForTabControls;

		// Token: 0x04001356 RID: 4950
		private static int _useLegacyImages;

		// Token: 0x04001357 RID: 4951
		private static int _enableVisualStyleValidation;

		// Token: 0x04001358 RID: 4952
		private static int _enableLegacyDangerousClipboardDeserializationMode;

		// Token: 0x04001359 RID: 4953
		private static int _enableLegacyChineseIMEIndicator;

		// Token: 0x0400135A RID: 4954
		private static int _enableLegacyIMEFocusInComboBox;

		// Token: 0x0400135B RID: 4955
		private static int _disconnectUiaProvidersOnWmDestroy;

		// Token: 0x0400135C RID: 4956
		private static int _noClientNotifications;

		// Token: 0x0400135D RID: 4957
		private static int _freeControlsForRefCountedAccessibleObjectsInLevel5;
	}
}
