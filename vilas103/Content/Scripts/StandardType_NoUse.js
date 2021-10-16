(function () {

    // Bo sung New GridView Part


    var rowKeyValueToCopy = 0;
    var copyFlag = false;
    var isGridviewRefresh = false;

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
            Owner: "[UserName] = '" + StrUserName + "'",
        };
        var str = filters[e.item.name];
        gridView.ApplyFilter(filters[e.item.name]);

        HideLeftPanelIfRequired();
    }

    function OnContextMenu(s, e) {
        if (e.objectType == "header") {
            var menuItemDataChooser = e.menu.GetItemByName("DataChooser");
            //menuItemDataChooser.SetEnabled(myIsAuthenticated);
        }
        if (e.objectType == "row" || e.objectType == "emptyrow") {
            var menuItemNewRow = e.menu.GetItemByName("NewRow");
            var menuItemCloneRow = e.menu.GetItemByName("CloneRow");
            var menuItemEditRow = e.menu.GetItemByName("EditRow");
            var menuItemEditRow = e.menu.GetItemByName("DetailRow");
            var menuItemDeleteRow = e.menu.GetItemByName("DeleteRow");
            var menuItemExportMenu = e.menu.GetItemByName("ExportMenu");
            var menuItemActived = e.menu.GetItemByName("Actived");
            var menuItemInActived = e.menu.GetItemByName("InActived");
            var menuItemBlocked = e.menu.GetItemByName("Blocked");
            var menuItemUnBlocked = e.menu.GetItemByName("UnBlocked");
            var menuItemInActivedBlocked = e.menu.GetItemByName("InActivedBlocked");
            var menuItemActivedUnBlocked = e.menu.GetItemByName("ActivedUnBlocked");
            var isRowSelected = s.IsRowSelectedOnPage(e.index);
            var FocusedRowIndex = gridView.GetFocusedRowIndex();
            if (myIsAuthenticated == true) {
                menuItemNewRow.SetEnabled(true);
                if (FocusedRowIndex >= 0) {
                    var isRowcpActived = s.cpActived[FocusedRowIndex];
                    var isRowcpBlocked = s.cpBlocked[FocusedRowIndex];

                    menuItemCloneRow.SetEnabled(true);
                    menuItemEditRow.SetEnabled(true);
                    menuItemDeleteRow.SetEnabled(true);
                    menuItemExportMenu.SetEnabled(true);
                    menuItemActived.SetVisible(!isRowcpActived && !isRowcpBlocked);
                    menuItemInActived.SetVisible(isRowcpActived);
                    menuItemBlocked.SetVisible(isRowcpActived && !isRowcpBlocked);
                    menuItemUnBlocked.SetVisible(isRowcpActived && isRowcpBlocked);
                    menuItemInActivedBlocked.SetVisible((!isRowcpActived && !isRowcpBlocked) || (isRowcpActived && isRowcpBlocked));
                    menuItemActivedUnBlocked.SetVisible(!isRowcpActived && isRowcpBlocked);
                } else {
                    menuItemCloneRow.SetEnabled(false);
                    menuItemEditRow.SetEnabled(false);
                    menuItemDeleteRow.SetEnabled(false);
                    menuItemExportMenu.SetEnabled(false);
                    menuItemActived.SetVisible(false);
                    menuItemInActived.SetVisible(false);
                    menuItemBlocked.SetVisible(false);
                    menuItemUnBlocked.SetVisible(false);
                    menuItemInActivedBlocked.SetVisible(false);
                    menuItemActivedUnBlocked.SetVisible(false);
                }
            } else {
                menuItemCloneRow.SetEnabled(false);
                menuItemEditRow.SetEnabled(false);
                menuItemDeleteRow.SetEnabled(false);
                menuItemExportMenu.SetEnabled(false);
                menuItemActived.SetVisible(false);
                menuItemInActived.SetVisible(false);
                menuItemBlocked.SetVisible(false);
                menuItemUnBlocked.SetVisible(false);
                menuItemInActivedBlocked.SetVisible(false);
                menuItemActivedUnBlocked.SetVisible(false);
            }
        }

        //https://supportcenter.devexpress.com/ticket/details/t120218/aspxgridview-change-visibility-or-disable-context-menu-items-based-on-selected-row-data#
        //https://supportcenter.devexpress.com/ticket/details/e4279/how-to-enable-context-menu-items-depending-on-selected-aspxgridview-rows#

        //if (e.objectType == "row") {
        //    var menuItemSelected = e.menu.GetItemByName("OnlySelectedRows");
        //    var menuItemSelectedAndDiscontinued = e.menu.GetItemByName("OnlySelectedAndDiscontinuedRows");
        //    var isRowSelected = s.IsRowSelectedOnPage(e.index);
        //    var isRowDiscontinued = s.cpDiscontinued[e.index];
        //    menuItemSelected.SetVisible(isRowSelected);
        //    menuItemSelectedAndDiscontinued.SetEnabled(isRowSelected && isRowDiscontinued);
        //}
    }

    function beginCallback(s, e) {
        if (e.command === "CUSTOMCALLBACK") {
            // can be used to perform specific client-side actions (for example, to display and hide an explanatory text or picture) while a callback is being processed on the server side.
        }
    }

    function endCallback(s, e) {
        switch (e.command) {
            case 'CUSTOMCALLBACK':
                switch (s.cpParameters) {
                    case "CloneRow":
                        gridView.AddNewRow();
                        //GridView.JSProperties["cpMessage"] = string.Format("OrderDate {0} is later than {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString());
                        break;
                    case "DetailRow":
                        gridView.StartEditRow(gridView.GetFocusedRowIndex());
                        break;
                    //case "Actived":
                    //    gridView.Refresh();
                    //    break;
                    //case "InActived":
                    //    gridView.Refresh();
                    //    break;
                    //case "Blocked":
                    //    gridView.Refresh();
                    //    break;
                    //case "UnBlocked":
                    //    gridView.Refresh();
                    //    break;
                    //case "InActivedBlocked":
                    //    gridView.Refresh();
                    //    break;
                    //case "ActivedUnBlocked":
                    //    gridView.Refresh();
                    //    break;
                    //case "DeleteRow":
                    //    gridView.Refresh();
                    //    break;
                }
                break;
            //case 'REFRESH':
            //    updateNavBarName();
            //    e.handled = true;
            //    break;
            //case 'UPDATEEDIT':
            //    gridView.Refresh();
            //    break;
            case 'STARTEDIT':
                break;
            case 'CANCELEDIT':
                break;
            case 'DELETEROW':
                break;
            case 'ADDNEWROW':
                break;
        }
        //if (e.command === "CUSTOMCALLBACK") {
        //    gridView.Refresh();
        //    // can be used to perform specific client-side actions (for example, to display and hide an explanatory text or picture) while a callback is being processed on the server side.
        //}
        updateNavBarName();
    }

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            window.location.href = '/Pages/MailMerge/MailMergeViaDocument.aspx?ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    function batchEditStartEditing(s, e) {
        if (copyFlag) {
            copyFlag = false;

            for (var i = 0; i < s.GetColumnsCount(); i++) {
                var column = s.GetColumn(i);
                if (column.visible == false || column.fieldName == undefined || column.fieldName === "") //add additional condition  
                    continue;
                ProcessCells(0, e, column, s);
            }
        }
    }

    function ProcessCells(selectedIndex, e, column, s) {
        var isCellEditMode = selectedIndex == 0;
        var cellValue = s.batchEditApi.GetCellValue(rowKeyValueToCopy, column.fieldName);

        if (isCellEditMode) {
            if (column.fieldName == e.focusedColumn.fieldName)
                e.rowValues[column.index].value = cellValue;
            else
                s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, cellValue);
        } else {
            e.rowValues[column.index].value = cellValue;
        }
    }

    function EflCompanyNameLostFocus(s, e) {
        gridView.SetEditValue("TaxCompanyName", gridView.GetEditValue("CompanyName"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }

    function EflAddressLostFocus(s, e) {
        gridView.SetEditValue("TaxAddress", gridView.GetEditValue("Address"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }

    // ********************

    function onGridViewInit(s, e) {
        //ASPxClientUtils.SetCookie("DXCurrentLanguage", "vi");
        AddAdjustmentDelegate(adjustGridView);
        updateToolbarButtonsStateByFocused();
        updateNavBarName();
    }
    function onGridViewFocusedRowChanged(s, e) {
        updateToolbarButtonsStateByFocused();
    }

    function onGridViewSelectionChanged(s, e) {
        //updateToolbarButtonsStateBySelection();
    }

    function adjustGridView() {
        gridView.AdjustControl();
    }

    //function updateToolbarButtonsStateBySelection() {
    //    var enabled = gridView.GetSelectedRowCount() > 0;
    //    pageToolbar.GetItemByName("DeleteRow").SetEnabled(enabled);
    //    pageToolbar.GetItemByName("Export").SetEnabled(enabled);
    //    pageToolbar.GetItemByName("EditRow").SetEnabled(enabled);
    //    pageToolbar.GetItemByName("DetailRow").SetEnabled(enabled);
    //    pageToolbar.GetItemByName("CloneRow").SetEnabled(enabled);
    //}

    function updateNavBarName() {
        var NavBarItem, NavBarItemTile ;
        if (gridView.cpAllCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("All");
            NavBarItemTile = NavBarItem.GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpAllCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpAllCount + ")");
                //NavBarItem.addClass("bold-navbar");
                //e.element.find(".dx-datagrid-addrow-button").addClass("bold-navbar"); 
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }

        if (gridView.cpInActivedUnBlockedCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("InActivedUnBlocked");
            NavBarItemTile = filtersNavBar.GetItemByName("InActivedUnBlocked").GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpInActivedUnBlockedCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpInActivedUnBlockedCount + ")");
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }
        if (gridView.cpActivedBlockedCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("ActivedBlocked");
            NavBarItemTile = filtersNavBar.GetItemByName("ActivedBlocked").GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpActivedBlockedCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpActivedBlockedCount + ")");
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }
        if (gridView.cpActivedBlockedCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("InactivedBlocked");
            NavBarItemTile = filtersNavBar.GetItemByName("InactivedBlocked").GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpInactivedBlockedCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpInactivedBlockedCount + ")");
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }
        if (gridView.cpActivedUnBlockedCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("ActivedUnBlocked");
            NavBarItemTile = filtersNavBar.GetItemByName("ActivedUnBlocked").GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpActivedUnBlockedCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpActivedUnBlockedCount + ")");
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }
        if (gridView.cpOwnerCount != undefined) {
            NavBarItem = filtersNavBar.GetItemByName("Owner");
            NavBarItemTile = filtersNavBar.GetItemByName("Owner").GetText();
            NavBarItemTile = NavBarItemTile.split('(')[0];
            if (gridView.cpOwnerCount > 0) {
                NavBarItem.SetText(NavBarItemTile + " (" + gridView.cpOwnerCount + ")");
            }
            else {
                NavBarItem.SetText(NavBarItemTile);
            }
        }
    }

    function updateToolbarButtonsStateByFocused() {
        if (myIsAuthenticated) {
            var enabled = gridView.GetFocusedRowIndex() >= 0
            pageToolbar.GetItemByName("DeleteRow").SetEnabled(enabled);
            pageToolbar.GetItemByName("Export").SetEnabled(enabled);
            pageToolbar.GetItemByName("EditRow").SetEnabled(enabled);
            pageToolbar.GetItemByName("DetailRow").SetEnabled(enabled);
            pageToolbar.GetItemByName("CloneRow").SetEnabled(enabled);
        }
        else {
            pageToolbar.GetItemByName("DeleteRow").SetEnabled(false);
            pageToolbar.GetItemByName("Export").SetEnabled(false);
            pageToolbar.GetItemByName("EditRow").SetEnabled(false);
            pageToolbar.GetItemByName("DetailRow").SetEnabled(false);
            pageToolbar.GetItemByName("CloneRow").SetEnabled(false);
        }
    }
    function OnContextMenuItemClick(s, e) {
        switch (e.item.name) {
            case "CloneRow":
                cloneFlag = true;
                gridView.PerformCallback('CloneRow');
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "DetailRow":
                gridView.PerformCallback('DetailRow');
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case 'Actived':
                gridView.PerformCallback('Actived');
                //var visibleIndex = gridView.GetFocusedRowIndex();
                //gridView.GetRowKey(visibleIndex)
                //gridView.SetCellValue(visibleIndex, 'Actived', true);
                //gridView.UpdateEdit();
                //e.processOnServer = false;
                //e.usePostBack = false;
                break;
            case 'InActived':
                gridView.PerformCallback('InActived');
                break;
            case 'Blocked':
                gridView.PerformCallback('Blocked');
                break;
            case 'UnBlocked':
                gridView.PerformCallback('UnBlocked');
                break;
            case 'InActivedBlocked':
                gridView.PerformCallback('InActivedBlocked');
                break;
            case 'ActivedUnBlocked':
                gridView.PerformCallback('ActivedUnBlocked');
                break;
        }
    }

    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            case "ToggleFilterPanel":
                toggleFilterPanel();
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "NewRow":
                gridView.AddNewRow();
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "CloneRow":
                cloneFlag = true;
                gridView.PerformCallback('CloneRow');
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "EditRow":
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "DetailRow":
                gridView.PerformCallback('DetailRow');
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "DeleteRow":
                if (confirm('Bạn có chắc chắn muốn xoá không ?')) {
                    gridView.DeleteRow(gridView.GetFocusedRowIndex());
                }
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "Export":
                gridView.ExportTo(ASPxClientGridViewExportFormat.Xlsx);
                e.processOnServer = false;
                e.usePostBack = false;
                break;
        }
    }

    function deleteSelectedRecords() {
        if (confirm('Confirm Delete?')) {
            gridView.PerformCallback('Delete');
        }
    }


    function toggleFilterPanel() {
        filterPanel.Toggle();
    }

    function onFilterPanelExpanded(s, e) {
        adjustPageControls();
        searchButtonEdit.SetFocus();
    }

    // Bo sung New GridView Part
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;
    window.OnContextMenu = OnContextMenu;
    window.OnContextMenuItemClick = OnContextMenuItemClick;
    window.beginCallback = beginCallback;
    window.endCallback = endCallback;
    window.customButtonClick = customButtonClick;
    window.batchEditStartEditing = batchEditStartEditing;
    window.EflCompanyNameLostFocus = EflCompanyNameLostFocus;
    window.EflAddressLostFocus = EflAddressLostFocus;
    // ********************
    window.onGridViewInit = onGridViewInit;
    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
    window.onGridViewFocusedRowChanged = onGridViewFocusedRowChanged
    window.onPageToolbarItemClick = onPageToolbarItemClick;
    window.onFilterPanelExpanded = onFilterPanelExpanded;
})();