/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 * 
 * 
 * This is the c# file that controls setting up a user. This class will either create a new user or be
 * used to login a returning user.
 * 
 * Uses azure table storage to save usernames and passwords
 *
 * userExist(string userName) - Checks to see if username already is in the table 
 *
 * createUser(string userName, string password, string reenteredPassword) - Creates a new user with given username and password
 * 
 * getUser(string userName, string password) - Checks if user information is valid, logs user into account. 
 *
*/

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


        //Method returns true or false depending on if a username is already taken.
        //-----------------------------------------userExist(string userName)----------------------------------------
        public bool userExist(string userName)
        {
            //see if user is already in database
            try
            {
                TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>()
                       .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userName));

                return (table.ExecuteQuery(query).Count() > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

       
        //Creates a new user with given username and password.
        //---------------createUser(string userName, string password, string reenteredPassword)----------
        public bool createUser(string userName, string password, string reenteredPassword)
        {
            if (!password.Equals(reenteredPassword))
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


        //Checks if user information is valid, logs user into account.
        //-----------------------getUser(string userName, string password)-----------------------
        public bool getUser(string userName, string password)
        {
            try
            {
                string filter = TableQuery.CombineFilters(
                       TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userName),
                       TableOperators.And,
                       TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, password));

                TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(filter);

                return (table.ExecuteQuery(query).Count() > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}