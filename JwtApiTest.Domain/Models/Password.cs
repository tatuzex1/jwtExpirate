﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JwtApiTest.Domain.Models
{
    public sealed class Password
    {
        public string Encoded { get; }

        public Password(string password)
        {
            Encoded = EncodedPassword(password);
        }

        public static implicit operator Password(string password) => new Password(password);

        private static string EncodedPassword(string password)
        {
            string result;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using (var sha256 = new SHA256Managed())
                {
                    var hash = sha256.ComputeHash(bytes);
                    stream.Write(hash, 0, hash.Length);

                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes);
                }
            }
            return result;

        }
    }
}
