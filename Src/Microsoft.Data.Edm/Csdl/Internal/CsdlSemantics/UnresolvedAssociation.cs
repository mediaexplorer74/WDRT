using System;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001A4 RID: 420
	internal class UnresolvedAssociation : BadAssociation, IUnresolvedElement
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00018854 File Offset: 0x00016A54
		public UnresolvedAssociation(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedType, Strings.Bad_UnresolvedType(qualifiedName))
			})
		{
		}
	}
}
