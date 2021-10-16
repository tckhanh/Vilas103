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
using System.Text;
namespace DevExpress.Web.Demos {
	public static class SearchUtils {
		static SearchUtils() {
			Search = new SearchAgregator(DemosModel.Instance.Search);
		}
		static SearchAgregator Search { get; set; }
		public static List<SearchResult> DoSearch(string request) {
			var results = new List<SearchResult>();
			if(!string.IsNullOrEmpty(request)) {
				var requests = Search.SplitRequests(request);
				try {
					var products = DemosModel.Instance.SortedDemoProducts.Where(dp => !dp.IsRootDemo && (dp == DemosModel.Current || (!dp.HideNavItem && Utils.IsSiteMode)));
					foreach(var product in products) {
						results.AddRange(DoSearch(requests, product));
					}
				} catch {
				}
				results = results.OrderByDescending(sr => sr.Rank).ThenBy(sr => sr.Product.OrderIndex).ToList();
			}
			return results;
		}
		static IEnumerable<SearchResult> DoSearch(List<string[]> requests, DemoProductModel product) {
			var results = new List<SearchResult>();
			if(product.Overview != null)
				results.AddRange(GetRes(requests, product.Overview, string.Format("{0}", SearchAgregator.HighlightOccurences(product.NavItemTitle, requests, true)), product.Overview.Product.Title.ToUpper()));
			foreach(var group in product.Groups) {
				foreach(var demo in group.Demos)
					results.AddRange(GetRes(requests, demo, string.Format("{0}", SearchAgregator.HighlightOccurences(demo.Title, requests, true)), demo.Product.Title.ToUpper()));
			}
			return results;
		}
		static IEnumerable<SearchResult> GetRes(List<string[]> requests, DemoPageModel demo, string text, string productText) {
			var results = new List<SearchResult>();
			var rank = CalculateRank(requests, demo);
			if(rank > -1) {
				var sr = new SearchResult(demo, rank);
				sr.Text = text;
				sr.ProductText = productText;
				results.Add(sr);
			}
			return results;
		}
		static int CalculateRank(List<string[]> requests, DemoPageModel demo) {
			int resultRank = 0;
			int keywordRank = 0;
			foreach(var request in requests) {
				int requestRank = -1;
				if(SearchAgregator.CalculateRank(request, demo.KeywordsRankList, out keywordRank))
					requestRank += keywordRank;
				if(demo.Group != null && SearchAgregator.CalculateRank(request, demo.Group.KeywordsRankList, out keywordRank))
					requestRank += keywordRank;
				if(SearchAgregator.CalculateRank(request, demo.Product.KeywordsRankList, out keywordRank))
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
			var textRanks = new List<TextRank>() {
				new TextRank(model.Keywords, 3)
			};
			var product = model as DemoProductModel;
			var group = model as DemoGroupModel;
			var demo = model as DemoPageModel;
			var overview = model as OverviewPageModel;
			if(product != null) {
				textRanks.Add(new TextRank(product.NavItemTitle, 5));
				textRanks.Add(new TextRank(product.Key, 3));
				textRanks.Add(new TextRank(product.Title, 3));
				textRanks.Add(new TextRank(product.SeoTitle, 2));
			} else if(group != null) {
				textRanks.Add(new TextRank(group.Title, 5));
				textRanks.Add(new TextRank(group.Key, 3));
				textRanks.Add(new TextRank(group.SeoTitle, 2));
			} else if(demo != null) {
				if(overview != null) {
					if(overview.Group != null)
						textRanks.Add(new TextRank(overview.Group.Title, 7));
					else if(overview.Product != null)
						textRanks.Add(new TextRank(overview.Product.Title, 15));
				} else
					textRanks.Add(new TextRank(demo.Title, 5));
				textRanks.Add(new TextRank(demo.Key, 3));
				textRanks.Add(new TextRank(demo.SeoTitle, 2));
				textRanks.Add(new TextRank(demo.Links, 2));
			}
			return Search.GetKeywordsRankList(textRanks);
		}
	}
	public class SearchResult : IComparable<SearchResult> {
		public SearchResult(DemoModel demo, int rank) {
			this.Demo = demo;
			this.Rank = rank;
			Product = demo.Product;
			if(demo is DemoPageModel)
				Group = (demo as DemoPageModel).Group;
		}
		public DemoProductModel Product {
			get;
			set;
		}
		public DemoModel Demo {
			get;
			set;
		}
		public DemoGroupModel Group {
			get;
			set;
		}
		public string Text {
			get;
			set;
		}
		public string ProductText {
			get;
			set;
		}
		public int Rank = 0;
#region IComparable<SearchResult> Members
		public int CompareTo(SearchResult other) {
			return other.Rank.CompareTo(Rank);
		}
#endregion
	}
}
