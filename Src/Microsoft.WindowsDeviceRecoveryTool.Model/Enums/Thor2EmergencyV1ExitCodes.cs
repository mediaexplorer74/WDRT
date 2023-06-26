using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x02000040 RID: 64
	public enum Thor2EmergencyV1ExitCodes : uint
	{
		// Token: 0x040000E0 RID: 224
		Emergencyflashv1ErrorUnknownSaharaState = 80996U,
		// Token: 0x040000E1 RID: 225
		Emergencyflashv1ErrorInvalidMessagingInterface,
		// Token: 0x040000E2 RID: 226
		Emergencyflashv1ErrorGeneralError,
		// Token: 0x040000E3 RID: 227
		Emergencyflashv1ErrorNotImplemented,
		// Token: 0x040000E4 RID: 228
		Emergencyflashv1ErrorXmlParsingFailed = 85000U,
		// Token: 0x040000E5 RID: 229
		Emergencyflashv1ErrorImageFileOpenFailed,
		// Token: 0x040000E6 RID: 230
		Emergencyflashv1ErrorImageFileReadFailed,
		// Token: 0x040000E7 RID: 231
		Emergencyflashv1ErrorImageFilesMissing,
		// Token: 0x040000E8 RID: 232
		Emergencyflashv1ErrorMsgUnexpectedResponse = 85020U,
		// Token: 0x040000E9 RID: 233
		Emergencyflashv1ErrorMsgSendReceiveFailed,
		// Token: 0x040000EA RID: 234
		Emergencyflashv1ErrorProgrammerSendFailed = 85030U,
		// Token: 0x040000EB RID: 235
		Emergencyflashv1ErrorNoBootImages,
		// Token: 0x040000EC RID: 236
		Emergencyflashv1ErrorSafeHexViolation,
		// Token: 0x040000ED RID: 237
		Emergencyflashv1ErrorHexFlasherDoesNotRespond,
		// Token: 0x040000EE RID: 238
		Emergencyflashv1ErrorSafeHexAddressViolation,
		// Token: 0x040000EF RID: 239
		Emergencyflashv1ErrorFfuParsingFailed = 85040U,
		// Token: 0x040000F0 RID: 240
		Emergencyflashv1ErrorFfuOpenFailed,
		// Token: 0x040000F1 RID: 241
		Emergencyflashv1ErrorFfuPartitionSizeMismatch,
		// Token: 0x040000F2 RID: 242
		Emergencyflashv1ErrorFfuGptFailure,
		// Token: 0x040000F3 RID: 243
		Emergencyflashv1ErrorBinFileOpen,
		// Token: 0x040000F4 RID: 244
		Emergencyflashv1ErrorGptNotFoundFromMbnFile,
		// Token: 0x040000F5 RID: 245
		Emergencyflashv1ErrorInvalidData,
		// Token: 0x040000F6 RID: 246
		Emergencyflashv1ErrorBinaryGptFailure
	}
}
