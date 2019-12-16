using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace LDAP_Auth
{
    public class LDAPUtil : IAuthenticationService
    {
        #region LDAP
        private const string Domain = "CHANGE_ME";      //example.com
        private const int Port = 636;       //The 636 port use SSL
        private const string SearchFilter = "(&(objectClass=user)(objectClass=person)(sAMAccountName={0}))";        //Base SearchFilter !!!DO NOT CHANGE!!!
        private const string BaseDC = "CHANGE_ME";      //DC=example,DC=com
        #endregion

        #region ActiveDirectory Attributes
        private const string GivenNameAttribute = "givenName";
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";
        #endregion

        private static AppUser User { get; set; }

        /// <summary>
        /// Validate user and return informations.
        /// Valideaza user-ul si returneaza informatiile.
        /// Validere brugeren og får hans gruppe properties.
        /// </summary>
        /// <param name="username">Brugernanv</param>
        /// <param name="password">Adgangskode</param>
        /// <returns></returns>
        public AppUser Validate(string username, string password)
        {
            string userDn = $"{username}@{Domain}";

            var connection = new LdapConnection { SecureSocketLayer = true };

            try
            {
                connection.Connect(Domain, Port);
                connection.Bind(userDn, password);
                var filter = string.Format(SearchFilter, username);
                var result = connection.Search(BaseDC, LdapConnection.SCOPE_SUB, filter, new[] { MemberOfAttribute, DisplayNameAttribute, SAMAccountNameAttribute, GivenNameAttribute, MailAttribute }, false);
                var userInfo = result.Next();
                if (connection.Bound)
                {
                    return User = new AppUser()
                    {
                        GivenName = userInfo.getAttribute(GivenNameAttribute).StringValue,
                        DisplayName = userInfo.getAttribute(DisplayNameAttribute).StringValue,
                        UserName = userInfo.getAttribute(SAMAccountNameAttribute).StringValue,
                        Email = userInfo.getAttribute(MailAttribute).StringValue,
                        Grupper = GetGroups(username)
                    };
                }
            }
            catch
            {
                throw new Exception("Login fejl.");
            }
            connection.Disconnect();
            return null;
        }

        /// <summary>
        /// Checks user in which groups is member of.
        /// Verifica user-ul in ce grupe este membru.
        /// Tjekker brugeren i hvilke grupper er han/hende medlem af.
        /// </summary>
        /// <param name="username">Brugernavn</param>
        /// <param name="gruppe">Gruppernavn</param>
        /// <returns></returns>
        public List<string> GetGroups(string username)
        {
            List<string> Grupper = new List<string>();

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);

            if(user != null)
            {
                PrincipalSearchResult<Principal> PGrupper = user.GetAuthorizationGroups();

                foreach(Principal p in PGrupper) if (p is GroupPrincipal) Grupper.Add(p.ToString());
            }

            return Grupper;
        }
    }
}
