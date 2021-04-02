using System.Collections.Generic;

namespace Action.Domain
{
    public class Personnel : Audit
    {
        public int Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public List<License> Licenses { get; private set; }

        protected Personnel()
        {
            Licenses = new List<License>();
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

        public void AddLicense(License license)
        {
            var licenseInList = Licenses.Find(l => l.LicenseName == license.LicenseName);
            if (licenseInList == null)
            {
                Licenses.Add(license);
            }
            else
            {
                license.Update(license);
            }
        }

        public void RemoveLicense(string licenseName)
        {
            var licenseInList = Licenses.Find(l => l.LicenseName == licenseName);

            if (licenseInList != null)
            {
                licenseInList.Delete();
            }
        }
    }
}
