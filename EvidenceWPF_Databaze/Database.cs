using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceWPF_Databaze
{
    public class Database
    {
        SQLiteAsyncConnection connection;
        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<ClassRoom>().Wait();
            connection.CreateTableAsync<Item>().Wait();
        }
        public Task<List<T>> GetItemsAsync<T>() where T : ATable, new()
        {
            return connection.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetItemsNotDoneAsync<T>() where T : ATable, new()
        {
            return connection.QueryAsync<T>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<T> GetItemAsync<T>(int id) where T : ATable, new()
        {
            return connection.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task SaveItemAsync<T>(T item) where T : ATable, new()
        {
            if (item.Id != 0)
            {
                return connection.UpdateAsync(item);
            }
            else
            {
                return connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : ATable, new()
        {
            return connection.DeleteAsync(item);
        }
    }
}
