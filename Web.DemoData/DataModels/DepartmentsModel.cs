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
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
namespace DevExpress.Web.Demos {
	public partial class DepartmentsContext : DevExpress.Web.Demos.ContextBase {
		static DepartmentsContext() {
			Database.SetInitializer<DepartmentsContext>(null);
		}
		public DepartmentsContext() : base("DepartmentsConnectionString") { }
		public DbSet<Department> Departments { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new DepartmentMap());
		}
	}
	public partial class Department {
		public int ID { get; set; }
		public Nullable<int> ParentID { get; set; }
		public Nullable<double> ImageIndex { get; set; }
		public string DepartmentName { get; set; }
		public Nullable<decimal> Budget { get; set; }
		public string Location { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
	}
	public class DepartmentMap : EntityTypeConfiguration<Department> {
		public DepartmentMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.DepartmentName)
				.HasMaxLength(100);
			this.Property(t => t.Location)
				.HasMaxLength(50);
			this.Property(t => t.Phone1)
				.HasMaxLength(15);
			this.Property(t => t.Phone2)
				.HasMaxLength(15);
			this.ToTable("Departments");
			this.Property(t => t.ID).HasColumnName("ID").HasDatabaseGeneratedOption(null);
			this.Property(t => t.ParentID).HasColumnName("ParentID");
			this.Property(t => t.ImageIndex).HasColumnName("ImageIndex");
			this.Property(t => t.DepartmentName).HasColumnName("Department");
			this.Property(t => t.Budget).HasColumnName("Budget");
			this.Property(t => t.Location).HasColumnName("Location");
			this.Property(t => t.Phone1).HasColumnName("Phone1");
			this.Property(t => t.Phone2).HasColumnName("Phone2");
		}
	}
}
