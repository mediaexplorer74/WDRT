using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000199 RID: 409
	internal class ResponseDescription
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000535F8 File Offset: 0x000517F8
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00053616 File Offset: 0x00051816
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00053637 File Offset: 0x00051837
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00053658 File Offset: 0x00051858
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00053679 File Offset: 0x00051879
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x040012F4 RID: 4852
		internal const int NoStatus = -1;

		// Token: 0x040012F5 RID: 4853
		internal bool Multiline;

		// Token: 0x040012F6 RID: 4854
		internal int Status = -1;

		// Token: 0x040012F7 RID: 4855
		internal string StatusDescription;

		// Token: 0x040012F8 RID: 4856
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x040012F9 RID: 4857
		internal string StatusCodeString;
	}
}
