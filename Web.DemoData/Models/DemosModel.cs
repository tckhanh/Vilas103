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
using System.Web;
using System.Linq;
using System.Configuration;
using System;
namespace DevExpress.Web.Demos {
	[XmlRoot("Demos")]
	public class DemosModel {
		static DemosModel _instance;
		static DemoProductModel _current;
		static Dictionary<string, DemoModel> _availableDemos = new Dictionary<string, DemoModel>();
		const string DemosModelPath = "Data.Asp.Demos.xml";
		const string DemosModelMvcPath = "Data.Mvc.Demos.xml";
		GlobalHeaderModel _globalHeader = new GlobalHeaderModel();
		static readonly object _instanceLock = new object();
		static readonly object _currentLock = new object();
		bool _expandAllDemosAtFirstTime;
		bool _disableTextWrap;
		[XmlIgnore]
		public static Dictionary<string, DemoModel> AvailableDemos {
			get { return _availableDemos; }
		}
		public static bool IsMvc {
			get {
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				return assemblies.Any(x => x.FullName.Contains("System.Web.Mvc"));
			}
		}
		public static DemosModel Instance {
			get {
				lock(_instanceLock) {
					if(_instance == null)
						_instance = CreateInstance();
					return _instance;
				}
			}
		}
		public static DemosModel CreateInstance() {
			return CreateInstance(IsMvc);
		}
		public static DemosModel CreateInstance(bool isMvc) {
			DemosModel instance = null;
			using(Stream stream = GetDemosModelContentStream(isMvc)) {
				XmlSerializer serializer = new XmlSerializer(typeof(DemosModel));
				instance = (DemosModel)serializer.Deserialize(stream);
			}
			foreach(var demoProduct in instance.DemoProducts) {
				foreach(var group in demoProduct.Groups) {
					foreach(var demo in group.GetAllDemos()) {
						demo.Group = group;
						demo.Product = demoProduct;
						AddDemoToCache(demo);
					}
					group.Product = demoProduct;
				}
				if(demoProduct.Intro != null)
					demoProduct.Intro.Product = demoProduct;
				if(demoProduct.Overview != null)
					demoProduct.Overview.Product = demoProduct;
			}
			return instance;
		}
		static void AddDemoToCache(DemoPageModel demo) {
			var productKey = demo.Product.Key;
			var groupKey = Utils.GetDemoGroupKey(demo);
			var fullKey = (productKey + groupKey + demo.Key).ToLower();
			AvailableDemos[fullKey] = demo;
		}
		public DemoModel FindDemo(string productKey, string groupKey, string demoKey) {
			var fullKey = (productKey + groupKey + demoKey).ToLower();
			if (AvailableDemos.ContainsKey(fullKey))
				return AvailableDemos[fullKey];
			return null;
		}
		public static DemoProductModel Current {
			get {
				lock(_currentLock) {
					if(_current == null)
						_current = Instance.DemoProducts.FirstOrDefault(dp => dp.Key == ConfigurationManager.AppSettings["DemoProduct"]);
					if(_current == null)
						throw new Exception("The current demo is not found");
					return _current;
				}
			}
		}
		static Stream GetDemosModelContentStream(bool isMvc) {
			var demosModelFilePath = HttpContext.Current.Server.MapPath("~/App_Data/Demos.xml");
			if(File.Exists(demosModelFilePath))
				return File.OpenRead(demosModelFilePath);
			return ResourcesUtils.GetEmbeddedResource(isMvc ? DemosModelMvcPath : DemosModelPath);
		}
		List<DemoProductModel> _demoProducts = new List<DemoProductModel>();
		List<DemoProductModel> _sortedDemoProducts;
		SearchModel _search;
		[XmlElement("DemoProduct")]
		public List<DemoProductModel> DemoProducts { get { return _demoProducts; } }
		[XmlIgnore]
		public List<DemoProductModel> SortedDemoProducts {
			get {
				if(_sortedDemoProducts == null)
					_sortedDemoProducts = _demoProducts.OrderBy(p => p.OrderIndex).ToList();
				return _sortedDemoProducts;
			}
		}
		[XmlElement("Search")]
		public SearchModel Search {
			get { return _search; }
			set { _search = value; }
		}
		[XmlElement("GlobalHeader")]
		public GlobalHeaderModel GlobalHeader {
			get { return _globalHeader; }
			set { _globalHeader = value; }
		}
		[XmlAttribute]
		public bool ExpandAllDemosAtFirstTime {
			get { return _expandAllDemosAtFirstTime; }
			set { _expandAllDemosAtFirstTime = value; }
		}
		[XmlAttribute]
		public bool DisableTextWrap {
			get { return _disableTextWrap; }
			set { _disableTextWrap = value; }
		}
	}
	public class GlobalHeaderModel {
		string _logoPlatformSubject = "ASP.NET AJAX";
		string _logoPlatformDescription = "WHEN THE WEB MEANS BUSINESS";
		[XmlAttribute]
		public string LogoPlatformSubject {
			get { return _logoPlatformSubject; }
			set { _logoPlatformSubject = value; }
		}
		[XmlAttribute]
		public string LogoPlatformDescription {
			get { return _logoPlatformDescription; }
			set { _logoPlatformDescription = value; }
		}
	}
	public class DemoProductModel : ModelBase {
		bool _isMvc;
		bool _isMvcRazor;
		bool _isRootDemo;
		bool _isPreview;
		bool _isNew;
		string _key;
		string _url;
		string _title;
		string _seoTitle;
		string _navItemTitle;
		bool _supportsTheming = true;
		bool _supportsThemeParameters = true;
		bool _hideNavItem = false;
		bool _hideSourceCode;
		List<DemoGroupModel> _groups = new List<DemoGroupModel>();
		IntroPageModel _intro;
		OverviewPageModel _overview;
		int _orderIndex = 1000;
		bool _integrationHighlighted = false;
		string _integrationImageUrl;
		string _integrationDescription;
		string _downloadUrl;
		string _buyUrl;
		string _docUrl;
		List<DemoPageModel> _highlightedDemos;
		[XmlAttribute]
		public bool IsMvc {
			get { return _isMvc; }
			set { _isMvc = value; }
		}
		[XmlAttribute]
		public bool IsMvcRazor {
			get { return _isMvcRazor; }
			set { _isMvcRazor = value; }
		}
		[XmlAttribute]
		public bool IsRootDemo {
			get { return _isRootDemo; }
			set { _isRootDemo = value; }
		}
		[XmlAttribute]
		public bool IsPreview {
			get { return _isPreview; }
			set { _isPreview = value; }
		}
		[XmlAttribute]
		public bool IsNew {
			get { return _isNew; }
			set { _isNew = value; }
		}
		[XmlAttribute]
		public bool HideNavItem {
			get { return _hideNavItem; }
			set { _hideNavItem = value; }
		}
		[XmlAttribute]
		public string Key {
			get {
				if(_key == null)
					return "";
				return _key;
			}
			set { _key = value; }
		}
		[XmlAttribute]
		public string Url {
			get {
				if(_url == null)
					throw new Exception("URL is not defined");
				return _url;
			}
			set { _url = value; }
		}
		[XmlAttribute]
		public string Title {
			get {
				if(_title == null)
					return "";
				return _title;
			}
			set { _title = value; }
		}
		[XmlAttribute]
		public string SeoTitle {
			get {
				if(_seoTitle == null)
					return "";
				return _seoTitle;
			}
			set { _seoTitle = value; }
		}
		[XmlAttribute]
		public string NavItemTitle {
			get {
				if(_navItemTitle == null)
					return "";
				return _navItemTitle;
			}
			set { _navItemTitle = value; }
		}
		[XmlAttribute]
		public int OrderIndex {
			get { return _orderIndex; }
			set { _orderIndex = value; }
		}
		[XmlAttribute]
		public bool IntegrationHighlighted {
			get { return _integrationHighlighted; }
			set { _integrationHighlighted = value; }
		}
		[XmlElement]
		public string DownloadUrl {
			get {
				if(_downloadUrl == null)
					return "";
				return _downloadUrl;
			}
			set {
				if(value != null)
					value = value.Trim();
				_downloadUrl = value;
			}
		}
		[XmlElement]
		public string BuyUrl {
			get {
				if(_buyUrl == null)
					return "";
				return _buyUrl;
			}
			set {
				if(value != null)
					value = value.Trim();
				_buyUrl = value;
			}
		}
		[XmlElement]
		public string DocUrl {
			get {
				if(_docUrl == null)
					return "";
				return _docUrl;
			}
			set {
				if(value != null)
					value = value.Trim();
				_docUrl = value;
			}
		}
		[XmlElement]
		public string IntegrationImageUrl {
			get { return _integrationImageUrl; }
			set { _integrationImageUrl = value; }
		}
		[XmlElement]
		public string IntegrationDescription {
			get { return _integrationDescription; }
			set { _integrationDescription = value; }
		}
		[XmlElement("DemoGroup")]
		public List<DemoGroupModel> Groups {
			get { return _groups; }
		}
		[XmlAttribute]
		public bool SupportsTheming {
			get { return _supportsTheming; }
			set { _supportsTheming = value; }
		}
		[XmlAttribute]
		public bool SupportsThemeParameters {
			get { return _supportsThemeParameters; }
			set { _supportsThemeParameters = value; }
		}
		[XmlIgnore]
		public List<DemoPageModel> HighlightedDemos {
			get {
				if(_highlightedDemos == null)
					_highlightedDemos = CreateHighlightedDemos();
				return _highlightedDemos;
			}
		}
		[XmlElement(Type = typeof(IntroPageModel), ElementName = "Intro")]
		public IntroPageModel Intro {
			get { return _intro; }
			set { _intro = value; }
		}
		[XmlElement(Type = typeof(OverviewPageModel), ElementName = "Overview")]
		public OverviewPageModel Overview {
			get { return _overview; }
			set { _overview = value; }
		}
		[XmlIgnore]
		public bool IsCurrent {
			get { return this == DemosModel.Current; }
		}
		[XmlAttribute]
		public virtual bool HideSourceCode {
			get { return _hideSourceCode; }
			set { _hideSourceCode = value; }
		}
		List<DemoPageModel> CreateHighlightedDemos() {
			List<DemoPageModel> result = new List<DemoPageModel>();
			foreach(DemoGroupModel group in Groups) {
				foreach(DemoPageModel demo in group.Demos) {
					if(demo.HighlightedIndex > -1)
						result.Add(demo);
				}
			}
			result.Sort(CompareHighlightedDemos);
			return result;
		}
		int CompareHighlightedDemos(DemoModel x, DemoModel y) {
			return Comparer<int>.Default.Compare(x.HighlightedIndex, y.HighlightedIndex);
		}
		public string GetSeoTitle() {
			if(!string.IsNullOrEmpty(SeoTitle))
				return SeoTitle;
			return Title;
		}
	}
}
