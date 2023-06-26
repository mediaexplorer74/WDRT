using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074A RID: 1866
	internal sealed class ObjectHolder
	{
		// Token: 0x060052A0 RID: 21152 RVA: 0x00123D7E File Offset: 0x00121F7E
		internal ObjectHolder(long objID)
			: this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00123D90 File Offset: 0x00121F90
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00123E4C File Offset: 0x0012204C
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00123ED5 File Offset: 0x001220D5
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00123EE5 File Offset: 0x001220E5
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00123F05 File Offset: 0x00122105
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00123F14 File Offset: 0x00122114
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00123F54 File Offset: 0x00122154
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00123F7F File Offset: 0x0012217F
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00123FA0 File Offset: 0x001221A0
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ParentChildIdentical"));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0012402B File Offset: 0x0012222B
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00124034 File Offset: 0x00122234
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x001240B4 File Offset: 0x001222B4
		// (set) Token: 0x060052AD RID: 21165 RVA: 0x001240C1 File Offset: 0x001222C1
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x001240E4 File Offset: 0x001222E4
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x001240F1 File Offset: 0x001222F1
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x001240FE File Offset: 0x001222FE
		// (set) Token: 0x060052B1 RID: 21169 RVA: 0x00124132 File Offset: 0x00122332
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x00124149 File Offset: 0x00122349
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x00124156 File Offset: 0x00122356
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x00124163 File Offset: 0x00122363
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x00124189 File Offset: 0x00122389
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x001241A5 File Offset: 0x001223A5
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060052B7 RID: 21175 RVA: 0x001241AD File Offset: 0x001223AD
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x001241BC File Offset: 0x001223BC
		// (set) Token: 0x060052B9 RID: 21177 RVA: 0x001241C4 File Offset: 0x001223C4
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x001241CD File Offset: 0x001223CD
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x001241D8 File Offset: 0x001223D8
		// (set) Token: 0x060052BC RID: 21180 RVA: 0x001241E0 File Offset: 0x001223E0
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x001241E9 File Offset: 0x001223E9
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x001241F1 File Offset: 0x001223F1
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x0012422E File Offset: 0x0012242E
		// (set) Token: 0x060052C0 RID: 21184 RVA: 0x00124236 File Offset: 0x00122436
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0012423F File Offset: 0x0012243F
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060052C2 RID: 21186 RVA: 0x00124247 File Offset: 0x00122447
		// (set) Token: 0x060052C3 RID: 21187 RVA: 0x0012424F File Offset: 0x0012244F
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x00124258 File Offset: 0x00122458
		// (set) Token: 0x060052C5 RID: 21189 RVA: 0x0012427F File Offset: 0x0012247F
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x001242A9 File Offset: 0x001224A9
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x001242B1 File Offset: 0x001224B1
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060052C8 RID: 21192 RVA: 0x001242C6 File Offset: 0x001224C6
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x04002492 RID: 9362
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x04002493 RID: 9363
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x04002494 RID: 9364
		internal const int HAS_SURROGATE = 4;

		// Token: 0x04002495 RID: 9365
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x04002496 RID: 9366
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x04002497 RID: 9367
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x04002498 RID: 9368
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x04002499 RID: 9369
		private object m_object;

		// Token: 0x0400249A RID: 9370
		internal long m_id;

		// Token: 0x0400249B RID: 9371
		private int m_missingElementsRemaining;

		// Token: 0x0400249C RID: 9372
		private int m_missingDecendents;

		// Token: 0x0400249D RID: 9373
		internal SerializationInfo m_serInfo;

		// Token: 0x0400249E RID: 9374
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x0400249F RID: 9375
		internal FixupHolderList m_missingElements;

		// Token: 0x040024A0 RID: 9376
		internal LongList m_dependentObjects;

		// Token: 0x040024A1 RID: 9377
		internal ObjectHolder m_next;

		// Token: 0x040024A2 RID: 9378
		internal int m_flags;

		// Token: 0x040024A3 RID: 9379
		private bool m_markForFixupWhenAvailable;

		// Token: 0x040024A4 RID: 9380
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x040024A5 RID: 9381
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x040024A6 RID: 9382
		private bool m_reachable;
	}
}
