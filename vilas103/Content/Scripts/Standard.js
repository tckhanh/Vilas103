(function () {
    // Bo sung New GridView Part
    function onUpdateBtnClick(s, e) {
        if (uc.GetText() !== '' || !uc.GetFileNames()) {
            uc.Upload();
        } else {
            if (gridView.GetEditValue("FileName") != "") {
                gridView.UpdateEdit();
            } else {
                //alert('Please select a file!');
                gridView.UpdateEdit();
            }
        }
    }

    function onCancelBtnClick(s, e) {
        gridView.CancelEdit();
    }

    function onTextChanged(s, e) {
        if (uc.GetText() !== '' || !uc.GetFileNames()) {
            var filename = uc.GetFileNames()[0];
            gridView.SetEditValue("FileName", filename);
        } else {
            gridView.SetEditValue("FileName", "");
        }
    }

    function onFileInputCountChanged(s, e) {
        if (uc.GetText() !== '' || !uc.GetFileNames()) {
            var filename = uc.GetFileNames()[0];
            gridView.SetEditValue("FileName", filename);
        }
    }

    function onFileUploadComplete(s, e) {
        gridView.UpdateEdit();
        //gridView.SetEditValue("FileName", gridView.GetEditValue("CompanyName"));
        //var filename = gridView.GetEditValue("FileName");
        //gridView.SetEditValue("URL", e.CallbackData);
    }

    function customButtonClick(s, e) {
        if (e.buttonID == "Download" && e.visibleIndex >= 0) {
            //window.location.href = '/Pages/MailMerge/MailMergeViaDocument.aspx?Template=RegisterFormTemplate.docx' + '&ID=' + gridView.GetRowKey(e.visibleIndex);
        }
    }

    window.customButtonClick = customButtonClick;
    window.onUpdateBtnClick = onUpdateBtnClick;
    window.onCancelBtnClick = onCancelBtnClick;
    window.onFileUploadComplete = onFileUploadComplete;
    window.onTextChanged = onTextChanged;
    window.onFileInputCountChanged = onFileInputCountChanged;
})();