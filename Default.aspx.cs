/*
 * Chyanne Haugen and Kathleen Guinee
 * CSS 436 Program 6
 * Last edited on 12/07/2019
 * 
 * 
 * This is the c# file that controls the buttons and functionality of the default/log in page. 
From this page, users can login or create an account

The buttons are available on this page:

Login -- queries the Azure Storage table that stores usernames and passwords. 
        If the user information is not found, returns an error message

Create-Account -- Users can create a new account. Account information is stored in an Azure storage table.
                The UserHandler class handles all of the specifics of 


Query -- Queries the Azure table storage based on the items entered for the first name and the last name.

Assumptions for the use of this webpage are as follows: 

Usernames and passwords are assumed to be any characters that are viable row and partition keys in Azure Storage tables. 

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace recipeFinder
{
    public partial class _Default : Page
    {
        UserHandler userClient = new UserHandler();
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

//This method controls the Login button. It uses the UserHandler instance to get user information from the
//Azure storage table. It also uses the session information to verify that the user is allowed to access
//other pages if they have a valid login.
//----------------------Login_Click(object sender, EventArgs e)----------------------------------------
        protected void Login_Click(object sender, EventArgs e)
        {
            Output.Text = userClient.getUser(UserName.Text, Password.Text).ToString();
            if (userClient.getUser(UserName.Text, Password.Text))
            {
                Session["User"] = UserName.Text;
                Response.Redirect("homepage.aspx", true);
            }
            else
            {
                Output.Text = "Login failed. Please try again!";
            }
        }//end Login_Click()

//This function controls the create user button. The User handler is called, which adds the given username and password
//to the Azure storage table for future use. 
//----------------------CreateUser_Click(object sender, EventArgs e)----------------------------------------
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (userClient.userExist(newUserName.Text))
            {
                Output.Text = "User already exist with that name!";
            }
            else
            {
                if (userClient.createUser(newUserName.Text, newPassword.Text, reNewPassword.Text))
                {
                    Output.Text = "User created!";
                }
                else
                {
                    Output.Text = "Error with creating user. Please try again!";
                }
            }

        }//end CreateUser_Click()
    }
}