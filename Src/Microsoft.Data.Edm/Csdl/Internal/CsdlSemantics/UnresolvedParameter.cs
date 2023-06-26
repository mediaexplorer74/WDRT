using System;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200018B RID: 395
	internal class UnresolvedParameter : BadElement, IEdmFunctionParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x00018260 File Offset: 0x00016460
		public UnresolvedParameter(IEdmFunctionBase declaringFunction, string name, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedParameter, Strings.Bad_UnresolvedParameter(name))
			})
		{
			this.name = name ?? string.Empty;
			this.declaringFunction = declaringFunction;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000182B1 File Offset: 0x000164B1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000182B9 File Offset: 0x000164B9
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, UnresolvedParameter.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000182CD File Offset: 0x000164CD
		public EdmFunctionParameterMode Mode
		{
			get
			{
				return EdmFunctionParameterMode.In;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000182D0 File Offset: 0x000164D0
		public IEdmFunctionBase DeclaringFunction
		{
			get
			{
				return this.declaringFunction;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000182D8 File Offset: 0x000164D8
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x0400044E RID: 1102
		private readonly string name;

		// Token: 0x0400044F RID: 1103
		private readonly IEdmFunctionBase declaringFunction;

		// Token: 0x04000450 RID: 1104
		private readonly Cache<UnresolvedParameter, IEdmTypeReference> type = new Cache<UnresolvedParameter, IEdmTypeReference>();

		// Token: 0x04000451 RID: 1105
		private static readonly Func<UnresolvedParameter, IEdmTypeReference> ComputeTypeFunc = (UnresolvedParameter me) => me.ComputeType();
	}
}
