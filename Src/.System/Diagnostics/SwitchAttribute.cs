using System;
using System.Collections;
using System.Reflection;

namespace System.Diagnostics
{
	/// <summary>Identifies a switch used in an assembly, class, or member.</summary>
	// Token: 0x020004A6 RID: 1190
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public sealed class SwitchAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchAttribute" /> class, specifying the name and the type of the switch.</summary>
		/// <param name="switchName">The display name of the switch.</param>
		/// <param name="switchType">The type of the switch.</param>
		// Token: 0x06002C0F RID: 11279 RVA: 0x000C7038 File Offset: 0x000C5238
		public SwitchAttribute(string switchName, Type switchType)
		{
			this.SwitchName = switchName;
			this.SwitchType = switchType;
		}

		/// <summary>Gets or sets the display name of the switch.</summary>
		/// <returns>The display name of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to an empty string.</exception>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000C704E File Offset: 0x000C524E
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000C7058 File Offset: 0x000C5258
		public string SwitchName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "value" }), "value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type of the switch.</summary>
		/// <returns>The type of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchType" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000C70A5 File Offset: 0x000C52A5
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000C70AD File Offset: 0x000C52AD
		public Type SwitchType
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

		/// <summary>Gets or sets the description of the switch.</summary>
		/// <returns>The description of the switch.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000C70CA File Offset: 0x000C52CA
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000C70D2 File Offset: 0x000C52D2
		public string SwitchDescription
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Returns all switch attributes for the specified assembly.</summary>
		/// <param name="assembly">The assembly to check for switch attributes.</param>
		/// <returns>An array that contains all the switch attributes for the assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		// Token: 0x06002C16 RID: 11286 RVA: 0x000C70DC File Offset: 0x000C52DC
		public static SwitchAttribute[] GetAll(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			ArrayList arrayList = new ArrayList();
			object[] customAttributes = assembly.GetCustomAttributes(typeof(SwitchAttribute), false);
			arrayList.AddRange(customAttributes);
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SwitchAttribute.GetAllRecursive(types[i], arrayList);
			}
			SwitchAttribute[] array = new SwitchAttribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000C7154 File Offset: 0x000C5354
		private static void GetAllRecursive(Type type, ArrayList switchAttribs)
		{
			SwitchAttribute.GetAllRecursive(type, switchAttribs);
			MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < members.Length; i++)
			{
				if (!(members[i] is Type))
				{
					SwitchAttribute.GetAllRecursive(members[i], switchAttribs);
				}
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000C7194 File Offset: 0x000C5394
		private static void GetAllRecursive(MemberInfo member, ArrayList switchAttribs)
		{
			object[] customAttributes = member.GetCustomAttributes(typeof(SwitchAttribute), false);
			switchAttribs.AddRange(customAttributes);
		}

		// Token: 0x040026A4 RID: 9892
		private Type type;

		// Token: 0x040026A5 RID: 9893
		private string name;

		// Token: 0x040026A6 RID: 9894
		private string description;
	}
}
