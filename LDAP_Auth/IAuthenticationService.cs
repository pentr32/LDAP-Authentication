using System;
using System.Collections.Generic;
using System.Text;

namespace LDAP_Auth
{
    public interface IAuthenticationService
    {
        // Validate user and return informations.
        // Valideaza user-ul si returneaza informatiile.
        // Validere brugeren og får hans gruppe properties.
        AppUser Validate(string username, string pass);

        // Checks user in which groups is member of.
        // Verifica user-ul in ce grupe este membru.
        // Tjekker brugeren i hvilke grupper er han/hende medlem af.
        List<string> GetGroups(string username);
    }
}
