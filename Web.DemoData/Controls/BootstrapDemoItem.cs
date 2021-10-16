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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace DevExpress.Web.Demos {
	public class BootstrapDemoItemBase : Panel {
		#region Unnecessary properties
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ClientIDMode ClientIDMode { get { return base.ClientIDMode; } set { base.ClientIDMode = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableViewState { get { return base.EnableViewState; } set { base.EnableViewState = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ViewStateMode ViewStateMode { get { return base.ViewStateMode; } set { base.ViewStateMode = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible { get { return base.Visible; } set { base.Visible = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string AccessKey { get { return base.AccessKey; } set { base.AccessKey = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor { get { return base.BackColor; } set { base.BackColor = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BorderColor { get { return base.BorderColor; } set { base.BorderColor = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit BorderWidth { get { return base.BorderWidth; } set { base.BorderWidth = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override BorderStyle BorderStyle { get { return base.BorderStyle; } set { base.BorderStyle = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string CssClass { get { return base.CssClass; } set { base.CssClass = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled { get { return base.Enabled; } set { base.Enabled = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming { get { return base.EnableTheming; } set { base.EnableTheming = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font { get { return base.Font; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Height { get { return base.Height; } set { base.Height = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID { get { return base.SkinID; } set { base.SkinID = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override short TabIndex { get { return base.TabIndex; } set { base.TabIndex = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToolTip { get { return base.ToolTip; } set { base.ToolTip = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Width { get { return base.Width; } set { base.Width = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string BackImageUrl { get { return base.BackImageUrl; } set { base.BackImageUrl = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string DefaultButton { get { return base.DefaultButton; } set { base.BackImageUrl = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ContentDirection Direction { get { return base.Direction; } set { base.Direction = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string GroupingText { get { return base.GroupingText; } set { base.GroupingText = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override HorizontalAlign HorizontalAlign { get { return base.HorizontalAlign; } set { base.HorizontalAlign = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ScrollBars ScrollBars { get { return base.ScrollBars; } set { base.ScrollBars = value; } }
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Wrap { get { return base.Wrap; } set { base.Wrap = value; } }
		#endregion
	}
	public class BootstrapDemoOverviewItem : BootstrapDemoItemBase {
		public BootstrapDemoGroupPageModel OverViewPage {
			get { return BootstrapUtils.CurrentDemo as BootstrapDemoGroupPageModel; }
		}
		public string Title { get { return OverViewPage != null ? OverViewPage.Title : "Demo Title"; } }
		public string PreDescription { get { return OverViewPage != null ? OverViewPage.GetProcessedPreDescription() : "PreDescription"; } }
		public string Description { get { return OverViewPage != null ? OverViewPage.GetProcessedDescription() : "Description"; } }
		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender(e);
			if(!DesignMode && OverViewPage == null)
				throw new Exception("The BootstrapDemoOverviewItem with ID='" + ID + "' doesn't have a corresponding DemoGroup in the Demos.xml file.");
		}
		protected override void Render(HtmlTextWriter writer) {
			writer.RenderBeginTag(HtmlTextWriterTag.H2);
			writer.WriteEncodedText(string.Format("{0} - Overview", Title));
			writer.RenderEndTag();
			writer.Write(PreDescription);
			RenderContents(writer);
			writer.Write(Description);
		}
	}
	[ControlBuilder(typeof(BootstrapDemoItemControlBuilder))]
	public class BootstrapDemoItem : BootstrapDemoItemBase {
		public BootstrapDemoItem() {
			Orientation = Orientation.Vertical;
		}
		public Orientation Orientation { get; set; }
		public BootstrapDemoSectionModel Section {
			get {
				BootstrapDemoPageModel demoPageModel = BootstrapUtils.CurrentDemo as BootstrapDemoPageModel;
				if(demoPageModel != null && !string.IsNullOrEmpty(ID))
					return demoPageModel.FindSection(ID);
				return null;
			}
		}
		public string Title { get { return Section != null ? Section.Title : "Demo Section Title"; } }
		public string HeaderID { get { return Section != null ? Section.Key : ""; } }
		public string Description { get { return Section != null ? Section.GetProcessedDescription() : "Description"; } }
		public string CodeASPX { get { return Section != null ? Section.CodeASPX : ""; } }
		public string CodeCS { get { return Section != null ? Section.CodeCS : ""; } }
		public string CodeVB { get { return Section != null ? Section.CodeVB : ""; } }
		public string CodeJS { get { return Section != null ? Section.CodeJS : ""; } }
		public List<BootstrapDemoSectionSourceFileModel> LoadCustomFiles() {
			var sourceFiles = Section.GetSourceFiles();
			foreach(var file in sourceFiles) {
				if(!BootstrapUtils.IsSourceFileExist(file.Path)) {
					var vbFilePath = Path.ChangeExtension(file.Path, ".vb");
					if(BootstrapUtils.IsSourceFileExist(vbFilePath))
						file.Path = vbFilePath;
				}
				if(BootstrapUtils.IsSourceFileExist(file.Path) && string.IsNullOrEmpty(file.Content))
					file.Content = BootstrapUtils.GetSourceFileCode(file.Path);
			}
			return sourceFiles;
		}
		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender(e);
			if(!DesignMode && Section == null)
				throw new Exception("The BootstrapDemoItem with ID='" + ID + "' doesn't have a corresponding DemoSection in the Demos.xml file.");
		}
		protected override void Render(HtmlTextWriter writer) {
			if(Title != null) {
				RenderHeader(writer);
			}
			if(Orientation == Orientation.Vertical) {
				if(Description != null) {
					RenderDescription(writer);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "example");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				RenderContents(writer);
				RenderCode(writer);
				writer.RenderEndTag();
			}
			else {
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "row");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				{
					if(Description != null) {
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-8");
						writer.RenderBeginTag(HtmlTextWriterTag.Div);
						{
							RenderDescription(writer);
						}
						writer.RenderEndTag();
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-4");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					{
						RenderContents(writer);
					}
					writer.RenderEndTag();
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "example col-lg-12");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					{
						RenderCode(writer);
					}
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}
		private void RenderCode(HtmlTextWriter writer) {
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "code");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			var customFiles = LoadCustomFiles();
			if(BootstrapUtils.BootstrapVersion == 4) {
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "lang-group nav nav-code");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				RenderLanguageButton(writer, "aspx", "ASPX", true);
			}
			if(customFiles.Any() || !string.IsNullOrEmpty(CodeCS) || !string.IsNullOrEmpty(CodeVB) || !string.IsNullOrEmpty(CodeJS)) {
				if(BootstrapUtils.BootstrapVersion == 3) {
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "lang-group btn-group");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					RenderLanguageButton(writer, "aspx", "ASPX", true);
				}
				if(!string.IsNullOrEmpty(CodeCS))
					RenderLanguageButton(writer, "cs", "C#", false);
				if(!string.IsNullOrEmpty(CodeVB))
					RenderLanguageButton(writer, "vb", "VB", false);
				if(!string.IsNullOrEmpty(CodeJS))
					RenderLanguageButton(writer, "js", "JS", false);
				foreach(var file in customFiles) {
					var fileName = Path.GetFileName(file.Path);
					var title = string.IsNullOrEmpty(file.CustomTitle) ? fileName : file.CustomTitle;
					RenderLanguageButton(writer, fileName, title, false);
				}
				if(BootstrapUtils.BootstrapVersion == 3)
					writer.RenderEndTag();
			}
			if(BootstrapUtils.BootstrapVersion == 4) {
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "nav-link nav-copy");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:;");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			else {
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default btn-copy");
				writer.RenderBeginTag(HtmlTextWriterTag.Button);
			}
			writer.WriteEncodedText("Copy");
			writer.RenderEndTag();
			if(BootstrapUtils.BootstrapVersion == 4)
				writer.RenderEndTag();
			if(!string.IsNullOrEmpty(CodeASPX))
				RenderLanguageContent(writer, "aspx", "xml", CodeASPX, true);
			if(!string.IsNullOrEmpty(CodeCS))
				RenderLanguageContent(writer, "cs", "cs", CodeCS, false);
			if(!string.IsNullOrEmpty(CodeVB))
				RenderLanguageContent(writer, "vb", "vb", CodeVB, false);
			if(!string.IsNullOrEmpty(CodeJS))
				RenderLanguageContent(writer, "js", "js", CodeJS, false);
			foreach(var file in customFiles) {
				var fileName = Path.GetFileName(file.Path);
				RenderLanguageContent(writer, fileName, GetDataCodeByFileName(fileName), file.Content, false);
			}
			writer.RenderEndTag();
		}
		private void RenderDescription(HtmlTextWriter writer) {
			writer.Write(Description);
		}
		private void RenderHeader(HtmlTextWriter writer) {
			writer.AddAttribute(HtmlTextWriterAttribute.Id, HeaderID);
			writer.RenderBeginTag(HtmlTextWriterTag.H2);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "alink icon icon-link");
			writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveUrl(BootstrapUtils.GenerateDemoSectionUrl(Section)));
			writer.AddAttribute(HtmlTextWriterAttribute.Target, "_top");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
			writer.WriteEncodedText(Title);
			writer.RenderEndTag();
		}
		protected string GetDataCodeByFileName(string fileName) {
			string ext = Path.GetExtension(fileName.ToLower());
			switch(ext) {
				case ".xml":
				case ".aspx":
				case ".ascx":
					return "xml";
				case ".js":
					return "js";
				case ".cs":
					return "cs";
				case ".vb":
					return "vb";
				case ".asax":
					return "cs";
				default:
					throw new Exception("Unknown extension");
			}
		}
		protected void RenderLanguageButton(HtmlTextWriter writer, string dataCode, string text, bool isActive) {
			if(BootstrapUtils.BootstrapVersion == 4) {
				writer.AddAttribute("data-code", dataCode);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:;");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "nav-link" + (isActive ? " active" : ""));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.WriteEncodedText(text);
				writer.RenderEndTag();
			}
			else {
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
				writer.AddAttribute("data-code", dataCode);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default" + (isActive ? " active" : ""));
				writer.RenderBeginTag(HtmlTextWriterTag.Button);
				writer.WriteEncodedText(text);
				writer.RenderEndTag();
			}
		}
		protected void RenderLanguageContent(HtmlTextWriter writer, string dataCode, string codeClass, string code, bool isActive) {
			string hiddenClass = BootstrapUtils.BootstrapVersion == 4 ? "d-none" : "hidden";
			string codeHtml = string.Format("<pre data-code='{0}' {1}><code class='{2}'>{3}</code></pre>",
				dataCode, !isActive ? string.Format("class='{0}'", hiddenClass) : "", codeClass, code);
			writer.Write(codeHtml);
		}
	}
	public class BootstrapDemoItemControlBuilder : ControlBuilder {
		public string DemoGroupKey { get; private set; }
		public string DemoKey { get; private set; }
		public string DemoSectionKey { get; private set; }
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs) {
			base.Init(parser, parentBuilder, type, tagName, id, attribs);
			if(!string.IsNullOrEmpty(PageVirtualPath)) {
				string groupKey = "";
				string demoKey = "";
				Utils.FindDemoKeysByVirtualPath(PageVirtualPath, out groupKey, out demoKey);
				DemoGroupKey = groupKey;
				DemoKey = demoKey;
				DemoSectionKey = (string)attribs["ID"];
			}
		}
		public void RegisterCode(string code) {
			var group = BootstrapDemosModel.Instance.FindGroup(DemoGroupKey);
			if(group != null) {
				var demo = group.FindDemo(DemoKey);
				if(demo != null) {
					var section = demo.FindSection(DemoSectionKey);
					if(section != null)
						section.Code = code;
				}
			}
		}
		public override void SetTagInnerText(string text) {
			base.SetTagInnerText(text);
			RegisterCode(text);
		}
		public override bool NeedsTagInnerText() {
			return true;
		}
	}
	public class BootstrapDemoContainerItem : BootstrapDemoItemBase {
		protected override void Render(HtmlTextWriter writer) {
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write(BootstrapUtils.CurrentDemo.GetProcessedDescription());
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "demo-container-item");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			RenderContents(writer);
			writer.RenderEndTag();
		}
	}
}
