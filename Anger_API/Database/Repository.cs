﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database
{
    public class Repository : IRepository
    {
        public virtual string TableName { get; set; }
        public async Task<string> CreateAsync(Table table)
        {
            table.CreatedAt = DateTime.UtcNow;
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            string ID = await db.Query(TableName)
                .InsertGetIdAsync<string>(table);
            return ID;
        }
        public async Task<T> RetrieveByID<T>(long ID)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var objs = await db.Query(TableName)
                .Where(nameof(ID), ID)
                .GetAsync<T>();
            return objs.FirstOrDefault();
        }
    }
}