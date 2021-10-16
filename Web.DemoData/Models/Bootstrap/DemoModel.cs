#region Copyright (c) 2000-2020 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2020 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2020 Developer Express Inc.

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
namespace DevExpress.Web.Demos {
	public class BootstrapDemoModelBase : DemoModelBase {
		Dictionary<string, string> processedDescriptions = new Dictionary<string, string>();
		string links;
		[XmlElement]
		public virtual string Description { get; set; }
		[XmlIgnore]
		public string Links {
			get {
				if(string.IsNullOrEmpty(links))
					links = ParseLinks();
				return links;
			}
		}
		public string GetProcessedDescription() {
			return GetProcessedDescription(Description, "Description");
		}
		protected string GetProcessedDescription(string description, string key) {
			if(!processedDescriptions.ContainsKey(key))
				processedDescriptions[key] = ProcessDescription(description);
			return processedDescriptions[key];
		}
		protected virtual string ParseLinks() {
			return DemoModelParser.ParseLinks(Description);
		}
		protected override Dictionary<string, int> GetKeywordsRankList() {
			return BootstrapSearchUtils.GetKeywordsRankList(this);
		}
		protected virtual string ProcessDescription(string description) {
			return DemoModelParser.ProcessDescription(description);
		}
	}
	public class BootstrapDemoPageModelBase : BootstrapDemoModelBase {
		public string GetSeoTitle() {
			if(!string.IsNullOrEmpty(SeoTitle))
				return SeoTitle;
			return Title;
		}
		public virtual bool HasPreviewStatus() {
			return IsPreview;
		}
		public virtual bool HasNewStatus() {
			return IsNew;
		}
		public virtual bool HasUpdatedStatus() {
			return IsUpdated;
		}
	}
	public class BootstrapDemoPageModel : BootstrapDemoPageModelBase {
		List<BootstrapDemoSectionModel> sections = new List<BootstrapDemoSectionModel>();
		[XmlElement(Type = typeof(BootstrapDemoSectionModel), ElementName = "DemoSection")]
		public List<BootstrapDemoSectionModel> Sections {
			get { return sections; }
		}
		[XmlIgnore]
		public BootstrapDemoGroupPageModel Group { get; internal set; }
		public BootstrapDemoSectionModel FindSection(string key) {
			if(!string.IsNullOrEmpty(key)) {
				key = key.ToLower();
				foreach(BootstrapDemoSectionModel section in Sections) {
					if(key == section.Key.ToLower())
						return section;
				}
			}
			return null;
		}
		public override bool HasUpdatedStatus() {
			return base.HasUpdatedStatus() || Sections.Where(s => s.IsNew || s.IsUpdated).Any();
		}
	}
}
