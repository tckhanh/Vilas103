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
using DevExpress.Web.Demos;
namespace DevExpress.Web.Demos {
	public partial class WorldCitiesContext : ContextBase {
		static WorldCitiesContext() {
			Database.SetInitializer<WorldCitiesContext>(null);
		}
		public WorldCitiesContext()
			: base("WorldCitiesConnectionString") {
		}
		public DbSet<City> Cities { get; set; }
		public DbSet<Country> Countries { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new CityMap());
			modelBuilder.Configurations.Add(new CountryMap());
		}
	}
	public partial class WorldCitiesContextSL : DbContext {
		static WorldCitiesContextSL() {
			Database.SetInitializer<WorldCitiesContext>(null);
		}
		public WorldCitiesContextSL() : base("WorldCitiesConnectionStringSL") { }
		public DbSet<City> Cities { get; set; }
		public DbSet<Country> Countries { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new CityMap());
			modelBuilder.Configurations.Add(new CountryMap());
		}
	}
	public partial class City {
		public City() {
			this.Countries = new List<Country>();
		}
		public int CityId { get; set; }
		public string CityName { get; set; }
		public Nullable<int> CountryId { get; set; }
		public virtual Country Country { get; set; }
		public virtual ICollection<Country> Countries { get; set; }
	}
	public partial class Country {
		public Country() {
			this.Cities = new List<City>();
		}
		public int CountryId { get; set; }
		public string CountryName { get; set; }
		public Nullable<int> CapitalId { get; set; }
		public virtual ICollection<City> Cities { get; set; }
		public virtual City City { get; set; }
	}
	public class CityMap : EntityTypeConfiguration<City> {
		public CityMap() {
			this.HasKey(t => t.CityId);
			this.Property(t => t.CityName)
				.HasMaxLength(255);
			this.ToTable("Cities");
			this.Property(t => t.CityId).HasColumnName("CityId");
			this.Property(t => t.CityName).HasColumnName("CityName");
			this.Property(t => t.CountryId).HasColumnName("CountryId");
			this.HasOptional(t => t.Country)
				.WithMany(t => t.Cities)
				.HasForeignKey(d => d.CountryId);
		}
	}
	public class CountryMap : EntityTypeConfiguration<Country> {
		public CountryMap() {
			this.HasKey(t => t.CountryId);
			this.Property(t => t.CountryName)
				.HasMaxLength(255);
			this.ToTable("Countries");
			this.Property(t => t.CountryId).HasColumnName("CountryId");
			this.Property(t => t.CountryName).HasColumnName("CountryName");
			this.Property(t => t.CapitalId).HasColumnName("CapitalId");
			this.HasOptional(t => t.City)
				.WithMany(t => t.Countries)
				.HasForeignKey(d => d.CapitalId);
		}
	}
}
