﻿using CDataAccess.Interface;
using CModels.Context;
using CModels.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CDataAccess.DataAccess
{
    public class DaLogin : ILogin
    {
        private UsersContext usersContext_;
        private readonly IUser user_;
        private readonly IConfiguration config_;
        private AuthenticatedUser authenticatedUser { get; set; }

        public DaLogin(UsersContext usersContext, IUser user, IConfiguration config)
        {
            this.usersContext_ = usersContext;
            user_ = user;
            config_ = config;
        }
        public bool Login(UsersData user)
        {
            try
            {

                string encryptedPassword = DaUser.Encrypt(user.Password);
                var response = usersContext_.UsersData.Where(i => i.Email == user.Email && i.Password == encryptedPassword && i.Username == user.Username).FirstOrDefault();
                if (response != null)
                {
                    var secretKey = config_.GetValue<string>("SecretKey");
                    var keyJWT = Encoding.ASCII.GetBytes(secretKey);

                    user.IdUserData = response.IdUserData;
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.IdUserData.ToString()));
                    claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                    var tokenDecrypted = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        NotBefore = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyJWT), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenH = new JwtSecurityTokenHandler();
                    var createdToken = tokenH.CreateToken(tokenDecrypted);
                    string bearerToken = tokenH.WriteToken(createdToken);
                    user.Token = bearerToken;

                    authenticatedUser = new AuthenticatedUser()
                    {
                        Username = response.Username,
                        Email = response.Email,
                        TokenJWT = bearerToken,
                        IdUser = response.IdUserData
                    };

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}
