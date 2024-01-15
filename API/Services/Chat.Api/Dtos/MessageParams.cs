namespace Chat.Api.Dto
{
    public class MessageParams
    {
        public string CurrentUsername { get; set; }
        public string Receiver { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
