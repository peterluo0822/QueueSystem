﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chloe;
using Model;

namespace DAL
{
    public class TDictionaryDAL
    {
        DbContext db;
        public TDictionaryDAL()
        {
            this.db = Factory.Instance.CreateDbContext();
        }

        public TDictionaryDAL(DbContext db)
        {
            this.db = db;
        }

        public TDictionaryDAL(string dbKey)
        {
            this.db = Factory.Instance.CreateDbContext(dbKey);
        }

        #region CommonMethods

        public List<TDictionaryModel> GetModelList()
        {
            return db.Query<TDictionaryModel>().ToList();
        }

        public List<TDictionaryModel> GetModelList(Expression<Func<TDictionaryModel, bool>> predicate)
        {
            return db.Query<TDictionaryModel>().Where(predicate).ToList();
        }

        public TDictionaryModel GetModel(int id)
        {
            return db.Query<TDictionaryModel>().Where(p => p.ID == id).FirstOrDefault();
        }

        public TDictionaryModel GetModel(Expression<Func<TDictionaryModel, bool>> predicate)
        {
            return db.Query<TDictionaryModel>().Where(predicate).FirstOrDefault();
        }

        public TDictionaryModel Insert(TDictionaryModel model)
        {
            return db.Insert(model);
        }

        public int Update(TDictionaryModel model)
        {
            return this.db.Update(model);
        }

        public int Delete(TDictionaryModel model)
        {
            return this.db.Delete(model);
        }

        #endregion

        public IQuery<TDictionaryModel> GetModelQuery(string name)
        {
            return this.db.Query<TDictionaryModel>()
                .Where(p => p.Name == name && p.Group == 0)
                .LeftJoin<TDictionaryModel>((c, i) => c.ID == i.Group)
                .Select((c, i) => i);
        }

        public List<TDictionaryModel> GetModelList(string name)
        {
            return this.GetModelQuery(name).ToList();
        }
    }
}
