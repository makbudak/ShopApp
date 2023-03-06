using System.Threading;

namespace ShopApp.Model.Dto.User
{
    public class AuthContent
    {
        public int UserId { get; set; }
        private static readonly AsyncLocal<AuthContent> _current = new AsyncLocal<AuthContent>();
        public static AuthContent Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
    }
}
