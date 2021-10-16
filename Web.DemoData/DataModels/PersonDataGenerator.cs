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
using System.Text;
namespace DevExpress.Web.Demos {
	public static class PersonDataGenerator {
		static DatabaseGenerator.TableData table;
		static readonly string[] firstNames = new string[] {
			"Aaron", "Abby", "Abigail", "Adam", "Adriana", "Aimee", "Alan", "Albert", "Alberto", "Alejandro", "Alex", "Alexander", 
			"Alexandra", "Alexandria", "Alexia", "Alfredo", "Alicia", "Alisha", "Alison", "Allison", "Alvin", "Alyssa", "Amanda", 
			"Amy", "Ana", "Andrea", "Andres", "Andy", "Angela", "Angelica", "Ann", "Anna", "April", "Ariana", "Arianna", "Armando", 
			"Arthur", "Arturo", "Ashlee", "Ashley", "Audrey", "Austin", "Bailey", "Barbara", "Beth", "Bethany", "Bianca", "Billy", 
			"Blake", "Bob", "Bobby", "Bonnie", "Brandi", "Brandon", "Brandy", "Brenda", "Brendan", "Brett", "Brian", "Briana", 
			"Brianna", "Bridget", "Brittany", "Brittney", "Brooke", "Bruce", "Bryan", "Bryce", "Caitlin", "Caleb", "Cameron", 
			"Candace", "Candice", "Cara", "Carl", "Carmen", "Carol", "Caroline", "Carrie", "Casey", "Cassandra", "Cassidy", 
			"Cedric", "Chad", "Charles", "Cheryl", "Chloe", "Christina", "Christopher", "Christy", "Clayton", "Clifford", "Clinton", 
			"Cody", "Cole", "Colin", "Colleen", "Connor", "Cory", "Courtney", "Crystal", "Cynthia", "Daisy", "Dakota", "Dale", "Dalton",
			"Damien", "Dan", "Dana", "Danielle", "Danny", "Darren", "David", "Dawn", "Deanna", "Deborah", "Debra", "Denean", "Dennis", 
			"Derrick", "Desiree", "Destiny", "Devin", "Devon", "Diana", "Diane", "Dominique", "Don", "Donald", "Donna", "Douglas", 
			"Drew", "Dustin", "Dwayne", "Dylan", "Ebony", "Eddie", "Edgar", "Eduardo", "Edward", "Edwin", "Elena", "Elijah", "Elsa", 
			"Emily", "Emma", "Emmanuel", "Erica", "Erika", "Erin", "Ethan", "Evan", "Evelyn", "Faith", "Fernando", "Francisco", "Frank", 
			"Franklin", "Fred", "Frederick", "Gabriel", "Gabrielle", "Garrett", "Gary", "George", "Gerald", "Gilbert", "Glenn", "Grant", 
			"Greg", "Gregory", "Hailey", "Haley", "Hannah", "Heather", "Hector", "Heidi", "Henry", "Holly", "Hunter", "Ian", "Imtiaz", 
			"Isabel", "Isabella", "Isabelle", "Isaiah", "Jack", "Jackie", "Jackson", "Jaclyn", "Jacob", "Jacqueline", "Jada", "Jae", 
			"Jaime", "Jake", "James", "Jamie", "Jan", "Janet", "Jarrod", "Jasmine", "Jason", "Javier", "Jay", "Jean", "Jeffery", "Jenna", 
			"Jennifer", "Jeremiah", "Jeremy", "Jermaine", "Jerome", "Jerry", "Jesse", "Jessica", "Jessie", "Jesus", "Jill", "Jillian", "Jimmy", 
			"Jo", "Joan", "Joanna", "Jocelyn", "Joel", "John", "Johnny", "Jon", "Jonathan", "Jonathon", "Jordan", "Jordyn", "Jorge", "Jose", 
			"Joseph", "Joshua", "Joy", "Juan", "Julia", "Julio", "Justin", "Kaitlin", "Kaitlyn", "Kara", "Karen", "Kari", "Karla", "Kate", 
			"Katelyn", "Katherine", "Kathie", "Kathryn", "Katrina", "Kayla", "Kaylee", "Keith", "Kelli", "Kelly", "Kelsey", "Kelvin", "Ken", 
			"Kendra", "Kenneth", "Kevin", "Kimberly", "Krishna", "Krista", "Kristen", "Kristi", "Kristine", "Krystal", "Kurt", "Kyle", 
			"Lacey", "Lance", "Larry", "Latasha", "Latoya", "Laura", "Lauren", "Leah", "Leonard", "Leslie", "Levi", "Linda", "Lindsay", 
			"Lindsey", "Lisa", "Logan", "Lori", "Louis", "Lucas", "Luis", "Luke", "Lydia", "Maciej", "Mackenzie", "Madison", "Magnus", 
			"Makayla", "Mandy", "Manuel", "Marco", "Marcus", "Margaret", "Maria", "Mariah", "Marie", "Marissa", "Marshall", "Martin", 
			"Marvin", "Mary", "Mason", "Mathew", "Maurice", "Megan", "Meghan", "Melanie", "Melissa", "Meredith", "Michael", "Micheal", 
			"Michele", "Miguel", "Mindy", "Misty", "Mitchell", "Molly", "Monica", "Monique", "Morgan", "Mya", "Nancy", "Naomi", "Natalie", 
			"Natasha", "Nathan", "Neil", "Nichole", "Nicolas", "Nicole", "Nina", "Noah", "Olivia", "Omar", "Oscar", "Paige", "Pamela", 
			"Patricia", "Patrick", "Paula", "Peter", "Phillip", "Phyllis", "Priscilla", "Rachel", "Rafael", "Ramon", "Randy", "Raquel", 
			"Raymond", "Rebecca", "Rebekah", "Reginald", "Renee", "Renee", "Ricardo", "Richard", "Ricky", "Riley", "Robert", "Roberto", 
			"Robin", "Robyn", "Roger", "Rolando", "Rosa", "Ross", "Rossane", "Roy", "Ruben", "Russell", "Ryan", "Sairaj", "Samantha", 
			"Samuel", "Sandra", "Sara", "Sarah", "Scott", "Sebastian", "Sergio", "Seth", "Shane", "Shannon", "Sharon", "Shawn", "Sheila", 
			"Shelby", "Shyamalan", "Sierra", "Stacey", "Stacy", "Stefan", "Stephanie", "Summer", "Suzanne", "Sydney", "Tabitha", "Tamara", 
			"Tammy", "Tara", "Tasha", "Taylor", "Teresa", "Terry", "Theodore", "Thomas", "Tiffany", "Tina", "Todd", "Tommy", "Tonya", "Tracy",
			"Trinity", "Tristan", "Troy", "Tyler", "Valerie", "Vanessa", "Veronica", "Victor", "Victoria", "Vincent", "Virginia", "Walter", 
			"Wesley", "Whitney", "William", "Willie", "Wyatt", "Xavier", "Yolanda", "Zachary", "Zheng"
		};
		static readonly string[] lastNames = new string[] {
			"Adams", "Alexander", "Allen", "Alonso", "Alvarez", "Anand", "Andersen", "Anderson", "Arthur", "Arun", "Bailey", "Baker", 
			"Barnes", "Beck", "Becker", "Bell", "Belli", "Bennett", "Black", "Blackwell", "Blanco", "Blue", "Bradley", "Brooks", "Brown",
			"Bryant", "Butler", "Byrnes", "Cai", "Campbell", "Carlson", "Carothers", "Carter", "Chande", "Chander", "Chandra", "Chapman", 
			"Chen", "Chisholm", "Chow", "Clark", "Coleman", "Collins", "Cook", "Cooper", "Cox", "Creasey", "Davis", "Delmarco", "Deng", 
			"Diaz", "Dominguez", "Dusza", "Edwards", "Evans", "Fernandez", "Ferrier", "Fine", "Flood", "Flores", "Foster", "Gao", "Garcia", 
			"German", "Gill", "Glimp", "Goel", "Goldberg", "Gomez", "Gonzales", "Gonzalez", "Gray", "Green", "Griffin", "Groncki", "Guo", 
			"Gutierrez", "Guzik", "Hagens", "Hall", "Harris", "Haugh", "Hayes", "He", "Hedlund", "Hee", "Henderson", "Hernandez", "Hill", 
			"Hite", "Hohman", "Howard", "Hu", "Huang", "Huff", "Hughes", "Ison", "Jackson", "Jai", "James", "Jenkins", "Jimenez", "Jimenez", 
			"Johnsen", "Johnson", "Johnston", "Jones", "Kaffer", "Kapoor", "Keiser", "Kelly", "Khan", "King", "Kumar", "Kwok", "Lal", 
			"Leavitt", "Lee", "Lewis", "Li", "Liang", "Lin", "Liu", "Logan", "Long", "Lopez", "Lu", "Luo", "Ma", "Madan", "Malhotra", 
			"Margheim", "Markwood", "Martin", "McDonald", "McKinley", "McPhearson", "McSharry Jensen", "Mehta", "Michaels", "Miller", 
			"Mitchell", "Mitzner", "Moore", "Moreno", "Morgan", "Morris", "Mu", "Munoz", "Munoz", "Murphy", "Nara", "Nath", "Navarro", 
			"Nelsen", "Nelson", "Ngoh", "Ortega", "Pak", "Pal", "Parker", "Patel", "Pather", "Patterson", "Perez", "Perry", "Peterson", 
			"Phillips", "Powell", "Prasad", "Preston", "Price", "Rai", "Raje", "Raji", "Raman", "Ramirez", "Ramos", "Rana", "Randall",
			"Reategui Alayo", "Reed", "Richardson", "Richmeier", "Rivera", "Roberts", "Robinson", "Rodriguez", "Rogers", "Romero", "Ross", 
			"Rowe", "Rubio", "Ruiz", "Russell", "Sai", "Salavaria", "Sanchez", "Sanchez", "Sanders", "Sandoval", "Sanz", "Sara", "Schmidt",
			"Scott", "Serrano", "Several", "Severino", "Shan", "Sharma", "She", "Shen", "Simmons", "Smith", "Srini", "Steele", "Stewart", 
			"Stone", "Suarez", "Subram", "Sullivan", "Sun", "Sunkammurali", "Suri", "Tanara", "Tang", "Taylor", "Thomas", "Thompson", 
			"Thoreson", "Torres", "Townsend", "Trenary", "Turner", "Uddin", "Van", "Vance", "Vazquez", "Velez Amezaga", "Vicknair", "Walker",
			"Walton", "Wang", "Ward", "Washington", "Watkins", "Watson", "West", "White", "Whitworth", "Williams", "Wilson", "Winston", "Wood",
			"Wright", "Wu", "Xie", "Xu", "Yang", "Ye", "Young", "Yuan", "Zeng", "Zhang", "Zhao", "Zheng", "Zhou", "Zhu", "Zimmerman"
		};
		public static DatabaseGenerator.TableData Table {
			get { return table; }
		}
		public static void Register() {
			table = new DatabaseGenerator.TableData();
			table.Name = "Persons";
			table.ConnectionStringName = "LargeDatabaseConnectionString";
			table.Fields.Add(new DatabaseGenerator.FieldData("FirstName", firstNames));
			table.Fields.Add(new DatabaseGenerator.FieldData("LastName", lastNames));
			table.Fields.Add(new DatabaseGenerator.FieldData("Phone", GeneratePhone));
			table.RecordCount = 20000;
			DatabaseGenerator.RegisterTable("GeneratedPersonTable", table);
		}
		static object GeneratePhone(Random rnd) {
			StringBuilder builder = new StringBuilder("1 (");
			builder.Append(rnd.Next(523, 529));
			builder.Append(") ");
			builder.Append(rnd.Next(100, 999));
			builder.Append("-");			
			builder.AppendFormat("{0:0000}", rnd.Next(0, 9999));
			return builder.ToString();
		}
	}
}
