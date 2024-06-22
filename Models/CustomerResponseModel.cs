namespace Webmvc.Models
{
    public class CustomerResponseModel
    {
        public List<CustomerModel> Data { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
