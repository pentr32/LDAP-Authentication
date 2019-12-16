using System.Collections.Generic;

namespace LDAP_Auth
{
    public class AppUser
    {
        // Firstname.
        // Nume.
        // Fornavn.
        private string givenname;                          
        public string GivenName { get => givenname; set => givenname = value; }

        // Fullname
        // Nume full.
        // Fuldenavn.
        private string displayname;                        
        public string DisplayName { get => displayname; set => displayname = value; }

        // Username
        // Numele utilizator-ului
        // Brugernavn.
        private string username;
        public string UserName { get => username; set => username = value; }

        // Email address.
        // Adresa de e-mail.
        // Email addresse.
        private string email;                              
        public string Email { get => email; set => email = value; }

        // Groups.
        // Grupe.
        // Grupper.
        private List<string> grupper;
        public List<string> Grupper { get { return grupper; } set { grupper = value; } }
    }
}
