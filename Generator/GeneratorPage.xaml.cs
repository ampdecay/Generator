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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Generator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneratorPage : Page
    {
        ListView npcList;
        private Random stat = new Random();
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
            return npcToReturn;
        }
        /// <summary>
        /// Sets the Type for the NPC from the selected radio button in the combo box
        /// </summary>
        /// <param name="npc"></param>
        private void setType(NPC npc)
        {
            TextBlock type = (TextBlock)comboBox.SelectedItem;
            npc.Type = type.Text;
        }
        /// <summary>
        /// Clears all Stats for the Generator
        /// </summary>
        private void clearGenStats()
        {
            nameBox.Text = "";
            strBox.Text = "";
            dexBox.Text = "";
            intBox.Text = "";
            contBox.Text = "";
            wisBox.Text = "";
            charBox.Text = "";
            comboBox.SelectedIndex = -1;
        }
        /// <summary>
        /// Generates the values to place into the text boxes
        /// </summary>
        private void generateStats()
        {
            strBox.Text = stat.Next(Range.min, Range.max).ToString();
            dexBox.Text = stat.Next(Range.min, Range.max).ToString();
            contBox.Text = stat.Next(Range.min, Range.max).ToString();
            intBox.Text = stat.Next(Range.min, Range.max).ToString();
            wisBox.Text = stat.Next(Range.min, Range.max).ToString();
            charBox.Text = stat.Next(Range.min, Range.max).ToString();
            comboBox.SelectedIndex = stat.Next(0, 3);
        }
        /// <summary>
        /// Adds NPC to the npcList
        /// </summary>
        private void addToList()
        {
            npcList.Items.Add(createNPC());
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
        private void radioButtonType_Click(object sender, RoutedEventArgs e)
        {
            comboBox.IsDropDownOpen = false;
            comboBox.SelectedItem = sender;
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
