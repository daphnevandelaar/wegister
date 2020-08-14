using System;

namespace Wegister.Models
{
    public class Status
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public DateTime MailedAt { get; set; }
    }
}
