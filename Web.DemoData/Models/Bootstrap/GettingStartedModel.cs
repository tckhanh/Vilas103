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
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
namespace DevExpress.Web.Demos {
	public class GettingStartedPageModel : BootstrapDemoPageModelBase {
		List<GettingStartedSectionModel> sections = new List<GettingStartedSectionModel>();
		[XmlElement(Type = typeof(GettingStartedSectionModel), ElementName = "Section")]
		public List<GettingStartedSectionModel> Sections {
			get { return sections; }
		}
	}
	public class GettingStartedSectionModel : BootstrapDemoModelBase {
		static readonly Regex CodeRegex = new Regex(@"(<pre[^>]*?>[\W]*<code[^>]*?>)(.*?)(</code>[\W]*</pre>)",
			RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		[XmlIgnore]
		public override string Key { get { return Title.ToLowerInvariant().Replace(' ', '-').Replace("'", ""); } }
		protected override string ProcessDescription(string text) {
			text = base.ProcessDescription(text);
			text = CodeRegex.Replace(text, CodeReplacer);
			return text;
		}
		static string CodeReplacer(Match match) {
			return match.Groups[1].Value + 
				match.Groups[2].Value
					.Trim()
					.Replace("<", "&lt;")
					.Replace(">", "&gt;")
					.Replace("[HL]", "<span class=\"code-hl\">")
					.Replace("[/HL]", "</span>")
					+
				match.Groups[3].Value;
		}
	}
}
