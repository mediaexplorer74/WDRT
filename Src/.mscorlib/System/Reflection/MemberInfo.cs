using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	/// <summary>Obtains information about the attributes of a member and provides access to member metadata.</summary>
	// Token: 0x020005FF RID: 1535
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MemberInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
	{
		// Token: 0x060046EF RID: 18159 RVA: 0x00104741 File Offset: 0x00102941
		internal virtual bool CacheEquals(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating the type of the member - method, constructor, event, and so on.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating the type of member.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060046F0 RID: 18160
		public abstract MemberTypes MemberType { get; }

		/// <summary>Gets the name of the current member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060046F1 RID: 18161
		[__DynamicallyInvokable]
		public abstract string Name
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The <see langword="Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060046F2 RID: 18162
		[__DynamicallyInvokable]
		public abstract Type DeclaringType
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the class object that was used to obtain this instance of <see langword="MemberInfo" />.</summary>
		/// <returns>The <see langword="Type" /> object through which this <see langword="MemberInfo" /> object was obtained.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060046F3 RID: 18163
		[__DynamicallyInvokable]
		public abstract Type ReflectedType
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a collection that contains this member's custom attributes.</summary>
		/// <returns>A collection that contains this member's custom attributes.</returns>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00104748 File Offset: 0x00102948
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		/// <summary>When overridden in a derived class, returns an array of all custom attributes applied to this member.</summary>
		/// <param name="inherit">
		///   <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />. This parameter is ignored for properties and events.</param>
		/// <returns>An array that contains all the custom attributes applied to this member, or an array with zero elements if no attributes are defined.</returns>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded.</exception>
		// Token: 0x060046F5 RID: 18165
		[__DynamicallyInvokable]
		public abstract object[] GetCustomAttributes(bool inherit);

		/// <summary>When overridden in a derived class, returns an array of custom attributes applied to this member and identified by <see cref="T:System.Type" />.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">
		///   <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />. This parameter is ignored for properties and events.</param>
		/// <returns>An array of custom attributes applied to this member, or an array with zero elements if no attributes assignable to <paramref name="attributeType" /> have been applied.</returns>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		// Token: 0x060046F6 RID: 18166
		[__DynamicallyInvokable]
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>When overridden in a derived class, indicates whether one or more attributes of the specified type or of its derived types is applied to this member.</summary>
		/// <param name="attributeType">The type of custom attribute to search for. The search includes derived types.</param>
		/// <param name="inherit">
		///   <see langword="true" /> to search this member's inheritance chain to find the attributes; otherwise, <see langword="false" />. This parameter is ignored for properties and events.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" /> or any of its derived types is applied to this member; otherwise, <see langword="false" />.</returns>
		// Token: 0x060046F7 RID: 18167
		[__DynamicallyInvokable]
		public abstract bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</returns>
		// Token: 0x060046F8 RID: 18168 RVA: 0x00104750 File Offset: 0x00102950
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that identifies a metadata element.</summary>
		/// <returns>A value which, in combination with <see cref="P:System.Reflection.MemberInfo.Module" />, uniquely identifies a metadata element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Reflection.MemberInfo" /> represents an array method, such as <see langword="Address" />, on an array type whose element type is a dynamic type that has not been completed. To get a metadata token in this case, pass the <see cref="T:System.Reflection.MemberInfo" /> object to the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetMethodToken(System.Reflection.MethodInfo)" /> method; or use the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethodToken(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" /> method to get the token directly, instead of using the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethod(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" /> method to get a <see cref="T:System.Reflection.MethodInfo" /> first.</exception>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x00104757 File Offset: 0x00102957
		public virtual int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets the module in which the type that declares the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is defined.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x0010475E File Offset: 0x0010295E
		[__DynamicallyInvokable]
		public virtual Module Module
		{
			[__DynamicallyInvokable]
			get
			{
				if (this is Type)
				{
					return ((Type)this).Module;
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MemberInfo" /> objects are equal.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060046FB RID: 18171 RVA: 0x0010477C File Offset: 0x0010297C
		[__DynamicallyInvokable]
		public static bool operator ==(MemberInfo left, MemberInfo right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			Type type;
			Type type2;
			if ((type = left as Type) != null && (type2 = right as Type) != null)
			{
				return type == type2;
			}
			MethodBase methodBase;
			MethodBase methodBase2;
			if ((methodBase = left as MethodBase) != null && (methodBase2 = right as MethodBase) != null)
			{
				return methodBase == methodBase2;
			}
			FieldInfo fieldInfo;
			FieldInfo fieldInfo2;
			if ((fieldInfo = left as FieldInfo) != null && (fieldInfo2 = right as FieldInfo) != null)
			{
				return fieldInfo == fieldInfo2;
			}
			EventInfo eventInfo;
			EventInfo eventInfo2;
			if ((eventInfo = left as EventInfo) != null && (eventInfo2 = right as EventInfo) != null)
			{
				return eventInfo == eventInfo2;
			}
			PropertyInfo propertyInfo;
			PropertyInfo propertyInfo2;
			return (propertyInfo = left as PropertyInfo) != null && (propertyInfo2 = right as PropertyInfo) != null && propertyInfo == propertyInfo2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MemberInfo" /> objects are not equal.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060046FC RID: 18172 RVA: 0x0010486C File Offset: 0x00102A6C
		[__DynamicallyInvokable]
		public static bool operator !=(MemberInfo left, MemberInfo right)
		{
			return !(left == right);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060046FD RID: 18173 RVA: 0x00104878 File Offset: 0x00102A78
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060046FE RID: 18174 RVA: 0x00104881 File Offset: 0x00102A81
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.MemberInfo" /> class.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.MemberInfo" /> class.</returns>
		// Token: 0x060046FF RID: 18175 RVA: 0x00104889 File Offset: 0x00102A89
		Type _MemberInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004700 RID: 18176 RVA: 0x00104891 File Offset: 0x00102A91
		void _MemberInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004701 RID: 18177 RVA: 0x00104898 File Offset: 0x00102A98
		void _MemberInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x06004702 RID: 18178 RVA: 0x0010489F File Offset: 0x00102A9F
		void _MemberInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x06004703 RID: 18179 RVA: 0x001048A6 File Offset: 0x00102AA6
		void _MemberInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
