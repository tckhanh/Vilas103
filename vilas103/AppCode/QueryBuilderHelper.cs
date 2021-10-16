using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Internal;
using DevExpress.Web.Demos;
using System.Configuration;
using System.Web.SessionState;

public static class QueryBuilderHelper {

    static readonly string ShowTooltipKey = "ShowTooltip";
    static readonly string SelectQueryKey = "SelectQuery";
    static readonly string SelectCommandKey = "SelectCommand";
    static readonly string DefaultSelectCommand =
@"select [Suppliers].[CompanyName],[Suppliers].[ContactName],[Suppliers].[City],
		[Suppliers].[Country],[Products].[ProductName],[Products].[UnitPrice]
from [dbo].[Suppliers] [Suppliers]
inner join [dbo].[Products] [Products] on [Products].[SupplierID] = [Suppliers].[SupplierID]";

    static SelectQuery GenerateDefaultQuery() {
        return SelectQueryFluentBuilder
            .AddTable("Suppliers")
            .SelectColumns("CompanyName", "ContactName", "City", "Country")
            .Join("Products", DevExpress.Xpo.DB.JoinType.LeftOuter, "SupplierID", "SupplierID")
            .SelectColumns("ProductName", "UnitPrice")
            .Build("Query1");
    }
    
    public static string NorthwindConnectionString {
        get {
            string sqlExpressString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            return Utils.PatchConnectionStrings ? DbEngineDetector.PatchConnectionString(sqlExpressString) : sqlExpressString;
        }
    }

    public static CustomStringConnectionParameters NorthwindConnectionParameters {
        get {
            return new CustomStringConnectionParameters(NorthwindConnectionString + ";XpoProvider=MSSqlServer");
        }
    }

    public static SelectQuery LoadQuery(HttpSessionState session) {
        return (session[SelectQueryKey] as SelectQuery) ?? GenerateDefaultQuery();
    }

    public static string LoadSelectCommand(HttpSessionState session) {
        return (session[SelectCommandKey] as string) ?? DefaultSelectCommand;
    }

    public static void SaveQuery(string selectCommand, SelectQuery query, HttpSessionState session) {
        session[SelectQueryKey] = query;
        session[SelectCommandKey] = selectCommand;
    }

    public static void HideTooltip(HttpSessionState session) {
        session[ShowTooltipKey] = false;
    }
    public static bool NeedToShowTooltip(HttpSessionState session) {
        var showTooltipValue = session[ShowTooltipKey];
        return showTooltipValue is bool ? (bool)showTooltipValue : true;
    }
}
