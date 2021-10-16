using DevExpress.Web.Demos;
using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class RichEditDemoBuiltInMailMerge : OfficeDemoPage {
    protected void Page_Load(object sender, EventArgs e) {
        if(!IsPostBack) {
            DemoRichEdit.WorkDirectory = Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "MailMergeResults");
            DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"MailMergeTemplate.docx"));
            DemoRichEdit.ViewMergedData = true;
        }
    }
    protected void DemoRichEdit_PreRender(object sender, EventArgs e) {
        DemoRichEdit.Focus();
    }
    protected void Page_Init(object sender, EventArgs e) {
        DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
    }
}
