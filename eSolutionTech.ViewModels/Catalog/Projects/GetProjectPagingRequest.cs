using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.ViewModels.Catalog.Projects
{
  public class GetProjectPagingRequest : PagingRequestBase
  {
    public string KeyWord { get; set; }
    public string UserId { get; set; }
  }
}
