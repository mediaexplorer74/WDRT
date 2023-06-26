using System;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020001E0 RID: 480
	internal class UnresolvedComplexType : BadComplexType, IUnresolvedElement
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x000212F4 File Offset: 0x0001F4F4
		public UnresolvedComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedComplexType, Strings.Bad_UnresolvedComplexType(qualifiedName))
			})
		{
		}
	}
}
