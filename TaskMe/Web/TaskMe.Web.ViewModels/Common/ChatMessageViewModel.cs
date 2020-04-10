namespace TaskMe.Web.ViewModels.Common
{
    using System;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class ChatMessageViewModel : IMapFrom<Message>
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string WriterFirstName { get; set; }

        public string WriterLastName { get; set; }

        public string WriterPictureUrl { get; set; }

        public string WriterNames { get => $"{this.WriterFirstName} {this.WriterLastName}"; }

        public string CreatedOnString { get => this.CreatedOn.ToString("O"); }
    }
}
