﻿namespace T2JuniorAPI.Entities
{
    public class NoteLike : BaseCommonProperties
    {
        public Guid NoteId { get; set; }
        public Guid UserId { get; set; }

        public virtual Note Note { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
