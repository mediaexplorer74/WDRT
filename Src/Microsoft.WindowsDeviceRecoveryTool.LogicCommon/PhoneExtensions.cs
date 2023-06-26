using System;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon
{
	// Token: 0x02000005 RID: 5
	public static class PhoneExtensions
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002738 File Offset: 0x00000938
		public static bool IsPhoneDeviceType(this Phone phone)
		{
			PhoneTypes[] array = new PhoneTypes[]
			{
				PhoneTypes.Alcatel,
				PhoneTypes.Blu,
				PhoneTypes.Htc,
				PhoneTypes.Lg,
				PhoneTypes.Lumia,
				PhoneTypes.Mcj
			};
			return array.Contains(phone.Type);
		}
	}
}
