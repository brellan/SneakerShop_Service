namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Nickname { get; private set; }

        // Навигационные свойства
        public Cart Cart { get; private set; }
        public Wishlist Wishlist { get; private set; }

        private User() { }

        public User(string email, string nickname)
        {
            Id = Guid.NewGuid();
            SetNickname(nickname);
        }

        public void SetNickname(string nickname)
        {
            if (string.IsNullOrWhiteSpace(Nickname))
                throw new ArgumentException("Имя не может быть пустым");
            Nickname = nickname;
        }

        public void CreateCart()
        {
            Cart = new Cart(Id);
        }

        public void CreateWishlist()
        {
            Wishlist = new Wishlist(Id);
        }

        public override string ToString()
        {
            return $"User [Id: {Id}, Name: {Nickname}]";
        }
    }
}