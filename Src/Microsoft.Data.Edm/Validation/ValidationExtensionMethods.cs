using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation.Internal;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x02000239 RID: 569
	public static class ValidationExtensionMethods
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002548D File Offset: 0x0002368D
		public static bool IsBad(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return element.Errors().FirstOrDefault<EdmError>() != null;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000254AC File Offset: 0x000236AC
		public static IEnumerable<EdmError> Errors(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return InterfaceValidator.GetStructuralErrors(element);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000254C0 File Offset: 0x000236C0
		public static IEnumerable<EdmError> TypeErrors(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return InterfaceValidator.GetStructuralErrors(type).Concat(InterfaceValidator.GetStructuralErrors(type.Definition));
		}
	}
}
