using System;
using Realms;
using System.Collections.Generic;
namespace Yodaz.Model
{
    public class User : RealmObject
    {
        [PrimaryKey]
        [MapTo("userId")]
        public string UserId { get; set; } = Guid.NewGuid().ToString();     //Behöver vi generera upp ett nytt id?

        [MapTo("username")]
        public string Username { get; set; }

        [MapTo("email")]
        public string Email { get; set; }

        [MapTo("contactList")]
        public IList<User> Contacts { get; }
    }
}
