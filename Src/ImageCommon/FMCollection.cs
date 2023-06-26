using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.WindowsPhone.FeatureAPI;
using Microsoft.WindowsPhone.ImageUpdate.PkgCommon;
using Microsoft.WindowsPhone.ImageUpdate.Tools.Common;

namespace Microsoft.WindowsPhone.Imaging
{
	// Token: 0x02000025 RID: 37
	public class FMCollection
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000A067 File Offset: 0x00008267
		public void LoadFromManifest(string xmlFile, IULogger logger)
		{
			this.Manifest = new FMCollectionManifest();
			this.Logger = logger;
			FMCollectionManifest.ValidateAndLoad(ref this.Manifest, xmlFile, this.Logger);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A090 File Offset: 0x00008290
		public PublishingPackageList GetPublishingPackageList2(string fmDirectory, string msPackageRoot, string buildType, CpuId cpuType, bool cbsBased = false, bool skipMissingPackages = false)
		{
			PublishingPackageList publishingPackageList = this.GetPublishingPackageList(fmDirectory, msPackageRoot, buildType, cpuType, skipMissingPackages);
			foreach (PublishingPackageInfo publishingPackageInfo in publishingPackageList.Packages)
			{
				string text = Path.Combine(msPackageRoot, publishingPackageInfo.Path);
				if (FileUtils.IsTargetUpToDate(publishingPackageInfo.Path, Path.ChangeExtension(text, PkgConstants.c_strCBSPackageExtension)))
				{
					publishingPackageInfo.Path = Path.ChangeExtension(publishingPackageInfo.Path, PkgConstants.c_strCBSPackageExtension);
				}
			}
			return publishingPackageList;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A228 File Offset: 0x00008428
		public PublishingPackageList GetPublishingPackageList(string fmDirectory, string msPackageRoot, string buildType, CpuId cpuType, bool skipMissingPackages = false)
		{
			PublishingPackageList publishingPackageList = new PublishingPackageList();
			publishingPackageList.MSFeatureGroups = new List<FMFeatureGrouping>();
			publishingPackageList.OEMFeatureGroups = new List<FMFeatureGrouping>();
			if (this.Manifest == null)
			{
				throw new ImageCommonException("ImageCommon!GetPublishingPackageList: Unable to generate Publishing Package List without a FM Collection.");
			}
			publishingPackageList.IsTargetFeatureEnabled = this.Manifest.IsBuildFeatureEnabled;
			if (!this.Manifest.IsBuildFeatureEnabled)
			{
				if (this.Manifest.FeatureIdentifierPackages != null)
				{
					publishingPackageList.FeatureIdentifierPackages = this.Manifest.FeatureIdentifierPackages;
				}
				else
				{
					publishingPackageList.FeatureIdentifierPackages = new List<FeatureIdentifierPackage>();
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (FMCollectionItem fmcollectionItem in this.Manifest.FMs)
			{
				if ((cpuType == fmcollectionItem.CPUType || fmcollectionItem.CPUType == null) && !fmcollectionItem.SkipForPublishing)
				{
					FeatureManifest featureManifest = new FeatureManifest();
					string text = Environment.ExpandEnvironmentVariables(fmcollectionItem.Path);
					text = text.ToUpper().Replace("$(FMDIRECTORY)", fmDirectory);
					FeatureManifest.ValidateAndLoad(ref featureManifest, text, this.Logger, fmcollectionItem.ownerType != 1);
					List<FeatureManifest.FMPkgInfo> list = new List<FeatureManifest.FMPkgInfo>();
					featureManifest.GetAllPackageByGroups(ref list, this.Manifest.SupportedLanguages, this.Manifest.SupportedLocales, this.Manifest.SupportedResolutions, buildType, cpuType.ToString(), msPackageRoot);
					List<PublishingPackageInfo> list2 = new List<PublishingPackageInfo>();
					if (skipMissingPackages)
					{
						List<string> missingPackageFeatures = (from pkg in list
							where !File.Exists(pkg.PackagePath)
							select pkg.FeatureID).ToList<string>();
						list = list.Where((FeatureManifest.FMPkgInfo pkg) => !missingPackageFeatures.Contains(pkg.FeatureID, this.IgnoreCase)).ToList<FeatureManifest.FMPkgInfo>();
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					bool flag2 = false;
					using (List<FeatureManifest.FMPkgInfo>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							FeatureManifest.FMPkgInfo pkgInfo = enumerator2.Current;
							if (this.Manifest.FeatureIdentifierPackages.Where((FeatureIdentifierPackage fip) => fip.FeatureID.Equals(pkgInfo.FeatureID, StringComparison.OrdinalIgnoreCase) && fip.ID.Equals(pkgInfo.ID, StringComparison.OrdinalIgnoreCase) && fip.FixUpAction == FeatureIdentifierPackage.FixUpActions.Ignore).Count<FeatureIdentifierPackage>() == 0)
							{
								PublishingPackageInfo publishingPackageInfo;
								try
								{
									publishingPackageInfo = new PublishingPackageInfo(pkgInfo, fmcollectionItem, msPackageRoot, fmcollectionItem.UserInstallable);
								}
								catch (FileNotFoundException)
								{
									flag2 = true;
									stringBuilder2.AppendFormat("\t{0}\n", pkgInfo.ID);
									continue;
								}
								if (publishingPackageInfo.IsFeatureIdentifierPackage)
								{
									FeatureIdentifierPackage idPkg = new FeatureIdentifierPackage(publishingPackageInfo);
									if (publishingPackageList.FeatureIdentifierPackages == null)
									{
										publishingPackageList.FeatureIdentifierPackages = new List<FeatureIdentifierPackage>();
									}
									else
									{
										List<FeatureIdentifierPackage> list3 = publishingPackageList.FeatureIdentifierPackages.Where((FeatureIdentifierPackage fipPkg) => string.Equals(fipPkg.ID, idPkg.ID, StringComparison.OrdinalIgnoreCase) && string.Equals(fipPkg.Partition, idPkg.Partition, StringComparison.OrdinalIgnoreCase)).ToList<FeatureIdentifierPackage>();
										int num = list3.Count<FeatureIdentifierPackage>();
										if (num > 0)
										{
											StringBuilder stringBuilder3 = new StringBuilder();
											stringBuilder3.Append(Environment.NewLine + idPkg.FeatureID + " : ");
											foreach (FeatureIdentifierPackage featureIdentifierPackage in list3)
											{
												stringBuilder3.Append(Environment.NewLine + "\t" + featureIdentifierPackage.ID);
											}
											throw new AmbiguousArgumentException("Some features have more than one FeatureIdentifierPackage defined: " + stringBuilder3.ToString());
										}
									}
									publishingPackageList.FeatureIdentifierPackages.Add(idPkg);
								}
								list2.Add(publishingPackageInfo);
							}
						}
					}
					if (flag2)
					{
						flag = true;
						stringBuilder.AppendFormat("\nThe FM File '{0}' following package file(s) could not be found: \n {1}", text, stringBuilder2.ToString());
					}
					publishingPackageList.Packages.AddRange(list2);
					if (featureManifest.Features != null)
					{
						if (featureManifest.Features.MSFeatureGroups != null)
						{
							foreach (FMFeatureGrouping fmfeatureGrouping in featureManifest.Features.MSFeatureGroups)
							{
								fmfeatureGrouping.FMID = fmcollectionItem.ID;
								publishingPackageList.MSFeatureGroups.Add(fmfeatureGrouping);
							}
						}
						if (featureManifest.Features.OEMFeatureGroups != null)
						{
							foreach (FMFeatureGrouping fmfeatureGrouping2 in featureManifest.Features.OEMFeatureGroups)
							{
								fmfeatureGrouping2.FMID = fmcollectionItem.ID;
								publishingPackageList.OEMFeatureGroups.Add(fmfeatureGrouping2);
							}
						}
					}
				}
			}
			if (flag)
			{
				throw new ImageCommonException("ImageCommon!GetPublishingPackageList: Errors processing FM File(s):\n" + stringBuilder.ToString());
			}
			this.DoFeatureIDFixUps(ref publishingPackageList);
			if (!this.Manifest.IsBuildFeatureEnabled)
			{
				List<FeatureIdentifierPackage> list4 = new List<FeatureIdentifierPackage>();
				foreach (FeatureIdentifierPackage featureIdentifierPackage2 in publishingPackageList.FeatureIdentifierPackages)
				{
					FeatureIdentifierPackage newFip = featureIdentifierPackage2;
					if (string.IsNullOrEmpty(newFip.FMID))
					{
						PublishingPackageInfo publishingPackageInfo2 = publishingPackageList.Packages.Find((PublishingPackageInfo pkg) => pkg.ID.Equals(newFip.ID, StringComparison.OrdinalIgnoreCase) && pkg.FeatureID.Equals(newFip.FeatureID, StringComparison.OrdinalIgnoreCase));
						if (publishingPackageInfo2 == null)
						{
							throw new ImageCommonException("ImageCommon!GetPublishingPackageList: Unable to find FeatureIdentifierPackage in Package List: " + featureIdentifierPackage2.ID);
						}
						newFip.FMID = publishingPackageInfo2.FMID;
					}
					list4.Add(newFip);
				}
				publishingPackageList.FeatureIdentifierPackages = list4;
			}
			this.ValidateFeatureIdentifers(publishingPackageList);
			publishingPackageList.ValidateConstraints();
			return publishingPackageList;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A988 File Offset: 0x00008B88
		private void DoFeatureIDFixUps(ref PublishingPackageList fullList)
		{
			if (fullList.FeatureIdentifierPackages == null || fullList.FeatureIdentifierPackages.Count<FeatureIdentifierPackage>() == 0)
			{
				return;
			}
			List<FeatureIdentifierPackage> list = fullList.FeatureIdentifierPackages.Where((FeatureIdentifierPackage pkg) => pkg.FixUpAction == FeatureIdentifierPackage.FixUpActions.Ignore || pkg.FixUpAction == FeatureIdentifierPackage.FixUpActions.MoveToAnotherFeature).ToList<FeatureIdentifierPackage>();
			using (List<FeatureIdentifierPackage>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FeatureIdentifierPackage fip = enumerator.Current;
					List<PublishingPackageInfo> list2;
					if (string.IsNullOrEmpty(fip.ID))
					{
						list2 = fullList.Packages.Where((PublishingPackageInfo pkg) => string.Equals(pkg.FeatureID, fip.FeatureID, StringComparison.OrdinalIgnoreCase)).ToList<PublishingPackageInfo>();
					}
					else
					{
						list2 = fullList.Packages.Where((PublishingPackageInfo pkg) => (string.Equals(pkg.ID, fip.ID, StringComparison.OrdinalIgnoreCase) || (pkg.SatelliteType == PublishingPackageInfo.SatelliteTypes.Language && pkg.ID.StartsWith(fip.ID + PkgFile.DefaultLanguagePattern, StringComparison.OrdinalIgnoreCase)) || (pkg.SatelliteType == PublishingPackageInfo.SatelliteTypes.Resolution && pkg.ID.StartsWith(fip.ID + PkgFile.DefaultResolutionPattern, StringComparison.OrdinalIgnoreCase))) && string.Equals(pkg.Partition, fip.Partition, StringComparison.OrdinalIgnoreCase) && string.Equals(pkg.FeatureID, fip.FeatureID, StringComparison.OrdinalIgnoreCase)).ToList<PublishingPackageInfo>();
					}
					fullList.Packages = fullList.Packages.Except(list2).ToList<PublishingPackageInfo>();
					if (fip.FixUpAction == FeatureIdentifierPackage.FixUpActions.MoveToAnotherFeature)
					{
						foreach (PublishingPackageInfo publishingPackageInfo in list2)
						{
							fullList.Packages.Remove(publishingPackageInfo);
							List<string> list3 = fip.FixUpActionValue.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
							foreach (string text in list3)
							{
								PublishingPackageInfo publishingPackageInfo2 = new PublishingPackageInfo(publishingPackageInfo);
								publishingPackageInfo2.FeatureID = text;
								fullList.Packages.Add(publishingPackageInfo2);
							}
						}
					}
				}
			}
			fullList.FeatureIdentifierPackages = fullList.FeatureIdentifierPackages.Except(list).ToList<FeatureIdentifierPackage>();
			if (!fullList.IsTargetFeatureEnabled)
			{
				fullList.Packages = fullList.Packages.Distinct(PublishingPackageInfoComparer.IgnorePaths).ToList<PublishingPackageInfo>();
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		private void ValidateFeatureIdentifers(PublishingPackageList list)
		{
			this.Manifest.ValidateFeatureIdentiferPackages(list.Packages);
			List<string> list2 = (from pkg in list.Packages
				where pkg.OwnerType == 1
				select pkg.FeatureIDWithFMID).Distinct<string>().ToList<string>();
			List<string> list3 = (from pkg in list.FeatureIdentifierPackages
				where pkg.ownerType == 1
				select pkg.FeatureIDWithFMID).Distinct<string>().ToList<string>();
			List<string> list4 = list2.Except(list3).ToList<string>();
			if (list4.Count<string>() != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in list4)
				{
					stringBuilder.Append(Environment.NewLine + "\t" + text);
				}
				throw new AmbiguousArgumentException("FeatureAPI!ValidateFeatureIdentifiers: The following features don't have the required FeatureIdentifierPackage defined: " + stringBuilder.ToString());
			}
			list.GetFeatureIDWithFMIDPackages(0);
			List<string> fipPackageIDs = new List<string>(from pkg in list.Packages
				where pkg.IsFeatureIdentifierPackage
				select pkg.ID + "." + pkg.Partition);
			List<PublishingPackageInfo> list5 = list.Packages.Where((PublishingPackageInfo pkg) => pkg.OwnerType == 1 && fipPackageIDs.Contains(pkg.ID + "." + pkg.Partition, this.IgnoreCase)).ToList<PublishingPackageInfo>();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			using (List<string>.Enumerator enumerator2 = fipPackageIDs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string packageID = enumerator2.Current;
					List<PublishingPackageInfo> list6 = new List<PublishingPackageInfo>(list5.Where((PublishingPackageInfo listPkg) => string.Equals(listPkg.ID + "." + listPkg.Partition, packageID, StringComparison.OrdinalIgnoreCase)));
					if (list6.Count > 1)
					{
						flag = true;
						foreach (PublishingPackageInfo publishingPackageInfo in list6)
						{
							stringBuilder2.AppendLine();
							stringBuilder2.AppendFormat("\t{0} ({1}) {2}", publishingPackageInfo.ID, publishingPackageInfo.FeatureID, publishingPackageInfo.IsFeatureIdentifierPackage ? "(IsFeatureIdentifierPackage)" : "");
						}
						stringBuilder2.AppendLine();
					}
				}
			}
			if (flag)
			{
				throw new AmbiguousArgumentException("FeatureAPI!ValidateFeatureIdentifiers: Feature Identifier Packages found in multiple Features: " + stringBuilder2.ToString());
			}
		}

		// Token: 0x04000108 RID: 264
		public FMCollectionManifest Manifest;

		// Token: 0x04000109 RID: 265
		public IULogger Logger;

		// Token: 0x0400010A RID: 266
		private StringComparer IgnoreCase = StringComparer.OrdinalIgnoreCase;
	}
}
