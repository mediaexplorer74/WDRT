using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200005B RID: 91
	internal static class ThrowHelper
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00008089 File Offset: 0x00006289
		internal static void ThrowArgumentOutOfRangeException()
		{
			ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008094 File Offset: 0x00006294
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_WrongType", new object[] { key, targetType }), "key");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000080B8 File Offset: 0x000062B8
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_WrongType", new object[] { value, targetType }), "value");
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000080DC File Offset: 0x000062DC
		internal static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000080E3 File Offset: 0x000062E3
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000080F5 File Offset: 0x000062F5
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000810D File Offset: 0x0000630D
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000811A File Offset: 0x0000631A
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008127 File Offset: 0x00006327
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), string.Empty);
			}
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008157 File Offset: 0x00006357
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw new InvalidOperationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008169 File Offset: 0x00006369
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000817B File Offset: 0x0000637B
		internal static void ThrowSecurityException(ExceptionResource resource)
		{
			throw new SecurityException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000818D File Offset: 0x0000638D
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000819F File Offset: 0x0000639F
		internal static void ThrowUnauthorizedAccessException(ExceptionResource resource)
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000081B1 File Offset: 0x000063B1
		internal static void ThrowObjectDisposedException(string objectName, ExceptionResource resource)
		{
			throw new ObjectDisposedException(objectName, Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000081C4 File Offset: 0x000063C4
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000081EC File Offset: 0x000063EC
		internal static string GetArgumentName(ExceptionArgument argument)
		{
			string text;
			switch (argument)
			{
			case ExceptionArgument.obj:
				text = "obj";
				break;
			case ExceptionArgument.dictionary:
				text = "dictionary";
				break;
			case ExceptionArgument.dictionaryCreationThreshold:
				text = "dictionaryCreationThreshold";
				break;
			case ExceptionArgument.array:
				text = "array";
				break;
			case ExceptionArgument.info:
				text = "info";
				break;
			case ExceptionArgument.key:
				text = "key";
				break;
			case ExceptionArgument.collection:
				text = "collection";
				break;
			case ExceptionArgument.list:
				text = "list";
				break;
			case ExceptionArgument.match:
				text = "match";
				break;
			case ExceptionArgument.converter:
				text = "converter";
				break;
			case ExceptionArgument.queue:
				text = "queue";
				break;
			case ExceptionArgument.stack:
				text = "stack";
				break;
			case ExceptionArgument.capacity:
				text = "capacity";
				break;
			case ExceptionArgument.index:
				text = "index";
				break;
			case ExceptionArgument.startIndex:
				text = "startIndex";
				break;
			case ExceptionArgument.value:
				text = "value";
				break;
			case ExceptionArgument.count:
				text = "count";
				break;
			case ExceptionArgument.arrayIndex:
				text = "arrayIndex";
				break;
			case ExceptionArgument.name:
				text = "name";
				break;
			case ExceptionArgument.mode:
				text = "mode";
				break;
			case ExceptionArgument.item:
				text = "item";
				break;
			case ExceptionArgument.options:
				text = "options";
				break;
			case ExceptionArgument.view:
				text = "view";
				break;
			case ExceptionArgument.sourceBytesToCopy:
				text = "sourceBytesToCopy";
				break;
			default:
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008348 File Offset: 0x00006548
		internal static string GetResourceName(ExceptionResource resource)
		{
			string text;
			switch (resource)
			{
			case ExceptionResource.Argument_ImplementIComparable:
				text = "Argument_ImplementIComparable";
				break;
			case ExceptionResource.Argument_InvalidType:
				text = "Argument_InvalidType";
				break;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				text = "Argument_InvalidArgumentForComparison";
				break;
			case ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck:
				text = "Argument_InvalidRegistryKeyPermissionCheck";
				break;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				text = "ArgumentOutOfRange_NeedNonNegNum";
				break;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				text = "Arg_ArrayPlusOffTooSmall";
				break;
			case ExceptionResource.Arg_NonZeroLowerBound:
				text = "Arg_NonZeroLowerBound";
				break;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				text = "Arg_RankMultiDimNotSupported";
				break;
			case ExceptionResource.Arg_RegKeyDelHive:
				text = "Arg_RegKeyDelHive";
				break;
			case ExceptionResource.Arg_RegKeyStrLenBug:
				text = "Arg_RegKeyStrLenBug";
				break;
			case ExceptionResource.Arg_RegSetStrArrNull:
				text = "Arg_RegSetStrArrNull";
				break;
			case ExceptionResource.Arg_RegSetMismatchedKind:
				text = "Arg_RegSetMismatchedKind";
				break;
			case ExceptionResource.Arg_RegSubKeyAbsent:
				text = "Arg_RegSubKeyAbsent";
				break;
			case ExceptionResource.Arg_RegSubKeyValueAbsent:
				text = "Arg_RegSubKeyValueAbsent";
				break;
			case ExceptionResource.Argument_AddingDuplicate:
				text = "Argument_AddingDuplicate";
				break;
			case ExceptionResource.Serialization_InvalidOnDeser:
				text = "Serialization_InvalidOnDeser";
				break;
			case ExceptionResource.Serialization_MissingKeys:
				text = "Serialization_MissingKeys";
				break;
			case ExceptionResource.Serialization_NullKey:
				text = "Serialization_NullKey";
				break;
			case ExceptionResource.Argument_InvalidArrayType:
				text = "Argument_InvalidArrayType";
				break;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				text = "NotSupported_KeyCollectionSet";
				break;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				text = "NotSupported_ValueCollectionSet";
				break;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				text = "ArgumentOutOfRange_SmallCapacity";
				break;
			case ExceptionResource.ArgumentOutOfRange_Index:
				text = "ArgumentOutOfRange_Index";
				break;
			case ExceptionResource.Argument_InvalidOffLen:
				text = "Argument_InvalidOffLen";
				break;
			case ExceptionResource.Argument_ItemNotExist:
				text = "Argument_ItemNotExist";
				break;
			case ExceptionResource.ArgumentOutOfRange_Count:
				text = "ArgumentOutOfRange_Count";
				break;
			case ExceptionResource.ArgumentOutOfRange_InvalidThreshold:
				text = "ArgumentOutOfRange_InvalidThreshold";
				break;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				text = "ArgumentOutOfRange_ListInsert";
				break;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				text = "NotSupported_ReadOnlyCollection";
				break;
			case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				text = "InvalidOperation_CannotRemoveFromStackOrQueue";
				break;
			case ExceptionResource.InvalidOperation_EmptyQueue:
				text = "InvalidOperation_EmptyQueue";
				break;
			case ExceptionResource.InvalidOperation_EnumOpCantHappen:
				text = "InvalidOperation_EnumOpCantHappen";
				break;
			case ExceptionResource.InvalidOperation_EnumFailedVersion:
				text = "InvalidOperation_EnumFailedVersion";
				break;
			case ExceptionResource.InvalidOperation_EmptyStack:
				text = "InvalidOperation_EmptyStack";
				break;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				text = "ArgumentOutOfRange_BiggerThanCollection";
				break;
			case ExceptionResource.InvalidOperation_EnumNotStarted:
				text = "InvalidOperation_EnumNotStarted";
				break;
			case ExceptionResource.InvalidOperation_EnumEnded:
				text = "InvalidOperation_EnumEnded";
				break;
			case ExceptionResource.NotSupported_SortedListNestedWrite:
				text = "NotSupported_SortedListNestedWrite";
				break;
			case ExceptionResource.InvalidOperation_NoValue:
				text = "InvalidOperation_NoValue";
				break;
			case ExceptionResource.InvalidOperation_RegRemoveSubKey:
				text = "InvalidOperation_RegRemoveSubKey";
				break;
			case ExceptionResource.Security_RegistryPermission:
				text = "Security_RegistryPermission";
				break;
			case ExceptionResource.UnauthorizedAccess_RegistryNoWrite:
				text = "UnauthorizedAccess_RegistryNoWrite";
				break;
			case ExceptionResource.ObjectDisposed_RegKeyClosed:
				text = "ObjectDisposed_RegKeyClosed";
				break;
			case ExceptionResource.NotSupported_InComparableType:
				text = "NotSupported_InComparableType";
				break;
			case ExceptionResource.Argument_InvalidRegistryOptionsCheck:
				text = "Argument_InvalidRegistryOptionsCheck";
				break;
			case ExceptionResource.Argument_InvalidRegistryViewCheck:
				text = "Argument_InvalidRegistryViewCheck";
				break;
			default:
				return string.Empty;
			}
			return text;
		}
	}
}
