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
        private void displayMods()
        {
            if(npc.statMod("strength") > 0)
                strMod.Text = "+ " + npc.statMod("strength").ToString();
            else
                strMod.Text = npc.statMod("strength").ToString();
            if (npc.statMod("dexterity") > 0)
                dexMod.Text = "+ " + npc.statMod("dexterity").ToString();
            else
                dexMod.Text = npc.statMod("dexterity").ToString();
            if (npc.statMod("constitution") > 0)
                contMod.Text = "+ " + npc.statMod("constitution").ToString();
            else
                contMod.Text = npc.statMod("constitution").ToString();
            if (npc.statMod("intelligence") > 0)
                intMod.Text ="+ " + npc.statMod("intelligence").ToString();
            else
                intMod.Text = npc.statMod("intelligence").ToString();
            if (npc.statMod("wisdom") > 0)
                wisMod.Text ="+ " + npc.statMod("wisdom").ToString();
            else
                wisMod.Text = npc.statMod("wisdom").ToString();
            if (npc.statMod("charisma") > 0)
                charMod.Text = "+ " + npc.statMod("charisma").ToString();
            else
                charMod.Text = npc.statMod("charisma").ToString();
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
                displayMods();
            }
            else
            {
                npcError();
            }
            base.OnNavigatedTo(e);
        }
    }
}
