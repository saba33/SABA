using SABA.Core.Abstractions;
using SABA.Services.Abstractiuons.Filtration;

namespace SABA.Services.Implementations.Filtration
{
    public class FilterProductService : IFilterProductService
    {
        private readonly IFilterProductRepository _filterRepo;
        public FilterProductService(IFilterProductRepository filterRepo)
        {
            _filterRepo = filterRepo;
        }



    }
}
