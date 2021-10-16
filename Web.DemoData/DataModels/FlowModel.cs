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
	public partial class FlowContextSL : DbContext {
		static FlowContextSL() {
			Database.SetInitializer<FlowContextSL>(null);
		}
		public FlowContextSL() : base("FlowConnectionStringSL") { }
		public DbSet<FlowObject> Nodes { get; set; }
		public DbSet<FlowConnection> Edges { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new NodeMap());
			modelBuilder.Configurations.Add(new EdgeMap());
		}
	}
	public class NodeMap : EntityTypeConfiguration<FlowObject> {
		public NodeMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(null);
			this.Property(t => t.Text)
				.HasMaxLength(100);
			this.ToTable("Nodes");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Text).HasColumnName("Text");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.Width).HasColumnName("Width");
			this.Property(t => t.Height).HasColumnName("Height");
		}
	}
	public class EdgeMap : EntityTypeConfiguration<FlowConnection> {
		public EdgeMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID)
				.HasDatabaseGeneratedOption(null);
			this.ToTable("Edges");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.FromID).HasColumnName("FromID");
			this.Property(t => t.ToID).HasColumnName("ToID");
			this.Property(t => t.Text).HasColumnName("Text");
		}
	}
	public partial class FlowObject : FlowEntity {
		public string Type { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
	public partial class FlowConnection : FlowEntity {
		public int? FromID { get; set; }
		public int? ToID { get; set; }
	}
	public class FlowEntity {
		public int ID { get; set; }
		public string Text { get; set; }
	}
}
