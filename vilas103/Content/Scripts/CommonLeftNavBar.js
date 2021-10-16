(function () {


    function onFiltersNavBarItemClick(s, e) {
        var StrUserName = '';
        if (myIsAuthenticated)
            StrUserName = myCurentUser.UserName;

        var filters = {
            All: "",
            InActivedUnBlocked: "[Actived] = false and [Blocked] = false",
            ActivedBlocked: "[Actived] = true and [Blocked] = true",
            InactivedBlocked: "[Actived] = false and [Blocked] = true",
            ActivedUnBlocked: "[Actived] = true and [Blocked] = false",
            UpdatedBy: "[UpdatedBy] = '" + StrUserName + "' Or " + "[CreatedBy] = '" + StrUserName + "'",
            Owner: "[UserName] = '" + StrUserName + "'",
        };
        gridView.ApplyFilter(filters[e.item.name]);

        HideLeftPanelIfRequired();
        e.processOnServer = false;
        e.usePostBack = false;
    }

    // Bo sung New GridView Part
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;

})();