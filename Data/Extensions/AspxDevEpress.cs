using Data.DataModels;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class AspxDevEpress
    {
        public static void BuildNavBarItems(this ASPxNavBar navbar, List<MenuInfo> menuitems)
        {
            string navUrl = string.Empty;

            // I use a Dictionary object to keep track of the Menu Groups

            Dictionary<string, NavBarGroup> navGroups =
                new Dictionary<string, NavBarGroup>();

            // Clear the Items to start fresh.
            navbar.Groups.Clear();


            foreach (MenuInfo m in menuitems)
            {
                navUrl = m.MenuUrl;

                // If the Key Exists in the Dictionary add the new Item to the items collection of that NavGroup
                if (navGroups.ContainsKey(m.MenuGroup))
                    navGroups[m.MenuGroup].Items.Add(new NavBarItem(m.MenuText, m.MenuName, "", ""));
                    //navGroups[m.MenuGroup].Items.Add(new NavBarItem(m.MenuText, m.MenuName, navUrl, ""));
                else
                {
                    // Otherwise we add a new navGroup to the Navbar collection, add the navbarItem to it and store it in the dictionary
                    NavBarGroup navgroup = navbar.Groups.Add(m.MenuGroup, m.MenuGroup);
                    navgroup.Items.Add(new NavBarItem(m.MenuText, m.MenuName, "", ""));
                    //navgroup.Items.Add(new NavBarItem(m.MenuText, m.MenuName, navUrl, ""));
                    navGroups.Add(m.MenuGroup, navgroup);
                }
            }
        }
    }
}
