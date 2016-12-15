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
        }
        //methods
        /// <summary>
        /// Creates an NPC from the stats generated on page
        /// </summary>
        /// <returns>NPC</returns>
        private NPC createNPC()
        {
            int str, dex, cont, intell, wis, charisma;
            NPC npcToReturn;
            int.TryParse(strBox.Text, out str);
            int.TryParse(dexBox.Text, out dex);
            int.TryParse(contBox.Text, out cont);
            int.TryParse(intBox.Text, out intell);
            int.TryParse(wisBox.Text, out wis);
            int.TryParse(charBox.Text, out charisma);
            //create npc with basic stats first
            npcToReturn = new NPC(str, dex, cont, intell, wis, charisma, nameBox.Text);
            //set additional stats for npc
            setType(npcToReturn);
            setRace(npcToReturn);
            setAlignment(npcToReturn);
            return npcToReturn;
        }
        private void setType(NPC npc)
        {
            TextBlock type = (TextBlock)typeBox.SelectedItem;
            npc.Type = type.Text;
        }
        private void setRace(NPC npc)
        {
            TextBlock race = (TextBlock)raceBox.SelectedItem;
            npc.Race = race.Text;
        }
        private void setAlignment(NPC npc)
        {
            TextBlock align = (TextBlock)alignBox.SelectedItem;
            npc.Alignment = align.Text;

        }
        /// <summary>
        /// Generates the values to place into the text boxes
        /// </summary>
        private void generateStats()
        {
            setRange();
            strBox.Text = rng.Next(range.min, range.max).ToString();
            dexBox.Text = rng.Next(range.min, range.max).ToString();
            contBox.Text = rng.Next(range.min, range.max).ToString();
            intBox.Text = rng.Next(range.min, range.max).ToString();
            wisBox.Text = rng.Next(range.min, range.max).ToString();
            charBox.Text = rng.Next(range.min, range.max).ToString();
            typeBox.SelectedIndex = rng.Next(0, 3);
            raceBox.SelectedIndex = rng.Next(0, 9);
            alignBox.SelectedIndex = rng.Next(0, 3);
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
            minBox.SelectedIndex = range.min;
            maxBox.SelectedIndex = range.max;
        }
        /// <summary>
        /// Adds NPC to the npcList
        /// </summary>
        private void addToList()
        {
            npcList.Items.Add(createNPC());
            //sets the selected index to the newly created NPC
            npcList.SelectedIndex = (npcList.Items.Count) - 1;
        }
        private async void displayAlert()
        {
            var dialog = new MessageDialog("Please Enter A Name");
            await dialog.ShowAsync();
        }
        
        //EVENTS
        private void addToList_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "")
            {
                displayAlert();
            }
            else
            {
                addToList();
                this.Frame.GoBack();
            }
        }
      
        private void genButton_Click(object sender, RoutedEventArgs e)
        {
            generateStats();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            npcList = (ListView)e.Parameter;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

       
    }
}
