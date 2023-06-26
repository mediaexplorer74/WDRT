using System;
using System.Configuration;

namespace System.Net
{
	// Token: 0x02000223 RID: 547
	internal sealed class TimeoutValidator : ConfigurationValidatorBase
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x0006A730 File Offset: 0x00068930
		internal TimeoutValidator(bool zeroValid)
		{
			this._zeroValid = zeroValid;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006A73F File Offset: 0x0006893F
		public override bool CanValidate(Type type)
		{
			return type == typeof(int) || type == typeof(long);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006A768 File Offset: 0x00068968
		public override void Validate(object value)
		{
			if (value == null)
			{
				return;
			}
			int num = (int)value;
			if (this._zeroValid && num == 0)
			{
				return;
			}
			if (num <= 0 && num != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_io_timeout_use_gt_zero"));
			}
		}

		// Token: 0x04001606 RID: 5638
		private bool _zeroValid;
	}
}
