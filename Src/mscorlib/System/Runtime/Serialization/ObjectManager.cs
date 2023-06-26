using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Serialization
{
	/// <summary>Keeps track of objects as they are deserialized.</summary>
	// Token: 0x02000749 RID: 1865
	[ComVisible(true)]
	public class ObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ObjectManager" /> class.</summary>
		/// <param name="selector">The surrogate selector to use. The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> determines the correct surrogate to use when deserializing objects of a given type. At deserialization time, the surrogate selector creates a new instance of the object from the information transmitted on the stream.</param>
		/// <param name="context">The streaming context. The <see cref="T:System.Runtime.Serialization.StreamingContext" /> is not used by <see langword="ObjectManager" />, but is passed as a parameter to any objects implementing <see cref="T:System.Runtime.Serialization.ISerializable" /> or having a <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />. These objects can take specific actions depending on the source of the information to deserialize.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600527B RID: 21115 RVA: 0x00122CB2 File Offset: 0x00120EB2
		[SecuritySafeCritical]
		public ObjectManager(ISurrogateSelector selector, StreamingContext context)
			: this(selector, context, true, false)
		{
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00122CC0 File Offset: 0x00120EC0
		[SecurityCritical]
		internal ObjectManager(ISurrogateSelector selector, StreamingContext context, bool checkSecurity, bool isCrossAppDomain)
		{
			if (checkSecurity)
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
			this.m_objects = new ObjectHolder[16];
			this.m_selector = selector;
			this.m_context = context;
			this.m_isCrossAppDomain = isCrossAppDomain;
			if (!isCrossAppDomain && AppContextSwitches.UseNewMaxArraySize)
			{
				this.MaxArraySize = 16777216;
			}
			else
			{
				this.MaxArraySize = 4096;
			}
			this.ArrayMask = this.MaxArraySize - 1;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00122D30 File Offset: 0x00120F30
		[SecurityCritical]
		private bool CanCallGetType(object obj)
		{
			return !RemotingServices.IsTransparentProxy(obj);
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x00122D46 File Offset: 0x00120F46
		// (set) Token: 0x0600527E RID: 21118 RVA: 0x00122D3D File Offset: 0x00120F3D
		internal object TopObject
		{
			get
			{
				return this.m_topObject;
			}
			set
			{
				this.m_topObject = value;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x00122D4E File Offset: 0x00120F4E
		internal ObjectHolderList SpecialFixupObjects
		{
			get
			{
				if (this.m_specialFixupObjects == null)
				{
					this.m_specialFixupObjects = new ObjectHolderList();
				}
				return this.m_specialFixupObjects;
			}
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00122D80 File Offset: 0x00120F80
		internal ObjectHolder FindObjectHolder(long objectID)
		{
			int num = (int)(objectID & (long)this.ArrayMask);
			if (num >= this.m_objects.Length)
			{
				return null;
			}
			ObjectHolder objectHolder;
			for (objectHolder = this.m_objects[num]; objectHolder != null; objectHolder = objectHolder.m_next)
			{
				if (objectHolder.m_id == objectID)
				{
					return objectHolder;
				}
			}
			return objectHolder;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00122DC8 File Offset: 0x00120FC8
		internal ObjectHolder FindOrCreateObjectHolder(long objectID)
		{
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null)
			{
				objectHolder = new ObjectHolder(objectID);
				this.AddObjectHolder(objectHolder);
			}
			return objectHolder;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00122DF0 File Offset: 0x00120FF0
		private void AddObjectHolder(ObjectHolder holder)
		{
			if (holder.m_id >= (long)this.m_objects.Length && this.m_objects.Length != this.MaxArraySize)
			{
				int num = this.MaxArraySize;
				if (holder.m_id < (long)(this.MaxArraySize / 2))
				{
					num = this.m_objects.Length * 2;
					while ((long)num <= holder.m_id && num < this.MaxArraySize)
					{
						num *= 2;
					}
					if (num > this.MaxArraySize)
					{
						num = this.MaxArraySize;
					}
				}
				ObjectHolder[] array = new ObjectHolder[num];
				Array.Copy(this.m_objects, array, this.m_objects.Length);
				this.m_objects = array;
			}
			int num2 = (int)(holder.m_id & (long)this.ArrayMask);
			ObjectHolder objectHolder = this.m_objects[num2];
			holder.m_next = objectHolder;
			this.m_objects[num2] = holder;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00122EB8 File Offset: 0x001210B8
		private bool GetCompletionInfo(FixupHolder fixup, out ObjectHolder holder, out object member, bool bThrowIfMissing)
		{
			member = fixup.m_fixupInfo;
			holder = this.FindObjectHolder(fixup.m_id);
			if (!holder.CompletelyFixed && holder.ObjectValue != null && holder.ObjectValue is ValueType)
			{
				this.SpecialFixupObjects.Add(holder);
				return false;
			}
			if (holder != null && !holder.CanObjectValueChange && holder.ObjectValue != null)
			{
				return true;
			}
			if (!bThrowIfMissing)
			{
				return false;
			}
			if (holder == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NeverSeen", new object[] { fixup.m_id }));
			}
			if (holder.IsIncompleteObjectReference)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_IORIncomplete", new object[] { fixup.m_id }));
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", new object[] { fixup.m_id }));
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00122FA0 File Offset: 0x001211A0
		[SecurityCritical]
		private void FixupSpecialObject(ObjectHolder holder)
		{
			ISurrogateSelector surrogateSelector = null;
			if (holder.HasSurrogate)
			{
				ISerializationSurrogate surrogate = holder.Surrogate;
				object obj = surrogate.SetObjectData(holder.ObjectValue, holder.SerializationInfo, this.m_context, surrogateSelector);
				if (obj != null)
				{
					if (!holder.CanSurrogatedObjectValueChange && obj != holder.ObjectValue)
					{
						throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_NotCyclicallyReferenceableSurrogate"), surrogate.GetType().FullName));
					}
					holder.SetObjectValue(obj, this);
				}
				holder.m_surrogate = null;
				holder.SetFlags();
			}
			else
			{
				this.CompleteISerializableObject(holder.ObjectValue, holder.SerializationInfo, this.m_context);
			}
			holder.SerializationInfo = null;
			holder.RequiresSerInfoFixup = false;
			if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
			{
				this.DoValueTypeFixup(null, holder, holder.ObjectValue);
			}
			this.DoNewlyRegisteredObjectFixups(holder);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00123074 File Offset: 0x00121274
		[SecurityCritical]
		private bool ResolveObjectReference(ObjectHolder holder)
		{
			int num = 0;
			try
			{
				object objectValue;
				for (;;)
				{
					objectValue = holder.ObjectValue;
					holder.SetObjectValue(((IObjectReference)holder.ObjectValue).GetRealObject(this.m_context), this);
					if (holder.ObjectValue == null)
					{
						break;
					}
					if (num++ == 100)
					{
						goto Block_3;
					}
					if (!(holder.ObjectValue is IObjectReference) || objectValue == holder.ObjectValue)
					{
						goto IL_69;
					}
				}
				holder.SetObjectValue(objectValue, this);
				return false;
				Block_3:
				throw new SerializationException(Environment.GetResourceString("Serialization_TooManyReferences"));
				IL_69:;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			holder.IsIncompleteObjectReference = false;
			this.DoNewlyRegisteredObjectFixups(holder);
			return true;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00123114 File Offset: 0x00121314
		[SecurityCritical]
		private bool DoValueTypeFixup(FieldInfo memberToFix, ObjectHolder holder, object value)
		{
			FieldInfo[] array = new FieldInfo[4];
			int num = 0;
			int[] array2 = null;
			object obj = holder.ObjectValue;
			while (holder.RequiresValueTypeFixup)
			{
				if (num + 1 >= array.Length)
				{
					FieldInfo[] array3 = new FieldInfo[array.Length * 2];
					Array.Copy(array, array3, array.Length);
					array = array3;
				}
				ValueTypeFixupInfo valueFixup = holder.ValueFixup;
				obj = holder.ObjectValue;
				if (valueFixup.ParentField != null)
				{
					FieldInfo parentField = valueFixup.ParentField;
					ObjectHolder objectHolder = this.FindObjectHolder(valueFixup.ContainerID);
					if (objectHolder.ObjectValue == null)
					{
						break;
					}
					if (Nullable.GetUnderlyingType(parentField.FieldType) != null)
					{
						array[num] = parentField.FieldType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
						num++;
					}
					array[num] = parentField;
					holder = objectHolder;
					num++;
				}
				else
				{
					holder = this.FindObjectHolder(valueFixup.ContainerID);
					array2 = valueFixup.ParentIndex;
					if (holder.ObjectValue == null)
					{
						break;
					}
					break;
				}
			}
			if (!(holder.ObjectValue is Array) && holder.ObjectValue != null)
			{
				obj = holder.ObjectValue;
			}
			if (num != 0)
			{
				FieldInfo[] array4 = new FieldInfo[num];
				for (int i = 0; i < num; i++)
				{
					FieldInfo fieldInfo = array[num - 1 - i];
					SerializationFieldInfo serializationFieldInfo = fieldInfo as SerializationFieldInfo;
					array4[i] = ((serializationFieldInfo == null) ? fieldInfo : serializationFieldInfo.FieldInfo);
				}
				TypedReference typedReference = TypedReference.MakeTypedReference(obj, array4);
				if (memberToFix != null)
				{
					((RuntimeFieldInfo)memberToFix).SetValueDirect(typedReference, value);
				}
				else
				{
					TypedReference.SetTypedReference(typedReference, value);
				}
			}
			else if (memberToFix != null)
			{
				FormatterServices.SerializationSetValue(memberToFix, obj, value);
			}
			if (array2 != null && holder.ObjectValue != null)
			{
				((Array)holder.ObjectValue).SetValue(obj, array2);
			}
			return true;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x001232D0 File Offset: 0x001214D0
		[Conditional("SER_LOGGING")]
		private void DumpValueTypeFixup(object obj, FieldInfo[] intermediateFields, FieldInfo memberToFix, object value)
		{
			StringBuilder stringBuilder = new StringBuilder("  " + ((obj != null) ? obj.ToString() : null));
			if (intermediateFields != null)
			{
				for (int i = 0; i < intermediateFields.Length; i++)
				{
					stringBuilder.Append("." + intermediateFields[i].Name);
				}
			}
			stringBuilder.Append("." + memberToFix.Name + "=" + ((value != null) ? value.ToString() : null));
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00123350 File Offset: 0x00121550
		[SecurityCritical]
		internal void CompleteObject(ObjectHolder holder, bool bObjectFullyComplete)
		{
			FixupHolderList missingElements = holder.m_missingElements;
			object obj = null;
			ObjectHolder objectHolder = null;
			int num = 0;
			if (holder.ObjectValue == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissingObject", new object[] { holder.m_id }));
			}
			if (missingElements == null)
			{
				return;
			}
			if (holder.HasSurrogate || holder.HasISerializable)
			{
				SerializationInfo serInfo = holder.m_serInfo;
				if (serInfo == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupDiscovered"));
				}
				if (missingElements != null)
				{
					for (int i = 0; i < missingElements.m_count; i++)
					{
						if (missingElements.m_values[i] != null && this.GetCompletionInfo(missingElements.m_values[i], out objectHolder, out obj, bObjectFullyComplete))
						{
							object objectValue = objectHolder.ObjectValue;
							if (this.CanCallGetType(objectValue))
							{
								serInfo.UpdateValue((string)obj, objectValue, objectValue.GetType());
							}
							else
							{
								serInfo.UpdateValue((string)obj, objectValue, typeof(MarshalByRefObject));
							}
							num++;
							missingElements.m_values[i] = null;
							if (!bObjectFullyComplete)
							{
								holder.DecrementFixupsRemaining(this);
								objectHolder.RemoveDependency(holder.m_id);
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < missingElements.m_count; j++)
				{
					FixupHolder fixupHolder = missingElements.m_values[j];
					if (fixupHolder != null && this.GetCompletionInfo(fixupHolder, out objectHolder, out obj, bObjectFullyComplete))
					{
						if (objectHolder.TypeLoadExceptionReachable)
						{
							holder.TypeLoadException = objectHolder.TypeLoadException;
							if (holder.Reachable)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", new object[] { holder.TypeLoadException.TypeName }));
							}
						}
						if (holder.Reachable)
						{
							objectHolder.Reachable = true;
						}
						int fixupType = fixupHolder.m_fixupType;
						if (fixupType != 1)
						{
							if (fixupType != 2)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
							}
							MemberInfo memberInfo = (MemberInfo)obj;
							if (memberInfo.MemberType != MemberTypes.Field)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
							}
							if (holder.RequiresValueTypeFixup && holder.ValueTypeFixupPerformed)
							{
								if (!this.DoValueTypeFixup((FieldInfo)memberInfo, holder, objectHolder.ObjectValue))
								{
									throw new SerializationException(Environment.GetResourceString("Serialization_PartialValueTypeFixup"));
								}
							}
							else
							{
								FormatterServices.SerializationSetValue(memberInfo, holder.ObjectValue, objectHolder.ObjectValue);
							}
							if (objectHolder.RequiresValueTypeFixup)
							{
								objectHolder.ValueTypeFixupPerformed = true;
							}
						}
						else
						{
							if (holder.RequiresValueTypeFixup)
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_ValueTypeFixup"));
							}
							((Array)holder.ObjectValue).SetValue(objectHolder.ObjectValue, (int[])obj);
						}
						num++;
						missingElements.m_values[j] = null;
						if (!bObjectFullyComplete)
						{
							holder.DecrementFixupsRemaining(this);
							objectHolder.RemoveDependency(holder.m_id);
						}
					}
				}
			}
			this.m_fixupCount -= (long)num;
			if (missingElements.m_count == num)
			{
				holder.m_missingElements = null;
			}
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00123630 File Offset: 0x00121830
		[SecurityCritical]
		private void DoNewlyRegisteredObjectFixups(ObjectHolder holder)
		{
			if (holder.CanObjectValueChange)
			{
				return;
			}
			LongList dependentObjects = holder.DependentObjects;
			if (dependentObjects == null)
			{
				return;
			}
			dependentObjects.StartEnumeration();
			while (dependentObjects.MoveNext())
			{
				long num = dependentObjects.Current;
				ObjectHolder objectHolder = this.FindObjectHolder(num);
				objectHolder.DecrementFixupsRemaining(this);
				if (objectHolder.DirectlyDependentObjects == 0)
				{
					if (objectHolder.ObjectValue != null)
					{
						this.CompleteObject(objectHolder, true);
					}
					else
					{
						objectHolder.MarkForCompletionWhenAvailable();
					}
				}
			}
		}

		/// <summary>Returns the object with the specified object ID.</summary>
		/// <param name="objectID">The ID of the requested object.</param>
		/// <returns>The object with the specified object ID if it has been previously stored or <see langword="null" /> if no such object has been registered.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectID" /> parameter is less than or equal to zero.</exception>
		// Token: 0x0600528C RID: 21132 RVA: 0x00123698 File Offset: 0x00121898
		public virtual object GetObject(long objectID)
		{
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
			}
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null || objectHolder.CanObjectValueChange)
			{
				return null;
			}
			return objectHolder.ObjectValue;
		}

		/// <summary>Registers an object as it is deserialized, associating it with <paramref name="objectID" />.</summary>
		/// <param name="obj">The object to register.</param>
		/// <param name="objectID">The ID of the object to register.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectID" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="objectID" /> has already been registered for an object other than <paramref name="obj" />.</exception>
		// Token: 0x0600528D RID: 21133 RVA: 0x001236DA File Offset: 0x001218DA
		[SecurityCritical]
		public virtual void RegisterObject(object obj, long objectID)
		{
			this.RegisterObject(obj, objectID, null, 0L, null);
		}

		/// <summary>Registers an object as it is deserialized, associating it with <paramref name="objectID" />, and recording the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used with it.</summary>
		/// <param name="obj">The object to register.</param>
		/// <param name="objectID">The ID of the object to register.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used if <paramref name="obj" /> implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or has a <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />. <paramref name="info" /> will be completed with any required fixup information and then passed to the required object when that object is completed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectID" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="objectID" /> has already been registered for an object other than <paramref name="obj" />.</exception>
		// Token: 0x0600528E RID: 21134 RVA: 0x001236E8 File Offset: 0x001218E8
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info)
		{
			this.RegisterObject(obj, objectID, info, 0L, null);
		}

		/// <summary>Registers a member of an object as it is deserialized, associating it with <paramref name="objectID" />, and recording the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to register.</param>
		/// <param name="objectID">The ID of the object to register.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used if <paramref name="obj" /> implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or has a <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />. <paramref name="info" /> will be completed with any required fixup information and then passed to the required object when that object is completed.</param>
		/// <param name="idOfContainingObj">The ID of the object that contains <paramref name="obj" />. This parameter is required only if <paramref name="obj" /> is a value type.</param>
		/// <param name="member">The field in the containing object where <paramref name="obj" /> exists. This parameter has meaning only if <paramref name="obj" /> is a value type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectID" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="objectID" /> has already been registered for an object other than <paramref name="obj" />, or <paramref name="member" /> is not a <see cref="T:System.Reflection.FieldInfo" /> and <paramref name="member" /> is not <see langword="null" />.</exception>
		// Token: 0x0600528F RID: 21135 RVA: 0x001236F6 File Offset: 0x001218F6
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
		{
			this.RegisterObject(obj, objectID, info, idOfContainingObj, member, null);
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00123708 File Offset: 0x00121908
		internal void RegisterString(string obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member)
		{
			ObjectHolder objectHolder = new ObjectHolder(obj, objectID, info, null, idOfContainingObj, (FieldInfo)member, null);
			this.AddObjectHolder(objectHolder);
		}

		/// <summary>Registers a member of an array contained in an object while it is deserialized, associating it with <paramref name="objectID" />, and recording the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to register.</param>
		/// <param name="objectID">The ID of the object to register.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used if <paramref name="obj" /> implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or has a <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />. <paramref name="info" /> will be completed with any required fixup information and then passed to the required object when that object is completed.</param>
		/// <param name="idOfContainingObj">The ID of the object that contains <paramref name="obj" />. This parameter is required only if <paramref name="obj" /> is a value type.</param>
		/// <param name="member">The field in the containing object where <paramref name="obj" /> exists. This parameter has meaning only if <paramref name="obj" /> is a value type.</param>
		/// <param name="arrayIndex">If <paramref name="obj" /> is a <see cref="T:System.ValueType" /> and a member of an array, <paramref name="arrayIndex" /> contains the index within that array where <paramref name="obj" /> exists. <paramref name="arrayIndex" /> is ignored if <paramref name="obj" /> is not both a <see cref="T:System.ValueType" /> and a member of an array.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectID" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="objectID" /> has already been registered for an object other than <paramref name="obj" />, or <paramref name="member" /> is not a <see cref="T:System.Reflection.FieldInfo" /> and <paramref name="member" /> isn't <see langword="null" />.</exception>
		// Token: 0x06005291 RID: 21137 RVA: 0x00123730 File Offset: 0x00121930
		[SecurityCritical]
		public void RegisterObject(object obj, long objectID, SerializationInfo info, long idOfContainingObj, MemberInfo member, int[] arrayIndex)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (objectID <= 0L)
			{
				throw new ArgumentOutOfRangeException("objectID", Environment.GetResourceString("ArgumentOutOfRange_ObjectID"));
			}
			if (member != null && !(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
			}
			ISerializationSurrogate serializationSurrogate = null;
			if (this.m_selector != null)
			{
				Type type;
				if (this.CanCallGetType(obj))
				{
					type = obj.GetType();
				}
				else
				{
					type = typeof(MarshalByRefObject);
				}
				ISurrogateSelector surrogateSelector;
				serializationSurrogate = this.m_selector.GetSurrogate(type, this.m_context, out surrogateSelector);
			}
			if (obj is IDeserializationCallback)
			{
				DeserializationEventHandler deserializationEventHandler = new DeserializationEventHandler(((IDeserializationCallback)obj).OnDeserialization);
				this.AddOnDeserialization(deserializationEventHandler);
			}
			if (arrayIndex != null)
			{
				arrayIndex = (int[])arrayIndex.Clone();
			}
			ObjectHolder objectHolder = this.FindObjectHolder(objectID);
			if (objectHolder == null)
			{
				objectHolder = new ObjectHolder(obj, objectID, info, serializationSurrogate, idOfContainingObj, (FieldInfo)member, arrayIndex);
				this.AddObjectHolder(objectHolder);
				if (objectHolder.RequiresDelayedFixup)
				{
					this.SpecialFixupObjects.Add(objectHolder);
				}
				this.AddOnDeserialized(obj);
				return;
			}
			if (objectHolder.ObjectValue != null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_RegisterTwice"));
			}
			objectHolder.UpdateData(obj, info, serializationSurrogate, idOfContainingObj, (FieldInfo)member, arrayIndex, this);
			if (objectHolder.DirectlyDependentObjects > 0)
			{
				this.CompleteObject(objectHolder, false);
			}
			if (objectHolder.RequiresDelayedFixup)
			{
				this.SpecialFixupObjects.Add(objectHolder);
			}
			if (objectHolder.CompletelyFixed)
			{
				this.DoNewlyRegisteredObjectFixups(objectHolder);
				objectHolder.DependentObjects = null;
			}
			if (objectHolder.TotalDependentObjects > 0)
			{
				this.AddOnDeserialized(obj);
				return;
			}
			this.RaiseOnDeserializedEvent(obj);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x001238C8 File Offset: 0x00121AC8
		[SecurityCritical]
		internal void CompleteISerializableObject(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!(obj is ISerializable))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_NotISer"));
			}
			RuntimeConstructorInfo runtimeConstructorInfo = null;
			RuntimeType runtimeType = (RuntimeType)obj.GetType();
			try
			{
				if (runtimeType == ObjectManager.TypeOfWindowsIdentity && this.m_isCrossAppDomain)
				{
					runtimeConstructorInfo = WindowsIdentity.GetSpecialSerializationCtor();
				}
				else
				{
					runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType);
				}
			}
			catch (Exception ex)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", new object[] { runtimeType }), ex);
			}
			runtimeConstructorInfo.SerializationInvoke(obj, info, context);
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00123964 File Offset: 0x00121B64
		internal static RuntimeConstructorInfo GetConstructor(RuntimeType t)
		{
			RuntimeConstructorInfo serializationCtor = t.GetSerializationCtor();
			if (serializationCtor == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ConstructorNotFound", new object[] { t.FullName }));
			}
			return serializationCtor;
		}

		/// <summary>Performs all the recorded fixups.</summary>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A fixup was not successfully completed.</exception>
		// Token: 0x06005294 RID: 21140 RVA: 0x001239A4 File Offset: 0x00121BA4
		[SecuritySafeCritical]
		public virtual void DoFixups()
		{
			int num = -1;
			while (num != 0)
			{
				num = 0;
				ObjectHolderListEnumerator fixupEnumerator = this.SpecialFixupObjects.GetFixupEnumerator();
				while (fixupEnumerator.MoveNext())
				{
					ObjectHolder objectHolder = fixupEnumerator.Current;
					if (objectHolder.ObjectValue == null)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_ObjectNotSupplied", new object[] { objectHolder.m_id }));
					}
					if (objectHolder.TotalDependentObjects == 0)
					{
						if (objectHolder.RequiresSerInfoFixup)
						{
							this.FixupSpecialObject(objectHolder);
							num++;
						}
						else if (!objectHolder.IsIncompleteObjectReference)
						{
							this.CompleteObject(objectHolder, true);
						}
						if (objectHolder.IsIncompleteObjectReference && this.ResolveObjectReference(objectHolder))
						{
							num++;
						}
					}
				}
			}
			if (this.m_fixupCount != 0L)
			{
				for (int i = 0; i < this.m_objects.Length; i++)
				{
					for (ObjectHolder objectHolder = this.m_objects[i]; objectHolder != null; objectHolder = objectHolder.m_next)
					{
						if (objectHolder.TotalDependentObjects > 0)
						{
							this.CompleteObject(objectHolder, true);
						}
					}
					if (this.m_fixupCount == 0L)
					{
						return;
					}
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_IncorrectNumberOfFixups"));
			}
			if (this.TopObject is TypeLoadExceptionHolder)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_TypeLoadFailure", new object[] { ((TypeLoadExceptionHolder)this.TopObject).TypeName }));
			}
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x00123ADC File Offset: 0x00121CDC
		private void RegisterFixup(FixupHolder fixup, long objectToBeFixed, long objectRequired)
		{
			ObjectHolder objectHolder = this.FindOrCreateObjectHolder(objectToBeFixed);
			if (objectHolder.RequiresSerInfoFixup && fixup.m_fixupType == 2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFixupType"));
			}
			objectHolder.AddFixup(fixup, this);
			ObjectHolder objectHolder2 = this.FindOrCreateObjectHolder(objectRequired);
			objectHolder2.AddDependency(objectToBeFixed);
			this.m_fixupCount += 1L;
		}

		/// <summary>Records a fixup for a member of an object, to be executed later.</summary>
		/// <param name="objectToBeFixed">The ID of the object that needs the reference to the <paramref name="objectRequired" /> object.</param>
		/// <param name="member">The member of <paramref name="objectToBeFixed" /> where the fixup will be performed.</param>
		/// <param name="objectRequired">The ID of the object required by <paramref name="objectToBeFixed" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="objectToBeFixed" /> or <paramref name="objectRequired" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="member" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005296 RID: 21142 RVA: 0x00123B38 File Offset: 0x00121D38
		public virtual void RecordFixup(long objectToBeFixed, MemberInfo member, long objectRequired)
		{
			if (objectToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((objectToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (!(member is RuntimeFieldInfo) && !(member is SerializationFieldInfo))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[] { member.GetType().ToString() }));
			}
			FixupHolder fixupHolder = new FixupHolder(objectRequired, member, 2);
			this.RegisterFixup(fixupHolder, objectToBeFixed, objectRequired);
		}

		/// <summary>Records a fixup for an object member, to be executed later.</summary>
		/// <param name="objectToBeFixed">The ID of the object that needs the reference to <paramref name="objectRequired" />.</param>
		/// <param name="memberName">The member name of <paramref name="objectToBeFixed" /> where the fixup will be performed.</param>
		/// <param name="objectRequired">The ID of the object required by <paramref name="objectToBeFixed" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="objectToBeFixed" /> or <paramref name="objectRequired" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="memberName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005297 RID: 21143 RVA: 0x00123BCC File Offset: 0x00121DCC
		public virtual void RecordDelayedFixup(long objectToBeFixed, string memberName, long objectRequired)
		{
			if (objectToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((objectToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (memberName == null)
			{
				throw new ArgumentNullException("memberName");
			}
			FixupHolder fixupHolder = new FixupHolder(objectRequired, memberName, 4);
			this.RegisterFixup(fixupHolder, objectToBeFixed, objectRequired);
		}

		/// <summary>Records a fixup for one element in an array.</summary>
		/// <param name="arrayToBeFixed">The ID of the array used to record a fixup.</param>
		/// <param name="index">The index within arrayFixup that a fixup is requested for.</param>
		/// <param name="objectRequired">The ID of the object that the current array element will point to after fixup is completed.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="arrayToBeFixed" /> or <paramref name="objectRequired" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="index" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005298 RID: 21144 RVA: 0x00123C24 File Offset: 0x00121E24
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int index, long objectRequired)
		{
			this.RecordArrayElementFixup(arrayToBeFixed, new int[] { index }, objectRequired);
		}

		/// <summary>Records fixups for the specified elements in an array, to be executed later.</summary>
		/// <param name="arrayToBeFixed">The ID of the array used to record a fixup.</param>
		/// <param name="indices">The indexes within the multidimensional array that a fixup is requested for.</param>
		/// <param name="objectRequired">The ID of the object the array elements will point to after fixup is completed.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="arrayToBeFixed" /> or <paramref name="objectRequired" /> parameter is less than or equal to zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="indices" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06005299 RID: 21145 RVA: 0x00123C48 File Offset: 0x00121E48
		public virtual void RecordArrayElementFixup(long arrayToBeFixed, int[] indices, long objectRequired)
		{
			if (arrayToBeFixed <= 0L || objectRequired <= 0L)
			{
				throw new ArgumentOutOfRangeException((arrayToBeFixed <= 0L) ? "objectToBeFixed" : "objectRequired", Environment.GetResourceString("Serialization_IdTooSmall"));
			}
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			FixupHolder fixupHolder = new FixupHolder(objectRequired, indices, 1);
			this.RegisterFixup(fixupHolder, arrayToBeFixed, objectRequired);
		}

		/// <summary>Raises the deserialization event to any registered object that implements <see cref="T:System.Runtime.Serialization.IDeserializationCallback" />.</summary>
		// Token: 0x0600529A RID: 21146 RVA: 0x00123CA0 File Offset: 0x00121EA0
		public virtual void RaiseDeserializationEvent()
		{
			if (this.m_onDeserializedHandler != null)
			{
				this.m_onDeserializedHandler(this.m_context);
			}
			if (this.m_onDeserializationHandler != null)
			{
				this.m_onDeserializationHandler(null);
			}
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x00123CCF File Offset: 0x00121ECF
		internal virtual void AddOnDeserialization(DeserializationEventHandler handler)
		{
			this.m_onDeserializationHandler = (DeserializationEventHandler)Delegate.Combine(this.m_onDeserializationHandler, handler);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x00123CE8 File Offset: 0x00121EE8
		internal virtual void RemoveOnDeserialization(DeserializationEventHandler handler)
		{
			this.m_onDeserializationHandler = (DeserializationEventHandler)Delegate.Remove(this.m_onDeserializationHandler, handler);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00123D04 File Offset: 0x00121F04
		[SecuritySafeCritical]
		internal virtual void AddOnDeserialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onDeserializedHandler = serializationEventsForType.AddOnDeserialized(obj, this.m_onDeserializedHandler);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00123D30 File Offset: 0x00121F30
		internal virtual void RaiseOnDeserializedEvent(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			serializationEventsForType.InvokeOnDeserialized(obj, this.m_context);
		}

		/// <summary>Invokes the method marked with the <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" />.</summary>
		/// <param name="obj">The instance of the type that contains the method to be invoked.</param>
		// Token: 0x0600529F RID: 21151 RVA: 0x00123D58 File Offset: 0x00121F58
		public void RaiseOnDeserializingEvent(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			serializationEventsForType.InvokeOnDeserializing(obj, this.m_context);
		}

		// Token: 0x04002482 RID: 9346
		private const int DefaultInitialSize = 16;

		// Token: 0x04002483 RID: 9347
		private const int DefaultMaxArraySize = 4096;

		// Token: 0x04002484 RID: 9348
		private const int NewMaxArraySize = 16777216;

		// Token: 0x04002485 RID: 9349
		private const int MaxReferenceDepth = 100;

		// Token: 0x04002486 RID: 9350
		private DeserializationEventHandler m_onDeserializationHandler;

		// Token: 0x04002487 RID: 9351
		private SerializationEventHandler m_onDeserializedHandler;

		// Token: 0x04002488 RID: 9352
		private static RuntimeType TypeOfWindowsIdentity = (RuntimeType)typeof(WindowsIdentity);

		// Token: 0x04002489 RID: 9353
		internal ObjectHolder[] m_objects;

		// Token: 0x0400248A RID: 9354
		internal object m_topObject;

		// Token: 0x0400248B RID: 9355
		internal ObjectHolderList m_specialFixupObjects;

		// Token: 0x0400248C RID: 9356
		internal long m_fixupCount;

		// Token: 0x0400248D RID: 9357
		internal ISurrogateSelector m_selector;

		// Token: 0x0400248E RID: 9358
		internal StreamingContext m_context;

		// Token: 0x0400248F RID: 9359
		private bool m_isCrossAppDomain;

		// Token: 0x04002490 RID: 9360
		private readonly int MaxArraySize;

		// Token: 0x04002491 RID: 9361
		private readonly int ArrayMask;
	}
}
