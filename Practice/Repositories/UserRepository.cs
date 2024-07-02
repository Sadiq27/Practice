using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Practice.Models;

namespace Practice.Repositories;
public class UserRepository
{
private readonly string connectionString;

public UserRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }


public async Task<IEnumerable<User>> GetAllCategoriesAsync()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var query = "Select * From Users";
            return await db.QueryAsync<User>(query);
        }
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var query = "Select * From Users Where Id = @Id";
            return await db.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }
    }

    public async Task<int> CreateUserAsync(User User)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var query = "Insert Into Users (Name) Values (@Name)";
            return await db.QuerySingleAsync<int>(query, new { User.Name });
        }
    }

    public async Task<bool> UpdateUserAsync(User User)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var query = "Update Users Set Name = @Name Where Id = @Id";
            var rowsAffected = await db.ExecuteAsync(query, User);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var query = "Delete From Users Where Id = @Id";
            var rowsAffected = await db.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
