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
namespace DevExpress.Web.Demos {
	public partial class NorthwindContext : ContextBase {
		static NorthwindContext() {
			Database.SetInitializer<NorthwindContext>(null);
		}
		public NorthwindContext() : base("NorthwindConnectionString") { }
		public DbSet<Alphabetical_list_of_product> Alphabetical_list_of_products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Category_Sales_for_1997> Category_Sales_for_1997 { get; set; }
		public DbSet<Current_Product_List> Current_Product_Lists { get; set; }
		public DbSet<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_Cities { get; set; }
		public DbSet<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }
		public DbSet<CustomerDemographic> CustomerDemographics { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Order_Detail> Order_Details { get; set; }
		public DbSet<Order_Details_Extended> Order_Details_Extendeds { get; set; }
		public DbSet<Order_Subtotal> Order_Subtotals { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Orders_Qry> Orders_Qries { get; set; }
		public DbSet<Product_Sales_for_1997> Product_Sales_for_1997 { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Products_Above_Average_Price> Products_Above_Average_Prices { get; set; }
		public DbSet<Products_by_Category> Products_by_Categories { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Sales_by_Category> Sales_by_Categories { get; set; }
		public DbSet<Sales_Totals_by_Amount> Sales_Totals_by_Amounts { get; set; }
		public DbSet<Shipper> Shippers { get; set; }
		public DbSet<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_Quarters { get; set; }
		public DbSet<Summary_of_Sales_by_Year> Summary_of_Sales_by_Years { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Territory> Territories { get; set; }
		public DbSet<CategoryProduct> CategoryProducts { get; set; }
		public DbSet<CustomerReport> CustomerReports { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<OrderReport> OrderReports { get; set; }
		public DbSet<ProductReport> ProductReports { get; set; }
		public DbSet<SalesPerson> SalesPersons { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new Alphabetical_list_of_productMap());
			modelBuilder.Configurations.Add(new CategoryMap());
			modelBuilder.Configurations.Add(new Category_Sales_for_1997Map());
			modelBuilder.Configurations.Add(new Current_Product_ListMap());
			modelBuilder.Configurations.Add(new Customer_and_Suppliers_by_CityMap());
			modelBuilder.Configurations.Add(new CustomerCustomerDemoMap());
			modelBuilder.Configurations.Add(new CustomerDemographicMap());
			modelBuilder.Configurations.Add(new CustomerMap());
			modelBuilder.Configurations.Add(new EmployeeMap());
			modelBuilder.Configurations.Add(new Order_DetailMap());
			modelBuilder.Configurations.Add(new Order_Details_ExtendedMap());
			modelBuilder.Configurations.Add(new Order_SubtotalMap());
			modelBuilder.Configurations.Add(new OrderMap());
			modelBuilder.Configurations.Add(new Orders_QryMap());
			modelBuilder.Configurations.Add(new Product_Sales_for_1997Map());
			modelBuilder.Configurations.Add(new ProductMap());
			modelBuilder.Configurations.Add(new Products_Above_Average_PriceMap());
			modelBuilder.Configurations.Add(new Products_by_CategoryMap());
			modelBuilder.Configurations.Add(new RegionMap());
			modelBuilder.Configurations.Add(new Sales_by_CategoryMap());
			modelBuilder.Configurations.Add(new Sales_Totals_by_AmountMap());
			modelBuilder.Configurations.Add(new ShipperMap());
			modelBuilder.Configurations.Add(new Summary_of_Sales_by_QuarterMap());
			modelBuilder.Configurations.Add(new Summary_of_Sales_by_YearMap());
			modelBuilder.Configurations.Add(new SupplierMap());
			modelBuilder.Configurations.Add(new TerritoryMap());
			modelBuilder.Configurations.Add(new CategoryProductMap());
			modelBuilder.Configurations.Add(new CustomerReportMap());
			modelBuilder.Configurations.Add(new InvoiceMap());
			modelBuilder.Configurations.Add(new OrderDetailMap());
			modelBuilder.Configurations.Add(new OrderReportMap());
			modelBuilder.Configurations.Add(new ProductReportMap());
			modelBuilder.Configurations.Add(new SalesPersonMap());
		}
	}
	public partial class NorthwindContextSL : DbContext {
		static NorthwindContextSL() {
			Database.SetInitializer<NorthwindContextSL>(null);
		}
		public NorthwindContextSL() : base("NorthwindConnectionStringSL") { }
		public DbSet<Alphabetical_list_of_product> AlphabeticalListOfProducts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Current_Product_List> CurrentProductList { get; set; }
		public DbSet<Customer_and_Suppliers_by_City> CustomerAndSuppliersByCity { get; set; }
		public DbSet<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }
		public DbSet<CustomerDemographic> CustomerDemographics { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Order_Details_Extended> OrderDetailsExtended { get; set; }
		public DbSet<Order_Subtotal> OrderSubtotals { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Order_Detail> Order_Details { get; set; }
		public DbSet<Orders_Qry> OrdersQry { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Products_Above_Average_Price> ProductsAboveAveragePrice { get; set; }
		public DbSet<Products_by_Category> ProductsByCategory { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Sales_by_Category> SalesByCategory { get; set; }
		public DbSet<Sales_Totals_by_Amount> SalesTotalsByAmount { get; set; }
		public DbSet<Shipper> Shippers { get; set; }
		public DbSet<Summary_of_Sales_by_Quarter> SummaryOfSalesByQuarter { get; set; }
		public DbSet<Summary_of_Sales_by_Year> SummaryOfSalesByYear { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Territory> Territories { get; set; }
		public DbSet<CategoryProduct> CategoryProducts { get; set; }
		public DbSet<CustomerReport> CustomerReports { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<OrderReport> OrderReports { get; set; }
		public DbSet<ProductReport> ProductReports { get; set; }
		public DbSet<SalesPerson> SalesPersons { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new AlphabeticalListOfProductsMap());
			modelBuilder.Configurations.Add(new CategoryMap());
			modelBuilder.Configurations.Add(new CurrentProductListMap());
			modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityMap());
			modelBuilder.Configurations.Add(new CustomerCustomerDemoMap());
			modelBuilder.Configurations.Add(new CustomerDemographicMap());
			modelBuilder.Configurations.Add(new CustomerMap());
			modelBuilder.Configurations.Add(new EmployeeMap());
			modelBuilder.Configurations.Add(new OrderDetailsExtendedMap());
			modelBuilder.Configurations.Add(new OrderSubtotalsMap());
			modelBuilder.Configurations.Add(new OrderMap());
			modelBuilder.Configurations.Add(new OrdersQryMap());
			modelBuilder.Configurations.Add(new Order_DetailMap());
			modelBuilder.Configurations.Add(new ProductMap());
			modelBuilder.Configurations.Add(new ProductsAboveAveragePriceMap());
			modelBuilder.Configurations.Add(new ProductsByCategoryMap());
			modelBuilder.Configurations.Add(new RegionMap());
			modelBuilder.Configurations.Add(new SalesByCategoryMap());
			modelBuilder.Configurations.Add(new SalesTotalsByAmountMap());
			modelBuilder.Configurations.Add(new ShipperMap());
			modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterMap());
			modelBuilder.Configurations.Add(new SummaryOfSalesByYearMap());
			modelBuilder.Configurations.Add(new SupplierMap());
			modelBuilder.Configurations.Add(new TerritoryMap());
			modelBuilder.Configurations.Add(new CategoryProductMap());
			modelBuilder.Configurations.Add(new CustomerReportMap());
			modelBuilder.Configurations.Add(new InvoiceMap());
			modelBuilder.Configurations.Add(new OrderDetailMap());
			modelBuilder.Configurations.Add(new OrderReportMap());
			modelBuilder.Configurations.Add(new ProductReportMap());
			modelBuilder.Configurations.Add(new SalesPersonMap());
		}
	}
	public partial class Alphabetical_list_of_product {
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public Nullable<int> SupplierID { get; set; }
		public Nullable<int> CategoryID { get; set; }
		public string QuantityPerUnit { get; set; }
		public Nullable<decimal> UnitPrice { get; set; }
		public Nullable<short> UnitsInStock { get; set; }
		public Nullable<short> UnitsOnOrder { get; set; }
		public Nullable<short> ReorderLevel { get; set; }
		public bool Discontinued { get; set; }
		public string CategoryName { get; set; }
		public virtual Category Category { get; set; }
		public virtual Product Product { get; set; }
		public virtual Supplier Supplier { get; set; }
	}
	public partial class Category {
		public Category() {
			this.Products = new List<Product>();
		}
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public byte[] Picture { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
	public partial class Category_Sales_for_1997 {
		public string CategoryName { get; set; }
		public Nullable<decimal> CategorySales { get; set; }
	}
	public partial class CategoryProduct {
		public int ProductID { get; set; }
		public Nullable<int> SupplierID { get; set; }
		public string ProductName { get; set; }
		public string CategoryName { get; set; }
		public byte[] Picture { get; set; }
		public string Description { get; set; }
	}
	public partial class Current_Product_List {
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public virtual Product Product { get; set; }
	}
	public partial class Customer {
		public Customer() {
			this.Orders_Qry = new List<Orders_Qry>();
		}
		public string CustomerID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public virtual ICollection<Orders_Qry> Orders_Qry { get; set; }
	}
	public partial class Customer_and_Suppliers_by_City {
		public string City { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string Relationship { get; set; }
	}
	public partial class CustomerCustomerDemo {
		public string CustomerID { get; set; }
		public string CustomerTypeID { get; set; }
	}
	public partial class CustomerDemographic {
		public string CustomerTypeID { get; set; }
		public string CustomerDesc { get; set; }
	}
	public partial class CustomerReport {
		public string ProductName { get; set; }
		public string CompanyName { get; set; }
		public Nullable<System.DateTime> OrderDate { get; set; }
		public Nullable<decimal> ProductAmount { get; set; }
	}
	public partial class Employee {
		public Employee() {
			this.Orders_Qry = new List<Orders_Qry>();
			this.Orders = new List<Order>();
			this.Orders1 = new List<Order>();
			this.Territories = new List<Territory>();
		}
		public int EmployeeID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Title { get; set; }
		public string TitleOfCourtesy { get; set; }
		public Nullable<System.DateTime> BirthDate { get; set; }
		public Nullable<System.DateTime> HireDate { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string HomePhone { get; set; }
		public string Extension { get; set; }
		public byte[] Photo { get; set; }
		public string Notes { get; set; }
		public Nullable<int> ReportsTo { get; set; }
		public string PhotoPath { get; set; }
		public virtual ICollection<Orders_Qry> Orders_Qry { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Order> Orders1 { get; set; }
		public virtual ICollection<Territory> Territories { get; set; }
	}
	public partial class Invoice {
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
		public string CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Salesperson { get; set; }
		public int OrderID { get; set; }
		public Nullable<System.DateTime> OrderDate { get; set; }
		public Nullable<System.DateTime> RequiredDate { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public string ShipperName { get; set; }
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public Nullable<decimal> ExtendedPrice { get; set; }
		public Nullable<decimal> Freight { get; set; }
	}
	public partial class Order {
		public Order() {
			this.Order_Details = new List<Order_Detail>();
			this.Order_Details_Extended = new List<Order_Details_Extended>();
			this.Orders_Qry = new List<Orders_Qry>();
			this.Sales_Totals_by_Amount = new List<Sales_Totals_by_Amount>();
		}
		public int OrderID { get; set; }
		public string CustomerID { get; set; }
		public Nullable<int> EmployeeID { get; set; }
		public Nullable<System.DateTime> OrderDate { get; set; }
		public Nullable<System.DateTime> RequiredDate { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public Nullable<int> ShipVia { get; set; }
		public Nullable<decimal> Freight { get; set; }
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual Employee Employee1 { get; set; }
		public virtual ICollection<Order_Detail> Order_Details { get; set; }
		public virtual ICollection<Order_Details_Extended> Order_Details_Extended { get; set; }
		public virtual Order_Subtotal Order_Subtotals { get; set; }
		public virtual ICollection<Orders_Qry> Orders_Qry { get; set; }
		public virtual ICollection<Sales_Totals_by_Amount> Sales_Totals_by_Amount { get; set; }
		public virtual Summary_of_Sales_by_Quarter Summary_of_Sales_by_Quarter { get; set; }
		public virtual Summary_of_Sales_by_Year Summary_of_Sales_by_Year { get; set; }
	}
	public partial class Order_Detail {
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
	public partial class Order_Details_Extended {
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public Nullable<decimal> ExtendedPrice { get; set; }
		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
	public partial class Order_Subtotal {
		public int OrderID { get; set; }
		public Nullable<decimal> Subtotal { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class OrderDetail {
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public short Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public float Discount { get; set; }
		public string ProductName { get; set; }
		public string Supplier { get; set; }
	}
	public partial class OrderReport {
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public Nullable<decimal> Extended_Price { get; set; }
	}
	public partial class Orders_Qry {
		public int OrderID { get; set; }
		public string CustomerID { get; set; }
		public Nullable<int> EmployeeID { get; set; }
		public Nullable<System.DateTime> OrderDate { get; set; }
		public Nullable<System.DateTime> RequiredDate { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public Nullable<int> ShipVia { get; set; }
		public Nullable<decimal> Freight { get; set; }
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class Product {
		public Product() {
		}
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public Nullable<int> SupplierID { get; set; }
		public Nullable<int> CategoryID { get; set; }
		public string QuantityPerUnit { get; set; }
		public Nullable<decimal> UnitPrice { get; set; }
		public Nullable<short> UnitsInStock { get; set; }
		public Nullable<short> UnitsOnOrder { get; set; }
		public Nullable<short> ReorderLevel { get; set; }
		public bool Discontinued { get; set; }
		public string EAN13 { get; set; }
		public virtual Category Category { get; set; }
		public virtual Supplier Supplier { get; set; }
	}
	public partial class Product_Sales_for_1997 {
		public string CategoryName { get; set; }
		public string ProductName { get; set; }
		public Nullable<decimal> ProductSales { get; set; }
	}
	public partial class ProductReport {
		public string CategoryName { get; set; }
		public string ProductName { get; set; }
		public Nullable<decimal> ProductSales { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
	}
	public partial class Products_Above_Average_Price {
		public string ProductName { get; set; }
		public Nullable<decimal> UnitPrice { get; set; }
	}
	public partial class Products_by_Category {
		public string CategoryName { get; set; }
		public string ProductName { get; set; }
		public string QuantityPerUnit { get; set; }
		public Nullable<short> UnitsInStock { get; set; }
		public bool Discontinued { get; set; }
	}
	public partial class Region {
		public Region() {
			this.Territories = new List<Territory>();
		}
		public int RegionID { get; set; }
		public string RegionDescription { get; set; }
		public virtual ICollection<Territory> Territories { get; set; }
	}
	public partial class Sales_by_Category {
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public string ProductName { get; set; }
		public Nullable<decimal> ProductSales { get; set; }
		public virtual Category Category { get; set; }
	}
	public partial class Sales_Totals_by_Amount {
		public Nullable<decimal> SaleAmount { get; set; }
		public int OrderID { get; set; }
		public string CompanyName { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class SalesTotalsByAmount {
		public Nullable<decimal> SaleAmount { get; set; }
		public int OrderID { get; set; }
		public string CompanyName { get; set; }
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class SalesPerson {
		public int OrderID { get; set; }
		public string Country { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ProductName { get; set; }
		public string CategoryName { get; set; }
		public Nullable<System.DateTime> OrderDate { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public Nullable<decimal> Extended_Price { get; set; }
		public string Sales_Person { get; set; }
	}
	public partial class Shipper {
		public int ShipperID { get; set; }
		public string CompanyName { get; set; }
		public string Phone { get; set; }
	}
	public partial class Summary_of_Sales_by_Quarter {
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public int OrderID { get; set; }
		public Nullable<decimal> Subtotal { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class Summary_of_Sales_by_Year {
		public Nullable<System.DateTime> ShippedDate { get; set; }
		public int OrderID { get; set; }
		public Nullable<decimal> Subtotal { get; set; }
		public virtual Order Order { get; set; }
	}
	public partial class Supplier {
		public Supplier() {
			this.Products = new List<Product>();
		}
		public int SupplierID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string HomePage { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
	public partial class Territory {
		public Territory() {
			this.Employees = new List<Employee>();
		}
		public string TerritoryID { get; set; }
		public string TerritoryDescription { get; set; }
		public int RegionID { get; set; }
		public virtual Region Region { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }
	}
	#region Mapping
	public class Alphabetical_list_of_productMap : EntityTypeConfiguration<Alphabetical_list_of_product> {
		public Alphabetical_list_of_productMap() {
			this.HasKey(t => new { t.ProductID, t.ProductName, t.Discontinued, t.CategoryName });
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.QuantityPerUnit)
				.HasMaxLength(20);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.ToTable("Alphabetical list of products");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.SupplierID).HasColumnName("SupplierID");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
			this.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
			this.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
			this.Property(t => t.Discontinued).HasColumnName("Discontinued");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
		}
	}
	public class AlphabeticalListOfProductsMap : EntityTypeConfiguration<Alphabetical_list_of_product> {
		public AlphabeticalListOfProductsMap() {
			this.HasKey(t => new { t.ProductID, t.ProductName, t.Discontinued, t.CategoryName });
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.QuantityPerUnit)
				.HasMaxLength(20);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.ToTable("AlphabeticalListOfProducts");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.SupplierID).HasColumnName("SupplierID");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
			this.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
			this.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
			this.Property(t => t.Discontinued).HasColumnName("Discontinued");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
		}
	}
	public class Category_Sales_for_1997Map : EntityTypeConfiguration<Category_Sales_for_1997> {
		public Category_Sales_for_1997Map() {
			this.HasKey(t => t.CategoryName);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.ToTable("Category Sales for 1997");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.CategorySales).HasColumnName("CategorySales");
		}
	}
	public class CategoryMap : EntityTypeConfiguration<Category> {
		public CategoryMap() {
			this.HasKey(t => t.CategoryID);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.ToTable("Categories");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Picture).HasColumnName("Picture");
		}
	}
	public class CategoryProductMap : EntityTypeConfiguration<CategoryProduct> {
		public CategoryProductMap() {
			this.HasKey(t => new { t.ProductID, t.ProductName, t.CategoryName });
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.ToTable("CategoryProducts");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.SupplierID).HasColumnName("SupplierID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.Picture).HasColumnName("Picture");
			this.Property(t => t.Description).HasColumnName("Description");
		}
	}
	public class Current_Product_ListMap : EntityTypeConfiguration<Current_Product_List> {
		public Current_Product_ListMap() {
			this.HasKey(t => new { t.ProductID, t.ProductName });
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("Current Product List");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
		}
	}
	public class CurrentProductListMap : EntityTypeConfiguration<Current_Product_List> {
		public CurrentProductListMap() {
			this.HasKey(t => new { t.ProductID, t.ProductName });
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("CurrentProductList");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
		}
	}
	public class Customer_and_Suppliers_by_CityMap : EntityTypeConfiguration<Customer_and_Suppliers_by_City> {
		public Customer_and_Suppliers_by_CityMap() {
			this.HasKey(t => new { t.CompanyName, t.Relationship });
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.ContactName)
				.HasMaxLength(30);
			this.Property(t => t.Relationship)
				.IsRequired()
				.HasMaxLength(9);
			this.ToTable("Customer and Suppliers by City");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ContactName).HasColumnName("ContactName");
			this.Property(t => t.Relationship).HasColumnName("Relationship");
		}
	}
	public class CustomerAndSuppliersByCityMap : EntityTypeConfiguration<Customer_and_Suppliers_by_City> {
		public CustomerAndSuppliersByCityMap() {
			this.HasKey(t => new { t.CompanyName, t.Relationship });
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.ContactName)
				.HasMaxLength(30);
			this.Property(t => t.Relationship)
				.IsRequired()
				.HasMaxLength(9);
			this.ToTable("CustomerAndSuppliersByCity");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ContactName).HasColumnName("ContactName");
			this.Property(t => t.Relationship).HasColumnName("Relationship");
		}
	}
	public class CustomerCustomerDemoMap : EntityTypeConfiguration<CustomerCustomerDemo> {
		public CustomerCustomerDemoMap() {
			this.HasKey(t => new { t.CustomerID, t.CustomerTypeID });
			this.Property(t => t.CustomerID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.CustomerTypeID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(10);
			this.ToTable("CustomerCustomerDemo");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
		}
	}
	public class CustomerDemographicMap : EntityTypeConfiguration<CustomerDemographic> {
		public CustomerDemographicMap() {
			this.HasKey(t => t.CustomerTypeID);
			this.Property(t => t.CustomerTypeID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(10);
			this.ToTable("CustomerDemographics");
			this.Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
			this.Property(t => t.CustomerDesc).HasColumnName("CustomerDesc");
		}
	}
	public class CustomerMap : EntityTypeConfiguration<Customer> {
		public CustomerMap() {
			this.HasKey(t => t.CustomerID);
			this.Property(t => t.CustomerID)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.ContactName)
				.HasMaxLength(30);
			this.Property(t => t.ContactTitle)
				.HasMaxLength(30);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.Property(t => t.Phone)
				.HasMaxLength(24);
			this.Property(t => t.Fax)
				.HasMaxLength(24);
			this.ToTable("Customers");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ContactName).HasColumnName("ContactName");
			this.Property(t => t.ContactTitle).HasColumnName("ContactTitle");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.Property(t => t.Phone).HasColumnName("Phone");
			this.Property(t => t.Fax).HasColumnName("Fax");
		}
	}
	public class CustomerReportMap : EntityTypeConfiguration<CustomerReport> {
		public CustomerReportMap() {
			this.HasKey(t => new { t.ProductName, t.CompanyName });
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("CustomerReports");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.ProductAmount).HasColumnName("ProductAmount");
		}
	}
	public class EmployeeMap : EntityTypeConfiguration<Employee> {
		public EmployeeMap() {
			this.HasKey(t => t.EmployeeID);
			this.Property(t => t.LastName)
				.IsRequired()
				.HasMaxLength(20);
			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(10);
			this.Property(t => t.Title)
				.HasMaxLength(30);
			this.Property(t => t.TitleOfCourtesy)
				.HasMaxLength(25);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.Property(t => t.HomePhone)
				.HasMaxLength(24);
			this.Property(t => t.Extension)
				.HasMaxLength(4);
			this.Property(t => t.PhotoPath)
				.HasMaxLength(255);
			this.ToTable("Employees");
			this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy");
			this.Property(t => t.BirthDate).HasColumnName("BirthDate");
			this.Property(t => t.HireDate).HasColumnName("HireDate");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.Property(t => t.HomePhone).HasColumnName("HomePhone");
			this.Property(t => t.Extension).HasColumnName("Extension");
			this.Property(t => t.Photo).HasColumnName("Photo");
			this.Property(t => t.Notes).HasColumnName("Notes");
			this.Property(t => t.ReportsTo).HasColumnName("ReportsTo");
			this.Property(t => t.PhotoPath).HasColumnName("PhotoPath");
			this.HasMany(t => t.Territories)
				.WithMany(t => t.Employees)
				.Map(m => {
					m.ToTable("EmployeeTerritories");
					m.MapLeftKey("EmployeeID");
					m.MapRightKey("TerritoryID");
				});
		}
	}
	public class InvoiceMap : EntityTypeConfiguration<Invoice> {
		public InvoiceMap() {
			this.HasKey(t => new { t.CustomerName, t.Salesperson, t.OrderID, t.ShipperName, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
			this.Property(t => t.ShipName)
				.HasMaxLength(40);
			this.Property(t => t.ShipAddress)
				.HasMaxLength(60);
			this.Property(t => t.ShipCity)
				.HasMaxLength(15);
			this.Property(t => t.ShipRegion)
				.HasMaxLength(15);
			this.Property(t => t.ShipPostalCode)
				.HasMaxLength(10);
			this.Property(t => t.ShipCountry)
				.HasMaxLength(15);
			this.Property(t => t.CustomerID)
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.CustomerName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.Property(t => t.Salesperson)
				.IsRequired()
				.HasMaxLength(31);
			this.Property(t => t.OrderID);
			this.Property(t => t.ShipperName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.ToTable("Invoices");
			this.Property(t => t.ShipName).HasColumnName("ShipName");
			this.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
			this.Property(t => t.ShipCity).HasColumnName("ShipCity");
			this.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
			this.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
			this.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.CustomerName).HasColumnName("CustomerName");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.Property(t => t.Salesperson).HasColumnName("Salesperson");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.ShipperName).HasColumnName("ShipperName");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
			this.Property(t => t.Freight).HasColumnName("Freight");
		}
	}
	public class Order_DetailMap : EntityTypeConfiguration<Order_Detail> {
		public Order_DetailMap() {
			this.HasKey(t => new { t.OrderID, t.ProductID, t.UnitPrice, t.Quantity, t.Discount });
			this.Property(t => t.OrderID);
			this.Property(t => t.ProductID);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.ToTable("Order Details");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Order_Details)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class Order_Details_ExtendedMap : EntityTypeConfiguration<Order_Details_Extended> {
		public Order_Details_ExtendedMap() {
			this.HasKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
			this.Property(t => t.OrderID);
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.ToTable("Order Details Extended");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Order_Details_Extended)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class OrderDetailsExtendedMap : EntityTypeConfiguration<Order_Details_Extended> {
		public OrderDetailsExtendedMap() {
			this.HasKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
			this.Property(t => t.OrderID);
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.ToTable("OrderDetailsExtended");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Order_Details_Extended)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class Order_SubtotalMap : EntityTypeConfiguration<Order_Subtotal> {
		public Order_SubtotalMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("Order Subtotals");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Order_Subtotals);
		}
	}
	public class OrderSubtotalsMap : EntityTypeConfiguration<Order_Subtotal> {
		public OrderSubtotalsMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("OrderSubtotals");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Order_Subtotals);
		}
	}
	public class OrderDetailMap : EntityTypeConfiguration<OrderDetail> {
		public OrderDetailMap() {
			this.HasKey(t => new { t.OrderID, t.Quantity, t.UnitPrice, t.Discount, t.ProductName });
			this.Property(t => t.OrderID);
			this.Property(t => t.Quantity);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.Supplier)
				.HasMaxLength(217);
			this.ToTable("OrderDetails");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.Supplier).HasColumnName("Supplier");
		}
	}
	public class OrderMap : EntityTypeConfiguration<Order> {
		public OrderMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.CustomerID)
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.ShipName)
				.HasMaxLength(40);
			this.Property(t => t.ShipAddress)
				.HasMaxLength(60);
			this.Property(t => t.ShipCity)
				.HasMaxLength(15);
			this.Property(t => t.ShipRegion)
				.HasMaxLength(15);
			this.Property(t => t.ShipPostalCode)
				.HasMaxLength(10);
			this.Property(t => t.ShipCountry)
				.HasMaxLength(15);
			this.ToTable("Orders");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.ShipVia).HasColumnName("ShipVia");
			this.Property(t => t.Freight).HasColumnName("Freight");
			this.Property(t => t.ShipName).HasColumnName("ShipName");
			this.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
			this.Property(t => t.ShipCity).HasColumnName("ShipCity");
			this.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
			this.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
			this.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
			this.HasOptional(t => t.Employee)
				.WithMany(t => t.Orders)
				.HasForeignKey(d => d.EmployeeID);
			this.HasOptional(t => t.Employee1)
				.WithMany(t => t.Orders1)
				.HasForeignKey(d => d.EmployeeID);
		}
	}
	public class OrderReportMap : EntityTypeConfiguration<OrderReport> {
		public OrderReportMap() {
			this.HasKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
			this.Property(t => t.OrderID);
			this.Property(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.ToTable("OrderReports");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.Extended_Price).HasColumnName("Extended Price");
		}
	}
	public class Orders_QryMap : EntityTypeConfiguration<Orders_Qry> {
		public Orders_QryMap() {
			this.HasKey(t => new { t.OrderID, t.CompanyName });
			this.Property(t => t.OrderID);
			this.Property(t => t.CustomerID)
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.ShipName)
				.HasMaxLength(40);
			this.Property(t => t.ShipAddress)
				.HasMaxLength(60);
			this.Property(t => t.ShipCity)
				.HasMaxLength(15);
			this.Property(t => t.ShipRegion)
				.HasMaxLength(15);
			this.Property(t => t.ShipPostalCode)
				.HasMaxLength(10);
			this.Property(t => t.ShipCountry)
				.HasMaxLength(15);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.ToTable("Orders Qry");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.ShipVia).HasColumnName("ShipVia");
			this.Property(t => t.Freight).HasColumnName("Freight");
			this.Property(t => t.ShipName).HasColumnName("ShipName");
			this.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
			this.Property(t => t.ShipCity).HasColumnName("ShipCity");
			this.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
			this.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
			this.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.HasOptional(t => t.Customer)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.CustomerID);
			this.HasOptional(t => t.Employee)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.EmployeeID);
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class OrdersQryMap : EntityTypeConfiguration<Orders_Qry> {
		public OrdersQryMap() {
			this.HasKey(t => new { t.OrderID, t.CompanyName });
			this.Property(t => t.OrderID);
			this.Property(t => t.CustomerID)
				.IsFixedLength()
				.HasMaxLength(5);
			this.Property(t => t.ShipName)
				.HasMaxLength(40);
			this.Property(t => t.ShipAddress)
				.HasMaxLength(60);
			this.Property(t => t.ShipCity)
				.HasMaxLength(15);
			this.Property(t => t.ShipRegion)
				.HasMaxLength(15);
			this.Property(t => t.ShipPostalCode)
				.HasMaxLength(10);
			this.Property(t => t.ShipCountry)
				.HasMaxLength(15);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.ToTable("OrdersQry");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.CustomerID).HasColumnName("CustomerID");
			this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.ShipVia).HasColumnName("ShipVia");
			this.Property(t => t.Freight).HasColumnName("Freight");
			this.Property(t => t.ShipName).HasColumnName("ShipName");
			this.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
			this.Property(t => t.ShipCity).HasColumnName("ShipCity");
			this.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
			this.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
			this.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.HasOptional(t => t.Customer)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.CustomerID);
			this.HasOptional(t => t.Employee)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.EmployeeID);
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Orders_Qry)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class Product_Sales_for_1997Map : EntityTypeConfiguration<Product_Sales_for_1997> {
		public Product_Sales_for_1997Map() {
			this.HasKey(t => new { t.CategoryName, t.ProductName });
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("Product Sales for 1997");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.ProductSales).HasColumnName("ProductSales");
		}
	}
	public class ProductMap : EntityTypeConfiguration<Product> {
		public ProductMap() {
			this.HasKey(t => t.ProductID);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.QuantityPerUnit)
				.HasMaxLength(20);
			this.ToTable("Products");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.SupplierID).HasColumnName("SupplierID");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
			this.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
			this.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
			this.Property(t => t.Discontinued).HasColumnName("Discontinued");
			this.Property(t => t.EAN13).HasColumnName("EAN13");
			this.HasOptional(t => t.Category)
				.WithMany(t => t.Products)
				.HasForeignKey(d => d.CategoryID);
			this.HasOptional(t => t.Supplier)
				.WithMany(t => t.Products)
				.HasForeignKey(d => d.SupplierID);
		}
	}
	public class ProductReportMap : EntityTypeConfiguration<ProductReport> {
		public ProductReportMap() {
			this.HasKey(t => new { t.CategoryName, t.ProductName });
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("ProductReports");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.ProductSales).HasColumnName("ProductSales");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
		}
	}
	public class Products_Above_Average_PriceMap : EntityTypeConfiguration<Products_Above_Average_Price> {
		public Products_Above_Average_PriceMap() {
			this.HasKey(t => t.ProductName);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("Products Above Average Price");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
		}
	}
	public class ProductsAboveAveragePriceMap : EntityTypeConfiguration<Products_Above_Average_Price> {
		public ProductsAboveAveragePriceMap() {
			this.HasKey(t => t.ProductName);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("ProductsAboveAveragePrice");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
		}
	}
	public class Products_by_CategoryMap : EntityTypeConfiguration<Products_by_Category> {
		public Products_by_CategoryMap() {
			this.HasKey(t => new { t.CategoryName, t.ProductName, t.Discontinued });
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.QuantityPerUnit)
				.HasMaxLength(20);
			this.ToTable("Products by Category");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
			this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
			this.Property(t => t.Discontinued).HasColumnName("Discontinued");
		}
	}
	public class ProductsByCategoryMap : EntityTypeConfiguration<Products_by_Category> {
		public ProductsByCategoryMap() {
			this.HasKey(t => new { t.CategoryName, t.ProductName, t.Discontinued });
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.QuantityPerUnit)
				.HasMaxLength(20);
			this.ToTable("ProductsByCategory");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
			this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
			this.Property(t => t.Discontinued).HasColumnName("Discontinued");
		}
	}
	public class RegionMap : EntityTypeConfiguration<Region> {
		public RegionMap() {
			this.HasKey(t => t.RegionID);
			this.Property(t => t.RegionDescription)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(50);
			this.ToTable("Region");
			this.Property(t => t.RegionID).HasColumnName("RegionID");
			this.Property(t => t.RegionDescription).HasColumnName("RegionDescription");
		}
	}
	public class Sales_by_CategoryMap : EntityTypeConfiguration<Sales_by_Category> {
		public Sales_by_CategoryMap() {
			this.HasKey(t => new { t.CategoryID, t.CategoryName, t.ProductName });
			this.Property(t => t.CategoryID);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("Sales by Category");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.ProductSales).HasColumnName("ProductSales");
		}
	}
	public class SalesByCategoryMap : EntityTypeConfiguration<Sales_by_Category> {
		public SalesByCategoryMap() {
			this.HasKey(t => new { t.CategoryID, t.CategoryName, t.ProductName });
			this.Property(t => t.CategoryID);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("SalesByCategory");
			this.Property(t => t.CategoryID).HasColumnName("CategoryID");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.ProductSales).HasColumnName("ProductSales");
		}
	}
	public class Sales_Totals_by_AmountMap : EntityTypeConfiguration<Sales_Totals_by_Amount> {
		public Sales_Totals_by_AmountMap() {
			this.HasKey(t => new { t.OrderID, t.CompanyName });
			this.Property(t => t.OrderID);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("Sales Totals by Amount");
			this.Property(t => t.SaleAmount).HasColumnName("SaleAmount");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Sales_Totals_by_Amount)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class SalesTotalsByAmountMap : EntityTypeConfiguration<Sales_Totals_by_Amount> {
		public SalesTotalsByAmountMap() {
			this.HasKey(t => new { t.OrderID, t.CompanyName });
			this.Property(t => t.OrderID);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.ToTable("SalesTotalsByAmount");
			this.Property(t => t.SaleAmount).HasColumnName("SaleAmount");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.HasRequired(t => t.Order)
				.WithMany(t => t.Sales_Totals_by_Amount)
				.HasForeignKey(d => d.OrderID);
		}
	}
	public class SalesPersonMap : EntityTypeConfiguration<SalesPerson> {
		public SalesPersonMap() {
			this.HasKey(t => new { t.OrderID, t.FirstName, t.LastName, t.ProductName, t.CategoryName, t.UnitPrice, t.Quantity, t.Discount, t.Sales_Person });
			this.Property(t => t.OrderID);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(10);
			this.Property(t => t.LastName)
				.IsRequired()
				.HasMaxLength(20);
			this.Property(t => t.ProductName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.CategoryName)
				.IsRequired()
				.HasMaxLength(15);
			this.Property(t => t.UnitPrice);
			this.Property(t => t.Quantity);
			this.Property(t => t.Sales_Person)
				.IsRequired()
				.HasMaxLength(31);
			this.ToTable("SalesPerson");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Country).HasColumnName("Country");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.ProductName).HasColumnName("ProductName");
			this.Property(t => t.CategoryName).HasColumnName("CategoryName");
			this.Property(t => t.OrderDate).HasColumnName("OrderDate");
			this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.Discount).HasColumnName("Discount");
			this.Property(t => t.Extended_Price).HasColumnName("Extended Price");
			this.Property(t => t.Sales_Person).HasColumnName("Sales Person");
		}
	}
	public class ShipperMap : EntityTypeConfiguration<Shipper> {
		public ShipperMap() {
			this.HasKey(t => t.ShipperID);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.Phone)
				.HasMaxLength(24);
			this.ToTable("Shippers");
			this.Property(t => t.ShipperID).HasColumnName("ShipperID");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.Phone).HasColumnName("Phone");
		}
	}
	public class Summary_of_Sales_by_QuarterMap : EntityTypeConfiguration<Summary_of_Sales_by_Quarter> {
		public Summary_of_Sales_by_QuarterMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("Summary of Sales by Quarter");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Summary_of_Sales_by_Quarter);
		}
	}
	public class SummaryOfSalesByQuarterMap : EntityTypeConfiguration<Summary_of_Sales_by_Quarter> {
		public SummaryOfSalesByQuarterMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("SummaryOfSalesByQuarter");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Summary_of_Sales_by_Quarter);
		}
	}
	public class Summary_of_Sales_by_YearMap : EntityTypeConfiguration<Summary_of_Sales_by_Year> {
		public Summary_of_Sales_by_YearMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("Summary of Sales by Year");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Summary_of_Sales_by_Year);
		}
	}
	public class SummaryOfSalesByYearMap : EntityTypeConfiguration<Summary_of_Sales_by_Year> {
		public SummaryOfSalesByYearMap() {
			this.HasKey(t => t.OrderID);
			this.Property(t => t.OrderID);
			this.ToTable("SummaryOfSalesByYear");
			this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
			this.Property(t => t.OrderID).HasColumnName("OrderID");
			this.Property(t => t.Subtotal).HasColumnName("Subtotal");
			this.HasRequired(t => t.Order)
				.WithOptional(t => t.Summary_of_Sales_by_Year);
		}
	}
	public class SupplierMap : EntityTypeConfiguration<Supplier> {
		public SupplierMap() {
			this.HasKey(t => t.SupplierID);
			this.Property(t => t.CompanyName)
				.IsRequired()
				.HasMaxLength(40);
			this.Property(t => t.ContactName)
				.HasMaxLength(30);
			this.Property(t => t.ContactTitle)
				.HasMaxLength(30);
			this.Property(t => t.Address)
				.HasMaxLength(60);
			this.Property(t => t.City)
				.HasMaxLength(15);
			this.Property(t => t.Region)
				.HasMaxLength(15);
			this.Property(t => t.PostalCode)
				.HasMaxLength(10);
			this.Property(t => t.Country)
				.HasMaxLength(15);
			this.Property(t => t.Phone)
				.HasMaxLength(24);
			this.Property(t => t.Fax)
				.HasMaxLength(24);
			this.ToTable("Suppliers");
			this.Property(t => t.SupplierID).HasColumnName("SupplierID");
			this.Property(t => t.CompanyName).HasColumnName("CompanyName");
			this.Property(t => t.ContactName).HasColumnName("ContactName");
			this.Property(t => t.ContactTitle).HasColumnName("ContactTitle");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.City).HasColumnName("City");
			this.Property(t => t.Region).HasColumnName("Region");
			this.Property(t => t.PostalCode).HasColumnName("PostalCode");
			this.Property(t => t.Country).HasColumnName("Country");
			this.Property(t => t.Phone).HasColumnName("Phone");
			this.Property(t => t.Fax).HasColumnName("Fax");
			this.Property(t => t.HomePage).HasColumnName("HomePage");
		}
	}
	public class TerritoryMap : EntityTypeConfiguration<Territory> {
		public TerritoryMap() {
			this.HasKey(t => t.TerritoryID);
			this.Property(t => t.TerritoryID)
				.IsRequired()
				.HasMaxLength(20);
			this.Property(t => t.TerritoryDescription)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(50);
			this.ToTable("Territories");
			this.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
			this.Property(t => t.TerritoryDescription).HasColumnName("TerritoryDescription");
			this.Property(t => t.RegionID).HasColumnName("RegionID");
			this.HasRequired(t => t.Region)
				.WithMany(t => t.Territories)
				.HasForeignKey(d => d.RegionID);
		}
	}
	#endregion
}
