using System;
using System.IO;
using DevExpress.Web.Demos;
using System.Web.UI.WebControls;

public partial class RichEditDemoFields : OfficeDemoPage {
    protected void Page_Load(object sender, EventArgs e) {
        if(!IsPostBack)
            DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"Fields.docx"));
    }
    protected void DemoRichEdit_PreRender(object sender, EventArgs e) {
        DemoRichEdit.Focus();
    }
    protected void Page_Init(object sender, EventArgs e) {
        RichEditDemoUtils.HideFileTab(DemoRichEdit);
        DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
    }
}
