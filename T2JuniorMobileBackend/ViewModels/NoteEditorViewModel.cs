using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{ 
    public class NoteEditorViewModel : BindableObject, INotifyPropertyChanged
    {
        private readonly INoteService _noteService;

        private string _name;
        private string _description;
        private Stream _mediaFile;
        private Guid _idOwner;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public NoteEditorViewModel(INoteService noteService, Guid idOwner)
        {
            _noteService = noteService;
            _idOwner = idOwner;
        }

        public async Task SendPost()
        {
            if(_mediaFile == null)
            {
                await _noteService.SendNoteAsync(_idOwner, Name, Description);
            }
            else
            {
                await _noteService.SendNoteAsync(_idOwner, Name, Description, _mediaFile);
            }
        }

        public async Task SetMediaFile()
        {
            try
            {

                var chosenImage = await MediaPicker.PickPhotoAsync();

                if (chosenImage != null) 

                {
                    var stream = await chosenImage.OpenReadAsync();
                    _mediaFile = stream;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] {ex.Message}");
            }
        }
    }
}
