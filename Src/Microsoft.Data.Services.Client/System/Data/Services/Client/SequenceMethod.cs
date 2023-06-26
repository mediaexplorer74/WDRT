using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000BE RID: 190
	internal enum SequenceMethod
	{
		// Token: 0x0400034A RID: 842
		Where,
		// Token: 0x0400034B RID: 843
		WhereOrdinal,
		// Token: 0x0400034C RID: 844
		OfType,
		// Token: 0x0400034D RID: 845
		Cast,
		// Token: 0x0400034E RID: 846
		Select,
		// Token: 0x0400034F RID: 847
		SelectOrdinal,
		// Token: 0x04000350 RID: 848
		SelectMany,
		// Token: 0x04000351 RID: 849
		SelectManyOrdinal,
		// Token: 0x04000352 RID: 850
		SelectManyResultSelector,
		// Token: 0x04000353 RID: 851
		SelectManyOrdinalResultSelector,
		// Token: 0x04000354 RID: 852
		Join,
		// Token: 0x04000355 RID: 853
		JoinComparer,
		// Token: 0x04000356 RID: 854
		GroupJoin,
		// Token: 0x04000357 RID: 855
		GroupJoinComparer,
		// Token: 0x04000358 RID: 856
		OrderBy,
		// Token: 0x04000359 RID: 857
		OrderByComparer,
		// Token: 0x0400035A RID: 858
		OrderByDescending,
		// Token: 0x0400035B RID: 859
		OrderByDescendingComparer,
		// Token: 0x0400035C RID: 860
		ThenBy,
		// Token: 0x0400035D RID: 861
		ThenByComparer,
		// Token: 0x0400035E RID: 862
		ThenByDescending,
		// Token: 0x0400035F RID: 863
		ThenByDescendingComparer,
		// Token: 0x04000360 RID: 864
		Take,
		// Token: 0x04000361 RID: 865
		TakeWhile,
		// Token: 0x04000362 RID: 866
		TakeWhileOrdinal,
		// Token: 0x04000363 RID: 867
		Skip,
		// Token: 0x04000364 RID: 868
		SkipWhile,
		// Token: 0x04000365 RID: 869
		SkipWhileOrdinal,
		// Token: 0x04000366 RID: 870
		GroupBy,
		// Token: 0x04000367 RID: 871
		GroupByComparer,
		// Token: 0x04000368 RID: 872
		GroupByElementSelector,
		// Token: 0x04000369 RID: 873
		GroupByElementSelectorComparer,
		// Token: 0x0400036A RID: 874
		GroupByResultSelector,
		// Token: 0x0400036B RID: 875
		GroupByResultSelectorComparer,
		// Token: 0x0400036C RID: 876
		GroupByElementSelectorResultSelector,
		// Token: 0x0400036D RID: 877
		GroupByElementSelectorResultSelectorComparer,
		// Token: 0x0400036E RID: 878
		Distinct,
		// Token: 0x0400036F RID: 879
		DistinctComparer,
		// Token: 0x04000370 RID: 880
		Concat,
		// Token: 0x04000371 RID: 881
		Union,
		// Token: 0x04000372 RID: 882
		UnionComparer,
		// Token: 0x04000373 RID: 883
		Intersect,
		// Token: 0x04000374 RID: 884
		IntersectComparer,
		// Token: 0x04000375 RID: 885
		Except,
		// Token: 0x04000376 RID: 886
		ExceptComparer,
		// Token: 0x04000377 RID: 887
		First,
		// Token: 0x04000378 RID: 888
		FirstPredicate,
		// Token: 0x04000379 RID: 889
		FirstOrDefault,
		// Token: 0x0400037A RID: 890
		FirstOrDefaultPredicate,
		// Token: 0x0400037B RID: 891
		Last,
		// Token: 0x0400037C RID: 892
		LastPredicate,
		// Token: 0x0400037D RID: 893
		LastOrDefault,
		// Token: 0x0400037E RID: 894
		LastOrDefaultPredicate,
		// Token: 0x0400037F RID: 895
		Single,
		// Token: 0x04000380 RID: 896
		SinglePredicate,
		// Token: 0x04000381 RID: 897
		SingleOrDefault,
		// Token: 0x04000382 RID: 898
		SingleOrDefaultPredicate,
		// Token: 0x04000383 RID: 899
		ElementAt,
		// Token: 0x04000384 RID: 900
		ElementAtOrDefault,
		// Token: 0x04000385 RID: 901
		DefaultIfEmpty,
		// Token: 0x04000386 RID: 902
		DefaultIfEmptyValue,
		// Token: 0x04000387 RID: 903
		Contains,
		// Token: 0x04000388 RID: 904
		ContainsComparer,
		// Token: 0x04000389 RID: 905
		Reverse,
		// Token: 0x0400038A RID: 906
		Empty,
		// Token: 0x0400038B RID: 907
		SequenceEqual,
		// Token: 0x0400038C RID: 908
		SequenceEqualComparer,
		// Token: 0x0400038D RID: 909
		Any,
		// Token: 0x0400038E RID: 910
		AnyPredicate,
		// Token: 0x0400038F RID: 911
		All,
		// Token: 0x04000390 RID: 912
		Count,
		// Token: 0x04000391 RID: 913
		CountPredicate,
		// Token: 0x04000392 RID: 914
		LongCount,
		// Token: 0x04000393 RID: 915
		LongCountPredicate,
		// Token: 0x04000394 RID: 916
		Min,
		// Token: 0x04000395 RID: 917
		MinSelector,
		// Token: 0x04000396 RID: 918
		Max,
		// Token: 0x04000397 RID: 919
		MaxSelector,
		// Token: 0x04000398 RID: 920
		MinInt,
		// Token: 0x04000399 RID: 921
		MinNullableInt,
		// Token: 0x0400039A RID: 922
		MinLong,
		// Token: 0x0400039B RID: 923
		MinNullableLong,
		// Token: 0x0400039C RID: 924
		MinDouble,
		// Token: 0x0400039D RID: 925
		MinNullableDouble,
		// Token: 0x0400039E RID: 926
		MinDecimal,
		// Token: 0x0400039F RID: 927
		MinNullableDecimal,
		// Token: 0x040003A0 RID: 928
		MinSingle,
		// Token: 0x040003A1 RID: 929
		MinNullableSingle,
		// Token: 0x040003A2 RID: 930
		MinIntSelector,
		// Token: 0x040003A3 RID: 931
		MinNullableIntSelector,
		// Token: 0x040003A4 RID: 932
		MinLongSelector,
		// Token: 0x040003A5 RID: 933
		MinNullableLongSelector,
		// Token: 0x040003A6 RID: 934
		MinDoubleSelector,
		// Token: 0x040003A7 RID: 935
		MinNullableDoubleSelector,
		// Token: 0x040003A8 RID: 936
		MinDecimalSelector,
		// Token: 0x040003A9 RID: 937
		MinNullableDecimalSelector,
		// Token: 0x040003AA RID: 938
		MinSingleSelector,
		// Token: 0x040003AB RID: 939
		MinNullableSingleSelector,
		// Token: 0x040003AC RID: 940
		MaxInt,
		// Token: 0x040003AD RID: 941
		MaxNullableInt,
		// Token: 0x040003AE RID: 942
		MaxLong,
		// Token: 0x040003AF RID: 943
		MaxNullableLong,
		// Token: 0x040003B0 RID: 944
		MaxDouble,
		// Token: 0x040003B1 RID: 945
		MaxNullableDouble,
		// Token: 0x040003B2 RID: 946
		MaxDecimal,
		// Token: 0x040003B3 RID: 947
		MaxNullableDecimal,
		// Token: 0x040003B4 RID: 948
		MaxSingle,
		// Token: 0x040003B5 RID: 949
		MaxNullableSingle,
		// Token: 0x040003B6 RID: 950
		MaxIntSelector,
		// Token: 0x040003B7 RID: 951
		MaxNullableIntSelector,
		// Token: 0x040003B8 RID: 952
		MaxLongSelector,
		// Token: 0x040003B9 RID: 953
		MaxNullableLongSelector,
		// Token: 0x040003BA RID: 954
		MaxDoubleSelector,
		// Token: 0x040003BB RID: 955
		MaxNullableDoubleSelector,
		// Token: 0x040003BC RID: 956
		MaxDecimalSelector,
		// Token: 0x040003BD RID: 957
		MaxNullableDecimalSelector,
		// Token: 0x040003BE RID: 958
		MaxSingleSelector,
		// Token: 0x040003BF RID: 959
		MaxNullableSingleSelector,
		// Token: 0x040003C0 RID: 960
		SumInt,
		// Token: 0x040003C1 RID: 961
		SumNullableInt,
		// Token: 0x040003C2 RID: 962
		SumLong,
		// Token: 0x040003C3 RID: 963
		SumNullableLong,
		// Token: 0x040003C4 RID: 964
		SumDouble,
		// Token: 0x040003C5 RID: 965
		SumNullableDouble,
		// Token: 0x040003C6 RID: 966
		SumDecimal,
		// Token: 0x040003C7 RID: 967
		SumNullableDecimal,
		// Token: 0x040003C8 RID: 968
		SumSingle,
		// Token: 0x040003C9 RID: 969
		SumNullableSingle,
		// Token: 0x040003CA RID: 970
		SumIntSelector,
		// Token: 0x040003CB RID: 971
		SumNullableIntSelector,
		// Token: 0x040003CC RID: 972
		SumLongSelector,
		// Token: 0x040003CD RID: 973
		SumNullableLongSelector,
		// Token: 0x040003CE RID: 974
		SumDoubleSelector,
		// Token: 0x040003CF RID: 975
		SumNullableDoubleSelector,
		// Token: 0x040003D0 RID: 976
		SumDecimalSelector,
		// Token: 0x040003D1 RID: 977
		SumNullableDecimalSelector,
		// Token: 0x040003D2 RID: 978
		SumSingleSelector,
		// Token: 0x040003D3 RID: 979
		SumNullableSingleSelector,
		// Token: 0x040003D4 RID: 980
		AverageInt,
		// Token: 0x040003D5 RID: 981
		AverageNullableInt,
		// Token: 0x040003D6 RID: 982
		AverageLong,
		// Token: 0x040003D7 RID: 983
		AverageNullableLong,
		// Token: 0x040003D8 RID: 984
		AverageDouble,
		// Token: 0x040003D9 RID: 985
		AverageNullableDouble,
		// Token: 0x040003DA RID: 986
		AverageDecimal,
		// Token: 0x040003DB RID: 987
		AverageNullableDecimal,
		// Token: 0x040003DC RID: 988
		AverageSingle,
		// Token: 0x040003DD RID: 989
		AverageNullableSingle,
		// Token: 0x040003DE RID: 990
		AverageIntSelector,
		// Token: 0x040003DF RID: 991
		AverageNullableIntSelector,
		// Token: 0x040003E0 RID: 992
		AverageLongSelector,
		// Token: 0x040003E1 RID: 993
		AverageNullableLongSelector,
		// Token: 0x040003E2 RID: 994
		AverageDoubleSelector,
		// Token: 0x040003E3 RID: 995
		AverageNullableDoubleSelector,
		// Token: 0x040003E4 RID: 996
		AverageDecimalSelector,
		// Token: 0x040003E5 RID: 997
		AverageNullableDecimalSelector,
		// Token: 0x040003E6 RID: 998
		AverageSingleSelector,
		// Token: 0x040003E7 RID: 999
		AverageNullableSingleSelector,
		// Token: 0x040003E8 RID: 1000
		Aggregate,
		// Token: 0x040003E9 RID: 1001
		AggregateSeed,
		// Token: 0x040003EA RID: 1002
		AggregateSeedSelector,
		// Token: 0x040003EB RID: 1003
		AsQueryable,
		// Token: 0x040003EC RID: 1004
		AsQueryableGeneric,
		// Token: 0x040003ED RID: 1005
		AsEnumerable,
		// Token: 0x040003EE RID: 1006
		ToList,
		// Token: 0x040003EF RID: 1007
		NotSupported
	}
}
