using System;
using System.Threading.Tasks;
using datingapp.Models;
using Microsoft.EntityFrameworkCore;

namespace datingapp.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)//checking if user exists. We use FirstOrDefaultAsync in here
                return null;
            //Verifying password (Check if it DOES NOT match)
            
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             //Instead of outputting a key here. we will actually use that key (PasswordSalt) and pass it
            //Create instance of HMACSHA512
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            { //Compute Hash from password to be compared against the stored hash.
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Add user
            await _context.Users.AddAsync(user);
            // Save changes
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        { // surrounding this with a using keyword in order to call the 'Dispose()' method which frees resources used
            // by Cryptography as soon as our using use of this class is done. (every thing inside { })
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            //ComputeHash requires input to be an array of bytes (byte[])
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}