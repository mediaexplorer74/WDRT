using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Spatial;
using Microsoft.Data.Edm;

namespace System.Data.Services.Client.Metadata
{
	// Token: 0x02000133 RID: 307
	[DebuggerDisplay("{PropertyName}")]
	internal sealed class ClientPropertyAnnotation
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002B894 File Offset: 0x00029A94
		internal ClientPropertyAnnotation(IEdmProperty edmProperty, PropertyInfo propertyInfo, FieldInfo backingField, ClientEdmModel model)
		{
			this.EdmProperty = edmProperty;
			this.PropertyName = propertyInfo.Name;
			this.NullablePropertyType = propertyInfo.PropertyType;
			this.PropertyType = Nullable.GetUnderlyingType(this.NullablePropertyType) ?? this.NullablePropertyType;
			this.DeclaringClrType = propertyInfo.DeclaringType;
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			this.propertyGetter = ((getMethod == null) ? null : ((Func<object, object>)Expression.Lambda(Expression.Convert(Expression.Call(Expression.Convert(parameterExpression, this.DeclaringClrType), getMethod), typeof(object)), new ParameterExpression[] { parameterExpression }).Compile()));
			this.propertySetter = ((setMethod == null) ? null : ((Action<object, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.DeclaringClrType), setMethod, new Expression[] { Expression.Convert(parameterExpression2, this.NullablePropertyType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
			if (backingField == null)
			{
				this.fieldOrPropertyGetter = this.propertyGetter;
			}
			else
			{
				this.fieldOrPropertyGetter = (Func<object, object>)Expression.Lambda(Expression.Convert(Expression.Field(Expression.Convert(parameterExpression, this.DeclaringClrType), backingField), typeof(object)), new ParameterExpression[] { parameterExpression }).Compile();
			}
			this.Model = model;
			this.IsKnownType = PrimitiveType.IsKnownType(this.PropertyType);
			if (!this.IsKnownType)
			{
				MethodInfo methodForGenericType = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(IDictionary<, >), "set_Item", out this.DictionaryValueType);
				if (methodForGenericType != null)
				{
					ParameterExpression parameterExpression3 = Expression.Parameter(typeof(string), "propertyName");
					this.dictionarySetter = (Action<object, string, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, typeof(IDictionary<, >).MakeGenericType(new Type[]
					{
						typeof(string),
						this.DictionaryValueType
					})), methodForGenericType, parameterExpression3, Expression.Convert(parameterExpression2, this.DictionaryValueType)), new ParameterExpression[] { parameterExpression, parameterExpression3, parameterExpression2 }).Compile();
					return;
				}
				MethodInfo methodForGenericType2 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Contains", out this.collectionGenericType);
				MethodInfo addToCollectionMethod = ClientTypeUtil.GetAddToCollectionMethod(this.PropertyType, out this.collectionGenericType);
				MethodInfo methodForGenericType3 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Remove", out this.collectionGenericType);
				MethodInfo methodForGenericType4 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Clear", out this.collectionGenericType);
				this.collectionContains = ((methodForGenericType2 == null) ? null : ((Func<object, object, bool>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType2, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionAdd = ((addToCollectionMethod == null) ? null : ((Action<object, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), addToCollectionMethod, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionRemove = ((methodForGenericType3 == null) ? null : ((Func<object, object, bool>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType3, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionClear = ((methodForGenericType4 == null) ? null : ((Action<object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType4), new ParameterExpression[] { parameterExpression }).Compile()));
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002BCFF File Offset: 0x00029EFF
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0002BD07 File Offset: 0x00029F07
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002BD10 File Offset: 0x00029F10
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0002BD18 File Offset: 0x00029F18
		internal ClientPropertyAnnotation MimeTypeProperty
		{
			get
			{
				return this.mimeTypeProperty;
			}
			set
			{
				this.mimeTypeProperty = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002BD21 File Offset: 0x00029F21
		internal Type EntityCollectionItemType
		{
			get
			{
				if (!this.IsEntityCollection)
				{
					return null;
				}
				return this.collectionGenericType;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002BD33 File Offset: 0x00029F33
		internal bool IsEntityCollection
		{
			get
			{
				return this.collectionGenericType != null && !this.IsPrimitiveOrComplexCollection;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0002BD4E File Offset: 0x00029F4E
		internal Type PrimitiveOrComplexCollectionItemType
		{
			get
			{
				if (this.IsPrimitiveOrComplexCollection)
				{
					return this.collectionGenericType;
				}
				return null;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002BD60 File Offset: 0x00029F60
		internal bool IsPrimitiveOrComplexCollection
		{
			get
			{
				if (this.isPrimitiveOrComplexCollection == null)
				{
					if (this.collectionGenericType == null)
					{
						this.isPrimitiveOrComplexCollection = new bool?(false);
					}
					else
					{
						bool flag = this.EdmProperty.PropertyKind == EdmPropertyKind.Structural && this.EdmProperty.Type.TypeKind() == EdmTypeKind.Collection;
						if (flag && this.Model.MaxProtocolVersion <= DataServiceProtocolVersion.V2)
						{
							throw new InvalidOperationException(Strings.ClientType_CollectionPropertyNotSupportedInV2AndBelow(this.DeclaringClrType.FullName, this.PropertyName));
						}
						this.isPrimitiveOrComplexCollection = new bool?(flag);
					}
				}
				return this.isPrimitiveOrComplexCollection.Value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002BE00 File Offset: 0x0002A000
		internal bool IsSpatialType
		{
			get
			{
				if (this.isSpatialType == null)
				{
					if (typeof(ISpatial).IsAssignableFrom(this.PropertyType))
					{
						this.isSpatialType = new bool?(true);
					}
					else
					{
						this.isSpatialType = new bool?(false);
					}
				}
				return this.isSpatialType.Value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002BE56 File Offset: 0x0002A056
		internal bool IsDictionary
		{
			get
			{
				return this.DictionaryValueType != null;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002BE64 File Offset: 0x0002A064
		internal bool IsStreamLinkProperty
		{
			get
			{
				return this.PropertyType == typeof(DataServiceStreamLink);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002BE7B File Offset: 0x0002A07B
		internal object GetValue(object instance)
		{
			return this.propertyGetter(instance);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002BE89 File Offset: 0x0002A089
		internal object GetFieldOrPropertyValue(object instance)
		{
			return this.fieldOrPropertyGetter(instance);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002BE97 File Offset: 0x0002A097
		internal void RemoveValue(object instance, object value)
		{
			this.collectionRemove(instance, value);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		internal void SetValue(object instance, object value, string propertyName, bool allowAdd)
		{
			if (this.dictionarySetter != null)
			{
				this.dictionarySetter(instance, propertyName, value);
				return;
			}
			if (allowAdd && this.collectionAdd != null)
			{
				if (!this.collectionContains(instance, value))
				{
					this.AddValueToBackingICollectionInstance(instance, value);
					return;
				}
				return;
			}
			else
			{
				if (this.propertySetter != null)
				{
					this.propertySetter(instance, value);
					return;
				}
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(value.GetType().ToString(), propertyName));
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002BF1D File Offset: 0x0002A11D
		internal void ClearBackingICollectionInstance(object collectionInstance)
		{
			this.collectionClear(collectionInstance);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002BF2B File Offset: 0x0002A12B
		internal void AddValueToBackingICollectionInstance(object collectionInstance, object value)
		{
			this.collectionAdd(collectionInstance, value);
		}

		// Token: 0x040005F4 RID: 1524
		internal readonly IEdmProperty EdmProperty;

		// Token: 0x040005F5 RID: 1525
		internal readonly string PropertyName;

		// Token: 0x040005F6 RID: 1526
		internal readonly Type NullablePropertyType;

		// Token: 0x040005F7 RID: 1527
		internal readonly Type PropertyType;

		// Token: 0x040005F8 RID: 1528
		internal readonly Type DictionaryValueType;

		// Token: 0x040005F9 RID: 1529
		internal readonly Type DeclaringClrType;

		// Token: 0x040005FA RID: 1530
		internal readonly bool IsKnownType;

		// Token: 0x040005FB RID: 1531
		private readonly Func<object, object> fieldOrPropertyGetter;

		// Token: 0x040005FC RID: 1532
		private readonly Func<object, object> propertyGetter;

		// Token: 0x040005FD RID: 1533
		private readonly Action<object, object> propertySetter;

		// Token: 0x040005FE RID: 1534
		private readonly Action<object, string, object> dictionarySetter;

		// Token: 0x040005FF RID: 1535
		private readonly Action<object, object> collectionAdd;

		// Token: 0x04000600 RID: 1536
		private readonly Func<object, object, bool> collectionRemove;

		// Token: 0x04000601 RID: 1537
		private readonly Func<object, object, bool> collectionContains;

		// Token: 0x04000602 RID: 1538
		private readonly Action<object> collectionClear;

		// Token: 0x04000603 RID: 1539
		private readonly Type collectionGenericType;

		// Token: 0x04000604 RID: 1540
		private bool? isPrimitiveOrComplexCollection;

		// Token: 0x04000605 RID: 1541
		private bool? isSpatialType;

		// Token: 0x04000606 RID: 1542
		private ClientPropertyAnnotation mimeTypeProperty;
	}
}
