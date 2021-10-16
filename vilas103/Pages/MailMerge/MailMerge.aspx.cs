using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using DevExpress.Web.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace vilas103.MailMerge
{
    public partial class MailMerge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DemoRichEdit.WorkDirectory = Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "");
                DemoRichEdit.ViewMergedData = true;
                DemoRichEdit.SettingsDocumentSelector.UploadSettings.Enabled = !Utils.IsSiteMode;

                //FileManager.Settings.RootFolder = DirectoryManagmentUtils.DocumentBrowsingFolderPath;
                //FileManager.SettingsUpload.Enabled = !Utils.IsSiteMode;
                //FileManagerFile file = FileManager.SelectedFolder.GetFiles().FirstOrDefault();
                //if (file != null)
                //{
                //    FileManager.SelectedFile = file;
                //    DemoRichEdit.Open(file.FullName);
                //}
                //else
                //{
                DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"MailMergeTemplate.docx"));
                //}
            }
        }
        protected void DemoRichEdit_PreRender(object sender, EventArgs e)
        {
            DemoRichEdit.Focus();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
        }

        protected void FileManager_FileUploading(object source, DevExpress.Web.FileManagerFileUploadEventArgs e)
        {
            e.Cancel = Utils.IsSiteMode;
            e.ErrorText = Utils.GetReadOnlyMessageText();
        }

        protected void FileManager_Load(object sender, EventArgs e)
        {
            //if (FileManager.Settings.RootFolder != DirectoryManagmentUtils.DocumentBrowsingFolderPath)
            //    DirectoryManagmentUtils.AssertTimeout();
        }

        protected void DemoRichEdit_Callback(object sender, CallbackEventArgsBase e)
        {
            //if (FileManager.SelectedFile != null)
            //    DemoRichEdit.Open(FileManager.SelectedFile.FullName);

            object keyValue = tree.FocusedNode[tree.KeyFieldName];
            TreeListNode selectedNode = tree.FocusedNode;
            string FileName = selectedNode.GetValue("_path").ToString();
            if (FileName != null)
                DemoRichEdit.Open(FileName);
        }

        protected void tree_VirtualModeCreateChildren(object sender, TreeListVirtualModeCreateChildrenEventArgs e)
        {
            //string nodePath = e.NodeObject == null ? FileManagerHelper.RootFolder : e.NodeObject.ToString();
            string nodePath = e.NodeObject == null ? FileManagerHelper.CurrentDataFolder : e.NodeObject.ToString();
            if (Directory.Exists(nodePath))
            {
                List<string> children = new List<string>();
                string[] names;
                names = Directory.GetDirectories(nodePath);
                foreach (string name in names)
                    children.Add(name);
                names = Directory.GetFiles(nodePath);
                foreach (string name in names)
                    children.Add(name);
                e.Children = children;
            }
        }
        protected void tree_VirtualModeNodeCreating(object sender, TreeListVirtualModeNodeCreatingEventArgs e)
        {
            string nodePath = e.NodeObject.ToString();
            e.NodeKeyValue = FileManagerHelper.GetPathKey(nodePath);
            e.IsLeaf = !Directory.Exists(nodePath)
                || (Directory.GetFiles(nodePath).Length < 1 && Directory.GetDirectories(nodePath).Length < 1);
            e.SetNodeValue(FileManagerHelper.FullPathName, nodePath);
            e.SetNodeValue(FileManagerHelper.NameFieldName, Path.GetFileName(nodePath));
        }


        protected void tree_NodeInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            TreeListNode parentNode = tree.FindNodeByKeyValue(tree.NewNodeParentKey);
            EnsureNode(parentNode);
            string parentFolder;
            if (parentNode == tree.RootNode)
                //parentFolder = FileManagerHelper.RootFolder;
                parentFolder = FileManagerHelper.CurrentDataFolder;
            else
                parentFolder = parentNode.GetValue(FileManagerHelper.FullPathName).ToString();
            string name = parentFolder + Path.DirectorySeparatorChar + ReadName(e.NewValues);
            if (Directory.Exists(name))
                throw new DemoException("Directory exists.");
            Directory.CreateDirectory(name);
            tree.RefreshVirtualTree(parentNode);
            FocusByPath(name);
        }

        protected void tree_NodeUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            string oldName = ReadName(e.OldValues);
            string newName = ReadName(e.NewValues);
            if (oldName == newName) return;
            string key = e.Keys[0].ToString();
            TreeListNode node = tree.FindNodeByKeyValue(key);
            EnsureNode(node);
            string oldPath = node.GetValue(FileManagerHelper.FullPathName).ToString();
            string newPath = Path.GetDirectoryName(oldPath) + Path.DirectorySeparatorChar + newName;
            FileManagerHelper.MovePath(oldPath, newPath);
        }

        protected void tree_NodeDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            string key = e.Keys[0].ToString();
            TreeListNode node = tree.FindNodeByKeyValue(key);
            EnsureNode(node);
            string name = node.GetValue(FileManagerHelper.FullPathName).ToString();
            if (Directory.Exists(name))
                Directory.Delete(name);
            else if (File.Exists(name))
                File.Delete(name);
            tree.RefreshVirtualTree(node.ParentNode);
        }

        protected void tree_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
        {
            string oldPath = e.Node.GetValue(FileManagerHelper.FullPathName).ToString();
            //string destination = e.NewParentNode == tree.RootNode
            //   ? FileManagerHelper.RootFolder
            //   : e.NewParentNode.GetValue(FileManagerHelper.FullPathName).ToString();
            string destination = e.NewParentNode == tree.RootNode
                ? FileManagerHelper.CurrentDataFolder
                : e.NewParentNode.GetValue(FileManagerHelper.FullPathName).ToString();
            string newPath = destination + Path.DirectorySeparatorChar + Path.GetFileName(oldPath);
            FileManagerHelper.MovePath(oldPath, newPath);
            tree.RefreshVirtualTree();
            e.Handled = true;
        }

        protected void tree_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            object obj = e.NewValues[FileManagerHelper.NameFieldName];
            if (obj == null || !IsValidName(obj.ToString()))
                e.NodeError = "Invalid name.";
        }

        protected void tree_CustomNodeSort(object sender, TreeListCustomNodeSortEventArgs e)
        {
            object value1 = e.Node1.GetValue(FileManagerHelper.FullPathName);
            object value2 = e.Node2.GetValue(FileManagerHelper.FullPathName);
            if (value1 == null || value2 == null) return;
            bool isFolder1 = Directory.Exists(value1.ToString());
            bool isFolder2 = Directory.Exists(value2.ToString());
            if (isFolder1 != isFolder2)
            {
                e.Handled = true;
                e.Result = isFolder2 ? 1 : -1;
            }
        }

        protected void tree_CustomJSProperties(object sender, TreeListCustomJSPropertiesEventArgs e)
        {
            List<string> keys = new List<string>();
            foreach (TreeListNode node in tree.GetVisibleNodes())
            {
                string pathName = node.GetValue(FileManagerHelper.FullPathName).ToString();
                if (Directory.Exists(pathName))
                    keys.Add(node.Key);
            }
            e.Properties["cpFolderKeys"] = keys;
        }

        protected void uploader_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var upload = sender as ASPxUploadControl;
            using (Stream fileContent = upload.UploadedFiles[0].FileContent)
            {
                FileManagerHelper.BeginUploadFile(upload.UploadedFiles[0].FileName, fileContent);
            }
        }
        protected void tree_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {
            if (e.Argument == "upload_complete")
            {
                TreeListNode node = tree.FocusedNode;
                //string folderName = node == null ? FileManagerHelper.RootFolder : node.GetValue(FileManagerHelper.FullPathName).ToString();
                string folderName = node == null ? FileManagerHelper.CurrentDataFolder : node.GetValue(FileManagerHelper.FullPathName).ToString();
                string uploadedName = FileManagerHelper.EndUploadFile(folderName);
                if (!string.IsNullOrEmpty(uploadedName))
                {
                    if (node != null)
                        node.Expanded = true;
                    tree.RefreshVirtualTree();
                    FocusByPath(uploadedName);
                }
            }
            if (e.Argument == "select_File")
            {
                object keyValue = tree.FocusedNode[tree.KeyFieldName];
                TreeListNode selectedNode = tree.FocusedNode;
                string FileName = selectedNode.GetValue("_path").ToString();
                DemoRichEdit.Open(FileName);
            }
        }

        protected string GetNodeGlyph(TreeListDataCellTemplateContainer container)
        {
            string fmt = "~/Content/Images/{0}.png";
            if (container.NodeKey == null)
                return string.Format(fmt, "closed");
            string name = container.GetValue(FileManagerHelper.FullPathName).ToString();
            if (Directory.Exists(name))
            {
                if (container.Expandable && container.Expanded)
                    return string.Format(fmt, "opened");
                return string.Format(fmt, "closed");
            }
            return string.Format(fmt, "file");
        }
        void EnsureNode(TreeListNode node)
        {
            if (node == null)
                throw new DemoException("Node not found.");
        }
        string ReadName(IDictionary values)
        {
            object obj = values[FileManagerHelper.NameFieldName];
            if (obj == null) return String.Empty;
            return obj.ToString().Trim();
        }
        bool IsValidName(string name)
        {
            name = name.Trim();
            return name.Length > 0 && !name.StartsWith(".") && !name.Contains("/") && !name.Contains("\\");
        }
        void FocusByPath(string name)
        {
            string key = FileManagerHelper.GetPathKey(name).ToString();
            TreeListNode node = tree.FindNodeByKeyValue(key);
            if (node == null) return;
            node.Focus();
        }

        protected void tree_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}