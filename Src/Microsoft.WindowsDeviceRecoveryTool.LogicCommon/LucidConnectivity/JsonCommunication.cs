using System;
using System.Text;
using System.Threading;
using Nokia.Lucid.UsbDeviceIo;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.LucidConnectivity
{
	// Token: 0x0200002C RID: 44
	public class JsonCommunication : IDisposable
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000A348 File Offset: 0x00008548
		public JsonCommunication(UsbDeviceIo deviceIo)
		{
			this.LucidDeviceIo = deviceIo;
			this.messageIdCounter = 0;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A36C File Offset: 0x0000856C
		~JsonCommunication()
		{
			this.Dispose();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A39C File Offset: 0x0000859C
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public UsbDeviceIo LucidDeviceIo
		{
			get
			{
				return this.lucidIo;
			}
			set
			{
				bool flag = this.lucidIo != null;
				if (flag)
				{
					this.lucidIo.OnReceived -= this.HandlReceivedData;
					this.lucidIo.Dispose();
				}
				this.lucidIo = value;
				bool flag2 = this.lucidIo != null;
				if (flag2)
				{
					this.lucidIo.OnReceived += this.HandlReceivedData;
					this.receiveBuffer = null;
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A42C File Offset: 0x0000862C
		public void Dispose()
		{
			this.messageIdCounter = 0;
			bool flag = this.lucidIo != null;
			if (flag)
			{
				this.lucidIo.OnReceived -= this.HandlReceivedData;
				this.lucidIo.Dispose();
				this.lucidIo = null;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A47B File Offset: 0x0000867B
		public void Send(byte[] request)
		{
			this.LucidDeviceIo.Send(request, (uint)request.Length);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A490 File Offset: 0x00008690
		public byte[] Receive(TimeSpan timeSpan)
		{
			Thread.Sleep(timeSpan);
			bool flag = this.receiveBuffer == null;
			if (flag)
			{
				throw new TimeoutException("JsonComms: No message received");
			}
			return this.receiveBuffer;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000213E File Offset: 0x0000033E
		public void SetFilteringState(bool doFilter)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public void Send(string message)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				this.messageIdCounter++;
				message = message.Replace("\"method\"", "\"id\":" + this.messageIdCounter.ToString() + ",\"method\"");
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				byte[] bytes = asciiencoding.GetBytes(message);
				this.Send(bytes);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A554 File Offset: 0x00008754
		public string ReceiveJson(TimeSpan timeSpan)
		{
			byte[] array = this.Receive(timeSpan);
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			return asciiencoding.GetString(array);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A580 File Offset: 0x00008780
		private void HandlReceivedData(object sender, OnReceivedEventArgs eventArgs)
		{
			byte[] data = eventArgs.Data;
			object obj = this.syncObject;
			lock (obj)
			{
				this.receiveBuffer = new byte[data.Length];
				Buffer.BlockCopy(data, 0, this.receiveBuffer, 0, data.Length);
			}
		}

		// Token: 0x0400012E RID: 302
		private readonly object syncObject = new object();

		// Token: 0x0400012F RID: 303
		private UsbDeviceIo lucidIo;

		// Token: 0x04000130 RID: 304
		private byte[] receiveBuffer;

		// Token: 0x04000131 RID: 305
		private int messageIdCounter;
	}
}
