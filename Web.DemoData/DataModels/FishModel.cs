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
using DevExpress.Web.Demos;
namespace DevExpress.Web.Demos {
	public partial class FishContext : ContextBase {
		static FishContext() {
			Database.SetInitializer<FishContext>(null);
		}
		public FishContext() : base("FishConnectionString") { }
		public DbSet<BioLife> BioLives { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new BioLifeMap());
		}
	}
	public partial class BioLife {
		public int ID { get; set; }
		public Nullable<int> ParentID { get; set; }
		public Nullable<int> Species_No { get; set; }
		public Nullable<float> Length { get; set; }
		public string Category { get; set; }
		public string Common_Name { get; set; }
		public string Species_Name { get; set; }
		public string Notes { get; set; }
		public byte[] Picture { get; set; }
		public Nullable<bool> Mark { get; set; }
		public Nullable<System.DateTime> RecordDate { get; set; }
	}
	public class BioLifeMap : EntityTypeConfiguration<BioLife> {
		public BioLifeMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID);
			this.Property(t => t.Category)
				.HasMaxLength(255);
			this.Property(t => t.Common_Name)
				.HasMaxLength(255);
			this.Property(t => t.Species_Name)
				.HasMaxLength(255);
			this.ToTable("BioLife");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.ParentID).HasColumnName("ParentID");
			this.Property(t => t.Species_No).HasColumnName("Species_No");
			this.Property(t => t.Length).HasColumnName("Length");
			this.Property(t => t.Category).HasColumnName("Category");
			this.Property(t => t.Common_Name).HasColumnName("Common_Name");
			this.Property(t => t.Species_Name).HasColumnName("Species_Name");
			this.Property(t => t.Notes).HasColumnName("Notes");
			this.Property(t => t.Picture).HasColumnName("Picture");
			this.Property(t => t.Mark).HasColumnName("Mark");
			this.Property(t => t.RecordDate).HasColumnName("RecordDate");
		}
	}
}
