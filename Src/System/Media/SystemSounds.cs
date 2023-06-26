using System;
using System.Security.Permissions;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	// Token: 0x020003A5 RID: 933
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	public sealed class SystemSounds
	{
		// Token: 0x060022CC RID: 8908 RVA: 0x000A5BCD File Offset: 0x000A3DCD
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000A5BD5 File Offset: 0x000A3DD5
		public static SystemSound Asterisk
		{
			get
			{
				if (SystemSounds.asterisk == null)
				{
					SystemSounds.asterisk = new SystemSound(64);
				}
				return SystemSounds.asterisk;
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000A5BF5 File Offset: 0x000A3DF5
		public static SystemSound Beep
		{
			get
			{
				if (SystemSounds.beep == null)
				{
					SystemSounds.beep = new SystemSound(0);
				}
				return SystemSounds.beep;
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000A5C14 File Offset: 0x000A3E14
		public static SystemSound Exclamation
		{
			get
			{
				if (SystemSounds.exclamation == null)
				{
					SystemSounds.exclamation = new SystemSound(48);
				}
				return SystemSounds.exclamation;
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000A5C34 File Offset: 0x000A3E34
		public static SystemSound Hand
		{
			get
			{
				if (SystemSounds.hand == null)
				{
					SystemSounds.hand = new SystemSound(16);
				}
				return SystemSounds.hand;
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Question" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Question" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000A5C54 File Offset: 0x000A3E54
		public static SystemSound Question
		{
			get
			{
				if (SystemSounds.question == null)
				{
					SystemSounds.question = new SystemSound(32);
				}
				return SystemSounds.question;
			}
		}

		// Token: 0x04001F9A RID: 8090
		private static volatile SystemSound asterisk;

		// Token: 0x04001F9B RID: 8091
		private static volatile SystemSound beep;

		// Token: 0x04001F9C RID: 8092
		private static volatile SystemSound exclamation;

		// Token: 0x04001F9D RID: 8093
		private static volatile SystemSound hand;

		// Token: 0x04001F9E RID: 8094
		private static volatile SystemSound question;

		// Token: 0x020007E4 RID: 2020
		private class NativeMethods
		{
			// Token: 0x060043B8 RID: 17336 RVA: 0x0011CE69 File Offset: 0x0011B069
			private NativeMethods()
			{
			}

			// Token: 0x040034D3 RID: 13523
			internal const int MB_ICONHAND = 16;

			// Token: 0x040034D4 RID: 13524
			internal const int MB_ICONQUESTION = 32;

			// Token: 0x040034D5 RID: 13525
			internal const int MB_ICONEXCLAMATION = 48;

			// Token: 0x040034D6 RID: 13526
			internal const int MB_ICONASTERISK = 64;
		}
	}
}
