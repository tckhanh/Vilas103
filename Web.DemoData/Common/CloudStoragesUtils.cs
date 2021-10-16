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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
namespace DevExpress.Web.Demos {
	public static class CloudStoragesUtils {
		const string FileManagerAmazonAccessKeyIDSettingName = "FileManagerAmazonAccessKeyID";
		const string FileManagerAmazonSecretAccessKeySettingName = "FileManagerAmazonSecretAccessKey";
		const string FileManagerAzureAccessKeySettingName = "FileManagerAzureAccessKey";
		const string FileManagerDropboxAccessTokenValueSettingName = "FileManagerDropboxAccessTokenValue";
		const string FileManagerOneDriveClientIDValueSettingName = "FileManagerOneDriveClientIDValue";
		const string FileManagerOneDriveClientSecretValueSettingName = "FileManagerOneDriveClientSecretValue";
		const string FileManagerGoogleDriveClientEmailValueSettingName = "FileManagerGoogleDriveClientEmailValue";
		const string FileManagerGoogleDrivePrivateKeyValueSettingName = "FileManagerGoogleDrivePrivateKeyValue";
		const string UploadDropboxAccessTokenValueSettingName = "UploadDropboxAccessTokenValue";
		const string UploadAzureAccessKeySettingName = "UploadAzureAccessKey";
		const string UploadAmazonAccessKeyIDSettingName = "UploadAmazonAccessKeyID";
		const string UploadAmazonSecretAccessKeySettingName = "UploadAmazonSecretAccessKey";
		const string UploadOneDriveClientIDValueSettingName = "UploadOneDriveClientIDValue";
		const string UploadOneDriveClientSecretValueSettingName = "UploadOneDriveClientSecretValue";
		const string UploadGoogleDriveClientEmailValueSettingName = "UploadGoogleDriveClientEmailValue";
		const string UploadGoogleDrivePrivateKeyValueSettingName = "UploadGoogleDrivePrivateKeyValue";
		public static string GetAzureStorageAccountName() {
			return "aspxdemos";
		}
		public static string GetFileManagerAmazonAccessKeyID() {
			return WebConfigurationManager.AppSettings[FileManagerAmazonAccessKeyIDSettingName];
		}
		public static string GetFileManagerAmazonSecretAccessKey() {
			return WebConfigurationManager.AppSettings[FileManagerAmazonSecretAccessKeySettingName];
		}
		public static string GetFileManagerAzureAccessKey() {
			return WebConfigurationManager.AppSettings[FileManagerAzureAccessKeySettingName];
		}
		public static string GetFileManagerDropboxAccessTokenValue() {
			return WebConfigurationManager.AppSettings[FileManagerDropboxAccessTokenValueSettingName];
		}
		public static string GetFileManagerOneDriveClientIDValue() {
			return WebConfigurationManager.AppSettings[FileManagerOneDriveClientIDValueSettingName];
		}
		public static string GetFileManagerOneDriveClientSecretValue() {
			return WebConfigurationManager.AppSettings[FileManagerOneDriveClientSecretValueSettingName];
		}
		public static string GetFileManagerGoogleDriveClientEmailValue() {
			return WebConfigurationManager.AppSettings[FileManagerGoogleDriveClientEmailValueSettingName];
		}
		public static string GetFileManagerGoogleDrivePrivateKeyValue() {
			return WebConfigurationManager.AppSettings[FileManagerGoogleDrivePrivateKeyValueSettingName];
		}
		public static string GetUploadAmazonAccessKeyID() {
			return WebConfigurationManager.AppSettings[UploadAmazonAccessKeyIDSettingName];
		}
		public static string GetUploadAmazonSecretAccessKey() {
			return WebConfigurationManager.AppSettings[UploadAmazonSecretAccessKeySettingName];
		}
		public static string GetUploadAzureAccessKey() {
			return WebConfigurationManager.AppSettings[UploadAzureAccessKeySettingName];
		}
		public static string GetUploadDropboxAccessTokenValue() {
			return WebConfigurationManager.AppSettings[UploadDropboxAccessTokenValueSettingName];
		}
		public static string GetUploadOneDriveClientIDValue() {
			return WebConfigurationManager.AppSettings[UploadOneDriveClientIDValueSettingName];
		}
		public static string GetUploadOneDriveClientSecretValue() {
			return WebConfigurationManager.AppSettings[UploadOneDriveClientSecretValueSettingName];
		}
		public static string GetUploadGoogleDriveClientEmailValue() {
			return WebConfigurationManager.AppSettings[UploadGoogleDriveClientEmailValueSettingName];
		}
		public static string GetUploadGoogleDrivePrivateKeyValue() {
			return WebConfigurationManager.AppSettings[UploadGoogleDrivePrivateKeyValueSettingName];
		}
	}
}
