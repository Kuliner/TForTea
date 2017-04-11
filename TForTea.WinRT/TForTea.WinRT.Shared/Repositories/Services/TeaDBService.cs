namespace TForTea.Repositories.Services
{
    using Managers;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WinRT.Models;

    public class TeaDBService
    {
        private string connectionString;
        private LogManager logManager;

        public TeaDBService(string connectionString, LogManager logManager)
        {
            this.connectionString = connectionString;
            this.logManager = logManager;
        }

        public bool Add(Tea obj)
        {
            try
            {
                using (var db = new SQLiteConnection(connectionString))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(obj);
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return false;
            }
        }

        public Tea Get(int id)
        {
            try
            {
                using (var db = new SQLiteConnection(connectionString))
                {
                    return db.Table<Tea>().FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return null;
            }
        }

        public Tea Get(string guid)
        {
            try
            {
                using (var db = new SQLiteConnection(connectionString))
                {
                    return db.Table<Tea>().FirstOrDefault(x => x.Guid == guid);
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return null;
            }
        }

        public List<Tea> GetAll()
        {
            try
            {
                using (var db = new SQLiteConnection(connectionString))
                {
                    return db.Table<Tea>().ToList();
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return null;
            }
        }

        public bool Update(Tea tea)
        {
            try
            {
                using (var db = new SQLiteConnection(connectionString))
                {
                    db.RunInTransaction(() =>
                    {
                        db.Update(tea);
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return false;
            }
        }
    }
}