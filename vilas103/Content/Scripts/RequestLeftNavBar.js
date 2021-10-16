(function () {


    function onFiltersNavBarItemClick(s, e) {
        var StrUserName = '';
        if (myIsAuthenticated)
            StrUserName = myCurentUser.UserName;

        gridView.ApplyFilter("[StatusId] = '" + e.item.name + "'");

        //var strFilterExpression = e.item.GetImageUrl();
        //var strFilterExpression = e.item.GetNavigateUrl();
        //e.item.SetNavigateUrl("");
        //strFilterExpression = strFilterExpression.split('/');
        //if (strFilterExpression.length > 1) {
        //    strFilterExpression = strFilterExpression[strFilterExpression.length - 1];
        //}
        //else {
        //    strFilterExpression = "";
        //}
        //gridView.ApplyFilter(decodeURI(strFilterExpression));

        HideLeftPanelIfRequired();
        e.processOnServer = false;
        e.usePostBack = false;
    }

    // Bo sung New GridView Part
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;

})();