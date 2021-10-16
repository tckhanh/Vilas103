using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

public class InMemoryPerson {
    int _id;
    string _firstName, _lastName;
    int _age;
    string _email;
    DateTime _arrivalDate;

    public InMemoryPerson() {
    }

    public InMemoryPerson(int id) {
        _id = id;
    }

    public InMemoryPerson(int id, string firstName, string lastName, int age, string email, DateTime arrivalDate)
        : this(id) {
        _firstName = firstName;
        _lastName = lastName;
        _age = age;
        _email = email;
        _arrivalDate = arrivalDate;
    }

    public int Id { get { return _id; } set { _id = value; } }
    public string FirstName { get { return _firstName; } set { _firstName = value; } }
    public string LastName { get { return _lastName; } set { _lastName = value; } }
    public string Email { get { return _email; } set { _email = value; } }
    public int Age { get { return _age; } set { _age = value; } }
    public DateTime ArrivalDate { get { return _arrivalDate; } set { _arrivalDate = value; } }

    internal void Assign(InMemoryPerson data) {
        _firstName = data.FirstName;
        _lastName = data.LastName;
        _email = data.Email;
        _age = data.Age;
        _arrivalDate = data.ArrivalDate;
    }
}
public class InMemoryPersonProvider {
    HttpSessionState Session { 
        get { return HttpContext.Current.Session; } 
    }

    const string SessionKey = "DXPersonProviderData";

    public List<InMemoryPerson> GetList() {
        if(Session[SessionKey] == null)
            Session[SessionKey] = CreateData();
        return (List<InMemoryPerson>)Session[SessionKey];
    }

    List<InMemoryPerson> CreateData() {
        List<InMemoryPerson> list = new List<InMemoryPerson>();
        list.Add(new InMemoryPerson(list.Count + 1, "Andrew", "Fuller", 42, "andrew.fuller@devexpress.com", DateTime.Today.AddDays(-32)));
        list.Add(new InMemoryPerson(list.Count + 1, "Nancy", "Davolio", 34, "nancy.davolio@devexpress.com", DateTime.Today.AddDays(4)));
        list.Add(new InMemoryPerson(list.Count + 1, "Margaret", "Peackop", 48, "margaret.peackop.devexpress.com", DateTime.Today.AddDays(6)));
        list.Add(new InMemoryPerson(list.Count + 1, "Robert", "K", 29, "robert.king@devexpress.com", DateTime.Today.AddDays(5)));
        list.Add(new InMemoryPerson(list.Count + 1, "Anne", "Dodsworth", 17, "anne.dodsworth@devexpress.com", DateTime.Today.AddDays(4)));
        return list;
    }

    public void Insert(InMemoryPerson data) {
        GetList().Add(data);
        data.Id = GetList().Count;
    }

    public void Update(InMemoryPerson data) {
        List<InMemoryPerson> list = GetList();
        foreach(InMemoryPerson item in list) {
            if(item.Id == data.Id) {
                item.Assign(data);
                return;
            }
        }
    }
}



