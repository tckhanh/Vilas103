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

using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
namespace DevExpress.Web.Demos {
	public class BootstrapDemoGroupPageModel : BootstrapDemoPageModelBase {
		List<BootstrapDemoPageModel> demos = new List<BootstrapDemoPageModel>();
		string demoGroupMarkerName;
		[XmlElement(Type = typeof(BootstrapDemoPageModel), ElementName = "Demo")]
		public List<BootstrapDemoPageModel> Demos {
			get { return demos; }
		}
		[XmlElement]
		public string PreDescription { get; set; }
		[XmlAttribute]
		public string IconCssClass { get; set; }
		[XmlAttribute]
		public int OrderIndex { get; set; }
		public string DemoGroupMarkerName {
			get {
				if(string.IsNullOrEmpty(demoGroupMarkerName))
					demoGroupMarkerName = Key + "Group";
				return demoGroupMarkerName;
			}
		}
		public string GetProcessedPreDescription() {
			return GetProcessedDescription(PreDescription, "PreDescription");
		}
		public BootstrapDemoPageModel FindDemo(string key) {
			if(!string.IsNullOrEmpty(key)) {
				key = key.ToLower();
				foreach(BootstrapDemoPageModel demo in Demos) {
					if(key == demo.Key.ToLower())
						return demo;
				}
			}
			return null;
		}
		public override bool HasUpdatedStatus() {
			return base.HasUpdatedStatus() || Demos.Where(d => d.HasNewStatus() || d.HasUpdatedStatus()).Any();
		}
		protected override string ParseLinks() {
			return DemoModelParser.ParseLinks(Description + PreDescription);
		}
	}
}
