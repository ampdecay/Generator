using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


namespace Generator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneratorPage : Page
    {
        ListView npcList;
        private Random rng = new Random();
        private Range range = new Range();
        public GeneratorPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Creates an NPC from the stats generated on page
        /// </summary>
        /// <returns>NPC</returns>
        private NPC CreateNPC()
        {
            return new NPC
            {
                Name = nameBox.Text,
                Strength = int.Parse(strBox.Text),
                Dexterity = int.Parse(dexBox.Text),
                Intelligence = int.Parse(intBox.Text),
                Constitution = int.Parse(contBox.Text),
                Wisdom = int.Parse(wisBox.Text),
                Charisma = int.Parse(charBox.Text),
                Type = new Func<string>(() => { TextBlock type = (TextBlock)typeBox.SelectedItem; return type.Text; })(),
                Race = new Func<string>(() => { TextBlock race = (TextBlock)raceBox.SelectedItem; return race.Text; })(),
                Alignment = new Func<string>(() => { TextBlock align = (TextBlock)alignBox.SelectedItem; return align.Text; })()
            };
        }

        /// <summary>
        /// Generates the values to place into the text boxes
        /// </summary>
        private void GenerateStats()
        {
            if (randRange.IsChecked == true)
                RandomizeRange();
            strBox.Text = rng.Next(range.min, range.max).ToString();
            dexBox.Text = rng.Next(range.min, range.max).ToString();
            contBox.Text = rng.Next(range.min, range.max).ToString();
            intBox.Text = rng.Next(range.min, range.max).ToString();
            wisBox.Text = rng.Next(range.min, range.max).ToString();
            charBox.Text = rng.Next(range.min, range.max).ToString();
            typeBox.SelectedIndex = rng.Next(0, 4);
            raceBox.SelectedIndex = rng.Next(0, 9);
            alignBox.SelectedIndex = rng.Next(0, 3);
            string firstName = names.firstNames[rng.Next(0, 119)];
            string lastName = names.lastNames[rng.Next(0, 119)];
            nameBox.Text = firstName + " " + lastName;

        }
        private void setRange()
        {
            TextBlock min = (TextBlock)minBox.SelectedItem;
            TextBlock max = (TextBlock)maxBox.SelectedItem;
            if (min != null && max != null)
            {
                int.TryParse(min.Text, out range.min);
                int.TryParse(max.Text, out range.max);
                if (range.min > range.max)
                    swap();
                //add 1 to max because it is non inclusive
                range.max += 1;
            }
        }
        private void swap()
        {
            int temp;
            temp = range.min;
            range.min = range.max;
            range.max = temp;
            minBox.SelectedIndex = range.min - 1;
            maxBox.SelectedIndex = range.max - 1;
        }
        /// <summary>
        /// Adds NPC to the npcList
        /// </summary>
        private void AddToList()
        {
            NPC npc = CreateNPC();
            npcList.Items.Add(npc);
            //sets the selected index to the newly created NPC
            npcList.SelectedIndex = (npcList.Items.Count) - 1;
        }
        private async void displayAlert()
        {
            var dialog = new MessageDialog("Please Enter A Name");
            await dialog.ShowAsync();
        }
        private void ClearPage()
        {
            strBox.Text = "0";
            dexBox.Text = "0";
            contBox.Text = "0";
            intBox.Text = "0";
            wisBox.Text = "0";
            charBox.Text = "0";
            nameBox.Text = "";
            typeBox.SelectedIndex = 0;
            raceBox.SelectedIndex = 0;
            alignBox.SelectedIndex = 0;
            minBox.SelectedIndex = 0;
            maxBox.SelectedIndex = 0;
        }

        //EVENTS
        private void addToList_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(nameBox.Text))
                displayAlert();
            else
            {
                AddToList();
                this.Frame.GoBack();
            }
        }

        private void genButton_Click(object sender, RoutedEventArgs e)
        {
            setRange();
            GenerateStats();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                npcList = (ListView)e.Parameter;
            ClearPage();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ClearPage();
        }

        private void massGen_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 300; i++)
            {
                GenerateStats();
                NPC temp = CreateNPC();
                temp.NPC_ID = i + 1;
                npcList.Items.Add(temp);
                ClearPage();
            }
            npcList.SelectedIndex = (npcList.Items.Count) - 1;
            this.Frame.GoBack();

        }

        private void RandomizeRange()
        {
            range.min = rng.Next(1, 21);
            range.max = rng.Next(1, 21);
            if (range.min > range.max)
                swap();
            else
            {
                minBox.SelectedIndex = range.min - 1;
                maxBox.SelectedIndex = range.max - 1;
            }
        }
    }
}
