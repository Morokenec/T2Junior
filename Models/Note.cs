using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DataModel
{
    public class Note
    {
        public int IdNote { get; set; }

        public int IdWall { get; set; } = 0;

        public int IdStatus { get; set; } = 0;

        public string Name { get; set; } = "ТитанКофе";

        public string CreationDateTime { get; set; } = "01/01/1974";

        public string Description { get; set; } = "";

        public string NoteMediaSource { get; set; } = "news_media_holder.svg"; //добавить условия

        public string Text { get; set; } = "Клуб любителей кофе";

        public int IdRepost { get; set; } = 0;

        public int LikeCount { get; set; } = 0;
    }
}
