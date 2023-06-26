using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000131 RID: 305
	internal static class WebUtil
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002B40C File Offset: 0x0002960C
		private static bool DataServiceCollectionAvailable
		{
			get
			{
				if (WebUtil.dataServiceCollectionAvailable == null)
				{
					try
					{
						WebUtil.dataServiceCollectionAvailable = new bool?(WebUtil.GetDataServiceCollectionOfTType() != null);
					}
					catch (FileNotFoundException)
					{
						WebUtil.dataServiceCollectionAvailable = new bool?(false);
					}
				}
				return WebUtil.dataServiceCollectionAvailable.Value;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002B464 File Offset: 0x00029664
		internal static long CopyStream(Stream input, Stream output, ref byte[] refBuffer)
		{
			long num = 0L;
			byte[] array = refBuffer;
			if (array == null)
			{
				refBuffer = (array = new byte[1000]);
			}
			int num2;
			while (input.CanRead && 0 < (num2 = input.Read(array, 0, array.Length)))
			{
				output.Write(array, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002B4B4 File Offset: 0x000296B4
		internal static InvalidOperationException GetHttpWebResponse(InvalidOperationException exception, ref IODataResponseMessage response)
		{
			if (response == null)
			{
				DataServiceTransportException ex = exception as DataServiceTransportException;
				if (ex != null)
				{
					response = ex.Response;
					return (InvalidOperationException)ex.InnerException;
				}
			}
			return exception;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002B4E4 File Offset: 0x000296E4
		internal static bool SuccessStatusCode(HttpStatusCode status)
		{
			return HttpStatusCode.OK <= status && status < HttpStatusCode.MultipleChoices;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002B4F8 File Offset: 0x000296F8
		internal static bool IsCLRTypeCollection(Type type, ClientEdmModel model)
		{
			if (!PrimitiveType.IsKnownNullableType(type))
			{
				Type implementationType = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
				if (implementationType != null && !ClientTypeUtil.TypeIsEntity(implementationType.GetGenericArguments()[0], model))
				{
					if (model.MaxProtocolVersion <= DataServiceProtocolVersion.V2)
					{
						throw new InvalidOperationException(Strings.WebUtil_CollectionTypeNotSupportedInV2OrBelow(type.FullName));
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002B554 File Offset: 0x00029754
		internal static bool IsWireTypeCollection(string wireTypeName)
		{
			return CommonUtil.GetCollectionItemTypeName(wireTypeName, false) != null;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002B563 File Offset: 0x00029763
		internal static string GetCollectionItemWireTypeName(string wireTypeName)
		{
			return CommonUtil.GetCollectionItemTypeName(wireTypeName, false);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002B56C File Offset: 0x0002976C
		internal static Type GetBackingTypeForCollectionProperty(Type collectionPropertyType, Type collectionItemType)
		{
			Type type;
			if (collectionPropertyType.IsInterface())
			{
				type = typeof(ObservableCollection<>).MakeGenericType(new Type[] { collectionItemType });
			}
			else
			{
				type = collectionPropertyType;
			}
			return type;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002B5A4 File Offset: 0x000297A4
		internal static T CheckArgumentNull<T>([WebUtil.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002B5C8 File Offset: 0x000297C8
		internal static void ValidateCollection(Type collectionItemType, object propertyValue, string propertyName)
		{
			if (!PrimitiveType.IsKnownNullableType(collectionItemType))
			{
				if (collectionItemType.GetInterfaces().SingleOrDefault((Type t) => t == typeof(IEnumerable)) != null)
				{
					throw Error.InvalidOperation(Strings.ClientType_CollectionOfCollectionNotSupported);
				}
			}
			if (propertyValue != null)
			{
				return;
			}
			if (propertyName != null)
			{
				throw Error.InvalidOperation(Strings.Collection_NullCollectionNotSupported(propertyName));
			}
			throw Error.InvalidOperation(Strings.Collection_NullNonPropertyCollectionNotSupported(collectionItemType));
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002B636 File Offset: 0x00029836
		internal static void ValidateCollectionItem(object itemValue)
		{
			if (itemValue == null)
			{
				throw Error.InvalidOperation(Strings.Collection_NullCollectionItemsNotSupported);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002B648 File Offset: 0x00029848
		internal static void ValidatePrimitiveCollectionItem(object itemValue, string propertyName, Type collectionItemType)
		{
			Type type = itemValue.GetType();
			if (!PrimitiveType.IsKnownNullableType(type))
			{
				throw Error.InvalidOperation(Strings.Collection_ComplexTypesInCollectionOfPrimitiveTypesNotAllowed);
			}
			if (collectionItemType.IsAssignableFrom(type))
			{
				return;
			}
			if (propertyName != null)
			{
				throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInCollection(propertyName));
			}
			throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInNonPropertyCollection(collectionItemType));
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002B694 File Offset: 0x00029894
		internal static void ValidateComplexCollectionItem(object itemValue, string propertyName, Type collectionItemType)
		{
			Type type = itemValue.GetType();
			if (PrimitiveType.IsKnownNullableType(type))
			{
				throw Error.InvalidOperation(Strings.Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed);
			}
			if (!(type != collectionItemType))
			{
				return;
			}
			if (propertyName != null)
			{
				throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInCollection(propertyName));
			}
			throw Error.InvalidOperation(Strings.WebUtil_TypeMismatchInNonPropertyCollection(collectionItemType));
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002B6E0 File Offset: 0x000298E0
		internal static void ValidateIdentityValue(string identity)
		{
			Uri uri = UriUtil.CreateUri(identity, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				throw Error.InvalidOperation(Strings.Context_TrackingExpectsAbsoluteUri);
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002B708 File Offset: 0x00029908
		internal static Uri ValidateLocationHeader(string location)
		{
			Uri uri = UriUtil.CreateUri(location, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				throw Error.InvalidOperation(Strings.Context_LocationHeaderExpectsAbsoluteUri);
			}
			return uri;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002B734 File Offset: 0x00029934
		internal static string GetPreferHeaderAndRequestVersion(DataServiceResponsePreference responsePreference, ref Version requestVersion)
		{
			string text = null;
			if (responsePreference != DataServiceResponsePreference.None)
			{
				if (responsePreference == DataServiceResponsePreference.IncludeContent)
				{
					text = "return-content";
				}
				else
				{
					text = "return-no-content";
				}
				WebUtil.RaiseVersion(ref requestVersion, Util.DataServiceVersion3);
			}
			return text;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002B764 File Offset: 0x00029964
		internal static void RaiseVersion(ref Version version, Version minimalVersion)
		{
			if (version == null || version < minimalVersion)
			{
				version = minimalVersion;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002B77D File Offset: 0x0002997D
		internal static bool IsDataServiceCollectionType(Type t)
		{
			return WebUtil.DataServiceCollectionAvailable && t == WebUtil.GetDataServiceCollectionOfTType();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002B793 File Offset: 0x00029993
		internal static Type GetDataServiceCollectionOfT(params Type[] typeArguments)
		{
			if (WebUtil.DataServiceCollectionAvailable)
			{
				return WebUtil.GetDataServiceCollectionOfTType().MakeGenericType(typeArguments);
			}
			return null;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002B7AC File Offset: 0x000299AC
		internal static object GetDefaultValue(Type type)
		{
			return WebUtil.getDefaultValueMethodInfo.MakeGenericMethod(new Type[] { type }).Invoke(null, null);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002B7D8 File Offset: 0x000299D8
		internal static T GetDefaultValue<T>()
		{
			return default(T);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002B7F0 File Offset: 0x000299F0
		internal static void DisposeMessage(IODataResponseMessage responseMessage)
		{
			IDisposable disposable = responseMessage as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002B80D File Offset: 0x00029A0D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Type GetDataServiceCollectionOfTType()
		{
			return typeof(DataServiceCollection<>);
		}

		// Token: 0x040005EF RID: 1519
		internal const int DefaultBufferSizeForStreamCopy = 65536;

		// Token: 0x040005F0 RID: 1520
		private static bool? dataServiceCollectionAvailable = null;

		// Token: 0x040005F1 RID: 1521
		private static MethodInfo getDefaultValueMethodInfo = (MethodInfo)typeof(WebUtil).GetMember("GetDefaultValue", BindingFlags.Static | BindingFlags.NonPublic).Single((MemberInfo m) => ((MethodInfo)m).GetGenericArguments().Count<Type>() == 1);

		// Token: 0x02000132 RID: 306
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
