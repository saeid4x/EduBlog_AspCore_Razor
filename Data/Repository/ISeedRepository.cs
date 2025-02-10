using SokanAcademy.Models;

namespace SokanAcademy.Data.Repository
{
    public interface ISeedRepository
    {
        Task CreateCategory(Category cat);
    }
}
