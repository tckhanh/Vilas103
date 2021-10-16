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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Configuration;
namespace DevExpress.Web.Demos {
	public static class BootstrapUtils {
		static HttpContext Context {
			get { return HttpContext.Current; }
		}
		public static int BootstrapVersion {
			get {
				int ver = 3;
				return int.TryParse(ConfigurationManager.AppSettings["DemoBSVer"], out ver) ? ver : 3;
			}
		}
		public static BootstrapDemoPageModelBase CurrentDemo {
			get { return Context != null ? (BootstrapDemoPageModelBase)Context.Items[Utils.CurrentDemoKey] : null; }
		}
		public static IEnumerable<BootstrapDemoModelBase> CurrentDemoSections {
			get {
				if(CurrentDemo is BootstrapDemoPageModel)
					return ((BootstrapDemoPageModel)CurrentDemo).Sections;
				if(CurrentDemo is GettingStartedPageModel)
					return ((GettingStartedPageModel)CurrentDemo).Sections;
				return null;
			}
		}
		static ConcurrentDictionary<string, string> sourceFilesCode = new ConcurrentDictionary<string, string>();
		public static string GetSourceFileCode(string fileName) {
			fileName = fileName.ToLowerInvariant();
			if(!sourceFilesCode.ContainsKey(fileName)) {
				string code = BootstrapDemoSectionModel.GetCodeFromFile(fileName);
				sourceFilesCode.AddOrUpdate(fileName, BootstrapDemoSectionCodeParser.GetFormattedCustomCode(code), (exCode, nCode) => nCode);
			}
			return sourceFilesCode[fileName];
		}
		public static bool IsSourceFileExist(string virtualPath) {
			if(Context != null) {
				string path = Context.Server.MapPath(virtualPath);
				return File.Exists(path);
			}
			return false;
		}
		public static void RegisterCurrentDemo(Page page) {
			if(page.AppRelativeVirtualPath.ToLowerInvariant().Contains("gettingstarted")) {
				Context.Items[Utils.CurrentDemoKey] = BootstrapDemosModel.Instance.GettingsStarted;
				DevExpress.Web.Internal.DemoUtils.RegisterDemo("Bootstrap", "GettingStarted", "");
			}
			else {
				string groupKey = "";
				string demoKey = "";
				Utils.FindDemoKeysByVirtualPath(page.AppRelativeVirtualPath, out groupKey, out demoKey);
				RegisterCurrentDemo(groupKey, demoKey);
			}
		}
		static void RegisterCurrentDemo(string groupKey, string demoKey) {
			BootstrapDemoPageModelBase demo = null;
			BootstrapDemoGroupPageModel group = BootstrapDemosModel.Instance.FindGroup(groupKey);
			if(group != null) {
				demo = group.FindDemo(demoKey);
				if(demo == null)
					demo = group;
			}
			Context.Items[Utils.CurrentDemoKey] = demo;
			DevExpress.Web.Internal.DemoUtils.RegisterDemo("Bootstrap", groupKey, demoKey);
		}
		public static string GenerateDemoPageUrl(BootstrapDemoPageModelBase demo) {
			StringBuilder stb = new StringBuilder();
			stb.Append("~/");
			if(demo is BootstrapDemoGroupPageModel) {
				stb.Append(demo.Key);
				stb.Append("/Default.aspx");
			}
			else if(demo is BootstrapDemoPageModel) {
				var group = ((BootstrapDemoPageModel)demo).Group;
				if(group != null)
					stb.Append(group.Key + "/");
				stb.Append(demo.Key + ".aspx");
			}
			return stb.ToString();
		}
		public static string GenerateDemoSectionUrl(BootstrapDemoSectionModel section) {
			if(section.Demo != null)
				return string.Format("{0}#{1}", GenerateDemoPageUrl(section.Demo), section.Key);
			return string.Empty;
		}
		public static string GenerateGettingStartedSectionUrl(GettingStartedSectionModel section) {
			return string.Format("GettingStarted.aspx#{0}", section.Key);
		}
		public static string GetCurrentDemoTitle() {
			return CurrentDemo.GetSeoTitle() + " | DevExpress Bootstrap Controls for ASP.NET";
		}
	}
}
