(function () {
    var isFolder = true;
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
    // TreeList handlers
    function tree_Init(s, e) {
        FM_UpdateToolbar();
    }
    function tree_FocusChanged(s, e) {
        FM_UpdateToolbar();
        if (!isFolder) {
            tree_OnSelectedFileChanged();
        }
    }
    function tree_BeginCallback(s, e) {
        FM_UpdateToolbar(true);
    }
    function tree_EndCallback(s, e) {
        FM_UpdateToolbar();
    }
    function tree_NodeClick(s, e) {
        var key = e.nodeKey;
        if (key == tree.GetFocusedNodeKey() && !tree.IsEditing())
            tree.StartEdit(key);
    }
    function tree_StartDragNode(s, e) {
        var rootTarget = document.getElementById("rootTarget");
        e.targets.push(rootTarget);
    }
    function tree_EndDragNode(s, e) {
        if (e.targetElement.id == "rootTarget") {
            e.cancel = true;
            tree.MoveNode(e.nodeKey, "");
        }
    }
    // Editor handlers
    function editor_Init(s, e) {
        s.SelectAll();
        s.SetFocus();
    }
    function editor_KeyPress(s, e) {
        var code = e.htmlEvent.keyCode;
        if (code == 13)
            tree.UpdateEdit();
        else if (code == 27)
            tree.CancelEdit();
        if (code == 13 || code == 27) {
            var he = e.htmlEvent;
            he.preventDefault && he.preventDefault();
            he.stopPropagation && he.stopPropagation();
            he.returnValue = false;
            he.cancelBubble = true;
        }
    }
    function editor_LostFocus(s, e) {
        tree.UpdateEdit();
    }
    // Uploader handlers
    function uploader_Complete(s, e) {
        tree.PerformCustomCallback("upload_complete");
    }
    // Helper functions
    function FM_UpdateToolbar(disableAll) {
        var isEditing = tree.IsEditing();
        var focusedKey = tree.GetFocusedNodeKey();
        isFolder = FM_IsFolderNode(focusedKey);
        UpdateToolbarItemEnabled("New", !disableAll && !isEditing && isFolder);
        UpdateToolbarItemEnabled("NewRoot", !disableAll && !isEditing);
        UpdateToolbarItemEnabled("Delete", !disableAll && focusedKey != "" && !isEditing);
        UpdateToolbarItemEnabled("Edit", !disableAll && focusedKey != "" && !isEditing);
        uploader.SetEnabled(!disableAll && !isEditing && isFolder);
    }
    function UpdateToolbarItemEnabled(name, enabled) {
        var item = tree.GetToolbar(0).GetItemByName(name);
        if (item)
            item.SetEnabled(enabled);
    }
    function FM_IsFolderNode(key) {
        for (var i = 0; i < tree.cpFolderKeys.length; i++) {
            if (tree.cpFolderKeys[i] == key)
                return true;
        }
        return false;
    }

    window.OnSelectedFileChanged = OnSelectedFileChanged;
    window.DemoRichEditEndCallback = DemoRichEditEndCallback;
    window.OnExceptionOccurred = OnExceptionOccurred;

    window.editor_Init = editor_Init;
    window.editor_KeyPress = editor_KeyPress;
    window.editor_LostFocus = editor_LostFocus;
    window.uploader_Complete = uploader_Complete;
    window.tree_Init = tree_Init;
    window.tree_FocusChanged = tree_FocusChanged;

    window.tree_BeginCallback = tree_BeginCallback;
    window.tree_EndCallback = tree_EndCallback;
    window.tree_NodeClick = tree_NodeClick;
    window.tree_StartDragNode = tree_StartDragNode;
    window.tree_EndDragNode = tree_EndDragNode;
})();