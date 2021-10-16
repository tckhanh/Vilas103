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
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DevExpress.Web.Demos {
	public partial class OrgItemsContextSL : DbContext {
		static OrgItemsContextSL() {
			Database.SetInitializer<FlowContextSL>(null);
		}
		public OrgItemsContextSL() : base("OrgItemsConnectionStringSL") { }
		public DbSet<OrgItem> Nodes { get; set; }
		public DbSet<OrgLink> Edges { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new OrgItemMap());
			modelBuilder.Configurations.Add(new OrgLinkMap());
		}
	}
	public class OrgItemMap : EntityTypeConfiguration<OrgItem> {
		public OrgItemMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(null);
			this.Property(t => t.Text)
				.HasMaxLength(100);
			this.ToTable("Nodes");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Text).HasColumnName("Text");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.Picture).HasColumnName("Picture");
		}
	}
	public class OrgLinkMap : EntityTypeConfiguration<OrgLink> {
		public OrgLinkMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(null);
			this.ToTable("Edges");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.FromID).HasColumnName("FromID");
			this.Property(t => t.ToID).HasColumnName("ToID");
		}
	}
	public partial class OrgItem : OrgItemBase {
		public string Text { get; set; }
		public string Type { get; set; }
		public string Picture { get; set; }
	}
	public partial class OrgLink : OrgItemBase {
		public int? FromID { get; set; }
		public int? ToID { get; set; }
	}
	public class OrgItemBase {
		public int ID { get; set; }
	}
}
