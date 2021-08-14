using System.Threading;

namespace ShopApp.Model.Dto.Customer
{
    public class CustomerAuthContent
    {
        public int CustomerId { get; set; }
        private static readonly AsyncLocal<CustomerAuthContent> _current = new AsyncLocal<CustomerAuthContent>();
        public static CustomerAuthContent Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
    }
}
