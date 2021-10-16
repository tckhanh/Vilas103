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

using DevExpress.Web.Demos;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
namespace DevExpress.Web.Demos {
	public partial class MedicsSchedulingDBContext : ContextBase {
		static MedicsSchedulingDBContext() {
			Database.SetInitializer<MedicsSchedulingDBContext>(null);
		}
		public MedicsSchedulingDBContext() : base("MedicsSchedulingDbConnectionString") { }
		public DbSet<MedicsSchedulingDb_MedicalAppointments> MedicalAppointments { get; set; }
		public DbSet<MedicsSchedulingDb_Medics> Medics { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new MedicsSchedulingDb_MedicalAppointmentsMap());
			modelBuilder.Configurations.Add(new MedicsSchedulingDb_MedicsMap());
		}
	}
	public partial class MedicsSchedulingDBContextSL : DbContext {
		static MedicsSchedulingDBContextSL() {
			Database.SetInitializer<MedicsSchedulingDBContextSL>(null);
		}
		public MedicsSchedulingDBContextSL() : base("MedicsSchedulingDbConnectionStringSL") { }
		public DbSet<MedicsSchedulingDb_MedicalAppointments> MedicalAppointments { get; set; }
		public DbSet<MedicsSchedulingDb_Medics> Medics { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new MedicsSchedulingDb_MedicalAppointmentsMap());
			modelBuilder.Configurations.Add(new MedicsSchedulingDb_MedicsMap());
		}
	}
	public partial class MedicsSchedulingDb_MedicalAppointments {
		public int ID { get; set; }
		public Nullable<int> MedicId { get; set; }
		public string MedicIds { get; set; }
		public Nullable<int> Status { get; set; }
		public string Subject { get; set; }
		public string Description { get; set; }
		public Nullable<int> Label { get; set; }
		public Nullable<System.DateTime> StartTime { get; set; }
		public Nullable<System.DateTime> EndTime { get; set; }
		public string Location { get; set; }
		public bool AllDay { get; set; }
		public Nullable<int> EventType { get; set; }
		public string RecurrenceInfo { get; set; }
		public string ReminderInfo { get; set; }
		public string ContactInfo { get; set; }
	}
	public partial class MedicsSchedulingDb_Medics {
		public int ID { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Department { get; set; }
		public string Phone { get; set; }
		public byte[] PhotoBytes { get; set; }
	}
	#region Mapping
	public class MedicsSchedulingDb_MedicalAppointmentsMap : EntityTypeConfiguration<MedicsSchedulingDb_MedicalAppointments> {
		public MedicsSchedulingDb_MedicalAppointmentsMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.Subject)
				.HasMaxLength(50);
			this.Property(t => t.Location)
				.HasMaxLength(50);
			this.ToTable("MedicalAppointments");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.MedicId).HasColumnName("MedicId");
			this.Property(t => t.MedicIds).HasColumnName("MedicIds");
			this.Property(t => t.Status).HasColumnName("Status");
			this.Property(t => t.Subject).HasColumnName("Subject");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Label).HasColumnName("Label");
			this.Property(t => t.StartTime).HasColumnName("StartTime");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.Location).HasColumnName("Location");
			this.Property(t => t.AllDay).HasColumnName("AllDay");
			this.Property(t => t.EventType).HasColumnName("EventType");
			this.Property(t => t.RecurrenceInfo).HasColumnName("RecurrenceInfo");
			this.Property(t => t.ReminderInfo).HasColumnName("ReminderInfo");
			this.Property(t => t.ContactInfo).HasColumnName("ContactInfo");
		}
	}
	public class MedicsSchedulingDb_MedicsMap : EntityTypeConfiguration<MedicsSchedulingDb_Medics> {
		public MedicsSchedulingDb_MedicsMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.ID);
			this.Property(t => t.Name)
				.HasMaxLength(50);
			this.Property(t => t.DisplayName)
				.HasMaxLength(50);
			this.Property(t => t.Department)
				.HasMaxLength(50);
			this.Property(t => t.Phone)
				.HasMaxLength(50);
			this.ToTable("Medics");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.DisplayName).HasColumnName("DisplayName");
			this.Property(t => t.Department).HasColumnName("Department");
			this.Property(t => t.Phone).HasColumnName("Phone");
			this.Property(t => t.PhotoBytes).HasColumnName("PhotoBytes");
		}
	}
	#endregion
}
