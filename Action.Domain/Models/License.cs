using System;

namespace Action.Domain
{
    public class License : Audit
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public string LicenseName { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }

        public License(int personnelId, string licenseName, DateTimeOffset issueDate, DateTimeOffset expiryDate)
        {
            PersonnelId = personnelId;
            LicenseName = licenseName;
            IssueDate = issueDate;
            ExpiryDate = expiryDate;
        }

        public void Update(License license)
        {
            IssueDate = license.IssueDate;
            ExpiryDate = license.ExpiryDate;
        }
    }
}
