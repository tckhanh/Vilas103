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
using System.Linq;
using System.Collections.Generic;
namespace DevExpress.Web.Demos {
	public static class BootstrapSearchUtils {
		static BootstrapSearchUtils() {
			Search = new SearchAgregator(BootstrapDemosModel.Instance.Search);
		}
		static SearchAgregator Search { get; set; }
		public static List<BootstrapSearchResult> DoSearch(string request) {
			var results = new List<BootstrapSearchResult>();
			if(!string.IsNullOrEmpty(request)) {
				var requests = Search.SplitRequests(request);
				try {
					foreach(var group in BootstrapDemosModel.Instance.SortedDemoGroups) 
						results.AddRange(DoSearch(requests, group));
				}
				catch {
				}
				results = results.OrderByDescending(sr => sr.Rank).ThenBy(sr => sr.Group.OrderIndex).ToList();
			}
			return results;
		}
		static IEnumerable<BootstrapSearchResult> DoSearch(List<string[]> requests, BootstrapDemoGroupPageModel group) {
			var results = new List<BootstrapSearchResult>();
			foreach(var demo in group.Demos) {
				foreach(var section in demo.Sections)
					results.AddRange(GetRes(requests, demo, section, SearchAgregator.HighlightOccurences(section.Title, requests)));
				if(results.Count == 0)
					results.AddRange(GetRes(requests, demo, null, SearchAgregator.HighlightOccurences(demo.Title, requests)));
			}
			results.AddRange(GetRes(requests, group, null, SearchAgregator.HighlightOccurences(group.Title, requests)));
			return results;
		}
		static IEnumerable<BootstrapSearchResult> GetRes(List<string[]> requests, BootstrapDemoPageModelBase demo, BootstrapDemoSectionModel section, string text) {
			var results = new List<BootstrapSearchResult>();
			var rank = CalculateRank(requests, demo, section);
			if(rank > -1) {
				var sr = new BootstrapSearchResult(demo, section, rank);
				sr.Text = text;
				results.Add(sr);
			}
			return results;
		}
		static int CalculateRank(List<string[]> requests, BootstrapDemoModelBase demo, BootstrapDemoSectionModel section) {
			int resultRank = 0;
			int keywordRank = 0;
			foreach(var request in requests) {
				int requestRank = -1;
				if(section != null && demo is BootstrapDemoPageModel && ((BootstrapDemoPageModel)demo).Sections.Count > 1 && SearchAgregator.CalculateRank(request, section.KeywordsRankList, out keywordRank))
					requestRank += keywordRank;
				if(SearchAgregator.CalculateRank(request, demo.KeywordsRankList, out keywordRank))
					requestRank += keywordRank;
				if(demo is BootstrapDemoGroupPageModel && SearchAgregator.CalculateRank(request, ((BootstrapDemoGroupPageModel)demo).KeywordsRankList, out keywordRank))
					requestRank += keywordRank;
				if(requestRank == -1 && Search.WordsExclusions.Any(re => re.Equals(request[0], SearchAgregator.DefaultStringComparison)))
					requestRank = 0;
				if(requestRank > -1)
					resultRank += requestRank;
				else
					return -1;
			}
			return resultRank;
		}
		public static Dictionary<string, int> GetKeywordsRankList(ModelBase model) {
			List<TextRank> textRanks = new List<TextRank>() {
				new TextRank(model.Keywords, 3)
			};
			var group = model as BootstrapDemoGroupPageModel;
			var demo = model as BootstrapDemoPageModel;
			var section = model as BootstrapDemoSectionModel;
			if(group != null) {
				textRanks.Add(new TextRank(group.Title, 15));
				textRanks.Add(new TextRank(group.Key, 7));
				textRanks.Add(new TextRank(group.SeoTitle, 5));
				textRanks.Add(new TextRank(group.Links, 2));
			}
			else if(demo != null) {
				textRanks.Add(new TextRank(demo.Title, 5));
				textRanks.Add(new TextRank(demo.Key, 3));
				textRanks.Add(new TextRank(demo.SeoTitle, 2));
				textRanks.Add(new TextRank(demo.Links, 2));
			}
			else if(section != null) {
				textRanks.Add(new TextRank(section.Title, 5));
				textRanks.Add(new TextRank(section.Key, 3));
				textRanks.Add(new TextRank(section.SeoTitle, 2));
				textRanks.Add(new TextRank(section.Links, 2));
			}
			return Search.GetKeywordsRankList(textRanks);
		}
	}
	public class BootstrapSearchResult : IComparable<BootstrapSearchResult> {
		public BootstrapSearchResult(BootstrapDemoPageModelBase demo, BootstrapDemoSectionModel section, int rank) {
			Demo = demo;
			Rank = rank;
			Section = section;
		}
		public BootstrapDemoSectionModel Section { get; set; }
		public BootstrapDemoPageModelBase Demo { get; set; }
		public BootstrapDemoGroupPageModel Group {
			get {
				if(Demo is BootstrapDemoGroupPageModel) 
					return (BootstrapDemoGroupPageModel)Demo;
				if(Demo is BootstrapDemoPageModel)
					return ((BootstrapDemoPageModel)Demo).Group;
				return null;
			}
		}
		public string Text { get; set; }
		public int Rank { get; set; }
		int IComparable<BootstrapSearchResult>.CompareTo(BootstrapSearchResult other) {
			return other.Rank.CompareTo(Rank);
		}
	}
}
