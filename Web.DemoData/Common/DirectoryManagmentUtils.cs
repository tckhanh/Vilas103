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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
public static class DirectoryManagmentUtils {
	public const int DisposeTimeout = 5;
	const string FolderKey = "WorkSessionDirectory";
	const string DemoPathKey = "CurrentDemoPath";
	static readonly object modifyUserDirectoriesLocker = new object();
	public class DemoDirectoryInfo {
		public string Name { get; set; }
		public DateTime LastUsageTime { get; set; }
	}
	public static string DocumentBrowsingFolderPath { get { return Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "DocumentBrowsing"); } }
	public static string SampleDocumentsFolderPath { get { return Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "SampleDocuments"); } }
	public static string CurrentDataDirectory {
		get {
			if(!Utils.IsSiteMode)
				return InitialDemoFilesPath;
			lock(modifyUserDirectoriesLocker) {
				var currentDataDirectory = (string)Context.Session[FolderKey];
				DemoDirectoryInfo directoryInfo = ActualDemoDirectories.Where(i => i.Name == currentDataDirectory).SingleOrDefault();
				if(directoryInfo == null || ((string)Context.Session[DemoPathKey] != Context.Request.Path && Context.Request.HttpMethod == "GET")) {
					Context.Session[DemoPathKey] = Context.Request.Path;
					Context.Session[FolderKey] = currentDataDirectory = CreateNewDemoFolder();
					directoryInfo = new DemoDirectoryInfo { Name = currentDataDirectory, LastUsageTime = DateTime.Now };
					ActualDemoDirectories.Add(directoryInfo);
				} else {
					directoryInfo.LastUsageTime = DateTime.Now;
				}
				return currentDataDirectory;
			}
		}
	}
	static IList<DemoDirectoryInfo> actualDemoDirectories;
	static IList<DemoDirectoryInfo> ActualDemoDirectories {
		get {
			if(actualDemoDirectories == null)
				actualDemoDirectories = new List<DemoDirectoryInfo>();
			return actualDemoDirectories;
		}
	}
	static HttpContext Context { get { return HttpContext.Current; } }
	static string RootDemoFilesPath { get { return Context.Request.MapPath("~/App_Data/"); } }
	static string InitialDemoFilesPath { get { return Path.Combine(RootDemoFilesPath, "Documents"); } }
	static string CreateNewDemoFolder() {
		string demoFilesDirectoty = GenerateDemoFilesFolderName();
		CopyDemoFiles(InitialDemoFilesPath, demoFilesDirectoty);
		return demoFilesDirectoty;
	}
	static void CopyDemoFiles(string sourceFilePath, string destinationPath) {
		IEnumerable<string> documentFileCollection = GetFilesInDirectory(sourceFilePath, "*.xlsx", "*.xls", "*.csv", "*.docx", "*.doc", "*.rtf", "*.txt");
		if(!Directory.Exists(destinationPath))
			Directory.CreateDirectory(destinationPath);
		foreach(var filePath in documentFileCollection) {
			string destinationFile = Path.Combine(destinationPath, Path.GetFileName(filePath));
			File.Copy(filePath, destinationFile, true);
			File.SetAttributes(destinationFile, FileAttributes.Normal);
		}
		foreach(string directoryPath in Directory.GetDirectories(sourceFilePath)) {
			string directoryName = Path.GetFileName(directoryPath);
			CopyDemoFiles(directoryPath, Path.Combine(destinationPath, directoryName));
		}
	}
	static IEnumerable<string> GetFilesInDirectory(string path, params string[] allowedExtensions) {
		IEnumerable<string> documentFileCollection = new string[0];
		foreach(string extension in allowedExtensions) {
			documentFileCollection = documentFileCollection.Concat(Directory.GetFiles(path, extension));
		}
		return documentFileCollection;
	}
	static string GenerateDemoFilesFolderName() {
		string currentFolder = null;
		while(string.IsNullOrEmpty(currentFolder) || Directory.Exists(Path.Combine(RootDemoFilesPath, currentFolder))) {
			currentFolder = Guid.NewGuid().ToString();
		}
		return Path.Combine(RootDemoFilesPath, currentFolder);
	}
	public static void PurgeOldUserDirectories() {
		if(!Utils.IsSiteMode)
			return;
		lock(modifyUserDirectoriesLocker) {
			string[] existingDirectories = Directory.GetDirectories(RootDemoFilesPath);
			foreach(string directoryPath in existingDirectories) {
				Guid guid = Guid.Empty;
				if(!Guid.TryParse(Path.GetFileName(directoryPath), out guid)) continue;
				DemoDirectoryInfo directoryInfo = ActualDemoDirectories.Where(i => i.Name == directoryPath).SingleOrDefault();
				if(directoryInfo == null || (DateTime.Now - directoryInfo.LastUsageTime).TotalMinutes > DisposeTimeout) {
					Directory.Delete(directoryPath, true);
					if(directoryInfo != null)
						ActualDemoDirectories.Remove(directoryInfo);
				}
			}
		}
	}
	public static string GetDeletedByTimeoutMessage() {
		return "The current work session has been expired. The page will be automatically re-requested.";
	}
	public static void AssertTimeout() {
		if(!Utils.IsSiteMode)
			return;
		throw new DemoException(GetDeletedByTimeoutMessage());
	}
}
public class OfficeDemoPage : System.Web.UI.Page {
	protected override void OnPreLoad(EventArgs e) {
		base.OnPreLoad(e);
		DirectoryManagmentUtils.PurgeOldUserDirectories();
	}
}
