(function () {
    // Bo sung New GridView Part
    function onTokensChanged(s, e) {
        //gridView.PerformCallback(myConstant.Tokens_Callback + s.GetText());
        gridView.GetValuesOnCustomCallback(s.GetText(), onGetValuesOnCustomCallbackComplete);
    }

    function onGetValuesOnCustomCallbackComplete(values) {
        gridView.SetEditValue("Price", values);
    }

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            window.location.href = '/Pages/MailMerge/MailMergeViaDocument.aspx?Template=RegisterFormTemplate.docx' + '&ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    window.customButtonClick = customButtonClick;
    window.onTokensChanged = onTokensChanged;
})();