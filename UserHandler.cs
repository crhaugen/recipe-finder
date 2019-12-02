using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;


namespace recipeFinder
{
    public class UserHandler
    {

        static string tableStorageName = "userdata";

        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["ConnectionString"]);

        static CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
        CloudTable table = cloudTableClient.GetTableReference(tableStorageName);

        public bool userExist(string userName)
        {  
            //see if user is already in database
            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>()
                   .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userName));

            return (table.ExecuteQuery(query).Count() > 0) ? true : false;
        }

        public bool createUser(string userName, string password, string reenteredPassword)
        {
            if(!password.Equals(reenteredPassword))
            {
                return false;
            }

            DynamicTableEntity tableEntity;

            try
            {
                //add the entity into the table
                var dict = new Dictionary<string, EntityProperty> { };
                tableEntity = new DynamicTableEntity(userName, password, "*", dict);
                table.Execute(TableOperation.InsertOrReplace(tableEntity));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }

        public bool getUser(string userName, string password)
        {

            string filter = TableQuery.CombineFilters(
                   TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userName),
                   TableOperators.And,
                   TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, password));

            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(filter);
            
            return (table.ExecuteQuery(query).Count() > 0) ? true : false;
        }
    }
}
