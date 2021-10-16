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
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
namespace DevExpress.Web.Demos {
	public partial class NewsGroupsContext : ContextBase {
		static NewsGroupsContext() {
			Database.SetInitializer<NewsGroupsContext>(null);
		}
		public NewsGroupsContext() : base("NewsGroupsConnectionString") { }
		public DbSet<Threads> Threads { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new ThreadsMap());
		}
	}
	public partial class Threads {
		public int ID { get; set; }
		public Nullable<int> ParentID { get; set; }
		public string Subject { get; set; }
		public string From { get; set; }
		public string Text { get; set; }
		public Nullable<System.DateTime> DateCreated { get; set; }
		public Nullable<bool> IsNew { get; set; }
		public Nullable<bool> HasAttachment { get; set; }
	}
	public class ThreadsMap : EntityTypeConfiguration<Threads> {
		public ThreadsMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.Subject)
				.HasMaxLength(50);
			this.Property(t => t.From)
				.HasMaxLength(50);
			this.ToTable("Threads");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ParentID).HasColumnName("ParentID");
			this.Property(t => t.Subject).HasColumnName("Subject");
			this.Property(t => t.From).HasColumnName("From");
			this.Property(t => t.Text).HasColumnName("Text");
			this.Property(t => t.DateCreated).HasColumnName("Date");
			this.Property(t => t.IsNew).HasColumnName("IsNew");
			this.Property(t => t.HasAttachment).HasColumnName("HasAttachment");
		}
	}
}
