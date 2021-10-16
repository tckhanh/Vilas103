(function () {

    // Bo sung New GridView Part

    function EflNameLostFocus(s, e) {
        gridView.SetEditValue("TaxName", gridView.GetEditValue("Name"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }

    function EflAddressLostFocus(s, e) {
        gridView.SetEditValue("TaxAddress", gridView.GetEditValue("Address"));
        ASPxClientEdit.ValidateGroup("TaxGroup")
    }

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            window.location.href = '/Pages/MailMerge/MailMergeViaDocument.aspx?Template=RegisterFormTemplate.docx' + '&ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    window.EflNameLostFocus = EflNameLostFocus;
    window.EflAddressLostFocus = EflAddressLostFocus;
    window.customButtonClick = customButtonClick;
})();