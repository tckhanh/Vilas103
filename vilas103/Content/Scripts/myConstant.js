﻿var lib  = {
    isEmptyOrNull: function (value) {
        return typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null;
    },
};

var myConstant = {
    Tokens_Callback = "Tokens:",

    System_CanView_Role: "System_CanView",
    System_CanViewDetail_Role: "System_CanViewDetail",
    System_CanViewChart_Role: "System_CanViewChart",
    System_CanViewStatitics_Role: "System_CanViewStatitics",
    System_CanAdd_Role: "System_CanAdd",
    System_CanImport_Role: "System_CanImport",
    System_CanExport_Role: "System_CanExport",
    System_CanEdit_Role: "System_CanEdit",
    System_CanReset_Role: "System_CanReset",
    System_CanLock_Role: "System_CanLock",
    System_CanDelete_Role: "System_CanDelete",

    Data_CanView_Role: "Data_CanView",
    Data_CanViewDetail_Role: "Data_CanViewDetail",
    Data_CanViewChart_Role: "Data_CanViewChart",
    Data_CanViewReport_Role: "Data_CanViewReport",
    Data_CanViewStatitics_Role: "Data_CanViewStatitics",
    Data_CanAdd_Role: "Data_CanAdd",
    Data_CanImport_Role: "Data_CanImport",
    Data_CanExport_Role: "Data_CanExport",
    Data_CanEdit_Role: "Data_CanEdit",
    Data_CanReset_Role: "Data_CanReset",
    Data_CanLock_Role: "Data_CanLock",
    Data_CanCancel_Role: "Data_CanCancel",
    Data_CanSign_Role: "Data_CanSign",
    Data_CanDelete_Role: "Data_CanDelete",

    Info_CanViewMap_Role: "Info_CanViewMap",
    Info_CanViewDetail_Role: "Info_CanViewDetail",
    Info_CanPrintCertificate_Role: "Info_CanPrintCertificate",
    Info_CanViewChart_Role: "Info_CanViewChart",
    Info_CanViewReport_Role: "Info_CanViewReport",
    Info_CanViewStatitics_Role: "Info_CanViewStatitics",

    System_CanView_Role: "System_CanView",
    System_CanViewDetail_Role: "System_CanViewDetail",
    System_CanViewChart_Role: "System_CanViewChart",
    System_CanViewStatitics_Role: "System_CanViewStatitics",
    System_CanAdd_Role: "System_CanAdd",
    System_CanImport_Role: "System_CanImport",
    System_CanExport_Role: "System_CanExport",
    System_CanEdit_Role: "System_CanEdit",
    System_CanReset_Role: "System_CanReset",
    System_CanLock_Role: "System_CanLock",
    System_CanDelete_Role: "System_CanDelete",

    Action_View: "View",
    Action_Add: "Add",
    Action_Detail: "Detail",
    Action_Edit: "Edit",
    Action_Delete: "Delete",
    Action_Lock: "Lock",
    Action_Sign: "Sign",
    Action_SignAll: "SignAll",
    Action_Cancel: "Cancel",
    Action_CancelAll: "CancelAll",
    Action_Sign_NoCert: "Sign_NoCert",
    Action_SignAll_NoCert: "SignAll_NoCert",
    Action_Cancel_NoCert: "Cancel_NoCert",
    Action_CancelAll_NoCert: "CancelAll_NoCert",
    Action_Cancel_NoRequiredBts: "Cancel_NoRequiredBts",
    Action_CancelAll_NoRequiredBts: "CancelAll_NoRequiredBts",
    Action_Reset: "Reset",
    Action_ViewMap: "ViewMap",
    Action_Print: "Print",

    SelectAll: "ALL",
};
Object.freeze(myConstant);