using System;

namespace Action.Domain
{
    public class Personnel : Audit
    {
        public int UserId { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public DateTimeOffset LastModifiedTime { get; set; }
        public bool IsDeleted { get; set; }

        protected Personnel()
        {
        }

        public Personnel(string firstName, string lastName, string eMail, string address) : base()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = eMail;
            Address = address;
        }

        public void EditAddress(string address)
        {
            Address = address;
        }

        public void EditEmail(string email)
        {
            Email = email;
        }
     
    }
}
