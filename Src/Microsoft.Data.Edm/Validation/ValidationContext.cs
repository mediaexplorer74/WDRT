using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x02000238 RID: 568
	public sealed class ValidationContext
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x00025430 File Offset: 0x00023630
		internal ValidationContext(IEdmModel model, Func<object, bool> isBad)
		{
			this.model = model;
			this.isBad = isBad;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00025451 File Offset: 0x00023651
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00025459 File Offset: 0x00023659
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00025461 File Offset: 0x00023661
		public bool IsBad(IEdmElement element)
		{
			return this.isBad(element);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002546F File Offset: 0x0002366F
		public void AddError(EdmLocation location, EdmErrorCode errorCode, string errorMessage)
		{
			this.AddError(new EdmError(location, errorCode, errorMessage));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002547F File Offset: 0x0002367F
		public void AddError(EdmError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x0400059A RID: 1434
		private readonly List<EdmError> errors = new List<EdmError>();

		// Token: 0x0400059B RID: 1435
		private readonly IEdmModel model;

		// Token: 0x0400059C RID: 1436
		private readonly Func<object, bool> isBad;
	}
}
