using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMLKeys.Models
{
    public class Keys
    {
        public string clientID { get; set; }
        public string clientSecret { get; set; }
        public string status { get; set; }

        public Keys(string clientID, string clientSecret, string status)
        {
                this.clientID = clientID;
                this.clientSecret = clientSecret;
                this.status = status;
        }
    }
}