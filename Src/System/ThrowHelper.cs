using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000039 RID: 57
	internal static class ThrowHelper
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00010D4F File Offset: 0x0000EF4F
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(SR.GetString("Arg_WrongType", new object[] { key, targetType }), "key");
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010D73 File Offset: 0x0000EF73
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(SR.GetString("Arg_WrongType", new object[] { value, targetType }), "value");
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010D97 File Offset: 0x0000EF97
		internal static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010D9E File Offset: 0x0000EF9E
		internal static void ThrowArgumentException(System.ExceptionResource resource)
		{
			throw new ArgumentException(SR.GetString(System.ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		internal static void ThrowArgumentNullException(System.ExceptionArgument argument)
		{
			throw new ArgumentNullException(System.ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010DBD File Offset: 0x0000EFBD
		internal static void ThrowArgumentOutOfRangeException(System.ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(System.ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010DCA File Offset: 0x0000EFCA
		internal static void ThrowArgumentOutOfRangeException(System.ExceptionArgument argument, System.ExceptionResource resource)
		{
			throw new ArgumentOutOfRangeException(System.ThrowHelper.GetArgumentName(argument), SR.GetString(System.ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010DE2 File Offset: 0x0000EFE2
		internal static void ThrowInvalidOperationException(System.ExceptionResource resource)
		{
			throw new InvalidOperationException(SR.GetString(System.ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		internal static void ThrowSerializationException(System.ExceptionResource resource)
		{
			throw new SerializationException(SR.GetString(System.ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010E06 File Offset: 0x0000F006
		internal static void ThrowNotSupportedException(System.ExceptionResource resource)
		{
			throw new NotSupportedException(SR.GetString(System.ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010E18 File Offset: 0x0000F018
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, System.ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				System.ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010E40 File Offset: 0x0000F040
		internal static string GetArgumentName(System.ExceptionArgument argument)
		{
			string text;
			switch (argument)
			{
			case System.ExceptionArgument.obj:
				text = "obj";
				break;
			case System.ExceptionArgument.dictionary:
				text = "dictionary";
				break;
			case System.ExceptionArgument.array:
				text = "array";
				break;
			case System.ExceptionArgument.info:
				text = "info";
				break;
			case System.ExceptionArgument.key:
				text = "key";
				break;
			case System.ExceptionArgument.collection:
				text = "collection";
				break;
			case System.ExceptionArgument.match:
				text = "match";
				break;
			case System.ExceptionArgument.converter:
				text = "converter";
				break;
			case System.ExceptionArgument.queue:
				text = "queue";
				break;
			case System.ExceptionArgument.stack:
				text = "stack";
				break;
			case System.ExceptionArgument.capacity:
				text = "capacity";
				break;
			case System.ExceptionArgument.index:
				text = "index";
				break;
			case System.ExceptionArgument.startIndex:
				text = "startIndex";
				break;
			case System.ExceptionArgument.value:
				text = "value";
				break;
			case System.ExceptionArgument.count:
				text = "count";
				break;
			case System.ExceptionArgument.arrayIndex:
				text = "arrayIndex";
				break;
			case System.ExceptionArgument.item:
				text = "item";
				break;
			default:
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00010F30 File Offset: 0x0000F130
		internal static string GetResourceName(System.ExceptionResource resource)
		{
			switch (resource)
			{
			case System.ExceptionResource.Argument_ImplementIComparable:
				return "Argument_ImplementIComparable";
			case System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				return "ArgumentOutOfRange_NeedNonNegNum";
			case System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired:
				return "ArgumentOutOfRange_NeedNonNegNumRequired";
			case System.ExceptionResource.Arg_ArrayPlusOffTooSmall:
				return "Arg_ArrayPlusOffTooSmall";
			case System.ExceptionResource.Argument_AddingDuplicate:
				return "Argument_AddingDuplicate";
			case System.ExceptionResource.Serialization_InvalidOnDeser:
				return "Serialization_InvalidOnDeser";
			case System.ExceptionResource.Serialization_MismatchedCount:
				return "Serialization_MismatchedCount";
			case System.ExceptionResource.Serialization_MissingValues:
				return "Serialization_MissingValues";
			case System.ExceptionResource.Arg_RankMultiDimNotSupported:
				return "Arg_MultiRank";
			case System.ExceptionResource.Arg_NonZeroLowerBound:
				return "Arg_NonZeroLowerBound";
			case System.ExceptionResource.Argument_InvalidArrayType:
				return "Invalid_Array_Type";
			case System.ExceptionResource.NotSupported_KeyCollectionSet:
				return "NotSupported_KeyCollectionSet";
			case System.ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				return "ArgumentOutOfRange_SmallCapacity";
			case System.ExceptionResource.ArgumentOutOfRange_Index:
				return "ArgumentOutOfRange_Index";
			case System.ExceptionResource.Argument_InvalidOffLen:
				return "Argument_InvalidOffLen";
			case System.ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				return "InvalidOperation_CannotRemoveFromStackOrQueue";
			case System.ExceptionResource.InvalidOperation_EmptyCollection:
				return "InvalidOperation_EmptyCollection";
			case System.ExceptionResource.InvalidOperation_EmptyQueue:
				return "InvalidOperation_EmptyQueue";
			case System.ExceptionResource.InvalidOperation_EnumOpCantHappen:
				return "InvalidOperation_EnumOpCantHappen";
			case System.ExceptionResource.InvalidOperation_EnumFailedVersion:
				return "InvalidOperation_EnumFailedVersion";
			case System.ExceptionResource.InvalidOperation_EmptyStack:
				return "InvalidOperation_EmptyStack";
			case System.ExceptionResource.InvalidOperation_EnumNotStarted:
				return "InvalidOperation_EnumNotStarted";
			case System.ExceptionResource.InvalidOperation_EnumEnded:
				return "InvalidOperation_EnumEnded";
			case System.ExceptionResource.NotSupported_SortedListNestedWrite:
				return "NotSupported_SortedListNestedWrite";
			case System.ExceptionResource.NotSupported_ValueCollectionSet:
				return "NotSupported_ValueCollectionSet";
			}
			return string.Empty;
		}
	}
}
