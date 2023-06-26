using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the <see cref="T:System.Net.HttpListener" /> timeouts element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000333 RID: 819
	public sealed class HttpListenerTimeoutsElement : ConfigurationElement
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x0008B5E0 File Offset: 0x000897E0
		static HttpListenerTimeoutsElement()
		{
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.entityBody);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.drainEntityBody);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.requestQueue);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.idleConnection);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.headerWait);
			HttpListenerTimeoutsElement.properties.Add(HttpListenerTimeoutsElement.minSendBytesPerSecond);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0008B6C3 File Offset: 0x000898C3
		private static ConfigurationProperty CreateTimeSpanProperty(string name)
		{
			return new ConfigurationProperty(name, typeof(TimeSpan), TimeSpan.Zero, null, new HttpListenerTimeoutsElement.TimeSpanValidator(), ConfigurationPropertyOptions.None);
		}

		/// <summary>Gets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0008B6E6 File Offset: 0x000898E6
		[ConfigurationProperty("entityBody", DefaultValue = 0, IsRequired = false)]
		public TimeSpan EntityBody
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.entityBody];
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0008B6F8 File Offset: 0x000898F8
		[ConfigurationProperty("drainEntityBody", DefaultValue = 0, IsRequired = false)]
		public TimeSpan DrainEntityBody
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.drainEntityBody];
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0008B70A File Offset: 0x0008990A
		[ConfigurationProperty("requestQueue", DefaultValue = 0, IsRequired = false)]
		public TimeSpan RequestQueue
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.requestQueue];
			}
		}

		/// <summary>Gets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0008B71C File Offset: 0x0008991C
		[ConfigurationProperty("idleConnection", DefaultValue = 0, IsRequired = false)]
		public TimeSpan IdleConnection
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.idleConnection];
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0008B72E File Offset: 0x0008992E
		[ConfigurationProperty("headerWait", DefaultValue = 0, IsRequired = false)]
		public TimeSpan HeaderWait
		{
			get
			{
				return (TimeSpan)base[HttpListenerTimeoutsElement.headerWait];
			}
		}

		/// <summary>Gets the minimum send rate, in bytes-per-second, for the response.</summary>
		/// <returns>The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0008B740 File Offset: 0x00089940
		[ConfigurationProperty("minSendBytesPerSecond", DefaultValue = 0L, IsRequired = false)]
		public long MinSendBytesPerSecond
		{
			get
			{
				return (long)base[HttpListenerTimeoutsElement.minSendBytesPerSecond];
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0008B752 File Offset: 0x00089952
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpListenerTimeoutsElement.properties;
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0008B75C File Offset: 0x0008995C
		internal long[] GetTimeouts()
		{
			return new long[]
			{
				Convert.ToInt64(this.EntityBody.TotalSeconds),
				Convert.ToInt64(this.DrainEntityBody.TotalSeconds),
				Convert.ToInt64(this.RequestQueue.TotalSeconds),
				Convert.ToInt64(this.IdleConnection.TotalSeconds),
				Convert.ToInt64(this.HeaderWait.TotalSeconds),
				this.MinSendBytesPerSecond
			};
		}

		// Token: 0x04001C25 RID: 7205
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C26 RID: 7206
		private static readonly ConfigurationProperty entityBody = HttpListenerTimeoutsElement.CreateTimeSpanProperty("entityBody");

		// Token: 0x04001C27 RID: 7207
		private static readonly ConfigurationProperty drainEntityBody = HttpListenerTimeoutsElement.CreateTimeSpanProperty("drainEntityBody");

		// Token: 0x04001C28 RID: 7208
		private static readonly ConfigurationProperty requestQueue = HttpListenerTimeoutsElement.CreateTimeSpanProperty("requestQueue");

		// Token: 0x04001C29 RID: 7209
		private static readonly ConfigurationProperty idleConnection = HttpListenerTimeoutsElement.CreateTimeSpanProperty("idleConnection");

		// Token: 0x04001C2A RID: 7210
		private static readonly ConfigurationProperty headerWait = HttpListenerTimeoutsElement.CreateTimeSpanProperty("headerWait");

		// Token: 0x04001C2B RID: 7211
		private static readonly ConfigurationProperty minSendBytesPerSecond = new ConfigurationProperty("minSendBytesPerSecond", typeof(long), 0L, null, new HttpListenerTimeoutsElement.LongValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x020007BF RID: 1983
		private class TimeSpanValidator : ConfigurationValidatorBase
		{
			// Token: 0x06004361 RID: 17249 RVA: 0x0011C10C File Offset: 0x0011A30C
			public override bool CanValidate(Type type)
			{
				return type == typeof(TimeSpan);
			}

			// Token: 0x06004362 RID: 17250 RVA: 0x0011C120 File Offset: 0x0011A320
			public override void Validate(object value)
			{
				TimeSpan timeSpan = (TimeSpan)value;
				long num = Convert.ToInt64(timeSpan.TotalSeconds);
				if (num < 0L || num > 65535L)
				{
					throw new ArgumentOutOfRangeException("value", timeSpan, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { "0:0:0", "18:12:15" }));
				}
			}
		}

		// Token: 0x020007C0 RID: 1984
		private class LongValidator : ConfigurationValidatorBase
		{
			// Token: 0x06004364 RID: 17252 RVA: 0x0011C188 File Offset: 0x0011A388
			public override bool CanValidate(Type type)
			{
				return type == typeof(long);
			}

			// Token: 0x06004365 RID: 17253 RVA: 0x0011C19C File Offset: 0x0011A39C
			public override void Validate(object value)
			{
				long num = (long)value;
				if (num < 0L || num > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { 0, uint.MaxValue }));
				}
			}
		}
	}
}
