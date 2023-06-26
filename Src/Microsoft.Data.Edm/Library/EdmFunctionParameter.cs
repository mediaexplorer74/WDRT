using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D4 RID: 468
	public class EdmFunctionParameter : EdmNamedElement, IEdmFunctionParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x00020837 File Offset: 0x0001EA37
		public EdmFunctionParameter(IEdmFunctionBase declaringFunction, string name, IEdmTypeReference type)
			: this(declaringFunction, name, type, EdmFunctionParameterMode.In)
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00020844 File Offset: 0x0001EA44
		public EdmFunctionParameter(IEdmFunctionBase declaringFunction, string name, IEdmTypeReference type, EdmFunctionParameterMode mode)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmFunctionBase>(declaringFunction, "declaringFunction");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.type = type;
			this.mode = mode;
			this.declaringFunction = declaringFunction;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00020892 File Offset: 0x0001EA92
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002089A File Offset: 0x0001EA9A
		public IEdmFunctionBase DeclaringFunction
		{
			get
			{
				return this.declaringFunction;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000208A2 File Offset: 0x0001EAA2
		public EdmFunctionParameterMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x0400053A RID: 1338
		private readonly IEdmTypeReference type;

		// Token: 0x0400053B RID: 1339
		private readonly EdmFunctionParameterMode mode;

		// Token: 0x0400053C RID: 1340
		private readonly IEdmFunctionBase declaringFunction;
	}
}
