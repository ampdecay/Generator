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
    public sealed partial class NPCStatPage : Page
    {
        NPC npc;
        public NPCStatPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        /// <summary>
        /// Displays the NPC stats in the text boxes on the current windows
        /// </summary>
        private void displayStats()
        {
            nameBox.Text = npc.Name;
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
            //data is cached so go ahead and clear it
            clearStats();
            var dialog = new MessageDialog("Please Go Back and Select an NPC");
            await dialog.ShowAsync();
        }
        /// <summary>
        /// Clears all text boxes by inserting empty string
        /// </summary>
        private void clearStats()
        {
            nameBox.Text = "";
            strBox.Text = "";
            dexBox.Text = "";
            contBox.Text = "";
            wisBox.Text = "";
            intBox.Text = "";
            charBox.Text = "";
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
