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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using DevExpress.Web.Internal;
namespace DevExpress.Web.Demos {
	public enum HeadphoneFit {
		[Description("Over ear")]
		OverEar,
		[Description("On ear")]
		OnEar,
		[Description("In ear")]
		InEar
	}
	public enum HeadphoneStyle {
		[Description("Pro")]
		Pro,
		[Description("Studio")]
		Studio,
		[Description("Sport and exercize")]
		SportAndExercize
	}
	public class Headphone {
		public Headphone() { }
		public int ID { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public HeadphoneFit Fit { get; set; }
		public HeadphoneStyle Style { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public decimal Rating { get; set; }
		public int ReviewCount { get; set; }
		public int Warranty { get; set; }
		public bool Wireless { get; set; }
		public bool Bluetooth { get; set; }
		public bool WaterResistant { get; set; }
		public bool PhoneControl { get; set; }
		public bool InLineVolumeControl { get; set; }
		public bool SoundIsolating { get; set; }
		public bool CarryingCase { get; set; }
		public bool BuiltInMicrophone { get; set; }
		public string Color { get; set; }
		public double Height { get; set; }
		public double Width { get; set; }
		public double Depth { get; set; }
		public double Weight { get; set; }
		public string ConnectorSize { get; set; }
		public int Sensitivity { get; set; }
		public int MinFrequency { get; set; }
		public int MaxFrequency { get; set; }
		public int Power { get; set; }
		public int Impedance { get; set; }
		public byte[] Photo { get; set; }
		public string PhotoUrl { get; set; }
		public byte[] LargePhoto { get; set; }
		public string LargePhotoUrl { get; set; }
	}
	public class HeadphonesDataProvider {
		const int ImageWidth = 180;
		const string
			ImageFileNameFormat = "{0}_{1}.jpg",
			DataFileVirtualPath = "~/App_Data/Headphones.xml",
			PhotoDirVirtualPath = "~/Content/HeadphonesPhoto";
		static readonly object createLocker = new object();
		static List<Headphone> headphones = null;
		static string MapPath(string path) { return HttpContext.Current.Server.MapPath(path); }
		static string DataFilePath { get { return MapPath(DataFileVirtualPath); } }
		static string PhotoDirPath { get { return MapPath(PhotoDirVirtualPath); } }
		static string PhotoUrl(int id, string postfix) { return Path.Combine(PhotoDirVirtualPath, string.Format(ImageFileNameFormat, id, postfix)); }
		public static List<Headphone> Headphones {
			get {
				if(headphones == null)
					headphones = CreateHeadphones();
				return headphones;
			}
		}
		public static List<Headphone> SelectHeadphones() { return Headphones; }
		static List<Headphone> CreateHeadphones() {
			lock(createLocker) {
				var serializer = new XmlSerializer(typeof(List<Headphone>));
				FileStream fs = File.OpenRead(DataFilePath);
				XmlReader reader = XmlReader.Create(fs);
				List<Headphone> list = (List<Headphone>)serializer.Deserialize(reader);
				fs.Close();
				foreach(var headphone in list) {
					headphone.PhotoUrl = GetPhotoUrl(headphone, headphone.Photo, "s");
					headphone.LargePhotoUrl = GetPhotoUrl(headphone, headphone.LargePhoto, "l");
				}
				return list;
			}
		}
		static string GetPhotoUrl(Headphone item, byte[] photo, string postfix) {
			var url = PhotoUrl(item.ID, postfix);
			var serverPath = MapPath(url);
			if(!File.Exists(serverPath)) {
				if(!Directory.Exists(PhotoDirPath))
					Directory.CreateDirectory(PhotoDirPath);
				SaveImage(photo, serverPath);
			}
			return url;
		}
		static void SaveImage(byte[] imageBytes, string path) {
			using(var stream = new MemoryStream(imageBytes))
			using(var img = Image.FromStream(stream)) {
				ImageUtils.SaveImage(img, path);
			}
		}
	}
}
