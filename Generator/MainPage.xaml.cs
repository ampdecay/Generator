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
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;


namespace Generator
{
    
    /// <summary>
    /// Main interface
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        //methods for main page
        /// <summary>
        /// Saves each object into a text file 
        /// in the form of "str,dex,int,name"
        /// </summary>
        private async void saveToFile()
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("NPC List", new List<string>() { ".npc" });
            savePicker.SuggestedFileName = "NPCs";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, string.Empty);
                foreach (NPC NPC in npcList.Items)
                {
                    await FileIO.AppendTextAsync(file, NPC.npcToText());
                    await FileIO.AppendTextAsync(file, Environment.NewLine);
                }
            }
        }
        /// <summary>
        /// Reads From a Text File in the form of
        /// "str,dex,int,name"
        /// </summary>
        private async void loadFromFile()
        {
            npcList.Items.Clear();
            string[] splitLine;
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".npc");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var readFile = await Windows.Storage.FileIO.ReadLinesAsync(file);
                foreach (var line in readFile)
                {
                    splitLine = line.Split(',');
                    if (splitLine.Length == 11)
                        npcFromFile(splitLine);
                }
                //SELECT FIRST ITEM IN LIST
                npcList.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// helper function for loading from files
        /// </summary>
        /// <param name="splitLine"></param>
        private void npcFromFile(string[] splitLine)
        {
            int str, dex, intell, cont, wis, charisma;
            string name, type, race, alignment;
            NPC npc;
            int.TryParse(splitLine[1], out str);
            int.TryParse(splitLine[2], out dex);
            int.TryParse(splitLine[3], out cont);
            int.TryParse(splitLine[4], out intell);
            int.TryParse(splitLine[5], out wis);
            int.TryParse(splitLine[6], out charisma);
            type = splitLine[7];
            race = splitLine[8];
            alignment = splitLine[9];
            name = splitLine[10];
            npc = new NPC(str, dex, cont, intell, wis, charisma, name);
            npc.Type = type;
            npc.Race = race;
            npc.Alignment = alignment;
            npcList.Items.Add(npc);
        }
        
        //EVENTS
        private void loadText_Click(object sender, RoutedEventArgs e)
        {
            loadFromFile();
        }
        private void saveText_Click(object sender, RoutedEventArgs e)
        {
           saveToFile();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            npcList.Items.Remove(npcList.SelectedItem);
            if(npcList.Items.Count > 0)
                npcList.SelectedIndex = 0;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveToFile();
        }
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            loadFromFile();
        }
        private void clearALL_Click(object sender, RoutedEventArgs e)
        {
            npcList.Items.Clear();
        }
        private void viewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NPCStatPage),npcList.SelectedItem);
        }
        private void createNewButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GeneratorPage), npcList);
        }
        
    }
}
