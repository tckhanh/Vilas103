(function () {
    // Bo sung New GridView Part
    function onSelectIndexChangedEquTypeId(s, e) {
        //gridView.PerformCallback(myConstant.Tokens_Callback + s.GetText());
        //gridView.GetValuesOnCustomCallback(s.GetText(), onGetValuesOnCustomCallbackComplete);
        //AspxClientCombobox 
        //var values = s.GetSelectedItem().GetColumnText("Standards");
        
        gridView.SetEditValue("EquName", s.GetSelectedItem().GetColumnText("DisplayName"));
        gridView.SetEditValue("Standards", s.GetSelectedItem().GetColumnText("Standards"));
        gridView.SetEditValue("Price", s.GetSelectedItem().GetColumnText("Price"));
    }

    function onSelectedIndexChangedCompanyId(s, e) {
        gridView.SetEditValue("ContractNo", s.GetSelectedItem().GetColumnText("ContractNo"));
    }

    function onTokensChangedStandardIds(s, e) {
        gridView.GetValuesOnCustomCallback(s.GetText(), onGetValuesOnCustomCallbackComplete);
    }

    function onGetValuesOnCustomCallbackComplete(values) {
        gridView.SetEditValue("Price", values);
    }

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            window.location.href = '/Pages/MailMerge/MailMergeRequest.aspx?Template=RequestFormTemplate.docx' + '&ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    window.onSelectIndexChangedEquTypeId = onSelectIndexChangedEquTypeId;
    window.onSelectedIndexChangedCompanyId = onSelectedIndexChangedCompanyId;
    window.onTokensChangedStandardIds = onTokensChangedStandardIds;
    window.customButtonClick = customButtonClick;
})();