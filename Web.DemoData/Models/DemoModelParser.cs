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

using DevExpress.Web.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace DevExpress.Web.Demos {
	public static class DemoModelParser {
		static Regex HelpLinkRegex = new Regex("<helplink([^>]*)>(.*?)</helplink>", RegexOptions.Singleline);
		static Regex AttributesRegex = new Regex("(\\S+)=[\"']{1}((?:.(?![\"']?\\s+(?:\\S+)|[>\"']))+.)[\"']{1}");
		static string LinkGroupName = "content";
		static Regex HelpLinkContentRegex = new Regex(String.Format(@"(<helplink([^>]*)>)(?<{0}>.+?)(</helplink>)", LinkGroupName));
		public static string ParseLinks(string text) {
			if(text == null)
				return string.Empty;
			var matches = HelpLinkContentRegex.Matches(text);
			var result = new List<string>();
			foreach(Match match in matches) {
				var group = match.Groups[LinkGroupName];
				if(group.Success) {
					result.Add(group.Value);
				}
			}
			return string.Join(" ", result.Distinct());
		}
		public static string ProcessDescription(string text) {
			if(text == null)
				text = "";
			text = Regex.Replace(text, @"<code\s+lang=([^>]+)>(.*?)</code>", DescriptionCodeReplacer, RegexOptions.Singleline);
			text = HelpLinkRegex.Replace(text, DescriptionHelpLinkReplacer);
			text = BootstrapDemoSectionCodeParser.ReplaceCollapseRegion(text, BootstrapDemoSectionCollapseRegionType.Description);
			return text;
		}
		static string DescriptionHelpLinkReplacer(Match match) {
			Dictionary<string, string> attributes = new Dictionary<string, string>();
			var attrMatches = AttributesRegex.Matches(match.Groups[1].Value);
			foreach(Match am in attrMatches) {
				attributes[am.Groups[1].Value] = am.Groups[2].Value;
			}
			if(!attributes.ContainsKey("href"))
				attributes["href"] = "http://help.devexpress.com/";
			return string.Format("<a href=\"{0}\" class=\"{1}\">{2}</a>", attributes["href"], "helplink", match.Groups[2].Value);
		}
		public static string ProcessPageControlTags(string text) {
			if(text == null)
				text = "";
			return text.Contains("<pageControl>") ? Regex.Replace(text, "<pageControl>(.*?)</pageControl>", DescriptionPageControlReplacer, RegexOptions.Singleline) : text;
		}
		static string DescriptionCodeReplacer(Match match) {
			string lang = match.Groups[1].Value.Trim('"', '\'');
			string code = match.Groups[2].Value;
			string result = "<code>" + CodeFormatter.GetFormattedCode(CodeFormatter.ParseLanguage(lang), code, Utils.IsMvc, new string[0]) + "<br /></code>";
			return Utils.IsOverview ? string.Format("<div class='{0}'>{1}</div>", "CodeBlock", result) : result;
		}
		static string DescriptionPageControlReplacer(Match match) {
			MatchCollection tabPages = Regex.Matches(match.Value, @"<tabPage\s+text=([^>]+)>(.*?)</tabPage>", RegexOptions.Singleline);
			if(tabPages.Count == 0)
				return match.Groups[1].Value;
			ASPxPageControl pageControl = new ASPxPageControl();
			pageControl.ID = "OverviewPageControl";
			pageControl.CssClass = "DemoPageControl";
			pageControl.EnableTabScrolling = true;
			pageControl.TabAlign = TabAlign.Justify;
			pageControl.EnableTheming = false;
			pageControl.ActiveTabIndex = 0;
			pageControl.EnableViewState = false;
			pageControl.Width = Unit.Percentage(100);
			foreach(Match tabPage in tabPages) {
				string text = tabPage.Groups[1].Value.Trim('"', '\'');
				var tab = pageControl.TabPages.Add(text, text);
				tab.Controls.Add(RenderUtils.CreateLiteralControl(tabPage.Groups[2].Value));
			}
			return RenderUtils.GetRenderResult(pageControl);
		}
	}
}
