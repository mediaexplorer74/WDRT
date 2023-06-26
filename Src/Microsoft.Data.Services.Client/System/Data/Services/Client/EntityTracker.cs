using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq;
using System.Threading;

namespace System.Data.Services.Client
{
	// Token: 0x02000062 RID: 98
	internal class EntityTracker : EntityTrackerBase
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000E4FA File Offset: 0x0000C6FA
		public EntityTracker(ClientEdmModel maxProtocolVersion)
		{
			this.model = maxProtocolVersion;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000E519 File Offset: 0x0000C719
		public IEnumerable<LinkDescriptor> Links
		{
			get
			{
				this.EnsureLinkBindings();
				return this.bindings.Values;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000E52C File Offset: 0x0000C72C
		public IEnumerable<EntityDescriptor> Entities
		{
			get
			{
				return this.entityDescriptors.Values;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E53C File Offset: 0x0000C73C
		internal EntityDescriptor TryGetEntityDescriptor(object entity)
		{
			EntityDescriptor entityDescriptor = null;
			this.entityDescriptors.TryGetValue(entity, out entityDescriptor);
			return entityDescriptor;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E55C File Offset: 0x0000C75C
		internal override EntityDescriptor GetEntityDescriptor(object resource)
		{
			EntityDescriptor entityDescriptor = this.TryGetEntityDescriptor(resource);
			if (entityDescriptor == null)
			{
				throw Error.InvalidOperation(Strings.Context_EntityNotContained);
			}
			return entityDescriptor;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E580 File Offset: 0x0000C780
		internal EntityDescriptor TryGetEntityDescriptor(string identity)
		{
			EntityDescriptor entityDescriptor;
			if (this.identityToDescriptor != null && this.identityToDescriptor.TryGetValue(identity, out entityDescriptor))
			{
				return entityDescriptor;
			}
			return null;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		internal void AddEntityDescriptor(EntityDescriptor descriptor)
		{
			try
			{
				this.entityDescriptors.Add(descriptor.Entity, descriptor);
			}
			catch (ArgumentException)
			{
				throw Error.InvalidOperation(Strings.Context_EntityAlreadyContained);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		internal bool DetachResource(EntityDescriptor resource)
		{
			this.EnsureLinkBindings();
			foreach (LinkDescriptor linkDescriptor in this.bindings.Values.Where(new Func<LinkDescriptor, bool>(resource.IsRelatedEntity)).ToList<LinkDescriptor>())
			{
				this.DetachExistingLink(linkDescriptor, linkDescriptor.Target == resource.Entity && resource.State == EntityStates.Added);
			}
			resource.ChangeOrder = uint.MaxValue;
			resource.State = EntityStates.Detached;
			this.entityDescriptors.Remove(resource.Entity);
			this.DetachResourceIdentity(resource);
			return true;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		internal void DetachResourceIdentity(EntityDescriptor resource)
		{
			EntityDescriptor entityDescriptor = null;
			if (resource.Identity != null && this.identityToDescriptor.TryGetValue(resource.Identity, out entityDescriptor) && object.ReferenceEquals(entityDescriptor, resource))
			{
				this.identityToDescriptor.Remove(resource.Identity);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		internal LinkDescriptor TryGetLinkDescriptor(object source, string sourceProperty, object target)
		{
			this.EnsureLinkBindings();
			LinkDescriptor linkDescriptor;
			this.bindings.TryGetValue(new LinkDescriptor(source, sourceProperty, target, this.model), out linkDescriptor);
			return linkDescriptor;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E718 File Offset: 0x0000C918
		internal override void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			LinkDescriptor linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
			LinkDescriptor linkDescriptor2 = this.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor2 != null)
			{
				switch (linkMerge)
				{
				case MergeOption.OverwriteChanges:
					linkDescriptor = linkDescriptor2;
					break;
				case MergeOption.PreserveChanges:
					if (EntityStates.Added == linkDescriptor2.State || EntityStates.Unchanged == linkDescriptor2.State || (EntityStates.Modified == linkDescriptor2.State && linkDescriptor2.Target != null))
					{
						linkDescriptor = linkDescriptor2;
					}
					break;
				case MergeOption.NoTracking:
					throw Error.InvalidOperation(Strings.Context_RelationAlreadyContained);
				}
			}
			else if (this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType())).GetProperty(sourceProperty, false).IsEntityCollection || (linkDescriptor2 = this.DetachReferenceLink(source, sourceProperty, target, linkMerge)) == null)
			{
				this.AddLink(linkDescriptor);
				this.IncrementChange(linkDescriptor);
			}
			else if (linkMerge != MergeOption.AppendOnly && (MergeOption.PreserveChanges != linkMerge || EntityStates.Modified != linkDescriptor2.State))
			{
				linkDescriptor = linkDescriptor2;
			}
			linkDescriptor.State = EntityStates.Unchanged;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E800 File Offset: 0x0000CA00
		internal LinkDescriptor DetachReferenceLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			LinkDescriptor linkDescriptor = this.GetLinks(source, sourceProperty).FirstOrDefault<LinkDescriptor>();
			if (linkDescriptor != null)
			{
				if (target == linkDescriptor.Target || linkMerge == MergeOption.AppendOnly || (MergeOption.PreserveChanges == linkMerge && EntityStates.Modified == linkDescriptor.State))
				{
					return linkDescriptor;
				}
				this.DetachExistingLink(linkDescriptor, false);
			}
			return null;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000E848 File Offset: 0x0000CA48
		internal void AddLink(LinkDescriptor linkDescriptor)
		{
			try
			{
				this.EnsureLinkBindings();
				this.bindings.Add(linkDescriptor, linkDescriptor);
			}
			catch (ArgumentException)
			{
				throw Error.InvalidOperation(Strings.Context_RelationAlreadyContained);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E888 File Offset: 0x0000CA88
		internal bool TryRemoveLinkDescriptor(LinkDescriptor linkDescriptor)
		{
			this.EnsureLinkBindings();
			return this.bindings.Remove(linkDescriptor);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		internal override IEnumerable<LinkDescriptor> GetLinks(object source, string sourceProperty)
		{
			this.EnsureLinkBindings();
			return this.bindings.Values.Where((LinkDescriptor o) => o.Source == source && o.SourceProperty == sourceProperty);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000E90C File Offset: 0x0000CB0C
		internal override void DetachExistingLink(LinkDescriptor existingLink, bool targetDelete)
		{
			if (existingLink.Target != null)
			{
				EntityDescriptor entityDescriptor = this.GetEntityDescriptor(existingLink.Target);
				if (entityDescriptor.IsDeepInsert && !targetDelete)
				{
					EntityDescriptor parentForInsert = entityDescriptor.ParentForInsert;
					if (object.ReferenceEquals(entityDescriptor.ParentEntity, existingLink.Source) && (parentForInsert.State != EntityStates.Deleted || parentForInsert.State != EntityStates.Detached))
					{
						throw new InvalidOperationException(Strings.Context_ChildResourceExists);
					}
				}
			}
			if (this.TryRemoveLinkDescriptor(existingLink))
			{
				existingLink.State = EntityStates.Detached;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000E980 File Offset: 0x0000CB80
		internal override void AttachIdentity(EntityDescriptor entityDescriptorFromMaterializer, MergeOption metadataMergeOption)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor = this.entityDescriptors[entityDescriptorFromMaterializer.Entity];
			this.ValidateDuplicateIdentity(entityDescriptorFromMaterializer.Identity, entityDescriptor);
			this.DetachResourceIdentity(entityDescriptor);
			if (entityDescriptor.IsDeepInsert)
			{
				LinkDescriptor linkDescriptor = this.bindings[entityDescriptor.GetRelatedEnd()];
				linkDescriptor.State = EntityStates.Unchanged;
			}
			entityDescriptor.Identity = entityDescriptorFromMaterializer.Identity;
			AtomMaterializerLog.MergeEntityDescriptorInfo(entityDescriptor, entityDescriptorFromMaterializer, true, metadataMergeOption);
			entityDescriptor.State = EntityStates.Unchanged;
			this.identityToDescriptor[entityDescriptorFromMaterializer.Identity] = entityDescriptor;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000EA08 File Offset: 0x0000CC08
		internal void AttachLocation(object entity, string identity, Uri editLink)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor = this.entityDescriptors[entity];
			this.ValidateDuplicateIdentity(identity, entityDescriptor);
			this.DetachResourceIdentity(entityDescriptor);
			if (entityDescriptor.IsDeepInsert)
			{
				LinkDescriptor linkDescriptor = this.bindings[entityDescriptor.GetRelatedEnd()];
				linkDescriptor.State = EntityStates.Unchanged;
			}
			entityDescriptor.Identity = identity;
			entityDescriptor.EditLink = editLink;
			this.identityToDescriptor[identity] = entityDescriptor;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000EA74 File Offset: 0x0000CC74
		internal override EntityDescriptor InternalAttachEntityDescriptor(EntityDescriptor entityDescriptorFromMaterializer, bool failIfDuplicated)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor;
			this.entityDescriptors.TryGetValue(entityDescriptorFromMaterializer.Entity, out entityDescriptor);
			EntityDescriptor entityDescriptor2;
			this.identityToDescriptor.TryGetValue(entityDescriptorFromMaterializer.Identity, out entityDescriptor2);
			if (failIfDuplicated && entityDescriptor != null)
			{
				throw Error.InvalidOperation(Strings.Context_EntityAlreadyContained);
			}
			if (entityDescriptor != entityDescriptor2)
			{
				throw Error.InvalidOperation(Strings.Context_DifferentEntityAlreadyContained);
			}
			if (entityDescriptor == null)
			{
				entityDescriptor = entityDescriptorFromMaterializer;
				this.IncrementChange(entityDescriptorFromMaterializer);
				this.entityDescriptors.Add(entityDescriptorFromMaterializer.Entity, entityDescriptorFromMaterializer);
				this.identityToDescriptor.Add(entityDescriptorFromMaterializer.Identity, entityDescriptorFromMaterializer);
			}
			return entityDescriptor;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000EB00 File Offset: 0x0000CD00
		internal override object TryGetEntity(string resourceUri, out EntityStates state)
		{
			state = EntityStates.Detached;
			EntityDescriptor entityDescriptor = null;
			if (this.identityToDescriptor != null && this.identityToDescriptor.TryGetValue(resourceUri, out entityDescriptor))
			{
				state = entityDescriptor.State;
				return entityDescriptor.Entity;
			}
			return null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		internal void IncrementChange(Descriptor descriptor)
		{
			descriptor.ChangeOrder = (this.nextChange += 1U);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EB60 File Offset: 0x0000CD60
		private void EnsureIdentityToResource()
		{
			if (this.identityToDescriptor == null)
			{
				Interlocked.CompareExchange<Dictionary<string, EntityDescriptor>>(ref this.identityToDescriptor, new Dictionary<string, EntityDescriptor>(StringComparer.Ordinal), null);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000EB81 File Offset: 0x0000CD81
		private void EnsureLinkBindings()
		{
			if (this.bindings == null)
			{
				Interlocked.CompareExchange<Dictionary<LinkDescriptor, LinkDescriptor>>(ref this.bindings, new Dictionary<LinkDescriptor, LinkDescriptor>(LinkDescriptor.EquivalenceComparer), null);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		private void ValidateDuplicateIdentity(string identity, EntityDescriptor descriptor)
		{
			EntityDescriptor entityDescriptor;
			if (this.identityToDescriptor.TryGetValue(identity, out entityDescriptor) && descriptor != entityDescriptor && entityDescriptor.State != EntityStates.Deleted && entityDescriptor.State != EntityStates.Detached)
			{
				throw Error.InvalidOperation(Strings.Context_DifferentEntityAlreadyContained);
			}
		}

		// Token: 0x0400028A RID: 650
		private readonly ClientEdmModel model;

		// Token: 0x0400028B RID: 651
		private Dictionary<object, EntityDescriptor> entityDescriptors = new Dictionary<object, EntityDescriptor>(EqualityComparer<object>.Default);

		// Token: 0x0400028C RID: 652
		private Dictionary<string, EntityDescriptor> identityToDescriptor;

		// Token: 0x0400028D RID: 653
		private Dictionary<LinkDescriptor, LinkDescriptor> bindings;

		// Token: 0x0400028E RID: 654
		private uint nextChange;
	}
}
