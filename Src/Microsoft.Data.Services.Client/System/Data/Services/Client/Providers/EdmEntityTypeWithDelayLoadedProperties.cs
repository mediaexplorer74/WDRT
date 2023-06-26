using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace System.Data.Services.Client.Providers
{
	// Token: 0x02000011 RID: 17
	internal class EdmEntityTypeWithDelayLoadedProperties : EdmEntityType, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003760 File Offset: 0x00001960
		internal EdmEntityTypeWithDelayLoadedProperties(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, Action<EdmEntityTypeWithDelayLoadedProperties> propertyLoadAction)
			: base(namespaceName, name, baseType, isAbstract, isOpen)
		{
			this.propertyLoadAction = propertyLoadAction;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003777 File Offset: 0x00001977
		public override IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				this.EnsurePropertyLoaded();
				return base.DeclaredKey;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003785 File Offset: 0x00001985
		public override IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				this.EnsurePropertyLoaded();
				return base.DeclaredProperties;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003794 File Offset: 0x00001994
		private void EnsurePropertyLoaded()
		{
			lock (this.LockObj)
			{
				if (this.propertyLoadAction != null)
				{
					this.propertyLoadAction(this);
					this.propertyLoadAction = null;
				}
			}
		}

		// Token: 0x04000015 RID: 21
		private Action<EdmEntityTypeWithDelayLoadedProperties> propertyLoadAction;
	}
}
