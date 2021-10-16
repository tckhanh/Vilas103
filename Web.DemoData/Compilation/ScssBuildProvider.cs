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
using System.Diagnostics;
using System.IO;
using System.Web.Compilation;
namespace DevExpress.Web.Demos {
	public class ScssBuildProvider : BuildProvider {
		public override void GenerateCode(AssemblyBuilder assemblyBuilder) {
			string compilerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\Tools\\ScssCompiler.exe");
			string sourceFilePath = GetSourceFilePath();
			if(File.Exists(compilerPath) && sourceFilePath.IndexOf("node_modules", StringComparison.InvariantCultureIgnoreCase) == -1) {
				Process process = new Process() {
					StartInfo = new ProcessStartInfo(compilerPath, sourceFilePath) {
						WindowStyle = ProcessWindowStyle.Hidden,
						UseShellExecute = false,
						RedirectStandardOutput = true
					}
				};
				process.Start();
				process.WaitForExit();
				if(process.ExitCode != 0) {
					string output = process.StandardOutput.ReadToEnd();
					throw new Exception(string.Format("Scss compiler exception: {0}", output));
				}
			}
		}
		string GetSourceFilePath() {
			using(FileStream fstream = (FileStream)OpenStream()) {
				return fstream.Name;
			}
		}
	}
}
