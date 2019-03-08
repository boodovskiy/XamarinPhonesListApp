using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinTests
{
    public class Page3 : ContentPage
    {
        Label stackLabel;

        public Page3()
        {
            Title = "Page 3";
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += GoToBack;

            Button goHomeBtn = new Button { Text = "Домой" };
            goHomeBtn.Clicked += GoHome;

            stackLabel = new Label();

            Content = new StackLayout
            {
                Children = {
                    backBtn,
                    goHomeBtn,
                    stackLabel
                }
            };
        }

        protected internal void DisplayStack()
        {
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            stackLabel.Text = "";
            foreach (Page p in navPage.Navigation.NavigationStack)
            {
                stackLabel.Text += p.Title + "\n";
            }
        }

        // Переход обратно на Page2
        private async void GoToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((CommonPage)navPage.CurrentPage).DisplayStack();
        }

        // Переход обратно на первую странцу стека
        private async void GoHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).DisplayStack();
        }
    }
}