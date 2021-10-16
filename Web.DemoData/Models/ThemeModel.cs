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
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
namespace DevExpress.Web.Demos {
	public class ThemeModel : ThemeModelBase {
		string _baseColor;
		string _fontFamily;
		string _fontSize;
		bool _showAsTop;
		string _previewColor;
		string _preveiwIconResourceName;
		[XmlAttribute]
		public string BaseColor {
			get {
				if(String.IsNullOrEmpty(_baseColor))
					return "";
				return _baseColor;
			}
			set {
				_baseColor = value.ToUpper();
			}
		}
		[XmlAttribute]
		public bool IsNew { get; set; }
		[XmlAttribute]
		public string FontFamily {
			get {
				if(String.IsNullOrEmpty(_fontFamily))
					return "";
				return _fontFamily;
			}
			set {
				_fontFamily = value;
			}
		}
		[XmlAttribute]
		public string FontSize {
			get {
				if(String.IsNullOrEmpty(_fontSize))
					return "";
				return _fontSize;
			}
			set {
				_fontSize = value;
			}
		}
		public string Font {
			get {
				var result = string.Empty;
				if(!string.IsNullOrEmpty(FontSize) && !string.IsNullOrEmpty(FontFamily))
					result = string.Format("{0} {1}", FontSize, FontFamily);
				return result;
			}
		}
		[XmlAttribute]
		public bool ShowAsTop {
			get {
				return _showAsTop;
			}
			set {
				_showAsTop = value;
			}
		}
		[XmlAttribute]
		public string PreveiwColor {
			get {
				if(String.IsNullOrEmpty(_previewColor))
					return "";
				return _previewColor;
			}
			set {
				_previewColor = value.ToUpper();
			}
		}
		[XmlAttribute]
		public string PreveiwIconResourceName {
			get {
				if(String.IsNullOrEmpty(_preveiwIconResourceName))
					return "";
				return _preveiwIconResourceName;
			}
			set {
				_preveiwIconResourceName = value;
			}
		}
	}
}
