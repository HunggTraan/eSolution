using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.ViewModels.Catalog.Departments
{
    public class GetDepartmentPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }
    }
}
