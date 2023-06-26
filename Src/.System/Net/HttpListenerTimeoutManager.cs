using System;
using System.Net.Configuration;

namespace System.Net
{
	/// <summary>The timeout manager to use for an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000101 RID: 257
	public class HttpListenerTimeoutManager
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x00035BDA File Offset: 0x00033DDA
		internal HttpListenerTimeoutManager(HttpListener context)
		{
			this.listener = context;
			this.timeouts = new int[5];
			this.LoadConfigurationSettings();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00035BFC File Offset: 0x00033DFC
		private void LoadConfigurationSettings()
		{
			long[] httpListenerTimeouts = SettingsSectionInternal.Section.HttpListenerTimeouts;
			bool flag = false;
			for (int i = 0; i < this.timeouts.Length; i++)
			{
				if (httpListenerTimeouts[i] != 0L)
				{
					this.timeouts[i] = (int)httpListenerTimeouts[i];
					flag = true;
				}
			}
			if (httpListenerTimeouts[5] != 0L)
			{
				this.minSendBytesPerSecond = (uint)httpListenerTimeouts[5];
				flag = true;
			}
			if (flag)
			{
				this.listener.SetServerTimeout(this.timeouts, this.minSendBytesPerSecond);
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00035C67 File Offset: 0x00033E67
		private TimeSpan GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE type)
		{
			return new TimeSpan(0, 0, this.timeouts[(int)type]);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00035C78 File Offset: 0x00033E78
		private void SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE type, TimeSpan value)
		{
			long num = Convert.ToInt64(value.TotalSeconds);
			if (num < 0L || num > 65535L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			int[] array = this.timeouts;
			array[(int)type] = (int)num;
			this.listener.SetServerTimeout(array, this.minSendBytesPerSecond);
			this.timeouts[(int)type] = (int)num;
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00035CD3 File Offset: 0x00033ED3
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00035CDC File Offset: 0x00033EDC
		public TimeSpan EntityBody
		{
			get
			{
				return this.GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.EntityBody);
			}
			set
			{
				this.SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.EntityBody, value);
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00035CE6 File Offset: 0x00033EE6
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00035CEF File Offset: 0x00033EEF
		public TimeSpan DrainEntityBody
		{
			get
			{
				return this.GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.DrainEntityBody);
			}
			set
			{
				this.SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.DrainEntityBody, value);
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00035CF9 File Offset: 0x00033EF9
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00035D02 File Offset: 0x00033F02
		public TimeSpan RequestQueue
		{
			get
			{
				return this.GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.RequestQueue);
			}
			set
			{
				this.SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.RequestQueue, value);
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00035D0C File Offset: 0x00033F0C
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00035D15 File Offset: 0x00033F15
		public TimeSpan IdleConnection
		{
			get
			{
				return this.GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.IdleConnection);
			}
			set
			{
				this.SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.IdleConnection, value);
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00035D1F File Offset: 0x00033F1F
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00035D28 File Offset: 0x00033F28
		public TimeSpan HeaderWait
		{
			get
			{
				return this.GetTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.HeaderWait);
			}
			set
			{
				this.SetTimespanTimeout(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_TYPE.HeaderWait, value);
			}
		}

		/// <summary>Gets or sets the minimum send rate, in bytes-per-second, for the response.</summary>
		/// <returns>The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00035D32 File Offset: 0x00033F32
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00035D3B File Offset: 0x00033F3B
		public long MinSendBytesPerSecond
		{
			get
			{
				return (long)((ulong)this.minSendBytesPerSecond);
			}
			set
			{
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.listener.SetServerTimeout(this.timeouts, (uint)value);
				this.minSendBytesPerSecond = (uint)value;
			}
		}

		// Token: 0x04000E44 RID: 3652
		private HttpListener listener;

		// Token: 0x04000E45 RID: 3653
		private int[] timeouts;

		// Token: 0x04000E46 RID: 3654
		private uint minSendBytesPerSecond;
	}
}
