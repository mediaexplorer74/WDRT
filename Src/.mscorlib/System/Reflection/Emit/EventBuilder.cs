using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Defines events for a class.</summary>
	// Token: 0x02000637 RID: 1591
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x06004A7A RID: 19066 RVA: 0x0010EB2F File Offset: 0x0010CD2F
		private EventBuilder()
		{
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x0010EB37 File Offset: 0x0010CD37
		internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
		{
			this.m_name = name;
			this.m_module = mod;
			this.m_attributes = attr;
			this.m_evToken = evToken;
			this.m_type = type;
		}

		/// <summary>Returns the token for this event.</summary>
		/// <returns>The <see langword="EventToken" /> for this event.</returns>
		// Token: 0x06004A7C RID: 19068 RVA: 0x0010EB64 File Offset: 0x0010CD64
		public EventToken GetEventToken()
		{
			return this.m_evToken;
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x0010EB6C File Offset: 0x0010CD6C
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_module.GetNativeHandle(), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		/// <summary>Sets the method used to subscribe to this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to subscribe to this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A7E RID: 19070 RVA: 0x0010EBC2 File Offset: 0x0010CDC2
		[SecuritySafeCritical]
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
		}

		/// <summary>Sets the method used to unsubscribe to this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to unsubscribe to this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A7F RID: 19071 RVA: 0x0010EBCC File Offset: 0x0010CDCC
		[SecuritySafeCritical]
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
		}

		/// <summary>Sets the method used to raise this event.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method used to raise this event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A80 RID: 19072 RVA: 0x0010EBD7 File Offset: 0x0010CDD7
		[SecuritySafeCritical]
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
		}

		/// <summary>Adds one of the "other" methods associated with this event. "Other" methods are methods other than the "on" and "raise" methods associated with an event. This function can be called many times to add as many "other" methods.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the other method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A81 RID: 19073 RVA: 0x0010EBE2 File Offset: 0x0010CDE2
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A82 RID: 19074 RVA: 0x0010EBEC File Offset: 0x0010CDEC
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06004A83 RID: 19075 RVA: 0x0010EC53 File Offset: 0x0010CE53
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_type.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004A84 RID: 19076 RVA: 0x0010EC85 File Offset: 0x0010CE85
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004A85 RID: 19077 RVA: 0x0010EC8C File Offset: 0x0010CE8C
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004A86 RID: 19078 RVA: 0x0010EC93 File Offset: 0x0010CE93
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004A87 RID: 19079 RVA: 0x0010EC9A File Offset: 0x0010CE9A
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001EB6 RID: 7862
		private string m_name;

		// Token: 0x04001EB7 RID: 7863
		private EventToken m_evToken;

		// Token: 0x04001EB8 RID: 7864
		private ModuleBuilder m_module;

		// Token: 0x04001EB9 RID: 7865
		private EventAttributes m_attributes;

		// Token: 0x04001EBA RID: 7866
		private TypeBuilder m_type;
	}
}
