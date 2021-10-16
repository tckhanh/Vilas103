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
using System.Xml.Serialization;
namespace DevExpress.Web.Demos {
	public class DemoModelBase : ModelBase {
		string _key;
		string _title;
		string _seoTitle;
		bool _isNew;
		bool _isPreview;
		bool _isUpdated;
		[XmlAttribute]
		public virtual string Key {
			get {
				if(_key == null)
					return "";
				return _key;
			}
			set {
				_key = value;
			}
		}
		[XmlAttribute]
		public string Title {
			get {
				if(_title == null)
					return "";
				return _title;
			}
			set {
				_title = value;
			}
		}
		[XmlAttribute]
		public string SeoTitle {
			get {
				if(_seoTitle == null)
					return "";
				return _seoTitle;
			}
			set {
				_seoTitle = value;
			}
		}
		[XmlAttribute]
		public bool IsNew {
			get {
				return _isNew;
			}
			set {
				_isNew = value;
			}
		}
		[XmlAttribute]
		public bool IsPreview {
			get {
				return _isPreview;
			}
			set {
				_isPreview = value;
			}
		}
		[XmlAttribute]
		public bool IsUpdated {
			get {
				return _isUpdated;
			}
			set {
				_isUpdated = value;
			}
		}
		public override string ToString() {
			return Title;
		}
	}
	public class ModelBase {
		string _keywords;
		Dictionary<string, int> _keywordsRankList;
		string _highlightedTagNames;
		[XmlAttribute]
		public string HighlightedTagNames {
			get {
				return _highlightedTagNames;
			}
			set {
				_highlightedTagNames = value;
			}
		}
		[XmlElement]
		public string Keywords {
			get {
				if(_keywords == null)
					return "";
				return _keywords;
			}
			set {
				if(value != null)
					value = value.Trim();
				_keywords = value;
			}
		}
		[XmlIgnore]
		public Dictionary<string, int> KeywordsRankList {
			get {
				if(_keywordsRankList == null)
					_keywordsRankList = GetKeywordsRankList();
				return _keywordsRankList;
			}
		}
		protected virtual Dictionary<string, int> GetKeywordsRankList() {
			return SearchUtils.GetKeywordsRankList(this);
		}
		public string[] GetHighlightedTagNames() {
			if(!string.IsNullOrEmpty(HighlightedTagNames))
				return HighlightedTagNames.Split();
			return new string[0];
		}
	}
}
