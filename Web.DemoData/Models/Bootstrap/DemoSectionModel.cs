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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Web;
using System.Web.Configuration;
using System.Text;
namespace DevExpress.Web.Demos {
	public class BootstrapDemoSectionModel : BootstrapDemoSectionModelBase {
		string code, codeASPX, codeCS, codeVB, codeJS;
		[XmlIgnore]
		public string Code {
			get {
				if(string.IsNullOrEmpty(code)) {
					code = GetCodeFromFile();
					code = GetPatchedCode(code);
				}
				return code;
			}
			set {
				code = GetPatchedCode(value);
			}
		}
		[XmlIgnore]
		public string CodeASPX {
			get {
				if(string.IsNullOrEmpty(codeASPX)) 
					codeASPX = !string.IsNullOrEmpty(Code) ? BootstrapDemoSectionCodeParser.GetFormattedASPXCode(Code) : SourceCodeUnavailable;
				return codeASPX;
			}
		}
		[XmlIgnore]
		public string CodeCS {
			get {
				if(string.IsNullOrEmpty(codeCS))
					codeCS = !string.IsNullOrEmpty(Code) && !IsVBCode(Code) ? BootstrapDemoSectionCodeParser.GetFormattedCSCode(Code) : string.Empty;
				return codeCS;
			}
		}
		[XmlIgnore]
		public string CodeVB {
			get {
				if(string.IsNullOrEmpty(codeVB))
					codeVB = !string.IsNullOrEmpty(Code) && IsVBCode(Code) ? BootstrapDemoSectionCodeParser.GetFormattedVBCode(Code) : string.Empty;
				return codeVB;
			}
		}
		[XmlIgnore]
		public string CodeJS {
			get {
				if(string.IsNullOrEmpty(codeJS))
					codeJS = !string.IsNullOrEmpty(Code) ? BootstrapDemoSectionCodeParser.GetFormattedJSCode(Code) : string.Empty;
				return codeJS;
			}
		}
		public override List<BootstrapDemoSectionSourceFileModel> GetSourceFiles() {
			var appCodeFilesTempFolder = WebConfigurationManager.AppSettings[DemoModel.AppCodeTempFolderName];
			var regex = new Regex("[\\\\/]App_Code[\\\\/]");
			return SourceFiles.Select(file => {
				var path = file.Path;
				if(!string.IsNullOrEmpty(appCodeFilesTempFolder) && regex.IsMatch(path))
					path = path.Replace("App_Code", appCodeFilesTempFolder);
				return new BootstrapDemoSectionSourceFileModel() {
					Path = path,
					CustomTitle = file.CustomTitle
				};
			}).ToList();
		}
		protected bool IsVBCode(string code) {
			return BootstrapDemoSectionCodeParser.VBCodeFileRegex.IsMatch(code) || BootstrapDemoSectionCodeParser.VBEndFunctionRegex.IsMatch(code);
		}
		protected string GetPatchedCode(string code) {
			var iFrameDemoUrl = BootstrapDemoSectionCodeParser.FindIFrameDemoUrlInCode(code);
			if(!string.IsNullOrEmpty(iFrameDemoUrl))
				code = GetCodeFromFile(iFrameDemoUrl);
			return code;
		}
		protected string GetCodeFromFile() {
			string virtualPath = BootstrapUtils.GenerateDemoPageUrl(Demo);
			return GetCodeFromFile(virtualPath, Key);
		}
		public static string GetCodeFromFile(string virtualPath, string sectionKey = null) {
			if(HttpContext.Current != null) {
				string path = HttpContext.Current.Server.MapPath(virtualPath);
				string content = File.ReadAllText(path);
				if(sectionKey != null)
					content = BootstrapDemoSectionCodeParser.GetSectionCode(content, sectionKey);
				return content;
			}
			return string.Empty;
		}
	}
	public class BootstrapDemoSectionCodeParser: BootstrapDemoSectionCodeParserBase {
		public static readonly Regex RunatServerRegex = new Regex("\\s+runat=\"server\"", RegexOptions.IgnoreCase);
		public static readonly Regex ControlIDRegex = new Regex("\\s+ID=\"(?<ID>[^\"]+)\"", RegexOptions.IgnoreCase);
		public static readonly Regex CollapseRegex = new Regex("<%--BeginCollapse--%>(?<Code>.*?)<%--EndCollapse--%>",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static readonly Regex HideRegex = new Regex("<%--BeginHide--%>.*?<%--EndHide--%>",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static readonly Regex HideRegexCS = new Regex("/\\*BeginHide\\*/.*?/\\*EndHide\\*/",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static readonly Regex IFrameViewerRegex = new Regex("<demo:IFrameViewer[^>]+DemoUrl=\"(?<DemoUrl>[^\"]+)\"",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static readonly Regex VBCodeFileRegex = new Regex("CodeFile =\"\\w+\\.as(p|c)x\\.vb\"",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static readonly Regex VBEndFunctionRegex = new Regex("End Sub",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static string GetSectionCode(string pageSourceCode, string sectionKey) {
			Regex regex = new Regex("<demo:BootstrapDemoItem[^>]+ID=\"" + sectionKey + "\"[^>]*>(?<Code>.+?)<\\/demo:BootstrapDemoItem>", RegexOptions.Multiline | RegexOptions.Singleline);
			Match match = regex.Match(pageSourceCode);
			if(match.Success)
				return match.Groups["Code"].Value;
			return string.Empty;
		}
		public static string GetFormattedASPXCode(string code) {
			code = RemoveControlID(code);
			code = ScriptRegex.Replace(code, "");
			code = ReplaceCollapseRegion(code);
			code = ReplaceHideRegion(code);
			code = ReplaceBrackets(code);
			code = TrimCode(code);
			code = FixCodeIndent(code);
			return code;
		}
		public static string GetFormattedCSCode(string code) {
			return GetFormattedScriptCode(code, true);
		}
		public static string GetFormattedVBCode(string code) {
			return GetFormattedScriptCode(code, true);
		}
		public static string GetFormattedJSCode(string code) {
			return GetFormattedScriptCode(code, false);
		}
		static string GetFormattedScriptCode(string code, bool serverSide) {
			code = ReplaceCollapseRegion(code);
			code = ReplaceHideRegion(code);
			List<string> scripts = new List<string>();
			MatchCollection matches = ScriptRegex.Matches(code);
			for(int i = 0; i < matches.Count; i++) {
				string script = matches[i].Groups["Code"].Value;
				if(serverSide && RunatServerRegex.IsMatch(matches[i].Value) || !serverSide && !RunatServerRegex.IsMatch(matches[i].Value))
					scripts.Add(script);
			}
			code = string.Join("\r\n", scripts);
			code = ReplaceBrackets(code);
			code = TrimCode(code);
			code = FixCodeIndent(code);
			return code;
		}
		public static string GetFormattedCustomCode(string code) {
			code = ReplaceCollapseRegion(code);
			code = ReplaceHideRegion(code);
			code = ReplaceHideRegionCS(code);
			code = ReplaceBrackets(code);
			code = TrimCode(code);
			code = FixCodeIndent(code);
			return code;
		}
		public static string FindIFrameDemoUrlInCode(string code) {
			Match match = IFrameViewerRegex.Match(code);
			if(match.Success)
				return match.Groups["DemoUrl"].Value;
			return string.Empty;
		}
		static string RemoveControlID(string code) {
			return ControlIDRegex.Replace(code, match => {
				int pos = 0;
				string id = match.Groups["ID"].Value;
				while(pos != -1) {
					pos = code.IndexOf(id, pos + id.Length);
					if(pos > 0 && match.Groups["ID"].Index != pos)
						return match.Value;
				}
				return "";
			});
		}
		public static string ReplaceHideRegion(string code) {
			return ReplaceHideRegion(HideRegex, code);
		}
		public static string ReplaceHideRegionCS(string code) {
			return ReplaceHideRegion(HideRegexCS, code);
		}
		public static string ReplaceCollapseRegion(string code, BootstrapDemoSectionCollapseRegionType regionType = BootstrapDemoSectionCollapseRegionType.Code) {
			return ReplaceCollapseRegion(CollapseRegex, code, regionType);
		}
	}
}
