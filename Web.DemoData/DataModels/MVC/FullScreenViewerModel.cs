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

using System.Collections.Generic;
using System.Linq;
namespace DevExpress.Web.Demos {
	public class FullScreenViewerModel {
		public FullScreenViewerModel() { }
		public FullScreenViewerModel(string url, int deviceHeight, int deviceWidth) {
			DeviceHeight = deviceHeight;
			DeviceWidth = deviceWidth;
			Url = url;
		}
		const int DocumentFrameSize = 40;
		public int DeviceHeight { get; set; }
		public int DeviceWidth { get; set; }
		public string Url { get; set; }
		public bool IsFullScreen { get { return DeviceHeight == 0 && DeviceWidth == 0; } }
		public int FullDeviceHeight { get { return DeviceHeight + DocumentFrameSize; } }
		public int FullDeviceWidth { get { return DeviceWidth + DocumentFrameSize; } }
		public List<ComboBoxViewModel> ComboBoxSizeDataSource {
			get {
				if(comboBoxSizeDataSource == null)
					comboBoxSizeDataSource = GetComboBoxSizeDataSource();
				return comboBoxSizeDataSource;
			}
		}
		public List<ComboBoxViewModel> ComboBoxThemeDataSource {
			get {
				if(comboBoxThemeDataSource == null)
					comboBoxThemeDataSource = GetComboBoxThemeDataSource();
				return comboBoxThemeDataSource;
			}
		}
		public List<ComboBoxViewModel> ComboBoxColorDataSource {
			get {
				if(comboBoxColorDataSource == null)
					comboBoxColorDataSource = GetComboBoxColorDataSource();
				return comboBoxColorDataSource;
			}
		}
		List<ComboBoxViewModel> comboBoxSizeDataSource;
		List<ComboBoxViewModel> comboBoxThemeDataSource;
		List<ComboBoxViewModel> comboBoxColorDataSource;
		List<ComboBoxViewModel> GetComboBoxSizeDataSource() {
			return FullScreenModeUtils.Resolutions.Select(r => new ComboBoxViewModel() {
				Text = r.Name,
				Value = "?width=" + r.Width + "&height=" + r.Height + "&Url=" + Url,
				ImageUrl = r.ImageUrl,
				Selected = r.Width == DeviceWidth && r.Height == DeviceHeight
			}).ToList();
		}
		List<ComboBoxViewModel> GetComboBoxThemeDataSource() {
			return Utils.CurrentThemesModel.MobileThemes.Select(t => new ComboBoxViewModel() {
				Value = t,
				ImageUrl = Utils.GetColoredSquareUrl(Utils.GetDefaultBaseColor(t)),
				Selected = t == Utils.CurrentTheme
			}).ToList();
		}
		List<ComboBoxViewModel> GetComboBoxColorDataSource() {
			string currentBaseColor = !string.IsNullOrWhiteSpace(Utils.CurrentBaseColor) ? Utils.CurrentBaseColor : Utils.GetDefaultBaseColor(Utils.CurrentTheme);
			return Utils.CustomBaseColors.Select(c => new ComboBoxViewModel() {
				Value = c,
				ImageUrl = Utils.GetColoredSquareUrl(c),
				Selected = c == currentBaseColor
			}).ToList();
		}
	}
	public class ComboBoxViewModel {
		public string Text { get; set; }
		public string Value { get; set; }
		public string ImageUrl { get; set; }
		public bool Selected { get; set; }
	}
}
