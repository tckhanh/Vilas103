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
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
namespace DevExpress.Web.Demos {
	[XmlRoot("Themes")]
	public abstract class ThemesModelBase {
		const int DefaultSize = 18;
		public virtual Unit IconHeight {
			get { return Unit.Pixel(DefaultSize); }
		}
		public virtual Unit IconWidth {
			get { return Unit.Pixel(DefaultSize); }
		}
		List<ThemeGroupModel> _groups = new List<ThemeGroupModel>();
		public abstract IEnumerable<ThemeModel> GetAllThemes();
		protected static T GetCurrentModel<T>(string resourceName) {
			using(Stream stream = ResourcesUtils.GetEmbeddedResource(resourceName)) {
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				return (T)serializer.Deserialize(stream);
			}
		}
		public List<ThemeGroupModel> GetAllGroups() {
			var topThemes = GetAllThemes().Where(t => t.ShowAsTop).ToList();
			var currentTheme = GetAllThemes().First(t => t.Name == Utils.CurrentTheme);
			if(!topThemes.Contains(currentTheme))
				topThemes.Add(currentTheme);
			var firstGroup = new ThemeGroupModel() {
				Name = "FirstGroup",
				Themes = topThemes
			};
			return Groups.Concat(new[] { firstGroup }).ToList();
		}	   
		public IEnumerable<string> MobileThemes {
			get {
				return Groups.First(g => g.Name == "Mobile").Themes.Select(t => t.Name);
			}
		}
		public IEnumerable<ThemeModel> NewThemes {
			get {
				return GetAllThemes().Where(theme => theme.IsNew);
			}
		}
		public ThemeModel GetThemeModel(string themeName) {
			return GetAllThemes().FirstOrDefault(t => t.Name == themeName);
		}
		[XmlElement("ThemeGroup")]
		public List<ThemeGroupModel> Groups {
			get { return _groups; }
		}
		public List<ThemeGroupModel> RightGroups {
			get { return (from g in Groups where g.Float == "Right" select g).ToList(); }
		}
		public List<ThemeGroupModel> LeftGroups {
			get { return (from g in Groups where g.Float == "Left" select g).ToList(); }
		}
	}
}
