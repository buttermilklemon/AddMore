using System;
using System.ComponentModel;

namespace Messenger
{
    public class Message
    {
        public string id { get; set; }

        public string parent_id { get; set; }

        public string message { get; set; }

        public DateTime datestamp { get; set; }

        public bool is_reply { get; set; }

        public bool has_attachments { get; set; }

        public string attachment_id { get; set; }
    }
}

