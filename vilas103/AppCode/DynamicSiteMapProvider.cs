using System.Collections.Generic;
using System.Web;


public class DynamicSiteMapProvider : XmlSiteMapProvider
{
    private List<SiteMapNode> ProviderAllNode = new List<SiteMapNode>();

    public DynamicSiteMapProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Check Quyền user truy cập từng trang
    /// </summary>
    /// <param name="context"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public override bool IsAccessibleToUser(System.Web.HttpContext context, System.Web.SiteMapNode node)
    {
        if (node == null)
        {
            throw new System.ArgumentNullException("node");
        }

        if (context == null)
        {
            throw new System.ArgumentNullException("context");
        }

        if (!this.SecurityTrimmingEnabled)
        {
            return true;
        }
        //add node vao listNode
        if (!ProviderAllNode.Contains(node))
            ProviderAllNode.Add(node.Clone());

        //check thuong tinh visible
        bool isVisible;
        if (bool.TryParse(node["visible"], out isVisible))
        {
            if (isVisible)
            {
                return false;
            }
        }

        SiteMapNodeCollection lstNode = new SiteMapNodeCollection();

        System.Collections.IList roles = node.Roles;

        System.Security.Principal.IPrincipal user = context.User;

        string strDeny = node["deny"];
        //check deny
        if (strDeny != null)
        {
            string[] lstDeny = strDeny.Split(',');
            for (int i = 0; i < lstDeny.Length; i++)
            {
                if (lstDeny[i].Trim() == "*")
                {
                    return false;
                }
                if (user != null)
                {
                    if (user.IsInRole(lstDeny[i].Trim())) return false;
                }
                else
                    if (lstDeny[i].Trim() == "?") return false;
            }
        }

        if (roles == null || roles.Count == 0)
        {
            return true;
        }
        if (user == null) return false;

        foreach (string role in roles)
        {

            if ((role == "*") || user.IsInRole(role))
            {

                return true;

            }

        }
        return false;
    }

    public override SiteMapNode FindSiteMapNode(HttpContext context)
    {
        SiteMapNode node = base.FindSiteMapNode(context);
        if (node == null)
            node = this.FindNode(context);
        return node;
    }
    public override SiteMapNode FindSiteMapNode(string rawUrl)
    {
        SiteMapNode node = base.FindSiteMapNode(rawUrl);
        if (node == null)
            node = this.FindNode(rawUrl);
        return node;
    }
    /// <summary>
    /// Tim kiem node thoa man
    /// </summary>
    /// <returns></returns>
    public SiteMapNode FindNode(HttpContext context)
    {

        string strUrl = context.Request.RawUrl.ToLower();
        foreach (SiteMapNode node in ProviderAllNode)
        {
            if (node.Url != null)
                if (node.Url.ToLower() == strUrl)
                {
                    return node;
                }
        }
        return null;
    }
    /// <summary>
    /// Tim kiem node thoa man
    /// </summary>
    /// <returns></returns>
    public SiteMapNode FindNode(string rawUrl)
    {
        //if (rawUrl == null) rawUrl = string.Empty;
        //if (rawUrl.IndexOf("?") >= 0)
        //    rawUrl = rawUrl.Substring(0, rawUrl.IndexOf("?"));
        foreach (SiteMapNode node in ProviderAllNode)
        {
            if (node.Url != null)
                if (node.Url.ToLower() == rawUrl.ToLower())
                {
                    return node;
                }
        }
        return null;
    }
}