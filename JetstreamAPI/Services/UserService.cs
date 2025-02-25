using MongoDB.Driver;
using System.Threading.Tasks;

namespace JetstreamAPI.Services
{
    // Service class responsible for user management, including user creation, authentication, and password hashing
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        // Constructor that initializes the MongoDB collection for users
        public UserService(IMongoClient client)
        {
            var database = client.GetDatabase("JetstreamDB");
            _usersCollection = database.GetCollection<User>("Users");
        }

        // Create a new user, hash the password, and store it in MongoDB
        public async Task CreateUser(User user)
        {
            // Hash the user's password before storing it in the database
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _usersCollection.InsertOneAsync(user);  // Save the user to the database
        }

        // Authenticate a user by checking their username and password
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await GetUserByUsername(username);  // Retrieve user by username
            if (user != null && VerifyPassword(password, user.PasswordHash))  // Verify the entered password against the stored hash
            {
                return user;  // Return user if authentication is successful
            }
            return null;  // Return null if authentication fails
        }

        // Retrieve a user by their username from MongoDB
        public async Task<User> GetUserByUsername(string username)
        {
            return await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();  // Fetch user with the given username
        }

        // Hash the password using a hashing algorithm (e.g., SHA256, bcrypt, PBKDF2)
        private string HashPassword(string password)
        {
            // Example using SHA256 for password hashing (replace with stronger methods like bcrypt)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);  // Return the password hash as a base64 string
            }
        }

        // Verify if the entered password matches the stored password hash
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Hash the entered password and compare it with the stored hash
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;  // Return true if hashes match, false otherwise
        }
    }
}
