using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.ViewModels.Catalog.TimeOffTypes.Dtos
{
    public class GetTimeOffTypePagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
