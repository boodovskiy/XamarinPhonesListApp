using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinTests
{
	public class CommonPage : ContentPage
	{
        Label stackLabel;

        public CommonPage ()
		{
            Title = "Внутренняя страница";

            Button backButton = new Button
            {
                Text = "Назад",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            backButton.Clicked += BackButton_Click;

            Button forwardBtn = new Button { Text = "Вперед" };
            forwardBtn.Clicked += GoToForward;

            stackLabel = new Label();

            Content = new StackLayout { Children = { forwardBtn, backButton, stackLabel } };
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

        // Переход вперед на Page3
        private async void GoToForward(object sender, EventArgs e)
        {
            Page3 page = new Page3();
            await Navigation.PushAsync(page);
            page.DisplayStack();
        }

        private async void BackButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).DisplayStack();
        }
    }
}