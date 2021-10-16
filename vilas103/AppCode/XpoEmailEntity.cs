using System;
using DevExpress.Xpo;
using DevExpress.Web.Demos;

[Persistent("Emails")]
class XpoEmailEntity : XPLiteObject {
    int _id;
    string _subject;
    string _from;
    DateTime _sent;
    long _size;
    bool _hasAttachment;

    public XpoEmailEntity(Session session)
        : base(session) {
    }

    [Key(true)]
    public int ID {
        get { return _id; }
        set { _id = value; }
    }

    public string Subject {
        get { return _subject; }
        set { _subject = value; }
    }

    public string From {
        get { return _from; }
        set { _from = value; }
    }

    public DateTime Sent {
        get { return _sent; }
        set { _sent = value; }
    }

    public long Size {
        get { return _size; }
        set { _size = value; }
    }

    public bool HasAttachment {
        get { return _hasAttachment; }
        set { _hasAttachment = value; }
    }

    protected override void OnSaving() {
        Utils.AssertNotReadOnly();
    }

    protected override void OnDeleting() {
        Utils.AssertNotReadOnly();
    }
}
