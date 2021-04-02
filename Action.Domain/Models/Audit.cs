using System;

namespace Action.Domain
{
    public class Audit
    {
        public DateTimeOffset LastModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public void Delete()
        {
            IsDeleted = true;
        }

    }
}