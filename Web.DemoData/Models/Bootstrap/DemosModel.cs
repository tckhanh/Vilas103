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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
namespace DevExpress.Web.Demos {
	[XmlRoot("Demos")]
	public class BootstrapDemosModel {
		const string Demos3ModelPath = "Data.Bootstrap.Demos3.xml";
		const string DemosModelPath = "Data.Bootstrap.Demos.xml";
		static BootstrapDemosModel _instance4;
		static readonly object _instance4Lock = new object();
		static BootstrapDemosModel _instance3;
		static readonly object _instance3Lock = new object();
		List<BootstrapDemoGroupPageModel> demoGroups = new List<BootstrapDemoGroupPageModel>();
		List<BootstrapDemoGroupPageModel> sortedDemoGroups;
		public static BootstrapDemosModel Instance {
			get { return BootstrapUtils.BootstrapVersion > 3 ? Instance4 : Instance3; }
		}
		protected static BootstrapDemosModel Instance4 {
			get {
				lock(_instance4Lock) {
					if(_instance4 == null) {
						using(Stream stream = ResourcesUtils.GetEmbeddedResource(DemosModelPath)) {
							XmlSerializer serializer = new XmlSerializer(typeof(BootstrapDemosModel));
							_instance4 = (BootstrapDemosModel)serializer.Deserialize(stream);
						}
						foreach(var group in _instance4.DemoGroups) {
							foreach(var demo in group.Demos) {
								demo.Group = group;
								foreach(var section in demo.Sections) {
									section.Demo = demo;
								}
							}
						}
					}
					return _instance4;
				}
			}
		}
		protected static BootstrapDemosModel Instance3 {
			get {
				lock(_instance3Lock) {
					if(_instance3 == null) {
						using(Stream stream = ResourcesUtils.GetEmbeddedResource(Demos3ModelPath)) {
							XmlSerializer serializer = new XmlSerializer(typeof(BootstrapDemosModel));
							_instance3 = (BootstrapDemosModel)serializer.Deserialize(stream);
						}
						foreach(var group in _instance3.DemoGroups) {
							foreach(var demo in group.Demos) {
								demo.Group = group;
								foreach(var section in demo.Sections) {
									section.Demo = demo;
								}
							}
						}
					}
					return _instance3;
				}
			}
		}
		[XmlElement("DemoGroup")]
		public List<BootstrapDemoGroupPageModel> DemoGroups {
			get { return demoGroups; }
		}
		[XmlElement("GettingStarted")]
		public GettingStartedPageModel GettingsStarted {
			get; set;
		}
		[XmlIgnore]
		public List<BootstrapDemoGroupPageModel> SortedDemoGroups {
			get {
				if(sortedDemoGroups == null)
					sortedDemoGroups = DemoGroups.OrderBy(p => p.OrderIndex).ToList();
				return sortedDemoGroups;
			}
		}
		[XmlElement("Search")]
		public SearchModel Search { get; set; }
		public BootstrapDemoGroupPageModel FindGroup(string key) {
			if(!string.IsNullOrEmpty(key)) {
				key = key.ToLower();
				foreach(BootstrapDemoGroupPageModel group in DemoGroups) {
					if(key == group.Key.ToLower())
						return group;
				}
			}
			return null;
		}
	}
}
