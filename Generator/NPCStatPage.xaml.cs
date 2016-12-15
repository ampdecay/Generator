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
    /// Paged used to display NPC stats
    /// </summary>
    public sealed partial class NPCStatPage : Page
    {
        NPC npc;
        public NPCStatPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Displays the NPC stats in the text boxes on the current windows
        /// </summary>
        private void displayStats()
        {
            nameBox.Text = npc.Name;
            typeBox.Text = npc.Type;
            raceBox.Text = npc.Race;
            alignBox.Text = npc.Alignment;
            strBox.Text = npc.Strength.ToString();
            dexBox.Text = npc.Dexterity.ToString();
            contBox.Text = npc.Constitution.ToString();
            wisBox.Text = npc.Wisdom.ToString();
            intBox.Text = npc.Intelligence.ToString();
            charBox.Text = npc.Charisma.ToString();
        }
        /// <summary>
        /// Displays error if no NPC selected
        /// </summary>
        private async void npcError()
        {
            var dialog = new MessageDialog("Please Go Back and Select an NPC");
            await dialog.ShowAsync();
        }
        //EVENTS
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                npc = (NPC)e.Parameter;
                displayStats();
            }
            else
            {
                npcError();
            }
            base.OnNavigatedTo(e);
        }
    }
}
