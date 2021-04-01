using System;

namespace Action.Domain
{
    public class Audit
    {
        DateTimeOffset LastModifiedTime { get; set; }
        bool IsDeleted { get; set; }
        public void Delete()
        {
            IsDeleted = true;
        }

    }
}