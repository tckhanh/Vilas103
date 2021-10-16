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
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Xml.Serialization;
namespace DevExpress.Web.Demos {
	public class DemoPageModel : DemoModel {
		DemoGroupModel _group;
		List<SeeAlsoLinkModel> _seeAlsoLinks = new List<SeeAlsoLinkModel>();
		string _highlightedDescription = string.Empty;
		[XmlIgnore]
		public DemoGroupModel Group {
			get { return _group; }
			internal set { _group = value; }
		}
		[XmlElement("SeeAlso")]
		public List<SeeAlsoLinkModel> SeeAlsoLinks { get { return _seeAlsoLinks; } set { _seeAlsoLinks = value; } }
		[XmlElement("HighlightedDescription")]
		public string HighlightedDescription { get { return _highlightedDescription; } set { _highlightedDescription = value; } }
	}
	public class DemoModel : DemoModelBase {
		string _description;
		string _links;
		string _metaDescription;
		bool _hideSourceCode;
		List<string> _sourceFiles = new List<string>();
		List<string> _resourceKeys = new List<string>();
		public const string AppCodeTempFolderName = "App_Code_Temp";
		int _highlightedIndex = -1;
		string _highlightedImageUrl;
		string _highlightedTitle;
		string _highlightedLink;
		string _virtualGroupKey;
		bool isErrorPage = false;
		DemoProductModel _product;
		bool _descriptionProcessed;
		bool _linksProcessed;
		public bool _sourceFilesPathsProcessed;
		[XmlIgnore]
		public DemoProductModel Product {
			get { return _product; }
			internal set { _product = value; }
		}
		[XmlIgnore]
		public bool IsErrorPage {
			get { return isErrorPage; }
			set { isErrorPage = value; }
		}
		[XmlAttribute]
		public virtual bool HideSourceCode {
			get { return _hideSourceCode; }
			set { _hideSourceCode = value; }
		}
		[XmlElement]
		public string Description {
			get {
				if(!_descriptionProcessed) {
					ParseLinks();
					_description = DemoModelParser.ProcessDescription(_description);
					_descriptionProcessed = true;
				}
				return DemoModelParser.ProcessPageControlTags(_description);
			}
			set {
				if(value != null)
					value = value.Trim();
				_description = value;
			}
		}
		[XmlIgnore]
		public string Links {
			get {
				if (!_linksProcessed) {
					ParseLinks();
				}
				return _links;
			}
		}
		[XmlElement]
		public string MetaDescription {
			get {
				if(_metaDescription == null)
					return "";
				return _metaDescription;
			}
			set {
				if(value != null)
					value = value.Trim();
				_metaDescription = value;
			}
		}
		[XmlElement("SourceFile")]
		public List<string> SourceFiles {
			get {
				if(!_sourceFilesPathsProcessed && _sourceFiles.Count != 0) {
					ProcessSourceFilesPaths(_sourceFiles);
					_sourceFilesPathsProcessed = true;
				}
				return _sourceFiles;
			}
		}
		[XmlElement("ResourceKey")]
		public List<string> ResourceKeys {
			get { return _resourceKeys; }
		}
		[XmlAttribute]
		public string VirtualGroupKey {
			get {
				if(_virtualGroupKey == null)
					return "";
				return _virtualGroupKey;
			}
			set { _virtualGroupKey = value; }
		}
		[XmlAttribute]
		public int HighlightedIndex {
			get { return _highlightedIndex; }
			set { _highlightedIndex = value; }
		}
		[XmlAttribute]
		public string HighlightedImageUrl {
			get {
				if(_highlightedImageUrl == null)
					return "";
				return _highlightedImageUrl;
			}
			set { _highlightedImageUrl = value; }
		}
		[XmlAttribute]
		public string HighlightedTitle {
			get {
				if(_highlightedTitle == null)
					return "";
				return _highlightedTitle;
			}
			set { _highlightedTitle = value; }
		}
		[XmlAttribute]
		public string HighlightedLink {
			get { return _highlightedLink; }
			set { _highlightedLink = value; }
		}
		public string GetHighlightedTitle() {
			if(!String.IsNullOrEmpty(HighlightedTitle))
				return HighlightedTitle;
			return Title;
		}
		void ParseLinks() {
			_links = DemoModelParser.ParseLinks(_description);
			_linksProcessed = true;
		}
		void ProcessSourceFilesPaths(List<string> _sourceFiles) {
			var appCodeFilesTempFolder = WebConfigurationManager.AppSettings[AppCodeTempFolderName];
			if(!string.IsNullOrEmpty(appCodeFilesTempFolder)) {
				for(var i = 0; i < _sourceFiles.Count; ++i) {
					if(Regex.IsMatch(_sourceFiles[i], "[\\\\/]App_Code[\\\\/]"))
						_sourceFiles[i] = _sourceFiles[i].Replace("App_Code", appCodeFilesTempFolder);
				}
			}
		}
		public string GetSeoTitle() {
			if(!String.IsNullOrEmpty(SeoTitle))
				return SeoTitle;
			return Title;
		}
	}
	public class SeeAlsoLinkModel {
		string _src;
		string _title;
		[XmlAttribute]
		public string Url { get { return _src; } set { _src = value; } }
		[XmlAttribute]
		public string Title { get { return _title; } set { _title = value; } }
	}
}
