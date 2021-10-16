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
using System.Web;
namespace DevExpress.Web.Demos {
	public class OverviewPageModel: DemoPageModel {
		List<KeyFeatureModel> _keyFeatures = new List<KeyFeatureModel>();
		string _descriptionTitle;
		[XmlIgnore]
		public override string Key {
			get { return "Overview"; }
			set { }
		}
		[XmlElement("KeyFeature")]
		public List<KeyFeatureModel> KeyFeatures {
			get { return _keyFeatures; }
			set { _keyFeatures = value; } 
		}
		[XmlElement("DescriptionTitle")]
		public string DescriptionTitle {
			get { return _descriptionTitle; }
			set { _descriptionTitle = value; }
		}
	}
	public class KeyFeatureModel {
		string _name;
		string _demoUrl;
		string _description;
		[XmlAttribute]
		public string Name {
			get { return _name; }
			set { _name = value; } 
		}
		[XmlAttribute]
		public string DemoUrl {
			get { return _demoUrl; }
			set { _demoUrl = value; }
		}
		[XmlElement]
		public string Description { 
			get { return _description; } 
			set { _description = value; } 
		}
		public string GetNameHtml() {
			return !string.IsNullOrEmpty(DemoUrl) ? string.Format("<a href='{0}'>{1}</a>", VirtualPathUtility.ToAbsolute(DemoUrl), Name) : Name;
		}
	}
}
