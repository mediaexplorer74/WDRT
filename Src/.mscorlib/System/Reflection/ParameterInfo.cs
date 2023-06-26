using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a parameter and provides access to parameter metadata.</summary>
	// Token: 0x02000614 RID: 1556
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ParameterInfo : _ParameterInfo, ICustomAttributeProvider, IObjectReference
	{
		/// <summary>Initializes a new instance of the <see langword="ParameterInfo" /> class.</summary>
		// Token: 0x06004828 RID: 18472 RVA: 0x001078AE File Offset: 0x00105AAE
		protected ParameterInfo()
		{
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x001078B6 File Offset: 0x00105AB6
		internal void SetName(string name)
		{
			this.NameImpl = name;
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x001078BF File Offset: 0x00105ABF
		internal void SetAttributes(ParameterAttributes attributes)
		{
			this.AttrsImpl = attributes;
		}

		/// <summary>Gets the <see langword="Type" /> of this parameter.</summary>
		/// <returns>The <see langword="Type" /> object that represents the <see langword="Type" /> of this parameter.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x001078C8 File Offset: 0x00105AC8
		[__DynamicallyInvokable]
		public virtual Type ParameterType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClassImpl;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>The simple name of this parameter.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x001078D0 File Offset: 0x00105AD0
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.NameImpl;
			}
		}

		/// <summary>Gets a value that indicates whether this parameter has a default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this parameter has a default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x001078D8 File Offset: 0x00105AD8
		[__DynamicallyInvokable]
		public virtual bool HasDefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x001078DF File Offset: 0x00105ADF
		[__DynamicallyInvokable]
		public virtual object DefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x001078E6 File Offset: 0x00105AE6
		public virtual object RawDefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the zero-based position of the parameter in the formal parameter list.</summary>
		/// <returns>An integer representing the position this parameter occupies in the parameter list.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x001078ED File Offset: 0x00105AED
		[__DynamicallyInvokable]
		public virtual int Position
		{
			[__DynamicallyInvokable]
			get
			{
				return this.PositionImpl;
			}
		}

		/// <summary>Gets the attributes for this parameter.</summary>
		/// <returns>A <see langword="ParameterAttributes" /> object representing the attributes for this parameter.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x001078F5 File Offset: 0x00105AF5
		[__DynamicallyInvokable]
		public virtual ParameterAttributes Attributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.AttrsImpl;
			}
		}

		/// <summary>Gets a value indicating the member in which the parameter is implemented.</summary>
		/// <returns>The member which implanted the parameter represented by this <see cref="T:System.Reflection.ParameterInfo" />.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x001078FD File Offset: 0x00105AFD
		[__DynamicallyInvokable]
		public virtual MemberInfo Member
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberImpl;
			}
		}

		/// <summary>Gets a value indicating whether this is an input parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is an input parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x00107905 File Offset: 0x00105B05
		[__DynamicallyInvokable]
		public bool IsIn
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is an output parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is an output parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00107912 File Offset: 0x00105B12
		[__DynamicallyInvokable]
		public bool IsOut
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is a locale identifier (lcid).</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is a locale identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x0010791F File Offset: 0x00105B1F
		[__DynamicallyInvokable]
		public bool IsLcid
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is a <see langword="Retval" /> parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is a <see langword="Retval" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x0010792C File Offset: 0x00105B2C
		[__DynamicallyInvokable]
		public bool IsRetval
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is optional.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is optional; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x00107939 File Offset: 0x00105B39
		[__DynamicallyInvokable]
		public bool IsOptional
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value that identifies this parameter in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this parameter in metadata.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00107948 File Offset: 0x00105B48
		public virtual int MetadataToken
		{
			get
			{
				RuntimeParameterInfo runtimeParameterInfo = this as RuntimeParameterInfo;
				if (runtimeParameterInfo != null)
				{
					return runtimeParameterInfo.MetadataToken;
				}
				return 134217728;
			}
		}

		/// <summary>Gets the required custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06004839 RID: 18489 RVA: 0x0010796B File Offset: 0x00105B6B
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		/// <summary>Gets the optional custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x0600483A RID: 18490 RVA: 0x00107972 File Offset: 0x00105B72
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		/// <summary>Gets the parameter type and name represented as a string.</summary>
		/// <returns>A string containing the type and the name of the parameter.</returns>
		// Token: 0x0600483B RID: 18491 RVA: 0x00107979 File Offset: 0x00105B79
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		/// <summary>Gets a collection that contains this parameter's custom attributes.</summary>
		/// <returns>A collection that contains this parameter's custom attributes.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600483C RID: 18492 RVA: 0x00107996 File Offset: 0x00105B96
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		/// <summary>Gets all the custom attributes defined on this parameter.</summary>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains all the custom attributes applied to this parameter.</returns>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded.</exception>
		// Token: 0x0600483D RID: 18493 RVA: 0x0010799E File Offset: 0x00105B9E
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return EmptyArray<object>.Value;
		}

		/// <summary>Gets the custom attributes of the specified type or its derived types that are applied to this parameter.</summary>
		/// <param name="attributeType">The custom attributes identified by type.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains the custom attributes of the specified type or its derived types.</returns>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded.</exception>
		// Token: 0x0600483E RID: 18494 RVA: 0x001079A5 File Offset: 0x00105BA5
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return EmptyArray<object>.Value;
		}

		/// <summary>Determines whether the custom attribute of the specified type or its derived types is applied to this parameter.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to search for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" /> or its derived types are applied to this parameter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the common language runtime.</exception>
		// Token: 0x0600483F RID: 18495 RVA: 0x001079C0 File Offset: 0x00105BC0
		[__DynamicallyInvokable]
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects for the current parameter, which can be used in the reflection-only context.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current parameter.</returns>
		// Token: 0x06004840 RID: 18496 RVA: 0x001079D7 File Offset: 0x00105BD7
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004841 RID: 18497 RVA: 0x001079DE File Offset: 0x00105BDE
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004842 RID: 18498 RVA: 0x001079E5 File Offset: 0x00105BE5
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x06004843 RID: 18499 RVA: 0x001079EC File Offset: 0x00105BEC
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x06004844 RID: 18500 RVA: 0x001079F3 File Offset: 0x00105BF3
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the real object that should be deserialized instead of the object that the serialized stream specifies.</summary>
		/// <param name="context">The serialized stream from which the current object is deserialized.</param>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The parameter's position in the parameter list of its associated member is not valid for that member's type.</exception>
		// Token: 0x06004845 RID: 18501 RVA: 0x001079FC File Offset: 0x00105BFC
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_NoParameterInfo"));
				}
				ParameterInfo[] array = ((RuntimePropertyInfo)this.MemberImpl).GetIndexParametersNoCopy();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
		}

		/// <summary>The name of the parameter.</summary>
		// Token: 0x04001DF4 RID: 7668
		protected string NameImpl;

		/// <summary>The <see langword="Type" /> of the parameter.</summary>
		// Token: 0x04001DF5 RID: 7669
		protected Type ClassImpl;

		/// <summary>The zero-based position of the parameter in the parameter list.</summary>
		// Token: 0x04001DF6 RID: 7670
		protected int PositionImpl;

		/// <summary>The attributes of the parameter.</summary>
		// Token: 0x04001DF7 RID: 7671
		protected ParameterAttributes AttrsImpl;

		/// <summary>The default value of the parameter.</summary>
		// Token: 0x04001DF8 RID: 7672
		protected object DefaultValueImpl;

		/// <summary>The member in which the field is implemented.</summary>
		// Token: 0x04001DF9 RID: 7673
		protected MemberInfo MemberImpl;

		// Token: 0x04001DFA RID: 7674
		[OptionalField]
		private IntPtr _importer;

		// Token: 0x04001DFB RID: 7675
		[OptionalField]
		private int _token;

		// Token: 0x04001DFC RID: 7676
		[OptionalField]
		private bool bExtraConstChecked;
	}
}
