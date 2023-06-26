using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000112 RID: 274
	internal enum InternalError
	{
		// Token: 0x0400051D RID: 1309
		UnexpectedReadState = 4,
		// Token: 0x0400051E RID: 1310
		UnvalidatedEntityState = 6,
		// Token: 0x0400051F RID: 1311
		NullResponseStream,
		// Token: 0x04000520 RID: 1312
		EntityNotDeleted,
		// Token: 0x04000521 RID: 1313
		EntityNotAddedState,
		// Token: 0x04000522 RID: 1314
		LinkNotAddedState,
		// Token: 0x04000523 RID: 1315
		EntryNotModified,
		// Token: 0x04000524 RID: 1316
		LinkBadState,
		// Token: 0x04000525 RID: 1317
		UnexpectedBeginChangeSet,
		// Token: 0x04000526 RID: 1318
		UnexpectedBatchState,
		// Token: 0x04000527 RID: 1319
		ChangeResponseMissingContentID,
		// Token: 0x04000528 RID: 1320
		ChangeResponseUnknownContentID,
		// Token: 0x04000529 RID: 1321
		InvalidHandleOperationResponse = 18,
		// Token: 0x0400052A RID: 1322
		InvalidEndGetRequestStream = 20,
		// Token: 0x0400052B RID: 1323
		InvalidEndGetRequestCompleted,
		// Token: 0x0400052C RID: 1324
		InvalidEndGetRequestStreamRequest,
		// Token: 0x0400052D RID: 1325
		InvalidEndGetRequestStreamStream,
		// Token: 0x0400052E RID: 1326
		InvalidEndGetRequestStreamContent,
		// Token: 0x0400052F RID: 1327
		InvalidEndGetRequestStreamContentLength,
		// Token: 0x04000530 RID: 1328
		InvalidEndWrite = 30,
		// Token: 0x04000531 RID: 1329
		InvalidEndWriteCompleted,
		// Token: 0x04000532 RID: 1330
		InvalidEndWriteRequest,
		// Token: 0x04000533 RID: 1331
		InvalidEndWriteStream,
		// Token: 0x04000534 RID: 1332
		InvalidEndGetResponse = 40,
		// Token: 0x04000535 RID: 1333
		InvalidEndGetResponseCompleted,
		// Token: 0x04000536 RID: 1334
		InvalidEndGetResponseRequest,
		// Token: 0x04000537 RID: 1335
		InvalidEndGetResponseResponse,
		// Token: 0x04000538 RID: 1336
		InvalidAsyncResponseStreamCopy,
		// Token: 0x04000539 RID: 1337
		InvalidAsyncResponseStreamCopyBuffer,
		// Token: 0x0400053A RID: 1338
		InvalidEndRead = 50,
		// Token: 0x0400053B RID: 1339
		InvalidEndReadCompleted,
		// Token: 0x0400053C RID: 1340
		InvalidEndReadStream,
		// Token: 0x0400053D RID: 1341
		InvalidEndReadCopy,
		// Token: 0x0400053E RID: 1342
		InvalidEndReadBuffer,
		// Token: 0x0400053F RID: 1343
		InvalidSaveNextChange = 60,
		// Token: 0x04000540 RID: 1344
		InvalidBeginNextChange,
		// Token: 0x04000541 RID: 1345
		SaveNextChangeIncomplete,
		// Token: 0x04000542 RID: 1346
		MaterializerReturningMoreThanOneEntity,
		// Token: 0x04000543 RID: 1347
		InvalidGetResponse = 71,
		// Token: 0x04000544 RID: 1348
		InvalidHandleCompleted,
		// Token: 0x04000545 RID: 1349
		InvalidMethodCallWhenNotReadingJsonLight
	}
}
