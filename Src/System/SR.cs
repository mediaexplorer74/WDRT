using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System
{
	// Token: 0x02000066 RID: 102
	internal sealed class SR
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0001E914 File Offset: 0x0001CB14
		internal SR()
		{
			this.resources = new ResourceManager("System", base.GetType().Assembly);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001E938 File Offset: 0x0001CB38
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR sr = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, sr, null);
			}
			return SR.loader;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001E964 File Offset: 0x0001CB64
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001E967 File Offset: 0x0001CB67
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001E974 File Offset: 0x0001CB74
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001EA1D File Offset: 0x0001CC1D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001EA28 File Offset: 0x0001CC28
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x04000531 RID: 1329
		internal const string RTL = "RTL";

		// Token: 0x04000532 RID: 1330
		internal const string ContinueButtonText = "ContinueButtonText";

		// Token: 0x04000533 RID: 1331
		internal const string DebugAssertBanner = "DebugAssertBanner";

		// Token: 0x04000534 RID: 1332
		internal const string DebugAssertShortMessage = "DebugAssertShortMessage";

		// Token: 0x04000535 RID: 1333
		internal const string DebugAssertLongMessage = "DebugAssertLongMessage";

		// Token: 0x04000536 RID: 1334
		internal const string DebugMessageTruncated = "DebugMessageTruncated";

		// Token: 0x04000537 RID: 1335
		internal const string DebugAssertTitle = "DebugAssertTitle";

		// Token: 0x04000538 RID: 1336
		internal const string NotSupported = "NotSupported";

		// Token: 0x04000539 RID: 1337
		internal const string DebugLaunchFailed = "DebugLaunchFailed";

		// Token: 0x0400053A RID: 1338
		internal const string DebugLaunchFailedTitle = "DebugLaunchFailedTitle";

		// Token: 0x0400053B RID: 1339
		internal const string ObjectDisposed = "ObjectDisposed";

		// Token: 0x0400053C RID: 1340
		internal const string ExceptionOccurred = "ExceptionOccurred";

		// Token: 0x0400053D RID: 1341
		internal const string MustAddListener = "MustAddListener";

		// Token: 0x0400053E RID: 1342
		internal const string ToStringNull = "ToStringNull";

		// Token: 0x0400053F RID: 1343
		internal const string EnumConverterInvalidValue = "EnumConverterInvalidValue";

		// Token: 0x04000540 RID: 1344
		internal const string ConvertFromException = "ConvertFromException";

		// Token: 0x04000541 RID: 1345
		internal const string ConvertToException = "ConvertToException";

		// Token: 0x04000542 RID: 1346
		internal const string ConvertInvalidPrimitive = "ConvertInvalidPrimitive";

		// Token: 0x04000543 RID: 1347
		internal const string ErrorMissingPropertyAccessors = "ErrorMissingPropertyAccessors";

		// Token: 0x04000544 RID: 1348
		internal const string ErrorInvalidPropertyType = "ErrorInvalidPropertyType";

		// Token: 0x04000545 RID: 1349
		internal const string ErrorMissingEventAccessors = "ErrorMissingEventAccessors";

		// Token: 0x04000546 RID: 1350
		internal const string ErrorInvalidEventHandler = "ErrorInvalidEventHandler";

		// Token: 0x04000547 RID: 1351
		internal const string ErrorInvalidEventType = "ErrorInvalidEventType";

		// Token: 0x04000548 RID: 1352
		internal const string InvalidMemberName = "InvalidMemberName";

		// Token: 0x04000549 RID: 1353
		internal const string ErrorBadExtenderType = "ErrorBadExtenderType";

		// Token: 0x0400054A RID: 1354
		internal const string NullableConverterBadCtorArg = "NullableConverterBadCtorArg";

		// Token: 0x0400054B RID: 1355
		internal const string TypeDescriptorExpectedElementType = "TypeDescriptorExpectedElementType";

		// Token: 0x0400054C RID: 1356
		internal const string TypeDescriptorSameAssociation = "TypeDescriptorSameAssociation";

		// Token: 0x0400054D RID: 1357
		internal const string TypeDescriptorAlreadyAssociated = "TypeDescriptorAlreadyAssociated";

		// Token: 0x0400054E RID: 1358
		internal const string TypeDescriptorProviderError = "TypeDescriptorProviderError";

		// Token: 0x0400054F RID: 1359
		internal const string TypeDescriptorUnsupportedRemoteObject = "TypeDescriptorUnsupportedRemoteObject";

		// Token: 0x04000550 RID: 1360
		internal const string TypeDescriptorArgsCountMismatch = "TypeDescriptorArgsCountMismatch";

		// Token: 0x04000551 RID: 1361
		internal const string ErrorCreateSystemEvents = "ErrorCreateSystemEvents";

		// Token: 0x04000552 RID: 1362
		internal const string ErrorCreateTimer = "ErrorCreateTimer";

		// Token: 0x04000553 RID: 1363
		internal const string ErrorKillTimer = "ErrorKillTimer";

		// Token: 0x04000554 RID: 1364
		internal const string ErrorSystemEventsNotSupported = "ErrorSystemEventsNotSupported";

		// Token: 0x04000555 RID: 1365
		internal const string ErrorGetTempPath = "ErrorGetTempPath";

		// Token: 0x04000556 RID: 1366
		internal const string CHECKOUTCanceled = "CHECKOUTCanceled";

		// Token: 0x04000557 RID: 1367
		internal const string ErrorInvalidServiceInstance = "ErrorInvalidServiceInstance";

		// Token: 0x04000558 RID: 1368
		internal const string ErrorServiceExists = "ErrorServiceExists";

		// Token: 0x04000559 RID: 1369
		internal const string Argument_InvalidNumberStyles = "Argument_InvalidNumberStyles";

		// Token: 0x0400055A RID: 1370
		internal const string Argument_InvalidHexStyle = "Argument_InvalidHexStyle";

		// Token: 0x0400055B RID: 1371
		internal const string Argument_ByteArrayLengthMustBeAMultipleOf4 = "Argument_ByteArrayLengthMustBeAMultipleOf4";

		// Token: 0x0400055C RID: 1372
		internal const string Argument_InvalidCharactersInString = "Argument_InvalidCharactersInString";

		// Token: 0x0400055D RID: 1373
		internal const string Argument_ParsedStringWasInvalid = "Argument_ParsedStringWasInvalid";

		// Token: 0x0400055E RID: 1374
		internal const string Argument_MustBeBigInt = "Argument_MustBeBigInt";

		// Token: 0x0400055F RID: 1375
		internal const string Format_InvalidFormatSpecifier = "Format_InvalidFormatSpecifier";

		// Token: 0x04000560 RID: 1376
		internal const string Format_TooLarge = "Format_TooLarge";

		// Token: 0x04000561 RID: 1377
		internal const string ArgumentOutOfRange_MustBeLessThanUInt32MaxValue = "ArgumentOutOfRange_MustBeLessThanUInt32MaxValue";

		// Token: 0x04000562 RID: 1378
		internal const string ArgumentOutOfRange_MustBeNonNeg = "ArgumentOutOfRange_MustBeNonNeg";

		// Token: 0x04000563 RID: 1379
		internal const string NotSupported_NumberStyle = "NotSupported_NumberStyle";

		// Token: 0x04000564 RID: 1380
		internal const string Overflow_BigIntInfinity = "Overflow_BigIntInfinity";

		// Token: 0x04000565 RID: 1381
		internal const string Overflow_NotANumber = "Overflow_NotANumber";

		// Token: 0x04000566 RID: 1382
		internal const string Overflow_ParseBigInteger = "Overflow_ParseBigInteger";

		// Token: 0x04000567 RID: 1383
		internal const string Overflow_Int32 = "Overflow_Int32";

		// Token: 0x04000568 RID: 1384
		internal const string Overflow_Int64 = "Overflow_Int64";

		// Token: 0x04000569 RID: 1385
		internal const string Overflow_UInt32 = "Overflow_UInt32";

		// Token: 0x0400056A RID: 1386
		internal const string Overflow_UInt64 = "Overflow_UInt64";

		// Token: 0x0400056B RID: 1387
		internal const string Overflow_Decimal = "Overflow_Decimal";

		// Token: 0x0400056C RID: 1388
		internal const string Argument_FrameworkNameTooShort = "Argument_FrameworkNameTooShort";

		// Token: 0x0400056D RID: 1389
		internal const string Argument_FrameworkNameInvalid = "Argument_FrameworkNameInvalid";

		// Token: 0x0400056E RID: 1390
		internal const string Argument_FrameworkNameInvalidVersion = "Argument_FrameworkNameInvalidVersion";

		// Token: 0x0400056F RID: 1391
		internal const string Argument_FrameworkNameMissingVersion = "Argument_FrameworkNameMissingVersion";

		// Token: 0x04000570 RID: 1392
		internal const string ArgumentNull_Key = "ArgumentNull_Key";

		// Token: 0x04000571 RID: 1393
		internal const string Argument_AddingDuplicate = "Argument_AddingDuplicate";

		// Token: 0x04000572 RID: 1394
		internal const string Argument_InvalidValue = "Argument_InvalidValue";

		// Token: 0x04000573 RID: 1395
		internal const string ArgumentOutOfRange_NeedNonNegNum = "ArgumentOutOfRange_NeedNonNegNum";

		// Token: 0x04000574 RID: 1396
		internal const string ArgumentOutOfRange_InvalidThreshold = "ArgumentOutOfRange_InvalidThreshold";

		// Token: 0x04000575 RID: 1397
		internal const string InvalidOperation_EnumFailedVersion = "InvalidOperation_EnumFailedVersion";

		// Token: 0x04000576 RID: 1398
		internal const string InvalidOperation_EnumOpCantHappen = "InvalidOperation_EnumOpCantHappen";

		// Token: 0x04000577 RID: 1399
		internal const string Arg_MultiRank = "Arg_MultiRank";

		// Token: 0x04000578 RID: 1400
		internal const string Arg_NonZeroLowerBound = "Arg_NonZeroLowerBound";

		// Token: 0x04000579 RID: 1401
		internal const string Arg_InsufficientSpace = "Arg_InsufficientSpace";

		// Token: 0x0400057A RID: 1402
		internal const string NotSupported_EnumeratorReset = "NotSupported_EnumeratorReset";

		// Token: 0x0400057B RID: 1403
		internal const string Invalid_Array_Type = "Invalid_Array_Type";

		// Token: 0x0400057C RID: 1404
		internal const string Serialization_InvalidOnDeser = "Serialization_InvalidOnDeser";

		// Token: 0x0400057D RID: 1405
		internal const string Serialization_MissingValues = "Serialization_MissingValues";

		// Token: 0x0400057E RID: 1406
		internal const string Serialization_MismatchedCount = "Serialization_MismatchedCount";

		// Token: 0x0400057F RID: 1407
		internal const string ExternalLinkedListNode = "ExternalLinkedListNode";

		// Token: 0x04000580 RID: 1408
		internal const string LinkedListNodeIsAttached = "LinkedListNodeIsAttached";

		// Token: 0x04000581 RID: 1409
		internal const string LinkedListEmpty = "LinkedListEmpty";

		// Token: 0x04000582 RID: 1410
		internal const string Arg_WrongType = "Arg_WrongType";

		// Token: 0x04000583 RID: 1411
		internal const string Argument_ItemNotExist = "Argument_ItemNotExist";

		// Token: 0x04000584 RID: 1412
		internal const string Argument_ImplementIComparable = "Argument_ImplementIComparable";

		// Token: 0x04000585 RID: 1413
		internal const string InvalidOperation_EmptyCollection = "InvalidOperation_EmptyCollection";

		// Token: 0x04000586 RID: 1414
		internal const string InvalidOperation_EmptyQueue = "InvalidOperation_EmptyQueue";

		// Token: 0x04000587 RID: 1415
		internal const string InvalidOperation_EmptyStack = "InvalidOperation_EmptyStack";

		// Token: 0x04000588 RID: 1416
		internal const string InvalidOperation_CannotRemoveFromStackOrQueue = "InvalidOperation_CannotRemoveFromStackOrQueue";

		// Token: 0x04000589 RID: 1417
		internal const string ArgumentOutOfRange_Index = "ArgumentOutOfRange_Index";

		// Token: 0x0400058A RID: 1418
		internal const string ArgumentOutOfRange_SmallCapacity = "ArgumentOutOfRange_SmallCapacity";

		// Token: 0x0400058B RID: 1419
		internal const string Arg_ArrayPlusOffTooSmall = "Arg_ArrayPlusOffTooSmall";

		// Token: 0x0400058C RID: 1420
		internal const string NotSupported_KeyCollectionSet = "NotSupported_KeyCollectionSet";

		// Token: 0x0400058D RID: 1421
		internal const string NotSupported_ValueCollectionSet = "NotSupported_ValueCollectionSet";

		// Token: 0x0400058E RID: 1422
		internal const string NotSupported_ReadOnlyCollection = "NotSupported_ReadOnlyCollection";

		// Token: 0x0400058F RID: 1423
		internal const string NotSupported_SortedListNestedWrite = "NotSupported_SortedListNestedWrite";

		// Token: 0x04000590 RID: 1424
		internal const string BlockingCollection_ctor_BoundedCapacityRange = "BlockingCollection_ctor_BoundedCapacityRange";

		// Token: 0x04000591 RID: 1425
		internal const string BlockingCollection_ctor_CountMoreThanCapacity = "BlockingCollection_ctor_CountMoreThanCapacity";

		// Token: 0x04000592 RID: 1426
		internal const string BlockingCollection_Add_ConcurrentCompleteAdd = "BlockingCollection_Add_ConcurrentCompleteAdd";

		// Token: 0x04000593 RID: 1427
		internal const string BlockingCollection_Add_Failed = "BlockingCollection_Add_Failed";

		// Token: 0x04000594 RID: 1428
		internal const string BlockingCollection_Take_CollectionModified = "BlockingCollection_Take_CollectionModified";

		// Token: 0x04000595 RID: 1429
		internal const string BlockingCollection_Completed = "BlockingCollection_Completed";

		// Token: 0x04000596 RID: 1430
		internal const string BlockingCollection_Disposed = "BlockingCollection_Disposed";

		// Token: 0x04000597 RID: 1431
		internal const string BlockingCollection_TimeoutInvalid = "BlockingCollection_TimeoutInvalid";

		// Token: 0x04000598 RID: 1432
		internal const string BlockingCollection_CantTakeWhenDone = "BlockingCollection_CantTakeWhenDone";

		// Token: 0x04000599 RID: 1433
		internal const string BlockingCollection_CantAddAnyWhenCompleted = "BlockingCollection_CantAddAnyWhenCompleted";

		// Token: 0x0400059A RID: 1434
		internal const string BlockingCollection_CantTakeAnyWhenAllDone = "BlockingCollection_CantTakeAnyWhenAllDone";

		// Token: 0x0400059B RID: 1435
		internal const string BlockingCollection_ValidateCollectionsArray_ZeroSize = "BlockingCollection_ValidateCollectionsArray_ZeroSize";

		// Token: 0x0400059C RID: 1436
		internal const string BlockingCollection_ValidateCollectionsArray_LargeSize = "BlockingCollection_ValidateCollectionsArray_LargeSize";

		// Token: 0x0400059D RID: 1437
		internal const string BlockingCollection_ValidateCollectionsArray_NullElems = "BlockingCollection_ValidateCollectionsArray_NullElems";

		// Token: 0x0400059E RID: 1438
		internal const string BlockingCollection_ValidateCollectionsArray_DispElems = "BlockingCollection_ValidateCollectionsArray_DispElems";

		// Token: 0x0400059F RID: 1439
		internal const string BlockingCollection_CompleteAdding_AlreadyDone = "BlockingCollection_CompleteAdding_AlreadyDone";

		// Token: 0x040005A0 RID: 1440
		internal const string BlockingCollection_CopyTo_NonNegative = "BlockingCollection_CopyTo_NonNegative";

		// Token: 0x040005A1 RID: 1441
		internal const string BlockingCollection_CopyTo_TooManyElems = "BlockingCollection_CopyTo_TooManyElems";

		// Token: 0x040005A2 RID: 1442
		internal const string BlockingCollection_CopyTo_MultiDim = "BlockingCollection_CopyTo_MultiDim";

		// Token: 0x040005A3 RID: 1443
		internal const string BlockingCollection_CopyTo_IncorrectType = "BlockingCollection_CopyTo_IncorrectType";

		// Token: 0x040005A4 RID: 1444
		internal const string ConcurrentBag_Ctor_ArgumentNullException = "ConcurrentBag_Ctor_ArgumentNullException";

		// Token: 0x040005A5 RID: 1445
		internal const string ConcurrentBag_CopyTo_ArgumentNullException = "ConcurrentBag_CopyTo_ArgumentNullException";

		// Token: 0x040005A6 RID: 1446
		internal const string ConcurrentBag_CopyTo_ArgumentOutOfRangeException = "ConcurrentBag_CopyTo_ArgumentOutOfRangeException";

		// Token: 0x040005A7 RID: 1447
		internal const string ConcurrentBag_CopyTo_ArgumentException_IndexGreaterThanLength = "ConcurrentBag_CopyTo_ArgumentException_IndexGreaterThanLength";

		// Token: 0x040005A8 RID: 1448
		internal const string ConcurrentBag_CopyTo_ArgumentException_NoEnoughSpace = "ConcurrentBag_CopyTo_ArgumentException_NoEnoughSpace";

		// Token: 0x040005A9 RID: 1449
		internal const string ConcurrentBag_CopyTo_ArgumentException_InvalidArrayType = "ConcurrentBag_CopyTo_ArgumentException_InvalidArrayType";

		// Token: 0x040005AA RID: 1450
		internal const string ConcurrentCollection_SyncRoot_NotSupported = "ConcurrentCollection_SyncRoot_NotSupported";

		// Token: 0x040005AB RID: 1451
		internal const string Common_OperationCanceled = "Common_OperationCanceled";

		// Token: 0x040005AC RID: 1452
		internal const string Barrier_ctor_ArgumentOutOfRange = "Barrier_ctor_ArgumentOutOfRange";

		// Token: 0x040005AD RID: 1453
		internal const string Barrier_AddParticipants_NonPositive_ArgumentOutOfRange = "Barrier_AddParticipants_NonPositive_ArgumentOutOfRange";

		// Token: 0x040005AE RID: 1454
		internal const string Barrier_AddParticipants_Overflow_ArgumentOutOfRange = "Barrier_AddParticipants_Overflow_ArgumentOutOfRange";

		// Token: 0x040005AF RID: 1455
		internal const string Barrier_InvalidOperation_CalledFromPHA = "Barrier_InvalidOperation_CalledFromPHA";

		// Token: 0x040005B0 RID: 1456
		internal const string Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange = "Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange";

		// Token: 0x040005B1 RID: 1457
		internal const string Barrier_RemoveParticipants_ArgumentOutOfRange = "Barrier_RemoveParticipants_ArgumentOutOfRange";

		// Token: 0x040005B2 RID: 1458
		internal const string Barrier_RemoveParticipants_InvalidOperation = "Barrier_RemoveParticipants_InvalidOperation";

		// Token: 0x040005B3 RID: 1459
		internal const string Barrier_SignalAndWait_ArgumentOutOfRange = "Barrier_SignalAndWait_ArgumentOutOfRange";

		// Token: 0x040005B4 RID: 1460
		internal const string Barrier_SignalAndWait_InvalidOperation_ZeroTotal = "Barrier_SignalAndWait_InvalidOperation_ZeroTotal";

		// Token: 0x040005B5 RID: 1461
		internal const string Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded = "Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded";

		// Token: 0x040005B6 RID: 1462
		internal const string Barrier_Dispose = "Barrier_Dispose";

		// Token: 0x040005B7 RID: 1463
		internal const string BarrierPostPhaseException = "BarrierPostPhaseException";

		// Token: 0x040005B8 RID: 1464
		internal const string UriTypeConverter_ConvertFrom_CannotConvert = "UriTypeConverter_ConvertFrom_CannotConvert";

		// Token: 0x040005B9 RID: 1465
		internal const string UriTypeConverter_ConvertTo_CannotConvert = "UriTypeConverter_ConvertTo_CannotConvert";

		// Token: 0x040005BA RID: 1466
		internal const string ISupportInitializeDescr = "ISupportInitializeDescr";

		// Token: 0x040005BB RID: 1467
		internal const string CantModifyListSortDescriptionCollection = "CantModifyListSortDescriptionCollection";

		// Token: 0x040005BC RID: 1468
		internal const string Argument_NullComment = "Argument_NullComment";

		// Token: 0x040005BD RID: 1469
		internal const string InvalidPrimitiveType = "InvalidPrimitiveType";

		// Token: 0x040005BE RID: 1470
		internal const string Cannot_Specify_Both_Compiler_Path_And_Version = "Cannot_Specify_Both_Compiler_Path_And_Version";

		// Token: 0x040005BF RID: 1471
		internal const string CodeGenOutputWriter = "CodeGenOutputWriter";

		// Token: 0x040005C0 RID: 1472
		internal const string CodeGenReentrance = "CodeGenReentrance";

		// Token: 0x040005C1 RID: 1473
		internal const string InvalidLanguageIdentifier = "InvalidLanguageIdentifier";

		// Token: 0x040005C2 RID: 1474
		internal const string InvalidTypeName = "InvalidTypeName";

		// Token: 0x040005C3 RID: 1475
		internal const string Empty_attribute = "Empty_attribute";

		// Token: 0x040005C4 RID: 1476
		internal const string Invalid_nonnegative_integer_attribute = "Invalid_nonnegative_integer_attribute";

		// Token: 0x040005C5 RID: 1477
		internal const string CodeDomProvider_NotDefined = "CodeDomProvider_NotDefined";

		// Token: 0x040005C6 RID: 1478
		internal const string Language_Names_Cannot_Be_Empty = "Language_Names_Cannot_Be_Empty";

		// Token: 0x040005C7 RID: 1479
		internal const string Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based = "Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based";

		// Token: 0x040005C8 RID: 1480
		internal const string Unable_To_Locate_Type = "Unable_To_Locate_Type";

		// Token: 0x040005C9 RID: 1481
		internal const string NotSupported_CodeDomAPI = "NotSupported_CodeDomAPI";

		// Token: 0x040005CA RID: 1482
		internal const string ArityDoesntMatch = "ArityDoesntMatch";

		// Token: 0x040005CB RID: 1483
		internal const string PartialTrustErrorTextReplacement = "PartialTrustErrorTextReplacement";

		// Token: 0x040005CC RID: 1484
		internal const string PartialTrustIllegalProvider = "PartialTrustIllegalProvider";

		// Token: 0x040005CD RID: 1485
		internal const string IllegalAssemblyReference = "IllegalAssemblyReference";

		// Token: 0x040005CE RID: 1486
		internal const string NullOrEmpty_Value_in_Property = "NullOrEmpty_Value_in_Property";

		// Token: 0x040005CF RID: 1487
		internal const string AutoGen_Comment_Line1 = "AutoGen_Comment_Line1";

		// Token: 0x040005D0 RID: 1488
		internal const string AutoGen_Comment_Line2 = "AutoGen_Comment_Line2";

		// Token: 0x040005D1 RID: 1489
		internal const string AutoGen_Comment_Line3 = "AutoGen_Comment_Line3";

		// Token: 0x040005D2 RID: 1490
		internal const string AutoGen_Comment_Line4 = "AutoGen_Comment_Line4";

		// Token: 0x040005D3 RID: 1491
		internal const string AutoGen_Comment_Line5 = "AutoGen_Comment_Line5";

		// Token: 0x040005D4 RID: 1492
		internal const string CantContainNullEntries = "CantContainNullEntries";

		// Token: 0x040005D5 RID: 1493
		internal const string InvalidPathCharsInChecksum = "InvalidPathCharsInChecksum";

		// Token: 0x040005D6 RID: 1494
		internal const string InvalidRegion = "InvalidRegion";

		// Token: 0x040005D7 RID: 1495
		internal const string Provider_does_not_support_options = "Provider_does_not_support_options";

		// Token: 0x040005D8 RID: 1496
		internal const string FileIntegrityCheckFailed = "FileIntegrityCheckFailed";

		// Token: 0x040005D9 RID: 1497
		internal const string MetaExtenderName = "MetaExtenderName";

		// Token: 0x040005DA RID: 1498
		internal const string InvalidEnumArgument = "InvalidEnumArgument";

		// Token: 0x040005DB RID: 1499
		internal const string InvalidArgument = "InvalidArgument";

		// Token: 0x040005DC RID: 1500
		internal const string InvalidNullArgument = "InvalidNullArgument";

		// Token: 0x040005DD RID: 1501
		internal const string InvalidNullEmptyArgument = "InvalidNullEmptyArgument";

		// Token: 0x040005DE RID: 1502
		internal const string LicExceptionTypeOnly = "LicExceptionTypeOnly";

		// Token: 0x040005DF RID: 1503
		internal const string LicExceptionTypeAndInstance = "LicExceptionTypeAndInstance";

		// Token: 0x040005E0 RID: 1504
		internal const string LicMgrContextCannotBeChanged = "LicMgrContextCannotBeChanged";

		// Token: 0x040005E1 RID: 1505
		internal const string LicMgrAlreadyLocked = "LicMgrAlreadyLocked";

		// Token: 0x040005E2 RID: 1506
		internal const string LicMgrDifferentUser = "LicMgrDifferentUser";

		// Token: 0x040005E3 RID: 1507
		internal const string InvalidElementType = "InvalidElementType";

		// Token: 0x040005E4 RID: 1508
		internal const string InvalidIdentifier = "InvalidIdentifier";

		// Token: 0x040005E5 RID: 1509
		internal const string ExecFailedToCreate = "ExecFailedToCreate";

		// Token: 0x040005E6 RID: 1510
		internal const string ExecTimeout = "ExecTimeout";

		// Token: 0x040005E7 RID: 1511
		internal const string ExecBadreturn = "ExecBadreturn";

		// Token: 0x040005E8 RID: 1512
		internal const string ExecCantGetRetCode = "ExecCantGetRetCode";

		// Token: 0x040005E9 RID: 1513
		internal const string ExecCantExec = "ExecCantExec";

		// Token: 0x040005EA RID: 1514
		internal const string ExecCantRevert = "ExecCantRevert";

		// Token: 0x040005EB RID: 1515
		internal const string CompilerNotFound = "CompilerNotFound";

		// Token: 0x040005EC RID: 1516
		internal const string DuplicateFileName = "DuplicateFileName";

		// Token: 0x040005ED RID: 1517
		internal const string CollectionReadOnly = "CollectionReadOnly";

		// Token: 0x040005EE RID: 1518
		internal const string BitVectorFull = "BitVectorFull";

		// Token: 0x040005EF RID: 1519
		internal const string ArrayConverterText = "ArrayConverterText";

		// Token: 0x040005F0 RID: 1520
		internal const string CollectionConverterText = "CollectionConverterText";

		// Token: 0x040005F1 RID: 1521
		internal const string MultilineStringConverterText = "MultilineStringConverterText";

		// Token: 0x040005F2 RID: 1522
		internal const string CultureInfoConverterDefaultCultureString = "CultureInfoConverterDefaultCultureString";

		// Token: 0x040005F3 RID: 1523
		internal const string CultureInfoConverterInvalidCulture = "CultureInfoConverterInvalidCulture";

		// Token: 0x040005F4 RID: 1524
		internal const string InvalidPrimitive = "InvalidPrimitive";

		// Token: 0x040005F5 RID: 1525
		internal const string TimerInvalidInterval = "TimerInvalidInterval";

		// Token: 0x040005F6 RID: 1526
		internal const string TraceSwitchLevelTooHigh = "TraceSwitchLevelTooHigh";

		// Token: 0x040005F7 RID: 1527
		internal const string TraceSwitchLevelTooLow = "TraceSwitchLevelTooLow";

		// Token: 0x040005F8 RID: 1528
		internal const string TraceSwitchInvalidLevel = "TraceSwitchInvalidLevel";

		// Token: 0x040005F9 RID: 1529
		internal const string TraceListenerIndentSize = "TraceListenerIndentSize";

		// Token: 0x040005FA RID: 1530
		internal const string TraceListenerFail = "TraceListenerFail";

		// Token: 0x040005FB RID: 1531
		internal const string TraceAsTraceSource = "TraceAsTraceSource";

		// Token: 0x040005FC RID: 1532
		internal const string InvalidLowBoundArgument = "InvalidLowBoundArgument";

		// Token: 0x040005FD RID: 1533
		internal const string DuplicateComponentName = "DuplicateComponentName";

		// Token: 0x040005FE RID: 1534
		internal const string NotImplemented = "NotImplemented";

		// Token: 0x040005FF RID: 1535
		internal const string OutOfMemory = "OutOfMemory";

		// Token: 0x04000600 RID: 1536
		internal const string EOF = "EOF";

		// Token: 0x04000601 RID: 1537
		internal const string IOError = "IOError";

		// Token: 0x04000602 RID: 1538
		internal const string BadChar = "BadChar";

		// Token: 0x04000603 RID: 1539
		internal const string toStringNone = "toStringNone";

		// Token: 0x04000604 RID: 1540
		internal const string toStringUnknown = "toStringUnknown";

		// Token: 0x04000605 RID: 1541
		internal const string InvalidEnum = "InvalidEnum";

		// Token: 0x04000606 RID: 1542
		internal const string IndexOutOfRange = "IndexOutOfRange";

		// Token: 0x04000607 RID: 1543
		internal const string ErrorPropertyAccessorException = "ErrorPropertyAccessorException";

		// Token: 0x04000608 RID: 1544
		internal const string InvalidOperation = "InvalidOperation";

		// Token: 0x04000609 RID: 1545
		internal const string EmptyStack = "EmptyStack";

		// Token: 0x0400060A RID: 1546
		internal const string PerformanceCounterDesc = "PerformanceCounterDesc";

		// Token: 0x0400060B RID: 1547
		internal const string PCCategoryName = "PCCategoryName";

		// Token: 0x0400060C RID: 1548
		internal const string PCCounterName = "PCCounterName";

		// Token: 0x0400060D RID: 1549
		internal const string PCInstanceName = "PCInstanceName";

		// Token: 0x0400060E RID: 1550
		internal const string PCMachineName = "PCMachineName";

		// Token: 0x0400060F RID: 1551
		internal const string PCInstanceLifetime = "PCInstanceLifetime";

		// Token: 0x04000610 RID: 1552
		internal const string PropertyCategoryAction = "PropertyCategoryAction";

		// Token: 0x04000611 RID: 1553
		internal const string PropertyCategoryAppearance = "PropertyCategoryAppearance";

		// Token: 0x04000612 RID: 1554
		internal const string PropertyCategoryAsynchronous = "PropertyCategoryAsynchronous";

		// Token: 0x04000613 RID: 1555
		internal const string PropertyCategoryBehavior = "PropertyCategoryBehavior";

		// Token: 0x04000614 RID: 1556
		internal const string PropertyCategoryData = "PropertyCategoryData";

		// Token: 0x04000615 RID: 1557
		internal const string PropertyCategoryDDE = "PropertyCategoryDDE";

		// Token: 0x04000616 RID: 1558
		internal const string PropertyCategoryDesign = "PropertyCategoryDesign";

		// Token: 0x04000617 RID: 1559
		internal const string PropertyCategoryDragDrop = "PropertyCategoryDragDrop";

		// Token: 0x04000618 RID: 1560
		internal const string PropertyCategoryFocus = "PropertyCategoryFocus";

		// Token: 0x04000619 RID: 1561
		internal const string PropertyCategoryFont = "PropertyCategoryFont";

		// Token: 0x0400061A RID: 1562
		internal const string PropertyCategoryFormat = "PropertyCategoryFormat";

		// Token: 0x0400061B RID: 1563
		internal const string PropertyCategoryKey = "PropertyCategoryKey";

		// Token: 0x0400061C RID: 1564
		internal const string PropertyCategoryList = "PropertyCategoryList";

		// Token: 0x0400061D RID: 1565
		internal const string PropertyCategoryLayout = "PropertyCategoryLayout";

		// Token: 0x0400061E RID: 1566
		internal const string PropertyCategoryDefault = "PropertyCategoryDefault";

		// Token: 0x0400061F RID: 1567
		internal const string PropertyCategoryMouse = "PropertyCategoryMouse";

		// Token: 0x04000620 RID: 1568
		internal const string PropertyCategoryPosition = "PropertyCategoryPosition";

		// Token: 0x04000621 RID: 1569
		internal const string PropertyCategoryText = "PropertyCategoryText";

		// Token: 0x04000622 RID: 1570
		internal const string PropertyCategoryScale = "PropertyCategoryScale";

		// Token: 0x04000623 RID: 1571
		internal const string PropertyCategoryWindowStyle = "PropertyCategoryWindowStyle";

		// Token: 0x04000624 RID: 1572
		internal const string PropertyCategoryConfig = "PropertyCategoryConfig";

		// Token: 0x04000625 RID: 1573
		internal const string ArgumentNull_ArrayWithNullElements = "ArgumentNull_ArrayWithNullElements";

		// Token: 0x04000626 RID: 1574
		internal const string OnlyAllowedOnce = "OnlyAllowedOnce";

		// Token: 0x04000627 RID: 1575
		internal const string BeginIndexNotNegative = "BeginIndexNotNegative";

		// Token: 0x04000628 RID: 1576
		internal const string LengthNotNegative = "LengthNotNegative";

		// Token: 0x04000629 RID: 1577
		internal const string UnimplementedState = "UnimplementedState";

		// Token: 0x0400062A RID: 1578
		internal const string UnexpectedOpcode = "UnexpectedOpcode";

		// Token: 0x0400062B RID: 1579
		internal const string NoResultOnFailed = "NoResultOnFailed";

		// Token: 0x0400062C RID: 1580
		internal const string UnterminatedBracket = "UnterminatedBracket";

		// Token: 0x0400062D RID: 1581
		internal const string TooManyParens = "TooManyParens";

		// Token: 0x0400062E RID: 1582
		internal const string NestedQuantify = "NestedQuantify";

		// Token: 0x0400062F RID: 1583
		internal const string QuantifyAfterNothing = "QuantifyAfterNothing";

		// Token: 0x04000630 RID: 1584
		internal const string InternalError = "InternalError";

		// Token: 0x04000631 RID: 1585
		internal const string IllegalRange = "IllegalRange";

		// Token: 0x04000632 RID: 1586
		internal const string NotEnoughParens = "NotEnoughParens";

		// Token: 0x04000633 RID: 1587
		internal const string BadClassInCharRange = "BadClassInCharRange";

		// Token: 0x04000634 RID: 1588
		internal const string ReversedCharRange = "ReversedCharRange";

		// Token: 0x04000635 RID: 1589
		internal const string UndefinedReference = "UndefinedReference";

		// Token: 0x04000636 RID: 1590
		internal const string MalformedReference = "MalformedReference";

		// Token: 0x04000637 RID: 1591
		internal const string UnrecognizedGrouping = "UnrecognizedGrouping";

		// Token: 0x04000638 RID: 1592
		internal const string UnterminatedComment = "UnterminatedComment";

		// Token: 0x04000639 RID: 1593
		internal const string IllegalEndEscape = "IllegalEndEscape";

		// Token: 0x0400063A RID: 1594
		internal const string MalformedNameRef = "MalformedNameRef";

		// Token: 0x0400063B RID: 1595
		internal const string UndefinedBackref = "UndefinedBackref";

		// Token: 0x0400063C RID: 1596
		internal const string UndefinedNameRef = "UndefinedNameRef";

		// Token: 0x0400063D RID: 1597
		internal const string TooFewHex = "TooFewHex";

		// Token: 0x0400063E RID: 1598
		internal const string MissingControl = "MissingControl";

		// Token: 0x0400063F RID: 1599
		internal const string UnrecognizedControl = "UnrecognizedControl";

		// Token: 0x04000640 RID: 1600
		internal const string UnrecognizedEscape = "UnrecognizedEscape";

		// Token: 0x04000641 RID: 1601
		internal const string IllegalCondition = "IllegalCondition";

		// Token: 0x04000642 RID: 1602
		internal const string TooManyAlternates = "TooManyAlternates";

		// Token: 0x04000643 RID: 1603
		internal const string MakeException = "MakeException";

		// Token: 0x04000644 RID: 1604
		internal const string IncompleteSlashP = "IncompleteSlashP";

		// Token: 0x04000645 RID: 1605
		internal const string MalformedSlashP = "MalformedSlashP";

		// Token: 0x04000646 RID: 1606
		internal const string InvalidGroupName = "InvalidGroupName";

		// Token: 0x04000647 RID: 1607
		internal const string CapnumNotZero = "CapnumNotZero";

		// Token: 0x04000648 RID: 1608
		internal const string AlternationCantCapture = "AlternationCantCapture";

		// Token: 0x04000649 RID: 1609
		internal const string AlternationCantHaveComment = "AlternationCantHaveComment";

		// Token: 0x0400064A RID: 1610
		internal const string CaptureGroupOutOfRange = "CaptureGroupOutOfRange";

		// Token: 0x0400064B RID: 1611
		internal const string SubtractionMustBeLast = "SubtractionMustBeLast";

		// Token: 0x0400064C RID: 1612
		internal const string UnknownProperty = "UnknownProperty";

		// Token: 0x0400064D RID: 1613
		internal const string ReplacementError = "ReplacementError";

		// Token: 0x0400064E RID: 1614
		internal const string CountTooSmall = "CountTooSmall";

		// Token: 0x0400064F RID: 1615
		internal const string EnumNotStarted = "EnumNotStarted";

		// Token: 0x04000650 RID: 1616
		internal const string Arg_InvalidArrayType = "Arg_InvalidArrayType";

		// Token: 0x04000651 RID: 1617
		internal const string Arg_RankMultiDimNotSupported = "Arg_RankMultiDimNotSupported";

		// Token: 0x04000652 RID: 1618
		internal const string RegexMatchTimeoutException_Occurred = "RegexMatchTimeoutException_Occurred";

		// Token: 0x04000653 RID: 1619
		internal const string IllegalDefaultRegexMatchTimeoutInAppDomain = "IllegalDefaultRegexMatchTimeoutInAppDomain";

		// Token: 0x04000654 RID: 1620
		internal const string FileObject_AlreadyOpen = "FileObject_AlreadyOpen";

		// Token: 0x04000655 RID: 1621
		internal const string FileObject_Closed = "FileObject_Closed";

		// Token: 0x04000656 RID: 1622
		internal const string FileObject_NotWhileWriting = "FileObject_NotWhileWriting";

		// Token: 0x04000657 RID: 1623
		internal const string FileObject_FileDoesNotExist = "FileObject_FileDoesNotExist";

		// Token: 0x04000658 RID: 1624
		internal const string FileObject_MustBeClosed = "FileObject_MustBeClosed";

		// Token: 0x04000659 RID: 1625
		internal const string FileObject_MustBeFileName = "FileObject_MustBeFileName";

		// Token: 0x0400065A RID: 1626
		internal const string FileObject_InvalidInternalState = "FileObject_InvalidInternalState";

		// Token: 0x0400065B RID: 1627
		internal const string FileObject_PathNotSet = "FileObject_PathNotSet";

		// Token: 0x0400065C RID: 1628
		internal const string FileObject_Reading = "FileObject_Reading";

		// Token: 0x0400065D RID: 1629
		internal const string FileObject_Writing = "FileObject_Writing";

		// Token: 0x0400065E RID: 1630
		internal const string FileObject_InvalidEnumeration = "FileObject_InvalidEnumeration";

		// Token: 0x0400065F RID: 1631
		internal const string FileObject_NoReset = "FileObject_NoReset";

		// Token: 0x04000660 RID: 1632
		internal const string DirectoryObject_MustBeDirName = "DirectoryObject_MustBeDirName";

		// Token: 0x04000661 RID: 1633
		internal const string DirectoryObjectPathDescr = "DirectoryObjectPathDescr";

		// Token: 0x04000662 RID: 1634
		internal const string FileObjectDetectEncodingDescr = "FileObjectDetectEncodingDescr";

		// Token: 0x04000663 RID: 1635
		internal const string FileObjectEncodingDescr = "FileObjectEncodingDescr";

		// Token: 0x04000664 RID: 1636
		internal const string FileObjectPathDescr = "FileObjectPathDescr";

		// Token: 0x04000665 RID: 1637
		internal const string Arg_EnumIllegalVal = "Arg_EnumIllegalVal";

		// Token: 0x04000666 RID: 1638
		internal const string Arg_OutOfRange_NeedNonNegNum = "Arg_OutOfRange_NeedNonNegNum";

		// Token: 0x04000667 RID: 1639
		internal const string Argument_InvalidPermissionState = "Argument_InvalidPermissionState";

		// Token: 0x04000668 RID: 1640
		internal const string Argument_InvalidOidValue = "Argument_InvalidOidValue";

		// Token: 0x04000669 RID: 1641
		internal const string Argument_WrongType = "Argument_WrongType";

		// Token: 0x0400066A RID: 1642
		internal const string Arg_EmptyOrNullString = "Arg_EmptyOrNullString";

		// Token: 0x0400066B RID: 1643
		internal const string Arg_EmptyOrNullArray = "Arg_EmptyOrNullArray";

		// Token: 0x0400066C RID: 1644
		internal const string Argument_InvalidClassAttribute = "Argument_InvalidClassAttribute";

		// Token: 0x0400066D RID: 1645
		internal const string Argument_InvalidNameType = "Argument_InvalidNameType";

		// Token: 0x0400066E RID: 1646
		internal const string InvalidOperation_EnumNotStarted = "InvalidOperation_EnumNotStarted";

		// Token: 0x0400066F RID: 1647
		internal const string InvalidOperation_DuplicateItemNotAllowed = "InvalidOperation_DuplicateItemNotAllowed";

		// Token: 0x04000670 RID: 1648
		internal const string Cryptography_Asn_MismatchedOidInCollection = "Cryptography_Asn_MismatchedOidInCollection";

		// Token: 0x04000671 RID: 1649
		internal const string Cryptography_Cms_Envelope_Empty_Content = "Cryptography_Cms_Envelope_Empty_Content";

		// Token: 0x04000672 RID: 1650
		internal const string Cryptography_Cms_Invalid_Recipient_Info_Type = "Cryptography_Cms_Invalid_Recipient_Info_Type";

		// Token: 0x04000673 RID: 1651
		internal const string Cryptography_Cms_Invalid_Subject_Identifier_Type = "Cryptography_Cms_Invalid_Subject_Identifier_Type";

		// Token: 0x04000674 RID: 1652
		internal const string Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch = "Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch";

		// Token: 0x04000675 RID: 1653
		internal const string Cryptography_Cms_Key_Agree_Date_Not_Available = "Cryptography_Cms_Key_Agree_Date_Not_Available";

		// Token: 0x04000676 RID: 1654
		internal const string Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available = "Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available";

		// Token: 0x04000677 RID: 1655
		internal const string Cryptography_Cms_MessageNotSigned = "Cryptography_Cms_MessageNotSigned";

		// Token: 0x04000678 RID: 1656
		internal const string Cryptography_Cms_MessageNotSignedByNoSignature = "Cryptography_Cms_MessageNotSignedByNoSignature";

		// Token: 0x04000679 RID: 1657
		internal const string Cryptography_Cms_MessageNotEncrypted = "Cryptography_Cms_MessageNotEncrypted";

		// Token: 0x0400067A RID: 1658
		internal const string Cryptography_Cms_Not_Supported = "Cryptography_Cms_Not_Supported";

		// Token: 0x0400067B RID: 1659
		internal const string Cryptography_Cms_RecipientCertificateNotFound = "Cryptography_Cms_RecipientCertificateNotFound";

		// Token: 0x0400067C RID: 1660
		internal const string Cryptography_Cms_Sign_Empty_Content = "Cryptography_Cms_Sign_Empty_Content";

		// Token: 0x0400067D RID: 1661
		internal const string Cryptography_Cms_Sign_No_Signature_First_Signer = "Cryptography_Cms_Sign_No_Signature_First_Signer";

		// Token: 0x0400067E RID: 1662
		internal const string Cryptography_DpApi_InvalidMemoryLength = "Cryptography_DpApi_InvalidMemoryLength";

		// Token: 0x0400067F RID: 1663
		internal const string Cryptography_InvalidHandle = "Cryptography_InvalidHandle";

		// Token: 0x04000680 RID: 1664
		internal const string Cryptography_InvalidContextHandle = "Cryptography_InvalidContextHandle";

		// Token: 0x04000681 RID: 1665
		internal const string Cryptography_InvalidStoreHandle = "Cryptography_InvalidStoreHandle";

		// Token: 0x04000682 RID: 1666
		internal const string Cryptography_Oid_InvalidValue = "Cryptography_Oid_InvalidValue";

		// Token: 0x04000683 RID: 1667
		internal const string Cryptography_Pkcs9_ExplicitAddNotAllowed = "Cryptography_Pkcs9_ExplicitAddNotAllowed";

		// Token: 0x04000684 RID: 1668
		internal const string Cryptography_Pkcs9_InvalidOid = "Cryptography_Pkcs9_InvalidOid";

		// Token: 0x04000685 RID: 1669
		internal const string Cryptography_Pkcs9_MultipleSigningTimeNotAllowed = "Cryptography_Pkcs9_MultipleSigningTimeNotAllowed";

		// Token: 0x04000686 RID: 1670
		internal const string Cryptography_Pkcs9_AttributeMismatch = "Cryptography_Pkcs9_AttributeMismatch";

		// Token: 0x04000687 RID: 1671
		internal const string Cryptography_X509_AddFailed = "Cryptography_X509_AddFailed";

		// Token: 0x04000688 RID: 1672
		internal const string Cryptography_X509_BadEncoding = "Cryptography_X509_BadEncoding";

		// Token: 0x04000689 RID: 1673
		internal const string Cryptography_X509_ExportFailed = "Cryptography_X509_ExportFailed";

		// Token: 0x0400068A RID: 1674
		internal const string Cryptography_X509_ExtensionMismatch = "Cryptography_X509_ExtensionMismatch";

		// Token: 0x0400068B RID: 1675
		internal const string Cryptography_X509_InvalidFindType = "Cryptography_X509_InvalidFindType";

		// Token: 0x0400068C RID: 1676
		internal const string Cryptography_X509_InvalidFindValue = "Cryptography_X509_InvalidFindValue";

		// Token: 0x0400068D RID: 1677
		internal const string Cryptography_X509_InvalidEncodingFormat = "Cryptography_X509_InvalidEncodingFormat";

		// Token: 0x0400068E RID: 1678
		internal const string Cryptography_X509_InvalidContentType = "Cryptography_X509_InvalidContentType";

		// Token: 0x0400068F RID: 1679
		internal const string Cryptography_X509_KeyMismatch = "Cryptography_X509_KeyMismatch";

		// Token: 0x04000690 RID: 1680
		internal const string Cryptography_X509_RemoveFailed = "Cryptography_X509_RemoveFailed";

		// Token: 0x04000691 RID: 1681
		internal const string Cryptography_X509_StoreNotOpen = "Cryptography_X509_StoreNotOpen";

		// Token: 0x04000692 RID: 1682
		internal const string Environment_NotInteractive = "Environment_NotInteractive";

		// Token: 0x04000693 RID: 1683
		internal const string NotSupported_InvalidKeyImpl = "NotSupported_InvalidKeyImpl";

		// Token: 0x04000694 RID: 1684
		internal const string NotSupported_KeyAlgorithm = "NotSupported_KeyAlgorithm";

		// Token: 0x04000695 RID: 1685
		internal const string NotSupported_PlatformRequiresNT = "NotSupported_PlatformRequiresNT";

		// Token: 0x04000696 RID: 1686
		internal const string NotSupported_UnreadableStream = "NotSupported_UnreadableStream";

		// Token: 0x04000697 RID: 1687
		internal const string Security_InvalidValue = "Security_InvalidValue";

		// Token: 0x04000698 RID: 1688
		internal const string Unknown_Error = "Unknown_Error";

		// Token: 0x04000699 RID: 1689
		internal const string security_ServiceNameCollection_EmptyServiceName = "security_ServiceNameCollection_EmptyServiceName";

		// Token: 0x0400069A RID: 1690
		internal const string security_ExtendedProtectionPolicy_UseDifferentConstructorForNever = "security_ExtendedProtectionPolicy_UseDifferentConstructorForNever";

		// Token: 0x0400069B RID: 1691
		internal const string security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection = "security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection";

		// Token: 0x0400069C RID: 1692
		internal const string security_ExtendedProtection_NoOSSupport = "security_ExtendedProtection_NoOSSupport";

		// Token: 0x0400069D RID: 1693
		internal const string net_nonClsCompliantException = "net_nonClsCompliantException";

		// Token: 0x0400069E RID: 1694
		internal const string net_illegalConfigWith = "net_illegalConfigWith";

		// Token: 0x0400069F RID: 1695
		internal const string net_illegalConfigWithout = "net_illegalConfigWithout";

		// Token: 0x040006A0 RID: 1696
		internal const string net_baddate = "net_baddate";

		// Token: 0x040006A1 RID: 1697
		internal const string net_writestarted = "net_writestarted";

		// Token: 0x040006A2 RID: 1698
		internal const string net_clsmall = "net_clsmall";

		// Token: 0x040006A3 RID: 1699
		internal const string net_reqsubmitted = "net_reqsubmitted";

		// Token: 0x040006A4 RID: 1700
		internal const string net_rspsubmitted = "net_rspsubmitted";

		// Token: 0x040006A5 RID: 1701
		internal const string net_ftp_no_http_cmd = "net_ftp_no_http_cmd";

		// Token: 0x040006A6 RID: 1702
		internal const string net_ftp_invalid_method_name = "net_ftp_invalid_method_name";

		// Token: 0x040006A7 RID: 1703
		internal const string net_ftp_invalid_renameto = "net_ftp_invalid_renameto";

		// Token: 0x040006A8 RID: 1704
		internal const string net_ftp_no_defaultcreds = "net_ftp_no_defaultcreds";

		// Token: 0x040006A9 RID: 1705
		internal const string net_ftpnoresponse = "net_ftpnoresponse";

		// Token: 0x040006AA RID: 1706
		internal const string net_ftp_response_invalid_format = "net_ftp_response_invalid_format";

		// Token: 0x040006AB RID: 1707
		internal const string net_ftp_no_offsetforhttp = "net_ftp_no_offsetforhttp";

		// Token: 0x040006AC RID: 1708
		internal const string net_ftp_invalid_uri = "net_ftp_invalid_uri";

		// Token: 0x040006AD RID: 1709
		internal const string net_ftp_invalid_status_response = "net_ftp_invalid_status_response";

		// Token: 0x040006AE RID: 1710
		internal const string net_ftp_server_failed_passive = "net_ftp_server_failed_passive";

		// Token: 0x040006AF RID: 1711
		internal const string net_ftp_active_address_different = "net_ftp_active_address_different";

		// Token: 0x040006B0 RID: 1712
		internal const string net_ftp_proxy_does_not_support_ssl = "net_ftp_proxy_does_not_support_ssl";

		// Token: 0x040006B1 RID: 1713
		internal const string net_ftp_invalid_response_filename = "net_ftp_invalid_response_filename";

		// Token: 0x040006B2 RID: 1714
		internal const string net_ftp_unsupported_method = "net_ftp_unsupported_method";

		// Token: 0x040006B3 RID: 1715
		internal const string net_resubmitcanceled = "net_resubmitcanceled";

		// Token: 0x040006B4 RID: 1716
		internal const string net_redirect_perm = "net_redirect_perm";

		// Token: 0x040006B5 RID: 1717
		internal const string net_resubmitprotofailed = "net_resubmitprotofailed";

		// Token: 0x040006B6 RID: 1718
		internal const string net_needchunked = "net_needchunked";

		// Token: 0x040006B7 RID: 1719
		internal const string net_nochunked = "net_nochunked";

		// Token: 0x040006B8 RID: 1720
		internal const string net_nochunkuploadonhttp10 = "net_nochunkuploadonhttp10";

		// Token: 0x040006B9 RID: 1721
		internal const string net_connarg = "net_connarg";

		// Token: 0x040006BA RID: 1722
		internal const string net_no100 = "net_no100";

		// Token: 0x040006BB RID: 1723
		internal const string net_fromto = "net_fromto";

		// Token: 0x040006BC RID: 1724
		internal const string net_rangetoosmall = "net_rangetoosmall";

		// Token: 0x040006BD RID: 1725
		internal const string net_entitytoobig = "net_entitytoobig";

		// Token: 0x040006BE RID: 1726
		internal const string net_invalidversion = "net_invalidversion";

		// Token: 0x040006BF RID: 1727
		internal const string net_invalidstatus = "net_invalidstatus";

		// Token: 0x040006C0 RID: 1728
		internal const string net_toosmall = "net_toosmall";

		// Token: 0x040006C1 RID: 1729
		internal const string net_toolong = "net_toolong";

		// Token: 0x040006C2 RID: 1730
		internal const string net_connclosed = "net_connclosed";

		// Token: 0x040006C3 RID: 1731
		internal const string net_noseek = "net_noseek";

		// Token: 0x040006C4 RID: 1732
		internal const string net_servererror = "net_servererror";

		// Token: 0x040006C5 RID: 1733
		internal const string net_nouploadonget = "net_nouploadonget";

		// Token: 0x040006C6 RID: 1734
		internal const string net_mutualauthfailed = "net_mutualauthfailed";

		// Token: 0x040006C7 RID: 1735
		internal const string net_invasync = "net_invasync";

		// Token: 0x040006C8 RID: 1736
		internal const string net_inasync = "net_inasync";

		// Token: 0x040006C9 RID: 1737
		internal const string net_mustbeuri = "net_mustbeuri";

		// Token: 0x040006CA RID: 1738
		internal const string net_format_shexp = "net_format_shexp";

		// Token: 0x040006CB RID: 1739
		internal const string net_cannot_load_proxy_helper = "net_cannot_load_proxy_helper";

		// Token: 0x040006CC RID: 1740
		internal const string net_invalid_host = "net_invalid_host";

		// Token: 0x040006CD RID: 1741
		internal const string net_repcall = "net_repcall";

		// Token: 0x040006CE RID: 1742
		internal const string net_wrongversion = "net_wrongversion";

		// Token: 0x040006CF RID: 1743
		internal const string net_badmethod = "net_badmethod";

		// Token: 0x040006D0 RID: 1744
		internal const string net_io_notenoughbyteswritten = "net_io_notenoughbyteswritten";

		// Token: 0x040006D1 RID: 1745
		internal const string net_io_timeout_use_ge_zero = "net_io_timeout_use_ge_zero";

		// Token: 0x040006D2 RID: 1746
		internal const string net_io_timeout_use_gt_zero = "net_io_timeout_use_gt_zero";

		// Token: 0x040006D3 RID: 1747
		internal const string net_io_no_0timeouts = "net_io_no_0timeouts";

		// Token: 0x040006D4 RID: 1748
		internal const string net_requestaborted = "net_requestaborted";

		// Token: 0x040006D5 RID: 1749
		internal const string net_tooManyRedirections = "net_tooManyRedirections";

		// Token: 0x040006D6 RID: 1750
		internal const string net_authmodulenotregistered = "net_authmodulenotregistered";

		// Token: 0x040006D7 RID: 1751
		internal const string net_authschemenotregistered = "net_authschemenotregistered";

		// Token: 0x040006D8 RID: 1752
		internal const string net_proxyschemenotsupported = "net_proxyschemenotsupported";

		// Token: 0x040006D9 RID: 1753
		internal const string net_maxsrvpoints = "net_maxsrvpoints";

		// Token: 0x040006DA RID: 1754
		internal const string net_unknown_prefix = "net_unknown_prefix";

		// Token: 0x040006DB RID: 1755
		internal const string net_notconnected = "net_notconnected";

		// Token: 0x040006DC RID: 1756
		internal const string net_notstream = "net_notstream";

		// Token: 0x040006DD RID: 1757
		internal const string net_timeout = "net_timeout";

		// Token: 0x040006DE RID: 1758
		internal const string net_nocontentlengthonget = "net_nocontentlengthonget";

		// Token: 0x040006DF RID: 1759
		internal const string net_contentlengthmissing = "net_contentlengthmissing";

		// Token: 0x040006E0 RID: 1760
		internal const string net_nonhttpproxynotallowed = "net_nonhttpproxynotallowed";

		// Token: 0x040006E1 RID: 1761
		internal const string net_nottoken = "net_nottoken";

		// Token: 0x040006E2 RID: 1762
		internal const string net_rangetype = "net_rangetype";

		// Token: 0x040006E3 RID: 1763
		internal const string net_need_writebuffering = "net_need_writebuffering";

		// Token: 0x040006E4 RID: 1764
		internal const string net_securitypackagesupport = "net_securitypackagesupport";

		// Token: 0x040006E5 RID: 1765
		internal const string net_securityprotocolnotsupported = "net_securityprotocolnotsupported";

		// Token: 0x040006E6 RID: 1766
		internal const string net_nodefaultcreds = "net_nodefaultcreds";

		// Token: 0x040006E7 RID: 1767
		internal const string net_stopped = "net_stopped";

		// Token: 0x040006E8 RID: 1768
		internal const string net_udpconnected = "net_udpconnected";

		// Token: 0x040006E9 RID: 1769
		internal const string net_readonlystream = "net_readonlystream";

		// Token: 0x040006EA RID: 1770
		internal const string net_writeonlystream = "net_writeonlystream";

		// Token: 0x040006EB RID: 1771
		internal const string net_no_concurrent_io_allowed = "net_no_concurrent_io_allowed";

		// Token: 0x040006EC RID: 1772
		internal const string net_needmorethreads = "net_needmorethreads";

		// Token: 0x040006ED RID: 1773
		internal const string net_MethodNotImplementedException = "net_MethodNotImplementedException";

		// Token: 0x040006EE RID: 1774
		internal const string net_PropertyNotImplementedException = "net_PropertyNotImplementedException";

		// Token: 0x040006EF RID: 1775
		internal const string net_MethodNotSupportedException = "net_MethodNotSupportedException";

		// Token: 0x040006F0 RID: 1776
		internal const string net_PropertyNotSupportedException = "net_PropertyNotSupportedException";

		// Token: 0x040006F1 RID: 1777
		internal const string net_ProtocolNotSupportedException = "net_ProtocolNotSupportedException";

		// Token: 0x040006F2 RID: 1778
		internal const string net_SelectModeNotSupportedException = "net_SelectModeNotSupportedException";

		// Token: 0x040006F3 RID: 1779
		internal const string net_InvalidSocketHandle = "net_InvalidSocketHandle";

		// Token: 0x040006F4 RID: 1780
		internal const string net_InvalidAddressFamily = "net_InvalidAddressFamily";

		// Token: 0x040006F5 RID: 1781
		internal const string net_InvalidEndPointAddressFamily = "net_InvalidEndPointAddressFamily";

		// Token: 0x040006F6 RID: 1782
		internal const string net_InvalidSocketAddressSize = "net_InvalidSocketAddressSize";

		// Token: 0x040006F7 RID: 1783
		internal const string net_invalidAddressList = "net_invalidAddressList";

		// Token: 0x040006F8 RID: 1784
		internal const string net_invalidPingBufferSize = "net_invalidPingBufferSize";

		// Token: 0x040006F9 RID: 1785
		internal const string net_cant_perform_during_shutdown = "net_cant_perform_during_shutdown";

		// Token: 0x040006FA RID: 1786
		internal const string net_cant_create_environment = "net_cant_create_environment";

		// Token: 0x040006FB RID: 1787
		internal const string net_completed_result = "net_completed_result";

		// Token: 0x040006FC RID: 1788
		internal const string net_protocol_invalid_family = "net_protocol_invalid_family";

		// Token: 0x040006FD RID: 1789
		internal const string net_protocol_invalid_multicast_family = "net_protocol_invalid_multicast_family";

		// Token: 0x040006FE RID: 1790
		internal const string net_empty_osinstalltype = "net_empty_osinstalltype";

		// Token: 0x040006FF RID: 1791
		internal const string net_unknown_osinstalltype = "net_unknown_osinstalltype";

		// Token: 0x04000700 RID: 1792
		internal const string net_cant_determine_osinstalltype = "net_cant_determine_osinstalltype";

		// Token: 0x04000701 RID: 1793
		internal const string net_osinstalltype = "net_osinstalltype";

		// Token: 0x04000702 RID: 1794
		internal const string net_entire_body_not_written = "net_entire_body_not_written";

		// Token: 0x04000703 RID: 1795
		internal const string net_must_provide_request_body = "net_must_provide_request_body";

		// Token: 0x04000704 RID: 1796
		internal const string net_ssp_dont_support_cbt = "net_ssp_dont_support_cbt";

		// Token: 0x04000705 RID: 1797
		internal const string net_sockets_zerolist = "net_sockets_zerolist";

		// Token: 0x04000706 RID: 1798
		internal const string net_sockets_blocking = "net_sockets_blocking";

		// Token: 0x04000707 RID: 1799
		internal const string net_sockets_useblocking = "net_sockets_useblocking";

		// Token: 0x04000708 RID: 1800
		internal const string net_sockets_select = "net_sockets_select";

		// Token: 0x04000709 RID: 1801
		internal const string net_sockets_toolarge_select = "net_sockets_toolarge_select";

		// Token: 0x0400070A RID: 1802
		internal const string net_sockets_empty_select = "net_sockets_empty_select";

		// Token: 0x0400070B RID: 1803
		internal const string net_sockets_mustbind = "net_sockets_mustbind";

		// Token: 0x0400070C RID: 1804
		internal const string net_sockets_mustlisten = "net_sockets_mustlisten";

		// Token: 0x0400070D RID: 1805
		internal const string net_sockets_mustnotlisten = "net_sockets_mustnotlisten";

		// Token: 0x0400070E RID: 1806
		internal const string net_sockets_mustnotbebound = "net_sockets_mustnotbebound";

		// Token: 0x0400070F RID: 1807
		internal const string net_sockets_namedmustnotbebound = "net_sockets_namedmustnotbebound";

		// Token: 0x04000710 RID: 1808
		internal const string net_sockets_invalid_socketinformation = "net_sockets_invalid_socketinformation";

		// Token: 0x04000711 RID: 1809
		internal const string net_sockets_invalid_ipaddress_length = "net_sockets_invalid_ipaddress_length";

		// Token: 0x04000712 RID: 1810
		internal const string net_sockets_invalid_optionValue = "net_sockets_invalid_optionValue";

		// Token: 0x04000713 RID: 1811
		internal const string net_sockets_invalid_optionValue_all = "net_sockets_invalid_optionValue_all";

		// Token: 0x04000714 RID: 1812
		internal const string net_sockets_invalid_dnsendpoint = "net_sockets_invalid_dnsendpoint";

		// Token: 0x04000715 RID: 1813
		internal const string net_sockets_disconnectedConnect = "net_sockets_disconnectedConnect";

		// Token: 0x04000716 RID: 1814
		internal const string net_sockets_disconnectedAccept = "net_sockets_disconnectedAccept";

		// Token: 0x04000717 RID: 1815
		internal const string net_tcplistener_mustbestopped = "net_tcplistener_mustbestopped";

		// Token: 0x04000718 RID: 1816
		internal const string net_sockets_no_duplicate_async = "net_sockets_no_duplicate_async";

		// Token: 0x04000719 RID: 1817
		internal const string net_socketopinprogress = "net_socketopinprogress";

		// Token: 0x0400071A RID: 1818
		internal const string net_buffercounttoosmall = "net_buffercounttoosmall";

		// Token: 0x0400071B RID: 1819
		internal const string net_multibuffernotsupported = "net_multibuffernotsupported";

		// Token: 0x0400071C RID: 1820
		internal const string net_ambiguousbuffers = "net_ambiguousbuffers";

		// Token: 0x0400071D RID: 1821
		internal const string net_sockets_ipv6only = "net_sockets_ipv6only";

		// Token: 0x0400071E RID: 1822
		internal const string net_perfcounter_initialized_success = "net_perfcounter_initialized_success";

		// Token: 0x0400071F RID: 1823
		internal const string net_perfcounter_initialized_error = "net_perfcounter_initialized_error";

		// Token: 0x04000720 RID: 1824
		internal const string net_perfcounter_nocategory = "net_perfcounter_nocategory";

		// Token: 0x04000721 RID: 1825
		internal const string net_perfcounter_initialization_started = "net_perfcounter_initialization_started";

		// Token: 0x04000722 RID: 1826
		internal const string net_perfcounter_cant_queue_workitem = "net_perfcounter_cant_queue_workitem";

		// Token: 0x04000723 RID: 1827
		internal const string net_config_proxy = "net_config_proxy";

		// Token: 0x04000724 RID: 1828
		internal const string net_config_proxy_module_not_public = "net_config_proxy_module_not_public";

		// Token: 0x04000725 RID: 1829
		internal const string net_config_authenticationmodules = "net_config_authenticationmodules";

		// Token: 0x04000726 RID: 1830
		internal const string net_config_webrequestmodules = "net_config_webrequestmodules";

		// Token: 0x04000727 RID: 1831
		internal const string net_config_requestcaching = "net_config_requestcaching";

		// Token: 0x04000728 RID: 1832
		internal const string net_config_section_permission = "net_config_section_permission";

		// Token: 0x04000729 RID: 1833
		internal const string net_config_element_permission = "net_config_element_permission";

		// Token: 0x0400072A RID: 1834
		internal const string net_config_property_permission = "net_config_property_permission";

		// Token: 0x0400072B RID: 1835
		internal const string net_WebResponseParseError_InvalidHeaderName = "net_WebResponseParseError_InvalidHeaderName";

		// Token: 0x0400072C RID: 1836
		internal const string net_WebResponseParseError_InvalidContentLength = "net_WebResponseParseError_InvalidContentLength";

		// Token: 0x0400072D RID: 1837
		internal const string net_WebResponseParseError_IncompleteHeaderLine = "net_WebResponseParseError_IncompleteHeaderLine";

		// Token: 0x0400072E RID: 1838
		internal const string net_WebResponseParseError_CrLfError = "net_WebResponseParseError_CrLfError";

		// Token: 0x0400072F RID: 1839
		internal const string net_WebResponseParseError_InvalidChunkFormat = "net_WebResponseParseError_InvalidChunkFormat";

		// Token: 0x04000730 RID: 1840
		internal const string net_WebResponseParseError_UnexpectedServerResponse = "net_WebResponseParseError_UnexpectedServerResponse";

		// Token: 0x04000731 RID: 1841
		internal const string net_webstatus_Success = "net_webstatus_Success";

		// Token: 0x04000732 RID: 1842
		internal const string net_webstatus_NameResolutionFailure = "net_webstatus_NameResolutionFailure";

		// Token: 0x04000733 RID: 1843
		internal const string net_webstatus_ConnectFailure = "net_webstatus_ConnectFailure";

		// Token: 0x04000734 RID: 1844
		internal const string net_webstatus_ReceiveFailure = "net_webstatus_ReceiveFailure";

		// Token: 0x04000735 RID: 1845
		internal const string net_webstatus_SendFailure = "net_webstatus_SendFailure";

		// Token: 0x04000736 RID: 1846
		internal const string net_webstatus_PipelineFailure = "net_webstatus_PipelineFailure";

		// Token: 0x04000737 RID: 1847
		internal const string net_webstatus_RequestCanceled = "net_webstatus_RequestCanceled";

		// Token: 0x04000738 RID: 1848
		internal const string net_webstatus_ConnectionClosed = "net_webstatus_ConnectionClosed";

		// Token: 0x04000739 RID: 1849
		internal const string net_webstatus_TrustFailure = "net_webstatus_TrustFailure";

		// Token: 0x0400073A RID: 1850
		internal const string net_webstatus_SecureChannelFailure = "net_webstatus_SecureChannelFailure";

		// Token: 0x0400073B RID: 1851
		internal const string net_webstatus_ServerProtocolViolation = "net_webstatus_ServerProtocolViolation";

		// Token: 0x0400073C RID: 1852
		internal const string net_webstatus_KeepAliveFailure = "net_webstatus_KeepAliveFailure";

		// Token: 0x0400073D RID: 1853
		internal const string net_webstatus_ProxyNameResolutionFailure = "net_webstatus_ProxyNameResolutionFailure";

		// Token: 0x0400073E RID: 1854
		internal const string net_webstatus_MessageLengthLimitExceeded = "net_webstatus_MessageLengthLimitExceeded";

		// Token: 0x0400073F RID: 1855
		internal const string net_webstatus_CacheEntryNotFound = "net_webstatus_CacheEntryNotFound";

		// Token: 0x04000740 RID: 1856
		internal const string net_webstatus_RequestProhibitedByCachePolicy = "net_webstatus_RequestProhibitedByCachePolicy";

		// Token: 0x04000741 RID: 1857
		internal const string net_webstatus_Timeout = "net_webstatus_Timeout";

		// Token: 0x04000742 RID: 1858
		internal const string net_webstatus_RequestProhibitedByProxy = "net_webstatus_RequestProhibitedByProxy";

		// Token: 0x04000743 RID: 1859
		internal const string net_InvalidStatusCode = "net_InvalidStatusCode";

		// Token: 0x04000744 RID: 1860
		internal const string net_ftpstatuscode_ServiceNotAvailable = "net_ftpstatuscode_ServiceNotAvailable";

		// Token: 0x04000745 RID: 1861
		internal const string net_ftpstatuscode_CantOpenData = "net_ftpstatuscode_CantOpenData";

		// Token: 0x04000746 RID: 1862
		internal const string net_ftpstatuscode_ConnectionClosed = "net_ftpstatuscode_ConnectionClosed";

		// Token: 0x04000747 RID: 1863
		internal const string net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy = "net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy";

		// Token: 0x04000748 RID: 1864
		internal const string net_ftpstatuscode_ActionAbortedLocalProcessingError = "net_ftpstatuscode_ActionAbortedLocalProcessingError";

		// Token: 0x04000749 RID: 1865
		internal const string net_ftpstatuscode_ActionNotTakenInsufficentSpace = "net_ftpstatuscode_ActionNotTakenInsufficentSpace";

		// Token: 0x0400074A RID: 1866
		internal const string net_ftpstatuscode_CommandSyntaxError = "net_ftpstatuscode_CommandSyntaxError";

		// Token: 0x0400074B RID: 1867
		internal const string net_ftpstatuscode_ArgumentSyntaxError = "net_ftpstatuscode_ArgumentSyntaxError";

		// Token: 0x0400074C RID: 1868
		internal const string net_ftpstatuscode_CommandNotImplemented = "net_ftpstatuscode_CommandNotImplemented";

		// Token: 0x0400074D RID: 1869
		internal const string net_ftpstatuscode_BadCommandSequence = "net_ftpstatuscode_BadCommandSequence";

		// Token: 0x0400074E RID: 1870
		internal const string net_ftpstatuscode_NotLoggedIn = "net_ftpstatuscode_NotLoggedIn";

		// Token: 0x0400074F RID: 1871
		internal const string net_ftpstatuscode_AccountNeeded = "net_ftpstatuscode_AccountNeeded";

		// Token: 0x04000750 RID: 1872
		internal const string net_ftpstatuscode_ActionNotTakenFileUnavailable = "net_ftpstatuscode_ActionNotTakenFileUnavailable";

		// Token: 0x04000751 RID: 1873
		internal const string net_ftpstatuscode_ActionAbortedUnknownPageType = "net_ftpstatuscode_ActionAbortedUnknownPageType";

		// Token: 0x04000752 RID: 1874
		internal const string net_ftpstatuscode_FileActionAborted = "net_ftpstatuscode_FileActionAborted";

		// Token: 0x04000753 RID: 1875
		internal const string net_ftpstatuscode_ActionNotTakenFilenameNotAllowed = "net_ftpstatuscode_ActionNotTakenFilenameNotAllowed";

		// Token: 0x04000754 RID: 1876
		internal const string net_httpstatuscode_NoContent = "net_httpstatuscode_NoContent";

		// Token: 0x04000755 RID: 1877
		internal const string net_httpstatuscode_NonAuthoritativeInformation = "net_httpstatuscode_NonAuthoritativeInformation";

		// Token: 0x04000756 RID: 1878
		internal const string net_httpstatuscode_ResetContent = "net_httpstatuscode_ResetContent";

		// Token: 0x04000757 RID: 1879
		internal const string net_httpstatuscode_PartialContent = "net_httpstatuscode_PartialContent";

		// Token: 0x04000758 RID: 1880
		internal const string net_httpstatuscode_MultipleChoices = "net_httpstatuscode_MultipleChoices";

		// Token: 0x04000759 RID: 1881
		internal const string net_httpstatuscode_Ambiguous = "net_httpstatuscode_Ambiguous";

		// Token: 0x0400075A RID: 1882
		internal const string net_httpstatuscode_MovedPermanently = "net_httpstatuscode_MovedPermanently";

		// Token: 0x0400075B RID: 1883
		internal const string net_httpstatuscode_Moved = "net_httpstatuscode_Moved";

		// Token: 0x0400075C RID: 1884
		internal const string net_httpstatuscode_Found = "net_httpstatuscode_Found";

		// Token: 0x0400075D RID: 1885
		internal const string net_httpstatuscode_Redirect = "net_httpstatuscode_Redirect";

		// Token: 0x0400075E RID: 1886
		internal const string net_httpstatuscode_SeeOther = "net_httpstatuscode_SeeOther";

		// Token: 0x0400075F RID: 1887
		internal const string net_httpstatuscode_RedirectMethod = "net_httpstatuscode_RedirectMethod";

		// Token: 0x04000760 RID: 1888
		internal const string net_httpstatuscode_NotModified = "net_httpstatuscode_NotModified";

		// Token: 0x04000761 RID: 1889
		internal const string net_httpstatuscode_UseProxy = "net_httpstatuscode_UseProxy";

		// Token: 0x04000762 RID: 1890
		internal const string net_httpstatuscode_TemporaryRedirect = "net_httpstatuscode_TemporaryRedirect";

		// Token: 0x04000763 RID: 1891
		internal const string net_httpstatuscode_RedirectKeepVerb = "net_httpstatuscode_RedirectKeepVerb";

		// Token: 0x04000764 RID: 1892
		internal const string net_httpstatuscode_BadRequest = "net_httpstatuscode_BadRequest";

		// Token: 0x04000765 RID: 1893
		internal const string net_httpstatuscode_Unauthorized = "net_httpstatuscode_Unauthorized";

		// Token: 0x04000766 RID: 1894
		internal const string net_httpstatuscode_PaymentRequired = "net_httpstatuscode_PaymentRequired";

		// Token: 0x04000767 RID: 1895
		internal const string net_httpstatuscode_Forbidden = "net_httpstatuscode_Forbidden";

		// Token: 0x04000768 RID: 1896
		internal const string net_httpstatuscode_NotFound = "net_httpstatuscode_NotFound";

		// Token: 0x04000769 RID: 1897
		internal const string net_httpstatuscode_MethodNotAllowed = "net_httpstatuscode_MethodNotAllowed";

		// Token: 0x0400076A RID: 1898
		internal const string net_httpstatuscode_NotAcceptable = "net_httpstatuscode_NotAcceptable";

		// Token: 0x0400076B RID: 1899
		internal const string net_httpstatuscode_ProxyAuthenticationRequired = "net_httpstatuscode_ProxyAuthenticationRequired";

		// Token: 0x0400076C RID: 1900
		internal const string net_httpstatuscode_RequestTimeout = "net_httpstatuscode_RequestTimeout";

		// Token: 0x0400076D RID: 1901
		internal const string net_httpstatuscode_Conflict = "net_httpstatuscode_Conflict";

		// Token: 0x0400076E RID: 1902
		internal const string net_httpstatuscode_Gone = "net_httpstatuscode_Gone";

		// Token: 0x0400076F RID: 1903
		internal const string net_httpstatuscode_LengthRequired = "net_httpstatuscode_LengthRequired";

		// Token: 0x04000770 RID: 1904
		internal const string net_httpstatuscode_InternalServerError = "net_httpstatuscode_InternalServerError";

		// Token: 0x04000771 RID: 1905
		internal const string net_httpstatuscode_NotImplemented = "net_httpstatuscode_NotImplemented";

		// Token: 0x04000772 RID: 1906
		internal const string net_httpstatuscode_BadGateway = "net_httpstatuscode_BadGateway";

		// Token: 0x04000773 RID: 1907
		internal const string net_httpstatuscode_ServiceUnavailable = "net_httpstatuscode_ServiceUnavailable";

		// Token: 0x04000774 RID: 1908
		internal const string net_httpstatuscode_GatewayTimeout = "net_httpstatuscode_GatewayTimeout";

		// Token: 0x04000775 RID: 1909
		internal const string net_httpstatuscode_HttpVersionNotSupported = "net_httpstatuscode_HttpVersionNotSupported";

		// Token: 0x04000776 RID: 1910
		internal const string net_uri_BadScheme = "net_uri_BadScheme";

		// Token: 0x04000777 RID: 1911
		internal const string net_uri_BadFormat = "net_uri_BadFormat";

		// Token: 0x04000778 RID: 1912
		internal const string net_uri_BadUserPassword = "net_uri_BadUserPassword";

		// Token: 0x04000779 RID: 1913
		internal const string net_uri_BadHostName = "net_uri_BadHostName";

		// Token: 0x0400077A RID: 1914
		internal const string net_uri_BadAuthority = "net_uri_BadAuthority";

		// Token: 0x0400077B RID: 1915
		internal const string net_uri_BadAuthorityTerminator = "net_uri_BadAuthorityTerminator";

		// Token: 0x0400077C RID: 1916
		internal const string net_uri_EmptyUri = "net_uri_EmptyUri";

		// Token: 0x0400077D RID: 1917
		internal const string net_uri_BadString = "net_uri_BadString";

		// Token: 0x0400077E RID: 1918
		internal const string net_uri_MustRootedPath = "net_uri_MustRootedPath";

		// Token: 0x0400077F RID: 1919
		internal const string net_uri_BadPort = "net_uri_BadPort";

		// Token: 0x04000780 RID: 1920
		internal const string net_uri_SizeLimit = "net_uri_SizeLimit";

		// Token: 0x04000781 RID: 1921
		internal const string net_uri_SchemeLimit = "net_uri_SchemeLimit";

		// Token: 0x04000782 RID: 1922
		internal const string net_uri_NotAbsolute = "net_uri_NotAbsolute";

		// Token: 0x04000783 RID: 1923
		internal const string net_uri_PortOutOfRange = "net_uri_PortOutOfRange";

		// Token: 0x04000784 RID: 1924
		internal const string net_uri_UserDrivenParsing = "net_uri_UserDrivenParsing";

		// Token: 0x04000785 RID: 1925
		internal const string net_uri_AlreadyRegistered = "net_uri_AlreadyRegistered";

		// Token: 0x04000786 RID: 1926
		internal const string net_uri_NeedFreshParser = "net_uri_NeedFreshParser";

		// Token: 0x04000787 RID: 1927
		internal const string net_uri_CannotCreateRelative = "net_uri_CannotCreateRelative";

		// Token: 0x04000788 RID: 1928
		internal const string net_uri_InvalidUriKind = "net_uri_InvalidUriKind";

		// Token: 0x04000789 RID: 1929
		internal const string net_uri_BadUnicodeHostForIdn = "net_uri_BadUnicodeHostForIdn";

		// Token: 0x0400078A RID: 1930
		internal const string net_uri_GenericAuthorityNotDnsSafe = "net_uri_GenericAuthorityNotDnsSafe";

		// Token: 0x0400078B RID: 1931
		internal const string net_uri_NotJustSerialization = "net_uri_NotJustSerialization";

		// Token: 0x0400078C RID: 1932
		internal const string net_emptystringcall = "net_emptystringcall";

		// Token: 0x0400078D RID: 1933
		internal const string net_emptystringset = "net_emptystringset";

		// Token: 0x0400078E RID: 1934
		internal const string net_headers_req = "net_headers_req";

		// Token: 0x0400078F RID: 1935
		internal const string net_headers_rsp = "net_headers_rsp";

		// Token: 0x04000790 RID: 1936
		internal const string net_headers_toolong = "net_headers_toolong";

		// Token: 0x04000791 RID: 1937
		internal const string net_WebHeaderInvalidControlChars = "net_WebHeaderInvalidControlChars";

		// Token: 0x04000792 RID: 1938
		internal const string net_WebHeaderInvalidCRLFChars = "net_WebHeaderInvalidCRLFChars";

		// Token: 0x04000793 RID: 1939
		internal const string net_WebHeaderInvalidHeaderChars = "net_WebHeaderInvalidHeaderChars";

		// Token: 0x04000794 RID: 1940
		internal const string net_WebHeaderInvalidNonAsciiChars = "net_WebHeaderInvalidNonAsciiChars";

		// Token: 0x04000795 RID: 1941
		internal const string net_WebHeaderMissingColon = "net_WebHeaderMissingColon";

		// Token: 0x04000796 RID: 1942
		internal const string net_headerrestrict = "net_headerrestrict";

		// Token: 0x04000797 RID: 1943
		internal const string net_io_completionportwasbound = "net_io_completionportwasbound";

		// Token: 0x04000798 RID: 1944
		internal const string net_io_writefailure = "net_io_writefailure";

		// Token: 0x04000799 RID: 1945
		internal const string net_io_readfailure = "net_io_readfailure";

		// Token: 0x0400079A RID: 1946
		internal const string net_io_connectionclosed = "net_io_connectionclosed";

		// Token: 0x0400079B RID: 1947
		internal const string net_io_transportfailure = "net_io_transportfailure";

		// Token: 0x0400079C RID: 1948
		internal const string net_io_internal_bind = "net_io_internal_bind";

		// Token: 0x0400079D RID: 1949
		internal const string net_io_invalidasyncresult = "net_io_invalidasyncresult";

		// Token: 0x0400079E RID: 1950
		internal const string net_io_invalidnestedcall = "net_io_invalidnestedcall";

		// Token: 0x0400079F RID: 1951
		internal const string net_io_invalidendcall = "net_io_invalidendcall";

		// Token: 0x040007A0 RID: 1952
		internal const string net_io_must_be_rw_stream = "net_io_must_be_rw_stream";

		// Token: 0x040007A1 RID: 1953
		internal const string net_io_header_id = "net_io_header_id";

		// Token: 0x040007A2 RID: 1954
		internal const string net_io_out_range = "net_io_out_range";

		// Token: 0x040007A3 RID: 1955
		internal const string net_io_encrypt = "net_io_encrypt";

		// Token: 0x040007A4 RID: 1956
		internal const string net_io_decrypt = "net_io_decrypt";

		// Token: 0x040007A5 RID: 1957
		internal const string net_io_read = "net_io_read";

		// Token: 0x040007A6 RID: 1958
		internal const string net_io_write = "net_io_write";

		// Token: 0x040007A7 RID: 1959
		internal const string net_io_eof = "net_io_eof";

		// Token: 0x040007A8 RID: 1960
		internal const string net_io_async_result = "net_io_async_result";

		// Token: 0x040007A9 RID: 1961
		internal const string net_listener_mustcall = "net_listener_mustcall";

		// Token: 0x040007AA RID: 1962
		internal const string net_listener_mustcompletecall = "net_listener_mustcompletecall";

		// Token: 0x040007AB RID: 1963
		internal const string net_listener_callinprogress = "net_listener_callinprogress";

		// Token: 0x040007AC RID: 1964
		internal const string net_listener_scheme = "net_listener_scheme";

		// Token: 0x040007AD RID: 1965
		internal const string net_listener_host = "net_listener_host";

		// Token: 0x040007AE RID: 1966
		internal const string net_listener_slash = "net_listener_slash";

		// Token: 0x040007AF RID: 1967
		internal const string net_listener_repcall = "net_listener_repcall";

		// Token: 0x040007B0 RID: 1968
		internal const string net_listener_invalid_cbt_type = "net_listener_invalid_cbt_type";

		// Token: 0x040007B1 RID: 1969
		internal const string net_listener_no_spns = "net_listener_no_spns";

		// Token: 0x040007B2 RID: 1970
		internal const string net_listener_cannot_set_custom_cbt = "net_listener_cannot_set_custom_cbt";

		// Token: 0x040007B3 RID: 1971
		internal const string net_listener_cbt_not_supported = "net_listener_cbt_not_supported";

		// Token: 0x040007B4 RID: 1972
		internal const string net_listener_detach_error = "net_listener_detach_error";

		// Token: 0x040007B5 RID: 1973
		internal const string net_listener_close_urlgroup_error = "net_listener_close_urlgroup_error";

		// Token: 0x040007B6 RID: 1974
		internal const string net_tls_version = "net_tls_version";

		// Token: 0x040007B7 RID: 1975
		internal const string net_perm_target = "net_perm_target";

		// Token: 0x040007B8 RID: 1976
		internal const string net_perm_both_regex = "net_perm_both_regex";

		// Token: 0x040007B9 RID: 1977
		internal const string net_perm_none = "net_perm_none";

		// Token: 0x040007BA RID: 1978
		internal const string net_perm_attrib_count = "net_perm_attrib_count";

		// Token: 0x040007BB RID: 1979
		internal const string net_perm_invalid_val = "net_perm_invalid_val";

		// Token: 0x040007BC RID: 1980
		internal const string net_perm_attrib_multi = "net_perm_attrib_multi";

		// Token: 0x040007BD RID: 1981
		internal const string net_perm_epname = "net_perm_epname";

		// Token: 0x040007BE RID: 1982
		internal const string net_perm_invalid_val_in_element = "net_perm_invalid_val_in_element";

		// Token: 0x040007BF RID: 1983
		internal const string net_invalid_ip_addr = "net_invalid_ip_addr";

		// Token: 0x040007C0 RID: 1984
		internal const string dns_bad_ip_address = "dns_bad_ip_address";

		// Token: 0x040007C1 RID: 1985
		internal const string net_bad_mac_address = "net_bad_mac_address";

		// Token: 0x040007C2 RID: 1986
		internal const string net_ping = "net_ping";

		// Token: 0x040007C3 RID: 1987
		internal const string net_bad_ip_address_prefix = "net_bad_ip_address_prefix";

		// Token: 0x040007C4 RID: 1988
		internal const string net_max_ip_address_list_length_exceeded = "net_max_ip_address_list_length_exceeded";

		// Token: 0x040007C5 RID: 1989
		internal const string net_ipv4_not_installed = "net_ipv4_not_installed";

		// Token: 0x040007C6 RID: 1990
		internal const string net_ipv6_not_installed = "net_ipv6_not_installed";

		// Token: 0x040007C7 RID: 1991
		internal const string net_webclient = "net_webclient";

		// Token: 0x040007C8 RID: 1992
		internal const string net_webclient_ContentType = "net_webclient_ContentType";

		// Token: 0x040007C9 RID: 1993
		internal const string net_webclient_Multipart = "net_webclient_Multipart";

		// Token: 0x040007CA RID: 1994
		internal const string net_webclient_no_concurrent_io_allowed = "net_webclient_no_concurrent_io_allowed";

		// Token: 0x040007CB RID: 1995
		internal const string net_webclient_invalid_baseaddress = "net_webclient_invalid_baseaddress";

		// Token: 0x040007CC RID: 1996
		internal const string net_container_add_cookie = "net_container_add_cookie";

		// Token: 0x040007CD RID: 1997
		internal const string net_cookie_invalid = "net_cookie_invalid";

		// Token: 0x040007CE RID: 1998
		internal const string net_cookie_size = "net_cookie_size";

		// Token: 0x040007CF RID: 1999
		internal const string net_cookie_parse_header = "net_cookie_parse_header";

		// Token: 0x040007D0 RID: 2000
		internal const string net_cookie_attribute = "net_cookie_attribute";

		// Token: 0x040007D1 RID: 2001
		internal const string net_cookie_format = "net_cookie_format";

		// Token: 0x040007D2 RID: 2002
		internal const string net_cookie_exists = "net_cookie_exists";

		// Token: 0x040007D3 RID: 2003
		internal const string net_cookie_capacity_range = "net_cookie_capacity_range";

		// Token: 0x040007D4 RID: 2004
		internal const string net_set_token = "net_set_token";

		// Token: 0x040007D5 RID: 2005
		internal const string net_revert_token = "net_revert_token";

		// Token: 0x040007D6 RID: 2006
		internal const string net_ssl_io_async_context = "net_ssl_io_async_context";

		// Token: 0x040007D7 RID: 2007
		internal const string net_ssl_io_encrypt = "net_ssl_io_encrypt";

		// Token: 0x040007D8 RID: 2008
		internal const string net_ssl_io_decrypt = "net_ssl_io_decrypt";

		// Token: 0x040007D9 RID: 2009
		internal const string net_ssl_io_context_expired = "net_ssl_io_context_expired";

		// Token: 0x040007DA RID: 2010
		internal const string net_ssl_io_handshake_start = "net_ssl_io_handshake_start";

		// Token: 0x040007DB RID: 2011
		internal const string net_ssl_io_handshake = "net_ssl_io_handshake";

		// Token: 0x040007DC RID: 2012
		internal const string net_ssl_io_frame = "net_ssl_io_frame";

		// Token: 0x040007DD RID: 2013
		internal const string net_ssl_io_corrupted = "net_ssl_io_corrupted";

		// Token: 0x040007DE RID: 2014
		internal const string net_ssl_io_cert_validation = "net_ssl_io_cert_validation";

		// Token: 0x040007DF RID: 2015
		internal const string net_ssl_io_invalid_end_call = "net_ssl_io_invalid_end_call";

		// Token: 0x040007E0 RID: 2016
		internal const string net_ssl_io_invalid_begin_call = "net_ssl_io_invalid_begin_call";

		// Token: 0x040007E1 RID: 2017
		internal const string net_ssl_io_no_server_cert = "net_ssl_io_no_server_cert";

		// Token: 0x040007E2 RID: 2018
		internal const string net_auth_bad_client_creds = "net_auth_bad_client_creds";

		// Token: 0x040007E3 RID: 2019
		internal const string net_auth_bad_client_creds_or_target_mismatch = "net_auth_bad_client_creds_or_target_mismatch";

		// Token: 0x040007E4 RID: 2020
		internal const string net_auth_context_expectation = "net_auth_context_expectation";

		// Token: 0x040007E5 RID: 2021
		internal const string net_auth_context_expectation_remote = "net_auth_context_expectation_remote";

		// Token: 0x040007E6 RID: 2022
		internal const string net_auth_supported_impl_levels = "net_auth_supported_impl_levels";

		// Token: 0x040007E7 RID: 2023
		internal const string net_auth_no_anonymous_support = "net_auth_no_anonymous_support";

		// Token: 0x040007E8 RID: 2024
		internal const string net_auth_reauth = "net_auth_reauth";

		// Token: 0x040007E9 RID: 2025
		internal const string net_auth_noauth = "net_auth_noauth";

		// Token: 0x040007EA RID: 2026
		internal const string net_auth_client_server = "net_auth_client_server";

		// Token: 0x040007EB RID: 2027
		internal const string net_auth_noencryption = "net_auth_noencryption";

		// Token: 0x040007EC RID: 2028
		internal const string net_auth_SSPI = "net_auth_SSPI";

		// Token: 0x040007ED RID: 2029
		internal const string net_auth_failure = "net_auth_failure";

		// Token: 0x040007EE RID: 2030
		internal const string net_auth_eof = "net_auth_eof";

		// Token: 0x040007EF RID: 2031
		internal const string net_auth_alert = "net_auth_alert";

		// Token: 0x040007F0 RID: 2032
		internal const string net_auth_ignored_reauth = "net_auth_ignored_reauth";

		// Token: 0x040007F1 RID: 2033
		internal const string net_auth_empty_read = "net_auth_empty_read";

		// Token: 0x040007F2 RID: 2034
		internal const string net_auth_message_not_encrypted = "net_auth_message_not_encrypted";

		// Token: 0x040007F3 RID: 2035
		internal const string net_auth_must_specify_extended_protection_scheme = "net_auth_must_specify_extended_protection_scheme";

		// Token: 0x040007F4 RID: 2036
		internal const string net_frame_size = "net_frame_size";

		// Token: 0x040007F5 RID: 2037
		internal const string net_frame_read_io = "net_frame_read_io";

		// Token: 0x040007F6 RID: 2038
		internal const string net_frame_read_size = "net_frame_read_size";

		// Token: 0x040007F7 RID: 2039
		internal const string net_frame_max_size = "net_frame_max_size";

		// Token: 0x040007F8 RID: 2040
		internal const string net_jscript_load = "net_jscript_load";

		// Token: 0x040007F9 RID: 2041
		internal const string net_proxy_not_gmt = "net_proxy_not_gmt";

		// Token: 0x040007FA RID: 2042
		internal const string net_proxy_invalid_dayofweek = "net_proxy_invalid_dayofweek";

		// Token: 0x040007FB RID: 2043
		internal const string net_proxy_invalid_url_format = "net_proxy_invalid_url_format";

		// Token: 0x040007FC RID: 2044
		internal const string net_param_not_string = "net_param_not_string";

		// Token: 0x040007FD RID: 2045
		internal const string net_value_cannot_be_negative = "net_value_cannot_be_negative";

		// Token: 0x040007FE RID: 2046
		internal const string net_invalid_offset = "net_invalid_offset";

		// Token: 0x040007FF RID: 2047
		internal const string net_offset_plus_count = "net_offset_plus_count";

		// Token: 0x04000800 RID: 2048
		internal const string net_cannot_be_false = "net_cannot_be_false";

		// Token: 0x04000801 RID: 2049
		internal const string net_invalid_enum = "net_invalid_enum";

		// Token: 0x04000802 RID: 2050
		internal const string net_listener_already = "net_listener_already";

		// Token: 0x04000803 RID: 2051
		internal const string net_cache_shadowstream_not_writable = "net_cache_shadowstream_not_writable";

		// Token: 0x04000804 RID: 2052
		internal const string net_cache_validator_fail = "net_cache_validator_fail";

		// Token: 0x04000805 RID: 2053
		internal const string net_cache_access_denied = "net_cache_access_denied";

		// Token: 0x04000806 RID: 2054
		internal const string net_cache_validator_result = "net_cache_validator_result";

		// Token: 0x04000807 RID: 2055
		internal const string net_cache_retrieve_failure = "net_cache_retrieve_failure";

		// Token: 0x04000808 RID: 2056
		internal const string net_cache_not_supported_body = "net_cache_not_supported_body";

		// Token: 0x04000809 RID: 2057
		internal const string net_cache_not_supported_command = "net_cache_not_supported_command";

		// Token: 0x0400080A RID: 2058
		internal const string net_cache_not_accept_response = "net_cache_not_accept_response";

		// Token: 0x0400080B RID: 2059
		internal const string net_cache_method_failed = "net_cache_method_failed";

		// Token: 0x0400080C RID: 2060
		internal const string net_cache_key_failed = "net_cache_key_failed";

		// Token: 0x0400080D RID: 2061
		internal const string net_cache_no_stream = "net_cache_no_stream";

		// Token: 0x0400080E RID: 2062
		internal const string net_cache_unsupported_partial_stream = "net_cache_unsupported_partial_stream";

		// Token: 0x0400080F RID: 2063
		internal const string net_cache_not_configured = "net_cache_not_configured";

		// Token: 0x04000810 RID: 2064
		internal const string net_cache_non_seekable_stream_not_supported = "net_cache_non_seekable_stream_not_supported";

		// Token: 0x04000811 RID: 2065
		internal const string net_invalid_cast = "net_invalid_cast";

		// Token: 0x04000812 RID: 2066
		internal const string net_collection_readonly = "net_collection_readonly";

		// Token: 0x04000813 RID: 2067
		internal const string net_not_ipermission = "net_not_ipermission";

		// Token: 0x04000814 RID: 2068
		internal const string net_no_classname = "net_no_classname";

		// Token: 0x04000815 RID: 2069
		internal const string net_no_typename = "net_no_typename";

		// Token: 0x04000816 RID: 2070
		internal const string net_array_too_small = "net_array_too_small";

		// Token: 0x04000817 RID: 2071
		internal const string net_servicePointAddressNotSupportedInHostMode = "net_servicePointAddressNotSupportedInHostMode";

		// Token: 0x04000818 RID: 2072
		internal const string net_Websockets_AlreadyOneOutstandingOperation = "net_Websockets_AlreadyOneOutstandingOperation";

		// Token: 0x04000819 RID: 2073
		internal const string net_Websockets_WebSocketBaseFaulted = "net_Websockets_WebSocketBaseFaulted";

		// Token: 0x0400081A RID: 2074
		internal const string net_WebSockets_NativeSendResponseHeaders = "net_WebSockets_NativeSendResponseHeaders";

		// Token: 0x0400081B RID: 2075
		internal const string net_WebSockets_Generic = "net_WebSockets_Generic";

		// Token: 0x0400081C RID: 2076
		internal const string net_WebSockets_NotAWebSocket_Generic = "net_WebSockets_NotAWebSocket_Generic";

		// Token: 0x0400081D RID: 2077
		internal const string net_WebSockets_UnsupportedWebSocketVersion_Generic = "net_WebSockets_UnsupportedWebSocketVersion_Generic";

		// Token: 0x0400081E RID: 2078
		internal const string net_WebSockets_HeaderError_Generic = "net_WebSockets_HeaderError_Generic";

		// Token: 0x0400081F RID: 2079
		internal const string net_WebSockets_UnsupportedProtocol_Generic = "net_WebSockets_UnsupportedProtocol_Generic";

		// Token: 0x04000820 RID: 2080
		internal const string net_WebSockets_UnsupportedPlatform = "net_WebSockets_UnsupportedPlatform";

		// Token: 0x04000821 RID: 2081
		internal const string net_WebSockets_AcceptNotAWebSocket = "net_WebSockets_AcceptNotAWebSocket";

		// Token: 0x04000822 RID: 2082
		internal const string net_WebSockets_AcceptUnsupportedWebSocketVersion = "net_WebSockets_AcceptUnsupportedWebSocketVersion";

		// Token: 0x04000823 RID: 2083
		internal const string net_WebSockets_AcceptHeaderNotFound = "net_WebSockets_AcceptHeaderNotFound";

		// Token: 0x04000824 RID: 2084
		internal const string net_WebSockets_AcceptUnsupportedProtocol = "net_WebSockets_AcceptUnsupportedProtocol";

		// Token: 0x04000825 RID: 2085
		internal const string net_WebSockets_ClientAcceptingNoProtocols = "net_WebSockets_ClientAcceptingNoProtocols";

		// Token: 0x04000826 RID: 2086
		internal const string net_WebSockets_ClientSecWebSocketProtocolsBlank = "net_WebSockets_ClientSecWebSocketProtocolsBlank";

		// Token: 0x04000827 RID: 2087
		internal const string net_WebSockets_ArgumentOutOfRange_TooSmall = "net_WebSockets_ArgumentOutOfRange_TooSmall";

		// Token: 0x04000828 RID: 2088
		internal const string net_WebSockets_ArgumentOutOfRange_InternalBuffer = "net_WebSockets_ArgumentOutOfRange_InternalBuffer";

		// Token: 0x04000829 RID: 2089
		internal const string net_WebSockets_ArgumentOutOfRange_TooBig = "net_WebSockets_ArgumentOutOfRange_TooBig";

		// Token: 0x0400082A RID: 2090
		internal const string net_WebSockets_InvalidState_Generic = "net_WebSockets_InvalidState_Generic";

		// Token: 0x0400082B RID: 2091
		internal const string net_WebSockets_InvalidState_ClosedOrAborted = "net_WebSockets_InvalidState_ClosedOrAborted";

		// Token: 0x0400082C RID: 2092
		internal const string net_WebSockets_InvalidState = "net_WebSockets_InvalidState";

		// Token: 0x0400082D RID: 2093
		internal const string net_WebSockets_ReceiveAsyncDisallowedAfterCloseAsync = "net_WebSockets_ReceiveAsyncDisallowedAfterCloseAsync";

		// Token: 0x0400082E RID: 2094
		internal const string net_WebSockets_InvalidMessageType = "net_WebSockets_InvalidMessageType";

		// Token: 0x0400082F RID: 2095
		internal const string net_WebSockets_InvalidBufferType = "net_WebSockets_InvalidBufferType";

		// Token: 0x04000830 RID: 2096
		internal const string net_WebSockets_InvalidMessageType_Generic = "net_WebSockets_InvalidMessageType_Generic";

		// Token: 0x04000831 RID: 2097
		internal const string net_WebSockets_Argument_InvalidMessageType = "net_WebSockets_Argument_InvalidMessageType";

		// Token: 0x04000832 RID: 2098
		internal const string net_WebSockets_ConnectionClosedPrematurely_Generic = "net_WebSockets_ConnectionClosedPrematurely_Generic";

		// Token: 0x04000833 RID: 2099
		internal const string net_WebSockets_InvalidCharInProtocolString = "net_WebSockets_InvalidCharInProtocolString";

		// Token: 0x04000834 RID: 2100
		internal const string net_WebSockets_InvalidEmptySubProtocol = "net_WebSockets_InvalidEmptySubProtocol";

		// Token: 0x04000835 RID: 2101
		internal const string net_WebSockets_ReasonNotNull = "net_WebSockets_ReasonNotNull";

		// Token: 0x04000836 RID: 2102
		internal const string net_WebSockets_InvalidCloseStatusCode = "net_WebSockets_InvalidCloseStatusCode";

		// Token: 0x04000837 RID: 2103
		internal const string net_WebSockets_InvalidCloseStatusDescription = "net_WebSockets_InvalidCloseStatusDescription";

		// Token: 0x04000838 RID: 2104
		internal const string net_WebSockets_Scheme = "net_WebSockets_Scheme";

		// Token: 0x04000839 RID: 2105
		internal const string net_WebSockets_AlreadyStarted = "net_WebSockets_AlreadyStarted";

		// Token: 0x0400083A RID: 2106
		internal const string net_WebSockets_Connect101Expected = "net_WebSockets_Connect101Expected";

		// Token: 0x0400083B RID: 2107
		internal const string net_WebSockets_InvalidResponseHeader = "net_WebSockets_InvalidResponseHeader";

		// Token: 0x0400083C RID: 2108
		internal const string net_WebSockets_NotConnected = "net_WebSockets_NotConnected";

		// Token: 0x0400083D RID: 2109
		internal const string net_WebSockets_InvalidRegistration = "net_WebSockets_InvalidRegistration";

		// Token: 0x0400083E RID: 2110
		internal const string net_WebSockets_NoDuplicateProtocol = "net_WebSockets_NoDuplicateProtocol";

		// Token: 0x0400083F RID: 2111
		internal const string net_log_exception = "net_log_exception";

		// Token: 0x04000840 RID: 2112
		internal const string net_log_listener_delegate_exception = "net_log_listener_delegate_exception";

		// Token: 0x04000841 RID: 2113
		internal const string net_log_listener_unsupported_authentication_scheme = "net_log_listener_unsupported_authentication_scheme";

		// Token: 0x04000842 RID: 2114
		internal const string net_log_listener_unmatched_authentication_scheme = "net_log_listener_unmatched_authentication_scheme";

		// Token: 0x04000843 RID: 2115
		internal const string net_log_listener_create_valid_identity_failed = "net_log_listener_create_valid_identity_failed";

		// Token: 0x04000844 RID: 2116
		internal const string net_log_listener_httpsys_registry_null = "net_log_listener_httpsys_registry_null";

		// Token: 0x04000845 RID: 2117
		internal const string net_log_listener_httpsys_registry_error = "net_log_listener_httpsys_registry_error";

		// Token: 0x04000846 RID: 2118
		internal const string net_log_listener_cant_convert_raw_path = "net_log_listener_cant_convert_raw_path";

		// Token: 0x04000847 RID: 2119
		internal const string net_log_listener_cant_convert_percent_value = "net_log_listener_cant_convert_percent_value";

		// Token: 0x04000848 RID: 2120
		internal const string net_log_listener_cant_convert_bytes = "net_log_listener_cant_convert_bytes";

		// Token: 0x04000849 RID: 2121
		internal const string net_log_listener_cant_convert_to_utf8 = "net_log_listener_cant_convert_to_utf8";

		// Token: 0x0400084A RID: 2122
		internal const string net_log_listener_cant_create_uri = "net_log_listener_cant_create_uri";

		// Token: 0x0400084B RID: 2123
		internal const string net_log_listener_no_cbt_disabled = "net_log_listener_no_cbt_disabled";

		// Token: 0x0400084C RID: 2124
		internal const string net_log_listener_no_cbt_http = "net_log_listener_no_cbt_http";

		// Token: 0x0400084D RID: 2125
		internal const string net_log_listener_no_cbt_platform = "net_log_listener_no_cbt_platform";

		// Token: 0x0400084E RID: 2126
		internal const string net_log_listener_no_cbt_trustedproxy = "net_log_listener_no_cbt_trustedproxy";

		// Token: 0x0400084F RID: 2127
		internal const string net_log_listener_cbt = "net_log_listener_cbt";

		// Token: 0x04000850 RID: 2128
		internal const string net_log_listener_no_spn_kerberos = "net_log_listener_no_spn_kerberos";

		// Token: 0x04000851 RID: 2129
		internal const string net_log_listener_no_spn_disabled = "net_log_listener_no_spn_disabled";

		// Token: 0x04000852 RID: 2130
		internal const string net_log_listener_no_spn_cbt = "net_log_listener_no_spn_cbt";

		// Token: 0x04000853 RID: 2131
		internal const string net_log_listener_no_spn_platform = "net_log_listener_no_spn_platform";

		// Token: 0x04000854 RID: 2132
		internal const string net_log_listener_no_spn_whensupported = "net_log_listener_no_spn_whensupported";

		// Token: 0x04000855 RID: 2133
		internal const string net_log_listener_no_spn_loopback = "net_log_listener_no_spn_loopback";

		// Token: 0x04000856 RID: 2134
		internal const string net_log_listener_spn = "net_log_listener_spn";

		// Token: 0x04000857 RID: 2135
		internal const string net_log_listener_spn_passed = "net_log_listener_spn_passed";

		// Token: 0x04000858 RID: 2136
		internal const string net_log_listener_spn_failed = "net_log_listener_spn_failed";

		// Token: 0x04000859 RID: 2137
		internal const string net_log_listener_spn_failed_always = "net_log_listener_spn_failed_always";

		// Token: 0x0400085A RID: 2138
		internal const string net_log_listener_spn_failed_empty = "net_log_listener_spn_failed_empty";

		// Token: 0x0400085B RID: 2139
		internal const string net_log_listener_spn_failed_dump = "net_log_listener_spn_failed_dump";

		// Token: 0x0400085C RID: 2140
		internal const string net_log_listener_spn_add = "net_log_listener_spn_add";

		// Token: 0x0400085D RID: 2141
		internal const string net_log_listener_spn_not_add = "net_log_listener_spn_not_add";

		// Token: 0x0400085E RID: 2142
		internal const string net_log_listener_spn_remove = "net_log_listener_spn_remove";

		// Token: 0x0400085F RID: 2143
		internal const string net_log_listener_spn_not_remove = "net_log_listener_spn_not_remove";

		// Token: 0x04000860 RID: 2144
		internal const string net_log_sspi_enumerating_security_packages = "net_log_sspi_enumerating_security_packages";

		// Token: 0x04000861 RID: 2145
		internal const string net_log_sspi_security_package_not_found = "net_log_sspi_security_package_not_found";

		// Token: 0x04000862 RID: 2146
		internal const string net_log_sspi_security_context_input_buffer = "net_log_sspi_security_context_input_buffer";

		// Token: 0x04000863 RID: 2147
		internal const string net_log_sspi_security_context_input_buffers = "net_log_sspi_security_context_input_buffers";

		// Token: 0x04000864 RID: 2148
		internal const string net_log_sspi_selected_cipher_suite = "net_log_sspi_selected_cipher_suite";

		// Token: 0x04000865 RID: 2149
		internal const string net_log_remote_certificate = "net_log_remote_certificate";

		// Token: 0x04000866 RID: 2150
		internal const string net_log_locating_private_key_for_certificate = "net_log_locating_private_key_for_certificate";

		// Token: 0x04000867 RID: 2151
		internal const string net_log_cert_is_of_type_2 = "net_log_cert_is_of_type_2";

		// Token: 0x04000868 RID: 2152
		internal const string net_log_found_cert_in_store = "net_log_found_cert_in_store";

		// Token: 0x04000869 RID: 2153
		internal const string net_log_did_not_find_cert_in_store = "net_log_did_not_find_cert_in_store";

		// Token: 0x0400086A RID: 2154
		internal const string net_log_open_store_failed = "net_log_open_store_failed";

		// Token: 0x0400086B RID: 2155
		internal const string net_log_got_certificate_from_delegate = "net_log_got_certificate_from_delegate";

		// Token: 0x0400086C RID: 2156
		internal const string net_log_no_delegate_and_have_no_client_cert = "net_log_no_delegate_and_have_no_client_cert";

		// Token: 0x0400086D RID: 2157
		internal const string net_log_no_delegate_but_have_client_cert = "net_log_no_delegate_but_have_client_cert";

		// Token: 0x0400086E RID: 2158
		internal const string net_log_attempting_restart_using_cert = "net_log_attempting_restart_using_cert";

		// Token: 0x0400086F RID: 2159
		internal const string net_log_no_issuers_try_all_certs = "net_log_no_issuers_try_all_certs";

		// Token: 0x04000870 RID: 2160
		internal const string net_log_server_issuers_look_for_matching_certs = "net_log_server_issuers_look_for_matching_certs";

		// Token: 0x04000871 RID: 2161
		internal const string net_log_selected_cert = "net_log_selected_cert";

		// Token: 0x04000872 RID: 2162
		internal const string net_log_n_certs_after_filtering = "net_log_n_certs_after_filtering";

		// Token: 0x04000873 RID: 2163
		internal const string net_log_finding_matching_certs = "net_log_finding_matching_certs";

		// Token: 0x04000874 RID: 2164
		internal const string net_log_using_cached_credential = "net_log_using_cached_credential";

		// Token: 0x04000875 RID: 2165
		internal const string net_log_remote_cert_user_declared_valid = "net_log_remote_cert_user_declared_valid";

		// Token: 0x04000876 RID: 2166
		internal const string net_log_remote_cert_user_declared_invalid = "net_log_remote_cert_user_declared_invalid";

		// Token: 0x04000877 RID: 2167
		internal const string net_log_remote_cert_has_no_errors = "net_log_remote_cert_has_no_errors";

		// Token: 0x04000878 RID: 2168
		internal const string net_log_remote_cert_has_errors = "net_log_remote_cert_has_errors";

		// Token: 0x04000879 RID: 2169
		internal const string net_log_remote_cert_not_available = "net_log_remote_cert_not_available";

		// Token: 0x0400087A RID: 2170
		internal const string net_log_remote_cert_name_mismatch = "net_log_remote_cert_name_mismatch";

		// Token: 0x0400087B RID: 2171
		internal const string net_log_proxy_autodetect_script_location_parse_error = "net_log_proxy_autodetect_script_location_parse_error";

		// Token: 0x0400087C RID: 2172
		internal const string net_log_proxy_autodetect_failed = "net_log_proxy_autodetect_failed";

		// Token: 0x0400087D RID: 2173
		internal const string net_log_proxy_script_execution_error = "net_log_proxy_script_execution_error";

		// Token: 0x0400087E RID: 2174
		internal const string net_log_proxy_script_download_compile_error = "net_log_proxy_script_download_compile_error";

		// Token: 0x0400087F RID: 2175
		internal const string net_log_proxy_system_setting_update = "net_log_proxy_system_setting_update";

		// Token: 0x04000880 RID: 2176
		internal const string net_log_proxy_update_due_to_ip_config_change = "net_log_proxy_update_due_to_ip_config_change";

		// Token: 0x04000881 RID: 2177
		internal const string net_log_proxy_called_with_null_parameter = "net_log_proxy_called_with_null_parameter";

		// Token: 0x04000882 RID: 2178
		internal const string net_log_proxy_called_with_invalid_parameter = "net_log_proxy_called_with_invalid_parameter";

		// Token: 0x04000883 RID: 2179
		internal const string net_log_proxy_ras_supported = "net_log_proxy_ras_supported";

		// Token: 0x04000884 RID: 2180
		internal const string net_log_proxy_ras_notsupported_exception = "net_log_proxy_ras_notsupported_exception";

		// Token: 0x04000885 RID: 2181
		internal const string net_log_proxy_winhttp_cant_open_session = "net_log_proxy_winhttp_cant_open_session";

		// Token: 0x04000886 RID: 2182
		internal const string net_log_proxy_winhttp_getproxy_failed = "net_log_proxy_winhttp_getproxy_failed";

		// Token: 0x04000887 RID: 2183
		internal const string net_log_proxy_winhttp_timeout_error = "net_log_proxy_winhttp_timeout_error";

		// Token: 0x04000888 RID: 2184
		internal const string net_log_cache_validation_failed_resubmit = "net_log_cache_validation_failed_resubmit";

		// Token: 0x04000889 RID: 2185
		internal const string net_log_cache_refused_server_response = "net_log_cache_refused_server_response";

		// Token: 0x0400088A RID: 2186
		internal const string net_log_cache_ftp_proxy_doesnt_support_partial = "net_log_cache_ftp_proxy_doesnt_support_partial";

		// Token: 0x0400088B RID: 2187
		internal const string net_log_cache_ftp_method = "net_log_cache_ftp_method";

		// Token: 0x0400088C RID: 2188
		internal const string net_log_cache_ftp_supports_bin_only = "net_log_cache_ftp_supports_bin_only";

		// Token: 0x0400088D RID: 2189
		internal const string net_log_cache_replacing_entry_with_HTTP_200 = "net_log_cache_replacing_entry_with_HTTP_200";

		// Token: 0x0400088E RID: 2190
		internal const string net_log_cache_now_time = "net_log_cache_now_time";

		// Token: 0x0400088F RID: 2191
		internal const string net_log_cache_max_age_absolute = "net_log_cache_max_age_absolute";

		// Token: 0x04000890 RID: 2192
		internal const string net_log_cache_age1 = "net_log_cache_age1";

		// Token: 0x04000891 RID: 2193
		internal const string net_log_cache_age1_date_header = "net_log_cache_age1_date_header";

		// Token: 0x04000892 RID: 2194
		internal const string net_log_cache_age1_last_synchronized = "net_log_cache_age1_last_synchronized";

		// Token: 0x04000893 RID: 2195
		internal const string net_log_cache_age1_last_synchronized_age_header = "net_log_cache_age1_last_synchronized_age_header";

		// Token: 0x04000894 RID: 2196
		internal const string net_log_cache_age2 = "net_log_cache_age2";

		// Token: 0x04000895 RID: 2197
		internal const string net_log_cache_max_age_cache_s_max_age = "net_log_cache_max_age_cache_s_max_age";

		// Token: 0x04000896 RID: 2198
		internal const string net_log_cache_max_age_expires_date = "net_log_cache_max_age_expires_date";

		// Token: 0x04000897 RID: 2199
		internal const string net_log_cache_max_age_cache_max_age = "net_log_cache_max_age_cache_max_age";

		// Token: 0x04000898 RID: 2200
		internal const string net_log_cache_no_max_age_use_10_percent = "net_log_cache_no_max_age_use_10_percent";

		// Token: 0x04000899 RID: 2201
		internal const string net_log_cache_no_max_age_use_default = "net_log_cache_no_max_age_use_default";

		// Token: 0x0400089A RID: 2202
		internal const string net_log_cache_validator_invalid_for_policy = "net_log_cache_validator_invalid_for_policy";

		// Token: 0x0400089B RID: 2203
		internal const string net_log_cache_response_last_modified = "net_log_cache_response_last_modified";

		// Token: 0x0400089C RID: 2204
		internal const string net_log_cache_cache_last_modified = "net_log_cache_cache_last_modified";

		// Token: 0x0400089D RID: 2205
		internal const string net_log_cache_partial_and_non_zero_content_offset = "net_log_cache_partial_and_non_zero_content_offset";

		// Token: 0x0400089E RID: 2206
		internal const string net_log_cache_response_valid_based_on_policy = "net_log_cache_response_valid_based_on_policy";

		// Token: 0x0400089F RID: 2207
		internal const string net_log_cache_null_response_failure = "net_log_cache_null_response_failure";

		// Token: 0x040008A0 RID: 2208
		internal const string net_log_cache_ftp_response_status = "net_log_cache_ftp_response_status";

		// Token: 0x040008A1 RID: 2209
		internal const string net_log_cache_resp_valid_based_on_retry = "net_log_cache_resp_valid_based_on_retry";

		// Token: 0x040008A2 RID: 2210
		internal const string net_log_cache_no_update_based_on_method = "net_log_cache_no_update_based_on_method";

		// Token: 0x040008A3 RID: 2211
		internal const string net_log_cache_removed_existing_invalid_entry = "net_log_cache_removed_existing_invalid_entry";

		// Token: 0x040008A4 RID: 2212
		internal const string net_log_cache_not_updated_based_on_policy = "net_log_cache_not_updated_based_on_policy";

		// Token: 0x040008A5 RID: 2213
		internal const string net_log_cache_not_updated_because_no_response = "net_log_cache_not_updated_because_no_response";

		// Token: 0x040008A6 RID: 2214
		internal const string net_log_cache_removed_existing_based_on_method = "net_log_cache_removed_existing_based_on_method";

		// Token: 0x040008A7 RID: 2215
		internal const string net_log_cache_existing_not_removed_because_unexpected_response_status = "net_log_cache_existing_not_removed_because_unexpected_response_status";

		// Token: 0x040008A8 RID: 2216
		internal const string net_log_cache_removed_existing_based_on_policy = "net_log_cache_removed_existing_based_on_policy";

		// Token: 0x040008A9 RID: 2217
		internal const string net_log_cache_not_updated_based_on_ftp_response_status = "net_log_cache_not_updated_based_on_ftp_response_status";

		// Token: 0x040008AA RID: 2218
		internal const string net_log_cache_update_not_supported_for_ftp_restart = "net_log_cache_update_not_supported_for_ftp_restart";

		// Token: 0x040008AB RID: 2219
		internal const string net_log_cache_removed_entry_because_ftp_restart_response_changed = "net_log_cache_removed_entry_because_ftp_restart_response_changed";

		// Token: 0x040008AC RID: 2220
		internal const string net_log_cache_last_synchronized = "net_log_cache_last_synchronized";

		// Token: 0x040008AD RID: 2221
		internal const string net_log_cache_suppress_update_because_synched_last_minute = "net_log_cache_suppress_update_because_synched_last_minute";

		// Token: 0x040008AE RID: 2222
		internal const string net_log_cache_updating_last_synchronized = "net_log_cache_updating_last_synchronized";

		// Token: 0x040008AF RID: 2223
		internal const string net_log_cache_cannot_remove = "net_log_cache_cannot_remove";

		// Token: 0x040008B0 RID: 2224
		internal const string net_log_cache_key_status = "net_log_cache_key_status";

		// Token: 0x040008B1 RID: 2225
		internal const string net_log_cache_key_remove_failed_status = "net_log_cache_key_remove_failed_status";

		// Token: 0x040008B2 RID: 2226
		internal const string net_log_cache_usecount_file = "net_log_cache_usecount_file";

		// Token: 0x040008B3 RID: 2227
		internal const string net_log_cache_stream = "net_log_cache_stream";

		// Token: 0x040008B4 RID: 2228
		internal const string net_log_cache_filename = "net_log_cache_filename";

		// Token: 0x040008B5 RID: 2229
		internal const string net_log_cache_lookup_failed = "net_log_cache_lookup_failed";

		// Token: 0x040008B6 RID: 2230
		internal const string net_log_cache_exception = "net_log_cache_exception";

		// Token: 0x040008B7 RID: 2231
		internal const string net_log_cache_expected_length = "net_log_cache_expected_length";

		// Token: 0x040008B8 RID: 2232
		internal const string net_log_cache_last_modified = "net_log_cache_last_modified";

		// Token: 0x040008B9 RID: 2233
		internal const string net_log_cache_expires = "net_log_cache_expires";

		// Token: 0x040008BA RID: 2234
		internal const string net_log_cache_max_stale = "net_log_cache_max_stale";

		// Token: 0x040008BB RID: 2235
		internal const string net_log_cache_dumping_metadata = "net_log_cache_dumping_metadata";

		// Token: 0x040008BC RID: 2236
		internal const string net_log_cache_create_failed = "net_log_cache_create_failed";

		// Token: 0x040008BD RID: 2237
		internal const string net_log_cache_set_expires = "net_log_cache_set_expires";

		// Token: 0x040008BE RID: 2238
		internal const string net_log_cache_set_last_modified = "net_log_cache_set_last_modified";

		// Token: 0x040008BF RID: 2239
		internal const string net_log_cache_set_last_synchronized = "net_log_cache_set_last_synchronized";

		// Token: 0x040008C0 RID: 2240
		internal const string net_log_cache_enable_max_stale = "net_log_cache_enable_max_stale";

		// Token: 0x040008C1 RID: 2241
		internal const string net_log_cache_disable_max_stale = "net_log_cache_disable_max_stale";

		// Token: 0x040008C2 RID: 2242
		internal const string net_log_cache_set_new_metadata = "net_log_cache_set_new_metadata";

		// Token: 0x040008C3 RID: 2243
		internal const string net_log_cache_dumping = "net_log_cache_dumping";

		// Token: 0x040008C4 RID: 2244
		internal const string net_log_cache_key = "net_log_cache_key";

		// Token: 0x040008C5 RID: 2245
		internal const string net_log_cache_no_commit = "net_log_cache_no_commit";

		// Token: 0x040008C6 RID: 2246
		internal const string net_log_cache_error_deleting_filename = "net_log_cache_error_deleting_filename";

		// Token: 0x040008C7 RID: 2247
		internal const string net_log_cache_update_failed = "net_log_cache_update_failed";

		// Token: 0x040008C8 RID: 2248
		internal const string net_log_cache_delete_failed = "net_log_cache_delete_failed";

		// Token: 0x040008C9 RID: 2249
		internal const string net_log_cache_commit_failed = "net_log_cache_commit_failed";

		// Token: 0x040008CA RID: 2250
		internal const string net_log_cache_committed_as_partial = "net_log_cache_committed_as_partial";

		// Token: 0x040008CB RID: 2251
		internal const string net_log_cache_max_stale_and_update_status = "net_log_cache_max_stale_and_update_status";

		// Token: 0x040008CC RID: 2252
		internal const string net_log_cache_failing_request_with_exception = "net_log_cache_failing_request_with_exception";

		// Token: 0x040008CD RID: 2253
		internal const string net_log_cache_request_method = "net_log_cache_request_method";

		// Token: 0x040008CE RID: 2254
		internal const string net_log_cache_http_status_parse_failure = "net_log_cache_http_status_parse_failure";

		// Token: 0x040008CF RID: 2255
		internal const string net_log_cache_http_status_line = "net_log_cache_http_status_line";

		// Token: 0x040008D0 RID: 2256
		internal const string net_log_cache_cache_control = "net_log_cache_cache_control";

		// Token: 0x040008D1 RID: 2257
		internal const string net_log_cache_invalid_http_version = "net_log_cache_invalid_http_version";

		// Token: 0x040008D2 RID: 2258
		internal const string net_log_cache_no_http_response_header = "net_log_cache_no_http_response_header";

		// Token: 0x040008D3 RID: 2259
		internal const string net_log_cache_http_header_parse_error = "net_log_cache_http_header_parse_error";

		// Token: 0x040008D4 RID: 2260
		internal const string net_log_cache_metadata_name_value_parse_error = "net_log_cache_metadata_name_value_parse_error";

		// Token: 0x040008D5 RID: 2261
		internal const string net_log_cache_content_range_error = "net_log_cache_content_range_error";

		// Token: 0x040008D6 RID: 2262
		internal const string net_log_cache_cache_control_error = "net_log_cache_cache_control_error";

		// Token: 0x040008D7 RID: 2263
		internal const string net_log_cache_unexpected_status = "net_log_cache_unexpected_status";

		// Token: 0x040008D8 RID: 2264
		internal const string net_log_cache_object_and_exception = "net_log_cache_object_and_exception";

		// Token: 0x040008D9 RID: 2265
		internal const string net_log_cache_revalidation_not_needed = "net_log_cache_revalidation_not_needed";

		// Token: 0x040008DA RID: 2266
		internal const string net_log_cache_not_updated_based_on_cache_protocol_status = "net_log_cache_not_updated_based_on_cache_protocol_status";

		// Token: 0x040008DB RID: 2267
		internal const string net_log_cache_closing_cache_stream = "net_log_cache_closing_cache_stream";

		// Token: 0x040008DC RID: 2268
		internal const string net_log_cache_exception_ignored = "net_log_cache_exception_ignored";

		// Token: 0x040008DD RID: 2269
		internal const string net_log_cache_no_cache_entry = "net_log_cache_no_cache_entry";

		// Token: 0x040008DE RID: 2270
		internal const string net_log_cache_null_cached_stream = "net_log_cache_null_cached_stream";

		// Token: 0x040008DF RID: 2271
		internal const string net_log_cache_requested_combined_but_null_cached_stream = "net_log_cache_requested_combined_but_null_cached_stream";

		// Token: 0x040008E0 RID: 2272
		internal const string net_log_cache_returned_range_cache = "net_log_cache_returned_range_cache";

		// Token: 0x040008E1 RID: 2273
		internal const string net_log_cache_entry_not_found_freshness_undefined = "net_log_cache_entry_not_found_freshness_undefined";

		// Token: 0x040008E2 RID: 2274
		internal const string net_log_cache_dumping_cache_context = "net_log_cache_dumping_cache_context";

		// Token: 0x040008E3 RID: 2275
		internal const string net_log_cache_result = "net_log_cache_result";

		// Token: 0x040008E4 RID: 2276
		internal const string net_log_cache_uri_with_query_has_no_expiration = "net_log_cache_uri_with_query_has_no_expiration";

		// Token: 0x040008E5 RID: 2277
		internal const string net_log_cache_uri_with_query_and_cached_resp_from_http_10 = "net_log_cache_uri_with_query_and_cached_resp_from_http_10";

		// Token: 0x040008E6 RID: 2278
		internal const string net_log_cache_valid_as_fresh_or_because_policy = "net_log_cache_valid_as_fresh_or_because_policy";

		// Token: 0x040008E7 RID: 2279
		internal const string net_log_cache_accept_based_on_retry_count = "net_log_cache_accept_based_on_retry_count";

		// Token: 0x040008E8 RID: 2280
		internal const string net_log_cache_date_header_older_than_cache_entry = "net_log_cache_date_header_older_than_cache_entry";

		// Token: 0x040008E9 RID: 2281
		internal const string net_log_cache_server_didnt_satisfy_range = "net_log_cache_server_didnt_satisfy_range";

		// Token: 0x040008EA RID: 2282
		internal const string net_log_cache_304_received_on_unconditional_request = "net_log_cache_304_received_on_unconditional_request";

		// Token: 0x040008EB RID: 2283
		internal const string net_log_cache_304_received_on_unconditional_request_expected_200_206 = "net_log_cache_304_received_on_unconditional_request_expected_200_206";

		// Token: 0x040008EC RID: 2284
		internal const string net_log_cache_last_modified_header_older_than_cache_entry = "net_log_cache_last_modified_header_older_than_cache_entry";

		// Token: 0x040008ED RID: 2285
		internal const string net_log_cache_freshness_outside_policy_limits = "net_log_cache_freshness_outside_policy_limits";

		// Token: 0x040008EE RID: 2286
		internal const string net_log_cache_need_to_remove_invalid_cache_entry_304 = "net_log_cache_need_to_remove_invalid_cache_entry_304";

		// Token: 0x040008EF RID: 2287
		internal const string net_log_cache_resp_status = "net_log_cache_resp_status";

		// Token: 0x040008F0 RID: 2288
		internal const string net_log_cache_resp_304_or_request_head = "net_log_cache_resp_304_or_request_head";

		// Token: 0x040008F1 RID: 2289
		internal const string net_log_cache_dont_update_cached_headers = "net_log_cache_dont_update_cached_headers";

		// Token: 0x040008F2 RID: 2290
		internal const string net_log_cache_update_cached_headers = "net_log_cache_update_cached_headers";

		// Token: 0x040008F3 RID: 2291
		internal const string net_log_cache_partial_resp_not_combined_with_existing_entry = "net_log_cache_partial_resp_not_combined_with_existing_entry";

		// Token: 0x040008F4 RID: 2292
		internal const string net_log_cache_request_contains_conditional_header = "net_log_cache_request_contains_conditional_header";

		// Token: 0x040008F5 RID: 2293
		internal const string net_log_cache_not_a_get_head_post = "net_log_cache_not_a_get_head_post";

		// Token: 0x040008F6 RID: 2294
		internal const string net_log_cache_cannot_update_cache_if_304 = "net_log_cache_cannot_update_cache_if_304";

		// Token: 0x040008F7 RID: 2295
		internal const string net_log_cache_cannot_update_cache_with_head_resp = "net_log_cache_cannot_update_cache_with_head_resp";

		// Token: 0x040008F8 RID: 2296
		internal const string net_log_cache_http_resp_is_null = "net_log_cache_http_resp_is_null";

		// Token: 0x040008F9 RID: 2297
		internal const string net_log_cache_resp_cache_control_is_no_store = "net_log_cache_resp_cache_control_is_no_store";

		// Token: 0x040008FA RID: 2298
		internal const string net_log_cache_resp_cache_control_is_public = "net_log_cache_resp_cache_control_is_public";

		// Token: 0x040008FB RID: 2299
		internal const string net_log_cache_resp_cache_control_is_private = "net_log_cache_resp_cache_control_is_private";

		// Token: 0x040008FC RID: 2300
		internal const string net_log_cache_resp_cache_control_is_private_plus_headers = "net_log_cache_resp_cache_control_is_private_plus_headers";

		// Token: 0x040008FD RID: 2301
		internal const string net_log_cache_resp_older_than_cache = "net_log_cache_resp_older_than_cache";

		// Token: 0x040008FE RID: 2302
		internal const string net_log_cache_revalidation_required = "net_log_cache_revalidation_required";

		// Token: 0x040008FF RID: 2303
		internal const string net_log_cache_needs_revalidation = "net_log_cache_needs_revalidation";

		// Token: 0x04000900 RID: 2304
		internal const string net_log_cache_resp_allows_caching = "net_log_cache_resp_allows_caching";

		// Token: 0x04000901 RID: 2305
		internal const string net_log_cache_auth_header_and_no_s_max_age = "net_log_cache_auth_header_and_no_s_max_age";

		// Token: 0x04000902 RID: 2306
		internal const string net_log_cache_post_resp_without_cache_control_or_expires = "net_log_cache_post_resp_without_cache_control_or_expires";

		// Token: 0x04000903 RID: 2307
		internal const string net_log_cache_valid_based_on_status_code = "net_log_cache_valid_based_on_status_code";

		// Token: 0x04000904 RID: 2308
		internal const string net_log_cache_resp_no_cache_control = "net_log_cache_resp_no_cache_control";

		// Token: 0x04000905 RID: 2309
		internal const string net_log_cache_age = "net_log_cache_age";

		// Token: 0x04000906 RID: 2310
		internal const string net_log_cache_policy_min_fresh = "net_log_cache_policy_min_fresh";

		// Token: 0x04000907 RID: 2311
		internal const string net_log_cache_policy_max_age = "net_log_cache_policy_max_age";

		// Token: 0x04000908 RID: 2312
		internal const string net_log_cache_policy_cache_sync_date = "net_log_cache_policy_cache_sync_date";

		// Token: 0x04000909 RID: 2313
		internal const string net_log_cache_policy_max_stale = "net_log_cache_policy_max_stale";

		// Token: 0x0400090A RID: 2314
		internal const string net_log_cache_control_no_cache = "net_log_cache_control_no_cache";

		// Token: 0x0400090B RID: 2315
		internal const string net_log_cache_control_no_cache_removing_some_headers = "net_log_cache_control_no_cache_removing_some_headers";

		// Token: 0x0400090C RID: 2316
		internal const string net_log_cache_control_must_revalidate = "net_log_cache_control_must_revalidate";

		// Token: 0x0400090D RID: 2317
		internal const string net_log_cache_cached_auth_header = "net_log_cache_cached_auth_header";

		// Token: 0x0400090E RID: 2318
		internal const string net_log_cache_cached_auth_header_no_control_directive = "net_log_cache_cached_auth_header_no_control_directive";

		// Token: 0x0400090F RID: 2319
		internal const string net_log_cache_after_validation = "net_log_cache_after_validation";

		// Token: 0x04000910 RID: 2320
		internal const string net_log_cache_resp_status_304 = "net_log_cache_resp_status_304";

		// Token: 0x04000911 RID: 2321
		internal const string net_log_cache_head_resp_has_different_content_length = "net_log_cache_head_resp_has_different_content_length";

		// Token: 0x04000912 RID: 2322
		internal const string net_log_cache_head_resp_has_different_content_md5 = "net_log_cache_head_resp_has_different_content_md5";

		// Token: 0x04000913 RID: 2323
		internal const string net_log_cache_head_resp_has_different_etag = "net_log_cache_head_resp_has_different_etag";

		// Token: 0x04000914 RID: 2324
		internal const string net_log_cache_304_head_resp_has_different_last_modified = "net_log_cache_304_head_resp_has_different_last_modified";

		// Token: 0x04000915 RID: 2325
		internal const string net_log_cache_existing_entry_has_to_be_discarded = "net_log_cache_existing_entry_has_to_be_discarded";

		// Token: 0x04000916 RID: 2326
		internal const string net_log_cache_existing_entry_should_be_discarded = "net_log_cache_existing_entry_should_be_discarded";

		// Token: 0x04000917 RID: 2327
		internal const string net_log_cache_206_resp_non_matching_entry = "net_log_cache_206_resp_non_matching_entry";

		// Token: 0x04000918 RID: 2328
		internal const string net_log_cache_206_resp_starting_position_not_adjusted = "net_log_cache_206_resp_starting_position_not_adjusted";

		// Token: 0x04000919 RID: 2329
		internal const string net_log_cache_combined_resp_requested = "net_log_cache_combined_resp_requested";

		// Token: 0x0400091A RID: 2330
		internal const string net_log_cache_updating_headers_on_304 = "net_log_cache_updating_headers_on_304";

		// Token: 0x0400091B RID: 2331
		internal const string net_log_cache_suppressing_headers_update_on_304 = "net_log_cache_suppressing_headers_update_on_304";

		// Token: 0x0400091C RID: 2332
		internal const string net_log_cache_status_code_not_304_206 = "net_log_cache_status_code_not_304_206";

		// Token: 0x0400091D RID: 2333
		internal const string net_log_cache_sxx_resp_cache_only = "net_log_cache_sxx_resp_cache_only";

		// Token: 0x0400091E RID: 2334
		internal const string net_log_cache_sxx_resp_can_be_replaced = "net_log_cache_sxx_resp_can_be_replaced";

		// Token: 0x0400091F RID: 2335
		internal const string net_log_cache_vary_header_empty = "net_log_cache_vary_header_empty";

		// Token: 0x04000920 RID: 2336
		internal const string net_log_cache_vary_header_contains_asterisks = "net_log_cache_vary_header_contains_asterisks";

		// Token: 0x04000921 RID: 2337
		internal const string net_log_cache_no_headers_in_metadata = "net_log_cache_no_headers_in_metadata";

		// Token: 0x04000922 RID: 2338
		internal const string net_log_cache_vary_header_mismatched_count = "net_log_cache_vary_header_mismatched_count";

		// Token: 0x04000923 RID: 2339
		internal const string net_log_cache_vary_header_mismatched_field = "net_log_cache_vary_header_mismatched_field";

		// Token: 0x04000924 RID: 2340
		internal const string net_log_cache_vary_header_match = "net_log_cache_vary_header_match";

		// Token: 0x04000925 RID: 2341
		internal const string net_log_cache_range = "net_log_cache_range";

		// Token: 0x04000926 RID: 2342
		internal const string net_log_cache_range_invalid_format = "net_log_cache_range_invalid_format";

		// Token: 0x04000927 RID: 2343
		internal const string net_log_cache_range_not_in_cache = "net_log_cache_range_not_in_cache";

		// Token: 0x04000928 RID: 2344
		internal const string net_log_cache_range_in_cache = "net_log_cache_range_in_cache";

		// Token: 0x04000929 RID: 2345
		internal const string net_log_cache_partial_resp = "net_log_cache_partial_resp";

		// Token: 0x0400092A RID: 2346
		internal const string net_log_cache_range_request_range = "net_log_cache_range_request_range";

		// Token: 0x0400092B RID: 2347
		internal const string net_log_cache_could_be_partial = "net_log_cache_could_be_partial";

		// Token: 0x0400092C RID: 2348
		internal const string net_log_cache_condition_if_none_match = "net_log_cache_condition_if_none_match";

		// Token: 0x0400092D RID: 2349
		internal const string net_log_cache_condition_if_modified_since = "net_log_cache_condition_if_modified_since";

		// Token: 0x0400092E RID: 2350
		internal const string net_log_cache_cannot_construct_conditional_request = "net_log_cache_cannot_construct_conditional_request";

		// Token: 0x0400092F RID: 2351
		internal const string net_log_cache_cannot_construct_conditional_range_request = "net_log_cache_cannot_construct_conditional_range_request";

		// Token: 0x04000930 RID: 2352
		internal const string net_log_cache_entry_size_too_big = "net_log_cache_entry_size_too_big";

		// Token: 0x04000931 RID: 2353
		internal const string net_log_cache_condition_if_range = "net_log_cache_condition_if_range";

		// Token: 0x04000932 RID: 2354
		internal const string net_log_cache_conditional_range_not_implemented_on_http_10 = "net_log_cache_conditional_range_not_implemented_on_http_10";

		// Token: 0x04000933 RID: 2355
		internal const string net_log_cache_saving_request_headers = "net_log_cache_saving_request_headers";

		// Token: 0x04000934 RID: 2356
		internal const string net_log_cache_only_byte_range_implemented = "net_log_cache_only_byte_range_implemented";

		// Token: 0x04000935 RID: 2357
		internal const string net_log_cache_multiple_complex_range_not_implemented = "net_log_cache_multiple_complex_range_not_implemented";

		// Token: 0x04000936 RID: 2358
		internal const string net_log_digest_hash_algorithm_not_supported = "net_log_digest_hash_algorithm_not_supported";

		// Token: 0x04000937 RID: 2359
		internal const string net_log_digest_qop_not_supported = "net_log_digest_qop_not_supported";

		// Token: 0x04000938 RID: 2360
		internal const string net_log_digest_requires_nonce = "net_log_digest_requires_nonce";

		// Token: 0x04000939 RID: 2361
		internal const string net_log_auth_invalid_challenge = "net_log_auth_invalid_challenge";

		// Token: 0x0400093A RID: 2362
		internal const string net_log_unknown = "net_log_unknown";

		// Token: 0x0400093B RID: 2363
		internal const string net_log_operation_returned_something = "net_log_operation_returned_something";

		// Token: 0x0400093C RID: 2364
		internal const string net_log_operation_failed_with_error = "net_log_operation_failed_with_error";

		// Token: 0x0400093D RID: 2365
		internal const string net_log_buffered_n_bytes = "net_log_buffered_n_bytes";

		// Token: 0x0400093E RID: 2366
		internal const string net_log_method_equal = "net_log_method_equal";

		// Token: 0x0400093F RID: 2367
		internal const string net_log_releasing_connection = "net_log_releasing_connection";

		// Token: 0x04000940 RID: 2368
		internal const string net_log_unexpected_exception = "net_log_unexpected_exception";

		// Token: 0x04000941 RID: 2369
		internal const string net_log_server_response_error_code = "net_log_server_response_error_code";

		// Token: 0x04000942 RID: 2370
		internal const string net_log_resubmitting_request = "net_log_resubmitting_request";

		// Token: 0x04000943 RID: 2371
		internal const string net_log_retrieving_localhost_exception = "net_log_retrieving_localhost_exception";

		// Token: 0x04000944 RID: 2372
		internal const string net_log_resolved_servicepoint_may_not_be_remote_server = "net_log_resolved_servicepoint_may_not_be_remote_server";

		// Token: 0x04000945 RID: 2373
		internal const string net_log_closed_idle = "net_log_closed_idle";

		// Token: 0x04000946 RID: 2374
		internal const string net_log_received_status_line = "net_log_received_status_line";

		// Token: 0x04000947 RID: 2375
		internal const string net_log_sending_headers = "net_log_sending_headers";

		// Token: 0x04000948 RID: 2376
		internal const string net_log_received_headers = "net_log_received_headers";

		// Token: 0x04000949 RID: 2377
		internal const string net_log_shell_expression_pattern_format_warning = "net_log_shell_expression_pattern_format_warning";

		// Token: 0x0400094A RID: 2378
		internal const string net_log_exception_in_callback = "net_log_exception_in_callback";

		// Token: 0x0400094B RID: 2379
		internal const string net_log_sending_command = "net_log_sending_command";

		// Token: 0x0400094C RID: 2380
		internal const string net_log_received_response = "net_log_received_response";

		// Token: 0x0400094D RID: 2381
		internal const string net_log_socket_connected = "net_log_socket_connected";

		// Token: 0x0400094E RID: 2382
		internal const string net_log_socket_accepted = "net_log_socket_accepted";

		// Token: 0x0400094F RID: 2383
		internal const string net_log_socket_not_logged_file = "net_log_socket_not_logged_file";

		// Token: 0x04000950 RID: 2384
		internal const string net_log_socket_connect_dnsendpoint = "net_log_socket_connect_dnsendpoint";

		// Token: 0x04000951 RID: 2385
		internal const string net_log_set_socketoption_reuseport = "net_log_set_socketoption_reuseport";

		// Token: 0x04000952 RID: 2386
		internal const string net_log_set_socketoption_reuseport_not_supported = "net_log_set_socketoption_reuseport_not_supported";

		// Token: 0x04000953 RID: 2387
		internal const string net_log_set_socketoption_reuseport_default_on = "net_log_set_socketoption_reuseport_default_on";

		// Token: 0x04000954 RID: 2388
		internal const string MailAddressInvalidFormat = "MailAddressInvalidFormat";

		// Token: 0x04000955 RID: 2389
		internal const string MailSubjectInvalidFormat = "MailSubjectInvalidFormat";

		// Token: 0x04000956 RID: 2390
		internal const string MailBase64InvalidCharacter = "MailBase64InvalidCharacter";

		// Token: 0x04000957 RID: 2391
		internal const string MailCollectionIsReadOnly = "MailCollectionIsReadOnly";

		// Token: 0x04000958 RID: 2392
		internal const string MailDateInvalidFormat = "MailDateInvalidFormat";

		// Token: 0x04000959 RID: 2393
		internal const string MailHeaderFieldAlreadyExists = "MailHeaderFieldAlreadyExists";

		// Token: 0x0400095A RID: 2394
		internal const string MailHeaderFieldInvalidCharacter = "MailHeaderFieldInvalidCharacter";

		// Token: 0x0400095B RID: 2395
		internal const string MailHeaderFieldMalformedHeader = "MailHeaderFieldMalformedHeader";

		// Token: 0x0400095C RID: 2396
		internal const string MailHeaderFieldMismatchedName = "MailHeaderFieldMismatchedName";

		// Token: 0x0400095D RID: 2397
		internal const string MailHeaderIndexOutOfBounds = "MailHeaderIndexOutOfBounds";

		// Token: 0x0400095E RID: 2398
		internal const string MailHeaderItemAccessorOnlySingleton = "MailHeaderItemAccessorOnlySingleton";

		// Token: 0x0400095F RID: 2399
		internal const string MailHeaderListHasChanged = "MailHeaderListHasChanged";

		// Token: 0x04000960 RID: 2400
		internal const string MailHeaderResetCalledBeforeEOF = "MailHeaderResetCalledBeforeEOF";

		// Token: 0x04000961 RID: 2401
		internal const string MailHeaderTargetArrayTooSmall = "MailHeaderTargetArrayTooSmall";

		// Token: 0x04000962 RID: 2402
		internal const string MailHeaderInvalidCID = "MailHeaderInvalidCID";

		// Token: 0x04000963 RID: 2403
		internal const string MailHostNotFound = "MailHostNotFound";

		// Token: 0x04000964 RID: 2404
		internal const string MailReaderGetContentStreamAlreadyCalled = "MailReaderGetContentStreamAlreadyCalled";

		// Token: 0x04000965 RID: 2405
		internal const string MailReaderTruncated = "MailReaderTruncated";

		// Token: 0x04000966 RID: 2406
		internal const string MailWriterIsInContent = "MailWriterIsInContent";

		// Token: 0x04000967 RID: 2407
		internal const string MailServerDoesNotSupportStartTls = "MailServerDoesNotSupportStartTls";

		// Token: 0x04000968 RID: 2408
		internal const string MailServerResponse = "MailServerResponse";

		// Token: 0x04000969 RID: 2409
		internal const string SSPIAuthenticationOrSPNNull = "SSPIAuthenticationOrSPNNull";

		// Token: 0x0400096A RID: 2410
		internal const string SSPIPInvokeError = "SSPIPInvokeError";

		// Token: 0x0400096B RID: 2411
		internal const string SSPIInvalidHandleType = "SSPIInvalidHandleType";

		// Token: 0x0400096C RID: 2412
		internal const string SmtpAlreadyConnected = "SmtpAlreadyConnected";

		// Token: 0x0400096D RID: 2413
		internal const string SmtpAuthenticationFailed = "SmtpAuthenticationFailed";

		// Token: 0x0400096E RID: 2414
		internal const string SmtpAuthenticationFailedNoCreds = "SmtpAuthenticationFailedNoCreds";

		// Token: 0x0400096F RID: 2415
		internal const string SmtpDataStreamOpen = "SmtpDataStreamOpen";

		// Token: 0x04000970 RID: 2416
		internal const string SmtpDefaultMimePreamble = "SmtpDefaultMimePreamble";

		// Token: 0x04000971 RID: 2417
		internal const string SmtpDefaultSubject = "SmtpDefaultSubject";

		// Token: 0x04000972 RID: 2418
		internal const string SmtpInvalidResponse = "SmtpInvalidResponse";

		// Token: 0x04000973 RID: 2419
		internal const string SmtpNotConnected = "SmtpNotConnected";

		// Token: 0x04000974 RID: 2420
		internal const string SmtpSystemStatus = "SmtpSystemStatus";

		// Token: 0x04000975 RID: 2421
		internal const string SmtpHelpMessage = "SmtpHelpMessage";

		// Token: 0x04000976 RID: 2422
		internal const string SmtpServiceReady = "SmtpServiceReady";

		// Token: 0x04000977 RID: 2423
		internal const string SmtpServiceClosingTransmissionChannel = "SmtpServiceClosingTransmissionChannel";

		// Token: 0x04000978 RID: 2424
		internal const string SmtpOK = "SmtpOK";

		// Token: 0x04000979 RID: 2425
		internal const string SmtpUserNotLocalWillForward = "SmtpUserNotLocalWillForward";

		// Token: 0x0400097A RID: 2426
		internal const string SmtpStartMailInput = "SmtpStartMailInput";

		// Token: 0x0400097B RID: 2427
		internal const string SmtpServiceNotAvailable = "SmtpServiceNotAvailable";

		// Token: 0x0400097C RID: 2428
		internal const string SmtpMailboxBusy = "SmtpMailboxBusy";

		// Token: 0x0400097D RID: 2429
		internal const string SmtpLocalErrorInProcessing = "SmtpLocalErrorInProcessing";

		// Token: 0x0400097E RID: 2430
		internal const string SmtpInsufficientStorage = "SmtpInsufficientStorage";

		// Token: 0x0400097F RID: 2431
		internal const string SmtpPermissionDenied = "SmtpPermissionDenied";

		// Token: 0x04000980 RID: 2432
		internal const string SmtpCommandUnrecognized = "SmtpCommandUnrecognized";

		// Token: 0x04000981 RID: 2433
		internal const string SmtpSyntaxError = "SmtpSyntaxError";

		// Token: 0x04000982 RID: 2434
		internal const string SmtpCommandNotImplemented = "SmtpCommandNotImplemented";

		// Token: 0x04000983 RID: 2435
		internal const string SmtpBadCommandSequence = "SmtpBadCommandSequence";

		// Token: 0x04000984 RID: 2436
		internal const string SmtpCommandParameterNotImplemented = "SmtpCommandParameterNotImplemented";

		// Token: 0x04000985 RID: 2437
		internal const string SmtpMailboxUnavailable = "SmtpMailboxUnavailable";

		// Token: 0x04000986 RID: 2438
		internal const string SmtpUserNotLocalTryAlternatePath = "SmtpUserNotLocalTryAlternatePath";

		// Token: 0x04000987 RID: 2439
		internal const string SmtpExceededStorageAllocation = "SmtpExceededStorageAllocation";

		// Token: 0x04000988 RID: 2440
		internal const string SmtpMailboxNameNotAllowed = "SmtpMailboxNameNotAllowed";

		// Token: 0x04000989 RID: 2441
		internal const string SmtpTransactionFailed = "SmtpTransactionFailed";

		// Token: 0x0400098A RID: 2442
		internal const string SmtpSendMailFailure = "SmtpSendMailFailure";

		// Token: 0x0400098B RID: 2443
		internal const string SmtpRecipientFailed = "SmtpRecipientFailed";

		// Token: 0x0400098C RID: 2444
		internal const string SmtpRecipientRequired = "SmtpRecipientRequired";

		// Token: 0x0400098D RID: 2445
		internal const string SmtpFromRequired = "SmtpFromRequired";

		// Token: 0x0400098E RID: 2446
		internal const string SmtpAllRecipientsFailed = "SmtpAllRecipientsFailed";

		// Token: 0x0400098F RID: 2447
		internal const string SmtpClientNotPermitted = "SmtpClientNotPermitted";

		// Token: 0x04000990 RID: 2448
		internal const string SmtpMustIssueStartTlsFirst = "SmtpMustIssueStartTlsFirst";

		// Token: 0x04000991 RID: 2449
		internal const string SmtpNeedAbsolutePickupDirectory = "SmtpNeedAbsolutePickupDirectory";

		// Token: 0x04000992 RID: 2450
		internal const string SmtpGetIisPickupDirectoryFailed = "SmtpGetIisPickupDirectoryFailed";

		// Token: 0x04000993 RID: 2451
		internal const string SmtpPickupDirectoryDoesnotSupportSsl = "SmtpPickupDirectoryDoesnotSupportSsl";

		// Token: 0x04000994 RID: 2452
		internal const string SmtpOperationInProgress = "SmtpOperationInProgress";

		// Token: 0x04000995 RID: 2453
		internal const string SmtpAuthResponseInvalid = "SmtpAuthResponseInvalid";

		// Token: 0x04000996 RID: 2454
		internal const string SmtpEhloResponseInvalid = "SmtpEhloResponseInvalid";

		// Token: 0x04000997 RID: 2455
		internal const string SmtpNonAsciiUserNotSupported = "SmtpNonAsciiUserNotSupported";

		// Token: 0x04000998 RID: 2456
		internal const string SmtpInvalidHostName = "SmtpInvalidHostName";

		// Token: 0x04000999 RID: 2457
		internal const string MimeTransferEncodingNotSupported = "MimeTransferEncodingNotSupported";

		// Token: 0x0400099A RID: 2458
		internal const string SeekNotSupported = "SeekNotSupported";

		// Token: 0x0400099B RID: 2459
		internal const string WriteNotSupported = "WriteNotSupported";

		// Token: 0x0400099C RID: 2460
		internal const string InvalidHexDigit = "InvalidHexDigit";

		// Token: 0x0400099D RID: 2461
		internal const string InvalidSSPIContext = "InvalidSSPIContext";

		// Token: 0x0400099E RID: 2462
		internal const string InvalidSSPIContextKey = "InvalidSSPIContextKey";

		// Token: 0x0400099F RID: 2463
		internal const string InvalidSSPINegotiationElement = "InvalidSSPINegotiationElement";

		// Token: 0x040009A0 RID: 2464
		internal const string InvalidHeaderName = "InvalidHeaderName";

		// Token: 0x040009A1 RID: 2465
		internal const string InvalidHeaderValue = "InvalidHeaderValue";

		// Token: 0x040009A2 RID: 2466
		internal const string CannotGetEffectiveTimeOfSSPIContext = "CannotGetEffectiveTimeOfSSPIContext";

		// Token: 0x040009A3 RID: 2467
		internal const string CannotGetExpiryTimeOfSSPIContext = "CannotGetExpiryTimeOfSSPIContext";

		// Token: 0x040009A4 RID: 2468
		internal const string ReadNotSupported = "ReadNotSupported";

		// Token: 0x040009A5 RID: 2469
		internal const string InvalidAsyncResult = "InvalidAsyncResult";

		// Token: 0x040009A6 RID: 2470
		internal const string UnspecifiedHost = "UnspecifiedHost";

		// Token: 0x040009A7 RID: 2471
		internal const string InvalidPort = "InvalidPort";

		// Token: 0x040009A8 RID: 2472
		internal const string SmtpInvalidOperationDuringSend = "SmtpInvalidOperationDuringSend";

		// Token: 0x040009A9 RID: 2473
		internal const string MimePartCantResetStream = "MimePartCantResetStream";

		// Token: 0x040009AA RID: 2474
		internal const string MediaTypeInvalid = "MediaTypeInvalid";

		// Token: 0x040009AB RID: 2475
		internal const string ContentTypeInvalid = "ContentTypeInvalid";

		// Token: 0x040009AC RID: 2476
		internal const string ContentDispositionInvalid = "ContentDispositionInvalid";

		// Token: 0x040009AD RID: 2477
		internal const string AttributeNotSupported = "AttributeNotSupported";

		// Token: 0x040009AE RID: 2478
		internal const string Cannot_remove_with_null = "Cannot_remove_with_null";

		// Token: 0x040009AF RID: 2479
		internal const string Config_base_elements_only = "Config_base_elements_only";

		// Token: 0x040009B0 RID: 2480
		internal const string Config_base_no_child_nodes = "Config_base_no_child_nodes";

		// Token: 0x040009B1 RID: 2481
		internal const string Config_base_required_attribute_empty = "Config_base_required_attribute_empty";

		// Token: 0x040009B2 RID: 2482
		internal const string Config_base_required_attribute_missing = "Config_base_required_attribute_missing";

		// Token: 0x040009B3 RID: 2483
		internal const string Config_base_time_overflow = "Config_base_time_overflow";

		// Token: 0x040009B4 RID: 2484
		internal const string Config_base_type_must_be_configurationvalidation = "Config_base_type_must_be_configurationvalidation";

		// Token: 0x040009B5 RID: 2485
		internal const string Config_base_type_must_be_typeconverter = "Config_base_type_must_be_typeconverter";

		// Token: 0x040009B6 RID: 2486
		internal const string Config_base_unknown_format = "Config_base_unknown_format";

		// Token: 0x040009B7 RID: 2487
		internal const string Config_base_unrecognized_attribute = "Config_base_unrecognized_attribute";

		// Token: 0x040009B8 RID: 2488
		internal const string Config_base_unrecognized_element = "Config_base_unrecognized_element";

		// Token: 0x040009B9 RID: 2489
		internal const string Config_invalid_boolean_attribute = "Config_invalid_boolean_attribute";

		// Token: 0x040009BA RID: 2490
		internal const string Config_invalid_integer_attribute = "Config_invalid_integer_attribute";

		// Token: 0x040009BB RID: 2491
		internal const string Config_invalid_positive_integer_attribute = "Config_invalid_positive_integer_attribute";

		// Token: 0x040009BC RID: 2492
		internal const string Config_invalid_type_attribute = "Config_invalid_type_attribute";

		// Token: 0x040009BD RID: 2493
		internal const string Config_missing_required_attribute = "Config_missing_required_attribute";

		// Token: 0x040009BE RID: 2494
		internal const string Config_name_value_file_section_file_invalid_root = "Config_name_value_file_section_file_invalid_root";

		// Token: 0x040009BF RID: 2495
		internal const string Config_provider_must_implement_type = "Config_provider_must_implement_type";

		// Token: 0x040009C0 RID: 2496
		internal const string Config_provider_name_null_or_empty = "Config_provider_name_null_or_empty";

		// Token: 0x040009C1 RID: 2497
		internal const string Config_provider_not_found = "Config_provider_not_found";

		// Token: 0x040009C2 RID: 2498
		internal const string Config_property_name_cannot_be_empty = "Config_property_name_cannot_be_empty";

		// Token: 0x040009C3 RID: 2499
		internal const string Config_section_cannot_clear_locked_section = "Config_section_cannot_clear_locked_section";

		// Token: 0x040009C4 RID: 2500
		internal const string Config_section_record_not_found = "Config_section_record_not_found";

		// Token: 0x040009C5 RID: 2501
		internal const string Config_source_cannot_contain_file = "Config_source_cannot_contain_file";

		// Token: 0x040009C6 RID: 2502
		internal const string Config_system_already_set = "Config_system_already_set";

		// Token: 0x040009C7 RID: 2503
		internal const string Config_unable_to_read_security_policy = "Config_unable_to_read_security_policy";

		// Token: 0x040009C8 RID: 2504
		internal const string Config_write_xml_returned_null = "Config_write_xml_returned_null";

		// Token: 0x040009C9 RID: 2505
		internal const string Cannot_clear_sections_within_group = "Cannot_clear_sections_within_group";

		// Token: 0x040009CA RID: 2506
		internal const string Cannot_exit_up_top_directory = "Cannot_exit_up_top_directory";

		// Token: 0x040009CB RID: 2507
		internal const string Could_not_create_listener = "Could_not_create_listener";

		// Token: 0x040009CC RID: 2508
		internal const string TL_InitializeData_NotSpecified = "TL_InitializeData_NotSpecified";

		// Token: 0x040009CD RID: 2509
		internal const string Could_not_create_type_instance = "Could_not_create_type_instance";

		// Token: 0x040009CE RID: 2510
		internal const string Could_not_find_type = "Could_not_find_type";

		// Token: 0x040009CF RID: 2511
		internal const string Could_not_get_constructor = "Could_not_get_constructor";

		// Token: 0x040009D0 RID: 2512
		internal const string EmptyTypeName_NotAllowed = "EmptyTypeName_NotAllowed";

		// Token: 0x040009D1 RID: 2513
		internal const string Incorrect_base_type = "Incorrect_base_type";

		// Token: 0x040009D2 RID: 2514
		internal const string Only_specify_one = "Only_specify_one";

		// Token: 0x040009D3 RID: 2515
		internal const string Provider_Already_Initialized = "Provider_Already_Initialized";

		// Token: 0x040009D4 RID: 2516
		internal const string Reference_listener_cant_have_properties = "Reference_listener_cant_have_properties";

		// Token: 0x040009D5 RID: 2517
		internal const string Reference_to_nonexistent_listener = "Reference_to_nonexistent_listener";

		// Token: 0x040009D6 RID: 2518
		internal const string SettingsPropertyNotFound = "SettingsPropertyNotFound";

		// Token: 0x040009D7 RID: 2519
		internal const string SettingsPropertyReadOnly = "SettingsPropertyReadOnly";

		// Token: 0x040009D8 RID: 2520
		internal const string SettingsPropertyWrongType = "SettingsPropertyWrongType";

		// Token: 0x040009D9 RID: 2521
		internal const string Type_isnt_tracelistener = "Type_isnt_tracelistener";

		// Token: 0x040009DA RID: 2522
		internal const string Unable_to_convert_type_from_string = "Unable_to_convert_type_from_string";

		// Token: 0x040009DB RID: 2523
		internal const string Unable_to_convert_type_to_string = "Unable_to_convert_type_to_string";

		// Token: 0x040009DC RID: 2524
		internal const string Value_must_be_numeric = "Value_must_be_numeric";

		// Token: 0x040009DD RID: 2525
		internal const string Could_not_create_from_default_value = "Could_not_create_from_default_value";

		// Token: 0x040009DE RID: 2526
		internal const string Could_not_create_from_default_value_2 = "Could_not_create_from_default_value_2";

		// Token: 0x040009DF RID: 2527
		internal const string InvalidDirName = "InvalidDirName";

		// Token: 0x040009E0 RID: 2528
		internal const string FSW_IOError = "FSW_IOError";

		// Token: 0x040009E1 RID: 2529
		internal const string PatternInvalidChar = "PatternInvalidChar";

		// Token: 0x040009E2 RID: 2530
		internal const string BufferSizeTooLarge = "BufferSizeTooLarge";

		// Token: 0x040009E3 RID: 2531
		internal const string FSW_ChangedFilter = "FSW_ChangedFilter";

		// Token: 0x040009E4 RID: 2532
		internal const string FSW_Enabled = "FSW_Enabled";

		// Token: 0x040009E5 RID: 2533
		internal const string FSW_Filter = "FSW_Filter";

		// Token: 0x040009E6 RID: 2534
		internal const string FSW_IncludeSubdirectories = "FSW_IncludeSubdirectories";

		// Token: 0x040009E7 RID: 2535
		internal const string FSW_Path = "FSW_Path";

		// Token: 0x040009E8 RID: 2536
		internal const string FSW_SynchronizingObject = "FSW_SynchronizingObject";

		// Token: 0x040009E9 RID: 2537
		internal const string FSW_Changed = "FSW_Changed";

		// Token: 0x040009EA RID: 2538
		internal const string FSW_Created = "FSW_Created";

		// Token: 0x040009EB RID: 2539
		internal const string FSW_Deleted = "FSW_Deleted";

		// Token: 0x040009EC RID: 2540
		internal const string FSW_Renamed = "FSW_Renamed";

		// Token: 0x040009ED RID: 2541
		internal const string FSW_BufferOverflow = "FSW_BufferOverflow";

		// Token: 0x040009EE RID: 2542
		internal const string FileSystemWatcherDesc = "FileSystemWatcherDesc";

		// Token: 0x040009EF RID: 2543
		internal const string NotSet = "NotSet";

		// Token: 0x040009F0 RID: 2544
		internal const string TimerAutoReset = "TimerAutoReset";

		// Token: 0x040009F1 RID: 2545
		internal const string TimerEnabled = "TimerEnabled";

		// Token: 0x040009F2 RID: 2546
		internal const string TimerInterval = "TimerInterval";

		// Token: 0x040009F3 RID: 2547
		internal const string TimerIntervalElapsed = "TimerIntervalElapsed";

		// Token: 0x040009F4 RID: 2548
		internal const string TimerSynchronizingObject = "TimerSynchronizingObject";

		// Token: 0x040009F5 RID: 2549
		internal const string MismatchedCounterTypes = "MismatchedCounterTypes";

		// Token: 0x040009F6 RID: 2550
		internal const string NoPropertyForAttribute = "NoPropertyForAttribute";

		// Token: 0x040009F7 RID: 2551
		internal const string InvalidAttributeType = "InvalidAttributeType";

		// Token: 0x040009F8 RID: 2552
		internal const string Generic_ArgCantBeEmptyString = "Generic_ArgCantBeEmptyString";

		// Token: 0x040009F9 RID: 2553
		internal const string BadLogName = "BadLogName";

		// Token: 0x040009FA RID: 2554
		internal const string InvalidProperty = "InvalidProperty";

		// Token: 0x040009FB RID: 2555
		internal const string CantMonitorEventLog = "CantMonitorEventLog";

		// Token: 0x040009FC RID: 2556
		internal const string InitTwice = "InitTwice";

		// Token: 0x040009FD RID: 2557
		internal const string InvalidParameter = "InvalidParameter";

		// Token: 0x040009FE RID: 2558
		internal const string MissingParameter = "MissingParameter";

		// Token: 0x040009FF RID: 2559
		internal const string ParameterTooLong = "ParameterTooLong";

		// Token: 0x04000A00 RID: 2560
		internal const string LocalSourceAlreadyExists = "LocalSourceAlreadyExists";

		// Token: 0x04000A01 RID: 2561
		internal const string SourceAlreadyExists = "SourceAlreadyExists";

		// Token: 0x04000A02 RID: 2562
		internal const string LocalLogAlreadyExistsAsSource = "LocalLogAlreadyExistsAsSource";

		// Token: 0x04000A03 RID: 2563
		internal const string LogAlreadyExistsAsSource = "LogAlreadyExistsAsSource";

		// Token: 0x04000A04 RID: 2564
		internal const string DuplicateLogName = "DuplicateLogName";

		// Token: 0x04000A05 RID: 2565
		internal const string RegKeyMissing = "RegKeyMissing";

		// Token: 0x04000A06 RID: 2566
		internal const string LocalRegKeyMissing = "LocalRegKeyMissing";

		// Token: 0x04000A07 RID: 2567
		internal const string RegKeyMissingShort = "RegKeyMissingShort";

		// Token: 0x04000A08 RID: 2568
		internal const string InvalidParameterFormat = "InvalidParameterFormat";

		// Token: 0x04000A09 RID: 2569
		internal const string NoLogName = "NoLogName";

		// Token: 0x04000A0A RID: 2570
		internal const string RegKeyNoAccess = "RegKeyNoAccess";

		// Token: 0x04000A0B RID: 2571
		internal const string MissingLog = "MissingLog";

		// Token: 0x04000A0C RID: 2572
		internal const string SourceNotRegistered = "SourceNotRegistered";

		// Token: 0x04000A0D RID: 2573
		internal const string LocalSourceNotRegistered = "LocalSourceNotRegistered";

		// Token: 0x04000A0E RID: 2574
		internal const string CantRetrieveEntries = "CantRetrieveEntries";

		// Token: 0x04000A0F RID: 2575
		internal const string IndexOutOfBounds = "IndexOutOfBounds";

		// Token: 0x04000A10 RID: 2576
		internal const string CantReadLogEntryAt = "CantReadLogEntryAt";

		// Token: 0x04000A11 RID: 2577
		internal const string MissingLogProperty = "MissingLogProperty";

		// Token: 0x04000A12 RID: 2578
		internal const string CantOpenLog = "CantOpenLog";

		// Token: 0x04000A13 RID: 2579
		internal const string NeedSourceToOpen = "NeedSourceToOpen";

		// Token: 0x04000A14 RID: 2580
		internal const string NeedSourceToWrite = "NeedSourceToWrite";

		// Token: 0x04000A15 RID: 2581
		internal const string CantOpenLogAccess = "CantOpenLogAccess";

		// Token: 0x04000A16 RID: 2582
		internal const string LogEntryTooLong = "LogEntryTooLong";

		// Token: 0x04000A17 RID: 2583
		internal const string TooManyReplacementStrings = "TooManyReplacementStrings";

		// Token: 0x04000A18 RID: 2584
		internal const string LogSourceMismatch = "LogSourceMismatch";

		// Token: 0x04000A19 RID: 2585
		internal const string NoAccountInfo = "NoAccountInfo";

		// Token: 0x04000A1A RID: 2586
		internal const string NoCurrentEntry = "NoCurrentEntry";

		// Token: 0x04000A1B RID: 2587
		internal const string MessageNotFormatted = "MessageNotFormatted";

		// Token: 0x04000A1C RID: 2588
		internal const string EventID = "EventID";

		// Token: 0x04000A1D RID: 2589
		internal const string LogDoesNotExists = "LogDoesNotExists";

		// Token: 0x04000A1E RID: 2590
		internal const string InvalidCustomerLogName = "InvalidCustomerLogName";

		// Token: 0x04000A1F RID: 2591
		internal const string CannotDeleteEqualSource = "CannotDeleteEqualSource";

		// Token: 0x04000A20 RID: 2592
		internal const string RentionDaysOutOfRange = "RentionDaysOutOfRange";

		// Token: 0x04000A21 RID: 2593
		internal const string MaximumKilobytesOutOfRange = "MaximumKilobytesOutOfRange";

		// Token: 0x04000A22 RID: 2594
		internal const string SomeLogsInaccessible = "SomeLogsInaccessible";

		// Token: 0x04000A23 RID: 2595
		internal const string SomeLogsInaccessibleToCreate = "SomeLogsInaccessibleToCreate";

		// Token: 0x04000A24 RID: 2596
		internal const string BadConfigSwitchValue = "BadConfigSwitchValue";

		// Token: 0x04000A25 RID: 2597
		internal const string ConfigSectionsUnique = "ConfigSectionsUnique";

		// Token: 0x04000A26 RID: 2598
		internal const string ConfigSectionsUniquePerSection = "ConfigSectionsUniquePerSection";

		// Token: 0x04000A27 RID: 2599
		internal const string SourceListenerDoesntExist = "SourceListenerDoesntExist";

		// Token: 0x04000A28 RID: 2600
		internal const string SourceSwitchDoesntExist = "SourceSwitchDoesntExist";

		// Token: 0x04000A29 RID: 2601
		internal const string CategoryHelpCorrupt = "CategoryHelpCorrupt";

		// Token: 0x04000A2A RID: 2602
		internal const string CounterNameCorrupt = "CounterNameCorrupt";

		// Token: 0x04000A2B RID: 2603
		internal const string CounterDataCorrupt = "CounterDataCorrupt";

		// Token: 0x04000A2C RID: 2604
		internal const string ReadOnlyCounter = "ReadOnlyCounter";

		// Token: 0x04000A2D RID: 2605
		internal const string ReadOnlyRemoveInstance = "ReadOnlyRemoveInstance";

		// Token: 0x04000A2E RID: 2606
		internal const string NotCustomCounter = "NotCustomCounter";

		// Token: 0x04000A2F RID: 2607
		internal const string CategoryNameMissing = "CategoryNameMissing";

		// Token: 0x04000A30 RID: 2608
		internal const string CounterNameMissing = "CounterNameMissing";

		// Token: 0x04000A31 RID: 2609
		internal const string InstanceNameProhibited = "InstanceNameProhibited";

		// Token: 0x04000A32 RID: 2610
		internal const string InstanceNameRequired = "InstanceNameRequired";

		// Token: 0x04000A33 RID: 2611
		internal const string MissingInstance = "MissingInstance";

		// Token: 0x04000A34 RID: 2612
		internal const string PerformanceCategoryExists = "PerformanceCategoryExists";

		// Token: 0x04000A35 RID: 2613
		internal const string InvalidCounterName = "InvalidCounterName";

		// Token: 0x04000A36 RID: 2614
		internal const string DuplicateCounterName = "DuplicateCounterName";

		// Token: 0x04000A37 RID: 2615
		internal const string CantChangeCategoryRegistration = "CantChangeCategoryRegistration";

		// Token: 0x04000A38 RID: 2616
		internal const string CantDeleteCategory = "CantDeleteCategory";

		// Token: 0x04000A39 RID: 2617
		internal const string MissingCategory = "MissingCategory";

		// Token: 0x04000A3A RID: 2618
		internal const string MissingCategoryDetail = "MissingCategoryDetail";

		// Token: 0x04000A3B RID: 2619
		internal const string CantReadCategory = "CantReadCategory";

		// Token: 0x04000A3C RID: 2620
		internal const string MissingCounter = "MissingCounter";

		// Token: 0x04000A3D RID: 2621
		internal const string CategoryNameNotSet = "CategoryNameNotSet";

		// Token: 0x04000A3E RID: 2622
		internal const string CounterExists = "CounterExists";

		// Token: 0x04000A3F RID: 2623
		internal const string CantReadCategoryIndex = "CantReadCategoryIndex";

		// Token: 0x04000A40 RID: 2624
		internal const string CantReadCounter = "CantReadCounter";

		// Token: 0x04000A41 RID: 2625
		internal const string CantReadInstance = "CantReadInstance";

		// Token: 0x04000A42 RID: 2626
		internal const string RemoteWriting = "RemoteWriting";

		// Token: 0x04000A43 RID: 2627
		internal const string CounterLayout = "CounterLayout";

		// Token: 0x04000A44 RID: 2628
		internal const string PossibleDeadlock = "PossibleDeadlock";

		// Token: 0x04000A45 RID: 2629
		internal const string SharedMemoryGhosted = "SharedMemoryGhosted";

		// Token: 0x04000A46 RID: 2630
		internal const string HelpNotAvailable = "HelpNotAvailable";

		// Token: 0x04000A47 RID: 2631
		internal const string PerfInvalidHelp = "PerfInvalidHelp";

		// Token: 0x04000A48 RID: 2632
		internal const string PerfInvalidCounterName = "PerfInvalidCounterName";

		// Token: 0x04000A49 RID: 2633
		internal const string PerfInvalidCategoryName = "PerfInvalidCategoryName";

		// Token: 0x04000A4A RID: 2634
		internal const string MustAddCounterCreationData = "MustAddCounterCreationData";

		// Token: 0x04000A4B RID: 2635
		internal const string RemoteCounterAdmin = "RemoteCounterAdmin";

		// Token: 0x04000A4C RID: 2636
		internal const string NoInstanceInformation = "NoInstanceInformation";

		// Token: 0x04000A4D RID: 2637
		internal const string PerfCounterPdhError = "PerfCounterPdhError";

		// Token: 0x04000A4E RID: 2638
		internal const string MultiInstanceOnly = "MultiInstanceOnly";

		// Token: 0x04000A4F RID: 2639
		internal const string SingleInstanceOnly = "SingleInstanceOnly";

		// Token: 0x04000A50 RID: 2640
		internal const string InstanceNameTooLong = "InstanceNameTooLong";

		// Token: 0x04000A51 RID: 2641
		internal const string CategoryNameTooLong = "CategoryNameTooLong";

		// Token: 0x04000A52 RID: 2642
		internal const string InstanceLifetimeProcessonReadOnly = "InstanceLifetimeProcessonReadOnly";

		// Token: 0x04000A53 RID: 2643
		internal const string InstanceLifetimeProcessforSingleInstance = "InstanceLifetimeProcessforSingleInstance";

		// Token: 0x04000A54 RID: 2644
		internal const string InstanceAlreadyExists = "InstanceAlreadyExists";

		// Token: 0x04000A55 RID: 2645
		internal const string CantSetLifetimeAfterInitialized = "CantSetLifetimeAfterInitialized";

		// Token: 0x04000A56 RID: 2646
		internal const string ProcessLifetimeNotValidInGlobal = "ProcessLifetimeNotValidInGlobal";

		// Token: 0x04000A57 RID: 2647
		internal const string CantConvertProcessToGlobal = "CantConvertProcessToGlobal";

		// Token: 0x04000A58 RID: 2648
		internal const string CantConvertGlobalToProcess = "CantConvertGlobalToProcess";

		// Token: 0x04000A59 RID: 2649
		internal const string PCNotSupportedUnderAppContainer = "PCNotSupportedUnderAppContainer";

		// Token: 0x04000A5A RID: 2650
		internal const string PriorityClassNotSupported = "PriorityClassNotSupported";

		// Token: 0x04000A5B RID: 2651
		internal const string WinNTRequired = "WinNTRequired";

		// Token: 0x04000A5C RID: 2652
		internal const string Win2kRequired = "Win2kRequired";

		// Token: 0x04000A5D RID: 2653
		internal const string NoAssociatedProcess = "NoAssociatedProcess";

		// Token: 0x04000A5E RID: 2654
		internal const string ProcessIdRequired = "ProcessIdRequired";

		// Token: 0x04000A5F RID: 2655
		internal const string NotSupportedRemote = "NotSupportedRemote";

		// Token: 0x04000A60 RID: 2656
		internal const string NoProcessInfo = "NoProcessInfo";

		// Token: 0x04000A61 RID: 2657
		internal const string WaitTillExit = "WaitTillExit";

		// Token: 0x04000A62 RID: 2658
		internal const string NoProcessHandle = "NoProcessHandle";

		// Token: 0x04000A63 RID: 2659
		internal const string MissingProccess = "MissingProccess";

		// Token: 0x04000A64 RID: 2660
		internal const string BadMinWorkset = "BadMinWorkset";

		// Token: 0x04000A65 RID: 2661
		internal const string BadMaxWorkset = "BadMaxWorkset";

		// Token: 0x04000A66 RID: 2662
		internal const string WinNTRequiredForRemote = "WinNTRequiredForRemote";

		// Token: 0x04000A67 RID: 2663
		internal const string ProcessHasExited = "ProcessHasExited";

		// Token: 0x04000A68 RID: 2664
		internal const string ProcessHasExitedNoId = "ProcessHasExitedNoId";

		// Token: 0x04000A69 RID: 2665
		internal const string ThreadExited = "ThreadExited";

		// Token: 0x04000A6A RID: 2666
		internal const string Win2000Required = "Win2000Required";

		// Token: 0x04000A6B RID: 2667
		internal const string ProcessNotFound = "ProcessNotFound";

		// Token: 0x04000A6C RID: 2668
		internal const string CantGetProcessId = "CantGetProcessId";

		// Token: 0x04000A6D RID: 2669
		internal const string ProcessDisabled = "ProcessDisabled";

		// Token: 0x04000A6E RID: 2670
		internal const string WaitReasonUnavailable = "WaitReasonUnavailable";

		// Token: 0x04000A6F RID: 2671
		internal const string NotSupportedRemoteThread = "NotSupportedRemoteThread";

		// Token: 0x04000A70 RID: 2672
		internal const string UseShellExecuteRequiresSTA = "UseShellExecuteRequiresSTA";

		// Token: 0x04000A71 RID: 2673
		internal const string CantRedirectStreams = "CantRedirectStreams";

		// Token: 0x04000A72 RID: 2674
		internal const string CantUseEnvVars = "CantUseEnvVars";

		// Token: 0x04000A73 RID: 2675
		internal const string CantStartAsUser = "CantStartAsUser";

		// Token: 0x04000A74 RID: 2676
		internal const string CouldntConnectToRemoteMachine = "CouldntConnectToRemoteMachine";

		// Token: 0x04000A75 RID: 2677
		internal const string CouldntGetProcessInfos = "CouldntGetProcessInfos";

		// Token: 0x04000A76 RID: 2678
		internal const string InputIdleUnkownError = "InputIdleUnkownError";

		// Token: 0x04000A77 RID: 2679
		internal const string FileNameMissing = "FileNameMissing";

		// Token: 0x04000A78 RID: 2680
		internal const string EnvironmentBlock = "EnvironmentBlock";

		// Token: 0x04000A79 RID: 2681
		internal const string EnumProcessModuleFailed = "EnumProcessModuleFailed";

		// Token: 0x04000A7A RID: 2682
		internal const string EnumProcessModuleFailedDueToWow = "EnumProcessModuleFailedDueToWow";

		// Token: 0x04000A7B RID: 2683
		internal const string PendingAsyncOperation = "PendingAsyncOperation";

		// Token: 0x04000A7C RID: 2684
		internal const string NoAsyncOperation = "NoAsyncOperation";

		// Token: 0x04000A7D RID: 2685
		internal const string InvalidApplication = "InvalidApplication";

		// Token: 0x04000A7E RID: 2686
		internal const string StandardOutputEncodingNotAllowed = "StandardOutputEncodingNotAllowed";

		// Token: 0x04000A7F RID: 2687
		internal const string StandardErrorEncodingNotAllowed = "StandardErrorEncodingNotAllowed";

		// Token: 0x04000A80 RID: 2688
		internal const string CountersOOM = "CountersOOM";

		// Token: 0x04000A81 RID: 2689
		internal const string MappingCorrupted = "MappingCorrupted";

		// Token: 0x04000A82 RID: 2690
		internal const string SetSecurityDescriptorFailed = "SetSecurityDescriptorFailed";

		// Token: 0x04000A83 RID: 2691
		internal const string CantCreateFileMapping = "CantCreateFileMapping";

		// Token: 0x04000A84 RID: 2692
		internal const string CantMapFileView = "CantMapFileView";

		// Token: 0x04000A85 RID: 2693
		internal const string CantGetMappingSize = "CantGetMappingSize";

		// Token: 0x04000A86 RID: 2694
		internal const string CantGetStandardOut = "CantGetStandardOut";

		// Token: 0x04000A87 RID: 2695
		internal const string CantGetStandardIn = "CantGetStandardIn";

		// Token: 0x04000A88 RID: 2696
		internal const string CantGetStandardError = "CantGetStandardError";

		// Token: 0x04000A89 RID: 2697
		internal const string CantMixSyncAsyncOperation = "CantMixSyncAsyncOperation";

		// Token: 0x04000A8A RID: 2698
		internal const string NoFileMappingSize = "NoFileMappingSize";

		// Token: 0x04000A8B RID: 2699
		internal const string EnvironmentBlockTooLong = "EnvironmentBlockTooLong";

		// Token: 0x04000A8C RID: 2700
		internal const string CantSetDuplicatePassword = "CantSetDuplicatePassword";

		// Token: 0x04000A8D RID: 2701
		internal const string Arg_InvalidSerialPort = "Arg_InvalidSerialPort";

		// Token: 0x04000A8E RID: 2702
		internal const string Arg_InvalidSerialPortExtended = "Arg_InvalidSerialPortExtended";

		// Token: 0x04000A8F RID: 2703
		internal const string Arg_SecurityException = "Arg_SecurityException";

		// Token: 0x04000A90 RID: 2704
		internal const string Argument_InvalidOffLen = "Argument_InvalidOffLen";

		// Token: 0x04000A91 RID: 2705
		internal const string ArgumentNull_Array = "ArgumentNull_Array";

		// Token: 0x04000A92 RID: 2706
		internal const string ArgumentNull_Buffer = "ArgumentNull_Buffer";

		// Token: 0x04000A93 RID: 2707
		internal const string ArgumentOutOfRange_Bounds_Lower_Upper = "ArgumentOutOfRange_Bounds_Lower_Upper";

		// Token: 0x04000A94 RID: 2708
		internal const string ArgumentOutOfRange_Enum = "ArgumentOutOfRange_Enum";

		// Token: 0x04000A95 RID: 2709
		internal const string ArgumentOutOfRange_NeedNonNegNumRequired = "ArgumentOutOfRange_NeedNonNegNumRequired";

		// Token: 0x04000A96 RID: 2710
		internal const string ArgumentOutOfRange_NeedPosNum = "ArgumentOutOfRange_NeedPosNum";

		// Token: 0x04000A97 RID: 2711
		internal const string ArgumentOutOfRange_Timeout = "ArgumentOutOfRange_Timeout";

		// Token: 0x04000A98 RID: 2712
		internal const string ArgumentOutOfRange_WriteTimeout = "ArgumentOutOfRange_WriteTimeout";

		// Token: 0x04000A99 RID: 2713
		internal const string ArgumentOutOfRange_OffsetOut = "ArgumentOutOfRange_OffsetOut";

		// Token: 0x04000A9A RID: 2714
		internal const string IndexOutOfRange_IORaceCondition = "IndexOutOfRange_IORaceCondition";

		// Token: 0x04000A9B RID: 2715
		internal const string IO_BindHandleFailed = "IO_BindHandleFailed";

		// Token: 0x04000A9C RID: 2716
		internal const string IO_OperationAborted = "IO_OperationAborted";

		// Token: 0x04000A9D RID: 2717
		internal const string NotSupported_UnseekableStream = "NotSupported_UnseekableStream";

		// Token: 0x04000A9E RID: 2718
		internal const string IO_EOF_ReadBeyondEOF = "IO_EOF_ReadBeyondEOF";

		// Token: 0x04000A9F RID: 2719
		internal const string ObjectDisposed_StreamClosed = "ObjectDisposed_StreamClosed";

		// Token: 0x04000AA0 RID: 2720
		internal const string UnauthorizedAccess_IODenied_Path = "UnauthorizedAccess_IODenied_Path";

		// Token: 0x04000AA1 RID: 2721
		internal const string IO_UnknownError = "IO_UnknownError";

		// Token: 0x04000AA2 RID: 2722
		internal const string Arg_WrongAsyncResult = "Arg_WrongAsyncResult";

		// Token: 0x04000AA3 RID: 2723
		internal const string InvalidOperation_EndReadCalledMultiple = "InvalidOperation_EndReadCalledMultiple";

		// Token: 0x04000AA4 RID: 2724
		internal const string InvalidOperation_EndWriteCalledMultiple = "InvalidOperation_EndWriteCalledMultiple";

		// Token: 0x04000AA5 RID: 2725
		internal const string IO_PortNotFound = "IO_PortNotFound";

		// Token: 0x04000AA6 RID: 2726
		internal const string IO_PortNotFoundFileName = "IO_PortNotFoundFileName";

		// Token: 0x04000AA7 RID: 2727
		internal const string UnauthorizedAccess_IODenied_NoPathName = "UnauthorizedAccess_IODenied_NoPathName";

		// Token: 0x04000AA8 RID: 2728
		internal const string IO_PathTooLong = "IO_PathTooLong";

		// Token: 0x04000AA9 RID: 2729
		internal const string IO_SharingViolation_NoFileName = "IO_SharingViolation_NoFileName";

		// Token: 0x04000AAA RID: 2730
		internal const string IO_SharingViolation_File = "IO_SharingViolation_File";

		// Token: 0x04000AAB RID: 2731
		internal const string NotSupported_UnwritableStream = "NotSupported_UnwritableStream";

		// Token: 0x04000AAC RID: 2732
		internal const string ObjectDisposed_WriterClosed = "ObjectDisposed_WriterClosed";

		// Token: 0x04000AAD RID: 2733
		internal const string BaseStream_Invalid_Not_Open = "BaseStream_Invalid_Not_Open";

		// Token: 0x04000AAE RID: 2734
		internal const string PortNameEmpty_String = "PortNameEmpty_String";

		// Token: 0x04000AAF RID: 2735
		internal const string Port_not_open = "Port_not_open";

		// Token: 0x04000AB0 RID: 2736
		internal const string Port_already_open = "Port_already_open";

		// Token: 0x04000AB1 RID: 2737
		internal const string Cant_be_set_when_open = "Cant_be_set_when_open";

		// Token: 0x04000AB2 RID: 2738
		internal const string Max_Baud = "Max_Baud";

		// Token: 0x04000AB3 RID: 2739
		internal const string In_Break_State = "In_Break_State";

		// Token: 0x04000AB4 RID: 2740
		internal const string Write_timed_out = "Write_timed_out";

		// Token: 0x04000AB5 RID: 2741
		internal const string CantSetRtsWithHandshaking = "CantSetRtsWithHandshaking";

		// Token: 0x04000AB6 RID: 2742
		internal const string NotSupportedOS = "NotSupportedOS";

		// Token: 0x04000AB7 RID: 2743
		internal const string NotSupportedEncoding = "NotSupportedEncoding";

		// Token: 0x04000AB8 RID: 2744
		internal const string BaudRate = "BaudRate";

		// Token: 0x04000AB9 RID: 2745
		internal const string DataBits = "DataBits";

		// Token: 0x04000ABA RID: 2746
		internal const string DiscardNull = "DiscardNull";

		// Token: 0x04000ABB RID: 2747
		internal const string DtrEnable = "DtrEnable";

		// Token: 0x04000ABC RID: 2748
		internal const string Encoding = "Encoding";

		// Token: 0x04000ABD RID: 2749
		internal const string Handshake = "Handshake";

		// Token: 0x04000ABE RID: 2750
		internal const string NewLine = "NewLine";

		// Token: 0x04000ABF RID: 2751
		internal const string Parity = "Parity";

		// Token: 0x04000AC0 RID: 2752
		internal const string ParityReplace = "ParityReplace";

		// Token: 0x04000AC1 RID: 2753
		internal const string PortName = "PortName";

		// Token: 0x04000AC2 RID: 2754
		internal const string ReadBufferSize = "ReadBufferSize";

		// Token: 0x04000AC3 RID: 2755
		internal const string ReadTimeout = "ReadTimeout";

		// Token: 0x04000AC4 RID: 2756
		internal const string ReceivedBytesThreshold = "ReceivedBytesThreshold";

		// Token: 0x04000AC5 RID: 2757
		internal const string RtsEnable = "RtsEnable";

		// Token: 0x04000AC6 RID: 2758
		internal const string SerialPortDesc = "SerialPortDesc";

		// Token: 0x04000AC7 RID: 2759
		internal const string StopBits = "StopBits";

		// Token: 0x04000AC8 RID: 2760
		internal const string WriteBufferSize = "WriteBufferSize";

		// Token: 0x04000AC9 RID: 2761
		internal const string WriteTimeout = "WriteTimeout";

		// Token: 0x04000ACA RID: 2762
		internal const string SerialErrorReceived = "SerialErrorReceived";

		// Token: 0x04000ACB RID: 2763
		internal const string SerialPinChanged = "SerialPinChanged";

		// Token: 0x04000ACC RID: 2764
		internal const string SerialDataReceived = "SerialDataReceived";

		// Token: 0x04000ACD RID: 2765
		internal const string CounterType = "CounterType";

		// Token: 0x04000ACE RID: 2766
		internal const string CounterName = "CounterName";

		// Token: 0x04000ACF RID: 2767
		internal const string CounterHelp = "CounterHelp";

		// Token: 0x04000AD0 RID: 2768
		internal const string EventLogDesc = "EventLogDesc";

		// Token: 0x04000AD1 RID: 2769
		internal const string ErrorDataReceived = "ErrorDataReceived";

		// Token: 0x04000AD2 RID: 2770
		internal const string LogEntries = "LogEntries";

		// Token: 0x04000AD3 RID: 2771
		internal const string LogLog = "LogLog";

		// Token: 0x04000AD4 RID: 2772
		internal const string LogMachineName = "LogMachineName";

		// Token: 0x04000AD5 RID: 2773
		internal const string LogMonitoring = "LogMonitoring";

		// Token: 0x04000AD6 RID: 2774
		internal const string LogSynchronizingObject = "LogSynchronizingObject";

		// Token: 0x04000AD7 RID: 2775
		internal const string LogSource = "LogSource";

		// Token: 0x04000AD8 RID: 2776
		internal const string LogEntryWritten = "LogEntryWritten";

		// Token: 0x04000AD9 RID: 2777
		internal const string LogEntryMachineName = "LogEntryMachineName";

		// Token: 0x04000ADA RID: 2778
		internal const string LogEntryData = "LogEntryData";

		// Token: 0x04000ADB RID: 2779
		internal const string LogEntryIndex = "LogEntryIndex";

		// Token: 0x04000ADC RID: 2780
		internal const string LogEntryCategory = "LogEntryCategory";

		// Token: 0x04000ADD RID: 2781
		internal const string LogEntryCategoryNumber = "LogEntryCategoryNumber";

		// Token: 0x04000ADE RID: 2782
		internal const string LogEntryEventID = "LogEntryEventID";

		// Token: 0x04000ADF RID: 2783
		internal const string LogEntryEntryType = "LogEntryEntryType";

		// Token: 0x04000AE0 RID: 2784
		internal const string LogEntryMessage = "LogEntryMessage";

		// Token: 0x04000AE1 RID: 2785
		internal const string LogEntrySource = "LogEntrySource";

		// Token: 0x04000AE2 RID: 2786
		internal const string LogEntryReplacementStrings = "LogEntryReplacementStrings";

		// Token: 0x04000AE3 RID: 2787
		internal const string LogEntryResourceId = "LogEntryResourceId";

		// Token: 0x04000AE4 RID: 2788
		internal const string LogEntryTimeGenerated = "LogEntryTimeGenerated";

		// Token: 0x04000AE5 RID: 2789
		internal const string LogEntryTimeWritten = "LogEntryTimeWritten";

		// Token: 0x04000AE6 RID: 2790
		internal const string LogEntryUserName = "LogEntryUserName";

		// Token: 0x04000AE7 RID: 2791
		internal const string OutputDataReceived = "OutputDataReceived";

		// Token: 0x04000AE8 RID: 2792
		internal const string PC_CounterHelp = "PC_CounterHelp";

		// Token: 0x04000AE9 RID: 2793
		internal const string PC_CounterType = "PC_CounterType";

		// Token: 0x04000AEA RID: 2794
		internal const string PC_ReadOnly = "PC_ReadOnly";

		// Token: 0x04000AEB RID: 2795
		internal const string PC_RawValue = "PC_RawValue";

		// Token: 0x04000AEC RID: 2796
		internal const string ProcessAssociated = "ProcessAssociated";

		// Token: 0x04000AED RID: 2797
		internal const string ProcessDesc = "ProcessDesc";

		// Token: 0x04000AEE RID: 2798
		internal const string ProcessExitCode = "ProcessExitCode";

		// Token: 0x04000AEF RID: 2799
		internal const string ProcessTerminated = "ProcessTerminated";

		// Token: 0x04000AF0 RID: 2800
		internal const string ProcessExitTime = "ProcessExitTime";

		// Token: 0x04000AF1 RID: 2801
		internal const string ProcessHandle = "ProcessHandle";

		// Token: 0x04000AF2 RID: 2802
		internal const string ProcessHandleCount = "ProcessHandleCount";

		// Token: 0x04000AF3 RID: 2803
		internal const string ProcessId = "ProcessId";

		// Token: 0x04000AF4 RID: 2804
		internal const string ProcessMachineName = "ProcessMachineName";

		// Token: 0x04000AF5 RID: 2805
		internal const string ProcessMainModule = "ProcessMainModule";

		// Token: 0x04000AF6 RID: 2806
		internal const string ProcessModules = "ProcessModules";

		// Token: 0x04000AF7 RID: 2807
		internal const string ProcessSynchronizingObject = "ProcessSynchronizingObject";

		// Token: 0x04000AF8 RID: 2808
		internal const string ProcessSessionId = "ProcessSessionId";

		// Token: 0x04000AF9 RID: 2809
		internal const string ProcessThreads = "ProcessThreads";

		// Token: 0x04000AFA RID: 2810
		internal const string ProcessEnableRaisingEvents = "ProcessEnableRaisingEvents";

		// Token: 0x04000AFB RID: 2811
		internal const string ProcessExited = "ProcessExited";

		// Token: 0x04000AFC RID: 2812
		internal const string ProcessFileName = "ProcessFileName";

		// Token: 0x04000AFD RID: 2813
		internal const string ProcessWorkingDirectory = "ProcessWorkingDirectory";

		// Token: 0x04000AFE RID: 2814
		internal const string ProcessBasePriority = "ProcessBasePriority";

		// Token: 0x04000AFF RID: 2815
		internal const string ProcessMainWindowHandle = "ProcessMainWindowHandle";

		// Token: 0x04000B00 RID: 2816
		internal const string ProcessMainWindowTitle = "ProcessMainWindowTitle";

		// Token: 0x04000B01 RID: 2817
		internal const string ProcessMaxWorkingSet = "ProcessMaxWorkingSet";

		// Token: 0x04000B02 RID: 2818
		internal const string ProcessMinWorkingSet = "ProcessMinWorkingSet";

		// Token: 0x04000B03 RID: 2819
		internal const string ProcessNonpagedSystemMemorySize = "ProcessNonpagedSystemMemorySize";

		// Token: 0x04000B04 RID: 2820
		internal const string ProcessPagedMemorySize = "ProcessPagedMemorySize";

		// Token: 0x04000B05 RID: 2821
		internal const string ProcessPagedSystemMemorySize = "ProcessPagedSystemMemorySize";

		// Token: 0x04000B06 RID: 2822
		internal const string ProcessPeakPagedMemorySize = "ProcessPeakPagedMemorySize";

		// Token: 0x04000B07 RID: 2823
		internal const string ProcessPeakWorkingSet = "ProcessPeakWorkingSet";

		// Token: 0x04000B08 RID: 2824
		internal const string ProcessPeakVirtualMemorySize = "ProcessPeakVirtualMemorySize";

		// Token: 0x04000B09 RID: 2825
		internal const string ProcessPriorityBoostEnabled = "ProcessPriorityBoostEnabled";

		// Token: 0x04000B0A RID: 2826
		internal const string ProcessPriorityClass = "ProcessPriorityClass";

		// Token: 0x04000B0B RID: 2827
		internal const string ProcessPrivateMemorySize = "ProcessPrivateMemorySize";

		// Token: 0x04000B0C RID: 2828
		internal const string ProcessPrivilegedProcessorTime = "ProcessPrivilegedProcessorTime";

		// Token: 0x04000B0D RID: 2829
		internal const string ProcessProcessName = "ProcessProcessName";

		// Token: 0x04000B0E RID: 2830
		internal const string ProcessProcessorAffinity = "ProcessProcessorAffinity";

		// Token: 0x04000B0F RID: 2831
		internal const string ProcessResponding = "ProcessResponding";

		// Token: 0x04000B10 RID: 2832
		internal const string ProcessStandardError = "ProcessStandardError";

		// Token: 0x04000B11 RID: 2833
		internal const string ProcessStandardInput = "ProcessStandardInput";

		// Token: 0x04000B12 RID: 2834
		internal const string ProcessStandardOutput = "ProcessStandardOutput";

		// Token: 0x04000B13 RID: 2835
		internal const string ProcessStartInfo = "ProcessStartInfo";

		// Token: 0x04000B14 RID: 2836
		internal const string ProcessStartTime = "ProcessStartTime";

		// Token: 0x04000B15 RID: 2837
		internal const string ProcessTotalProcessorTime = "ProcessTotalProcessorTime";

		// Token: 0x04000B16 RID: 2838
		internal const string ProcessUserProcessorTime = "ProcessUserProcessorTime";

		// Token: 0x04000B17 RID: 2839
		internal const string ProcessVirtualMemorySize = "ProcessVirtualMemorySize";

		// Token: 0x04000B18 RID: 2840
		internal const string ProcessWorkingSet = "ProcessWorkingSet";

		// Token: 0x04000B19 RID: 2841
		internal const string ProcModModuleName = "ProcModModuleName";

		// Token: 0x04000B1A RID: 2842
		internal const string ProcModFileName = "ProcModFileName";

		// Token: 0x04000B1B RID: 2843
		internal const string ProcModBaseAddress = "ProcModBaseAddress";

		// Token: 0x04000B1C RID: 2844
		internal const string ProcModModuleMemorySize = "ProcModModuleMemorySize";

		// Token: 0x04000B1D RID: 2845
		internal const string ProcModEntryPointAddress = "ProcModEntryPointAddress";

		// Token: 0x04000B1E RID: 2846
		internal const string ProcessVerb = "ProcessVerb";

		// Token: 0x04000B1F RID: 2847
		internal const string ProcessArguments = "ProcessArguments";

		// Token: 0x04000B20 RID: 2848
		internal const string ProcessErrorDialog = "ProcessErrorDialog";

		// Token: 0x04000B21 RID: 2849
		internal const string ProcessWindowStyle = "ProcessWindowStyle";

		// Token: 0x04000B22 RID: 2850
		internal const string ProcessCreateNoWindow = "ProcessCreateNoWindow";

		// Token: 0x04000B23 RID: 2851
		internal const string ProcessEnvironmentVariables = "ProcessEnvironmentVariables";

		// Token: 0x04000B24 RID: 2852
		internal const string ProcessRedirectStandardInput = "ProcessRedirectStandardInput";

		// Token: 0x04000B25 RID: 2853
		internal const string ProcessRedirectStandardOutput = "ProcessRedirectStandardOutput";

		// Token: 0x04000B26 RID: 2854
		internal const string ProcessRedirectStandardError = "ProcessRedirectStandardError";

		// Token: 0x04000B27 RID: 2855
		internal const string ProcessUseShellExecute = "ProcessUseShellExecute";

		// Token: 0x04000B28 RID: 2856
		internal const string ThreadBasePriority = "ThreadBasePriority";

		// Token: 0x04000B29 RID: 2857
		internal const string ThreadCurrentPriority = "ThreadCurrentPriority";

		// Token: 0x04000B2A RID: 2858
		internal const string ThreadId = "ThreadId";

		// Token: 0x04000B2B RID: 2859
		internal const string ThreadPriorityBoostEnabled = "ThreadPriorityBoostEnabled";

		// Token: 0x04000B2C RID: 2860
		internal const string ThreadPriorityLevel = "ThreadPriorityLevel";

		// Token: 0x04000B2D RID: 2861
		internal const string ThreadPrivilegedProcessorTime = "ThreadPrivilegedProcessorTime";

		// Token: 0x04000B2E RID: 2862
		internal const string ThreadStartAddress = "ThreadStartAddress";

		// Token: 0x04000B2F RID: 2863
		internal const string ThreadStartTime = "ThreadStartTime";

		// Token: 0x04000B30 RID: 2864
		internal const string ThreadThreadState = "ThreadThreadState";

		// Token: 0x04000B31 RID: 2865
		internal const string ThreadTotalProcessorTime = "ThreadTotalProcessorTime";

		// Token: 0x04000B32 RID: 2866
		internal const string ThreadUserProcessorTime = "ThreadUserProcessorTime";

		// Token: 0x04000B33 RID: 2867
		internal const string ThreadWaitReason = "ThreadWaitReason";

		// Token: 0x04000B34 RID: 2868
		internal const string VerbEditorDefault = "VerbEditorDefault";

		// Token: 0x04000B35 RID: 2869
		internal const string AppSettingsReaderNoKey = "AppSettingsReaderNoKey";

		// Token: 0x04000B36 RID: 2870
		internal const string AppSettingsReaderNoParser = "AppSettingsReaderNoParser";

		// Token: 0x04000B37 RID: 2871
		internal const string AppSettingsReaderCantParse = "AppSettingsReaderCantParse";

		// Token: 0x04000B38 RID: 2872
		internal const string AppSettingsReaderEmptyString = "AppSettingsReaderEmptyString";

		// Token: 0x04000B39 RID: 2873
		internal const string InvalidPermissionState = "InvalidPermissionState";

		// Token: 0x04000B3A RID: 2874
		internal const string PermissionNumberOfElements = "PermissionNumberOfElements";

		// Token: 0x04000B3B RID: 2875
		internal const string PermissionItemExists = "PermissionItemExists";

		// Token: 0x04000B3C RID: 2876
		internal const string PermissionItemDoesntExist = "PermissionItemDoesntExist";

		// Token: 0x04000B3D RID: 2877
		internal const string PermissionBadParameterEnum = "PermissionBadParameterEnum";

		// Token: 0x04000B3E RID: 2878
		internal const string PermissionInvalidLength = "PermissionInvalidLength";

		// Token: 0x04000B3F RID: 2879
		internal const string PermissionTypeMismatch = "PermissionTypeMismatch";

		// Token: 0x04000B40 RID: 2880
		internal const string Argument_NotAPermissionElement = "Argument_NotAPermissionElement";

		// Token: 0x04000B41 RID: 2881
		internal const string Argument_InvalidXMLBadVersion = "Argument_InvalidXMLBadVersion";

		// Token: 0x04000B42 RID: 2882
		internal const string InvalidPermissionLevel = "InvalidPermissionLevel";

		// Token: 0x04000B43 RID: 2883
		internal const string TargetNotWebBrowserPermissionLevel = "TargetNotWebBrowserPermissionLevel";

		// Token: 0x04000B44 RID: 2884
		internal const string WebBrowserBadXml = "WebBrowserBadXml";

		// Token: 0x04000B45 RID: 2885
		internal const string KeyedCollNeedNonNegativeNum = "KeyedCollNeedNonNegativeNum";

		// Token: 0x04000B46 RID: 2886
		internal const string KeyedCollDuplicateKey = "KeyedCollDuplicateKey";

		// Token: 0x04000B47 RID: 2887
		internal const string KeyedCollReferenceKeyNotFound = "KeyedCollReferenceKeyNotFound";

		// Token: 0x04000B48 RID: 2888
		internal const string KeyedCollKeyNotFound = "KeyedCollKeyNotFound";

		// Token: 0x04000B49 RID: 2889
		internal const string KeyedCollInvalidKey = "KeyedCollInvalidKey";

		// Token: 0x04000B4A RID: 2890
		internal const string KeyedCollCapacityOverflow = "KeyedCollCapacityOverflow";

		// Token: 0x04000B4B RID: 2891
		internal const string InvalidOperation_EnumEnded = "InvalidOperation_EnumEnded";

		// Token: 0x04000B4C RID: 2892
		internal const string OrderedDictionary_ReadOnly = "OrderedDictionary_ReadOnly";

		// Token: 0x04000B4D RID: 2893
		internal const string OrderedDictionary_SerializationMismatch = "OrderedDictionary_SerializationMismatch";

		// Token: 0x04000B4E RID: 2894
		internal const string Async_ExceptionOccurred = "Async_ExceptionOccurred";

		// Token: 0x04000B4F RID: 2895
		internal const string Async_QueueingFailed = "Async_QueueingFailed";

		// Token: 0x04000B50 RID: 2896
		internal const string Async_OperationCancelled = "Async_OperationCancelled";

		// Token: 0x04000B51 RID: 2897
		internal const string Async_OperationAlreadyCompleted = "Async_OperationAlreadyCompleted";

		// Token: 0x04000B52 RID: 2898
		internal const string Async_NullDelegate = "Async_NullDelegate";

		// Token: 0x04000B53 RID: 2899
		internal const string BackgroundWorker_AlreadyRunning = "BackgroundWorker_AlreadyRunning";

		// Token: 0x04000B54 RID: 2900
		internal const string BackgroundWorker_CancellationNotSupported = "BackgroundWorker_CancellationNotSupported";

		// Token: 0x04000B55 RID: 2901
		internal const string BackgroundWorker_OperationCompleted = "BackgroundWorker_OperationCompleted";

		// Token: 0x04000B56 RID: 2902
		internal const string BackgroundWorker_ProgressNotSupported = "BackgroundWorker_ProgressNotSupported";

		// Token: 0x04000B57 RID: 2903
		internal const string BackgroundWorker_WorkerAlreadyRunning = "BackgroundWorker_WorkerAlreadyRunning";

		// Token: 0x04000B58 RID: 2904
		internal const string BackgroundWorker_WorkerDoesntReportProgress = "BackgroundWorker_WorkerDoesntReportProgress";

		// Token: 0x04000B59 RID: 2905
		internal const string BackgroundWorker_WorkerDoesntSupportCancellation = "BackgroundWorker_WorkerDoesntSupportCancellation";

		// Token: 0x04000B5A RID: 2906
		internal const string Async_ProgressChangedEventArgs_ProgressPercentage = "Async_ProgressChangedEventArgs_ProgressPercentage";

		// Token: 0x04000B5B RID: 2907
		internal const string Async_ProgressChangedEventArgs_UserState = "Async_ProgressChangedEventArgs_UserState";

		// Token: 0x04000B5C RID: 2908
		internal const string Async_AsyncEventArgs_Cancelled = "Async_AsyncEventArgs_Cancelled";

		// Token: 0x04000B5D RID: 2909
		internal const string Async_AsyncEventArgs_Error = "Async_AsyncEventArgs_Error";

		// Token: 0x04000B5E RID: 2910
		internal const string Async_AsyncEventArgs_UserState = "Async_AsyncEventArgs_UserState";

		// Token: 0x04000B5F RID: 2911
		internal const string BackgroundWorker_CancellationPending = "BackgroundWorker_CancellationPending";

		// Token: 0x04000B60 RID: 2912
		internal const string BackgroundWorker_DoWork = "BackgroundWorker_DoWork";

		// Token: 0x04000B61 RID: 2913
		internal const string BackgroundWorker_IsBusy = "BackgroundWorker_IsBusy";

		// Token: 0x04000B62 RID: 2914
		internal const string BackgroundWorker_ProgressChanged = "BackgroundWorker_ProgressChanged";

		// Token: 0x04000B63 RID: 2915
		internal const string BackgroundWorker_RunWorkerCompleted = "BackgroundWorker_RunWorkerCompleted";

		// Token: 0x04000B64 RID: 2916
		internal const string BackgroundWorker_WorkerReportsProgress = "BackgroundWorker_WorkerReportsProgress";

		// Token: 0x04000B65 RID: 2917
		internal const string BackgroundWorker_WorkerSupportsCancellation = "BackgroundWorker_WorkerSupportsCancellation";

		// Token: 0x04000B66 RID: 2918
		internal const string BackgroundWorker_DoWorkEventArgs_Argument = "BackgroundWorker_DoWorkEventArgs_Argument";

		// Token: 0x04000B67 RID: 2919
		internal const string BackgroundWorker_DoWorkEventArgs_Result = "BackgroundWorker_DoWorkEventArgs_Result";

		// Token: 0x04000B68 RID: 2920
		internal const string BackgroundWorker_Desc = "BackgroundWorker_Desc";

		// Token: 0x04000B69 RID: 2921
		internal const string InstanceCreationEditorDefaultText = "InstanceCreationEditorDefaultText";

		// Token: 0x04000B6A RID: 2922
		internal const string PropertyTabAttributeBadPropertyTabScope = "PropertyTabAttributeBadPropertyTabScope";

		// Token: 0x04000B6B RID: 2923
		internal const string PropertyTabAttributeTypeLoadException = "PropertyTabAttributeTypeLoadException";

		// Token: 0x04000B6C RID: 2924
		internal const string PropertyTabAttributeArrayLengthMismatch = "PropertyTabAttributeArrayLengthMismatch";

		// Token: 0x04000B6D RID: 2925
		internal const string PropertyTabAttributeParamsBothNull = "PropertyTabAttributeParamsBothNull";

		// Token: 0x04000B6E RID: 2926
		internal const string InstanceDescriptorCannotBeStatic = "InstanceDescriptorCannotBeStatic";

		// Token: 0x04000B6F RID: 2927
		internal const string InstanceDescriptorMustBeStatic = "InstanceDescriptorMustBeStatic";

		// Token: 0x04000B70 RID: 2928
		internal const string InstanceDescriptorMustBeReadable = "InstanceDescriptorMustBeReadable";

		// Token: 0x04000B71 RID: 2929
		internal const string InstanceDescriptorLengthMismatch = "InstanceDescriptorLengthMismatch";

		// Token: 0x04000B72 RID: 2930
		internal const string ToolboxItemAttributeFailedGetType = "ToolboxItemAttributeFailedGetType";

		// Token: 0x04000B73 RID: 2931
		internal const string PropertyDescriptorCollectionBadValue = "PropertyDescriptorCollectionBadValue";

		// Token: 0x04000B74 RID: 2932
		internal const string PropertyDescriptorCollectionBadKey = "PropertyDescriptorCollectionBadKey";

		// Token: 0x04000B75 RID: 2933
		internal const string AspNetHostingPermissionBadXml = "AspNetHostingPermissionBadXml";

		// Token: 0x04000B76 RID: 2934
		internal const string CorruptedGZipHeader = "CorruptedGZipHeader";

		// Token: 0x04000B77 RID: 2935
		internal const string UnknownCompressionMode = "UnknownCompressionMode";

		// Token: 0x04000B78 RID: 2936
		internal const string UnknownState = "UnknownState";

		// Token: 0x04000B79 RID: 2937
		internal const string InvalidHuffmanData = "InvalidHuffmanData";

		// Token: 0x04000B7A RID: 2938
		internal const string InvalidCRC = "InvalidCRC";

		// Token: 0x04000B7B RID: 2939
		internal const string InvalidStreamSize = "InvalidStreamSize";

		// Token: 0x04000B7C RID: 2940
		internal const string UnknownBlockType = "UnknownBlockType";

		// Token: 0x04000B7D RID: 2941
		internal const string InvalidBlockLength = "InvalidBlockLength";

		// Token: 0x04000B7E RID: 2942
		internal const string GenericInvalidData = "GenericInvalidData";

		// Token: 0x04000B7F RID: 2943
		internal const string CannotReadFromDeflateStream = "CannotReadFromDeflateStream";

		// Token: 0x04000B80 RID: 2944
		internal const string CannotWriteToDeflateStream = "CannotWriteToDeflateStream";

		// Token: 0x04000B81 RID: 2945
		internal const string NotReadableStream = "NotReadableStream";

		// Token: 0x04000B82 RID: 2946
		internal const string NotWriteableStream = "NotWriteableStream";

		// Token: 0x04000B83 RID: 2947
		internal const string InvalidArgumentOffsetCount = "InvalidArgumentOffsetCount";

		// Token: 0x04000B84 RID: 2948
		internal const string InvalidBeginCall = "InvalidBeginCall";

		// Token: 0x04000B85 RID: 2949
		internal const string InvalidEndCall = "InvalidEndCall";

		// Token: 0x04000B86 RID: 2950
		internal const string StreamSizeOverflow = "StreamSizeOverflow";

		// Token: 0x04000B87 RID: 2951
		internal const string ZLibErrorDLLLoadError = "ZLibErrorDLLLoadError";

		// Token: 0x04000B88 RID: 2952
		internal const string ZLibErrorUnexpected = "ZLibErrorUnexpected";

		// Token: 0x04000B89 RID: 2953
		internal const string ZLibErrorInconsistentStream = "ZLibErrorInconsistentStream";

		// Token: 0x04000B8A RID: 2954
		internal const string ZLibErrorSteamFreedPrematurely = "ZLibErrorSteamFreedPrematurely";

		// Token: 0x04000B8B RID: 2955
		internal const string ZLibErrorNotEnoughMemory = "ZLibErrorNotEnoughMemory";

		// Token: 0x04000B8C RID: 2956
		internal const string ZLibErrorIncorrectInitParameters = "ZLibErrorIncorrectInitParameters";

		// Token: 0x04000B8D RID: 2957
		internal const string ZLibErrorVersionMismatch = "ZLibErrorVersionMismatch";

		// Token: 0x04000B8E RID: 2958
		internal const string InvalidOperation_HCCountOverflow = "InvalidOperation_HCCountOverflow";

		// Token: 0x04000B8F RID: 2959
		internal const string Argument_InvalidThreshold = "Argument_InvalidThreshold";

		// Token: 0x04000B90 RID: 2960
		internal const string Argument_SemaphoreInitialMaximum = "Argument_SemaphoreInitialMaximum";

		// Token: 0x04000B91 RID: 2961
		internal const string Argument_WaitHandleNameTooLong = "Argument_WaitHandleNameTooLong";

		// Token: 0x04000B92 RID: 2962
		internal const string WaitHandleCannotBeOpenedException_InvalidHandle = "WaitHandleCannotBeOpenedException_InvalidHandle";

		// Token: 0x04000B93 RID: 2963
		internal const string ArgumentNotAPermissionElement = "ArgumentNotAPermissionElement";

		// Token: 0x04000B94 RID: 2964
		internal const string ArgumentWrongType = "ArgumentWrongType";

		// Token: 0x04000B95 RID: 2965
		internal const string BadXmlVersion = "BadXmlVersion";

		// Token: 0x04000B96 RID: 2966
		internal const string BinarySerializationNotSupported = "BinarySerializationNotSupported";

		// Token: 0x04000B97 RID: 2967
		internal const string BothScopeAttributes = "BothScopeAttributes";

		// Token: 0x04000B98 RID: 2968
		internal const string NoScopeAttributes = "NoScopeAttributes";

		// Token: 0x04000B99 RID: 2969
		internal const string PositionOutOfRange = "PositionOutOfRange";

		// Token: 0x04000B9A RID: 2970
		internal const string ProviderInstantiationFailed = "ProviderInstantiationFailed";

		// Token: 0x04000B9B RID: 2971
		internal const string ProviderTypeLoadFailed = "ProviderTypeLoadFailed";

		// Token: 0x04000B9C RID: 2972
		internal const string SaveAppScopedNotSupported = "SaveAppScopedNotSupported";

		// Token: 0x04000B9D RID: 2973
		internal const string SettingsResetFailed = "SettingsResetFailed";

		// Token: 0x04000B9E RID: 2974
		internal const string SettingsSaveFailed = "SettingsSaveFailed";

		// Token: 0x04000B9F RID: 2975
		internal const string SettingsSaveFailedNoSection = "SettingsSaveFailedNoSection";

		// Token: 0x04000BA0 RID: 2976
		internal const string StringDeserializationFailed = "StringDeserializationFailed";

		// Token: 0x04000BA1 RID: 2977
		internal const string StringSerializationFailed = "StringSerializationFailed";

		// Token: 0x04000BA2 RID: 2978
		internal const string UnknownSerializationFormat = "UnknownSerializationFormat";

		// Token: 0x04000BA3 RID: 2979
		internal const string UnknownSeekOrigin = "UnknownSeekOrigin";

		// Token: 0x04000BA4 RID: 2980
		internal const string UnknownUserLevel = "UnknownUserLevel";

		// Token: 0x04000BA5 RID: 2981
		internal const string UserSettingsNotSupported = "UserSettingsNotSupported";

		// Token: 0x04000BA6 RID: 2982
		internal const string XmlDeserializationFailed = "XmlDeserializationFailed";

		// Token: 0x04000BA7 RID: 2983
		internal const string XmlSerializationFailed = "XmlSerializationFailed";

		// Token: 0x04000BA8 RID: 2984
		internal const string MemberRelationshipService_RelationshipNotSupported = "MemberRelationshipService_RelationshipNotSupported";

		// Token: 0x04000BA9 RID: 2985
		internal const string MaskedTextProviderPasswordAndPromptCharError = "MaskedTextProviderPasswordAndPromptCharError";

		// Token: 0x04000BAA RID: 2986
		internal const string MaskedTextProviderInvalidCharError = "MaskedTextProviderInvalidCharError";

		// Token: 0x04000BAB RID: 2987
		internal const string MaskedTextProviderMaskNullOrEmpty = "MaskedTextProviderMaskNullOrEmpty";

		// Token: 0x04000BAC RID: 2988
		internal const string MaskedTextProviderMaskInvalidChar = "MaskedTextProviderMaskInvalidChar";

		// Token: 0x04000BAD RID: 2989
		internal const string StandardOleMarshalObjectGetMarshalerFailed = "StandardOleMarshalObjectGetMarshalerFailed";

		// Token: 0x04000BAE RID: 2990
		internal const string SoundAPIBadSoundLocation = "SoundAPIBadSoundLocation";

		// Token: 0x04000BAF RID: 2991
		internal const string SoundAPIFileDoesNotExist = "SoundAPIFileDoesNotExist";

		// Token: 0x04000BB0 RID: 2992
		internal const string SoundAPIFormatNotSupported = "SoundAPIFormatNotSupported";

		// Token: 0x04000BB1 RID: 2993
		internal const string SoundAPIInvalidWaveFile = "SoundAPIInvalidWaveFile";

		// Token: 0x04000BB2 RID: 2994
		internal const string SoundAPIInvalidWaveHeader = "SoundAPIInvalidWaveHeader";

		// Token: 0x04000BB3 RID: 2995
		internal const string SoundAPILoadTimedOut = "SoundAPILoadTimedOut";

		// Token: 0x04000BB4 RID: 2996
		internal const string SoundAPILoadTimeout = "SoundAPILoadTimeout";

		// Token: 0x04000BB5 RID: 2997
		internal const string SoundAPIReadError = "SoundAPIReadError";

		// Token: 0x04000BB6 RID: 2998
		internal const string WrongActionForCtor = "WrongActionForCtor";

		// Token: 0x04000BB7 RID: 2999
		internal const string MustBeResetAddOrRemoveActionForCtor = "MustBeResetAddOrRemoveActionForCtor";

		// Token: 0x04000BB8 RID: 3000
		internal const string ResetActionRequiresNullItem = "ResetActionRequiresNullItem";

		// Token: 0x04000BB9 RID: 3001
		internal const string ResetActionRequiresIndexMinus1 = "ResetActionRequiresIndexMinus1";

		// Token: 0x04000BBA RID: 3002
		internal const string IndexCannotBeNegative = "IndexCannotBeNegative";

		// Token: 0x04000BBB RID: 3003
		internal const string ObservableCollectionReentrancyNotAllowed = "ObservableCollectionReentrancyNotAllowed";

		// Token: 0x04000BBC RID: 3004
		internal const string net_ssl_io_already_shutdown = "net_ssl_io_already_shutdown";

		// Token: 0x04000BBD RID: 3005
		internal const string net_io_readwritefailure = "net_io_readwritefailure";

		// Token: 0x04000BBE RID: 3006
		internal const string Cryptography_X509_InvalidFlagCombination = "Cryptography_X509_InvalidFlagCombination";

		// Token: 0x04000BBF RID: 3007
		private static SR loader;

		// Token: 0x04000BC0 RID: 3008
		private ResourceManager resources;
	}
}
