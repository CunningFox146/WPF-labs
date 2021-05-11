using lab6_7.Commands;
using lab6_7.Models;
using lab6_7.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;

namespace lab6_7.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Команды

        public ICommand CloseAplicationCommand { get; }
        private bool OnCanCloseAplicationCommandExecuted(object p) => true;
        private void OnCloseAplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        
        public ICommand ChangeImageCommand { get; }
        private bool OnCanChangeImageCommand(object p) => SelectedItem != null;
        private void OnChangeImageCommand(object p)
        {
            if (SelectedItem == null) return;

            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";

            if (dlg.ShowDialog() == true)
            {
                SelectedItem.ImagePath = dlg.FileName;
                OnPropertyChanged("SelectedItem");
                //SelectedImageTextBlock.Text = System.IO.Path.GetFileName(dlg.FileName);
                //ImgPreview.Source = selectedImg;
            }
        }
        public ICommand RemoveItemCommand { get; }
        private bool OnCanRemoveItemCommand(object p) => SelectedItem != null;
        private void OnRemoveItemCommand(object p)
        {
            if (SelectedItem == null) return;

            Items.Remove(SelectedItem);
            SelectedItem = null;
            OnPropertyChanged("SelectedItem");   
        }
        
        public ICommand SetImageCommand { get; }
        private bool OnCanSetImageCommand(object p) => true;
        private void OnSetImageCommand(object p)
        {
            if (ItemToCreate == null) return;

            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";

            if (dlg.ShowDialog() == true)
            {
                ItemToCreate.ImagePath = dlg.FileName;
                OnPropertyChanged("ItemToCreate");
                //SelectedImageTextBlock.Text = System.IO.Path.GetFileName(dlg.FileName);
                //ImgPreview.Source = selectedImg;
            }
        }
        
        public ICommand AddItemCommand { get; }
        private bool OnCanAddItemCommand(object p) => ItemToCreate.IsValid();
        private void OnAddItemCommand(object p)
        {
            Items.Add(ItemToCreate);
            ItemToCreate = new Item();
            OnPropertyChanged("ItemToCreate");
        }

        public ICommand LoadJsonCommand { get; }
        private bool OnCanLoadJsonCommand(object p) => true;
        private void OnLoadJsonCommand(object p)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Json (*.json)|*.json";
            

            if (dlg.ShowDialog() == true)
            {
                var (LoadedItems, _) = ItemSerializer.DeserializeFromFile<Item>(dlg.FileName);
                Items = new ObservableCollection<Item>(LoadedItems);
                OnPropertyChanged("Items");
            }
        }

        public ICommand SaveJsonCommand { get; }
        private bool OnCanSaveJsonCommand(object p) => true;
        private void OnSaveJsonCommand(object p)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Json (*.json)|*.json";
            dlg.FileName = "Save.json";

            if (dlg.ShowDialog() == true)
            {
                string _ = ItemSerializer.Serialize<Item>(Items, dlg.FileName);
            }
        }

        public ICommand ChangeLanguageCommand { get; }
        private bool OnCanChangeLanguageCommand(object p) => true;
        private void OnChangeLanguageCommand(object p)
        {
            Uri uri;
            ResourceDictionary dict;
            if (CurrentLanguage == "English")
            {
                uri = new Uri("Resources/Russian.xaml", UriKind.Relative);
                CurrentLanguage = "Russian";
            }
            else
            {
                uri = new Uri("Resources/English.xaml", UriKind.Relative);
                CurrentLanguage = "English";
            }

            dict = Application.LoadComponent(uri) as ResourceDictionary;

            Application.Current.Resources.Clear();

            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        #endregion

        public string CurrentLanguage { get; set; } = "Russian";

        public ObservableCollection<Item> Items { get; set; }
        private Item selectedItem;
        public Item SelectedItem { get => selectedItem; set => Set(ref selectedItem, value); }

        private Item itemToCreate;
        public Item ItemToCreate { get => itemToCreate; set => Set(ref itemToCreate, value); }

        public MainWindowViewModel()
        {
            #region Команды

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, OnCanCloseAplicationCommandExecuted);
            ChangeImageCommand = new LambdaCommand(OnChangeImageCommand, OnCanChangeImageCommand);
            SetImageCommand = new LambdaCommand(OnSetImageCommand, OnCanSetImageCommand);
            AddItemCommand = new LambdaCommand(OnAddItemCommand, OnCanAddItemCommand);
            RemoveItemCommand = new LambdaCommand(OnRemoveItemCommand, OnCanRemoveItemCommand);
            LoadJsonCommand = new LambdaCommand(OnLoadJsonCommand, OnCanLoadJsonCommand);
            SaveJsonCommand = new LambdaCommand(OnSaveJsonCommand, OnCanSaveJsonCommand);
            ChangeLanguageCommand = new LambdaCommand(OnChangeLanguageCommand, OnCanChangeLanguageCommand);

            #endregion

            ItemToCreate = new Item();

            var item_idx = 1;
            var items = Enumerable.Range(1, 10).Select(i => new Item
            {
                Name = $"Board game {item_idx}",
                SmallDescription = $"Very cool game with name {item_idx}",
                Description = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras dictum lectus nec mollis sollicitudin. Quisque quis nisl posuere, lacinia justo id, iaculis sapien. Aliquam pulvinar euismod augue, vel elementum ante sagittis eu. Nullam vitae neque laoreet, scelerisque ipsum placerat, facilisis libero. Lorem ipsum dolor sit amet, consectetur adipiscing eli{item_idx}",
                Rating = item_idx,
                Price = item_idx * 10,
                Quantity = item_idx * 15,
                ItemCategory = (ItemCategory)(item_idx++ % 3),
                ImagePath = null,
            });

            Items = new ObservableCollection<Item>(items);

        }
    }
}
