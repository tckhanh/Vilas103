using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vilas103.Pages
{
    public partial class CreateData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDatabaseProvider.CreateStdType();
            InitDatabaseProvider.CreateStandard();
            InitDatabaseProvider.CreatEquGroup();
            InitDatabaseProvider.CreateStatus();
            InitDatabaseProvider.CreatCompany();
        }
    }
}