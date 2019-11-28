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
                //Output.Text = "Login failed. Please try again!";
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if(userClient.userExist(newUserName.Text))
            {
                Output.Text = "User already exist with that name!";
            }
            else
            {
                if(userClient.createUser(newUserName.Text, newPassword.Text, reNewPassword.Text))
                {
                    Output.Text = "User created!";
                }
                else
                {
                    Output.Text = "Error with creating user. Please try again!";
                }
            }

        }
    }
}