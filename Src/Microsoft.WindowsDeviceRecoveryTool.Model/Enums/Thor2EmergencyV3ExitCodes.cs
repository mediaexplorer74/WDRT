using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Enums
{
	// Token: 0x0200003F RID: 63
	public enum Thor2EmergencyV3ExitCodes : uint
	{
		// Token: 0x040000C3 RID: 195
		EmergencyflashErrorUnknownSaharaState = 80996U,
		// Token: 0x040000C4 RID: 196
		EmergencyflashErrorInvalidMessagingInterface,
		// Token: 0x040000C5 RID: 197
		EmergencyflashErrorGeneralError,
		// Token: 0x040000C6 RID: 198
		EmergencyflashErrorNotImplemented,
		// Token: 0x040000C7 RID: 199
		EmergencyflashErrorXmlParsingFailed = 85000U,
		// Token: 0x040000C8 RID: 200
		EmergencyflashErrorImageFileOpenFailed,
		// Token: 0x040000C9 RID: 201
		EmergencyflashErrorImageFileReadFailed,
		// Token: 0x040000CA RID: 202
		EmergencyflashErrorImageFilesMissing,
		// Token: 0x040000CB RID: 203
		EmergencyflashErrorEdimageParsingFailed,
		// Token: 0x040000CC RID: 204
		EmergencyflashErrorMsgUnexpectedResponse = 85020U,
		// Token: 0x040000CD RID: 205
		EmergencyflashErrorMsgSendReceiveFailed,
		// Token: 0x040000CE RID: 206
		EmergencyflashErrorMsgNakReceived,
		// Token: 0x040000CF RID: 207
		EmergencyflashErrorProgrammerSendFailed = 85030U,
		// Token: 0x040000D0 RID: 208
		EmergencyflashErrorNoBootImages,
		// Token: 0x040000D1 RID: 209
		EmergencyflashErrorSafeHexViolation,
		// Token: 0x040000D2 RID: 210
		EmergencyflashErrorHwInitFailed,
		// Token: 0x040000D3 RID: 211
		EmergencyflashErrorEmmcInitFailed = 85035U,
		// Token: 0x040000D4 RID: 212
		EmergencyflashErrorEmmcWriteFailed,
		// Token: 0x040000D5 RID: 213
		EmergencyflashErrorEmmcEraseFailed,
		// Token: 0x040000D6 RID: 214
		EmergencyflashErrorEmmcReadFailed,
		// Token: 0x040000D7 RID: 215
		EmergencyflashErrorSignatureAuthFailed,
		// Token: 0x040000D8 RID: 216
		EmergencyflashErrorFfuParsingFailed,
		// Token: 0x040000D9 RID: 217
		EmergencyflashErrorFfuOpenFailed,
		// Token: 0x040000DA RID: 218
		EmergencyflashErrorFfuPartitionSizeMismatch,
		// Token: 0x040000DB RID: 219
		EmergencyflashErrorFfuGptFailure,
		// Token: 0x040000DC RID: 220
		EmergencyflashErrorFfuPartitionNotFound,
		// Token: 0x040000DD RID: 221
		EmergencyflashErrorReadbackVerifyFailed = 85050U,
		// Token: 0x040000DE RID: 222
		EmergencyflashErrorSdramTestFailed
	}
}
