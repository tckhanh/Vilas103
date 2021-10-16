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
using System.Text;
namespace DevExpress.Web.Demos {
	public class BootstrapDemoSectionModelBase : BootstrapDemoModelBase {
		public const string SourceCodeUnavailable = "The source code is unavailable.";
		public BootstrapDemoSectionModelBase() {
			SourceFiles = new List<BootstrapDemoSectionSourceFileModel>();
		}
		[XmlElement("SourceFile")]
		public List<BootstrapDemoSectionSourceFileModel> SourceFiles { get; private set; }
		[XmlIgnore]
		public BootstrapDemoPageModel Demo { get; internal set; }
		public virtual List<BootstrapDemoSectionSourceFileModel> GetSourceFiles() {
			return SourceFiles;
		}
	}
	public class BootstrapDemoSectionSourceFileModel : IEquatable<BootstrapDemoSectionSourceFileModel> {
		[XmlText]
		public string Path { get; set; }
		[XmlAttribute("Title")]
		public string CustomTitle { get; set; }
		[XmlIgnore]
		public string Content { get; set; }
		public bool Equals(BootstrapDemoSectionSourceFileModel other) {
			return other.Path.Equals(Path);
		}
		public override int GetHashCode() {
			return Path.GetHashCode();
		}
	}
	public enum BootstrapDemoSectionCollapseRegionType { Code, Description }
	public class BootstrapDemoSectionCodeParserBase {
		public static readonly Regex ScriptRegex = new Regex("<script[^>]*?>(?<Code>.*?)</script>",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		public static string TrimCode(string code) {
			string[] lines = code.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			for(int i = 0; i < lines.Length; i++)
				lines[i] = lines[i].TrimEnd(' ', '\t');
			return string.Join("\r\n", lines).Trim('\r', '\n');
		}
		public static string ReplaceBrackets(string code) {
			StringBuilder stb = new StringBuilder();
			bool skipRegion = false;
			for(int i = 0; i < code.Length; i++) {
				if(code[i] == '<') {
					if(i + 3 < code.Length && code[i + 1] == '%' && code[i + 2] == '-' && code[i + 3] == '-') {
						skipRegion = true;
						i += 3;
					}
					else if(!skipRegion)
						stb.Append("&lt;");
					else
						stb.Append(code[i]);
				}
				else if(code[i] == '>') {
					if(i - 3 >= 0 && code[i - 1] == '%' && code[i - 2] == '-' && code[i - 3] == '-') {
						skipRegion = false;
						stb.Remove(stb.Length - 3, 3);
					}
					else if(!skipRegion)
						stb.Append("&gt;");
					else
						stb.Append(code[i]);
				}
				else
					stb.Append(code[i]);
			}
			return stb.ToString();
		}
		public static string GetCollapseRegionHtml(string code, BootstrapDemoSectionCollapseRegionType regionType = BootstrapDemoSectionCollapseRegionType.Code) {
			string startComment = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "<%--" : "";
			string endComment = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "--%>" : "";
			string cssClass = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "more-code" : "more-description";
			string wrapperTag = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "span" : "div";
			string btnCssClass = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "btn btn-secondary" : "btn btn-link";
			string coverHtml = regionType == BootstrapDemoSectionCollapseRegionType.Code ? "" : "<span class=\"more-description-cover\"></span>";
			return string.Format("{1}<{6} class=\"{3}\">{5}<button type=\"button\" class=\"{4}\" onclick=\"dxbsDemo.codeEditor.expandCode(this)\"></button>" +
				   "<{6} style=\"display:none\">{2}{0}{1}</{6}></{6}>{2}", code.Trim(' ', '\t', '\r', '\n'), startComment, endComment, cssClass, btnCssClass, coverHtml, wrapperTag);
		}
		public static string FixCodeIndent(string code) {
			string[] lines = code.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			if(lines.Length > 1) {
				int whiteSpaceCount = 0;
				while(lines[0].Length > whiteSpaceCount && (lines[0][whiteSpaceCount] == ' ' || lines[0][whiteSpaceCount] == '\t'))
					whiteSpaceCount++;
				if(whiteSpaceCount > 0) {
					var whiteSpaces = lines[0].Substring(0, whiteSpaceCount);
					for(int i = 0; i < lines.Length; i++) {
						if(lines[i].StartsWith(whiteSpaces))
							lines[i] = lines[i].Substring(whiteSpaceCount);
					}
				}
			}
			return string.Join("\r\n", lines);
		}
		public static string ReplaceHideRegion(Regex regex, string code) {
			return regex.Replace(code, "");
		}
		public static string ReplaceCollapseRegion(Regex regex, string code, BootstrapDemoSectionCollapseRegionType regionType = BootstrapDemoSectionCollapseRegionType.Code) {
			return regex.Replace(code, match => {
				return GetCollapseRegionHtml(match.Groups["Code"].Value, regionType);
			});
		}
	}
}
