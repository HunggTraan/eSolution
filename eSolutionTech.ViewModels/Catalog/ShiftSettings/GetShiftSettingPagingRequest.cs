using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.ViewModels.Catalog.ShiftSettings
{
    public class GetShiftSettingPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
