namespace TForTea.Repositories.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TForTea.Managers;
    using TForTea.Models;

    public class TeaDBService
    {
        private LogManager logManager;

        public TeaDBService(LogManager logManager)
        {
            this.logManager = logManager;
        }

        public bool Add(Tea obj)
        {
            try
            {
                using (var db = new TeaContext())
                {
                    db.Teas.Add(obj);
                    db.SaveChanges();
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
                using (var db = new TeaContext())
                {
                    return db.Teas.FirstOrDefault(x => x.Id == id);
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
                using (var db = new TeaContext())
                {
                    return db.Teas.FirstOrDefault(x => x.Guid == guid);
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
                using (var db = new TeaContext())
                {
                    return db.Teas.ToList();
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return null;
            }
        }

        public bool Remove(Tea obj)
        {
            try
            {
                using (var db = new TeaContext())
                {
                    db.Teas.Remove(obj);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return false;
            }
        }

        public bool Update(Tea obj)
        {
            try
            {
                using (var db = new TeaContext())
                {
                    db.Teas.Update(obj);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return false;
            }
        }
    }
}