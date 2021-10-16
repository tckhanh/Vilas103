using DevExpress.Web.Demos;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class RichEditDemoMailMergeViaDocumentServer : OfficeDemoPage {
    const string documentId = "mailMergeDocId";

    protected void Page_Load(object sender, EventArgs e) {
        if(!IsCallback) {
            RichEditDocumentServer documentServer = new RichEditDocumentServer();
            documentServer.LoadDocument(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"MailMergeTemplate.docx"));
            documentServer.Options.MailMerge.DataSource = GetData();

            using(MemoryStream stream = new MemoryStream()) {
                documentServer.MailMerge(stream, DocumentFormat.OpenXml);
                stream.Position = 0;
                DocumentManager.CloseDocument(documentId);
                DemoRichEdit.Open(documentId, DocumentFormat.OpenXml, () => {
                    return stream;
                });
            }
        }
    }
    IEnumerable<Employee> GetData() {
        return new NorthwindContext().Employees.Where(e => e.City == (string)cmbCity.Value).ToList();
    }
    protected void Page_Init(object sender, EventArgs e) {
        DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
    }
}
