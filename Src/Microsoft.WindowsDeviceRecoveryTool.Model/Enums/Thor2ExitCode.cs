using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x02000046 RID: 70
	public enum Thor2ExitCode : uint
	{
		// Token: 0x04000138 RID: 312
		Thor2AllOk,
		// Token: 0x04000139 RID: 313
		Thor2UnexpectedExit,
		// Token: 0x0400013A RID: 314
		Thor2NotResponding = 103U,
		// Token: 0x0400013B RID: 315
		Thor2ErrorConnectionNotFound = 84000U,
		// Token: 0x0400013C RID: 316
		Thor2ErrorConnectionOpenFailed,
		// Token: 0x0400013D RID: 317
		Thor2ErrorBootToFlashAppFailed,
		// Token: 0x0400013E RID: 318
		Thor2ErrorNoDeviceWithinTimeout,
		// Token: 0x0400013F RID: 319
		Thor2ErrorUefiFlasherInitFailed,
		// Token: 0x04000140 RID: 320
		Thor2ErrorConnectionCloseFailed,
		// Token: 0x04000141 RID: 321
		Thor2ErrorInvalidHandle,
		// Token: 0x04000142 RID: 322
		Thor2ErrorMessageSendFailed,
		// Token: 0x04000143 RID: 323
		Thor2ErrorNoSaharaHandshake,
		// Token: 0x04000144 RID: 324
		Thor2ErrorInvalidArgumets,
		// Token: 0x04000145 RID: 325
		Thor2ErrorBootToNbmFailed,
		// Token: 0x04000146 RID: 326
		Thor2ErrorConnectionChangeFailed,
		// Token: 0x04000147 RID: 327
		Thor2ErrorRebootOfDeviceFailed,
		// Token: 0x04000148 RID: 328
		Thor2ErrorBootToPhoneInfoAppFailed,
		// Token: 0x04000149 RID: 329
		Thor2ErrorVplParseFailed = 84100U,
		// Token: 0x0400014A RID: 330
		Thor2ErrorNoFfuEntryInVpl,
		// Token: 0x0400014B RID: 331
		Thor2ErrorToCommunicateWithDevice,
		// Token: 0x0400014C RID: 332
		Thor2ErrorToCommunicateWithUefiInDevice,
		// Token: 0x0400014D RID: 333
		Thor2ErrorSecureFfuNotSupported,
		// Token: 0x0400014E RID: 334
		Thor2ErrorUefiFileProgrammingFailed,
		// Token: 0x0400014F RID: 335
		Thor2ErrorFileDoesNotFitIntoPartition,
		// Token: 0x04000150 RID: 336
		Thor2ErrorOutOfMemory,
		// Token: 0x04000151 RID: 337
		Thor2ErrorProgrammingFailed,
		// Token: 0x04000152 RID: 338
		Thor2ErrorUnsecureFfuNotSupported,
		// Token: 0x04000153 RID: 339
		Thor2ErrorInvalidGpt,
		// Token: 0x04000154 RID: 340
		Thor2ErrorUnexpectedResult,
		// Token: 0x04000155 RID: 341
		Thor2ErrorInvalidRawMsgReq,
		// Token: 0x04000156 RID: 342
		Thor2ErrorInvalidRawMsgResp,
		// Token: 0x04000157 RID: 343
		Thor2ErrorUnknownMessage,
		// Token: 0x04000158 RID: 344
		Thor2ErrorInvalidSaharaMsgResp,
		// Token: 0x04000159 RID: 345
		Thor2ErrorUnknownDeviceProtocol,
		// Token: 0x0400015A RID: 346
		Thor2ErrorFactoryResetFailed,
		// Token: 0x0400015B RID: 347
		Thor2ErrorRkhNotFoundFromBootImage,
		// Token: 0x0400015C RID: 348
		Thor2ErrorRkhMismatchBetweenBootImageAndDevice,
		// Token: 0x0400015D RID: 349
		Thor2ErrorUefiDoesNotSupportFullNviUpdate,
		// Token: 0x0400015E RID: 350
		Thor2ErrorUefiDoesNotSupportProductCodeUpdate,
		// Token: 0x0400015F RID: 351
		Thor2ErrorUefiCannotFindProductDatFile,
		// Token: 0x04000160 RID: 352
		Thor2ErrorBatteryLevelTooLow,
		// Token: 0x04000161 RID: 353
		Thor2ErrorFfuReadNotFfuFile = 84201U,
		// Token: 0x04000162 RID: 354
		Thor2ErrorFfuReadFileOpenFailed,
		// Token: 0x04000163 RID: 355
		Thor2ErrorFfuReadWrongVersion,
		// Token: 0x04000164 RID: 356
		Thor2ErrorFfuReadCorruptedFfu,
		// Token: 0x04000165 RID: 357
		Thor2ErrorFfuReadAssertionFailed,
		// Token: 0x04000166 RID: 358
		Thor2ErrorFfuReadTooSmallBlockSize,
		// Token: 0x04000167 RID: 359
		Thor2ErrorFfuReadUpdateModeNotSupported,
		// Token: 0x04000168 RID: 360
		Thor2ErrorFfuReadGptHeaderCrcMismatch,
		// Token: 0x04000169 RID: 361
		Thor2ErrorFfuReadGptPartitionEntryArrayCrcMismatch,
		// Token: 0x0400016A RID: 362
		Thor2ErrorFfuReadFileOperationFailed,
		// Token: 0x0400016B RID: 363
		Thor2ErrorFfuReadTerminatedByUser,
		// Token: 0x0400016C RID: 364
		Thor2ErrorFfuReadNotBootFile,
		// Token: 0x0400016D RID: 365
		Thor2ErrorUefiNotSupported,
		// Token: 0x0400016E RID: 366
		Thor2ErrorUefiRdcOrAuthenticationRequired,
		// Token: 0x0400016F RID: 367
		Thor2ErrorUefiInvalidParameter,
		// Token: 0x04000170 RID: 368
		Thor2ErrorUefiProtocolNotFound,
		// Token: 0x04000171 RID: 369
		Thor2ErrorUefiPartNotFound,
		// Token: 0x04000172 RID: 370
		Thor2ErrorUefiEraseFail,
		// Token: 0x04000173 RID: 371
		Thor2ErrorUefiOutOfMemory,
		// Token: 0x04000174 RID: 372
		Thor2ErrorUefiReadFail,
		// Token: 0x04000175 RID: 373
		Thor2ErrorUefiVerifyFail,
		// Token: 0x04000176 RID: 374
		Thor2ErrorInvalidNviFile,
		// Token: 0x04000177 RID: 375
		Thor2ErrorNviWriteError,
		// Token: 0x04000178 RID: 376
		Thor2ErrorJsonOperationError,
		// Token: 0x04000179 RID: 377
		Thor2ErrorInvalidJsonFile,
		// Token: 0x0400017A RID: 378
		Thor2ErrorCertificateOperationFailed,
		// Token: 0x0400017B RID: 379
		Thor2ErrorFileOpenFailed,
		// Token: 0x0400017C RID: 380
		Thor2ErrorWinUsbInvalidParameter,
		// Token: 0x0400017D RID: 381
		Thor2ErrorWinUsbInvalidMutex,
		// Token: 0x0400017E RID: 382
		Thor2ErrorWinUsbStartNotificationFailed,
		// Token: 0x0400017F RID: 383
		Thor2ErrorWinUsbEventCreationFailed,
		// Token: 0x04000180 RID: 384
		Thor2ErrorWinUsbHandleCreationFailed,
		// Token: 0x04000181 RID: 385
		Thor2ErrorWinUsbPreparationFailed,
		// Token: 0x04000182 RID: 386
		Thor2ErrorWinUsbNotInitialized,
		// Token: 0x04000183 RID: 387
		Thor2ErrorWinUsbInvalidPointer,
		// Token: 0x04000184 RID: 388
		Thor2ErrorWinUsbWaitForConnection,
		// Token: 0x04000185 RID: 389
		Thor2ErrorWinUsbForConnect,
		// Token: 0x04000186 RID: 390
		Thor2ErrorWinUsbConnectionLost,
		// Token: 0x04000187 RID: 391
		Thor2ErrorWinUsbMessageSendFailedNotSupported = 84240U,
		// Token: 0x04000188 RID: 392
		Thor2ErrorWinUsbMessageSendFailed,
		// Token: 0x04000189 RID: 393
		Thor2ErrorWinUsbNoMessage,
		// Token: 0x0400018A RID: 394
		Thor2ErrorWinUsbRxThreadCreationFailed,
		// Token: 0x0400018B RID: 395
		Thor2ErrorWinUsbArrivedDeviceThreadCreationFailed,
		// Token: 0x0400018C RID: 396
		Thor2ErrorWinUsbDeviceScannerThreadCreationFailed,
		// Token: 0x0400018D RID: 397
		Thor2ErrorWinUsHighSpeedUsbRequired,
		// Token: 0x0400018E RID: 398
		Thor2ErrorUefiTestsFailed = 84950U,
		// Token: 0x0400018F RID: 399
		Thor2ErrorNotImplemented = 84999U,
		// Token: 0x04000190 RID: 400
		OutOfMemory = 65537U,
		// Token: 0x04000191 RID: 401
		FileErrorInvalidFilePath = 131073U,
		// Token: 0x04000192 RID: 402
		FileSeekError,
		// Token: 0x04000193 RID: 403
		FileReadError,
		// Token: 0x04000194 RID: 404
		DevInvalidMode = 196609U,
		// Token: 0x04000195 RID: 405
		DevTooSmallTransferBuffer,
		// Token: 0x04000196 RID: 406
		DevReportedErrorDuringProgramming,
		// Token: 0x04000197 RID: 407
		DevReturnedResponseWithInvalidId,
		// Token: 0x04000198 RID: 408
		DevNoMemoryCardAvailable,
		// Token: 0x04000199 RID: 409
		DevMemoryCardFileSizeError,
		// Token: 0x0400019A RID: 410
		DevRkhMismatchError,
		// Token: 0x0400019B RID: 411
		DevSbl1NotSigned,
		// Token: 0x0400019C RID: 412
		DevReportedErrorResendForbidden,
		// Token: 0x0400019D RID: 413
		DevUefiNotSigned,
		// Token: 0x0400019E RID: 414
		MsgNoBufferInfoProvided = 262146U,
		// Token: 0x0400019F RID: 415
		MsgUnknownSubBlokInfoQueryResp,
		// Token: 0x040001A0 RID: 416
		MsgInvalidSizeInfoQueryResp,
		// Token: 0x040001A1 RID: 417
		MsgInvalidSizeFlashResp,
		// Token: 0x040001A2 RID: 418
		MsgInvalidSizeSecureFlashResp,
		// Token: 0x040001A3 RID: 419
		MsgSecureFlashRespMissingReqSbl,
		// Token: 0x040001A4 RID: 420
		MsgInvalidSizeFfuPayloadStatusSbl,
		// Token: 0x040001A5 RID: 421
		MsgSecureFlashRespMissingPayloadStatusSbl,
		// Token: 0x040001A6 RID: 422
		MsgInvalidSizeAsyncFlashResp,
		// Token: 0x040001A7 RID: 423
		MsgInvalidSizeEraseWpDataPartitionResp,
		// Token: 0x040001A8 RID: 424
		MsgInvalidReadRkhResp,
		// Token: 0x040001A9 RID: 425
		MsgFfuHeaderMessageDoesNotFitIntoMessageBuffer,
		// Token: 0x040001AA RID: 426
		MsgInvalidSizeBackupResp,
		// Token: 0x040001AB RID: 427
		MsgInvalidFlashAppWriteParamResp,
		// Token: 0x040001AC RID: 428
		MsgUnableToSendOrReceiveMessageDuringSffuProgramming,
		// Token: 0x040001AD RID: 429
		UnexpectedExceptionDuringSffuProgramming,
		// Token: 0x040001AE RID: 430
		MsgUnableToSendMessage = 327681U,
		// Token: 0x040001AF RID: 431
		DevReportedErrorDuringSffuProgramming = 393216U,
		// Token: 0x040001B0 RID: 432
		DevReportedErrorResendSffuForbidden = 458752U,
		// Token: 0x040001B1 RID: 433
		FfuParsingError = 2228224U,
		// Token: 0x040001B2 RID: 434
		InvalidFfuReaderVersion,
		// Token: 0x040001B3 RID: 435
		DppPartitionHasDataInFfu,
		// Token: 0x040001B4 RID: 436
		FfuFileTooBigForDevice,
		// Token: 0x040001B5 RID: 437
		FaOk = 4194304000U,
		// Token: 0x040001B6 RID: 438
		FaErrOutOfMemory,
		// Token: 0x040001B7 RID: 439
		FaErrReadFail,
		// Token: 0x040001B8 RID: 440
		FaErrBuEmpty,
		// Token: 0x040001B9 RID: 441
		FaErrWriteFail,
		// Token: 0x040001BA RID: 442
		FaErrEraseFail,
		// Token: 0x040001BB RID: 443
		FaErrPartNotFound,
		// Token: 0x040001BC RID: 444
		FaErrInvalidPart,
		// Token: 0x040001BD RID: 445
		FaErrInvalidParameter,
		// Token: 0x040001BE RID: 446
		FaErrProtocolNotFound,
		// Token: 0x040001BF RID: 447
		FaErrNotFound,
		// Token: 0x040001C0 RID: 448
		FaErrNotSupported,
		// Token: 0x040001C1 RID: 449
		FaErrLoadFail,
		// Token: 0x040001C2 RID: 450
		FaErrVerifyFail,
		// Token: 0x040001C3 RID: 451
		FaErrInvalidSbId,
		// Token: 0x040001C4 RID: 452
		FaErrInvalidSbCount,
		// Token: 0x040001C5 RID: 453
		FaErrInvalidSbLength,
		// Token: 0x040001C6 RID: 454
		FaErrNssFail,
		// Token: 0x040001C7 RID: 455
		FaErrAuthenticationRequired,
		// Token: 0x040001C8 RID: 456
		FaErrAsyncMsgSendingFailed,
		// Token: 0x040001C9 RID: 457
		FaErrInvalidMsgLength,
		// Token: 0x040001CA RID: 458
		FaErrFileNotFound,
		// Token: 0x040001CB RID: 459
		FaErrFfuInvalidHeaderType = 4194308096U,
		// Token: 0x040001CC RID: 460
		FaErrFfuInvalidHeaderSize,
		// Token: 0x040001CD RID: 461
		FaErrFfuHeaderImportFail,
		// Token: 0x040001CE RID: 462
		FaErrFfuHashFail,
		// Token: 0x040001CF RID: 463
		FaErrFfuHeaderMissing,
		// Token: 0x040001D0 RID: 464
		FaErrFfuWrongChunkSize,
		// Token: 0x040001D1 RID: 465
		FaErrFfuHashNotFound,
		// Token: 0x040001D2 RID: 466
		FaErrFfuPartialPayloadData,
		// Token: 0x040001D3 RID: 467
		FaErrFfuNullBlockDataEntry,
		// Token: 0x040001D4 RID: 468
		FaErrFfuNullLocationEntry,
		// Token: 0x040001D5 RID: 469
		FaErrFfuFlashingNotCompleted,
		// Token: 0x040001D6 RID: 470
		FaErrFfuTooMuchPayloadData,
		// Token: 0x040001D7 RID: 471
		FaErrFfuSecHdrInvalidSignature = 4194308352U,
		// Token: 0x040001D8 RID: 472
		FaErrFfuSecHdrInvalidStrSize,
		// Token: 0x040001D9 RID: 473
		FaErrFfuSecHdrInvalidAlgorithm,
		// Token: 0x040001DA RID: 474
		FaErrFfuSecHdrInvalidChunkSize,
		// Token: 0x040001DB RID: 475
		FaErrFfuSecHdrInvalidCatalogSize,
		// Token: 0x040001DC RID: 476
		FaErrFfuSecHdrInvalidSecHashTableSize,
		// Token: 0x040001DD RID: 477
		FaErrFfuSecHdrValidationFail,
		// Token: 0x040001DE RID: 478
		FaErrFfuImgHdrInvalidSignature = 4194308609U,
		// Token: 0x040001DF RID: 479
		FaErrFfuImgHdrInvalidStrSize,
		// Token: 0x040001E0 RID: 480
		FaErrFfuImgHdrInvalidManifestSize,
		// Token: 0x040001E1 RID: 481
		FaErrFfuImgHdrInvalidChunkSize,
		// Token: 0x040001E2 RID: 482
		FaErrFfuStrHdrInvalidUpdateType = 4194308865U,
		// Token: 0x040001E3 RID: 483
		FaErrFfuStrHdrInvalidStrVersion,
		// Token: 0x040001E4 RID: 484
		FaErrFfuStrHdrInvalidFfuVersion,
		// Token: 0x040001E5 RID: 485
		FaErrFfuStrHdrInvalidPlatformId,
		// Token: 0x040001E6 RID: 486
		FaErrFfuStrHdrInvalidBlockSize,
		// Token: 0x040001E7 RID: 487
		FaErrFfuStrHdrInvalidWriteDescriptor,
		// Token: 0x040001E8 RID: 488
		FaErrFfuStrHdrInvalidValidateDescriptorInfo,
		// Token: 0x040001E9 RID: 489
		FaErrNssAuthInitFail = 4194309121U,
		// Token: 0x040001EA RID: 490
		FaErrNssAuthFail,
		// Token: 0x040001EB RID: 491
		FaErrNssAuthVerifyFail,
		// Token: 0x040001EC RID: 492
		FaErrNssAuthSdTypeFail,
		// Token: 0x040001ED RID: 493
		ResetProtectionEnabledOnDeviceNotFoundInFfu = 4194308613U,
		// Token: 0x040001EE RID: 494
		ResetProtectionEnabledOnDeviceLowerInFfu,
		// Token: 0x040001EF RID: 495
		ApplicationForcedToClose = 4294967295U,
		// Token: 0x040001F0 RID: 496
		UnknownError = 255U,
		// Token: 0x040001F1 RID: 497
		UnknownError2 = 3221225477U,
		// Token: 0x040001F2 RID: 498
		WriteErrorFromFlashApp = 393220U,
		// Token: 0x040001F3 RID: 499
		UnknownMessageResponseError = 1313819477U,
		// Token: 0x040001F4 RID: 500
		WindowsOsError = 3221225477U,
		// Token: 0x040001F5 RID: 501
		WindowsOsError2 = 3221226356U
	}
}
