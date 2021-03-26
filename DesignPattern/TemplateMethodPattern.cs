using System;

namespace DesignPattern
{

    public abstract class Record
    {
        public void Save()
        {
            this.Validate();
            this.beforeSave();
            // some DB query Save;
        }

        public abstract void Validate(); // abstraction Method

        public virtual void beforeSave() 
        { 
            // default hook method
        }
    }

    public class User : Record
    {
        public override void beforeSave()
        {
            // some before saving specific to user
            Console.WriteLine("user BeforeSave");
        }

        public override void Validate()
        {
            // some specific user validation
            Console.WriteLine("user Validate");
        }
    }

    public class TemplateMethodPattern
    {
        User _user;

        public TemplateMethodPattern()
        {
            _user = new User();
        }

        public void Save()
        {
            _user.Save();
            // should output "user Validate" && user BeforeSave
        }
    }
}
