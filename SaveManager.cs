/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 *
 * This is the c# file that controls save recipes data for the user.
 *
 * Uses azure blob storage to save the users recipes.
 * 
 * getBlobContents(string recipeTitle) - This method will return a recipes as a string, recipes is found by given title
 * 
 * getSavedRecipes(string username) - This method returns all the recipe that a given user has saved
 * 
 * addRecipe(string recipeName, string recipe, string username) - This method add the recipe as a text file into blob storage
 * 
*/

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
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["ConnectionString"]);
        static CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        static string tableStorageName = "savedrecipes";
        static string blobStorageName = "recipecontainer";

        CloudTable table = cloudTableClient.GetTableReference(tableStorageName);
        CloudBlobContainer blobContainer = blobClient.GetContainerReference(blobStorageName);

        //This method add the recipe as a text file into blob storage.
        //------------------ addRecipe(string recipeName, string recipe, string username)----------------
        public bool addRecipe(string recipeName, string recipe, string username)
        {
            CloudBlockBlob blob;

            try
            {
                string fileName = recipeName.Replace(@"/", "") + ".txt";
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

        //This method returns all the recipe that a given user has saved.
        //------------------getSavedRecipes(string username)----------------
        public List<string> getSavedRecipes(string username)
        {
            try
            {
                var query = from entity in table.CreateQuery<DynamicTableEntity>()
                            where entity.PartitionKey.Equals(username)
                            select entity;

                List<string> returnList = new List<string>();
               // Dictionary<string, Uri> returnDic = new Dictionary<string, Uri>();


                foreach (DynamicTableEntity entity in query)
                {
                    returnList.Add(entity.RowKey);
                    //Uri uri = new Uri(entity["BlobURL"].StringValue);
                    //returnDic.Add(entity.RowKey, uri);
                }
                return returnList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //https://social.msdn.microsoft.com/Forums/azure/en-US/d640e0b4-3800-47b4-8772-28fd33e2ce17/how-to-read-and-write-a-files-from-blob-storage?forum=windowsazuredata
        //This method will return a recipes as a string, recipe is found by given title.
        //------------------getBlobContents(string recipeTitle)----------------
        public string getBlobContents(string recipeTitle)
        {
            CloudBlob blob = blobContainer.GetBlobReference(recipeTitle + ".txt");
            string content;

            if (!blob.Exists())
            {
                content = "";
            }
            else
            {
                using (StreamReader reader = new StreamReader(blob.OpenRead()))
                {
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }

    }
}