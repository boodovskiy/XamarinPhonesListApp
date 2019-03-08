using NavigationApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinTests.NavigationApp;

namespace XamarinTests
{
    public partial class MainPage : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        protected internal ObservableCollection<Phone> Phones { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Title = "Главная";

            stackLabel = new Label();

            Button toCommonPageBtn = new Button
            {
                Text = "На обычную страницу",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            toCommonPageBtn.Clicked += ToCommonPage;

            Button toModalPageBtn = new Button
            {
                Text = "На модальную страницу",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            toModalPageBtn.Clicked += ToModalPage;

            // Phone List
            Phones = new ObservableCollection<Phone>
            {
                new Phone {Name="iPhone 7", Company="Apple", Price=52000},
                new Phone {Name="Galaxy S8", Company="Samsung", Price=50000},
                new Phone {Name="LG G6", Company="LG", Price=45000},
                new Phone {Name="Huawei P10", Company="Huawei", Price=35000}
            };

            //phonesList.BindingContext = Phones;

            // Navigation Lesson 1 
            Content = new StackLayout { Children = { stackLabel, toCommonPageBtn, toModalPageBtn } };
        }

        private async void ToCommonPage(object sender, EventArgs e)
        {
            CommonPage page = new CommonPage();
            await Navigation.PushAsync(page);
            page.DisplayStack();
        }

        private async void ToModalPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (loaded == false)
            {
                DisplayStack();
                loaded = true;
            }
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

        // обработчик выбора элемента в списке
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Получаем выбранный элемент 
            Phone selectedPhone = args.SelectedItem as Phone;
            if (selectedPhone != null)
            {
                // Снимаем выделение
                phonesList.SelectedItem = null;
                // Переходим на страницу редактирования элемента 
                await Navigation.PushAsync(new PhonePage(selectedPhone));
            }
        }

        // переходим на страницу PhonePage для добавления нового элемента
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PhonePage(null));
        }

        // вспомогательный метод для добавления элемента в список
        protected internal void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

    }
}
