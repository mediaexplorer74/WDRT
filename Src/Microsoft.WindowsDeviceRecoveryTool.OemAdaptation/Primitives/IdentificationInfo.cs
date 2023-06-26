using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives
{
	// Token: 0x02000004 RID: 4
	public sealed class IdentificationInfo
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public IdentificationInfo(IEnumerable<string> deviceReturnedValues)
		{
			if (deviceReturnedValues == null)
			{
				throw new ArgumentNullException("deviceReturnedValues");
			}
			string[] array = deviceReturnedValues.ToArray<string>();
			if (array.Length == 0)
			{
				throw new ArgumentException("deviceReturnedValues should have at least one element");
			}
			this.DeviceReturnedValues = array;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021DE File Offset: 0x000003DE
		public string[] DeviceReturnedValues { get; private set; }
	}
}
