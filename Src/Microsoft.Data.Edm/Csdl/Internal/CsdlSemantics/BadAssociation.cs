using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000180 RID: 384
	internal class BadAssociation : BadElement, IEdmAssociation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00017E9E File Offset: 0x0001609E
		public BadAssociation(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00017EC6 File Offset: 0x000160C6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00017ECE File Offset: 0x000160CE
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00017ED6 File Offset: 0x000160D6
		public IEdmAssociationEnd End1
		{
			get
			{
				return new BadAssociationEnd(this, "End1", base.Errors);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00017EE9 File Offset: 0x000160E9
		public IEdmAssociationEnd End2
		{
			get
			{
				return new BadAssociationEnd(this, "End2", base.Errors);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00017EFC File Offset: 0x000160FC
		public CsdlSemanticsReferentialConstraint ReferentialConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000439 RID: 1081
		private string namespaceName;

		// Token: 0x0400043A RID: 1082
		private string name;
	}
}
