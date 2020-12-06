using API_UsuariosPosts.Models;
using CDataAccess.Interface;
using CModels.Context;
using CModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CDataAccess.DataAccess
{
    public class DaUser : IUser
    {
        private UsersContext usersContext_;

        public DaUser(UsersContext usersContext)
        {
            this.usersContext_ = usersContext;
        }

        public int ValidateExistence(string email)
        {
            try
            {
                var validation = usersContext_.UsersData.Select(i => i.Email == email).FirstOrDefault();
                if (validation)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int CreateUser(UsersData user)
        {
            try
            {
                bool validation = validateEmail(user.Email);
                if (!validation)
                {
                    user.Password = Encrypt(user.Password);
                    usersContext_.UsersData.Add(user);
                    int response = usersContext_.SaveChanges();
                    if (response > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string Encrypt(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public bool validateEmail(string email)
        {
            try
            {
                var validation = usersContext_.UsersData.Where(i => i.Email == email).ToList();
                if (validation.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
