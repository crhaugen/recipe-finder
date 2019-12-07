<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMasterPage.Master" AutoEventWireup="true" CodeBehind="SavedRecipes.aspx.cs" Inherits="recipeFinder.SavedRecipes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Saved Recipes </h1>
  
    <%--<asp:Label ID ="RecipeList" runat="server"></asp:Label>
--%>

    <button class="accordion" id ="acc1" type="button">
        <asp:Label ID="Recipe1" runat="server">Recipe 1</asp:Label>
    </button>

    <div class="panel" id ="p1">
        <p>
            <asp:Label ID="Recipe1Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc2" type="button">
        <asp:Label ID="Recipe2" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p2">
        <p>
            <asp:Label ID="Recipe2Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc3" type="button">
        <asp:Label ID="Recipe3" runat="server"></asp:Label>
    </button>
    <div class="panel" id="p3">
            <p>
            <asp:Label ID="Recipe3Info" runat="server"></asp:Label>
        </p>
    </div>

        <button class="accordion" id="acc4" type="button">
        <asp:Label ID="Recipe4" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p4">
        <p>
            <asp:Label ID="Recipe4Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc5" type="button">
        <asp:Label ID="Recipe5" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p5">
        <p>
            <asp:Label ID="Recipe5Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc6" type="button">
        <asp:Label ID="Recipe6" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p6">
            <p>
            <asp:Label ID="Recipe6Info" runat="server"></asp:Label>
        </p>
    </div>

        <button class="accordion" id="acc7"  type="button">
        <asp:Label ID="Recipe7" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p7">
        <p>
            <asp:Label ID="Recipe7Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc8"  type="button">
        <asp:Label ID="Recipe8" runat="server"></asp:Label>
    </button>

    <div class="panel" id="p8">
        <p>
            <asp:Label ID="Recipe8Info" runat="server"></asp:Label>
        </p>
    </div>

    <button class="accordion" id="acc9"  type="button">
        <asp:Label ID="Recipe9" runat="server"></asp:Label>
    </button>

    <div class="panel" id ="p9">
            <p>
            <asp:Label ID="Recipe9Info" runat="server"></asp:Label>
        </p>
    </div>

    <br />
    <br />

    <asp:Label ID="ViewFraction" runat="server">Currently seeing page 1 / 1 Recipes </asp:Label>
    <asp:Button ID="nextRecipes" runat="server" OnClick="Next_Recipes_Click" Text="See more recipes" />
    <script>

        function myFunction() {
            var label = document.getElementById("section1Label");
            label.innerHTML = "This is the function you just created"
        }
    </script>

    <script>
        var acc = document.getElementsByClassName("accordion");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var panel = this.nextElementSibling;
                if (panel.style.display === "block") {
                    panel.style.display = "none";
                } else {
                    panel.style.display = "block";
                }
            });
        }
    </script>

</asp:Content>
