using System;

namespace System.Net.Mail
{
	// Token: 0x02000297 RID: 663
	internal class SmtpPooledStream : PooledStream
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0007CEB7 File Offset: 0x0007B0B7
		internal SmtpPooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
			: base(connectionPool, lifetime, checkLifetime)
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0007CEC4 File Offset: 0x0007B0C4
		protected override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "SmtpPooledStream::Dispose #" + ValidationHelper.HashString(this));
			}
			if (disposing && base.NetworkStream.Connected)
			{
				this.Write(SmtpCommands.Quit, 0, SmtpCommands.Quit.Length);
				this.Flush();
				byte[] array = new byte[80];
				int num = this.Read(array, 0, 80);
			}
			base.Dispose(disposing);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, "SmtpPooledStream::Dispose #" + ValidationHelper.HashString(this));
			}
		}

		// Token: 0x04001878 RID: 6264
		internal bool previouslyUsed;

		// Token: 0x04001879 RID: 6265
		internal bool dsnEnabled;

		// Token: 0x0400187A RID: 6266
		internal bool serverSupportsEai;

		// Token: 0x0400187B RID: 6267
		internal ICredentialsByHost creds;

		// Token: 0x0400187C RID: 6268
		private const int safeBufferLength = 80;
	}
}
