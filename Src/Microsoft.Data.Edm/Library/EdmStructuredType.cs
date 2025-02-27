﻿using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CA RID: 458
	public abstract class EdmStructuredType : EdmType, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001FDBB File Offset: 0x0001DFBB
		protected EdmStructuredType(bool isAbstract, bool isOpen, IEdmStructuredType baseStructuredType)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.baseStructuredType = baseStructuredType;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0001FDF9 File Offset: 0x0001DFF9
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001FE01 File Offset: 0x0001E001
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001FE09 File Offset: 0x0001E009
		public virtual IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredProperties;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0001FE11 File Offset: 0x0001E011
		public IEdmStructuredType BaseType
		{
			get
			{
				return this.baseStructuredType;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001FE1C File Offset: 0x0001E01C
		protected IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				IDictionary<string, IEdmProperty> value;
				lock (this.LockObj)
				{
					value = this.propertiesDictionary.GetValue(this, EdmStructuredType.ComputePropertiesDictionaryFunc, null);
				}
				return value;
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001FE6C File Offset: 0x0001E06C
		public void AddProperty(IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			if (!object.ReferenceEquals(this, property.DeclaringType))
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(property.Name));
			}
			this.declaredProperties.Add(property);
			lock (this.LockObj)
			{
				this.propertiesDictionary.Clear(null);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001FEEC File Offset: 0x0001E0EC
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, true));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001FF18 File Offset: 0x0001E118
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type, bool isNullable)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, isNullable));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001FF44 File Offset: 0x0001E144
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001FF64 File Offset: 0x0001E164
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type, string defaultValue, EdmConcurrencyMode concurrencyMode)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type, defaultValue, concurrencyMode);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001FF88 File Offset: 0x0001E188
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			if (!this.PropertiesDictionary.TryGetValue(name, out edmProperty))
			{
				return null;
			}
			return edmProperty;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04000516 RID: 1302
		protected readonly object LockObj = new object();

		// Token: 0x04000517 RID: 1303
		private readonly IEdmStructuredType baseStructuredType;

		// Token: 0x04000518 RID: 1304
		private readonly List<IEdmProperty> declaredProperties = new List<IEdmProperty>();

		// Token: 0x04000519 RID: 1305
		private readonly bool isAbstract;

		// Token: 0x0400051A RID: 1306
		private readonly bool isOpen;

		// Token: 0x0400051B RID: 1307
		private readonly Cache<EdmStructuredType, IDictionary<string, IEdmProperty>> propertiesDictionary = new Cache<EdmStructuredType, IDictionary<string, IEdmProperty>>();

		// Token: 0x0400051C RID: 1308
		private static readonly Func<EdmStructuredType, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (EdmStructuredType me) => me.ComputePropertiesDictionary();
	}
}
