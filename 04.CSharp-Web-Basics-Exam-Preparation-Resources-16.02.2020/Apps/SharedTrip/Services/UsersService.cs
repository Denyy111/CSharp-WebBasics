﻿using Microsoft.EntityFrameworkCore.Internal;
using SharedTrip.Data;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;
        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string username, string email, string passord)
        {
            //Create user and inicialised property inside.
            var user = new User
            {
                Email = email,
                Username = username,
                Password = ComputeHash(passord),
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsEmailAvailable(string email)
        {
            //trqbwa da nqma takyv
            return !this.db.Users.Any(x => x.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = ComputeHash(password);
           var user =  this.db.Users.FirstOrDefault(x => x.Username == username
            && x.Password == hashPassword);
            //Can be null
            return user?.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            //trqbwa da nqma takyw
            return !this.db.Users.Any(x => x.Username == username);
        }

        //Method for hashering password
        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            //Convert to text
            //StringBuilder Capacity is 128. because 512 bits/ 8bits in byte * 2 symbols for byte
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }


    }
}
