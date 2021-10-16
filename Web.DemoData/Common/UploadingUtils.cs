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
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
namespace DevExpress.Web.Demos {
	public static class UploadingUtils {
		const string RemoveTaskKeyPrefix = "DXRemoveTask_";
		public static string GetAmazonAccessKeyID() {
			return CloudStoragesUtils.GetUploadAmazonAccessKeyID();
		}
		public static string GetAmazonSecretAccessKey() {
			return CloudStoragesUtils.GetUploadAmazonSecretAccessKey();
		}
		public static string GetAzureStorageAccountName() {
			return CloudStoragesUtils.GetAzureStorageAccountName();
		}
		public static string GetAzureAccessKey() {
			return CloudStoragesUtils.GetUploadAzureAccessKey();
		}
		public static string GetDropboxAccessTokenValue() {
			return CloudStoragesUtils.GetUploadDropboxAccessTokenValue();
		}
		public static string GetOneDriveClientIDValue() {
			return CloudStoragesUtils.GetUploadOneDriveClientIDValue();
		}
		public static string GetOneDriveClientSecretValue() {
			return CloudStoragesUtils.GetUploadOneDriveClientSecretValue();
		}
		public static string GetGoogleDriveClientEmailValue() {
			return CloudStoragesUtils.GetUploadGoogleDriveClientEmailValue();
		}
		public static string GetGoogleDrivePrivateKeyValue() {
			return CloudStoragesUtils.GetUploadGoogleDrivePrivateKeyValue();
		}
		public static void RemoveFileWithDelay(string key, string fullPath, int delay) {
			RemoveFileWithDelayInternal(key, fullPath, delay, FileSystemRemoveAction);
		}
		public static void RemoveFileFromAzureWithDelay(string fileKeyName, string accountName, string containerName, int delay) {
			AzureFileInfo azureFile = new AzureFileInfo(fileKeyName, accountName, containerName);
			RemoveFileWithDelayInternal(fileKeyName, azureFile, delay, AzureStorageRemoveAction);
		}
		public static void RemoveFileFromAmazonWithDelay(string fileKeyName, string accountName, string bucketName, string region, int delay) {
			AmazonFileInfo amazonFile = new AmazonFileInfo(fileKeyName, accountName, bucketName, region);
			RemoveFileWithDelayInternal(fileKeyName, amazonFile, delay, AmazonStorageRemoveAction);
		}
		public static void RemoveFileFromDropboxWithDelay(string fileKeyName, string accountName, int delay) {
			DropboxFileInfo dropboxFile = new DropboxFileInfo(fileKeyName, accountName);
			RemoveFileWithDelayInternal(fileKeyName, dropboxFile, delay, DropboxStorageRemoveAction);
		}
		public static void RemoveFileFromOneDriveWithDelay(string fileKeyName, string accountName, string tokenEndpoint, string redirectUri, int delay) {
			OneDriveFileInfo oneDriveFile = new OneDriveFileInfo(fileKeyName, accountName, tokenEndpoint, redirectUri);
			RemoveFileWithDelayInternal(fileKeyName, oneDriveFile, delay, OneDriveStorageRemoveAction);
		}
		public static void RemoveFileFromSharePointWithDelay(string fileKeyName, string accountName, string tokenEndpoint, string redirectUri, string siteName, string siteHostName, int delay) {
			var fileInfo = new SharePointFileInfo(fileKeyName, accountName, tokenEndpoint, redirectUri, siteName, siteHostName);
			RemoveFileWithDelayInternal(fileKeyName, fileInfo, delay, SharePointStorageRemoveAction);
		}
		public static void RemoveFileFromGoogleDriveWithDelay(string fileKeyName, string accountName, int delay) {
			GoogleDriveFileInfo oneDriveFile = new GoogleDriveFileInfo(fileKeyName, accountName);
			RemoveFileWithDelayInternal(fileKeyName, oneDriveFile, delay, GoogleDriveStorageRemoveAction);
		}
		static void FileSystemRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			string fileFullPath = value.ToString();
			if(File.Exists(fileFullPath))
				File.Delete(fileFullPath);
		}
		static void AzureStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			AzureFileInfo azureFile = value as AzureFileInfo;
			if(azureFile != null) {
				AzureFileSystemProvider provider = new AzureFileSystemProvider("");
				provider.AccountName = azureFile.AccountName;
				provider.ContainerName = azureFile.ContainerName;
				FileManagerFile file = new FileManagerFile(provider, azureFile.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void AmazonStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			AmazonFileInfo amazonFile = value as AmazonFileInfo;
			if(amazonFile != null) {
				AmazonFileSystemProvider provider = new AmazonFileSystemProvider("");
				provider.AccountName = amazonFile.AccountName;
				provider.BucketName = amazonFile.BucketName;
				provider.Region = amazonFile.Region;
				FileManagerFile file = new FileManagerFile(provider, amazonFile.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void DropboxStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			DropboxFileInfo dropboxFile = value as DropboxFileInfo;
			if(dropboxFile != null) {
				DropboxFileSystemProvider provider = new DropboxFileSystemProvider("");
				provider.AccountName = dropboxFile.AccountName;
				FileManagerFile file = new FileManagerFile(provider, dropboxFile.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void OneDriveStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			OneDriveFileInfo oneDriveFile = value as OneDriveFileInfo;
			if(oneDriveFile != null) {
				OneDriveFileSystemProvider provider = new OneDriveFileSystemProvider("UploadedFiles");
				provider.AccountName = oneDriveFile.AccountName;
				provider.TokenEndpoint = oneDriveFile.TokenEndpoint;
				provider.RedirectUri = oneDriveFile.RedirectUri;
				FileManagerFile file = new FileManagerFile(provider, oneDriveFile.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void SharePointStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			var fileInfo = value as SharePointFileInfo;
			if(fileInfo != null) {
				var provider = new SharePointFileSystemProvider("UploadedFiles");
				provider.AccountName = fileInfo.AccountName;
				provider.TokenEndpoint = fileInfo.TokenEndpoint;
				provider.RedirectUri = fileInfo.RedirectUri;
				provider.SiteName = fileInfo.SiteName;
				provider.SiteHostName = fileInfo.SiteHostName;
				var file = new FileManagerFile(provider, fileInfo.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void GoogleDriveStorageRemoveAction(string key, object value, CacheItemRemovedReason reason) {
			GoogleDriveFileInfo googleDriveFile = value as GoogleDriveFileInfo;
			if(googleDriveFile != null) {
				GoogleDriveFileSystemProvider provider = new GoogleDriveFileSystemProvider("UploadedFiles");
				provider.AccountName = googleDriveFile.AccountName;
				FileManagerFile file = new FileManagerFile(provider, googleDriveFile.FileKeyName);
				provider.DeleteFile(file);
			}
		}
		static void RemoveFileWithDelayInternal(string fileKey, object fileData, int delay, CacheItemRemovedCallback removeAction) {
			string key = RemoveTaskKeyPrefix + fileKey;
			if(HttpRuntime.Cache[key] == null) {
				DateTime absoluteExpiration = DateTime.UtcNow.Add(new TimeSpan(0, delay, 0));
				HttpRuntime.Cache.Insert(key, fileData, null, absoluteExpiration,
					Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, removeAction);
			}
		}
		class AzureFileInfo {
			public string FileKeyName { get; set; }
			public string AccountName { get; set; }
			public string ContainerName { get; set; }
			public AzureFileInfo(string fileKeyName, string accountName, string containerName) {
				FileKeyName = fileKeyName;
				AccountName = accountName;
				ContainerName = containerName;
			}
		}
		class AmazonFileInfo {
			public string FileKeyName { get; set; }
			public string AccountName { get; set; }
			public string BucketName { get; set; }
			public string Region { get; set; }
			public AmazonFileInfo(string fileKeyName, string accountName, string bucketName, string region) {
				FileKeyName = fileKeyName;
				AccountName = accountName;
				BucketName = bucketName;
				Region = region;
			}
		}
		class DropboxFileInfo {
			public string FileKeyName { get; set; }
			public string AccountName { get; set; }
			public DropboxFileInfo(string fileKeyName, string accountName) {
				FileKeyName = fileKeyName;
				AccountName = accountName;
			}
		}
		class OneDriveFileInfo {
			public string FileKeyName { get; set; }
			public string AccountName { get; set; }
			public string TokenEndpoint { get; set; }
			public string RedirectUri { get; set; }
			public OneDriveFileInfo(string fileKeyName, string accountName, string tokenEndpoint, string redirectUri) {
				FileKeyName = fileKeyName;
				AccountName = accountName;
				TokenEndpoint = tokenEndpoint;
				RedirectUri = redirectUri;
			}
		}
		class SharePointFileInfo : OneDriveFileInfo {
			public string SiteName { get; set; }
			public string SiteHostName { get; set; }
			public SharePointFileInfo(string fileKeyName, string accountName, string tokenEndpoint, string redirectUri, string siteName, string siteHostName)
				: base(fileKeyName, accountName, tokenEndpoint, redirectUri) {
				SiteName = siteName;
				SiteHostName = siteHostName;
			}
		}
		class GoogleDriveFileInfo {
			public string FileKeyName { get; set; }
			public string AccountName { get; set; }
			public GoogleDriveFileInfo(string fileKeyName, string accountName) {
				FileKeyName = fileKeyName;
				AccountName = accountName;
			}
		}
	}
}
