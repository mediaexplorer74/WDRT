using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200022A RID: 554
	internal sealed class ODataAtomReader : ODataReaderCore
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x000412FC File Offset: 0x0003F4FC
		internal ODataAtomReader(ODataAtomInputContext atomInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed)
			: base(atomInputContext, readingFeed, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomEntryAndFeedDeserializer = new ODataAtomEntryAndFeedDeserializer(atomInputContext);
			if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
			{
				this.atomInputContext.InitializeReaderCustomization();
				this.atomEntryAndFeedDeserializersStack = new Stack<ODataAtomEntryAndFeedDeserializer>();
				this.atomEntryAndFeedDeserializersStack.Push(this.atomEntryAndFeedDeserializer);
			}
			base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Start, null, entitySet, expectedEntityType));
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0004136E File Offset: 0x0003F56E
		private IODataAtomReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataAtomReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004137B File Offset: 0x0003F57B
		private IODataAtomReaderFeedState CurrentFeedState
		{
			get
			{
				return (IODataAtomReaderFeedState)base.CurrentScope;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00041388 File Offset: 0x0003F588
		protected override bool ReadAtStartImplementation()
		{
			this.atomEntryAndFeedDeserializer.ReadPayloadStart();
			if (base.ReadingFeed)
			{
				this.ReadFeedStart();
				return true;
			}
			this.ReadEntryStart();
			return true;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000413AC File Offset: 0x0003F5AC
		protected override bool ReadAtFeedStartImplementation()
		{
			if (this.atomEntryAndFeedDeserializer.XmlReader.NodeType == XmlNodeType.EndElement || this.CurrentFeedState.FeedElementEmpty)
			{
				this.ReplaceScopeToFeedEnd();
			}
			else
			{
				this.ReadEntryStart();
			}
			return true;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000413E0 File Offset: 0x0003F5E0
		protected override bool ReadAtFeedEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool flag = this.atomEntryAndFeedDeserializer.IsReaderOnInlineEndElement();
			if (!flag)
			{
				this.atomEntryAndFeedDeserializer.ReadFeedEnd();
			}
			base.PopScope(ODataReaderState.FeedEnd);
			bool flag2;
			if (isTopLevel)
			{
				this.atomEntryAndFeedDeserializer.ReadPayloadEnd();
				this.ReplaceScope(ODataReaderState.Completed);
				flag2 = false;
			}
			else
			{
				this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentAfterExpansion(flag);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00041448 File Offset: 0x0003F648
		protected override bool ReadAtEntryStartImplementation()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.atomEntryAndFeedDeserializer.XmlReader.NodeType == XmlNodeType.EndElement || this.CurrentEntryState.EntryElementEmpty)
			{
				this.EndEntry();
			}
			else if (this.atomInputContext.UseServerApiBehavior)
			{
				ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
				if (odataAtomReaderNavigationLinkDescriptor == null)
				{
					this.EndEntry();
				}
				else
				{
					this.StartNavigationLink(odataAtomReaderNavigationLinkDescriptor);
				}
			}
			else
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLinkDescriptor);
			}
			return true;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000414D4 File Offset: 0x0003F6D4
		protected override bool ReadAtEntryEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			bool flag = base.CurrentEntry == null;
			base.PopScope(ODataReaderState.EntryEnd);
			if (!flag)
			{
				bool flag2 = false;
				if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
				{
					XmlReader xmlReader = this.atomInputContext.PopCustomReader();
					if (!object.ReferenceEquals(this.atomInputContext.XmlReader, xmlReader))
					{
						flag2 = true;
						this.atomEntryAndFeedDeserializersStack.Pop();
						this.atomEntryAndFeedDeserializer = this.atomEntryAndFeedDeserializersStack.Peek();
					}
				}
				if (!flag2)
				{
					this.atomEntryAndFeedDeserializer.ReadEntryEnd();
				}
			}
			bool flag3 = true;
			if (isTopLevel)
			{
				this.atomEntryAndFeedDeserializer.ReadPayloadEnd();
				this.ReplaceScope(ODataReaderState.Completed);
				flag3 = false;
			}
			else if (isExpandedLinkContent)
			{
				this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentAfterExpansion(flag);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			else if (this.atomEntryAndFeedDeserializer.ReadFeedContent(this.CurrentFeedState, base.IsExpandedLinkContent))
			{
				this.ReadEntryStart();
			}
			else
			{
				this.ReplaceScopeToFeedEnd();
			}
			return flag3;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000415C4 File Offset: 0x0003F7C4
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataAtomReaderEntryState iodataAtomReaderEntryState = (IODataAtomReaderEntryState)base.LinkParentEntityScope;
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			IEdmNavigationProperty navigationProperty = atomScope.NavigationProperty;
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.ReadAtNonExpandedNavigationLinkStart();
			}
			else
			{
				this.atomEntryAndFeedDeserializer.XmlReader.Read();
				ODataAtomDeserializerExpandedNavigationLinkContent odataAtomDeserializerExpandedNavigationLinkContent = this.atomEntryAndFeedDeserializer.ReadNavigationLinkContentBeforeExpansion();
				if (odataAtomDeserializerExpandedNavigationLinkContent != ODataAtomDeserializerExpandedNavigationLinkContent.None && navigationProperty == null && this.atomInputContext.Model.IsUserModel() && this.atomInputContext.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					if (this.atomInputContext.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
					{
						this.atomEntryAndFeedDeserializer.SkipNavigationLinkContentOnExpansion();
						this.ReadAtNonExpandedNavigationLinkStart();
						return true;
					}
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(currentNavigationLink.Name, base.LinkParentEntityScope.EntityType.ODataFullName()));
				}
				else
				{
					switch (odataAtomDeserializerExpandedNavigationLinkContent)
					{
					case ODataAtomDeserializerExpandedNavigationLinkContent.None:
						this.ReadAtNonExpandedNavigationLinkStart();
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Empty:
						if (currentNavigationLink.IsCollection == true)
						{
							ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
							this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
							this.CurrentFeedState.FeedElementEmpty = true;
						}
						else
						{
							currentNavigationLink.IsCollection = new bool?(false);
							ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
							this.EnterScope(ODataReaderState.EntryStart, null, base.CurrentEntityType);
						}
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Entry:
						if (currentNavigationLink.IsCollection == true || (navigationProperty != null && navigationProperty.Type.IsCollection()))
						{
							throw new ODataException(Strings.ODataAtomReader_ExpandedEntryInFeedNavigationLink);
						}
						currentNavigationLink.IsCollection = new bool?(false);
						ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
						this.ReadEntryStart();
						break;
					case ODataAtomDeserializerExpandedNavigationLinkContent.Feed:
						if (currentNavigationLink.IsCollection == false)
						{
							throw new ODataException(Strings.ODataAtomReader_ExpandedFeedInEntryNavigationLink);
						}
						currentNavigationLink.IsCollection = new bool?(true);
						ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
						this.ReadFeedStart();
						break;
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomReader_ReadAtNavigationLinkStartImplementation));
					}
				}
			}
			return true;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00041818 File Offset: 0x0003FA18
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			this.atomEntryAndFeedDeserializer.ReadNavigationLinkEnd();
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
			if (odataAtomReaderNavigationLinkDescriptor == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataAtomReaderNavigationLinkDescriptor);
			}
			return true;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0004185C File Offset: 0x0003FA5C
		protected override bool ReadAtEntityReferenceLink()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			return true;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00041870 File Offset: 0x0003FA70
		private void ReadFeedStart()
		{
			ODataFeed odataFeed = new ODataFeed();
			this.atomEntryAndFeedDeserializer.ReadFeedStart();
			this.EnterScope(ODataReaderState.FeedStart, odataFeed, base.CurrentEntityType);
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.CurrentFeedState.FeedElementEmpty = true;
				return;
			}
			this.atomEntryAndFeedDeserializer.XmlReader.Read();
			this.atomEntryAndFeedDeserializer.ReadFeedContent(this.CurrentFeedState, base.IsExpandedLinkContent);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000418E4 File Offset: 0x0003FAE4
		private void ReadEntryStart()
		{
			ODataEntry odataEntry = ReaderUtils.CreateNewEntry();
			if (this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback != null)
			{
				this.atomEntryAndFeedDeserializer.VerifyEntryStart();
				Uri xmlBaseUri = this.atomInputContext.XmlReader.XmlBaseUri;
				XmlReader xmlReader = this.atomInputContext.MessageReaderSettings.AtomEntryXmlCustomizationCallback(odataEntry, this.atomInputContext.XmlReader, this.atomInputContext.XmlReader.ParentXmlBaseUri);
				if (xmlReader != null)
				{
					if (object.ReferenceEquals(this.atomInputContext.XmlReader, xmlReader))
					{
						throw new ODataException(Strings.ODataAtomReader_EntryXmlCustomizationCallbackReturnedSameInstance);
					}
					this.atomInputContext.PushCustomReader(xmlReader, xmlBaseUri);
					this.atomEntryAndFeedDeserializer = new ODataAtomEntryAndFeedDeserializer(this.atomInputContext);
					this.atomEntryAndFeedDeserializersStack.Push(this.atomEntryAndFeedDeserializer);
				}
				else
				{
					this.atomInputContext.PushCustomReader(this.atomInputContext.XmlReader, null);
				}
			}
			this.atomEntryAndFeedDeserializer.ReadEntryStart(odataEntry);
			this.EnterScope(ODataReaderState.EntryStart, odataEntry, base.CurrentEntityType);
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			atomScope.DuplicatePropertyNamesChecker = this.atomInputContext.CreateDuplicatePropertyNamesChecker();
			string text = this.atomEntryAndFeedDeserializer.FindTypeName();
			base.ApplyEntityTypeNameFromPayload(text);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = this.atomInputContext.Model.EnsureEpmCache(this.CurrentEntryState.EntityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				atomScope.CachedEpm = odataEntityPropertyMappingCache;
			}
			if (this.atomEntryAndFeedDeserializer.XmlReader.IsEmptyElement)
			{
				this.CurrentEntryState.EntryElementEmpty = true;
				return;
			}
			this.atomEntryAndFeedDeserializer.XmlReader.Read();
			if (this.atomInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLinkDescriptor = null;
				return;
			}
			this.CurrentEntryState.FirstNavigationLinkDescriptor = this.atomEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00041ABC File Offset: 0x0003FCBC
		private void EndEntry()
		{
			IODataAtomReaderEntryState currentEntryState = this.CurrentEntryState;
			ODataEntry entry = currentEntryState.Entry;
			if (entry != null)
			{
				if (currentEntryState.CachedEpm != null)
				{
					ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
					if (atomScope.HasAtomEntryMetadata)
					{
						EpmSyndicationReader.ReadEntryEpm(currentEntryState, this.atomInputContext);
					}
					if (atomScope.HasEpmCustomReaderValueCache)
					{
						EpmCustomReader.ReadEntryEpm(currentEntryState, this.atomInputContext);
					}
				}
				if (currentEntryState.AtomEntryMetadata != null)
				{
					entry.SetAnnotation<AtomEntryMetadata>(currentEntryState.AtomEntryMetadata);
				}
				IEdmEntityType entityType = currentEntryState.EntityType;
				if (currentEntryState.MediaLinkEntry == null && entityType != null && this.atomInputContext.Model.HasDefaultStream(entityType))
				{
					ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(currentEntryState, true);
				}
				bool flag = this.atomInputContext.UseDefaultFormatBehavior || this.atomInputContext.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.atomInputContext.Model, flag);
			}
			base.EndEntry(new ODataAtomReader.AtomScope(ODataReaderState.EntryEnd, this.Item, base.CurrentEntityType));
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00041BAC File Offset: 0x0003FDAC
		private void StartNavigationLink(ODataAtomReaderNavigationLinkDescriptor navigationLinkDescriptor)
		{
			IEdmEntityType edmEntityType = null;
			if (navigationLinkDescriptor.NavigationProperty != null)
			{
				IEdmTypeReference type = navigationLinkDescriptor.NavigationProperty.Type;
				if (!type.IsCollection())
				{
					if (navigationLinkDescriptor.NavigationLink.IsCollection == true)
					{
						throw new ODataException(Strings.ODataAtomReader_FeedNavigationLinkForResourceReferenceProperty(navigationLinkDescriptor.NavigationLink.Name));
					}
					navigationLinkDescriptor.NavigationLink.IsCollection = new bool?(false);
					edmEntityType = type.AsEntity().EntityDefinition();
				}
				else
				{
					if (navigationLinkDescriptor.NavigationLink.IsCollection == null)
					{
						navigationLinkDescriptor.NavigationLink.IsCollection = new bool?(true);
					}
					edmEntityType = type.AsCollection().ElementType().AsEntity()
						.EntityDefinition();
				}
			}
			this.EnterScope(ODataReaderState.NavigationLinkStart, navigationLinkDescriptor.NavigationLink, edmEntityType);
			((ODataAtomReader.AtomScope)base.CurrentScope).NavigationProperty = navigationLinkDescriptor.NavigationProperty;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00041C90 File Offset: 0x0003FE90
		private void ReadAtNonExpandedNavigationLinkStart()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataAtomReaderEntryState iodataAtomReaderEntryState = (IODataAtomReaderEntryState)base.LinkParentEntityScope;
			ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataAtomReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
			if (!this.atomInputContext.ReadingResponse)
			{
				this.EnterScope(ODataReaderState.EntityReferenceLink, new ODataEntityReferenceLink
				{
					Url = currentNavigationLink.Url
				}, null);
				return;
			}
			ODataAtomReader.AtomScope atomScope = (ODataAtomReader.AtomScope)base.CurrentScope;
			IEdmNavigationProperty navigationProperty = atomScope.NavigationProperty;
			if (currentNavigationLink.IsCollection == false && navigationProperty != null && navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataAtomReader_DeferredEntryInFeedNavigationLink);
			}
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00041D41 File Offset: 0x0003FF41
		private void EnterScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
		{
			base.EnterScope(new ODataAtomReader.AtomScope(state, item, expectedEntityType));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00041D51 File Offset: 0x0003FF51
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataAtomReader.AtomScope(state, this.Item, base.CurrentEntityType));
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00041D6C File Offset: 0x0003FF6C
		private void ReplaceScopeToFeedEnd()
		{
			IODataAtomReaderFeedState currentFeedState = this.CurrentFeedState;
			ODataFeed currentFeed = base.CurrentFeed;
			if (this.atomInputContext.MessageReaderSettings.EnableAtomMetadataReading)
			{
				currentFeed.SetAnnotation<AtomFeedMetadata>(currentFeedState.AtomFeedMetadata);
			}
			this.ReplaceScope(ODataReaderState.FeedEnd);
		}

		// Token: 0x04000662 RID: 1634
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x04000663 RID: 1635
		private ODataAtomEntryAndFeedDeserializer atomEntryAndFeedDeserializer;

		// Token: 0x04000664 RID: 1636
		private Stack<ODataAtomEntryAndFeedDeserializer> atomEntryAndFeedDeserializersStack;

		// Token: 0x0200022B RID: 555
		private sealed class AtomScope : ODataReaderCore.Scope, IODataAtomReaderEntryState, IODataAtomReaderFeedState
		{
			// Token: 0x06001181 RID: 4481 RVA: 0x00041DAC File Offset: 0x0003FFAC
			internal AtomScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
				: base(state, item, null, expectedEntityType)
			{
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x06001182 RID: 4482 RVA: 0x00041DB8 File Offset: 0x0003FFB8
			// (set) Token: 0x06001183 RID: 4483 RVA: 0x00041DC5 File Offset: 0x0003FFC5
			public bool ElementEmpty
			{
				get
				{
					return (this.atomScopeState & ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement) == ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
				}
				set
				{
					if (value)
					{
						this.atomScopeState |= ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
						return;
					}
					this.atomScopeState &= ~ODataAtomReader.AtomScope.AtomScopeStateBitMask.EmptyElement;
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06001184 RID: 4484 RVA: 0x00041DE8 File Offset: 0x0003FFE8
			// (set) Token: 0x06001185 RID: 4485 RVA: 0x00041DF0 File Offset: 0x0003FFF0
			public bool? MediaLinkEntry
			{
				get
				{
					return this.mediaLinkEntry;
				}
				set
				{
					if (this.mediaLinkEntry != null && this.mediaLinkEntry.Value != value)
					{
						throw new ODataException(Strings.ODataAtomReader_MediaLinkEntryMismatch);
					}
					this.mediaLinkEntry = value;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06001186 RID: 4486 RVA: 0x00041E43 File Offset: 0x00040043
			// (set) Token: 0x06001187 RID: 4487 RVA: 0x00041E4B File Offset: 0x0004004B
			public ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06001188 RID: 4488 RVA: 0x00041E54 File Offset: 0x00040054
			// (set) Token: 0x06001189 RID: 4489 RVA: 0x00041E5C File Offset: 0x0004005C
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x0600118A RID: 4490 RVA: 0x00041E65 File Offset: 0x00040065
			// (set) Token: 0x0600118B RID: 4491 RVA: 0x00041E6D File Offset: 0x0004006D
			public ODataEntityPropertyMappingCache CachedEpm { get; set; }

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x0600118C RID: 4492 RVA: 0x00041E76 File Offset: 0x00040076
			public bool HasEpmCustomReaderValueCache
			{
				get
				{
					return this.epmCustomReaderValueCache != null;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x0600118D RID: 4493 RVA: 0x00041E84 File Offset: 0x00040084
			public bool HasAtomEntryMetadata
			{
				get
				{
					return this.atomEntryMetadata != null;
				}
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x0600118E RID: 4494 RVA: 0x00041E92 File Offset: 0x00040092
			// (set) Token: 0x0600118F RID: 4495 RVA: 0x00041E9A File Offset: 0x0004009A
			public IEdmNavigationProperty NavigationProperty { get; set; }

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x06001190 RID: 4496 RVA: 0x00041EA3 File Offset: 0x000400A3
			ODataEntry IODataAtomReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06001191 RID: 4497 RVA: 0x00041EB0 File Offset: 0x000400B0
			IEdmEntityType IODataAtomReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06001192 RID: 4498 RVA: 0x00041EB8 File Offset: 0x000400B8
			// (set) Token: 0x06001193 RID: 4499 RVA: 0x00041EC0 File Offset: 0x000400C0
			bool IODataAtomReaderEntryState.EntryElementEmpty
			{
				get
				{
					return this.ElementEmpty;
				}
				set
				{
					this.ElementEmpty = value;
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06001194 RID: 4500 RVA: 0x00041EC9 File Offset: 0x000400C9
			// (set) Token: 0x06001195 RID: 4501 RVA: 0x00041ED2 File Offset: 0x000400D2
			bool IODataAtomReaderEntryState.HasReadLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLink);
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06001196 RID: 4502 RVA: 0x00041EDC File Offset: 0x000400DC
			// (set) Token: 0x06001197 RID: 4503 RVA: 0x00041EE5 File Offset: 0x000400E5
			bool IODataAtomReaderEntryState.HasEditLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditLink);
				}
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06001198 RID: 4504 RVA: 0x00041EEF File Offset: 0x000400EF
			// (set) Token: 0x06001199 RID: 4505 RVA: 0x00041EFC File Offset: 0x000400FC
			bool IODataAtomReaderEntryState.HasEditMediaLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditMediaLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasEditMediaLink);
				}
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x0600119A RID: 4506 RVA: 0x00041F0A File Offset: 0x0004010A
			// (set) Token: 0x0600119B RID: 4507 RVA: 0x00041F13 File Offset: 0x00040113
			bool IODataAtomReaderEntryState.HasId
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasId);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasId);
				}
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x0600119C RID: 4508 RVA: 0x00041F1D File Offset: 0x0004011D
			// (set) Token: 0x0600119D RID: 4509 RVA: 0x00041F27 File Offset: 0x00040127
			bool IODataAtomReaderEntryState.HasContent
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasContent);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasContent);
				}
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x0600119E RID: 4510 RVA: 0x00041F32 File Offset: 0x00040132
			// (set) Token: 0x0600119F RID: 4511 RVA: 0x00041F3C File Offset: 0x0004013C
			bool IODataAtomReaderEntryState.HasTypeNameCategory
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasTypeNameCategory);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasTypeNameCategory);
				}
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00041F47 File Offset: 0x00040147
			// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00041F51 File Offset: 0x00040151
			bool IODataAtomReaderEntryState.HasProperties
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasProperties);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasProperties);
				}
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00041F5C File Offset: 0x0004015C
			// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00041F69 File Offset: 0x00040169
			bool IODataAtomReaderFeedState.HasCount
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasCount);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasCount);
				}
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00041F77 File Offset: 0x00040177
			// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00041F84 File Offset: 0x00040184
			bool IODataAtomReaderFeedState.HasNextPageLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasNextPageLinkInFeed);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasNextPageLinkInFeed);
				}
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00041F92 File Offset: 0x00040192
			// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00041F9F File Offset: 0x0004019F
			bool IODataAtomReaderFeedState.HasReadLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLinkInFeed);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasReadLinkInFeed);
				}
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00041FAD File Offset: 0x000401AD
			// (set) Token: 0x060011A9 RID: 4521 RVA: 0x00041FBA File Offset: 0x000401BA
			bool IODataAtomReaderFeedState.HasDeltaLink
			{
				get
				{
					return this.GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasDeltaLink);
				}
				set
				{
					this.SetAtomScopeState(value, ODataAtomReader.AtomScope.AtomScopeStateBitMask.HasDeltaLink);
				}
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x060011AA RID: 4522 RVA: 0x00041FC8 File Offset: 0x000401C8
			AtomEntryMetadata IODataAtomReaderEntryState.AtomEntryMetadata
			{
				get
				{
					if (this.atomEntryMetadata == null)
					{
						this.atomEntryMetadata = AtomMetadataReaderUtils.CreateNewAtomEntryMetadata();
					}
					return this.atomEntryMetadata;
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x060011AB RID: 4523 RVA: 0x00041FE4 File Offset: 0x000401E4
			EpmCustomReaderValueCache IODataAtomReaderEntryState.EpmCustomReaderValueCache
			{
				get
				{
					EpmCustomReaderValueCache epmCustomReaderValueCache;
					if ((epmCustomReaderValueCache = this.epmCustomReaderValueCache) == null)
					{
						epmCustomReaderValueCache = (this.epmCustomReaderValueCache = new EpmCustomReaderValueCache());
					}
					return epmCustomReaderValueCache;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x060011AC RID: 4524 RVA: 0x00042009 File Offset: 0x00040209
			AtomFeedMetadata IODataAtomReaderFeedState.AtomFeedMetadata
			{
				get
				{
					if (this.atomFeedMetadata == null)
					{
						this.atomFeedMetadata = AtomMetadataReaderUtils.CreateNewAtomFeedMetadata();
					}
					return this.atomFeedMetadata;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x060011AD RID: 4525 RVA: 0x00042024 File Offset: 0x00040224
			ODataFeed IODataAtomReaderFeedState.Feed
			{
				get
				{
					return (ODataFeed)base.Item;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x060011AE RID: 4526 RVA: 0x00042031 File Offset: 0x00040231
			// (set) Token: 0x060011AF RID: 4527 RVA: 0x00042039 File Offset: 0x00040239
			bool IODataAtomReaderFeedState.FeedElementEmpty
			{
				get
				{
					return this.ElementEmpty;
				}
				set
				{
					this.ElementEmpty = value;
				}
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x00042042 File Offset: 0x00040242
			private void SetAtomScopeState(bool value, ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				if (value)
				{
					this.atomScopeState |= bitMask;
					return;
				}
				this.atomScopeState &= ~bitMask;
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x00042065 File Offset: 0x00040265
			private bool GetAtomScopeState(ODataAtomReader.AtomScope.AtomScopeStateBitMask bitMask)
			{
				return (this.atomScopeState & bitMask) == bitMask;
			}

			// Token: 0x04000665 RID: 1637
			private bool? mediaLinkEntry;

			// Token: 0x04000666 RID: 1638
			private ODataAtomReader.AtomScope.AtomScopeStateBitMask atomScopeState;

			// Token: 0x04000667 RID: 1639
			private AtomEntryMetadata atomEntryMetadata;

			// Token: 0x04000668 RID: 1640
			private AtomFeedMetadata atomFeedMetadata;

			// Token: 0x04000669 RID: 1641
			private EpmCustomReaderValueCache epmCustomReaderValueCache;

			// Token: 0x0200022C RID: 556
			[Flags]
			private enum AtomScopeStateBitMask
			{
				// Token: 0x0400066F RID: 1647
				None = 0,
				// Token: 0x04000670 RID: 1648
				EmptyElement = 1,
				// Token: 0x04000671 RID: 1649
				HasReadLink = 2,
				// Token: 0x04000672 RID: 1650
				HasEditLink = 4,
				// Token: 0x04000673 RID: 1651
				HasId = 8,
				// Token: 0x04000674 RID: 1652
				HasContent = 16,
				// Token: 0x04000675 RID: 1653
				HasTypeNameCategory = 32,
				// Token: 0x04000676 RID: 1654
				HasProperties = 64,
				// Token: 0x04000677 RID: 1655
				HasCount = 128,
				// Token: 0x04000678 RID: 1656
				HasNextPageLinkInFeed = 256,
				// Token: 0x04000679 RID: 1657
				HasReadLinkInFeed = 512,
				// Token: 0x0400067A RID: 1658
				HasEditMediaLink = 1024,
				// Token: 0x0400067B RID: 1659
				HasDeltaLink = 2048
			}
		}
	}
}
