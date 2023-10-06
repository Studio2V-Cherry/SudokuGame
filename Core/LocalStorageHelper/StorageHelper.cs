using Core.Models;
using Microsoft.Maui.Platform;
using SQLite;

namespace Core.LocalStorageHelper
{

    /// <summary>
    /// 
    /// </summary>
    public class StorageHelper
    {
        private static StorageHelper _instance;

        public static StorageHelper Instance
        {
            get=>_instance ?? (_instance = new StorageHelper());
        }
        /// <summary>
        /// The database
        /// </summary>
        SQLiteAsyncConnection Database;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageHelper" /> class.
        /// </summary>
        public StorageHelper()
        {

        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        async Task Init()
        {
            try
            {

                if (Database is not null)
                    return;
                Database = new SQLiteAsyncConnection(Constants.Constants.DatabasePath, Constants.Constants.Flags);
                var result = await Database.CreateTableAsync<SudukoLocalDataHelper>(CreateFlags.None);
            }
            catch (Exception e)
            {
                _ = e;
            }
        }

        //Operations
        /// <summary>
        /// Gets the items not done asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SudukoLocalDataHelper>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<SudukoLocalDataHelper>().ToListAsync();
            //return await Database.Table<SudukoLocalDataHelper>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<SudukoLocalDataHelper>("SELECT * FROM [SudukoLocalDataHelper] WHERE [Done] = 0");
        }

        public async Task InitialiaeDb()
        {
            await Init().ConfigureAwait(true);
        }
        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<SudukoLocalDataHelper> GetItemAsync(int id)
        {
            await Init();
            //return await Database.Table<SudukoLocalDataHelper>().Where(i => i.ID == id).FirstOrDefaultAsync();
            return await Database.Table<SudukoLocalDataHelper>().FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the saved instance asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SudukoLocalDataHelper>> GetSavedInstanceAsync()
        {
            await Init();
            return await Database.Table<SudukoLocalDataHelper>().ToListAsync();
        }

        public async Task<bool> isDataPresent()
        {
            await Init();
            try
            {
                var res = await Database.Table<SudukoLocalDataHelper>().CountAsync();
                return res != 0;
            }
            catch(Exception e)
            {
                _ = e;
                return false;
            }
        }

        /// <summary>
        /// Saves the instance asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<int> SaveInstanceAsync(SudukoLocalDataHelper item)
        {
            await Init();
            item.BoardSavedName = Constants.DBHelperConstants.SudukoGenerated;
            //if (item.ID != 0)
            return await Database.InsertOrReplaceAsync(item);
            //else
            //    return await Database.InsertAsync(item);
        }

        /// <summary>
        /// Deletes the instance asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<int> DeleteInstanceAsync(SudukoLocalDataHelper item)
        {
           // await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
