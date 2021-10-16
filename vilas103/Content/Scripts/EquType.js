(function () {
    // Bo sung New GridView Part

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            //window.location.href = '/Pages/MailMerge/MailMergeViaDocument.aspx?Template=RegisterFormTemplate.docx' + '&ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    window.customButtonClick = customButtonClick;
})();