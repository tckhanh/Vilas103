using DevExpress.Web;
using DevExpress.Web.Demos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vilas103.MailMerge
{
    public partial class MailMerge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DemoRichEdit.WorkDirectory = Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "MailMergeResults");
                DemoRichEdit.ViewMergedData = true;
                DemoRichEdit.SettingsDocumentSelector.UploadSettings.Enabled = !Utils.IsSiteMode;

                FileManager.Settings.RootFolder = DirectoryManagmentUtils.DocumentBrowsingFolderPath;
                FileManager.SettingsUpload.Enabled = !Utils.IsSiteMode;
                FileManagerFile file = FileManager.SelectedFolder.GetFiles().FirstOrDefault();
                if (file != null)
                {
                    FileManager.SelectedFile = file;
                    DemoRichEdit.Open(file.FullName);
                }
                else
                {
                    DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"MailMergeTemplate.docx"));
                }
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
            if (FileManager.Settings.RootFolder != DirectoryManagmentUtils.DocumentBrowsingFolderPath)
                DirectoryManagmentUtils.AssertTimeout();
        }

        protected void DemoRichEdit_Callback(object sender, CallbackEventArgsBase e)
        {
            if (FileManager.SelectedFile != null)
                DemoRichEdit.Open(FileManager.SelectedFile.FullName);
        }
    }
}