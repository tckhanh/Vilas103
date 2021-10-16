using DevExpress.Web.Demos;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class RichEditDemoDocVariable : OfficeDemoPage {
    protected void Page_Load(object sender, EventArgs e) {
        if(!IsPostBack)
            DemoRichEdit.Open(Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, @"NORTHWIND.docx"));
    }
    protected void DemoRichEdit_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
        switch(e.VariableName) {
            case "Chart":
                var sales = GetSales(e.Arguments[0].Value);
                DocumentImageSource chart = DocumentImageSource.FromStream(CreateChart(sales));
                RichEditDocumentServer srv = new RichEditDocumentServer();
                srv.Document.Images.Append(chart);
                e.Value = srv.Document;
                e.Handled = true;
                break;
            case "CommonSales":
                var commonSales = GetCommonSales(e.Arguments[0].Value);
                e.Value = commonSales.ToString("C");
                e.Handled = true;
                break;
            default:
                break;
        }
    }

    IEnumerable<Sales_by_Category> GetSales(string categoryName) {
        return new NorthwindContext().Sales_by_Categories.Where(s => s.CategoryName == categoryName).ToList();
    }

    decimal GetCommonSales(string categoryName) {
        return GetSales(categoryName).Sum(s => s.ProductSales.Value);
    }

    Stream CreateChart(IEnumerable<Sales_by_Category> sales) {
        var cc = new WebChartControl();
        cc.Width = Unit.Pixel(600);
        cc.Height = Unit.Pixel(400);
        cc.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        var series = new DevExpress.XtraCharts.Series("Products", DevExpress.XtraCharts.ViewType.Bar);
        series.DataSource = sales;
        series.ArgumentDataMember = "ProductName";
        series.ValueScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        series.ValueDataMembers.AddRange(new string[] { "ProductSales" });
        cc.Series.Add(series);
        Controls.Add(cc);
        cc.DataBind();
        MemoryStream stream = new MemoryStream();
        cc.ExportToImage(stream, System.Drawing.Imaging.ImageFormat.Png);
        stream.Position = 0;
        return stream;
    }
    protected void Page_Init(object sender, EventArgs e) {
        RichEditDemoUtils.HideFileTab(DemoRichEdit);
        DemoHelper.Instance.ControlAreaMaxWidth = Unit.Percentage(100);
    }
    protected void DemoRichEdit_PreRender(object sender, EventArgs e) {
        DemoRichEdit.Focus();
    }
}
