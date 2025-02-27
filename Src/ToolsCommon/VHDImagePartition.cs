﻿using System;
using System.IO;
using System.Management;
using System.Threading;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x0200003A RID: 58
	public class VHDImagePartition : ImagePartition
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00008C34 File Offset: 0x00006E34
		public VHDImagePartition(string deviceId, string partitionId)
		{
			base.PhysicalDeviceId = deviceId;
			base.Name = partitionId;
			string text = string.Empty;
			int num = 10;
			int num2 = 0;
			bool flag;
			do
			{
				flag = false;
				text = this.GetLogicalDriveFromWMI(deviceId, partitionId);
				if (string.IsNullOrEmpty(text))
				{
					Console.WriteLine("  ImagePartition.GetLogicalDriveFromWMI({0}, {1}) not found, sleeping...", deviceId, partitionId);
					num2++;
					flag = num2 < num;
					Thread.Sleep(500);
				}
			}
			while (flag);
			if (string.IsNullOrEmpty(text))
			{
				throw new IUException("Failed to retrieve logical drive name of partition {0} using WMI", new object[] { partitionId });
			}
			if (string.Compare(text, "NONE", true) != 0)
			{
				base.MountedDriveInfo = new DriveInfo(Path.GetPathRoot(text));
				base.Root = base.MountedDriveInfo.RootDirectory.FullName;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private string GetLogicalDriveFromWMI(string deviceId, string partitionId)
		{
			string text = string.Empty;
			bool flag = false;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("Select * from Win32_DiskPartition where Name='{0}'", partitionId)))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					Console.WriteLine("  ImagePartition.GetLogicalDriveFromWMI: Path={0}", managementObject.Path.ToString());
					if (string.Compare(managementObject.GetPropertyValue("Type").ToString(), "unknown", true) == 0)
					{
						text = "NONE";
						break;
					}
					RelatedObjectQuery relatedObjectQuery = new RelatedObjectQuery(managementObject.Path.ToString(), "Win32_LogicalDisk");
					ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher(relatedObjectQuery);
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator2 = managementObjectSearcher2.Get().GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							ManagementObject managementObject2 = (ManagementObject)enumerator2.Current;
							text = managementObject2.GetPropertyValue("Name").ToString();
							flag = true;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x040000E6 RID: 230
		private const string WMI_GETPARTITIONS_QUERY = "Select * from Win32_DiskPartition where Name='{0}'";

		// Token: 0x040000E7 RID: 231
		private const string WMI_DISKPARTITION_CLASS = "Win32_DiskPartition";

		// Token: 0x040000E8 RID: 232
		private const string WMI_LOGICALDISK_CLASS = "Win32_LogicalDisk";

		// Token: 0x040000E9 RID: 233
		private const string STR_NAME = "Name";

		// Token: 0x040000EA RID: 234
		private const int MAX_RETRY = 10;

		// Token: 0x040000EB RID: 235
		private const int SLEEP_500 = 500;
	}
}
