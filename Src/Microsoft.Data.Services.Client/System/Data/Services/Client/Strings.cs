using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200013D RID: 317
	internal static class Strings
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		internal static string Batch_ExpectedContentType(object p0)
		{
			return TextRes.GetString("Batch_ExpectedContentType", new object[] { p0 });
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		internal static string Batch_ExpectedResponse(object p0)
		{
			return TextRes.GetString("Batch_ExpectedResponse", new object[] { p0 });
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002CEFB File Offset: 0x0002B0FB
		internal static string Batch_IncompleteResponseCount
		{
			get
			{
				return TextRes.GetString("Batch_IncompleteResponseCount");
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CF08 File Offset: 0x0002B108
		internal static string Batch_UnexpectedContent(object p0)
		{
			return TextRes.GetString("Batch_UnexpectedContent", new object[] { p0 });
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002CF2B File Offset: 0x0002B12B
		internal static string Context_BaseUri
		{
			get
			{
				return TextRes.GetString("Context_BaseUri");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002CF37 File Offset: 0x0002B137
		internal static string Context_BaseUriRequired
		{
			get
			{
				return TextRes.GetString("Context_BaseUriRequired");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002CF43 File Offset: 0x0002B143
		internal static string Context_ResolveReturnedInvalidUri
		{
			get
			{
				return TextRes.GetString("Context_ResolveReturnedInvalidUri");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002CF4F File Offset: 0x0002B14F
		internal static string Context_RequestUriIsRelativeBaseUriRequired
		{
			get
			{
				return TextRes.GetString("Context_RequestUriIsRelativeBaseUriRequired");
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CF5C File Offset: 0x0002B15C
		internal static string Context_ResolveEntitySetOrBaseUriRequired(object p0)
		{
			return TextRes.GetString("Context_ResolveEntitySetOrBaseUriRequired", new object[] { p0 });
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CF80 File Offset: 0x0002B180
		internal static string Context_CannotConvertKey(object p0)
		{
			return TextRes.GetString("Context_CannotConvertKey", new object[] { p0 });
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002CFA3 File Offset: 0x0002B1A3
		internal static string Context_TrackingExpectsAbsoluteUri
		{
			get
			{
				return TextRes.GetString("Context_TrackingExpectsAbsoluteUri");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002CFAF File Offset: 0x0002B1AF
		internal static string Context_LocationHeaderExpectsAbsoluteUri
		{
			get
			{
				return TextRes.GetString("Context_LocationHeaderExpectsAbsoluteUri");
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002CFBB File Offset: 0x0002B1BB
		internal static string Context_LinkResourceInsertFailure
		{
			get
			{
				return TextRes.GetString("Context_LinkResourceInsertFailure");
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
		internal static string Context_InternalError(object p0)
		{
			return TextRes.GetString("Context_InternalError", new object[] { p0 });
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002CFEB File Offset: 0x0002B1EB
		internal static string Context_BatchExecuteError
		{
			get
			{
				return TextRes.GetString("Context_BatchExecuteError");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002CFF7 File Offset: 0x0002B1F7
		internal static string Context_EntitySetName
		{
			get
			{
				return TextRes.GetString("Context_EntitySetName");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002D003 File Offset: 0x0002B203
		internal static string Context_BatchNotSupportedForNamedStreams
		{
			get
			{
				return TextRes.GetString("Context_BatchNotSupportedForNamedStreams");
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D010 File Offset: 0x0002B210
		internal static string Context_SetSaveStreamWithoutNamedStreamEditLink(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamWithoutNamedStreamEditLink", new object[] { p0 });
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0002D033 File Offset: 0x0002B233
		internal static string Content_EntityWithoutKey
		{
			get
			{
				return TextRes.GetString("Content_EntityWithoutKey");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002D03F File Offset: 0x0002B23F
		internal static string Content_EntityIsNotEntityType
		{
			get
			{
				return TextRes.GetString("Content_EntityIsNotEntityType");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002D04B File Offset: 0x0002B24B
		internal static string Context_EntityNotContained
		{
			get
			{
				return TextRes.GetString("Context_EntityNotContained");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002D057 File Offset: 0x0002B257
		internal static string Context_EntityAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_EntityAlreadyContained");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002D063 File Offset: 0x0002B263
		internal static string Context_DifferentEntityAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_DifferentEntityAlreadyContained");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002D06F File Offset: 0x0002B26F
		internal static string Context_DidNotOriginateAsync
		{
			get
			{
				return TextRes.GetString("Context_DidNotOriginateAsync");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002D07B File Offset: 0x0002B27B
		internal static string Context_AsyncAlreadyDone
		{
			get
			{
				return TextRes.GetString("Context_AsyncAlreadyDone");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002D087 File Offset: 0x0002B287
		internal static string Context_OperationCanceled
		{
			get
			{
				return TextRes.GetString("Context_OperationCanceled");
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D094 File Offset: 0x0002B294
		internal static string Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX(object p0, object p1)
		{
			return TextRes.GetString("Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX", new object[] { p0, p1 });
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002D0BB File Offset: 0x0002B2BB
		internal static string Context_NoLoadWithInsertEnd
		{
			get
			{
				return TextRes.GetString("Context_NoLoadWithInsertEnd");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002D0C7 File Offset: 0x0002B2C7
		internal static string Context_NoRelationWithInsertEnd
		{
			get
			{
				return TextRes.GetString("Context_NoRelationWithInsertEnd");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002D0D3 File Offset: 0x0002B2D3
		internal static string Context_NoRelationWithDeleteEnd
		{
			get
			{
				return TextRes.GetString("Context_NoRelationWithDeleteEnd");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002D0DF File Offset: 0x0002B2DF
		internal static string Context_RelationAlreadyContained
		{
			get
			{
				return TextRes.GetString("Context_RelationAlreadyContained");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002D0EB File Offset: 0x0002B2EB
		internal static string Context_RelationNotRefOrCollection
		{
			get
			{
				return TextRes.GetString("Context_RelationNotRefOrCollection");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002D0F7 File Offset: 0x0002B2F7
		internal static string Context_AddLinkCollectionOnly
		{
			get
			{
				return TextRes.GetString("Context_AddLinkCollectionOnly");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002D103 File Offset: 0x0002B303
		internal static string Context_AddRelatedObjectCollectionOnly
		{
			get
			{
				return TextRes.GetString("Context_AddRelatedObjectCollectionOnly");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0002D10F File Offset: 0x0002B30F
		internal static string Context_AddRelatedObjectSourceDeleted
		{
			get
			{
				return TextRes.GetString("Context_AddRelatedObjectSourceDeleted");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002D11B File Offset: 0x0002B31B
		internal static string Context_SetLinkReferenceOnly
		{
			get
			{
				return TextRes.GetString("Context_SetLinkReferenceOnly");
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D128 File Offset: 0x0002B328
		internal static string Context_NoContentTypeForMediaLink(object p0, object p1)
		{
			return TextRes.GetString("Context_NoContentTypeForMediaLink", new object[] { p0, p1 });
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002D14F File Offset: 0x0002B34F
		internal static string Context_BatchNotSupportedForMediaLink
		{
			get
			{
				return TextRes.GetString("Context_BatchNotSupportedForMediaLink");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002D15B File Offset: 0x0002B35B
		internal static string Context_UnexpectedZeroRawRead
		{
			get
			{
				return TextRes.GetString("Context_UnexpectedZeroRawRead");
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D168 File Offset: 0x0002B368
		internal static string Context_VersionNotSupported(object p0, object p1)
		{
			return TextRes.GetString("Context_VersionNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D190 File Offset: 0x0002B390
		internal static string Context_ResponseVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("Context_ResponseVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002D1B8 File Offset: 0x0002B3B8
		internal static string Context_RequestVersionIsBiggerThanProtocolVersion(object p0, object p1)
		{
			return TextRes.GetString("Context_RequestVersionIsBiggerThanProtocolVersion", new object[] { p0, p1 });
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D1DF File Offset: 0x0002B3DF
		internal static string Context_ChildResourceExists
		{
			get
			{
				return TextRes.GetString("Context_ChildResourceExists");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002D1EB File Offset: 0x0002B3EB
		internal static string Context_ContentTypeRequiredForNamedStream
		{
			get
			{
				return TextRes.GetString("Context_ContentTypeRequiredForNamedStream");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002D1F7 File Offset: 0x0002B3F7
		internal static string Context_EntityNotMediaLinkEntry
		{
			get
			{
				return TextRes.GetString("Context_EntityNotMediaLinkEntry");
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002D204 File Offset: 0x0002B404
		internal static string Context_MLEWithoutSaveStream(object p0)
		{
			return TextRes.GetString("Context_MLEWithoutSaveStream", new object[] { p0 });
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002D228 File Offset: 0x0002B428
		internal static string Context_SetSaveStreamOnMediaEntryProperty(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamOnMediaEntryProperty", new object[] { p0 });
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002D24B File Offset: 0x0002B44B
		internal static string Context_SetSaveStreamWithoutEditMediaLink
		{
			get
			{
				return TextRes.GetString("Context_SetSaveStreamWithoutEditMediaLink");
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002D258 File Offset: 0x0002B458
		internal static string Context_SetSaveStreamOnInvalidEntityState(object p0)
		{
			return TextRes.GetString("Context_SetSaveStreamOnInvalidEntityState", new object[] { p0 });
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002D27C File Offset: 0x0002B47C
		internal static string Context_EntityDoesNotContainNamedStream(object p0)
		{
			return TextRes.GetString("Context_EntityDoesNotContainNamedStream", new object[] { p0 });
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		internal static string Context_MissingSelfAndEditLinkForNamedStream(object p0)
		{
			return TextRes.GetString("Context_MissingSelfAndEditLinkForNamedStream", new object[] { p0 });
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002D2C3 File Offset: 0x0002B4C3
		internal static string Context_BothLocationAndIdMustBeSpecified
		{
			get
			{
				return TextRes.GetString("Context_BothLocationAndIdMustBeSpecified");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002D2CF File Offset: 0x0002B4CF
		internal static string Context_BodyOperationParametersNotAllowedWithGet
		{
			get
			{
				return TextRes.GetString("Context_BodyOperationParametersNotAllowedWithGet");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002D2DB File Offset: 0x0002B4DB
		internal static string Context_MissingOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_MissingOperationParameterName");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002D2E7 File Offset: 0x0002B4E7
		internal static string Context_DuplicateUriOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_DuplicateUriOperationParameterName");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002D2F3 File Offset: 0x0002B4F3
		internal static string Context_DuplicateBodyOperationParameterName
		{
			get
			{
				return TextRes.GetString("Context_DuplicateBodyOperationParameterName");
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002D300 File Offset: 0x0002B500
		internal static string Context_NullKeysAreNotSupported(object p0)
		{
			return TextRes.GetString("Context_NullKeysAreNotSupported", new object[] { p0 });
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002D323 File Offset: 0x0002B523
		internal static string Context_ExecuteExpectsGetOrPost
		{
			get
			{
				return TextRes.GetString("Context_ExecuteExpectsGetOrPost");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002D32F File Offset: 0x0002B52F
		internal static string Context_EndExecuteExpectedVoidResponse
		{
			get
			{
				return TextRes.GetString("Context_EndExecuteExpectedVoidResponse");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0002D33B File Offset: 0x0002B53B
		internal static string Context_NullElementInOperationParameterArray
		{
			get
			{
				return TextRes.GetString("Context_NullElementInOperationParameterArray");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002D347 File Offset: 0x0002B547
		internal static string Context_EntityMetadataBuilderIsRequired
		{
			get
			{
				return TextRes.GetString("Context_EntityMetadataBuilderIsRequired");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002D353 File Offset: 0x0002B553
		internal static string Context_BuildingRequestAndSendingRequestCannotBeUsedTogether
		{
			get
			{
				return TextRes.GetString("Context_BuildingRequestAndSendingRequestCannotBeUsedTogether");
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002D35F File Offset: 0x0002B55F
		internal static string Context_CannotChangeStateToAdded
		{
			get
			{
				return TextRes.GetString("Context_CannotChangeStateToAdded");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002D36B File Offset: 0x0002B56B
		internal static string Context_CannotChangeStateToModifiedIfNotUnchanged
		{
			get
			{
				return TextRes.GetString("Context_CannotChangeStateToModifiedIfNotUnchanged");
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002D378 File Offset: 0x0002B578
		internal static string Context_CannotChangeStateIfAdded(object p0)
		{
			return TextRes.GetString("Context_CannotChangeStateIfAdded", new object[] { p0 });
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002D39B File Offset: 0x0002B59B
		internal static string Context_OnMessageCreatingReturningNull
		{
			get
			{
				return TextRes.GetString("Context_OnMessageCreatingReturningNull");
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002D3A7 File Offset: 0x0002B5A7
		internal static string Context_SendingRequest_InvalidWhenUsingOnMessageCreating
		{
			get
			{
				return TextRes.GetString("Context_SendingRequest_InvalidWhenUsingOnMessageCreating");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002D3B3 File Offset: 0x0002B5B3
		internal static string DataServiceClientFormat_AtomEventsOnlySupportedWithAtomFormat
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_AtomEventsOnlySupportedWithAtomFormat");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002D3BF File Offset: 0x0002B5BF
		internal static string DataServiceClientFormat_LoadServiceModelRequired
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_LoadServiceModelRequired");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002D3CB File Offset: 0x0002B5CB
		internal static string DataServiceClientFormat_JsonUnsupportedForLessThanV3
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_JsonUnsupportedForLessThanV3");
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0002D3D7 File Offset: 0x0002B5D7
		internal static string DataServiceClientFormat_ValidServiceModelRequiredForJson
		{
			get
			{
				return TextRes.GetString("DataServiceClientFormat_ValidServiceModelRequiredForJson");
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		internal static string DataServiceClientFormat_JsonVerboseUnsupported(object p0)
		{
			return TextRes.GetString("DataServiceClientFormat_JsonVerboseUnsupported", new object[] { p0 });
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002D408 File Offset: 0x0002B608
		internal static string Collection_NullCollectionReference(object p0, object p1)
		{
			return TextRes.GetString("Collection_NullCollectionReference", new object[] { p0, p1 });
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002D430 File Offset: 0x0002B630
		internal static string ClientType_MissingOpenProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingOpenProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002D458 File Offset: 0x0002B658
		internal static string Clienttype_MultipleOpenProperty(object p0)
		{
			return TextRes.GetString("Clienttype_MultipleOpenProperty", new object[] { p0 });
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002D47C File Offset: 0x0002B67C
		internal static string ClientType_MissingProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002D4A4 File Offset: 0x0002B6A4
		internal static string ClientType_KeysMustBeSimpleTypes(object p0)
		{
			return TextRes.GetString("ClientType_KeysMustBeSimpleTypes", new object[] { p0 });
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002D4C8 File Offset: 0x0002B6C8
		internal static string ClientType_KeysOnDifferentDeclaredType(object p0)
		{
			return TextRes.GetString("ClientType_KeysOnDifferentDeclaredType", new object[] { p0 });
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002D4EC File Offset: 0x0002B6EC
		internal static string ClientType_MissingMimeTypeProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMimeTypeProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002D514 File Offset: 0x0002B714
		internal static string ClientType_MissingMimeTypeDataProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMimeTypeDataProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002D53C File Offset: 0x0002B73C
		internal static string ClientType_MissingMediaEntryProperty(object p0, object p1)
		{
			return TextRes.GetString("ClientType_MissingMediaEntryProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002D564 File Offset: 0x0002B764
		internal static string ClientType_NoSettableFields(object p0)
		{
			return TextRes.GetString("ClientType_NoSettableFields", new object[] { p0 });
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002D587 File Offset: 0x0002B787
		internal static string ClientType_MultipleImplementationNotSupported
		{
			get
			{
				return TextRes.GetString("ClientType_MultipleImplementationNotSupported");
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002D594 File Offset: 0x0002B794
		internal static string ClientType_NullOpenProperties(object p0)
		{
			return TextRes.GetString("ClientType_NullOpenProperties", new object[] { p0 });
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002D5B8 File Offset: 0x0002B7B8
		internal static string ClientType_Ambiguous(object p0, object p1)
		{
			return TextRes.GetString("ClientType_Ambiguous", new object[] { p0, p1 });
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		internal static string ClientType_UnsupportedType(object p0)
		{
			return TextRes.GetString("ClientType_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002D603 File Offset: 0x0002B803
		internal static string ClientType_CollectionOfCollectionNotSupported
		{
			get
			{
				return TextRes.GetString("ClientType_CollectionOfCollectionNotSupported");
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002D610 File Offset: 0x0002B810
		internal static string ClientType_CollectionPropertyNotSupportedInV2AndBelow(object p0, object p1)
		{
			return TextRes.GetString("ClientType_CollectionPropertyNotSupportedInV2AndBelow", new object[] { p0, p1 });
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002D638 File Offset: 0x0002B838
		internal static string ClientType_MultipleTypesWithSameName(object p0)
		{
			return TextRes.GetString("ClientType_MultipleTypesWithSameName", new object[] { p0 });
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002D65C File Offset: 0x0002B85C
		internal static string WebUtil_CollectionTypeNotSupportedInV2OrBelow(object p0)
		{
			return TextRes.GetString("WebUtil_CollectionTypeNotSupportedInV2OrBelow", new object[] { p0 });
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002D680 File Offset: 0x0002B880
		internal static string WebUtil_TypeMismatchInCollection(object p0)
		{
			return TextRes.GetString("WebUtil_TypeMismatchInCollection", new object[] { p0 });
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		internal static string WebUtil_TypeMismatchInNonPropertyCollection(object p0)
		{
			return TextRes.GetString("WebUtil_TypeMismatchInNonPropertyCollection", new object[] { p0 });
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		internal static string ClientTypeCache_NonEntityTypeCannotContainEntityProperties(object p0, object p1)
		{
			return TextRes.GetString("ClientTypeCache_NonEntityTypeCannotContainEntityProperties", new object[] { p0, p1 });
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0002D6EF File Offset: 0x0002B8EF
		internal static string DataServiceException_GeneralError
		{
			get
			{
				return TextRes.GetString("DataServiceException_GeneralError");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002D6FB File Offset: 0x0002B8FB
		internal static string Deserialize_GetEnumerator
		{
			get
			{
				return TextRes.GetString("Deserialize_GetEnumerator");
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002D708 File Offset: 0x0002B908
		internal static string Deserialize_Current(object p0, object p1)
		{
			return TextRes.GetString("Deserialize_Current", new object[] { p0, p1 });
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002D72F File Offset: 0x0002B92F
		internal static string Deserialize_MixedTextWithComment
		{
			get
			{
				return TextRes.GetString("Deserialize_MixedTextWithComment");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002D73B File Offset: 0x0002B93B
		internal static string Deserialize_ExpectingSimpleValue
		{
			get
			{
				return TextRes.GetString("Deserialize_ExpectingSimpleValue");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002D747 File Offset: 0x0002B947
		internal static string Deserialize_MismatchAtomLinkLocalSimple
		{
			get
			{
				return TextRes.GetString("Deserialize_MismatchAtomLinkLocalSimple");
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002D754 File Offset: 0x0002B954
		internal static string Deserialize_MismatchAtomLinkFeedPropertyNotCollection(object p0)
		{
			return TextRes.GetString("Deserialize_MismatchAtomLinkFeedPropertyNotCollection", new object[] { p0 });
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002D778 File Offset: 0x0002B978
		internal static string Deserialize_MismatchAtomLinkEntryPropertyIsCollection(object p0)
		{
			return TextRes.GetString("Deserialize_MismatchAtomLinkEntryPropertyIsCollection", new object[] { p0 });
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002D79B File Offset: 0x0002B99B
		internal static string Deserialize_NoLocationHeader
		{
			get
			{
				return TextRes.GetString("Deserialize_NoLocationHeader");
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002D7A8 File Offset: 0x0002B9A8
		internal static string Deserialize_ServerException(object p0)
		{
			return TextRes.GetString("Deserialize_ServerException", new object[] { p0 });
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002D7CB File Offset: 0x0002B9CB
		internal static string Deserialize_MissingIdElement
		{
			get
			{
				return TextRes.GetString("Deserialize_MissingIdElement");
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
		internal static string Collection_NullCollectionNotSupported(object p0)
		{
			return TextRes.GetString("Collection_NullCollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		internal static string Collection_NullNonPropertyCollectionNotSupported(object p0)
		{
			return TextRes.GetString("Collection_NullNonPropertyCollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002D81F File Offset: 0x0002BA1F
		internal static string Collection_NullCollectionItemsNotSupported
		{
			get
			{
				return TextRes.GetString("Collection_NullCollectionItemsNotSupported");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002D82B File Offset: 0x0002BA2B
		internal static string Collection_ComplexTypesInCollectionOfPrimitiveTypesNotAllowed
		{
			get
			{
				return TextRes.GetString("Collection_ComplexTypesInCollectionOfPrimitiveTypesNotAllowed");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0002D837 File Offset: 0x0002BA37
		internal static string Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed
		{
			get
			{
				return TextRes.GetString("Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed");
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002D844 File Offset: 0x0002BA44
		internal static string EntityDescriptor_MissingSelfEditLink(object p0)
		{
			return TextRes.GetString("EntityDescriptor_MissingSelfEditLink", new object[] { p0 });
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002D868 File Offset: 0x0002BA68
		internal static string EpmSourceTree_InvalidSourcePath(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_InvalidSourcePath", new object[] { p0, p1 });
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002D890 File Offset: 0x0002BA90
		internal static string EpmSourceTree_DuplicateEpmAttrsWithSameSourceName(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_DuplicateEpmAttrsWithSameSourceName", new object[] { p0, p1 });
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002D8B8 File Offset: 0x0002BAB8
		internal static string EpmSourceTree_EndsWithNonPrimitiveType(object p0)
		{
			return TextRes.GetString("EpmSourceTree_EndsWithNonPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002D8DC File Offset: 0x0002BADC
		internal static string EpmSourceTree_InaccessiblePropertyOnType(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_InaccessiblePropertyOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002D904 File Offset: 0x0002BB04
		internal static string EpmSourceTree_NamedStreamCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_NamedStreamCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002D92C File Offset: 0x0002BB2C
		internal static string EpmSourceTree_SpatialTypeCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_SpatialTypeCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002D954 File Offset: 0x0002BB54
		internal static string EpmSourceTree_CollectionPropertyCannotBeMapped(object p0, object p1)
		{
			return TextRes.GetString("EpmSourceTree_CollectionPropertyCannotBeMapped", new object[] { p0, p1 });
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002D97C File Offset: 0x0002BB7C
		internal static string EpmTargetTree_InvalidTargetPath(object p0)
		{
			return TextRes.GetString("EpmTargetTree_InvalidTargetPath", new object[] { p0 });
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		internal static string EpmTargetTree_AttributeInMiddle(object p0)
		{
			return TextRes.GetString("EpmTargetTree_AttributeInMiddle", new object[] { p0 });
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		internal static string EpmTargetTree_DuplicateEpmAttrsWithSameTargetName(object p0, object p1, object p2, object p3)
		{
			return TextRes.GetString("EpmTargetTree_DuplicateEpmAttrsWithSameTargetName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002D9F3 File Offset: 0x0002BBF3
		internal static string HttpProcessUtility_ContentTypeMissing
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_ContentTypeMissing");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002D9FF File Offset: 0x0002BBFF
		internal static string HttpProcessUtility_MediaTypeMissingValue
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeMissingValue");
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002DA0B File Offset: 0x0002BC0B
		internal static string HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter");
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002DA17 File Offset: 0x0002BC17
		internal static string HttpProcessUtility_MediaTypeRequiresSlash
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSlash");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002DA23 File Offset: 0x0002BC23
		internal static string HttpProcessUtility_MediaTypeRequiresSubType
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeRequiresSubType");
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002DA2F File Offset: 0x0002BC2F
		internal static string HttpProcessUtility_MediaTypeUnspecified
		{
			get
			{
				return TextRes.GetString("HttpProcessUtility_MediaTypeUnspecified");
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002DA3C File Offset: 0x0002BC3C
		internal static string HttpProcessUtility_EncodingNotSupported(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EncodingNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002DA60 File Offset: 0x0002BC60
		internal static string HttpProcessUtility_EscapeCharWithoutQuotes(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EscapeCharWithoutQuotes", new object[] { p0 });
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002DA84 File Offset: 0x0002BC84
		internal static string HttpProcessUtility_EscapeCharAtEnd(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_EscapeCharAtEnd", new object[] { p0 });
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002DAA8 File Offset: 0x0002BCA8
		internal static string HttpProcessUtility_ClosingQuoteNotFound(object p0)
		{
			return TextRes.GetString("HttpProcessUtility_ClosingQuoteNotFound", new object[] { p0 });
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002DACB File Offset: 0x0002BCCB
		internal static string MaterializeFromAtom_CountNotPresent
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_CountNotPresent");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002DAD7 File Offset: 0x0002BCD7
		internal static string MaterializeFromAtom_TopLevelLinkNotAvailable
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_TopLevelLinkNotAvailable");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002DAE3 File Offset: 0x0002BCE3
		internal static string MaterializeFromAtom_CollectionKeyNotPresentInLinkTable
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_CollectionKeyNotPresentInLinkTable");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002DAEF File Offset: 0x0002BCEF
		internal static string MaterializeFromAtom_GetNestLinkForFlatCollection
		{
			get
			{
				return TextRes.GetString("MaterializeFromAtom_GetNestLinkForFlatCollection");
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002DAFB File Offset: 0x0002BCFB
		internal static string ODataRequestMessage_GetStreamMethodNotSupported
		{
			get
			{
				return TextRes.GetString("ODataRequestMessage_GetStreamMethodNotSupported");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002DB07 File Offset: 0x0002BD07
		internal static string Util_EmptyString
		{
			get
			{
				return TextRes.GetString("Util_EmptyString");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002DB13 File Offset: 0x0002BD13
		internal static string Util_EmptyArray
		{
			get
			{
				return TextRes.GetString("Util_EmptyArray");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002DB1F File Offset: 0x0002BD1F
		internal static string Util_NullArrayElement
		{
			get
			{
				return TextRes.GetString("Util_NullArrayElement");
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		internal static string ALinq_UnsupportedExpression(object p0)
		{
			return TextRes.GetString("ALinq_UnsupportedExpression", new object[] { p0 });
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002DB50 File Offset: 0x0002BD50
		internal static string ALinq_CouldNotConvert(object p0)
		{
			return TextRes.GetString("ALinq_CouldNotConvert", new object[] { p0 });
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002DB74 File Offset: 0x0002BD74
		internal static string ALinq_MethodNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_MethodNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002DB98 File Offset: 0x0002BD98
		internal static string ALinq_UnaryNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_UnaryNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002DBBC File Offset: 0x0002BDBC
		internal static string ALinq_BinaryNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_BinaryNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		internal static string ALinq_ConstantNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_ConstantNotSupported", new object[] { p0 });
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002DC03 File Offset: 0x0002BE03
		internal static string ALinq_TypeBinaryNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_TypeBinaryNotSupported");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002DC0F File Offset: 0x0002BE0F
		internal static string ALinq_ConditionalNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ConditionalNotSupported");
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002DC1B File Offset: 0x0002BE1B
		internal static string ALinq_ParameterNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ParameterNotSupported");
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002DC28 File Offset: 0x0002BE28
		internal static string ALinq_MemberAccessNotSupported(object p0)
		{
			return TextRes.GetString("ALinq_MemberAccessNotSupported", new object[] { p0 });
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002DC4B File Offset: 0x0002BE4B
		internal static string ALinq_LambdaNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_LambdaNotSupported");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002DC57 File Offset: 0x0002BE57
		internal static string ALinq_NewNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_NewNotSupported");
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002DC63 File Offset: 0x0002BE63
		internal static string ALinq_MemberInitNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_MemberInitNotSupported");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002DC6F File Offset: 0x0002BE6F
		internal static string ALinq_ListInitNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_ListInitNotSupported");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002DC7B File Offset: 0x0002BE7B
		internal static string ALinq_NewArrayNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_NewArrayNotSupported");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002DC87 File Offset: 0x0002BE87
		internal static string ALinq_InvocationNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_InvocationNotSupported");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002DC93 File Offset: 0x0002BE93
		internal static string ALinq_QueryOptionsOnlyAllowedOnLeafNodes
		{
			get
			{
				return TextRes.GetString("ALinq_QueryOptionsOnlyAllowedOnLeafNodes");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002DC9F File Offset: 0x0002BE9F
		internal static string ALinq_CantExpand
		{
			get
			{
				return TextRes.GetString("ALinq_CantExpand");
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		internal static string ALinq_CantCastToUnsupportedPrimitive(object p0)
		{
			return TextRes.GetString("ALinq_CantCastToUnsupportedPrimitive", new object[] { p0 });
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002DCCF File Offset: 0x0002BECF
		internal static string ALinq_CantNavigateWithoutKeyPredicate
		{
			get
			{
				return TextRes.GetString("ALinq_CantNavigateWithoutKeyPredicate");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002DCDB File Offset: 0x0002BEDB
		internal static string ALinq_CanOnlyApplyOneKeyPredicate
		{
			get
			{
				return TextRes.GetString("ALinq_CanOnlyApplyOneKeyPredicate");
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		internal static string ALinq_CantTranslateExpression(object p0)
		{
			return TextRes.GetString("ALinq_CantTranslateExpression", new object[] { p0 });
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002DD0C File Offset: 0x0002BF0C
		internal static string ALinq_TranslationError(object p0)
		{
			return TextRes.GetString("ALinq_TranslationError", new object[] { p0 });
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002DD2F File Offset: 0x0002BF2F
		internal static string ALinq_CantAddQueryOption
		{
			get
			{
				return TextRes.GetString("ALinq_CantAddQueryOption");
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002DD3C File Offset: 0x0002BF3C
		internal static string ALinq_CantAddDuplicateQueryOption(object p0)
		{
			return TextRes.GetString("ALinq_CantAddDuplicateQueryOption", new object[] { p0 });
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002DD60 File Offset: 0x0002BF60
		internal static string ALinq_CantAddAstoriaQueryOption(object p0)
		{
			return TextRes.GetString("ALinq_CantAddAstoriaQueryOption", new object[] { p0 });
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002DD84 File Offset: 0x0002BF84
		internal static string ALinq_CantAddQueryOptionStartingWithDollarSign(object p0)
		{
			return TextRes.GetString("ALinq_CantAddQueryOptionStartingWithDollarSign", new object[] { p0 });
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
		internal static string ALinq_CantReferToPublicField(object p0)
		{
			return TextRes.GetString("ALinq_CantReferToPublicField", new object[] { p0 });
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002DDCB File Offset: 0x0002BFCB
		internal static string ALinq_QueryOptionsOnlyAllowedOnSingletons
		{
			get
			{
				return TextRes.GetString("ALinq_QueryOptionsOnlyAllowedOnSingletons");
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002DDD8 File Offset: 0x0002BFD8
		internal static string ALinq_QueryOptionOutOfOrder(object p0, object p1)
		{
			return TextRes.GetString("ALinq_QueryOptionOutOfOrder", new object[] { p0, p1 });
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002DDFF File Offset: 0x0002BFFF
		internal static string ALinq_CannotAddCountOption
		{
			get
			{
				return TextRes.GetString("ALinq_CannotAddCountOption");
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002DE0B File Offset: 0x0002C00B
		internal static string ALinq_CannotAddCountOptionConflict
		{
			get
			{
				return TextRes.GetString("ALinq_CannotAddCountOptionConflict");
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002DE17 File Offset: 0x0002C017
		internal static string ALinq_ProjectionOnlyAllowedOnLeafNodes
		{
			get
			{
				return TextRes.GetString("ALinq_ProjectionOnlyAllowedOnLeafNodes");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002DE23 File Offset: 0x0002C023
		internal static string ALinq_ProjectionCanOnlyHaveOneProjection
		{
			get
			{
				return TextRes.GetString("ALinq_ProjectionCanOnlyHaveOneProjection");
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002DE30 File Offset: 0x0002C030
		internal static string ALinq_ProjectionMemberAssignmentMismatch(object p0, object p1, object p2)
		{
			return TextRes.GetString("ALinq_ProjectionMemberAssignmentMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002DE5C File Offset: 0x0002C05C
		internal static string ALinq_InvalidExpressionInNavigationPath(object p0)
		{
			return TextRes.GetString("ALinq_InvalidExpressionInNavigationPath", new object[] { p0 });
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002DE80 File Offset: 0x0002C080
		internal static string ALinq_ExpressionNotSupportedInProjectionToEntity(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionNotSupportedInProjectionToEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		internal static string ALinq_ExpressionNotSupportedInProjection(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionNotSupportedInProjection", new object[] { p0, p1 });
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002DECF File Offset: 0x0002C0CF
		internal static string ALinq_CannotConstructKnownEntityTypes
		{
			get
			{
				return TextRes.GetString("ALinq_CannotConstructKnownEntityTypes");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002DEDB File Offset: 0x0002C0DB
		internal static string ALinq_CannotCreateConstantEntity
		{
			get
			{
				return TextRes.GetString("ALinq_CannotCreateConstantEntity");
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
		internal static string ALinq_PropertyNamesMustMatchInProjections(object p0, object p1)
		{
			return TextRes.GetString("ALinq_PropertyNamesMustMatchInProjections", new object[] { p0, p1 });
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002DF0F File Offset: 0x0002C10F
		internal static string ALinq_CanOnlyProjectTheLeaf
		{
			get
			{
				return TextRes.GetString("ALinq_CanOnlyProjectTheLeaf");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0002DF1B File Offset: 0x0002C11B
		internal static string ALinq_CannotProjectWithExplicitExpansion
		{
			get
			{
				return TextRes.GetString("ALinq_CannotProjectWithExplicitExpansion");
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002DF28 File Offset: 0x0002C128
		internal static string ALinq_CollectionPropertyNotSupportedInOrderBy(object p0)
		{
			return TextRes.GetString("ALinq_CollectionPropertyNotSupportedInOrderBy", new object[] { p0 });
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002DF4C File Offset: 0x0002C14C
		internal static string ALinq_CollectionPropertyNotSupportedInWhere(object p0)
		{
			return TextRes.GetString("ALinq_CollectionPropertyNotSupportedInWhere", new object[] { p0 });
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002DF70 File Offset: 0x0002C170
		internal static string ALinq_CollectionMemberAccessNotSupportedInNavigation(object p0)
		{
			return TextRes.GetString("ALinq_CollectionMemberAccessNotSupportedInNavigation", new object[] { p0 });
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002DF94 File Offset: 0x0002C194
		internal static string ALinq_LinkPropertyNotSupportedInExpression(object p0)
		{
			return TextRes.GetString("ALinq_LinkPropertyNotSupportedInExpression", new object[] { p0 });
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002DFB7 File Offset: 0x0002C1B7
		internal static string ALinq_OfTypeArgumentNotAvailable
		{
			get
			{
				return TextRes.GetString("ALinq_OfTypeArgumentNotAvailable");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002DFC3 File Offset: 0x0002C1C3
		internal static string ALinq_CannotUseTypeFiltersMultipleTimes
		{
			get
			{
				return TextRes.GetString("ALinq_CannotUseTypeFiltersMultipleTimes");
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
		internal static string ALinq_ExpressionCannotEndWithTypeAs(object p0, object p1)
		{
			return TextRes.GetString("ALinq_ExpressionCannotEndWithTypeAs", new object[] { p0, p1 });
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002DFF7 File Offset: 0x0002C1F7
		internal static string ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3
		{
			get
			{
				return TextRes.GetString("ALinq_TypeAsNotSupportedForMaxDataServiceVersionLessThan3");
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002E004 File Offset: 0x0002C204
		internal static string ALinq_MethodNotSupportedForMaxDataServiceVersionLessThanX(object p0, object p1)
		{
			return TextRes.GetString("ALinq_MethodNotSupportedForMaxDataServiceVersionLessThanX", new object[] { p0, p1 });
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002E02C File Offset: 0x0002C22C
		internal static string ALinq_TypeAsArgumentNotEntityType(object p0)
		{
			return TextRes.GetString("ALinq_TypeAsArgumentNotEntityType", new object[] { p0 });
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002E050 File Offset: 0x0002C250
		internal static string ALinq_InvalidSourceForAnyAll(object p0)
		{
			return TextRes.GetString("ALinq_InvalidSourceForAnyAll", new object[] { p0 });
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002E074 File Offset: 0x0002C274
		internal static string ALinq_AnyAllNotSupportedInOrderBy(object p0)
		{
			return TextRes.GetString("ALinq_AnyAllNotSupportedInOrderBy", new object[] { p0 });
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002E097 File Offset: 0x0002C297
		internal static string ALinq_FormatQueryOptionNotSupported
		{
			get
			{
				return TextRes.GetString("ALinq_FormatQueryOptionNotSupported");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002E0A3 File Offset: 0x0002C2A3
		internal static string DSKAttribute_MustSpecifyAtleastOnePropertyName
		{
			get
			{
				return TextRes.GetString("DSKAttribute_MustSpecifyAtleastOnePropertyName");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002E0AF File Offset: 0x0002C2AF
		internal static string DataServiceCollection_LoadRequiresTargetCollectionObserved
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadRequiresTargetCollectionObserved");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002E0BB File Offset: 0x0002C2BB
		internal static string DataServiceCollection_CannotStopTrackingChildCollection
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_CannotStopTrackingChildCollection");
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002E0C7 File Offset: 0x0002C2C7
		internal static string DataServiceCollection_OperationForTrackedOnly
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_OperationForTrackedOnly");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002E0D3 File Offset: 0x0002C2D3
		internal static string DataServiceCollection_CannotDetermineContextFromItems
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_CannotDetermineContextFromItems");
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002E0DF File Offset: 0x0002C2DF
		internal static string DataServiceCollection_InsertIntoTrackedButNotLoadedCollection
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_InsertIntoTrackedButNotLoadedCollection");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002E0EB File Offset: 0x0002C2EB
		internal static string DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002E0F7 File Offset: 0x0002C2F7
		internal static string DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0002E103 File Offset: 0x0002C303
		internal static string DataServiceCollection_LoadAsyncRequiresDataServiceQuery
		{
			get
			{
				return TextRes.GetString("DataServiceCollection_LoadAsyncRequiresDataServiceQuery");
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002E110 File Offset: 0x0002C310
		internal static string DataBinding_DataServiceCollectionArgumentMustHaveEntityType(object p0)
		{
			return TextRes.GetString("DataBinding_DataServiceCollectionArgumentMustHaveEntityType", new object[] { p0 });
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002E134 File Offset: 0x0002C334
		internal static string DataBinding_CollectionPropertySetterValueHasObserver(object p0, object p1)
		{
			return TextRes.GetString("DataBinding_CollectionPropertySetterValueHasObserver", new object[] { p0, p1 });
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002E15C File Offset: 0x0002C35C
		internal static string DataBinding_DataServiceCollectionChangedUnknownActionCollection(object p0)
		{
			return TextRes.GetString("DataBinding_DataServiceCollectionChangedUnknownActionCollection", new object[] { p0 });
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002E180 File Offset: 0x0002C380
		internal static string DataBinding_CollectionChangedUnknownActionCollection(object p0, object p1)
		{
			return TextRes.GetString("DataBinding_CollectionChangedUnknownActionCollection", new object[] { p0, p1 });
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002E1A7 File Offset: 0x0002C3A7
		internal static string DataBinding_BindingOperation_DetachedSource
		{
			get
			{
				return TextRes.GetString("DataBinding_BindingOperation_DetachedSource");
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002E1B4 File Offset: 0x0002C3B4
		internal static string DataBinding_BindingOperation_ArrayItemNull(object p0)
		{
			return TextRes.GetString("DataBinding_BindingOperation_ArrayItemNull", new object[] { p0 });
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		internal static string DataBinding_BindingOperation_ArrayItemNotEntity(object p0)
		{
			return TextRes.GetString("DataBinding_BindingOperation_ArrayItemNotEntity", new object[] { p0 });
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		internal static string DataBinding_Util_UnknownEntitySetName(object p0)
		{
			return TextRes.GetString("DataBinding_Util_UnknownEntitySetName", new object[] { p0 });
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002E220 File Offset: 0x0002C420
		internal static string DataBinding_EntityAlreadyInCollection(object p0)
		{
			return TextRes.GetString("DataBinding_EntityAlreadyInCollection", new object[] { p0 });
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002E244 File Offset: 0x0002C444
		internal static string DataBinding_NotifyPropertyChangedNotImpl(object p0)
		{
			return TextRes.GetString("DataBinding_NotifyPropertyChangedNotImpl", new object[] { p0 });
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002E268 File Offset: 0x0002C468
		internal static string DataBinding_NotifyCollectionChangedNotImpl(object p0)
		{
			return TextRes.GetString("DataBinding_NotifyCollectionChangedNotImpl", new object[] { p0 });
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002E28C File Offset: 0x0002C48C
		internal static string DataBinding_ComplexObjectAssociatedWithMultipleEntities(object p0)
		{
			return TextRes.GetString("DataBinding_ComplexObjectAssociatedWithMultipleEntities", new object[] { p0 });
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		internal static string DataBinding_CollectionAssociatedWithMultipleEntities(object p0)
		{
			return TextRes.GetString("DataBinding_CollectionAssociatedWithMultipleEntities", new object[] { p0 });
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002E2D3 File Offset: 0x0002C4D3
		internal static string AtomParser_SingleEntry_NoneFound
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_NoneFound");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002E2DF File Offset: 0x0002C4DF
		internal static string AtomParser_SingleEntry_MultipleFound
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_MultipleFound");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002E2EB File Offset: 0x0002C4EB
		internal static string AtomParser_SingleEntry_ExpectedFeedOrEntry
		{
			get
			{
				return TextRes.GetString("AtomParser_SingleEntry_ExpectedFeedOrEntry");
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002E2F8 File Offset: 0x0002C4F8
		internal static string AtomMaterializer_CannotAssignNull(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_CannotAssignNull", new object[] { p0, p1 });
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002E320 File Offset: 0x0002C520
		internal static string AtomMaterializer_EntryIntoCollectionMismatch(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_EntryIntoCollectionMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002E348 File Offset: 0x0002C548
		internal static string AtomMaterializer_EntryToAccessIsNull(object p0)
		{
			return TextRes.GetString("AtomMaterializer_EntryToAccessIsNull", new object[] { p0 });
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002E36C File Offset: 0x0002C56C
		internal static string AtomMaterializer_EntryToInitializeIsNull(object p0)
		{
			return TextRes.GetString("AtomMaterializer_EntryToInitializeIsNull", new object[] { p0 });
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002E390 File Offset: 0x0002C590
		internal static string AtomMaterializer_ProjectEntityTypeMismatch(object p0, object p1, object p2)
		{
			return TextRes.GetString("AtomMaterializer_ProjectEntityTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		internal static string AtomMaterializer_PropertyMissing(object p0)
		{
			return TextRes.GetString("AtomMaterializer_PropertyMissing", new object[] { p0 });
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		internal static string AtomMaterializer_PropertyNotExpectedEntry(object p0)
		{
			return TextRes.GetString("AtomMaterializer_PropertyNotExpectedEntry", new object[] { p0 });
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002E403 File Offset: 0x0002C603
		internal static string AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities
		{
			get
			{
				return TextRes.GetString("AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities");
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002E410 File Offset: 0x0002C610
		internal static string AtomMaterializer_NoParameterlessCtorForCollectionProperty(object p0, object p1)
		{
			return TextRes.GetString("AtomMaterializer_NoParameterlessCtorForCollectionProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002E438 File Offset: 0x0002C638
		internal static string AtomMaterializer_InvalidCollectionItem(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidCollectionItem", new object[] { p0 });
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002E45C File Offset: 0x0002C65C
		internal static string AtomMaterializer_InvalidEntityType(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidEntityType", new object[] { p0 });
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002E480 File Offset: 0x0002C680
		internal static string AtomMaterializer_InvalidNonEntityType(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidNonEntityType", new object[] { p0 });
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002E4A4 File Offset: 0x0002C6A4
		internal static string AtomMaterializer_CollectionExpectedCollection(object p0)
		{
			return TextRes.GetString("AtomMaterializer_CollectionExpectedCollection", new object[] { p0 });
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		internal static string AtomMaterializer_InvalidResponsePayload(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidResponsePayload", new object[] { p0 });
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		internal static string AtomMaterializer_InvalidContentTypeEncountered(object p0)
		{
			return TextRes.GetString("AtomMaterializer_InvalidContentTypeEncountered", new object[] { p0 });
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002E510 File Offset: 0x0002C710
		internal static string AtomMaterializer_MaterializationTypeError(object p0)
		{
			return TextRes.GetString("AtomMaterializer_MaterializationTypeError", new object[] { p0 });
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002E533 File Offset: 0x0002C733
		internal static string AtomMaterializer_ResetAfterEnumeratorCreationError
		{
			get
			{
				return TextRes.GetString("AtomMaterializer_ResetAfterEnumeratorCreationError");
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002E540 File Offset: 0x0002C740
		internal static string AtomMaterializer_TypeShouldBeCollectionError(object p0)
		{
			return TextRes.GetString("AtomMaterializer_TypeShouldBeCollectionError", new object[] { p0 });
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002E564 File Offset: 0x0002C764
		internal static string Serializer_LoopsNotAllowedInComplexTypes(object p0)
		{
			return TextRes.GetString("Serializer_LoopsNotAllowedInComplexTypes", new object[] { p0 });
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002E588 File Offset: 0x0002C788
		internal static string Serializer_LoopsNotAllowedInNonPropertyComplexTypes(object p0)
		{
			return TextRes.GetString("Serializer_LoopsNotAllowedInNonPropertyComplexTypes", new object[] { p0 });
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002E5AC File Offset: 0x0002C7AC
		internal static string Serializer_InvalidCollectionParamterItemType(object p0, object p1)
		{
			return TextRes.GetString("Serializer_InvalidCollectionParamterItemType", new object[] { p0, p1 });
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
		internal static string Serializer_NullCollectionParamterItemValue(object p0)
		{
			return TextRes.GetString("Serializer_NullCollectionParamterItemValue", new object[] { p0 });
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
		internal static string Serializer_InvalidParameterType(object p0, object p1)
		{
			return TextRes.GetString("Serializer_InvalidParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002E620 File Offset: 0x0002C820
		internal static string Serializer_UriDoesNotContainParameterAlias(object p0)
		{
			return TextRes.GetString("Serializer_UriDoesNotContainParameterAlias", new object[] { p0 });
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002E643 File Offset: 0x0002C843
		internal static string DataServiceQuery_EnumerationNotSupported
		{
			get
			{
				return TextRes.GetString("DataServiceQuery_EnumerationNotSupported");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002E64F File Offset: 0x0002C84F
		internal static string Context_SendingRequestEventArgsNotHttp
		{
			get
			{
				return TextRes.GetString("Context_SendingRequestEventArgsNotHttp");
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002E65C File Offset: 0x0002C85C
		internal static string General_InternalError(object p0)
		{
			return TextRes.GetString("General_InternalError", new object[] { p0 });
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002E680 File Offset: 0x0002C880
		internal static string ODataMetadataBuilder_MissingEntitySetUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntitySetUri", new object[] { p0 });
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
		internal static string ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix(object p0, object p1)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingSegmentForEntitySetUriSuffix", new object[] { p0, p1 });
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002E6CC File Offset: 0x0002C8CC
		internal static string ODataMetadataBuilder_MissingEntityInstanceUri(object p0)
		{
			return TextRes.GetString("ODataMetadataBuilder_MissingEntityInstanceUri", new object[] { p0 });
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		internal static string EdmValueUtils_UnsupportedPrimitiveType(object p0)
		{
			return TextRes.GetString("EdmValueUtils_UnsupportedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002E714 File Offset: 0x0002C914
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKind(object p0, object p1, object p2)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKind", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002E740 File Offset: 0x0002C940
		internal static string EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(object p0, object p1)
		{
			return TextRes.GetString("EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002E768 File Offset: 0x0002C968
		internal static string EdmValueUtils_CannotConvertTypeToClrValue(object p0)
		{
			return TextRes.GetString("EdmValueUtils_CannotConvertTypeToClrValue", new object[] { p0 });
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0002E78B File Offset: 0x0002C98B
		internal static string DataServiceRequest_FailGetCount
		{
			get
			{
				return TextRes.GetString("DataServiceRequest_FailGetCount");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002E797 File Offset: 0x0002C997
		internal static string Context_ExecuteExpectedVoidResponse
		{
			get
			{
				return TextRes.GetString("Context_ExecuteExpectedVoidResponse");
			}
		}
	}
}
