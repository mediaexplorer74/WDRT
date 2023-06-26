using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Resources
{
	// Token: 0x0200007D RID: 125
	public static class Consts
	{
		// Token: 0x040001D5 RID: 469
		public const string SplashScreenThread = "SplashScreenThread";

		// Token: 0x040001D6 RID: 470
		public const string ShellWindow = "ShellWindow";

		// Token: 0x040001D7 RID: 471
		public const string ErrorSelector = "ErrorSelector";

		// Token: 0x040001D8 RID: 472
		public const string MainArea = "MainArea";

		// Token: 0x040001D9 RID: 473
		public const string SettingsArea = "SettingsArea";

		// Token: 0x040001DA RID: 474
		public const string SettingsMainArea = "SettingsMainArea";

		// Token: 0x040001DB RID: 475
		public const string HelpMainArea = "HelpMainArea";

		// Token: 0x040001DC RID: 476
		public const string MenuArea = "MenuArea";

		// Token: 0x040001DD RID: 477
		public const string CarouselArea = "CarouselArea";

		// Token: 0x040001DE RID: 478
		public const string DeviceInfoArea = "DeviceInfoArea";

		// Token: 0x040001DF RID: 479
		public const string ShortMenuArea = "ShortMenuArea";

		// Token: 0x040001E0 RID: 480
		public const string PackageManagerArea = "PackageManagerArea";

		// Token: 0x040001E1 RID: 481
		public const string FolderBrowseAreaState = "FolderBrowseAreaState";

		// Token: 0x040001E2 RID: 482
		public const string ErrorState = "ErrorState";

		// Token: 0x040001E3 RID: 483
		public const string ManualManufacturerSelectionState = "ManualManufacturerSelectionState";

		// Token: 0x040001E4 RID: 484
		public const string AutomaticManufacturerSelectionState = "AutomaticManufacturerSelectionState";

		// Token: 0x040001E5 RID: 485
		public const string ManualPackageSelectionState = "ManualPackageSelectionState";

		// Token: 0x040001E6 RID: 486
		public const string AppAutoUpdateState = "AppAutoUpdateState";

		// Token: 0x040001E7 RID: 487
		public const string CheckAppAutoUpdateState = "CheckAppAutoUpdateState";

		// Token: 0x040001E8 RID: 488
		public const string AutomaticPackageSelectionState = "AutomaticPackageSelectionState";

		// Token: 0x040001E9 RID: 489
		public const string CheckLatestPackageState = "CheckLatestPackageState";

		// Token: 0x040001EA RID: 490
		public const string DownloadPackageState = "DownloadPackageState";

		// Token: 0x040001EB RID: 491
		public const string AwaitGenericDeviceState = "AwaitGenericDeviceState";

		// Token: 0x040001EC RID: 492
		public const string AwaitHtcState = "AwaitHtcState";

		// Token: 0x040001ED RID: 493
		public const string AwaitRecoveryDeviceState = "AwaitRecoveryDeviceState";

		// Token: 0x040001EE RID: 494
		public const string AwaitAnalogDeviceState = "AwaitAnalogDeviceState";

		// Token: 0x040001EF RID: 495
		public const string FlashingState = "FlashingState";

		// Token: 0x040001F0 RID: 496
		public const string SummaryState = "SummaryState";

		// Token: 0x040001F1 RID: 497
		public const string PackageIntegrityCheckState = "PackageIntegrityCheckState";

		// Token: 0x040001F2 RID: 498
		public const string PreviousState = "PreviousState";

		// Token: 0x040001F3 RID: 499
		public const string SettingsState = "SettingsState";

		// Token: 0x040001F4 RID: 500
		public const string HelpState = "HelpState";

		// Token: 0x040001F5 RID: 501
		public const string DeviceSelectionState = "DeviceSelectionState";

		// Token: 0x040001F6 RID: 502
		public const string ReadingDeviceInfoState = "ReadingDeviceInfoState";

		// Token: 0x040001F7 RID: 503
		public const string ReadingDeviceInfoWithThorState = "ReadingDeviceInfoWithThorState";

		// Token: 0x040001F8 RID: 504
		public const string BatteryCheckingState = "BatteryCheckingState";

		// Token: 0x040001F9 RID: 505
		public const string DisclaimerState = "DisclaimerState";

		// Token: 0x040001FA RID: 506
		public const string ManualHtcRestartState = "ManualHtcRestartState";

		// Token: 0x040001FB RID: 507
		public const string RebootHtcState = "RebootHtcState";

		// Token: 0x040001FC RID: 508
		public const string ManualDeviceTypeSelectionState = "ManualDeviceTypeSelectionState";

		// Token: 0x040001FD RID: 509
		public const string DownloadEmergencyPackageState = "DownloadEmergencyPackageState";

		// Token: 0x040001FE RID: 510
		public const string AwaitRecoveryModeAfterEmergencyFlashingState = "AwaitRecoveryModeAfterEmergencyFlashingState";

		// Token: 0x040001FF RID: 511
		public const string AbsoluteConfirmationState = "AbsoluteConfirmationState";

		// Token: 0x04000200 RID: 512
		public const string SurveyState = "SurveyState";

		// Token: 0x04000201 RID: 513
		public const string PreferencesState = "PreferencesState";

		// Token: 0x04000202 RID: 514
		public const string NetworkState = "NetworkState";

		// Token: 0x04000203 RID: 515
		public const string TraceState = "TraceState";

		// Token: 0x04000204 RID: 516
		public const string PackagesState = "PackagesState";

		// Token: 0x04000205 RID: 517
		public const string ApplicationDataState = "ApplicationDataState";

		// Token: 0x04000206 RID: 518
		public const string ChooseManufacturerHelpState = "ChooseManufacturerHelpState";

		// Token: 0x04000207 RID: 519
		public const string LumiaChooseHelpState = "LumiaChooseHelpState";

		// Token: 0x04000208 RID: 520
		public const string LumiaEmergencyHelpState = "LumiaEmergencyHelpState";

		// Token: 0x04000209 RID: 521
		public const string LumiaFlashingHelpState = "LumiaFlashingHelpState";

		// Token: 0x0400020A RID: 522
		public const string LumiaNormalHelpState = "LumiaNormalHelpState";

		// Token: 0x0400020B RID: 523
		public const string LumiaOldFlashingHelpState = "LumiaOldFlashingHelpState";

		// Token: 0x0400020C RID: 524
		public const string HtcChooseHelpState = "HtcChooseHelpState";

		// Token: 0x0400020D RID: 525
		public const string HtcBootloaderHelpState = "HtcBootloaderHelpState";

		// Token: 0x0400020E RID: 526
		public const string HtcNormalHelpState = "HtcNormalHelpState";

		// Token: 0x0400020F RID: 527
		public const string ManualGenericModelSelectionState = "ManualGenericModelSelectionState";

		// Token: 0x04000210 RID: 528
		public const string ManualGenericVariantSelectionState = "ManualGenericVariantSelectionState";

		// Token: 0x04000211 RID: 529
		public const string AwaitFawkesDeviceState = "AwaitFawkesDeviceState";

		// Token: 0x04000212 RID: 530
		public const string UnsupportedDeviceState = "UnsupportedDeviceState";
	}
}
