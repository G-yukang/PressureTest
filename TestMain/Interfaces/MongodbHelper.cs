using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestMain.Model;

namespace TestMain.Interfaces
{
    public class MongoDBHelper
    {
        private static string connectionString = "mongodb://localhost:27017";
        private static IMongoClient client;
        private static IMongoDatabase database;

        static MongoDBHelper()
        {
            client = new MongoClient(connectionString);
        }
        public static IMongoDatabase GetDatabase(string dbName)
        {
            if (database == null)
            {
                try
                {
                    database = client.GetDatabase(dbName);
                }
                catch (Exception ex)
                {
                    LogError($"连接数据库失败：{ex.Message}");
                    throw;
                }
            }
            return database;
        }

        public static IMongoCollection<T> GetCollection<T>(string dbName, string collectionName)
        {
            var database = GetDatabase(dbName);
            return database.GetCollection<T>(collectionName);
        }

        public static async Task InsertDocumentAsync<T>(string dbName, string collectionName, T document)
        {
            try
            {
                var collection = GetCollection<T>(dbName, collectionName);
                await collection.InsertOneAsync(document);
            }
            catch (Exception ex)
            {
                LogError($"插入文档失败：{ex.Message}");
                throw;
            }
        }
        public static async Task InsertDocumentsAsync<T>(string dbName, string collectionName, List<T> documents)
        {
            try
            {
                var collection = GetCollection<T>(dbName, collectionName);
                await collection.InsertManyAsync(documents);
            }
            catch (Exception ex)
            {
                LogError($"批量插入文档失败：{ex.Message}");
                throw;
            }
        }

        public static async Task<T> FindDocumentAsync<T>(string dbName, string collectionName, FilterDefinition<T> filter)
        {
            try
            {
                var collection = GetCollection<T>(dbName, collectionName);
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                LogError($"查找文档失败：{ex.Message}");
                throw;
            }
        }

        // 实现更新文档的异步方法
        public static async Task<UpdateResult> UpdateDocumentAsync<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            try
            {
                var collection = GetCollection<T>(dbName, collectionName);
                return await collection.UpdateOneAsync(filter, update);


            }
            catch (Exception ex)
            {
                LogError($"更新文档失败：{ex.Message}");
                throw;
            }
        }

        // 实现删除文档的异步方法
        public static async Task<DeleteResult> DeleteDocumentAsync<T>(string dbName, string collectionName, FilterDefinition<T> filter)
        {
            try
            {
                var collection = GetCollection<T>(dbName, collectionName);
                return await collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                LogError($"删除文档失败：{ex.Message}");
                throw;
            }
        }

        // 连接字符串可配置化
        public static void SetConnectionString(string newConnectionString)
        {
            // 可以添加一些验证新连接字符串的逻辑
            connectionString = newConnectionString;
            client = new MongoClient(connectionString);
            database = null; // 重置数据库对象
        }


        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogError(string message)
        {
            Log.Error(message);
        }
    }
}
