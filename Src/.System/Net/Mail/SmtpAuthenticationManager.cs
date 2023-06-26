using System;
using System.Collections;

namespace System.Net.Mail
{
	// Token: 0x02000277 RID: 631
	internal static class SmtpAuthenticationManager
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x00078A48 File Offset: 0x00076C48
		static SmtpAuthenticationManager()
		{
			SmtpAuthenticationManager.Register(new SmtpNegotiateAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpNtlmAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpDigestAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpLoginAuthenticationModule());
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00078A7C File Offset: 0x00076C7C
		internal static void Register(ISmtpAuthenticationModule module)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			ArrayList arrayList = SmtpAuthenticationManager.modules;
			lock (arrayList)
			{
				SmtpAuthenticationManager.modules.Add(module);
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00078AD0 File Offset: 0x00076CD0
		internal static ISmtpAuthenticationModule[] GetModules()
		{
			ArrayList arrayList = SmtpAuthenticationManager.modules;
			ISmtpAuthenticationModule[] array2;
			lock (arrayList)
			{
				ISmtpAuthenticationModule[] array = new ISmtpAuthenticationModule[SmtpAuthenticationManager.modules.Count];
				SmtpAuthenticationManager.modules.CopyTo(0, array, 0, SmtpAuthenticationManager.modules.Count);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x040017E8 RID: 6120
		private static ArrayList modules = new ArrayList();
	}
}
