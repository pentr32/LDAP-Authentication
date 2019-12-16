using System;
using System.Collections.Generic;
using System.Text;

namespace LDAP_Auth
{
    public interface IAuthenticationService
    {
        //Validere brugeren og får hans gruppe properties.
        AppUser Validate(string username, string pass);

        //Validere brugeren i hvilke grupper er han/hende medlem af.
        List<string> GetGroups(string username);
    }
}
