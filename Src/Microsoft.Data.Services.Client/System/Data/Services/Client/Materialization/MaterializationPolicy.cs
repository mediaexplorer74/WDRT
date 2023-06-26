using System;
using Microsoft.Data.Edm;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000041 RID: 65
	internal abstract class MaterializationPolicy
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000AD97 File Offset: 0x00008F97
		public virtual object CreateNewInstance(IEdmTypeReference edmTypeReference, Type type)
		{
			return Util.ActivatorCreateInstance(type, new object[0]);
		}
	}
}
