using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DontGetSpicy.Controllers;
using DontGetSpicy.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DontGetSpicy.JWT
{
    public class JWTGenerator
    {       private static JWTGenerator instance;
            public static void Instantiate(IConfiguration conf)
            {
                instance=new JWTGenerator(conf);
            }
            public static JWTGenerator getInstance()
            {
                return instance;
            }
            private static IConfiguration _config;
             public JWTGenerator(IConfiguration c)
            {
                _config=c;
            }

            public static string GenerateLoginToken(Korisnik korisnik)
            {
                Claim[] claims=new Claim[]{
                new Claim(JwtRegisteredClaimNames.Email,korisnik.email)
                };

                return GenerateToken(claims);
            }
             public static string GenerateGameToken(Korisnik korisnik,Igra igra,Boja boja)
            {
                Claim[] claims=new Claim[]{
                new Claim(JwtRegisteredClaimNames.Email,korisnik.email),
                new Claim(JwtRegisteredClaimNames.Sub,igra.groupNameGUID),
                new Claim("Boja", boja.ToString())
                //new Claim("kreatorIgre?",(igra.kreatorIgre.ID==korisnik.ID).ToString(), ClaimValueTypes.Boolean/*na klijentu se ocitava kao bool*/)
                };

                return GenerateToken(claims);
            }

             private static string GenerateToken(Claim[] claims)
            { 
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            
            
            var token=new JwtSecurityToken(
                issuer:_config["Jwt:Issuer"],
                audience:_config["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(60),
                signingCredentials:credentials);
            var encodetoken=new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
      
      
      
        }
    }

}