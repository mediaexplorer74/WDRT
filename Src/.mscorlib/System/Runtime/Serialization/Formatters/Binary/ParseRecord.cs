﻿using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000793 RID: 1939
	internal sealed class ParseRecord
	{
		// Token: 0x06005450 RID: 21584 RVA: 0x0012A3B4 File Offset: 0x001285B4
		internal ParseRecord()
		{
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0012A3BC File Offset: 0x001285BC
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x0400262B RID: 9771
		internal static int parseRecordIdCount = 1;

		// Token: 0x0400262C RID: 9772
		internal int PRparseRecordId;

		// Token: 0x0400262D RID: 9773
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x0400262E RID: 9774
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x0400262F RID: 9775
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x04002630 RID: 9776
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04002631 RID: 9777
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04002632 RID: 9778
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04002633 RID: 9779
		internal string PRname;

		// Token: 0x04002634 RID: 9780
		internal string PRvalue;

		// Token: 0x04002635 RID: 9781
		internal object PRvarValue;

		// Token: 0x04002636 RID: 9782
		internal string PRkeyDt;

		// Token: 0x04002637 RID: 9783
		internal Type PRdtType;

		// Token: 0x04002638 RID: 9784
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x04002639 RID: 9785
		internal bool PRisVariant;

		// Token: 0x0400263A RID: 9786
		internal bool PRisEnum;

		// Token: 0x0400263B RID: 9787
		internal long PRobjectId;

		// Token: 0x0400263C RID: 9788
		internal long PRidRef;

		// Token: 0x0400263D RID: 9789
		internal string PRarrayElementTypeString;

		// Token: 0x0400263E RID: 9790
		internal Type PRarrayElementType;

		// Token: 0x0400263F RID: 9791
		internal bool PRisArrayVariant;

		// Token: 0x04002640 RID: 9792
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x04002641 RID: 9793
		internal int PRrank;

		// Token: 0x04002642 RID: 9794
		internal int[] PRlengthA;

		// Token: 0x04002643 RID: 9795
		internal int[] PRpositionA;

		// Token: 0x04002644 RID: 9796
		internal int[] PRlowerBoundA;

		// Token: 0x04002645 RID: 9797
		internal int[] PRupperBoundA;

		// Token: 0x04002646 RID: 9798
		internal int[] PRindexMap;

		// Token: 0x04002647 RID: 9799
		internal int PRmemberIndex;

		// Token: 0x04002648 RID: 9800
		internal int PRlinearlength;

		// Token: 0x04002649 RID: 9801
		internal int[] PRrectangularMap;

		// Token: 0x0400264A RID: 9802
		internal bool PRisLowerBound;

		// Token: 0x0400264B RID: 9803
		internal long PRtopId;

		// Token: 0x0400264C RID: 9804
		internal long PRheaderId;

		// Token: 0x0400264D RID: 9805
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x0400264E RID: 9806
		internal bool PRisValueTypeFixup;

		// Token: 0x0400264F RID: 9807
		internal object PRnewObj;

		// Token: 0x04002650 RID: 9808
		internal object[] PRobjectA;

		// Token: 0x04002651 RID: 9809
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x04002652 RID: 9810
		internal bool PRisRegistered;

		// Token: 0x04002653 RID: 9811
		internal object[] PRmemberData;

		// Token: 0x04002654 RID: 9812
		internal SerializationInfo PRsi;

		// Token: 0x04002655 RID: 9813
		internal int PRnullCount;
	}
}
