using System;
using StajAppCore.Models.Auth;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace StajAppCore.Services
{
    public class PasswdHesher<T> where T: IUser
    {
        public bool SetHeshContSalt(T us, string passwd)
        {
            if (passwd == null || passwd == "")
                return false;

            byte[] salt = new byte[128];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            byte[] hashed = KeyDerivation.Pbkdf2(
            password: passwd,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256);

            us.Salt = Convert.ToBase64String(salt);
            us.Password = Convert.ToBase64String(hashed);

            return true;
        }

        public bool VerifyPasswd(T us, string passwd)
        {
            if (passwd == null || passwd == "")
                return false;
            
            byte[] salt = Convert.FromBase64String(us.Salt);
            byte[] hashed = KeyDerivation.Pbkdf2(
            password: passwd,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256);

            var hashPaswd = Convert.ToBase64String(hashed);
            if (us.Password == hashPaswd)
                return true;

            return false;
        }
    }
}
