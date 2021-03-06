﻿using FirstXamarin.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirstXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (File.Exists(_fileName))
            {
                editor.Text = File.ReadAllText(_fileName);
            }
        }
        int count = 0;
        private void Button_Clicked(object sender, EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"You Click Me {count} Times";
        }
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");
        async void Button_SaveNote(object sender,EventArgs e)
        {
            var note = (NoteObject)BindingContext;

            if (string.IsNullOrWhiteSpace(note.FileName))
            {
                // Save
                var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update
                File.WriteAllText(note.FileName, note.Text);
            }

            await Navigation.PopAsync();
        }
        async void Button_DeleteNote(object sender, EventArgs e)
        {
            var note = (NoteObject)BindingContext;

            if (File.Exists(note.FileName))
            {
                File.Delete(note.FileName);
            }

            await Navigation.PopAsync();
        }
    }
}
