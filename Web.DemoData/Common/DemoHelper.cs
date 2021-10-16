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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.Web.Internal;
namespace DevExpress.Web.Demos
{
	public enum RecalculateColumnCountMode
	{
		RootGroup,
		ChildGroups
	}
	public struct ControlOptionsSettings
	{
		public int RightBlockWidth { get; set; }
		public int ColumnMinWidth { get; set; }
		public RecalculateColumnCountMode ColumnCountMode { get; set; }
	}
	public class DemoHelper
	{
		private const int DefaultControlAreaMaxWidth = 960;
		private const int DefaultControlAreaMinWidth = 0;
		public const string Theme = "MaterialCompactOrange";
		internal const string CurrentDemoHelperKey = "DXCurrentDemoHelper";
		public DemoHelper() {
			ControlAreaMaxWidth = DefaultControlAreaMaxWidth;
			ControlAreaMinWidth = DefaultControlAreaMinWidth;
			ControlAreaMinHeight = ControlOptionsRightBlockWidth = 0;
		}
		public bool SuppressThemeSelector { get; set; }
		public bool UseDevExtremeThemeSelector { get; set; }
		public Unit ControlAreaMinHeight { get; set; }
		public Unit ControlAreaMaxWidth { get; set; }
		public Unit ControlAreaMinWidth { get; set; }
		public Unit ControlOptionsRightBlockWidth { get; set; }
		private void AppyCssClassesForFormLayout(ASPxFormLayout formLayout) {
			formLayout.CssClass = " form-layout";
			formLayout.Styles.LayoutGroupBox.CssClass += " group-box";
			formLayout.Styles.LayoutGroupBox.Caption.CssClass += " group-box-caption";
			formLayout.Styles.LayoutGroup.CssClass += " group";
			formLayout.Styles.LayoutGroup.Cell.CssClass += " cell";
			formLayout.Styles.LayoutItem.Caption.CssClass += " item-caption";
		}
		private void ApplySettingsForFormLayout(ASPxFormLayout formLayout) {
			formLayout.AlignItemCaptionsInAllGroups = true;
			formLayout.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Left;
			formLayout.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit;
		}
		public void PrepareControlOptions(ASPxFormLayout formLayout, ControlOptionsSettings? settings = null) {
			PrepareFormLayout(formLayout, true, settings);
		}
		public void PrepareControlLayout(ASPxFormLayout formLayout, ControlOptionsSettings? settings = null) {
			PrepareFormLayout(formLayout, false, settings);
		}
		private void PrepareFormLayout(ASPxFormLayout formLayout, bool isOptionFormLayout, ControlOptionsSettings? settings = null) {
			if(settings != null) {
				var optionsValue = settings.Value;
				if(optionsValue.ColumnMinWidth != 0) {
					LayoutBreakpoints breakpoints = formLayout.SettingsAdaptivity.GridSettings.Breakpoints;
					if(optionsValue.ColumnCountMode == RecalculateColumnCountMode.RootGroup && breakpoints.IsEmpty)
						for(int i = 0; i < 10; i++)
							breakpoints.Add(new LayoutBreakpoint(i.ToString(), ((i + 2) * optionsValue.ColumnMinWidth), i + 1));
					formLayout.SettingsAdaptivity.GridSettings.StretchLastItem = DefaultBoolean.True;
				}
				if(optionsValue.RightBlockWidth != 0)
					ControlOptionsRightBlockWidth = Unit.Pixel(optionsValue.RightBlockWidth);
			}
			formLayout.ForRootChildGroups(group => PrepareFLGroup(group, isOptionFormLayout, settings));
			AppyCssClassesForFormLayout(formLayout);
			ApplySettingsForFormLayout(formLayout);
			if(isOptionFormLayout)
				formLayout.Theme = Theme;
			formLayout.ForEach(itemBase => PrepareFLItem(itemBase, isOptionFormLayout));
		}
		public void PrepareFLGroup(LayoutGroup group, bool isOptionFormLayout, ControlOptionsSettings? settings) {
			if(isOptionFormLayout) {
				group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine;
				group.Height = Unit.Percentage(100d);
			}
			if(settings != null) {
				var optionsValue = settings.Value;
				int layoutGroupMaxWidth = 1240; 
				int maxLayoutColumnsInGroup = layoutGroupMaxWidth / optionsValue.ColumnMinWidth;
				LayoutBreakpoints breakpoints = group.GridSettings.Breakpoints;
				if(optionsValue.ColumnMinWidth != 0 && optionsValue.ColumnCountMode == RecalculateColumnCountMode.ChildGroups && breakpoints.IsEmpty) {
					int itemsCount = 10;
					if(isOptionFormLayout) {
						itemsCount = maxLayoutColumnsInGroup;
						group.ColumnCount = maxLayoutColumnsInGroup;
					}
					for(int i = 0; i < itemsCount; i++) {
						int breakpointMaxWidth = (i + 2) * optionsValue.ColumnMinWidth;
						breakpoints.Add(new LayoutBreakpoint(i.ToString(), breakpointMaxWidth, i + 1));
					}
				}
				group.GridSettings.StretchLastItem = DefaultBoolean.False;
				group.GridSettings.WrapCaptionAtWidth = optionsValue.ColumnMinWidth;
			}
		}
		public void PrepareFLItem(LayoutItemBase itemBase, bool isOptionFormLayout) {
			var layoutItem = itemBase as LayoutItem;
			if(layoutItem == null)
				return;
			var controls = layoutItem.Controls.OfType<Control>().ToList();
			if(Utils.IsMvc)
				controls.Add(layoutItem.GetNestedControl());
			foreach(var rawControl in controls) {
				var control = rawControl as ASPxWebControl;
				if(control == null)
					continue;
				control.CssClass += " field";
				if(isOptionFormLayout)
					control.Theme = Theme;
				if(control is ASPxCheckBox)
					layoutItem.CaptionSettings.AllowWrapCaption = DefaultBoolean.False;
			}
		}
		public static DemoHelper Instance {
			get {
				if(HttpContext.Current.Items[CurrentDemoHelperKey] == null)
					HttpContext.Current.Items[CurrentDemoHelperKey] = new DemoHelper();
				return (DemoHelper)HttpContext.Current.Items[CurrentDemoHelperKey];
			}
		}
	}
	public static class FormLayoutDemoExtenstion
	{
		public static void ForEachNestedControl(this ASPxFormLayout formLayout, Action<Control> callback) {
			formLayout.ForEach(itemBase => {
				var item = itemBase as LayoutItem;
				if(item != null)
					foreach(Control nestedControl in item.LayoutItemNestedControlCollection[0].Controls)
						callback(nestedControl);
			});
		}
		public static void ForRootChildGroups(this ASPxFormLayout formLayout, Action<LayoutGroup> callback) {
			foreach(LayoutItemBase item in formLayout.Items) {
				var group = item as LayoutGroup;
				if(group != null)
					callback(group);
			}
		}
	}
}
