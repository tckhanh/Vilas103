using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;

public static class FileManagerHelper {
	const string Ident = "ASPxTreeListFileManagerDemo";
	const string KeyMapID = Ident + "KM";
	const string CleanerTimestampID = Ident + "GC";
	const string UploadedNameID = Ident + "uname";
	const string UploadedTmpNameID = Ident + "utmp";

	public const string NameFieldName = "Name";
	public const string FullPathName = "_path";

	static readonly long CleanerInterval = TimeSpan.FromMinutes(2).Ticks;
	static readonly long UserDataTTL = TimeSpan.FromMinutes(10).Ticks;

	static HttpSessionState Session { get { return HttpContext.Current.Session; } }
	static Dictionary<string, Guid> PathKeyMap {
		get {
			Dictionary<string, Guid> map = Session[KeyMapID] as Dictionary<string, Guid>;
			if(map == null) {
				map = new Dictionary<string, Guid>();
				Session[KeyMapID] = map;
			}
			return map;
		}
	}
	static long CleanerLastRunTime {
		get {
			if(Session[CleanerTimestampID] is long)
				return (long)Session[CleanerTimestampID];
			return 0;
		}
		set {
			Session[CleanerTimestampID] = value;
		}
	}

	public static string RootFolder {
		get {
			string name = StorageFolder + Path.DirectorySeparatorChar + Session.SessionID;
			if(!Directory.Exists(name)) {
				Directory.CreateDirectory(name);
				Directory.CreateDirectory(name + "/My Pictures");
				Directory.CreateDirectory(name + "/My Music");
				Directory.CreateDirectory(name + "/Sandbox");				
			}
			Directory.SetCreationTime(name, DateTime.Now);
			PerformCleanup();
			return name;
		}
	}
	static string StorageFolder {
		get { return Path.GetTempPath() + Path.DirectorySeparatorChar + Ident; }
	}

    public static string CurrentDataFolder
    {
        get { return Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, ""); }
    }

	public static Guid GetPathKey(string name) {
		name = Path.GetFullPath(name);
		if(PathKeyMap.ContainsKey(name))
			return PathKeyMap[name];
		Guid guid = Guid.NewGuid();
		PathKeyMap.Add(name, guid);
		return guid;
	}
	public static void MovePath(string oldName, string newName) {
		oldName = Path.GetFullPath(oldName);
		newName = Path.GetFullPath(newName);
		if(oldName == newName) return;
		if(Directory.Exists(oldName))
			Directory.Move(oldName, newName);
		else if(File.Exists(oldName))
			File.Move(oldName, newName);
		else
			return;
		// Rename session keys
		Dictionary<string, Guid> itemsToRename = new Dictionary<string,Guid>();
		foreach(string name in PathKeyMap.Keys) {
			if(name.StartsWith(oldName))
				itemsToRename.Add(name, PathKeyMap[name]);
		}
		foreach(string name in itemsToRename.Keys)
			PathKeyMap.Remove(name);
		foreach(string name in itemsToRename.Keys) {
			PathKeyMap.Add(newName + name.Substring(oldName.Length), itemsToRename[name]);
		}
	}
	public static void BeginUploadFile(string name, Stream stream) {
		string tmpName = Path.GetTempFileName();
		Session[UploadedNameID] = Path.GetFileName(name);
		Session[UploadedTmpNameID] = tmpName;
		using(Stream myFile = File.Create(tmpName)) {
			// save contents
		}		
	}
	public static string EndUploadFile(string destination) {
		string fullName = null;
		if(Session[UploadedNameID] != null && Session[UploadedTmpNameID] != null) {
			fullName = destination + Path.DirectorySeparatorChar + Session[UploadedNameID].ToString();
			string tmpName = Session[UploadedTmpNameID].ToString();
			try {
                if(!File.Exists(fullName))
				    File.Move(tmpName, fullName);
			} catch {
				File.Delete(tmpName);
				throw;
			}
		}		
		Session[UploadedNameID] = null;
		Session[UploadedTmpNameID] = null;
		return fullName;
	}

	static void PerformCleanup() {
		long now = DateTime.Now.Ticks;
		if(now - CleanerLastRunTime < CleanerInterval)
			return;
		PerformCleanupCore();
		CleanerLastRunTime = now;
	}
	static void PerformCleanupCore() {
		long now = DateTime.Now.Ticks;
		string[] names = Directory.GetDirectories(StorageFolder);
		foreach(string name in names) {
			if(now - Directory.GetCreationTime(name).Ticks < UserDataTTL)
				continue;
			try {
				Directory.Delete(name, true);
			} catch { }
		}
	}
}
