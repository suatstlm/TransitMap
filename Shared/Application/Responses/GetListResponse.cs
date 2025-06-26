using Persistence.Paging;

namespace Applicaton.Responses;
public class GetListResponse<T> : BasePageableModel
{
    private IList<T>? _items;

    public IList<T> Items
    {
        get
        {
            return _items ?? (_items = new List<T>());
        }
        set
        {
            _items = value;
        }
    }
}
