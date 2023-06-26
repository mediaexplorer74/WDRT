using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D3 RID: 467
	public class EdmFunctionImport : EdmFunctionBase, IEdmFunctionImport, IEdmFunctionBase, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x00020796 File Offset: 0x0001E996
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmTypeReference returnType)
			: this(container, name, returnType, null, true, false, false)
		{
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000207A5 File Offset: 0x0001E9A5
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmTypeReference returnType, IEdmExpression entitySet)
			: this(container, name, returnType, entitySet, true, false, false)
		{
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000207B8 File Offset: 0x0001E9B8
		public EdmFunctionImport(IEdmEntityContainer container, string name, IEdmTypeReference returnType, IEdmExpression entitySet, bool isSideEffecting, bool isComposable, bool isBindable)
			: base(name, returnType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.container = container;
			this.entitySet = entitySet;
			this.isSideEffecting = isSideEffecting;
			this.isComposable = isComposable;
			this.isBindable = isBindable;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002080C File Offset: 0x0001EA0C
		public bool IsSideEffecting
		{
			get
			{
				return this.isSideEffecting;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00020814 File Offset: 0x0001EA14
		public bool IsComposable
		{
			get
			{
				return this.isComposable;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002081C File Offset: 0x0001EA1C
		public bool IsBindable
		{
			get
			{
				return this.isBindable;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00020824 File Offset: 0x0001EA24
		public IEdmExpression EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002082C File Offset: 0x0001EA2C
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002082F File Offset: 0x0001EA2F
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x04000535 RID: 1333
		private readonly IEdmEntityContainer container;

		// Token: 0x04000536 RID: 1334
		private readonly IEdmExpression entitySet;

		// Token: 0x04000537 RID: 1335
		private readonly bool isSideEffecting;

		// Token: 0x04000538 RID: 1336
		private readonly bool isComposable;

		// Token: 0x04000539 RID: 1337
		private readonly bool isBindable;
	}
}
