using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000286 RID: 646
	internal static class SmtpCommands
	{
		// Token: 0x0400180F RID: 6159
		internal static readonly byte[] Auth = Encoding.ASCII.GetBytes("AUTH ");

		// Token: 0x04001810 RID: 6160
		internal static readonly byte[] CRLF = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x04001811 RID: 6161
		internal static readonly byte[] Data = Encoding.ASCII.GetBytes("DATA\r\n");

		// Token: 0x04001812 RID: 6162
		internal static readonly byte[] DataStop = Encoding.ASCII.GetBytes("\r\n.\r\n");

		// Token: 0x04001813 RID: 6163
		internal static readonly byte[] EHello = Encoding.ASCII.GetBytes("EHLO ");

		// Token: 0x04001814 RID: 6164
		internal static readonly byte[] Expand = Encoding.ASCII.GetBytes("EXPN ");

		// Token: 0x04001815 RID: 6165
		internal static readonly byte[] Hello = Encoding.ASCII.GetBytes("HELO ");

		// Token: 0x04001816 RID: 6166
		internal static readonly byte[] Help = Encoding.ASCII.GetBytes("HELP");

		// Token: 0x04001817 RID: 6167
		internal static readonly byte[] Mail = Encoding.ASCII.GetBytes("MAIL FROM:");

		// Token: 0x04001818 RID: 6168
		internal static readonly byte[] Noop = Encoding.ASCII.GetBytes("NOOP\r\n");

		// Token: 0x04001819 RID: 6169
		internal static readonly byte[] Quit = Encoding.ASCII.GetBytes("QUIT\r\n");

		// Token: 0x0400181A RID: 6170
		internal static readonly byte[] Recipient = Encoding.ASCII.GetBytes("RCPT TO:");

		// Token: 0x0400181B RID: 6171
		internal static readonly byte[] Reset = Encoding.ASCII.GetBytes("RSET\r\n");

		// Token: 0x0400181C RID: 6172
		internal static readonly byte[] Send = Encoding.ASCII.GetBytes("SEND FROM:");

		// Token: 0x0400181D RID: 6173
		internal static readonly byte[] SendAndMail = Encoding.ASCII.GetBytes("SAML FROM:");

		// Token: 0x0400181E RID: 6174
		internal static readonly byte[] SendOrMail = Encoding.ASCII.GetBytes("SOML FROM:");

		// Token: 0x0400181F RID: 6175
		internal static readonly byte[] Turn = Encoding.ASCII.GetBytes("TURN\r\n");

		// Token: 0x04001820 RID: 6176
		internal static readonly byte[] Verify = Encoding.ASCII.GetBytes("VRFY ");

		// Token: 0x04001821 RID: 6177
		internal static readonly byte[] StartTls = Encoding.ASCII.GetBytes("STARTTLS");
	}
}
