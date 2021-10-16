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
using System.Drawing;
using System.Web;
using System.Web.UI;
using DevExpress.Web.Internal;
using DevExpress.XtraPrinting.BarCode;
using System.Reflection;
namespace DevExpress.Web.Demos {
	public class FullScreenModeResolution {
		public string Name {
			get {
				if(Width != 0 && Height != 0)
					return string.Format("{0}x{1}", Width, Height);
				return "Fullscreen";
			}
		}
		public int Width { get; set; }
		public int Height { get; set; }
		public string ImageUrl { get; set; }
	}
	public static class FullScreenModeUtils {
		public static readonly string FullScreenViewerUrl = "~/UserControls/FullScreenViewer.aspx";
		public static readonly string FullScreenViewerQRCodeUrl = "~/UserControls/FullScreenViewerQRCode.aspx";
		static System.Resources.ResourceManager resourceManager;
		static List<FullScreenModeResolution> resolutions;
		static System.Resources.ResourceManager ResourceManager {
			get {
				if(resourceManager == null)
					resourceManager = new System.Resources.ResourceManager("DevExpress.Web.Demos.Properties.Resources", Assembly.GetExecutingAssembly());
				return resourceManager;
			}
		}
		public static List<FullScreenModeResolution> Resolutions {
			get {
				if(resolutions == null) {
					resolutions = new List<FullScreenModeResolution>();
					resolutions.Add(new FullScreenModeResolution { Width = 0, Height = 0, ImageUrl = ResourceManager.GetString("IconDataUriFullscreen") });
					resolutions.Add(new FullScreenModeResolution { Width = 1200, Height = 800, ImageUrl = ResourceManager.GetString("IconDataUri1200x800") });
					resolutions.Add(new FullScreenModeResolution { Width = 800, Height = 1200, ImageUrl = ResourceManager.GetString("IconDataUri800x1200") });
					resolutions.Add(new FullScreenModeResolution { Width = 768, Height = 576, ImageUrl = ResourceManager.GetString("IconDataUri768x576") });
					resolutions.Add(new FullScreenModeResolution { Width = 576, Height = 768, ImageUrl = ResourceManager.GetString("IconDataUri576x768") });
				}
				return resolutions;
			}
		}
		public static string GetUrl(Page page, string targetUrl, int width, int height) {
			string url = FullScreenViewerUrl;
			if(!string.IsNullOrEmpty(targetUrl)) {
				url += "?url=" + page.ResolveUrl(targetUrl);
				if(width > 0)
					url += "&width=" + width.ToString();
				if(height > 0)
					url += "&height=" + height.ToString();
			}
			return url;
		}
		public static string GetQRCodeImageUrl(Page page, string url) {
			return string.Format("{0}?url={1}", page.ResolveUrl(FullScreenViewerQRCodeUrl), page.ResolveUrl(url));
		}
		public static System.Drawing.Image GetQRCodeImage(string url) {
			const int imageSize = 120;
			HttpRequest request = HttpUtils.GetRequest();
			string resolvedUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
			var image = new Bitmap(imageSize, imageSize);
			using(var graphicsObj = Graphics.FromImage(image)) {
				var gdiGraphics = new DevExpress.Printing.GdiGraphicsWrapperBase(graphicsObj);
				var qrcode = new QRCodeGenerator() { CompactionMode = QRCodeCompactionMode.Byte };
				var rectf = new RectangleF(0, 0, imageSize, imageSize);
				var barcodeData = new QRBarCodeData(resolvedUrl);
				qrcode.DrawContent(gdiGraphics, rectf, barcodeData);
			}
			return image;
		}
	}
	public class QRBarCodeData : IBarCodeData {
		readonly string text;
		readonly DevExpress.XtraPrinting.BrickStyle style;
		public QRBarCodeData(string text) {
			this.text = text;
			this.style = new DevExpress.XtraPrinting.BrickStyle {
				ForeColor = Color.Black,
				BackColor = Color.Transparent,
				Padding = new DevExpress.XtraPrinting.PaddingInfo()
			};
		}
		DevExpress.XtraPrinting.TextAlignment IBarCodeData.Alignment { get { return DevExpress.XtraPrinting.TextAlignment.MiddleCenter; } }
		bool IBarCodeData.AutoModule { get { return true; } }
		double IBarCodeData.Module { get { return 3.0; } }
		BarCodeOrientation IBarCodeData.Orientation { get { return BarCodeOrientation.Normal; } }
		bool IBarCodeData.ShowText { get { return false; } }
		DevExpress.XtraPrinting.BrickStyle IBarCodeData.Style { get { return style; } }
		string IBarCodeData.Text { get { return text; } }
	}
}
