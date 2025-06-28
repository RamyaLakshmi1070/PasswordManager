using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.Helper;
using PasswordManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Repository;

public class PasswordManagerRepository(PasswordManagerDbContext context)
{      
    private readonly PasswordManagerDbContext _context = context;

    public async Task<List<Password>> GetAllAsync()
    {
        return await _context.Passwords
            .Select(p => new Password
            {
                Id = p.Id,
                Category = p.Category,
                App = p.App,
                UserName = p.UserName,
                EncryptedPassword = p.EncryptedPassword
            })
            .ToListAsync();
    }

    public async Task<Password?> GetByIdAsync(int id, bool decrypt = false)
    {
        var password = await _context.Passwords.FindAsync(id);
        if (password == null) return null;

        var model = new Password
        {
            Id = password.Id,
            Category = password.Category,
            App = password.App,
            UserName = password.UserName,
            EncryptedPassword = decrypt ? PasswordEncryptionHelper.Decrypt(password.EncryptedPassword) : password.EncryptedPassword
        };
        return model;
    }

    public async Task AddAsync(Password password)
    {
        var entity = new Models.Password
        {
            Category = password.Category,
            App = password.App,
            UserName = password.UserName,
            EncryptedPassword = PasswordEncryptionHelper.Encrypt(password.EncryptedPassword)
        };
        await _context.Passwords.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Password password)
    {
        var existing = await _context.Passwords.FindAsync(password.Id);
        if (existing != null)
        {
            existing.Category = password.Category;
            existing.App = password.App;
            existing.UserName = password.UserName;
            existing.EncryptedPassword = PasswordEncryptionHelper.Encrypt(password.EncryptedPassword);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var password = await _context.Passwords.FindAsync(id);
        if (password != null)
        {
            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
        }
    }
}