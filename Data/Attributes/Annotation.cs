using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Attributes
{
    public static class Annotation
    {
        public static void GetAttribute(Type t)
        {
            RequiredAttribute att;

            // Get the class-level attributes.

            // Put the instance of the attribute on the class level in the att object.
            att = (RequiredAttribute)Attribute.GetCustomAttribute(t, typeof(RequiredAttribute));

            if (att == null)
            {
                Console.WriteLine("No attribute in class {0}.\n", t.ToString());
            }
            else
            {
                Console.WriteLine("The Name Attribute on the class level is: {0}.", att.ErrorMessage);
                //Console.WriteLine("The Level Attribute on the class level is: {0}.", att.);
                //Console.WriteLine("The Reviewed Attribute on the class level is: {0}.\n", att.Reviewed);
            }

            // Get the method-level attributes.

            // Get all methods in this class, and put them
            // in an array of System.Reflection.MemberInfo objects.
            PropertyInfo[] MyPropertyInfo = t.GetProperties();

            // Loop through all methods in this class that are in the
            // MyMemberInfo array.
            for (int i = 0; i < MyPropertyInfo.Length; i++)
            {
                att = (RequiredAttribute)Attribute.GetCustomAttribute(MyPropertyInfo[i], typeof(RequiredAttribute));
                if (att == null)
                {
                    Console.WriteLine("No attribute in member function {0}.\n", MyPropertyInfo[i].ToString());
                }
                else
                {
                    Console.WriteLine("The Name Attribute for the {0} member is: {1}.",
                        MyPropertyInfo[i].ToString(), att.ErrorMessage);
                    //Console.WriteLine("The Level Attribute for the {0} member is: {1}.",
                    //    MyMemberInfo[i].ToString(), att.Level);
                    //Console.WriteLine("The Reviewed Attribute for the {0} member is: {1}.\n",
                    //    MyMemberInfo[i].ToString(), att.Reviewed);
                }
            }

            // Get the method-level attributes.

            // Get all methods in this class, and put them
            // in an array of System.Reflection.MemberInfo objects.
            MemberInfo[] MyMemberInfo = t.GetMethods();

            // Loop through all methods in this class that are in the
            // MyMemberInfo array.
            for (int i = 0; i < MyMemberInfo.Length; i++)
            {
                att = (RequiredAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RequiredAttribute));
                if (att == null)
                {
                    Console.WriteLine("No attribute in member function {0}.\n", MyMemberInfo[i].ToString());
                }
                else
                {
                    Console.WriteLine("The Name Attribute for the {0} member is: {1}.",
                        MyMemberInfo[i].ToString(), att.ErrorMessage);
                    //Console.WriteLine("The Level Attribute for the {0} member is: {1}.",
                    //    MyMemberInfo[i].ToString(), att.Level);
                    //Console.WriteLine("The Reviewed Attribute for the {0} member is: {1}.\n",
                    //    MyMemberInfo[i].ToString(), att.Reviewed);
                }
            }
        }
    }
}
