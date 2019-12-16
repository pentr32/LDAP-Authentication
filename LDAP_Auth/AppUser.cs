using System.Collections.Generic;

namespace LDAP_Auth
{
    public class AppUser
    {
        //Fornavn.
        private string givenname;                          
        public string GivenName { get => givenname; set => givenname = value; }

        //Fuldenavn.
        private string displayname;                        
        public string DisplayName { get => displayname; set => displayname = value; }

        //Brugernavn.
        private string username;
        public string UserName { get => username; set => username = value; }

        //Email.
        private string email;                              
        public string Email { get => email; set => email = value; }

        //Grupper
        private List<string> grupper;
        public List<string> Grupper { get { return grupper; } set { grupper = value; } }
    }
}
