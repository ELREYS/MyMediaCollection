
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyMediaCollection.Enums;
using MyMediaCollection.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace MyMediaCollection
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private IList<MediaItem> _items { get; set; }
        private IList<MediaItem> _allItems { get; set; }
        private IList<string> _mediums { get; set; }
        private bool _isLoaded;
        public MainWindow()
        {
            this.InitializeComponent();
            ItemList.Loaded += ItemList_Loaded;
            PopulateData();
            ItemFilter.Loaded += ItemFilter_Loaded;

        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ItemFilter.SelectionChanged += ItemFilter_SelectionChanged;
        }
        private void ItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var updatedItems =
                                 (from item in _allItems
                                  where
                                 string.IsNullOrWhiteSpace(ItemFilter.
                                 SelectedValue.ToString()) ||
                                 ItemFilter.SelectedValue.ToString() ==
                                 "All" ||
                                 ItemFilter.SelectedValue.ToString() ==
                                 item.MediaType.ToString()
                                  select item).ToList();
            ItemList.ItemsSource = updatedItems;
        }
        private void ItemFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var filterCombo = (ComboBox)sender;
            PopulateData();
            filterCombo.ItemsSource = _mediums;
            filterCombo.SelectedIndex = 0;
        }
        private void ItemList_Loaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            PopulateData();
            listView.ItemsSource = _items;
        }
        public void PopulateData()
        {
            if (_isLoaded) return;
            _isLoaded = true;
            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favorites",
                MediaType = ItemType.Music,
                MediumInfo = new Medium
                {
                    Id = 1,
                    MediaType = ItemType.Music,
                    Name = "CD"
                }
            };
            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = new Medium
                {
                    Id = 2,
                    MediaType = ItemType.Book,
                    Name = "Book"
                }
            };
            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = new Medium
                {
                    Id = 3,
                    MediaType = ItemType.Video,
                    Name = "Blu Ray"
                }
            };
            _items = new List<MediaItem>
            {
                cd,
                book,
                bluRay
            };
            _allItems = new List<MediaItem>
            {
                cd,
                book,
                bluRay
            };
            _mediums = new List<string>
            {
                "All",
                nameof(ItemType.Book),
                nameof(ItemType.Music),
                nameof(ItemType.Video)
            };
        }

        private async void AddItem_Click()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "My Media Collections",
                Content = "Adding items to the collection is not yet available",
                CloseButtonText = "Ok"
            };
            noWifiDialog.XamlRoot = this.Content.XamlRoot;


            await noWifiDialog.ShowAsync();
        }
    }
}
