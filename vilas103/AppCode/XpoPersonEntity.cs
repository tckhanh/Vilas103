using System;
using DevExpress.Xpo;
using DevExpress.Web.Demos;

[Persistent("Persons")]
class XpoPersonEntity : XPLiteObject {
    int _id;
    string _lastName;
    string _firstName;
    string _phone;

    public XpoPersonEntity(Session session)
        : base(session) {
    }

    [Key(true)]
    public int ID {
        get { return _id; }
        set { _id = value; }
    }

    public string FirstName {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string LastName {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public string Phone {
        get { return _phone; }
        set { _phone = value; }
    }

    protected override void OnSaving() {
        Utils.AssertNotReadOnly();
    }

    protected override void OnDeleting() {
        Utils.AssertNotReadOnly();
    }
}
