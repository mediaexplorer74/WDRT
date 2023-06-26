using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of an event and provides access to event metadata.</summary>
	// Token: 0x020005E0 RID: 1504
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class EventInfo : MemberInfo, _EventInfo
	{
		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060045CD RID: 17869 RVA: 0x001029CD File Offset: 0x00100BCD
		[__DynamicallyInvokable]
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeEventInfo) && !(right is RuntimeEventInfo) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060045CE RID: 17870 RVA: 0x001029F4 File Offset: 0x00100BF4
		[__DynamicallyInvokable]
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060045CF RID: 17871 RVA: 0x00102A00 File Offset: 0x00100C00
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060045D0 RID: 17872 RVA: 0x00102A09 File Offset: 0x00100C09
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x00102A11 File Offset: 0x00100C11
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		/// <summary>Returns the methods that have been associated with the event in metadata using the <see langword=".other" /> directive, specifying whether to include non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> to include non-public methods; otherwise, <see langword="false" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing methods that have been associated with an event in metadata by using the <see langword=".other" /> directive. If there are no methods matching the specification, an empty array is returned.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x060045D2 RID: 17874 RVA: 0x00102A14 File Offset: 0x00100C14
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, retrieves the <see langword="MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060045D3 RID: 17875
		[__DynamicallyInvokable]
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, retrieves the <see langword="MethodInfo" /> object for removing a method of the event, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060045D4 RID: 17876
		[__DynamicallyInvokable]
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns the method that is called when the event is raised, specifying whether to return non-public methods.</summary>
		/// <param name="nonPublic">
		///   <see langword="true" /> if non-public methods can be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object that was called when the event was raised.</returns>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is <see langword="true" />, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060045D5 RID: 17877
		[__DynamicallyInvokable]
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		/// <summary>Gets the attributes for this event.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060045D6 RID: 17878
		[__DynamicallyInvokable]
		public abstract EventAttributes Attributes
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, including non-public methods.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x00102A1B File Offset: 0x00100C1B
		[__DynamicallyInvokable]
		public virtual MethodInfo AddMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetAddMethod(true);
			}
		}

		/// <summary>Gets the <see langword="MethodInfo" /> object for removing a method of the event, including non-public methods.</summary>
		/// <returns>The <see langword="MethodInfo" /> object for removing a method of the event.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00102A24 File Offset: 0x00100C24
		[__DynamicallyInvokable]
		public virtual MethodInfo RemoveMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		/// <summary>Gets the method that is called when the event is raised, including non-public methods.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x00102A2D File Offset: 0x00100C2D
		[__DynamicallyInvokable]
		public virtual MethodInfo RaiseMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		/// <summary>Returns the public methods that have been associated with an event in metadata using the <see langword=".other" /> directive.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public methods that have been associated with the event in metadata by using the <see langword=".other" /> directive. If there are no such public methods, an empty array is returned.</returns>
		// Token: 0x060045DA RID: 17882 RVA: 0x00102A36 File Offset: 0x00100C36
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		/// <summary>Returns the method used to add an event handler delegate to the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		// Token: 0x060045DB RID: 17883 RVA: 0x00102A3F File Offset: 0x00100C3F
		[__DynamicallyInvokable]
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		/// <summary>Returns the method used to remove an event handler delegate from the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		// Token: 0x060045DC RID: 17884 RVA: 0x00102A48 File Offset: 0x00100C48
		[__DynamicallyInvokable]
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		/// <summary>Returns the method that is called when the event is raised.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x060045DD RID: 17885 RVA: 0x00102A51 File Offset: 0x00100C51
		[__DynamicallyInvokable]
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		/// <summary>Adds an event handler to an event source.</summary>
		/// <param name="target">The event source.</param>
		/// <param name="handler">Encapsulates a method or methods to be invoked when the event is raised by the target.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public <see langword="add" /> accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.MethodAccessException">In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have access permission to the member.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="target" /> parameter is <see langword="null" /> and the event is not static.  
		///  -or-  
		///  The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		// Token: 0x060045DE RID: 17886 RVA: 0x00102A5C File Offset: 0x00100C5C
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			MethodInfo addMethod = this.GetAddMethod();
			if (addMethod == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
			}
			if (addMethod.ReturnType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
			}
			addMethod.Invoke(target, new object[] { handler });
		}

		/// <summary>Removes an event handler from an event source.</summary>
		/// <param name="target">The event source.</param>
		/// <param name="handler">The delegate to be disassociated from the events raised by target.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public <see langword="remove" /> accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="target" /> parameter is <see langword="null" /> and the event is not static.  
		///  -or-  
		///  The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		/// <exception cref="T:System.MethodAccessException">In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have access permission to the member.</exception>
		// Token: 0x060045DF RID: 17887 RVA: 0x00102AC4 File Offset: 0x00100CC4
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod();
			if (removeMethod == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicRemoveMethod"));
			}
			ParameterInfo[] parametersNoCopy = removeMethod.GetParametersNoCopy();
			if (parametersNoCopy[0].ParameterType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
			}
			removeMethod.Invoke(target, new object[] { handler });
		}

		/// <summary>Gets the <see langword="Type" /> object of the underlying event-handler delegate associated with this event.</summary>
		/// <returns>A read-only <see langword="Type" /> object representing the delegate event handler.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00102B34 File Offset: 0x00100D34
		[__DynamicallyInvokable]
		public virtual Type EventHandlerType
		{
			[__DynamicallyInvokable]
			get
			{
				MethodInfo addMethod = this.GetAddMethod(true);
				ParameterInfo[] parametersNoCopy = addMethod.GetParametersNoCopy();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					Type parameterType = parametersNoCopy[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the <see langword="EventInfo" /> has a name with a special meaning.</summary>
		/// <returns>
		///   <see langword="true" /> if this event has a special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x00102B81 File Offset: 0x00100D81
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether the event is multicast.</summary>
		/// <returns>
		///   <see langword="true" /> if the delegate is an instance of a multicast delegate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00102B94 File Offset: 0x00100D94
		[__DynamicallyInvokable]
		public virtual bool IsMulticast
		{
			[__DynamicallyInvokable]
			get
			{
				Type eventHandlerType = this.EventHandlerType;
				Type typeFromHandle = typeof(MulticastDelegate);
				return typeFromHandle.IsAssignableFrom(eventHandlerType);
			}
		}

		/// <summary>Returns a T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</summary>
		/// <returns>A T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</returns>
		// Token: 0x060045E3 RID: 17891 RVA: 0x00102BBA File Offset: 0x00100DBA
		Type _EventInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060045E4 RID: 17892 RVA: 0x00102BC2 File Offset: 0x00100DC2
		void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060045E5 RID: 17893 RVA: 0x00102BC9 File Offset: 0x00100DC9
		void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060045E6 RID: 17894 RVA: 0x00102BD0 File Offset: 0x00100DD0
		void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060045E7 RID: 17895 RVA: 0x00102BD7 File Offset: 0x00100DD7
		void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
