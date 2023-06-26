using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Client.Metadata
{
	// Token: 0x02000134 RID: 308
	internal static class ClientTypeUtil
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0002BF3A File Offset: 0x0002A13A
		internal static void SetClientTypeAnnotation(this IEdmModel model, IEdmType edmType, ClientTypeAnnotation annotation)
		{
			model.SetAnnotationValue(edmType, annotation);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002BF44 File Offset: 0x0002A144
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this ClientEdmModel model, Type type)
		{
			IEdmType orCreateEdmType = model.GetOrCreateEdmType(type);
			return model.GetClientTypeAnnotation(orCreateEdmType);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002BF60 File Offset: 0x0002A160
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this IEdmModel model, IEdmType edmType)
		{
			return model.GetAnnotationValue(edmType);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002BF69 File Offset: 0x0002A169
		internal static void SetClientPropertyAnnotation(this IEdmProperty edmProperty, ClientPropertyAnnotation annotation)
		{
			annotation.Model.SetAnnotationValue(edmProperty, annotation);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002BF78 File Offset: 0x0002A178
		internal static ClientPropertyAnnotation GetClientPropertyAnnotation(this IEdmModel model, IEdmProperty edmProperty)
		{
			return model.GetAnnotationValue(edmProperty);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002BF84 File Offset: 0x0002A184
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this IEdmModel model, IEdmProperty edmProperty)
		{
			IEdmType definition = edmProperty.Type.Definition;
			return model.GetAnnotationValue(definition);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		internal static IEdmTypeReference ToEdmTypeReference(this IEdmType edmType, bool isNullable)
		{
			return edmType.ToTypeReference(isNullable);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002BFB0 File Offset: 0x0002A1B0
		internal static string FullName(this IEdmType edmType)
		{
			IEdmSchemaElement edmSchemaElement = edmType as IEdmSchemaElement;
			if (edmSchemaElement != null)
			{
				return edmSchemaElement.FullName();
			}
			return null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002BFD0 File Offset: 0x0002A1D0
		internal static bool IsPrimitive(this IEdmType edmType)
		{
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			IEdmType edmType2 = ((edmCollectionType != null) ? edmCollectionType.ElementType.Definition : edmType);
			return edmType2.TypeKind == EdmTypeKind.Primitive;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002C000 File Offset: 0x0002A200
		internal static MethodInfo GetMethodForGenericType(Type propertyType, Type genericTypeDefinition, string methodName, out Type type)
		{
			type = null;
			Type implementationType = ClientTypeUtil.GetImplementationType(propertyType, genericTypeDefinition);
			if (null != implementationType)
			{
				Type[] genericArguments = implementationType.GetGenericArguments();
				MethodInfo method = implementationType.GetMethod(methodName);
				type = genericArguments[genericArguments.Length - 1];
				return method;
			}
			return null;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002C03C File Offset: 0x0002A23C
		internal static Action<object, object> GetAddToCollectionDelegate(Type listType)
		{
			Type type;
			MethodInfo addToCollectionMethod = ClientTypeUtil.GetAddToCollectionMethod(listType, out type);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "list");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "element");
			Expression expression = Expression.Call(Expression.Convert(parameterExpression, listType), addToCollectionMethod, new Expression[] { Expression.Convert(parameterExpression2, type) });
			LambdaExpression lambdaExpression = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression, parameterExpression2 });
			return (Action<object, object>)lambdaExpression.Compile();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002C0C9 File Offset: 0x0002A2C9
		internal static MethodInfo GetAddToCollectionMethod(Type collectionType, out Type type)
		{
			return ClientTypeUtil.GetMethodForGenericType(collectionType, typeof(ICollection<>), "Add", out type);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
		internal static Type GetImplementationType(Type type, Type genericTypeDefinition)
		{
			if (ClientTypeUtil.IsConstructedGeneric(type, genericTypeDefinition))
			{
				return type;
			}
			Type type2 = null;
			foreach (Type type3 in type.GetInterfaces())
			{
				if (ClientTypeUtil.IsConstructedGeneric(type3, genericTypeDefinition))
				{
					if (!(null == type2))
					{
						throw Error.NotSupported(Strings.ClientType_MultipleImplementationNotSupported);
					}
					type2 = type3;
				}
			}
			return type2;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002C139 File Offset: 0x0002A339
		internal static bool TypeIsEntity(Type t, ClientEdmModel model)
		{
			return model.GetOrCreateEdmType(t).TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002C14A File Offset: 0x0002A34A
		internal static bool TypeOrElementTypeIsEntity(Type type)
		{
			type = TypeSystem.GetElementType(type);
			type = Nullable.GetUnderlyingType(type) ?? type;
			return !PrimitiveType.IsKnownType(type) && ClientTypeUtil.GetKeyPropertiesOnType(type) != null;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002C177 File Offset: 0x0002A377
		internal static bool IsDataServiceCollection(Type type)
		{
			while (type != null)
			{
				if (type.IsGenericType() && WebUtil.IsDataServiceCollectionType(type.GetGenericTypeDefinition()))
				{
					return true;
				}
				type = type.GetBaseType();
			}
			return false;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
		internal static bool CanAssignNull(Type type)
		{
			return !type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
		internal static IEnumerable<PropertyInfo> GetPropertiesOnType(Type type, bool declaredOnly)
		{
			type.ToString();
			if (!PrimitiveType.IsKnownType(type))
			{
				foreach (PropertyInfo propertyInfo in type.GetPublicProperties(true, declaredOnly))
				{
					Type propertyType = propertyInfo.PropertyType;
					propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
					if (!propertyType.IsPointer && (!propertyType.IsArray || !(typeof(byte[]) != propertyType) || !(typeof(char[]) != propertyType)) && !(typeof(IntPtr) == propertyType) && !(typeof(UIntPtr) == propertyType) && (!declaredOnly || !ClientTypeUtil.IsOverride(type, propertyInfo)) && propertyInfo.CanRead && (!propertyType.IsValueType() || propertyInfo.CanWrite) && !propertyType.ContainsGenericParameters() && propertyInfo.GetIndexParameters().Length == 0)
					{
						yield return propertyInfo;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		internal static PropertyInfo[] GetKeyPropertiesOnType(Type type)
		{
			bool flag;
			return ClientTypeUtil.GetKeyPropertiesOnType(type, out flag);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002C550 File Offset: 0x0002A750
		internal static PropertyInfo[] GetKeyPropertiesOnType(Type type, out bool hasProperties)
		{
			if (CommonUtil.IsUnsupportedType(type))
			{
				throw new InvalidOperationException(Strings.ClientType_UnsupportedType(type));
			}
			string text = type.ToString();
			IEnumerable<object> customAttributes = type.GetCustomAttributes(true);
			bool flag = customAttributes.OfType<DataServiceEntityAttribute>().Any<DataServiceEntityAttribute>();
			DataServiceKeyAttribute dataServiceKeyAttribute = customAttributes.OfType<DataServiceKeyAttribute>().FirstOrDefault<DataServiceKeyAttribute>();
			List<PropertyInfo> list = new List<PropertyInfo>();
			PropertyInfo[] properties = ClientTypeUtil.GetPropertiesOnType(type, false).ToArray<PropertyInfo>();
			hasProperties = properties.Length > 0;
			ClientTypeUtil.KeyKind keyKind = ClientTypeUtil.KeyKind.NotKey;
			ClientTypeUtil.KeyKind keyKind2 = ClientTypeUtil.KeyKind.NotKey;
			foreach (PropertyInfo propertyInfo in properties)
			{
				if ((keyKind2 = ClientTypeUtil.IsKeyProperty(propertyInfo, dataServiceKeyAttribute)) != ClientTypeUtil.KeyKind.NotKey)
				{
					if (keyKind2 > keyKind)
					{
						list.Clear();
						keyKind = keyKind2;
						list.Add(propertyInfo);
					}
					else if (keyKind2 == keyKind)
					{
						list.Add(propertyInfo);
					}
				}
			}
			Type type2 = null;
			foreach (PropertyInfo propertyInfo2 in list)
			{
				if (null == type2)
				{
					type2 = propertyInfo2.DeclaringType;
				}
				else if (type2 != propertyInfo2.DeclaringType)
				{
					throw Error.InvalidOperation(Strings.ClientType_KeysOnDifferentDeclaredType(text));
				}
				if (!PrimitiveType.IsKnownType(propertyInfo2.PropertyType))
				{
					throw Error.InvalidOperation(Strings.ClientType_KeysMustBeSimpleTypes(text));
				}
			}
			if (keyKind2 == ClientTypeUtil.KeyKind.AttributedKey && list.Count != dataServiceKeyAttribute.KeyNames.Count)
			{
				string text2 = (from string a in dataServiceKeyAttribute.KeyNames
					where null == properties.Where((PropertyInfo b) => b.Name == a).FirstOrDefault<PropertyInfo>()
					select a).First<string>();
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(text, text2));
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			if (!flag)
			{
				return null;
			}
			return ClientTypeUtil.EmptyPropertyInfoArray;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002C728 File Offset: 0x0002A928
		internal static Type GetMemberType(MemberInfo member)
		{
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			FieldInfo fieldInfo = member as FieldInfo;
			return fieldInfo.FieldType;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002C75C File Offset: 0x0002A95C
		private static ClientTypeUtil.KeyKind IsKeyProperty(PropertyInfo propertyInfo, DataServiceKeyAttribute dataServiceKeyAttribute)
		{
			string name = propertyInfo.Name;
			ClientTypeUtil.KeyKind keyKind = ClientTypeUtil.KeyKind.NotKey;
			if (dataServiceKeyAttribute != null && dataServiceKeyAttribute.KeyNames.Contains(name))
			{
				keyKind = ClientTypeUtil.KeyKind.AttributedKey;
			}
			else if (name.EndsWith("ID", StringComparison.Ordinal))
			{
				string name2 = propertyInfo.DeclaringType.Name;
				if (name.Length == name2.Length + 2 && name.StartsWith(name2, StringComparison.Ordinal))
				{
					keyKind = ClientTypeUtil.KeyKind.TypeNameId;
				}
				else if (2 == name.Length)
				{
					keyKind = ClientTypeUtil.KeyKind.Id;
				}
			}
			return keyKind;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002C7CB File Offset: 0x0002A9CB
		private static bool IsConstructedGeneric(Type type, Type genericTypeDefinition)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == genericTypeDefinition && !type.ContainsGenericParameters();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		private static bool IsOverride(Type type, PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			return getMethod != null && getMethod.GetBaseDefinition().DeclaringType != type;
		}

		// Token: 0x04000608 RID: 1544
		internal static readonly PropertyInfo[] EmptyPropertyInfoArray = new PropertyInfo[0];

		// Token: 0x02000135 RID: 309
		private enum KeyKind
		{
			// Token: 0x0400060A RID: 1546
			NotKey,
			// Token: 0x0400060B RID: 1547
			Id,
			// Token: 0x0400060C RID: 1548
			TypeNameId,
			// Token: 0x0400060D RID: 1549
			AttributedKey
		}
	}
}
