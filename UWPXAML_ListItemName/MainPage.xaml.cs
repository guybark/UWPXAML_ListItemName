using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace UWPXAML_ListItemName
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Bird> items { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            // Populate the list with some test data.
            items = new ObservableCollection<Bird>();
            items.Add(new Bird() { 
                Name = "House Sparrow", 
                Habitat = "Cities, suburbs, farms", 
                Voice = "Repeated series of chirrup sounds" });
            items.Add(new Bird() { 
                Name = "Golden-crowned Sparrow", 
                Habitat = "Brushy places, including neighborhoods", 
                Voice = "Series of long, raspy, whistled notes" });
            items.Add(new Bird() { 
                Name = "Song Sparrow", 
                Habitat = "Found throughout Puget Sound Region, up to mountain passes", 
                Voice = "Song begins with several clear notes, followed by lower note, jumbled trill" });
            BirdList.ItemsSource = items;
        }

        private void BirdList_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Delete)
            {
                var listBoxItem = e.OriginalSource as ListBoxItem;
                DeleteBirdItem(listBoxItem);
            }
        }

        private void DeleteBirdItemButton_Click(object sender, RoutedEventArgs e)
        {
            var element = sender as DependencyObject;

            while (element.GetType() != typeof(ListBoxItem))
            {
                element = VisualTreeHelper.GetParent(element);
                if (element == null)
                {
                    return;
                }
            }

            DeleteBirdItem(element as ListBoxItem);
        }

        private void DeleteBirdItem(ListBoxItem listBoxItem)
        {
            var bird = ((FrameworkElement)listBoxItem).DataContext as Bird;
            items.Remove(bird);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }

    public class Bird
    {
        public string Name { get; set; }
        public string Habitat { get; set; }
        public string Voice { get; set; }
    }

    public class BirdListBox : ListBox
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var resourceLoader = ResourceLoader.GetForCurrentView();
            
            AutomationProperties.SetHelpText(
                element, 
                resourceLoader.GetString("BirdItemHelp"));
        }
    }
}
