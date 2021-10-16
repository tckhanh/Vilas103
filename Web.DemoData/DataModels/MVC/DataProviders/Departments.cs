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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;
namespace DevExpress.Web.Demos {
	public class DepartmentDataProvider {
		const string DepartmentDataContextKey = "DXDepartmentContext";
		public static DepartmentsContext DB {
			get {
				if(HttpContext.Current.Items[DepartmentDataContextKey] == null)
					HttpContext.Current.Items[DepartmentDataContextKey] = new DepartmentsContext();
				return (DepartmentsContext)HttpContext.Current.Items[DepartmentDataContextKey];
			}
		}
		public static IList<Department> GetDepartments() {
			IList<Department> departments = HttpContext.Current.Session["DXDepartments"] as IList<Department>;
			if(departments == null) {
				departments = (from department in DB.Departments
							   select department).ToList();
				HttpContext.Current.Session["DXDepartments"] = departments;
			}
			return departments;
		}
		public static IList<EditableDepartment> GetEditableDepartments() {
			IList<EditableDepartment> objects = (IList<EditableDepartment>)HttpContext.Current.Session["DXEditableDepartments"];
			if(objects == null) {
				objects = (from dep in DB.Departments
						   select new EditableDepartment {
							   ID = dep.ID,
							   ParentID = dep.ParentID,
							   DepartmentName = dep.DepartmentName
						   }).ToList();
				HttpContext.Current.Session["DXEditableDepartments"] = objects;
			}
			return objects;
		}
		public static void Update(EditableDepartment department) {
			var editObject = GetEditableObject(department.ID);
			if(editObject != null) {
				editObject.DepartmentName = department.DepartmentName;
				editObject.ParentID = department.ParentID;
			}
		}
		public static void Delete(int departmentID) {
			var editObject = GetEditableObject(departmentID);
			if(editObject != null)
				GetEditableDepartments().Remove(editObject);
		}
		private static object ObjectInsertLock = new object();
		public static EditableDepartment Insert(EditableDepartment department) {
			lock(ObjectInsertLock) {
				var editObject = new EditableDepartment();
				editObject.ID = GetNextDepartmentID();
				editObject.DepartmentName = department.DepartmentName;
				editObject.ParentID = department.ParentID;
				return editObject;
			}
		}
		static int GetNextDepartmentID() {
			var deps = GetEditableDepartments();
			return deps.Any() ? deps.Select(d => d.ID).Max() + 1 : 0;
		}
		static EditableDepartment GetEditableObject(int departmentID) {
			return (from obj in GetEditableDepartments() where obj.ID == departmentID select obj).FirstOrDefault();
		}
	}
	public class EditableDepartment {
		public int ID { get; set; }
		public Nullable<int> ParentID { get; set; }
		public string DepartmentName { get; set; }
	}
}
