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
namespace DevExpress.Web.Demos {
	public static class EmailDataGenerator {
		static DatabaseGenerator.TableData table;
		static readonly string[] subjects = new string[] { 
			"Integrating Developer Express MasterView control into an Accounting System.",
			"Web Edition: Data Entry Page. There is an issue with date validation.",
			"Payables Due Calculator is ready for testing.",
			"Web Edition: Search Page is ready for testing.",
			"Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
			"Receivables Calculator. Where can I find the complete specs?",
			"Ledger: Inconsistency. Please fix it.",
			"Receivables Printing module is ready for testing.",
			"Screen Redraw. Somebody has to look at it.",
			"Email System. What library are we going to use?",
			"Cannot add new vendor. This module doesn't work!",
			"History. Will we track sales history in our system?",
			"Main Menu: Add a File menu. File menu item is missing.",
			"Currency Mask. The current currency mask in completely unusable.",
			"Drag & Drop operations are not available in the scheduler module.",
			"Data Import. What database types will we support?",
			"Reports. The list of incomplete reports.",
			"Data Archiving. We still don't have this features in our application.",
			"Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.",
			"Check Register. We are using different paths for different modules.",
			"Data Export. Our customers asked us for export to Microsoft Excel"
		};
		static readonly string[] names = new string[] {
			"Peter Dolan", "Ryan Fischer", "Richard Fisher", 
			"Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", 
			"Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", 
			"Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"
		};
		public static DatabaseGenerator.TableData Table {
			get { return table; }
		}
		public static void Register() {
			table = new DatabaseGenerator.TableData();
			table.Name = "Emails";
			table.ConnectionStringName = "LargeDatabaseConnectionString";
			table.Fields.Add(new DatabaseGenerator.FieldData("Subject", subjects));
			table.Fields.Add(new DatabaseGenerator.FieldData("From", names));
			table.Fields.Add(new DatabaseGenerator.FieldData("Sent", GenerateSent));
			table.Fields.Add(new DatabaseGenerator.FieldData("Size", GenerateSize));
			table.Fields.Add(new DatabaseGenerator.FieldData("HasAttachment", GenerateHasAttachment));
			table.RecordCount = 300000;
			DatabaseGenerator.RegisterTable("GeneratedEmailTable", table);
		}
		static object GenerateSent(Random rnd) {
			return DateTime.Now.Date.Subtract(TimeSpan.FromDays(rnd.Next(50)));
		}
		static object GenerateSize(Random rnd) {
			return (long)(1000 + rnd.Next(300000));
		}
		static object GenerateHasAttachment(Random rnd) {
			return rnd.Next(10) > 5;
		}
	}
}
