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
	public partial class BinaryImagesContext : ContextBase {
		static BinaryImagesContext() {
			Database.SetInitializer<BinaryImagesContext>(null);
		}
		public BinaryImagesContext() : base("BinaryImagesConnectionString") { }
		public DbSet<BinaryImage> BinaryImages { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new BinaryImageMap());
		}
	}
	public partial class BinaryImage {
		public int ID { get; set; }
		public string Text { get; set; }
		public byte[] Picture { get; set; }
		public Nullable<int> CategoryID { get; set; }
	}
	public class BinaryImageMap : EntityTypeConfiguration<BinaryImage> {
		public BinaryImageMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID);
			this.Property(t => t.Text)
				.IsRequired()
				.HasMaxLength(50);
			this.ToTable("BinaryImages");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Text).HasColumnName("Text");
			this.Property(t => t.Picture).HasColumnName("Picture");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
		}
	}
}
