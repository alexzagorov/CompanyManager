namespace TaskMe.Services.Data.Picture
{
    using System.Threading.Tasks;

    public interface IPuctureService
    {
        Task<string> AddPictureAsync(string url);
    }
}
