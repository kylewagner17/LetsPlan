using LetsPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlan.Services.Interfaces;
using SQLite;

namespace LetsPlan.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _db;

        private async Task InitAsync()
        {
            if (_db != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "events.db");
            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<Event>();
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            await InitAsync();
            return await _db.Table<Event>().ToListAsync();
        }

        public async Task<List<Event>> GetEventsForDateAsync(DateTime selectedDate)
        {
            await InitAsync();
            return await _db.Table<Event>()
                .Where(e => e.Date == selectedDate.Date).ToListAsync();
        }

        public async Task SaveEventAsync(Event ev)
        {
            await InitAsync();
            await _db.InsertAsync(ev);
        }

        public async Task ClearEventsAsync()
        {
            await InitAsync();
            await _db.DeleteAllAsync<Event>();
        }
    }
}
