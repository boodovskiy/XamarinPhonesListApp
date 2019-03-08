using NavigationApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTests.NavigationApp
{
	public partial class PhonePage : ContentPage
	{
        bool edited = true; // флаг редактирования
        public Phone Phone { get; set; }

        public PhonePage (Phone phone)
		{
			InitializeComponent ();

            Phone = phone;
            if (phone == null)
            {
                Phone = new Phone();
                edited = false;
            }
            this.BindingContext = Phone;
        }
        async void SavePhone(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            // если добавление
            if (edited == false)
            {
                // находим в стеке предпоследнюю страницу - то есть MainPage
                NavigationPage navPage = (NavigationPage)Application.Current.MainPage;
                IReadOnlyList<Page> navStack = navPage.Navigation.NavigationStack;
                MainPage homePage = navStack[navPage.Navigation.NavigationStack.Count - 1] as MainPage;

                if (homePage != null)
                {
                    homePage.AddPhone(Phone);
                }
            }
        }
    }
}