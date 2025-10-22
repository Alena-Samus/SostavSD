using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IChapterService
    {
        Task <List<ChapterModel>> GetChaptersByCountryAsync (string country);
    }
}
