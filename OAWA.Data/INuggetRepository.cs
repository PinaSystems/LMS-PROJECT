using System.Threading.Tasks;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public interface INuggetRepository
    {
        Task<PagedList<NuggetDto>> GetNuggets(NuggetParams nuggetParams);
    }
}