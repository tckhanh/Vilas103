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

using System.Linq;
using System.Web;
using System.Collections.Generic;
using DevExpress.Web.Demos;
namespace DevExpress.Web.Demos {
	public static class LargeDatabaseDataProvider {
		const string LargeDatabaseDataContextKey = "DXLargeDatabaseDataContext";
		public static LargeDatabaseContext DB {
			get {
				if(HttpContext.Current.Items[LargeDatabaseDataContextKey] == null)
					HttpContext.Current.Items[LargeDatabaseDataContextKey] = new LargeDatabaseContext();
				return (LargeDatabaseContext)HttpContext.Current.Items[LargeDatabaseDataContextKey];
			}
		}
		public static IQueryable<Email> Emails { get { return DB.Emails; } }
		public static IEnumerable<Person> GetPersonsRange(ListEditItemsRequestedByFilterConditionEventArgs args) {
			var skip = args.BeginIndex;
			var take = args.EndIndex - args.BeginIndex + 1;
			var query = (from person in DB.Persons
						 where (person.FirstName + " " + person.LastName + " " + person.Phone).Contains(args.Filter)
						 orderby person.LastName
						 select person
					).Skip(skip).Take(take);
			return query.ToList();
		}
		public static Person GetPersonByID(ListEditItemRequestedByValueEventArgs args) {
			int id;
			if(args.Value == null || !int.TryParse(args.Value.ToString(), out id))
				return null;
			return DB.Persons.Where(p => p.ID == id).Take(1).SingleOrDefault();
		}
	}
}
