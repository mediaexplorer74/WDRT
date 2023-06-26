using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the member of a type that is the default member used by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
	// Token: 0x020005DE RID: 1502
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.DefaultMemberAttribute" /> class.</summary>
		/// <param name="memberName">A <see langword="String" /> containing the name of the member to invoke. This may be a constructor, method, property, or field. A suitable invocation attribute must be specified when the member is invoked. The default member of a class can be specified by passing an empty <see langword="String" /> as the name of the member.  
		///  The default member of a type is marked with the <see langword="DefaultMemberAttribute" /> custom attribute or marked in COM in the usual way.</param>
		// Token: 0x060045CA RID: 17866 RVA: 0x001029AE File Offset: 0x00100BAE
		[__DynamicallyInvokable]
		public DefaultMemberAttribute(string memberName)
		{
			this.m_memberName = memberName;
		}

		/// <summary>Gets the name from the attribute.</summary>
		/// <returns>A string representing the member name.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x001029BD File Offset: 0x00100BBD
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x04001C9B RID: 7323
		private string m_memberName;
	}
}
