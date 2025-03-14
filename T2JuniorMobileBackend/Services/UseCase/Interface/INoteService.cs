﻿using MauiApp1.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.UseCase.Interface
{
    public interface INoteService
    {
        Task<List<Note>> GetNotesAsync(Guid idOwner);
        Task<List<Note>> GetNewsAsync();
        Task<string> SendNoteAsync(Guid idOwner, string name, string description);
        Task SendNoteAsync(Guid idOwner, string name, string description, Stream mediaFile);
    }
}
