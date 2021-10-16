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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace DevExpress.Web.Demos {
	public class CompanyEmployee {
		public int EmployeeID { get; set; }
		[Required(ErrorMessage = "First Name is required")]
		[StringLength(10, ErrorMessage = "Must be under 10 characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is required")]
		[StringLength(20, ErrorMessage = "Must be under 20 characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[StringLength(30, ErrorMessage = "Must be under 30 characters")]
		[Display(Name = "Position")]
		public string Title { get; set; }
		[Display(Name = "Address")]
		public string Address { get; set; }
		[Display(Name = "Birth Date")]
		public DateTime? BirthDate { get; set; }
		[Display(Name = "Hire Date")]
		public DateTime? HireDate { get; set; }
		[Display(Name = "Fire Date")]
		public DateTime? FireDate { get; set; }
		[Display(Name = "Reason")]
		public string FireReason { get; set; }
		[Display(Name = "New Company")]
		public string NewCompany { get; set; }
		[Display(Name = "References")]
		public string References { get; set; }
		public string Notes { get; set; }
	}
	public class CompanyEmployeesDataProviderSL: CompanyEmployeesDataProviderBase {
		protected override string SessionKey { get { return "2E8D8636-1414-4652-AD49-F0A2EE1499F9"; } }
		protected override IList<Employee> GetEmployees() {
			using (var dataContext = new NorthwindContextSL()) {
				return dataContext.Employees.ToList();
			}
		}
	}
	public class CompanyEmployeesDataProvider : CompanyEmployeesDataProviderBase {
		protected override string SessionKey { get { return "A3B3E8EC-CA0D-4735-9615-5E0CB40F215A"; } }
		protected override IList<Employee> GetEmployees() {
			return Mvc.NorthwindDataProvider.GetEmployees()
				.OfType<Employee>()
				.ToList();
		}
	}
	public abstract class CompanyEmployeesDataProviderBase {
		static readonly object GetEmployeesLocker = new object();
		protected abstract string SessionKey { get; }
		protected abstract IList<Employee> GetEmployees();
		public IList<CompanyEmployee> GetCompanyEmployees() {
			var customersList = HttpContext.Current.Session[SessionKey] as IList<CompanyEmployee>;
			lock (GetEmployeesLocker) {
				if (customersList == null) {
					customersList = new List<CompanyEmployee>();
					foreach (var emp in GetEmployees()) {
						customersList.Add(new CompanyEmployee() {
							EmployeeID = emp.EmployeeID,
							FirstName = emp.FirstName,
							LastName = emp.LastName,
							Address = String.Format("{0}, {1}, {2}", emp.Address, emp.City, emp.Country),
							BirthDate = emp.BirthDate.Value,
							HireDate = emp.HireDate.Value,
							Notes = emp.Notes,
							Title = emp.Title
						});
					}
					HttpContext.Current.Session[SessionKey] = customersList;
				}
			}
			return customersList;
		}
		public CompanyEmployee GetCompanyEmployeeByID(int emnployeeID) {
			return GetCompanyEmployees().Where(e => e.EmployeeID.Equals(emnployeeID)).SingleOrDefault();
		}
		public int GetNewCompanyEmployeeID() {
			IList<CompanyEmployee> companyEmployees = GetCompanyEmployees();
			if(companyEmployees.Count() == 0) return 1;
			return companyEmployees.OrderBy(e => e.EmployeeID).Last().EmployeeID + 1;
		}
		public void InsertCompanyEmployee(CompanyEmployee employee) {
			var newEmployee = new CompanyEmployee(){
				EmployeeID = GetNewCompanyEmployeeID(),
				FirstName = employee.FirstName,
				LastName = employee.FirstName,
				Title = employee.Title,
				Address = employee.Address,
				BirthDate = employee.BirthDate,
				HireDate = employee.HireDate,
				Notes = employee.Notes
			};
			GetCompanyEmployees().Add(newEmployee);
		}
		public void UpdateCompanyEmployee(CompanyEmployee employee) {
			CompanyEmployee editableEmployee = GetCompanyEmployeeByID(employee.EmployeeID);
			if(editableEmployee == null)
				return;
			editableEmployee.FirstName = employee.FirstName;
			editableEmployee.LastName = employee.FirstName;
			editableEmployee.Title = employee.Title;
			editableEmployee.Address = employee.Address;
			editableEmployee.BirthDate = employee.BirthDate;
			editableEmployee.HireDate = employee.HireDate;
			editableEmployee.Notes = employee.Notes;
			editableEmployee.FireDate = employee.FireDate;
			editableEmployee.FireReason = employee.FireReason;
			editableEmployee.NewCompany = employee.NewCompany;
			editableEmployee.References = employee.References;
		}
		public void DeleteCompanyEmployee(CompanyEmployee employee) {
			CompanyEmployee employeeToRemove = GetCompanyEmployeeByID(employee.EmployeeID);
			if(employeeToRemove == null)
				return;
			GetCompanyEmployees().Remove(employeeToRemove);
		}
	}
}
