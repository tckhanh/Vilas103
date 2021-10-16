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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using System.Drawing;
using DevExpress.Data.Extensions;
namespace DevExpress.Web.Demos {
	public class SourceCodePage {
		public string Title = "";
		public string Code = "";
		public bool Expanded = false;
		public SourceCodePage(string title, string code, bool expanded) {
			Title = title;
			Code = code;
			Expanded = expanded;
		}
	}
	public class FeaturedDemoInfo {
		public string Title {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public string NavigateUrl {
			get;
			set;
		}
		public string ImageUrl {
			get;
			set;
		}
	}
	public class ProductInfo {
		public string Title {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public string NavigateUrl {
			get;
			set;
		}
		public string ImageUrl {
			get;
			set;
		}
	}
	public static class Utils {
		internal const string
			CurrentDemoKey = "DXCurrentDemo",
			CurrentThemeAspCookieKeyPrefix = "DXCurrentTheme",
			CurrentThemeDevExtremeCookieKeyPrefix = "DXCurrentDevExtremeTheme",
			CurrentBaseColorCookieKeyPrefix = "DXCurrentBaseColor",
			CurrentFontCookieKey = "DXCurrentFont",
			TunableThemeCookieKeyPrefix = "DXTunableTheme",
			DefaultThemeName = "MaterialCompact",
			DevextremeDefaultThemeName = "light",
			BogusDemoTitle = "Delivered!",
			IsAccessibilityDemoKey = "IsAccessibilityDemo",
			ResourseFilePath = "DevExpress.Web.Demos.Properties.Resources",
			CorpColor = "#F78119";
		static readonly Dictionary<DemoModel, IEnumerable<SourceCodePage>> sourceCodeCache = new Dictionary<DemoModel, IEnumerable<SourceCodePage>>();
		static readonly object sourceCodeCacheLock = new object();
		static string _codeLanguage;
		static HttpContext Context {
			get {
				return HttpContext.Current;
			}
		}
		static HttpRequest Request {
			get {
				return Context.Request;
			}
		}
		public static bool IsMvc {
			get {
				return DemosModel.Current.IsMvc;
			}
		}
		public static bool IsIncludeThirdPartyClientLibraries {
			get {
				var resources = DevExpress.Web.Internal.ConfigurationSettings.Resources;
				return resources != null && resources.Contains(ResourcesType.ThirdParty);
			}
		}
		public static DemoModel CurrentDemo {
			get {
				return (DemoModel)Context.Items[CurrentDemoKey];
			}
		}
		public static DemoProductModel RootProduct {
			get {
				return DemosModel.Instance.SortedDemoProducts.Find(p => p.IsRootDemo);
			}
		}
		public static string CurrentDemoNodeName {
			get {
				if(IsOverview && CurrentOverview.Group == null)
					return string.Format("{0}_{1}", CurrentOverview.Product.Key, CurrentOverview.Key);
				if(CurrentDemoPage != null)
					return string.Format("{0}_{1}_{2}", CurrentDemoPage.Product.Key, CurrentDemoPage.Group.Key, CurrentDemoPage.Key);
				if(CurrentDemo != null)
					return CurrentDemo.Product.Key;
				return null;
			}
		}
		public static string CurrentDemoTitleHtml {
			get {
				return GetDemoTitleHtml(CurrentDemo);
			}
		}
		public static IntroPageModel CurrentIntro {
			get {
				if(CurrentDemo is IntroPageModel || CurrentDemo == null)
					return (IntroPageModel)CurrentDemo;
				return CurrentDemo.Product.Intro;
			}
		}
		public static OverviewPageModel CurrentOverview {
			get {
				if(CurrentDemo is OverviewPageModel || CurrentDemo == null)
					return (OverviewPageModel)CurrentDemo;
				return CurrentDemo.Product.Overview;
			}
		}
		public static DemoPageModel CurrentDemoPage {
			get {
				return CurrentDemo as DemoPageModel;
			}
		}
		public static string CurrentThemeAspCookieKey {
			get {
				return CurrentThemeAspCookieKeyPrefix;
			}
		}
		public static string CurrentThemeDevExtremeCookieKey {
			get {
				return CurrentThemeDevExtremeCookieKeyPrefix;
			}
		}
		public static string CurrentBaseColorCookieKey {
			get {
				return CurrentBaseColorCookieKeyPrefix;
			}
		}
		public static string TunableThemeCookieKey {
			get {
				return TunableThemeCookieKeyPrefix;
			}
		}
		public static bool IsLargeTheme {
			get {
				return ThemesProvider.IsLargeTheme(CurrentTheme);
			}
		}
		public static string CurrentTheme {
			get {
				string storedTheme;
				if(CanChangeTheme && TryGetStoredTheme(out storedTheme))
					return storedTheme;
				return !DemoHelper.Instance.UseDevExtremeThemeSelector ? DefaultThemeName : DevextremeDefaultThemeName;
			}
		}
		public static string CurrentStoredTheme(string cookieKey) {
			return Request.Cookies[cookieKey] != null ? HttpUtility.UrlDecode(Request.Cookies[cookieKey].Value) : Utils.CurrentTheme;
		}
		public static ThemesModelBase CurrentThemesModel {
			get {
				if(!DemoHelper.Instance.UseDevExtremeThemeSelector) {
					return ThemesModel.Current;
				} else {
					return DevExtremeThemesModel.Current;
				}
			}
		}
		public static string CurrentThemeCookieKey {
			get {
				if(!DemoHelper.Instance.UseDevExtremeThemeSelector) {
					return CurrentThemeAspCookieKey;
				} else {
					return CurrentThemeDevExtremeCookieKey;
				}
			}
		}
		public static bool TryGetStoredTheme(out string storedTheme) {
			storedTheme = null;
			if(Request.Cookies[CurrentThemeCookieKey] != null) {
				var rawTheme = HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
				if(CurrentThemesModel.GetAllThemes().Any(t => t.Name == rawTheme)) {
					storedTheme = rawTheme;
					return true;
				} else {
					Request.Cookies.Remove(CurrentThemeCookieKey);
					Context.Response.Cookies[CurrentThemeCookieKey].Expires = DateTime.Now.AddDays(-1);
				}
			}
			return false;
		}
		public static string CurrentBaseColor {
			get {
				if(Request.Cookies[CurrentBaseColorCookieKey] != null)
					return HttpUtility.UrlDecode(Request.Cookies[CurrentBaseColorCookieKey].Value);
				if(Request.Cookies[CurrentThemeAspCookieKey] == null)
					return CorpColor;
				return CurrentThemeDefaultBaseColor;
			}
		}
		static void SetCurrentBaseColorCookie(string value) {
			if(string.IsNullOrWhiteSpace(value))
				Request.Cookies.Remove(CurrentBaseColorCookieKey);
			else
				Request.Cookies[CurrentBaseColorCookieKey].Value = value;
			Context.Response.Cookies[CurrentBaseColorCookieKey].Value = value;
		}
		public static string CurrentFont {
			get {
				if(Request.Cookies[CurrentFontCookieKey] != null)
					return HttpUtility.UrlDecode(Request.Cookies[CurrentFontCookieKey].Value);
				return CurrentThemeDefaultFont;
			}
		}
		static void SetCurrentFontCookie(string value) {
			Context.Response.Cookies[CurrentFontCookieKey].Value = value;
		}
		public static string CurrentThemeTitle {
			get {
				var theme = CurrentTheme;
				var themeModel = (CurrentThemesModel.GetThemeModel(theme));
				return themeModel != null ? themeModel.Title : theme;
			}
		}
		static string TunableTheme {
			get {
				if(Request.Cookies[TunableThemeCookieKey] != null)
					return HttpUtility.UrlDecode(Request.Cookies[TunableThemeCookieKey].Value);
				return CurrentTheme;
			}
			set {
				Context.Response.Cookies[TunableThemeCookieKey].Value = value;
			}
		}
		public static bool IsIntro {
			get {
				return Utils.CurrentDemo is IntroPageModel;
			}
		}
		public static bool IsPageRequest {
			get {
				return Request.Url.AbsolutePath.ToLower().Contains(".aspx");
			}
		}
		public static bool IsOverview {
			get {
				return Utils.CurrentDemo is OverviewPageModel;
			}
		}
		public static bool IsBogusDemo {
			get {
				return CurrentDemo != null ? CurrentDemo.Title == BogusDemoTitle : false;
			}
		}
		public static string GetDemoTitleHtml(DemoModel demo) {
			string result = string.Empty;
			if(demo is DemoPageModel)
				result = string.Format("{0} - {1}", ((DemoPageModel)demo).Group.Title, demo.Title);
			if(string.IsNullOrEmpty(result))
				result = demo.Title;
			else if(result.Length > 60)
				result = demo.Title;
			return HttpUtility.HtmlEncode(result);
		}
		public static string CodeLanguage {
			get {
				if(_codeLanguage == null) {
					try {
						using(FileStream _file = File.OpenRead(Context.Server.MapPath("~/Site.master")))
						using(TextReader reader = new StreamReader(_file)) {
							string line = reader.ReadLine();
							Match match = Regex.Match(line, @"language=""([^""]+)", RegexOptions.IgnoreCase);
							if(match.Success) {
								_codeLanguage = match.Groups[1].Value.ToUpper();
							}
						}
					} catch {
					}
					if(String.IsNullOrEmpty(_codeLanguage))
						_codeLanguage = "C#";
				}
				return _codeLanguage;
			}
		}
		public static IEnumerable<SourceCodePage> GetCurrentSourceCodePages() {
			return GetSourceCodePages(CurrentDemo as DemoPageModel);
		}
		public static IEnumerable<SourceCodePage> GetSourceCodePages(DemoPageModel demo) {
			lock(sourceCodeCacheLock) {
				if(!sourceCodeCache.ContainsKey(demo))
					sourceCodeCache[demo] = CreateSourceCodePages(demo);
				return sourceCodeCache[demo];
			}
		}
		static IEnumerable<SourceCodePage> CreateSourceCodePages(DemoPageModel demo) {
			List<SourceCodePage> result = new List<SourceCodePage>();
			if(IsMvc) {
				FillMvcModelPages(demo, result);
				FillMvcControllerPages(demo, result);
				FillMvcViewPages(demo, result);
				FillMvcOtherPages(demo, result);
			} else {
				var baseUrl = GenerateDemoUrl(demo);
				var highlightedTagNames = GetHighlightedTagNames(demo);
				AddSourceCodePage(result, "ASPX", baseUrl, true, true, highlightedTagNames);
				if(HasCodeFile(baseUrl)) {
					AddSourceCodePage(result, "C#", baseUrl + ".cs", CodeLanguage == "C#");
					AddSourceCodePage(result, "VB", baseUrl + ".vb", CodeLanguage == "VB");
				}
				foreach(string fileName in demo.SourceFiles)
					AddSourceCodePage(result, Path.GetFileName(fileName), fileName, false, true, highlightedTagNames);
			}
			return result;
		}
		static string[] GetHighlightedTagNames(DemoPageModel demo) {
			var highlightedTagNames = new string[0];
			if(!IsOverview)
				highlightedTagNames = demo.Group.GetHighlightedTagNames().
									  Concat(demo.GetHighlightedTagNames()).
									  Concat(demo.Product.GetHighlightedTagNames()).ToArray();
			return highlightedTagNames;
		}
		static void FillMvcModelPages(DemoPageModel demo, List<SourceCodePage> result) {
			FillMvcModelFromResources(demo, result);
			foreach(string fileName in demo.SourceFiles) {
				if(fileName.StartsWith("~/Models/"))
					AddSourceCodePage(result, string.Format("Model ({0})", Path.GetFileNameWithoutExtension(fileName)), fileName, false);
			}
		}
		public static void FillMvcModelFromResources(DemoPageModel demo, List<SourceCodePage> result) {
			foreach(string key in demo.ResourceKeys) {
				var keyParts = key.Split('_');
				if(keyParts[0].Equals("Model") || keyParts[0].Equals("DataProvider"))
					AddSourceCodePage(result, string.Format("Model ({0})", keyParts[1]), key, false, true);
			}
		}
		public static string GetDemoGroupKey(DemoPageModel demo) {
			if(!string.IsNullOrEmpty(demo.VirtualGroupKey))
				return demo.VirtualGroupKey;
			var group = demo.Group;
			if(!string.IsNullOrEmpty(group.VirtualGroupKey))
				return group.VirtualGroupKey;
			return group.Key;
		}
		static void FillMvcControllerPages(DemoPageModel demo, List<SourceCodePage> result) {
			string controllerUrl = string.Format("~/Controllers/{0}/{0}Controller.{1}.cs", GetDemoGroupKey(demo), demo.Key);
			AddSourceCodePageIfExists(result, "Controller", controllerUrl, true);
			string commonControllerUrl = string.Format("~/Controllers/{0}Controller.cs", GetDemoGroupKey(demo));
			AddSourceCodePageIfExists(result, "Controller (common)", commonControllerUrl, false);
		}
		static void FillMvcViewPages(DemoPageModel demo, List<SourceCodePage> result) {
			string viewUrl = string.Format("~/Views/{0}/{1}.cshtml", GetDemoGroupKey(demo), demo.Key);
			AddSourceCodePage(result, "View", viewUrl, true, false);
		}
		static void FillMvcOtherPages(DemoPageModel demo, List<SourceCodePage> result) {
			FillMvcOtherPagesFromResources(demo, result);
			foreach(string fileName in demo.SourceFiles) {
				if(fileName.StartsWith("~/Views/"))
					AddSourceCodePage(result, string.Format("View ({0})", Path.GetFileNameWithoutExtension(fileName)), fileName, true);
				else if(!fileName.StartsWith("~/Models/"))
					AddSourceCodePage(result, Path.GetFileName(fileName), fileName, true);
			}
		}
		public static void FillMvcOtherPagesFromResources(DemoPageModel demo, List<SourceCodePage> result) {
			foreach(string key in demo.ResourceKeys) {
				var keyParts = key.Split('_');
				if(keyParts[0].Equals("Code"))
					AddSourceCodePage(result, keyParts[1], key, false, true);
			}
		}
		static void AddSourceCodePageIfExists(List<SourceCodePage> list, string title, string url, bool expanded) {
			AddSourceCodePage(list, title, url, expanded, false, new string[0], false);
		}
		static void AddSourceCodePage(List<SourceCodePage> list, string title, string url, bool expanded, bool isResource = false) {
			AddSourceCodePage(list, title, url, expanded, true, new string[0], isResource);
		}
		static void AddSourceCodePage(List<SourceCodePage> list, string title, string url, bool expanded, bool showIfError, bool isResource = false) {
			AddSourceCodePage(list, title, url, expanded, showIfError, new string[0], isResource);
		}
		static void AddSourceCodePage(List<SourceCodePage> list, string title, string url, bool expanded, bool showIfError, string[] highlightedTagNames, bool isResource = false) {
			string content = string.Empty;
			try {
				content = GetHighlightedFileContent(url, highlightedTagNames, isResource);
			} catch {
				content = showIfError ? "Error rendering source code" : string.Empty;
			}
			if(!string.IsNullOrEmpty(content))
				list.Add(new SourceCodePage(title, content, expanded));
		}
		static bool HasCodeFile(string url) {
			string filePath = GetHighlightedFilePath(url);
			if(!File.Exists(filePath))
				return false;
			string text = File.ReadAllText(filePath);
			return Regex.IsMatch(text, @"<[^>]*\bCodeFile\s*=\s*""[\w\.]+\""[^>]*>");
		}
		static string GetHighlightedFileContent(string virtualPath, string[] highlightedTagNames, bool isResource) {
			string text = string.Empty;
			string filePath = virtualPath;
			if(!isResource) {
				filePath = GetHighlightedFilePath(virtualPath);
				text = File.ReadAllText(filePath);
			} else {
				var resourceManager = new System.Resources.ResourceManager(ResourseFilePath, Assembly.GetExecutingAssembly());
				text = resourceManager.GetString(virtualPath);
			}
			return CodeFormatter.GetFormattedCode(Path.GetExtension(filePath.Replace("_", ".")), text, IsMvc, highlightedTagNames);
		}
		static string GetHighlightedFilePath(string virtualPath) {
			string result = new DirectoryInfo(Context.Server.MapPath("~/")).FullName;
			result = Path.Combine(result, ".Source");
			result = Path.Combine(result, virtualPath.Substring(2));
			if(File.Exists(result))
				return result;
			result = Context.Server.MapPath(virtualPath);
			if(!File.Exists(result))
				result = CorrectSourceFilePath(result);
			return result;
		}
		static string CorrectSourceFilePath(string filePath) {
			string csPathItem = String.Format("{0}cs{0}", Path.DirectorySeparatorChar);
			string vbPathItem = String.Format("{0}vb{0}", Path.DirectorySeparatorChar);
			filePath = filePath.ToLower();
			if(filePath.EndsWith(".cs"))
				return filePath.Replace(vbPathItem, csPathItem);
			if(filePath.EndsWith(".vb"))
				return filePath.Replace(csPathItem, vbPathItem);
			return filePath;
		}
		public static string GetVersionText() {
			string[] parts = AssemblyInfo.Version.Split('.');
			return string.Format("v{0} vol {1}.{2}", 2000 + int.Parse(parts[0]), parts[1], parts[2]);
		}
		public static string GetVersionSuffix() {
			return AssemblyInfo.Version.Replace(".", "_");
		}
		public static void FindDemoKeysByVirtualPath(string virtualPath, out string groupKey, out string demoKey) {
			string path = virtualPath.ToLower().Replace("~/", "").Replace(".aspx", "").Trim('/');
			string[] parts = path.Split('/');
			if(parts.Length < 1)
				throw new ArgumentException("Invalid demo URL");
			if(parts.Length > 1) {
				groupKey = parts[parts.Length - 2];
				demoKey = parts[parts.Length - 1];
			} else {
				groupKey = "";
				demoKey = parts[0];
			}
		}
		public static void RegisterCurrentWebFormsDemo(Page page) {
			string groupKey = "";
			string demoKey = "";
			FindDemoKeysByVirtualPath(page.AppRelativeVirtualPath, out groupKey, out demoKey);
			RegisterCurrentDemo(groupKey, demoKey);
		}
		public static void RegisterCurrentMvcDemoOnCallback() {
			string groupKey, demoKey;
			string controllerName = Request.RequestContext.RouteData.Values["controller"].ToString();
			FindDemoKeysByRoute(controllerName, Request.UrlReferrer.Segments, Request.AppRelativeCurrentExecutionFilePath, out groupKey, out demoKey);
			RegisterCurrentMvcDemo(groupKey, demoKey);
		}
		public static void FindDemoKeysByRoute(string controllerName, string[] referrerSegments, string appRelativeCurrentExecutionFilePath, out string groupKey, out string demoKey) {
			groupKey = controllerName;
			demoKey = string.Empty;
			var controllerNamePartIndex = referrerSegments.FindIndex(part => string.Equals(part.Trim('/'), controllerName, StringComparison.InvariantCultureIgnoreCase));
			if(controllerNamePartIndex > -1 && appRelativeCurrentExecutionFilePath.Contains(controllerName)) {
				if(string.Equals(controllerName, "overview", StringComparison.InvariantCultureIgnoreCase)) {
					demoKey = controllerName;
					groupKey = string.Empty;
				} else if(referrerSegments.Length > controllerNamePartIndex + 1)
					demoKey = referrerSegments[controllerNamePartIndex + 1];
			}
			demoKey = !string.IsNullOrEmpty(demoKey) ? demoKey : "Index";
		}
		public static void RegisterCurrentMvcDemo(string controllerName, string actionName) {
			RegisterCurrentDemo(controllerName, actionName);
		}
		public static bool IsIntroPage(string groupKey, string demoKey) {
			if(IsMvc)
				return groupKey.Equals("Home", StringComparison.InvariantCultureIgnoreCase) && demoKey.Equals("Index", StringComparison.InvariantCultureIgnoreCase);
			return demoKey.Equals("default", StringComparison.InvariantCultureIgnoreCase);
		}
		public static bool IsOverviewPage(string demoKey) {
			return demoKey.Equals("overview", StringComparison.InvariantCultureIgnoreCase);
		}
		static void RegisterCurrentDemo(string groupKey, string demoKey) {
			DemoModel demo = null;
			if(IsIntroPage(groupKey, demoKey))
				demo = DemosModel.Current.Intro;
			else if(IsOverviewPage(demoKey) && String.IsNullOrEmpty(groupKey))
				demo = DemosModel.Current.Overview;
			else if(IsErrorPage(demoKey)) {
				demo = CreateErrorPageDemoModel();
			} else {
				demo = DemosModel.Instance.FindDemo(DemosModel.Current.Key, groupKey, demoKey);
			}
			if(demo == null)
				demo = CreateBogusDemoModel();
			Context.Items[CurrentDemoKey] = demo;
			DevExpress.Web.Internal.DemoUtils.RegisterDemo(DemosModel.Current.Key, groupKey, demoKey);
		}
		static DemoPageModel CreateBogusDemoModel() {
			DemoGroupModel group = new DemoGroupModel();
			group.Title = "ASP.NET";
			DemoPageModel result = new DemoPageModel();
			result.Group = group;
			result.HideSourceCode = true;
			result.Title = BogusDemoTitle;
			return result;
		}
		static DemoPageModel CreateErrorPageDemoModel() {
			DemoPageModel result = new DemoPageModel {
				IsErrorPage = true,
				Product = DemosModel.Current,
				Group = new DemoGroupModel()
			};
			return result;
		}
		public static string GetCurrentDemoPageTitle() {
			StringBuilder builder = new StringBuilder();
			if(CurrentDemo.IsErrorPage) {
				builder.Append("Error Page - ");
				builder.Append(DemosModel.Current.GetSeoTitle());
				if(!DemosModel.Current.IsRootDemo)
					builder.Append(" Demo");
			} else if(CurrentDemo is IntroPageModel) {
				builder.Append(CurrentDemo.SeoTitle);
			} else if(CurrentDemo is DemoPageModel) {
				string product = DemosModel.Current.GetSeoTitle();
				DemoGroupModel demoGroup = ((DemoPageModel)CurrentDemo).Group;
				string group = demoGroup != null ? demoGroup.SeoTitle : null;
				builder.Append(CurrentDemo.GetSeoTitle());
				builder.Append(" - ");
				builder.Append(string.IsNullOrEmpty(group) ? product : group);
				builder.Append(" Demo");
			}
			builder.Append(" | DevExpress");
			return builder.ToString();
		}
		public static string GetDemoNavigationTitle() {
			string result = Utils.CurrentDemo.Product.NavItemTitle;
			if(Utils.CurrentDemoPage.Group != null)
				result += " - " + Utils.CurrentDemoPage.Group.Title;
			return result;
		}
		public static void RegisterCssLink(Page page, string url) {
			RegisterCssLink(page, url, null);
		}
		public static string GetDescriptionTitle() {
			if(Utils.CurrentOverview != null && !string.IsNullOrEmpty(Utils.CurrentOverview.DescriptionTitle))
				return Utils.CurrentOverview.DescriptionTitle;
			return string.Format("About the {0}", Utils.CurrentDemoPage.Group == null ? Utils.CurrentDemo.Product.NavItemTitle : Utils.CurrentDemoPage.Group.Title);
		}
		public static void RegisterCssLink(Page page, string url, string clientId) {
			if(IsMvc)
				throw new NotImplementedException();
			HtmlLink link = new HtmlLink();
			page.Header.Controls.Add(link);
			link.EnableViewState = false;
			link.Attributes["type"] = "text/css";
			link.Attributes["rel"] = "stylesheet";
			if(!string.IsNullOrEmpty(clientId))
				link.Attributes["id"] = clientId;
			link.Href = url;
		}
		public static string GenerateDemoUrl(DemoModel demo) {
			if(!string.IsNullOrEmpty(demo.HighlightedLink))
				return demo.HighlightedLink;
			StringBuilder str = new StringBuilder();
			if(demo.Product.IsCurrent) {
				str.Append("~/");
			} else {
				var url = HttpContext.Current.Request.Url.AbsolutePath;
				var productUrl = "/" + CurrentDemo.Product.Url;
				url = url.Substring(0, url.IndexOf(productUrl, StringComparison.InvariantCultureIgnoreCase) + 1);
				str.AppendFormat("{0}{1}/", url, demo.Product.Url);
			}
			DemoPageModel demoPage = demo as DemoPageModel;
			bool hasDemoVirtualGroupKey = demoPage != null && !string.IsNullOrEmpty(demoPage.VirtualGroupKey);
			bool hasGroupKey = demoPage != null && demoPage.Group != null && !string.IsNullOrEmpty(demoPage.Group.Key);
			if(hasDemoVirtualGroupKey || hasGroupKey) {
				str.Append(GetDemoGroupKey(demoPage));
				str.Append("/");
			}
			if(!IsMvc || !string.Equals("Index", demo.Key))
				str.Append(demo.Key);
			if(!IsMvc)
				str.Append(".aspx");
			return str.ToString();
		}
		public static List<FeaturedDemoInfo> GenerateFeaturedDemos() {
			var result = new List<FeaturedDemoInfo>();
			foreach(var demo in DemosModel.Current.HighlightedDemos) {
				result.Add(new FeaturedDemoInfo {
					Title = demo.GetHighlightedTitle(),
					ImageUrl = demo.HighlightedImageUrl,
					NavigateUrl = GenerateDemoUrl(demo),
					Description = demo.HighlightedDescription
				});
			}
			if(Utils.CurrentIntro != null) {
				foreach(var demo in Utils.CurrentIntro.ExternalDemos) {
					result.Add(new FeaturedDemoInfo {
						Title = demo.Title,
						ImageUrl = demo.ImageUrl,
						NavigateUrl = demo.Url
					});
				}
			}
			return result;
		}
		public static List<ProductInfo> GenerateProductDemos(bool highlited) {
			var result = new List<ProductInfo>();
			foreach(var item in DemosModel.Instance.SortedDemoProducts.Where(p => !p.HideNavItem && !p.IsRootDemo && p.IntegrationHighlighted == highlited)) {
				result.Add(new ProductInfo() {
					NavigateUrl = GenerateDemoUrl(item.Intro),
					ImageUrl = item.IntegrationImageUrl,
					Description = item.IntegrationDescription,
					Title = item.NavItemTitle
				});
			}
			return result;
		}
		public static void EnsureRequestValidationMode() {
			try {
				if(Environment.Version.Major >= 4) {
					Type type = typeof(WebControl).Assembly.GetType("System.Web.Configuration.RuntimeConfig");
					MethodInfo getConfig = type.GetMethod("GetConfig", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { }, null);
					object runtimeConfig = getConfig.Invoke(null, null);
					MethodInfo getSection = type.GetMethod("GetSection", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string), typeof(Type) }, null);
					HttpRuntimeSection httpRuntimeSection = (HttpRuntimeSection)getSection.Invoke(runtimeConfig, new object[] { "system.web/httpRuntime", typeof(HttpRuntimeSection) });
					FieldInfo bReadOnly = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
					bReadOnly.SetValue(httpRuntimeSection, false);
					PropertyInfo pi = typeof(HttpRuntimeSection).GetProperty("RequestValidationMode");
					if(pi != null) {
						Version version = (Version)pi.GetValue(httpRuntimeSection, null);
						Version RequiredRequestValidationMode = new Version(2, 0);
						if(version != null && !Version.Equals(version, RequiredRequestValidationMode)) {
							pi.SetValue(httpRuntimeSection, RequiredRequestValidationMode, null);
						}
					}
					bReadOnly.SetValue(httpRuntimeSection, true);
				}
			} catch {
			}
		}
		static bool? _isSiteMode;
		public static bool IsSiteMode {
			get {
				if(!_isSiteMode.HasValue) {
					_isSiteMode = ConfigurationManager.AppSettings["SiteMode"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
				}
				return _isSiteMode.Value;
			}
		}
		static bool? _patchConnectionStrings;
		public static bool PatchConnectionStrings {
			get {
				if(!_patchConnectionStrings.HasValue) {
					string appSettingsValue = ConfigurationManager.AppSettings["PatchConnectionStrings"];
					_patchConnectionStrings = appSettingsValue != null && appSettingsValue.Equals("true", StringComparison.InvariantCultureIgnoreCase);
				}
				return _patchConnectionStrings.Value;
			}
		}
		public static string GetReadOnlyMessageHtml() {
			return String.Format(
				"<b>Data modifications are not allowed in the online demo.</b><br />" +
				"If you need to test data editing functionality, please install " +
				"{0} on your machine and run the demo locally.", DemosModel.Current.Title);
		}
		public static string GetReadOnlyMessageText() {
			return String.Format(
				"Data modifications are not allowed in the online demo.\n" +
				"If you need to test data editing functionality, please install \n" +
				"{0} on your machine and run the demo locally.", DemosModel.Current.Title);
		}
		public static void AssertNotReadOnly() {
			if(!IsSiteMode)
				return;
			throw new DemoException(GetReadOnlyMessageHtml());
		}
		public static void AssertNotReadOnlyText() {
			if(!IsSiteMode)
				return;
			throw new DemoException(GetReadOnlyMessageText());
		}
		public static bool CanChangeTheme {
			get {
				return !IsIntro && !IsIE6() && DemosModel.Current.SupportsTheming && !DemoHelper.Instance.SuppressThemeSelector;
			}
		}
		public static bool CanChangeBaseColor {
			get {
				return CurrentThemesModel.GetAllGroups().Where(g => g.Themes.Where(t => t.Name == CurrentTheme && !String.IsNullOrEmpty(t.BaseColor)).Count() != 0).Count() > 0;
			}
		}
		public static bool CanApplyThemeParameters {
			get {
				return DemosModel.Current.SupportsThemeParameters && (!string.IsNullOrEmpty(CurrentThemeDefaultFont) || !string.IsNullOrEmpty(CurrentThemeDefaultBaseColor));
			}
		}
		public static string CurrentThemeDefaultFont { get { return CurrentThemeModel.Font; } }
		public static string CurrentThemeDefaultFontSize { get { return CurrentThemeModel.FontSize; } }
		public static string CurrentThemeDefaultBaseColor { get { return CurrentThemeModel.BaseColor; } }
		static ThemeModel CurrentThemeModel {
			get { return CurrentThemesModel.GetAllGroups().SelectMany(g => g.Themes).FirstOrDefault(t => t.Name == CurrentTheme); }
		}
		public static string[] CustomFonts {
			get {
				return new string[] {
					CurrentThemeDefaultFont,
					CurrentThemeDefaultFontSize + " " + "'Asap', normal",
					CurrentThemeDefaultFontSize + " " + "'Arima Madurai', normal",
					CurrentThemeDefaultFontSize + " " + "'Comfortaa', normal"
				};
			}
		}
		public static List<ListEditItem> GetFontFamiliesDataSource() {
			return CustomFonts.Select(f => new ListEditItem() { Text = GetShortFontName(f), Value = f }).ToList();
		}
		static string GetShortFontName(string fullName) {
			if(string.IsNullOrWhiteSpace(fullName))
				return fullName;
			int indexOfFirstSpace = fullName.IndexOf(' ');
			int indexOfFirstComma = fullName.IndexOf(',');
			int endPosition = indexOfFirstComma > -1 ? indexOfFirstComma - 1 : fullName.Length - 1;
			return fullName.Substring(indexOfFirstSpace + 1, endPosition - indexOfFirstSpace).Trim('\'');
		}
		public static string[] CustomBaseColors {
			get {
				return new string[] {
					GetFirstItemForBaseColorList(),
					"#4796CE",
					"#35B86B",
					"#CE5B47",
					CorpColor,
					"#9F47CE",
					"#5C57C9",
					"#CE4776",
				};
			}
		}
		static string GetFirstItemForBaseColorList() {
			if(CurrentTheme.Equals("MaterialCompact")) {
				return (CurrentThemesModel.GetThemeModel("MaterialCompact")).BaseColor;
			}
			return CurrentThemeDefaultBaseColor;
		}
		public static string GetDefaultBaseColor(string themeName) {
			return (CurrentThemesModel.GetThemeModel(themeName)).BaseColor;
		}
		public static string EncodeSvgIconBase64(string svg) {
			var svgEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
			return "data:image/svg+xml;base64," + svgEncoded;
		}
		public static string GetColoredSquareUrl(string color) {
			var svg = "<svg xmlns='http://www.w3.org/2000/svg'><g><rect height='100%' width='100%' y='0' x='0' fill='" +
				color + "'/></g></svg>";
			return EncodeSvgIconBase64(svg);
		}
		public static string GetCurrentResource(string resourceName) {
			using(Stream stream = ResourcesUtils.GetEmbeddedResource(resourceName)) {
				stream.Position = 0;
				using(StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
					return reader.ReadToEnd();
				}
			}
		}
		public static void ResolveThemeParametes() {
			if(!CanChangeTheme || !DemosModel.Current.SupportsThemeParameters)
				return;
			string baseColor = CurrentBaseColor;
			string font = CurrentFont;
			if(IsThemeChanged || !string.IsNullOrEmpty(baseColor) && !CustomBaseColors.Contains(baseColor) || baseColor == CurrentThemeDefaultBaseColor) {
				baseColor = string.Empty;
				SetCurrentBaseColorCookie(baseColor);
			}
			if(IsThemeChanged || !string.IsNullOrEmpty(font) && !CustomFonts.Contains(font) || font == CurrentThemeDefaultFont) {
				font = string.Empty;
				SetCurrentFontCookie(font);
			}
			TunableTheme = CurrentTheme;
			ASPxWebControl.GlobalThemeBaseColor = baseColor;
			ASPxWebControl.GlobalThemeFont = font; 
		}
		public static void ResetThemeParameters() {
			ASPxWebControl.GlobalThemeBaseColor = null;
			ASPxWebControl.GlobalThemeFont = null;
		}
		static bool IsThemeChanged {
			get {
				return CurrentTheme != TunableTheme;
			}
		}
		public static string GetAccessibilityAttribute(string attributeName, string value) {
			if(IsAccessibilityDemo)
				return String.Format("{0}=\"{1}\"", attributeName, value);
			return string.Empty;
		}
		public static bool IsAccessibilityDemo {
			get {
				if(Request == null)
					return false;
				string demoUrl = Request.Url.AbsolutePath.ToLower();
				return demoUrl.IndexOf("compliance") != -1 || (demoUrl.IndexOf("accessibility") != -1 && demoUrl.IndexOf("linkedcontrols") != -1);
			}
		}
		public static void InjectDescriptionMeta(Control parent) {
			if(String.IsNullOrEmpty(Utils.CurrentDemo.MetaDescription))
				return;
			Page page = parent as Page;
			HtmlHead header = (page != null && page.Header != null) ? page.Header : RenderUtils.FindHead(parent);
			if(header != null) {
				LiteralControl metaControl = new LiteralControl(string.Format("<meta name=\"description\" content=\"{0}\" />", Utils.CurrentDemo.MetaDescription));
				header.Controls.AddAt(0, metaControl);
			}
		}
		public static bool IsIE6() {
			return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version < 7;
		}
		public static bool IsIE9() {
			return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version > 8;
		}
		public static bool IsIE10() {
			return RenderUtils.Browser.IsIE && RenderUtils.Browser.Version > 9;
		}
		public static string EncodeUrl(string url) {
			return Uri.EscapeUriString(url.Trim());
		}
		public static string FormatSize(object value) {
			double amount = Convert.ToDouble(value);
			string unit = "KB";
			if(amount != 0) {
				if(amount <= 1024)
					amount = 1;
				else
					amount /= 1024;
				if(amount > 1024) {
					amount /= 1024;
					unit = "MB";
				}
				if(amount > 1024) {
					amount /= 1024;
					unit = "GB";
				}
			}
			return String.Format("{0:#,0} {1}", Math.Round(amount, MidpointRounding.AwayFromZero), unit);
		}
		public static void PrepareSecurityProtocol() {
			System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
		}
		private static bool IsErrorPage(string demoKey) {
			return demoKey.Equals("Error404", StringComparison.OrdinalIgnoreCase) ||
				demoKey.Equals("Error500", StringComparison.OrdinalIgnoreCase);
		}
	}
	public class ErrorMessageModel {
		public string Title { get; set; }
		public string NavigateUrl { get; set; }
	}
	public class DemoException : Exception {
		public DemoException(string message) : base(message) { }
	}
	public static class ExtensionMethods {
		public static string GetUrlWithVersionSuffix(this Control control, string url) {
			return string.Format("{0}?v={1}", control.ResolveClientUrl(url), Utils.GetVersionSuffix());
		}
	}
}
