using AutoMapper;
using Data.DataVMs;
using Data.Providers;
using DevExpress.Web.Demos;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace vilas103.MailMerge
{
    public partial class MailMergeViaDocument : System.Web.UI.Page
    {
        const string documentId = "mailMergeDocId";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && Request["Template"] != null && Request["ID"] != null)
                {
                var TemplateDoc = Request["Template"].ToString();
                var ID = Request["ID"].ToString();

                RichEditDocumentServer documentServer = new RichEditDocumentServer();
                documentServer.LoadDocument(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, TemplateDoc));
                IEnumerable<CompanyVM> items = GetData(ID);
                string FileName = "RegisterForm_";
                FileName += items.FirstOrDefault()?.Id;
                FileName += @".docx";
                documentServer.Options.MailMerge.DataSource = items;

                using (MemoryStream stream = new MemoryStream())
                {
                    documentServer.MailMerge(stream, DocumentFormat.OpenXml);
                    stream.Position = 0;
                    DocumentManager.CloseDocument(documentId);
                    SaveStreamToFile(stream, Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, FileName));
                    stream.Position = 0;
                    SendFiletoClientBrowser(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, FileName));

                    DemoRichEdit.Open(documentId, DocumentFormat.OpenXml, () =>
                    {
                        return stream;
                    });
                }
            }
        }

        public void SaveStreamToFile(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        public void SendFiletoClientBrowser(string FilePath)
        {
            // retrieve the physical path of the file to download, and create
            // a FileInfo object to read its properties
            //String FilePath = Server.MapPath(virtualFilePath);
            FileInfo TargetFile = new FileInfo(FilePath);
            // clear the current output content from the buffer
            Response.Clear();
            // add the header that specifies the default filename for the Download/
            // SaveAs dialog
            Response.AddHeader("Content-Disposition", "attachment; filename=" + TargetFile.Name);
            // add the header that specifies the file size, so that the browser
            // can show the download progress
            Response.AddHeader("Content-Length", TargetFile.Length.ToString());
            // specify that the response is a stream that cannot be read by the
            // client and must be downloaded
            Response.ContentType = "application/octet-stream";
            // send the file stream to the client
            Response.WriteFile(TargetFile.FullName);
            // stop the execution of this page
            Response.End();
        }

        IEnumerable<CompanyVM> GetData(string ID)
        {
            CompanyVM item = Mapper.Map<CompanyVM>(CompanyProvider.GetSingleById(ID));
            List<CompanyVM> ItemList = new List<CompanyVM>();
            if (item != null)
            {
                ItemList.Add(item);
            }
            return ItemList;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
        }
    }
}