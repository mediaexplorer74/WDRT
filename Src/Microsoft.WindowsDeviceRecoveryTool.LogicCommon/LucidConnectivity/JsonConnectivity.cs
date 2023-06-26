using System;
using Nokia.Lucid.UsbDeviceIo;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity
{
	// Token: 0x0200002D RID: 45
	public class JsonConnectivity
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public JsonCommunication CreateJsonConnectivity(string path)
		{
			this.CloseConnection();
			this.deviceIo = new UsbDeviceIo(path);
			this.jsonConnection = new JsonCommunication(this.deviceIo);
			return this.jsonConnection;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A624 File Offset: 0x00008824
		public void CloseConnection()
		{
			bool flag = this.jsonConnection != null;
			if (flag)
			{
				this.jsonConnection.Dispose();
				this.jsonConnection = null;
			}
			bool flag2 = this.deviceIo != null;
			if (flag2)
			{
				this.deviceIo.Dispose();
				this.deviceIo = null;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A676 File Offset: 0x00008876
		public void Dispose()
		{
			this.CloseConnection();
		}

		// Token: 0x04000132 RID: 306
		private JsonCommunication jsonConnection;

		// Token: 0x04000133 RID: 307
		private UsbDeviceIo deviceIo;
	}
}
