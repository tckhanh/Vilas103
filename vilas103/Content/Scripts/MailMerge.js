(function () {
    var postponedCallbackRequired = false;
    function OnSelectedFileChanged(s, e) {
        if (e.file) {
            if (!DemoRichEdit.InCallback())
                DemoRichEdit.PerformCallback();
            else
                postponedCallbackRequired = true;
        }
    }

    function tree_OnSelectedFileChanged() {
        if (!DemoRichEdit.InCallback())
            DemoRichEdit.PerformCallback();
        else
            postponedCallbackRequired = true;
    }

    function DemoRichEditEndCallback(s, e) {
        if (postponedCallbackRequired) {
            DemoRichEdit.PerformCallback();
            postponedCallbackRequired = false;
        }
    }
    function OnExceptionOccurred(s, e) {
        e.handled = true;
        alert(e.message);
        window.location.reload();
    }

    window.OnSelectedFileChanged = OnSelectedFileChanged;
    window.DemoRichEditEndCallback = DemoRichEditEndCallback;
    window.OnExceptionOccurred = OnExceptionOccurred;
})();