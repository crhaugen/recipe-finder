using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Diagnostics;

namespace recipeFinder
{
    public class SaveManager
    {
        static string accountKey = ConfigurationManager.AppSettings["TableConnectionString"];
        static string tableStorageName = "savedrecipes";
        static string blobStorageName = "recipecontainer";
        static string accountName = "recipefinderstorage";

        static StorageCredentials credentials = new StorageCredentials(accountName, accountKey);
        static CloudStorageAccount storageAccount = new CloudStorageAccount(credentials, useHttps: true);

       // static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["TableConnectionString"]);
        static CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        

        CloudTable table = cloudTableClient.GetTableReference(tableStorageName);
        CloudBlobContainer blobContainer = blobClient.GetContainerReference(blobStorageName);

        public bool addRecipe(string recipeName, string recipe, string username)
        {
            CloudBlockBlob blob;

            try
            {
                string fileName = recipeName + ".txt";
                blob = blobContainer.GetBlockBlobReference(fileName);
                blob.UploadText(recipe);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


            try
            {
                string blobURL = blob.StorageUri.PrimaryUri.ToString();
                Dictionary<string, EntityProperty> URLS = new Dictionary<string, EntityProperty>();
                URLS.Add("BlobURL", new EntityProperty(blobURL));
                string rowKey = recipeName.Replace(@"/", "");
                var newEntity = new DynamicTableEntity(username, rowKey, "*", URLS);
                TableOperation operation = TableOperation.InsertOrReplace(newEntity);
                table.Execute(operation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }
        public Dictionary<string, Uri> getSavedRecipes(string username)
        {
            try
            {
                var query = from entity in table.CreateQuery<DynamicTableEntity>()
                            where entity.PartitionKey.Equals(username)
                            select entity;

                Dictionary<string, Uri> returnDic = new Dictionary<string, Uri>();


                foreach (DynamicTableEntity entity in query)
                {
                    Uri uri = new Uri(entity["BlobURL"].StringValue);
                    returnDic.Add(entity.RowKey, uri);
                }
                return returnDic;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

    }
}