using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.Helper;
using PasswordManager.Data.Models;
using PasswordManager.Data.Repository;
namespace PasswordManager.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddAsync_ShouldAddEncryptedPassword()
        {
            var context = TestHelper.GetInMemoryDbContext();
            var repo = new PasswordManagerRepository(context);

            var password = new Password
            {
                App = "Gmail",
                Category = "Personal",
                UserName = "test@example.com",
                EncryptedPassword = "MyPlainPassword"
            };

            await repo.AddAsync(password);

            var result = await context.Passwords.FirstOrDefaultAsync();

            Assert.NotNull(result);
            Assert.Equal("Gmail", result.App);
            Assert.NotEqual("MyPlainPassword", result.EncryptedPassword); // Should be encrypted
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPasswords()
        {
            var context = TestHelper.GetInMemoryDbContext();
            context.Passwords.AddRange(
                new Password { App = "Outlook", Category = "Work", UserName = "user1", EncryptedPassword = "encrypted1" },
                new Password { App = "Messenger", Category = "School", UserName = "user2", EncryptedPassword = "encrypted2" }
            );
            context.SaveChanges();

            var repo = new PasswordManagerRepository(context);

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WithDecrypt_ShouldReturnDecryptedPassword()
        {
            var context = TestHelper.GetInMemoryDbContext();
            var encrypted = PasswordEncryptionHelper.Encrypt("Secret123");

            var password = new Password { App = "Zoom", Category = "Work", UserName = "admin", EncryptedPassword = encrypted };
            context.Passwords.Add(password);
            context.SaveChanges();

            var repo = new PasswordManagerRepository(context);

            var result = await repo.GetByIdAsync(password.Id, decrypt: true);

            Assert.NotNull(result);
            Assert.Equal("Secret123", result.EncryptedPassword);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePassword()
        {
            var context = TestHelper.GetInMemoryDbContext();
            var encrypted = PasswordEncryptionHelper.Encrypt("OldPassword");
            var password = new Password { App = "Skype", Category = "Work", UserName = "user", EncryptedPassword = encrypted };
            context.Passwords.Add(password);
            context.SaveChanges();

            var repo = new PasswordManagerRepository(context);

            password.EncryptedPassword = "NewPassword";
            password.App = "SkypeUpdated";

            await repo.UpdateAsync(password);

            var updated = await context.Passwords.FindAsync(password.Id);
            Assert.Equal("SkypeUpdated", updated.App);
            Assert.NotEqual("NewPassword", updated.EncryptedPassword); // Should be encrypted
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemovePassword()
        {
            var context = TestHelper.GetInMemoryDbContext();
            var password = new Password { App = "Discord", Category = "Chat", UserName = "deluser", EncryptedPassword = "delete" };
            context.Passwords.Add(password);
            context.SaveChanges();

            var repo = new PasswordManagerRepository(context);

            await repo.DeleteAsync(password.Id);

            var deleted = await context.Passwords.FindAsync(password.Id);
            Assert.Null(deleted);
        }
    }
}