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

using DevExpress.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
namespace DevExpress.Web.Demos {
	public static class DatabaseGenerator {
		public class TableData {
			public string ConnectionStringName;
			public string Name;
			public List<FieldData> Fields = new List<FieldData>();
			public int RecordCount;
			public string ConnectionString {
				get {
					string sqlExpressString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
					return Utils.PatchConnectionStrings ? DbEngineDetector.PatchConnectionString(sqlExpressString) : sqlExpressString;
				}
			}
		}
		public class FieldData {
			static Random rnd = new Random();
			public delegate object ValueGeneratorDelegate(Random rnd);
			protected FieldData(string name) {
				Name = name;
			}
			public FieldData(string name, IList possibleValues) :
				this(name) {
				PossibleValues = possibleValues;
			}
			public FieldData(string name, ValueGeneratorDelegate valueGenerator) :
				this(name) {
				ValueGenerator = valueGenerator;
			}
			public string Name;
			public IList PossibleValues;
			public ValueGeneratorDelegate ValueGenerator;
			public object GenerateValue() {
				if(PossibleValues != null)
					return PossibleValues[rnd.Next(PossibleValues.Count - 1)];
				if(ValueGenerator != null)
					return ValueGenerator(rnd);
				return null;
			}
		}
		static readonly Dictionary<string, object> _lockers = new Dictionary<string, object>();
		static readonly object _lockersLock = new object();
		static Dictionary<string, int> _recordCountTable = new Dictionary<string, int>();
		static Dictionary<string, TableData> _tables = new Dictionary<string, TableData>();
		public static void RegisterTable(string key, TableData table) {
			_tables[key] = table;
		}
		public static TableData GetTable(string key) {
			return _tables[key];
		}
		public static bool IsReady(string tableKey) {
			return !IsDatabaseCreating(tableKey) && IsDatabaseCreated(tableKey);
		}
		public static bool TryCreateDatabase(string tableKey) {
			if(IsDatabaseCreating(tableKey))
				return false;
			if(!_lockers.ContainsKey(tableKey)) {
				lock(_lockersLock) {
					_lockers[tableKey] = new object();
				}
			}
			lock(_lockers[tableKey]) {
				if(IsDatabaseCreated(tableKey))
					return true;
				_recordCountTable.Add(tableKey, 0);
				try {
					GenerateDatabase(tableKey);
				} finally {
					_recordCountTable.Remove(tableKey);
				}
				return false;
			}
		}
		public static int GetCreatingDatabaseRecordCount(string tableKey) {
			if(_recordCountTable.ContainsKey(tableKey))
				return _recordCountTable[tableKey];
			return GetRecordCount(tableKey);
		}
		static bool IsDatabaseCreated(string tableKey) {
			return GetRecordCount(tableKey) > 0;
		}
		static bool IsDatabaseCreating(string tableKey) {
			return _recordCountTable.ContainsKey(tableKey);
		}
		static void GenerateDatabase(string tableKey) {
			TableData table = _tables[tableKey];
			using(SqlConnection connection = new SqlConnection(table.ConnectionString)) {
				connection.Open();
				SqlTransaction transaction = null;
				for(int i = 0; i < table.RecordCount; i++) {
					if(i == 1 || i % 1000 == 0) {
						CommitTransaction(transaction);
						_recordCountTable[tableKey] = i;
						transaction = connection.BeginTransaction();
					}
					using(SqlCommand cmd = CreateInsertCommand(table.Name, table.Fields)) {
						cmd.Connection = connection;
						cmd.Transaction = transaction;
						cmd.ExecuteNonQuery();
					}
				}
				CommitTransaction(transaction);
			}
		}
		static void CommitTransaction(SqlTransaction transaction) {
			if(transaction == null)
				return;
			try {
				transaction.Commit();
			} catch {
				transaction.Rollback();
				throw;
			}
		}
		static SqlCommand CreateInsertCommand(string tableName, List<FieldData> fields) {
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("insert into [{0}] (", tableName);
			for(int i = 0; i < fields.Count; i++) {
				if(i > 0)
					builder.Append(", ");
				builder.AppendFormat("[{0}]", fields[i].Name);
			}
			builder.Append(") values (");
			for(int i = 0; i < fields.Count; i++) {
				if(i > 0)
					builder.Append(", ");
				builder.Append("@p" + i);
			}
			builder.Append(")");
			SqlCommand result = new SqlCommand(builder.ToString());
			for(int i = 0; i < fields.Count; i++)
				result.Parameters.Add(new SqlParameter("@p" + i, fields[i].GenerateValue()));
			return result;
		}
		static int GetRecordCount(string tableKey) {
			try {
				TableData table = _tables[tableKey];
				using(SqlConnection connection = new SqlConnection(table.ConnectionString)) {
					connection.Open();
					SqlCommand cmd = new SqlCommand("select count(*) from " + table.Name, connection);
					return (int)cmd.ExecuteScalar();
				}
			} catch {
				return 0;
			}
		}
	}
}
