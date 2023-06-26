using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000035 RID: 53
	public enum UriData
	{
		// Token: 0x0400014E RID: 334
		[UriDescription("Software is not correctly signed")]
		SoftwareNotCorrectlySigned = 105601,
		// Token: 0x0400014F RID: 335
		[UriDescription("Reset protection status is incorrect")]
		ResetProtectionStatusIsIncorrect,
		// Token: 0x04000150 RID: 336
		[UriDescription("Emergency flashing failed")]
		EmergencyFlashingFailed,
		// Token: 0x04000151 RID: 337
		[UriDescription("Emergency succesfully finished")]
		EmergencyFlashingSuccesfullyFinished,
		// Token: 0x04000152 RID: 338
		[UriDescription("Recovery after emergency flashing failed")]
		RecoveryAfterEmergencyFlashingFailed,
		// Token: 0x04000153 RID: 339
		[UriDescription("Dead phone recovered after emergency flashing")]
		DeadPhoneRecoveredAfterEmergencyFlashing,
		// Token: 0x04000154 RID: 340
		[UriDescription("Reading device info after emergency flashing failed")]
		ReadingDeviceInfoAfterEmergencyFlashingFailed,
		// Token: 0x04000155 RID: 341
		[UriDescription("Uefi mode appeared after emergency flashing")]
		UefiModeAfterEmergencyFlashing,
		// Token: 0x04000156 RID: 342
		[UriDescription("Emergency mode appeared after emergency flashing")]
		EmergencyModeAfterEmergencyFlashing,
		// Token: 0x04000157 RID: 343
		[UriDescription("Await after emergency flashing canceled")]
		AwaitAfterEmergencyFlashingCanceled,
		// Token: 0x04000158 RID: 344
		[UriDescription("Emergency flash files not found on server")]
		EmergencyFlashFilesNotFoundOnServer,
		// Token: 0x04000159 RID: 345
		[UriDescription("Change to flash mode failed")]
		ChangeToFlashModeFailed = 105614,
		// Token: 0x0400015A RID: 346
		[UriDescription("Download package success")]
		DownloadPackageSuccess,
		// Token: 0x0400015B RID: 347
		[UriDescription("Download Generic package aborted by user")]
		DownloadGenericPackageAbortedByUser = 105618,
		// Token: 0x0400015C RID: 348
		[UriDescription("Download Generic package failed")]
		DownloadGenericPackageFailed,
		// Token: 0x0400015D RID: 349
		[UriDescription("Invalid Code")]
		InvalidUriCode = 0,
		// Token: 0x0400015E RID: 350
		[UriDescription("Failed to download variant package. FIRE service break")]
		FailedToDownloadVariantPackageFireServiceBreak = 323,
		// Token: 0x0400015F RID: 351
		[UriDescription("Failed to download variant package")]
		FailedToDownloadVariantPackage = 7,
		// Token: 0x04000160 RID: 352
		[UriDescription("Cannot download file, CRC32 checksum does not match")]
		FailedToDownloadVariantPackageCrc32Failed = 21027,
		// Token: 0x04000161 RID: 353
		[UriDescription("Add variant packages location to product api search path fails")]
		AddVariantPackagesLocationToProductApiSearchPathFails = 9,
		// Token: 0x04000162 RID: 354
		[UriDescription("Can not set flash programmer search path")]
		CanNotSetFlashProgrammerSearchPath = 20,
		// Token: 0x04000163 RID: 355
		[UriDescription("Product code read failed")]
		ProductCodeReadFailed = 22,
		// Token: 0x04000164 RID: 356
		[UriDescription("Download variant package aborted by user")]
		DownloadVariantPackageAbortedByUser = 24,
		// Token: 0x04000165 RID: 357
		[UriDescription("Firmware update start failed")]
		FirmwareUpdateStartFailed = 35,
		// Token: 0x04000166 RID: 358
		[UriDescription("Programming phone failed")]
		ProgrammingPhoneFailed = 44,
		// Token: 0x04000167 RID: 359
		[UriDescription("Finalizing the programming failed")]
		FinalizingTheProgrammingFailed,
		// Token: 0x04000168 RID: 360
		[UriDescription("Firmware update successful")]
		FirmwareUpdateSuccessful,
		// Token: 0x04000169 RID: 361
		[UriDescription("Device support is blocked")]
		DeviceSupportIsBlocked = 57,
		// Token: 0x0400016A RID: 362
		[UriDescription("Firmware update successful with Retry")]
		FirmwareUpdateSuccessfulWithRetry = 67,
		// Token: 0x0400016B RID: 363
		[UriDescription("Could not read product type")]
		CouldNotReadProductType = 117,
		// Token: 0x0400016C RID: 364
		[UriDescription("Download variant package files failed because of insufficient disk space")]
		DownloadVariantPackageFilesFailedBecauseOfInsufficientDiskSpace = 154,
		// Token: 0x0400016D RID: 365
		[UriDescription("Battery low, please recharge")]
		DeviceBatteryLow = 311,
		// Token: 0x0400016E RID: 366
		[UriDescription("Can not downgrade software")]
		CanNotDowngradeSoftware,
		// Token: 0x0400016F RID: 367
		[UriDescription("Recovery flashing aborted by user")]
		RecoveryFlashingAbortedByUser = 321,
		// Token: 0x04000170 RID: 368
		[UriDescription("Recovery flashing failed")]
		RecoveryFlashingFailed,
		// Token: 0x04000171 RID: 369
		[UriDescription("Automatic Driver Update disabling failed")]
		AutomaticDriverUpdateDisablingFailed = 338,
		// Token: 0x04000172 RID: 370
		[UriDescription("No package found")]
		NoPackageFound = 81602,
		// Token: 0x04000173 RID: 371
		[UriDescription("Unhandled exception")]
		UnhandledException = 210,
		// Token: 0x04000174 RID: 372
		[UriDescription("Select device failed")]
		SelectDeviceFailed,
		// Token: 0x04000175 RID: 373
		[UriDescription("Computer battery low")]
		ComputerBatteryLow,
		// Token: 0x04000176 RID: 374
		[UriDescription("Could not load dll")]
		CouldNotLoadDll,
		// Token: 0x04000177 RID: 375
		[UriDescription("Unhandled error during download preconditions check")]
		UnhandledErrorDuringDownloadPreconditionsCheck,
		// Token: 0x04000178 RID: 376
		[UriDescription("Software update aborted by user due to lack of proper memory card")]
		SoftwareUpdateAbortedByUserFromMemoryCardDialog = 220,
		// Token: 0x04000179 RID: 377
		[UriDescription("Device doesn't support secure flashing")]
		DeviceDoesntSupportSecureFlashing,
		// Token: 0x0400017A RID: 378
		[UriDescription("Changing device mode failed")]
		ChangingDeviceModeFailed,
		// Token: 0x0400017B RID: 379
		[UriDescription("Configuration file not found")]
		ConfigurationFileNotFound,
		// Token: 0x0400017C RID: 380
		[UriDescription("Dead phone detection started")]
		DeadPhoneDetectionStarted = 230,
		// Token: 0x0400017D RID: 381
		[UriDescription("Dead phone information read")]
		DeadPhoneInformationRead,
		// Token: 0x0400017E RID: 382
		[UriDescription("Dead phone detection aborted by user")]
		DeadPhoneDetectionAbortedByUser,
		// Token: 0x0400017F RID: 383
		[UriDescription("Dead phone detection failed")]
		DeadPhoneDetectionFailed,
		// Token: 0x04000180 RID: 384
		[UriDescription("Dead phone recovery started")]
		DeadPhoneRecoveryStarted,
		// Token: 0x04000181 RID: 385
		[UriDescription("Dead phone recovered")]
		DeadPhoneRecovered,
		// Token: 0x04000182 RID: 386
		[UriDescription("Dead phone recovery failed")]
		DeadPhoneRecoveryFailed
	}
}
