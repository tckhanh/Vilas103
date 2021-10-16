(function () {
    // New GridView Part

    var rowKeyValueToCopy = 0;
    var copyFlag = false;

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >=0 )
        {
            window.location.href = '/MailMerge/MailMergeViaDocument.aspx?ID=' + gridView.GetRowKey(e.visibleIndex);
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

    function beginCallback(s, e) {
        if (e.command === "CUSTOMCALLBACK") {
            // can be used to perform specific client-side actions (for example, to display and hide an explanatory text or picture) while a callback is being processed on the server side.
        }
    }

    // Modify GridView Part

    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            case "ToggleFilterPanel":
                toggleFilterPanel();
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "New":
                gridView.AddNewRow();
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "Clone":
                rowKeyValueToCopy = gridView.GetFocusedRowIndex();
                var tt = gridView.GetRowKey(rowKeyValueToCopy);
                //gridView.PerformCallback(gridView.GetRowKey(rowKeyValueToCopy));
                gridView.PerformCallback('Clone');
                copyFlag = true;
                gridView.AddNewRow();
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "Edit":
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                e.processOnServer = false;
                e.usePostBack = false;
                break;
            case "Delete":
                if (confirm('Bạn có chắc chắn muốn xoá không ?')) {
                    gridView.PerformCallback('delete');
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

    // Common GridView Part

    function onGridViewInit(s, e) {
        AddAdjustmentDelegate(adjustGridView);
        updateToolbarButtonsState();
    }
    function onGridViewSelectionChanged(s, e) {
        updateToolbarButtonsState();
    }
    function adjustGridView() {
        gridView.AdjustControl();
    }
    function updateToolbarButtonsState() {
        var enabled = gridView.GetSelectedRowCount() > 0;
        pageToolbar.GetItemByName("Delete").SetEnabled(enabled);
        pageToolbar.GetItemByName("Export").SetEnabled(enabled);
        pageToolbar.GetItemByName("Clone").SetEnabled(enabled);
        pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
    }



    function deleteSelectedRecords() {
        if (confirm('Bạn có chắc chắn muốn xoá không ?')) {
            gridView.PerformCallback('delete');
        }
    }
    function onFiltersNavBarItemClick(s, e) {
        var filters = {
            All: "",
            Filter1: "[Actived] = false",
            Filter2: "[Blocked] = true",
            Filter3: "[Actived] = false and [Blocked] = true",
            Filter4: "[FailLoginTimes] >= 1"
        };
        var str = filters[e.item.name];
        gridView.ApplyFilter(filters[e.item.name]);
        HideLeftPanelIfRequired();
    }

    function toggleFilterPanel() {
        filterPanel.Toggle();
    }

    function onFilterPanelExpanded(s, e) {
        adjustPageControls();
        searchButtonEdit.SetFocus();
    }

    function EflCompanyNameLostFocus(s, e){
        gridView.SetEditValue("TaxCompanyName", gridView.GetEditValue("CompanyName"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }

    function EflAddressLostFocus(s, e) {
        gridView.SetEditValue("TaxAddress", gridView.GetEditValue("Address"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }
    

    window.onGridViewInit = onGridViewInit;
    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
    window.onPageToolbarItemClick = onPageToolbarItemClick;
    window.onFilterPanelExpanded = onFilterPanelExpanded;
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;
    window.beginCallback = beginCallback;
    window.batchEditStartEditing = batchEditStartEditing;
    window.customButtonClick = customButtonClick;
    window.EflCompanyNameLostFocus = EflCompanyNameLostFocus;
    window.EflAddressLostFocus = EflAddressLostFocus;
})();