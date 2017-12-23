using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
using DataBindingExample.ViewModel;
using System.Reactive.Linq;

namespace DataBindingExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainViewModel;

            CreateButtons();


            //var wnd = new SimpleWindow();
            //wnd.Show();

            var api = new Api(_mainViewModel.PersonsCollection);

            var textchanges = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
                h => searchBox.TextChanged += h,
                h => searchBox.TextChanged -= h
                ).Select(x => ((TextBox)x.Sender).Text);

            textchanges
                .Throttle(TimeSpan.FromMilliseconds(300)) // result on threadpool
                .Select(api.Search)
                .Switch()
                .ObserveOnDispatcher() // send back to dispatcher
                .Subscribe(OnSearchResult);

            api.Search("").Subscribe(OnSearchResult);

        }

        private void OnSearchResult(List<string> list)
        {
           // listbox.ItemsSource = list;
        }


        private Button[] CreateButtons()
        {
            StackPanel SearchingPanel = LayoutRoot.FindName("SearchingPanel") as StackPanel;
            Button[] buttons = new Button[33];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Width = 20d;
                buttons[i].Margin = new Thickness(1d); 
                buttons[i].Content = ((char)(i + 'А')).ToString();
                buttons[i].Name = "button" + buttons[i].Content;
                buttons[i].Command = _mainViewModel.FilterCommand;
                buttons[i].CommandParameter = buttons[i].Content;
                if (i == 32)
                {
                    buttons[i].Content = "ALL";
                    buttons[i].CommandParameter = "";
                    buttons[i].Width = 30d;
                }
                SearchingPanel.Children.Add(buttons[i]); 
            }

            return buttons;
        }
    }
}
