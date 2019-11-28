using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Table;

namespace recipeFinder
{
    public class UserHandler
    {
        static string accountname = "css436program4storage";
        static string accountkey = "YOUR KEY GOES HERE";
        static string tablestoragename = "userdata";

        static StorageCredentials credentials = new StorageCredentials(accountname, accountkey);
        static CloudStorageAccount storageaccount = new CloudStorageAccount(credentials, useHttps: true);

        static CloudTableClient cloudtableclient = storageaccount.CreateCloudTableClient();
        CloudTable table = cloudtableclient.GetTableReference(tablestoragename);

        public bool userExist(string username)
        {
            //see if user is already in database
            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>()
                   .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username));

            return (table.ExecuteQuery(query).Count() > 0) ? true : false;
        }

        public bool createUser(string username, string password, string reenteredpassword)
        {
            if (!password.Equals(reenteredpassword))
            {
                return false;
            }

            DynamicTableEntity tableentity;

            try
            {
                //add the entity into the table
                var dict = new Dictionary<string, EntityProperty> { };
                tableentity = new DynamicTableEntity(username, password, "*", dict);
                table.Execute(TableOperation.InsertOrReplace(tableentity));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }

        public bool getUser(string username, string password)
        {

            string filter = TableQuery.CombineFilters(
                   TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username),
                   TableOperators.And,
                   TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, password));

            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(filter);

            return (table.ExecuteQuery(query).Count() > 0) ? true : false;
        }
    }
}