using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.ViewModels.Catalog.JobTitles
{
    public class GetJobTitlePagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
