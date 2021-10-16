using System;
using System.Configuration;
using System.Threading;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.Web.Demos;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Model;

public static class XpoHelper {
    public static Section GetNewSession() {
        return new Section(DataLayer);
    }

    private readonly static object _lockObject = new object();

    static object _dataLayer;
    static IDataLayer DataLayer {
        get {
            if(_dataLayer == null) {
                lock(_lockObject) {
                    if(Thread.VolatileRead(ref _dataLayer) == null) {
                        Thread.VolatileWrite(ref _dataLayer, CreateDataLayer());
                    }
                }
            }
            return (IDataLayer)_dataLayer;
        }
    }

    static IDataLayer CreateDataLayer() {
        XpoDefault.Session = null;
        XPDictionary dict = new ReflectionDictionary();
        IDataStore store = XpoDefault.GetConnectionProvider(GetConnectionString(), AutoCreateOption.SchemaAlreadyExists);
        dict.GetDataStoreSchema(typeof(XpoEmailEntity), typeof(XpoPersonEntity));
        IDataLayer dl = new ThreadSafeDataLayer(dict, store);
        return dl;
    }

    static string GetConnectionString() {
        DatabaseGenerator.TableData table = EmailDataGenerator.Table ?? PersonDataGenerator.Table;
        string result = table.ConnectionString;
        result += ";XpoProvider=MSSqlServer";
        return result;
    }
}
