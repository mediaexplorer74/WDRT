using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation
{
	// Token: 0x02000002 RID: 2
	public sealed class ManufacturerModelsCatalog
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ManufacturerModelsCatalog(ManufacturerInfo manufacturerInfo, IEnumerable<ModelInfo> modelInfos)
		{
			if (manufacturerInfo == null)
			{
				throw new ArgumentNullException("manufacturerInfo");
			}
			if (modelInfos == null)
			{
				throw new ArgumentNullException("modelInfos");
			}
			ModelInfo[] array = modelInfos.ToArray<ModelInfo>();
			if (array.Length == 0)
			{
				throw new ArgumentException("modelInfos should have at least one element");
			}
			this.ManufacturerInfo = manufacturerInfo;
			this.Models = array;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020A3 File Offset: 0x000002A3
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020AB File Offset: 0x000002AB
		public ManufacturerInfo ManufacturerInfo { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		public ModelInfo[] Models { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020C5 File Offset: 0x000002C5
		public DeviceDetectionInformation[] GetDeviceDetectionInformations()
		{
			return this.Models.SelectMany((ModelInfo m) => m.DetectionInfo.DeviceDetectionInformations).ToArray<DeviceDetectionInformation>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public bool TryGetModelInfo(string deviceReturnedVariantName, out ModelInfo modelInfo)
		{
			if (string.IsNullOrEmpty(deviceReturnedVariantName))
			{
				modelInfo = null;
				return false;
			}
			Func<string, bool> <>9__2;
			Func<VariantInfo, bool> <>9__1;
			ModelInfo modelInfo2 = this.Models.FirstOrDefault(delegate(ModelInfo m)
			{
				IEnumerable<VariantInfo> variants = m.Variants;
				Func<VariantInfo, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(VariantInfo v)
					{
						IEnumerable<string> deviceReturnedValues = v.IdentificationInfo.DeviceReturnedValues;
						Func<string, bool> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (string dv) => deviceReturnedVariantName.IndexOf(dv, StringComparison.OrdinalIgnoreCase) >= 0);
						}
						return deviceReturnedValues.Any(func2);
					});
				}
				return variants.Any(func);
			});
			if (modelInfo2 == null)
			{
				modelInfo = null;
				return false;
			}
			modelInfo = modelInfo2;
			return true;
		}
	}
}
