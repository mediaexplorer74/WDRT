using System;

namespace System.Diagnostics
{
	/// <summary>Identifies the level type for a switch.</summary>
	// Token: 0x020004A9 RID: 1193
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchLevelAttribute" /> class, specifying the type that determines whether a trace should be written.</summary>
		/// <param name="switchLevelType">The <see cref="T:System.Type" /> that determines whether a trace should be written.</param>
		// Token: 0x06002C29 RID: 11305 RVA: 0x000C73B9 File Offset: 0x000C55B9
		public SwitchLevelAttribute(Type switchLevelType)
		{
			this.SwitchLevelType = switchLevelType;
		}

		/// <summary>Gets or sets the type that determines whether a trace should be written.</summary>
		/// <returns>The <see cref="T:System.Type" /> that determines whether a trace should be written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The set operation failed because the value is <see langword="null" />.</exception>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000C73C8 File Offset: 0x000C55C8
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x000C73D0 File Offset: 0x000C55D0
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x040026AB RID: 9899
		private Type type;
	}
}
