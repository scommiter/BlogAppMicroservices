﻿namespace Chat.Api.Dto
{
    public class MessageParams
    {
        public string CurrentUsername { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
