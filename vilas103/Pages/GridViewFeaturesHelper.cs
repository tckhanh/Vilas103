using DevExpress.Utils;
using DevExpress.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace vilas103
{
    internal class GridViewFeaturesHelper
    {
        public static void SetupGlobalGridViewBehavior(ASPxGridView gridView)
        {
            if (gridView == null)
                return;
            gridView.EnablePagingGestures = AutoBoolean.False;
            gridView.SettingsPager.EnableAdaptivity = true;
            gridView.Styles.Header.Wrap = DefaultBoolean.True;
            if (InjectGridNoWrapGroupPanelCssStyle(gridView.Page))
                gridView.Styles.GroupPanel.CssClass = "GridNoWrapGroupPanel";
        }
        static bool InjectGridNoWrapGroupPanelCssStyle(Page page)
        {
            HtmlHead header = (page != null && page.Header != null) ? page.Header : DevExpress.Web.Internal.RenderUtils.FindHead(page);
            if (header != null)
            {
                header.Controls.Add(new LiteralControl()
                {
                    Text = "\r\n<style>.GridNoWrapGroupPanel td.dx-wrap { white-space: nowrap !important; }</style>\r\n"
                });
                return true;
            }
            return false;
        }
    }
}