using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvidenceWPF_Databaze
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ClassRoom> SchClassRooms;
        List<Item> SchItems;
        int SelectedClassRoom = 0;
        int SelectedItem = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            
        }
        public async void LoadData()
        {
            SchClassRooms = await Table.Database.GetItemsAsync<ClassRoom>();
            SchItems = await Table.Database.GetItemsAsync<Item>();
            PrintClassRooms();
            PrintItems();
        }
        public void PrintClassRooms()
        {
            ClassRooms.Children.Clear();
            for(int i =0;i < SchClassRooms.Count(); i++)
            {
                Button button = new Button();
                button.Content = SchClassRooms[i].Name;
                button.Tag = i;
                button.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click_ClassRoom_Select));
                ClassRooms.Children.Add(button);
            }
        }
        public void PrintItems()
        {
            Items.Children.Clear();
            for (int i = 0; i < SchItems.Count(); i++)
            {
                if(SchItems[i].IdClassRoom == SchClassRooms[SelectedClassRoom].Id)
                {
                    Button button = new Button();
                    button.Content = SchItems[i].Name + " | " + SchItems[i].Cost.ToString()+"Kč";
                    button.Tag = i;
                    button.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click_Item_Select));
                    Items.Children.Add(button);
                }
                
            }
        }







        private void Button_Click_ClassRoom_Select(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SelectedClassRoom = (int)button.Tag;
            LoadData();
        }
        private void Button_Click_Item_Select(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SelectedItem = (int)button.Tag;
            LoadData();
        }

        private async void Button_Click_Add_ClassRoom(object sender, RoutedEventArgs e)
        {
            ClassRoom SchClassRoom = new ClassRoom();
            SchClassRoom.Name = TextBoxClass.Text;
            await Table.Database.SaveItemAsync<ClassRoom>(SchClassRoom);

            LoadData();
        }
        private async void Button_Click_Add_Item(object sender, RoutedEventArgs e)
        {
            Item SchItem = new Item();
            SchItem.Name = TextBoxItemsName.Text;
            int value;

            if (int.TryParse(TextBoxItemsCost.Text, out value))
            {
            }
            else
            {
                return;
            }
            
            SchItem.Cost = value;
            SchItem.IdClassRoom = SchClassRooms[SelectedClassRoom].Id;

            await Table.Database.SaveItemAsync<Item>(SchItem);

            LoadData();
        }
        private async void Button_Click_Remove_ClassRoom(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < SchItems.Count(); i++)
            {
                if(SchClassRooms[SelectedClassRoom].Id == SchItems[i].IdClassRoom)
                {
                    SelectedItem = i;
                    await RemoveItem();
                }
            }
            await Table.Database.DeleteItemAsync<ClassRoom>(SchClassRooms[SelectedClassRoom]);
            LoadData();
        }
        private async void Button_Click_Remove_Item(object sender, RoutedEventArgs e)
        {
            await RemoveItem();
            LoadData();
        }

        async Task RemoveItem()
        {
            await Table.Database.DeleteItemAsync<Item>(SchItems[SelectedItem]);
            
        }
        private async void Button_Click_Update_Item(object sender, RoutedEventArgs e)
        {
            Item item = new Item();

            int value;

            if (int.TryParse(TextBoxItemsCost.Text, out value))
            {
            }
            else
            {
                return;
            }


            item.Cost = value;
            item.Id = SchItems[SelectedItem].Id;
            item.IdClassRoom = SchItems[SelectedItem].IdClassRoom;
            item.Name = TextBoxItemsName.Text;

            await Table.Database.SaveItemAsync<Item>(item);
            LoadData();

        }
        private async void Button_Click_Update_Class(object sender, RoutedEventArgs e)
        {
            ClassRoom SchClass = new ClassRoom();
            SchClass.Id = SchClassRooms[SelectedClassRoom].Id;
            SchClass.Name = TextBoxClass.Text;

            await Table.Database.SaveItemAsync<ClassRoom>(SchClass);

            LoadData();
        }
    }
}
