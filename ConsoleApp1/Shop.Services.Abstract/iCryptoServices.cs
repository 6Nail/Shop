using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Abstract
{
    public interface iCryptoServices
    {
        string EnCryptPassword(string password);
        bool VerifyPassword(string password, string passwordCandidate);
    }
}
