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

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using DevExpress.Web.Demos;
namespace DevExpress.Web.Demos {
	public partial class LargeDatabaseContext : ContextBase {
		static LargeDatabaseContext() {
			Database.SetInitializer<LargeDatabaseContext>(null);
		}
		public LargeDatabaseContext() : base("LargeDatabaseConnectionString") { }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Person> Persons { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new EmailMap());
			modelBuilder.Configurations.Add(new PersonMap());
		}
	}
	public partial class Person {
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
	}
	public partial class Email {
		public int ID { get; set; }
		public string Subject { get; set; }
		public string From { get; set; }
		public System.DateTime Sent { get; set; }
		public long Size { get; set; }
		public bool HasAttachment { get; set; }
	}
	public class PersonMap : EntityTypeConfiguration<Person> {
		public PersonMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(32);
			this.Property(t => t.LastName)
				.IsRequired()
				.HasMaxLength(32);
			this.Property(t => t.Phone)
				.IsRequired()
				.HasMaxLength(20);
			this.ToTable("Persons");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.Phone).HasColumnName("Phone");
		}
	}
	public class EmailMap : EntityTypeConfiguration<Email> {
		public EmailMap() {
			this.HasKey(t => t.ID);
			this.Property(t => t.Subject)
				.IsRequired()
				.HasMaxLength(100);
			this.Property(t => t.From)
				.IsRequired()
				.HasMaxLength(32);
			this.ToTable("Emails");
			this.Property(t => t.ID).HasColumnName("ID");
			this.Property(t => t.Subject).HasColumnName("Subject");
			this.Property(t => t.From).HasColumnName("From");
			this.Property(t => t.Sent).HasColumnName("Sent");
			this.Property(t => t.Size).HasColumnName("Size");
			this.Property(t => t.HasAttachment).HasColumnName("HasAttachment");
		}
	}
}
