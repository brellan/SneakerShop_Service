using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Enums;
using SneakerShop.Domain.Exceptions;
using SneakerShop.ValueObjects;

namespace SneakerShop.DomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===== ПОДГОТОВКА ТЕСТОВЫХ ДАННЫХ =====
            Console.WriteLine("--- Подготовка тестовых данных ---\n");

            // Создание пользователя
            var user = new User(new Nickname("SneakerHead"));
            Console.WriteLine($"Создан пользователь: {user}\n");

            // Создание корзины и вишлиста
            var cart = new Cart(user);
            var wishlist = new Wishlist(user);
            Console.WriteLine($"Создана корзина: {cart}");
            Console.WriteLine($"Создан вишлист: {wishlist}\n");

            // Создание бренда
            var brand = new Brand(
                new BrandName("Nike"),
                new LogoUrl("https://example.com/nike-logo.png"),
                new Description("Just Do It - всемирно известный бренд спортивной обуви"));
            Console.WriteLine($"Создан бренд: {brand}\n");

            // Создание продукта
            var product = brand.AddProduct(
                new ProductName("Air Jordan 1"),
                new Description("Культовые кроссовки, выпущенные в 1985 году"),
                new Price(18999.99m),
                Currency.RUB,
                new ImageUrl("https://example.com/jordan1.jpg"));
            Console.WriteLine($"Создан продукт: {product}\n");

            // Добавление вариантов
            var variant1 = product.AddVariant(
                new Size(41f),
                new Color("Красный"),
                new Sku("AJ1-41-RED"),
                new StockQuantity(10));
            var variant2 = product.AddVariant(
                new Size(42f),
                new Color("Красный"),
                new Sku("AJ1-42-RED"),
                new StockQuantity(5));
            var variant3 = product.AddVariant(
                new Size(43f),
                new Color("Черный"),
                new Sku("AJ1-43-BLK"),
                new StockQuantity(3));
            Console.WriteLine("Добавлены варианты продукта:");
            Console.WriteLine($"  - {variant1}");
            Console.WriteLine($"  - {variant2}");
            Console.WriteLine($"  - {variant3}\n");

            // ===== 1. ДОБАВЛЕНИЕ ТОВАРА В КОРЗИНУ =====
            Console.WriteLine("--- 1. Добавление товара в корзину ---");
            var cartItem = user.AddToCart(cart, variant2, new Quantity(2));
            Console.WriteLine($"Добавлен товар: {cartItem}");
            Console.WriteLine($"Корзина: {cart}\n");

            // ===== 2. ДОБАВЛЕНИЕ ТОГО ЖЕ ВАРИАНТА (увеличение количества) =====
            Console.WriteLine("--- 2. Добавление того же варианта (увеличение количества) ---");
            var sameItem = user.AddToCart(cart, variant2, new Quantity(3));
            Console.WriteLine($"Теперь: {sameItem}\n");

            // ===== 3. ИЗМЕНЕНИЕ КОЛИЧЕСТВА В КОРЗИНЕ =====
            Console.WriteLine("--- 3. Изменение количества в корзине ---");
            user.UpdateCartItemQuantity(cart, cartItem.Id, new Quantity(1));
            Console.WriteLine($"Количество изменено: {cartItem}\n");

            // ===== 4. ДОБАВЛЕНИЕ ДРУГОГО ТОВАРА В КОРЗИНУ =====
            Console.WriteLine("--- 4. Добавление другого товара в корзину ---");
            var cartItem2 = user.AddToCart(cart, variant1, new Quantity(1));
            Console.WriteLine($"Добавлен товар: {cartItem2}");
            Console.WriteLine($"Товаров в корзине: {cart.Items.Count}\n");

            // ===== 5. УДАЛЕНИЕ ТОВАРА ИЗ КОРЗИНЫ =====
            Console.WriteLine("--- 5. Удаление товара из корзины ---");
            user.RemoveFromCart(cart, cartItem2.Id);
            Console.WriteLine($"Товаров в корзине после удаления: {cart.Items.Count}\n");

            // ===== 6. ОЧИСТКА КОРЗИНЫ =====
            Console.WriteLine("--- 6. Очистка корзины ---");
            user.ClearCart(cart);
            Console.WriteLine($"Товаров в корзине: {cart.Items.Count}");
            Console.WriteLine($"Корзина: {cart}\n");

            // ===== 7. ДОБАВЛЕНИЕ В ВИШЛИСТ =====
            Console.WriteLine("--- 7. Добавление в вишлист ---");
            var wishlistItem = user.AddToWishlist(wishlist, variant3);
            Console.WriteLine($"Добавлен товар в вишлист: {wishlistItem}\n");

            // ===== 8. ПРОВЕРКА НАЛИЧИЯ В ВИШЛИСТЕ =====
            Console.WriteLine("--- 8. Проверка наличия в вишлисте ---");
            bool contains = user.IsInWishlist(wishlist, variant3.Id);
            Console.WriteLine($"Вариант {variant3.Sku.Value} в вишлисте: {contains}");
            bool notContains = user.IsInWishlist(wishlist, variant1.Id);
            Console.WriteLine($"Вариант {variant1.Sku.Value} в вишлисте: {notContains}\n");

            // ===== 9. ДОБАВЛЕНИЕ ДУБЛИКАТА В ВИШЛИСТ =====
            Console.WriteLine("--- 9. Попытка добавить дубликат в вишлист ---");
            var duplicateItem = user.AddToWishlist(wishlist, variant3);
            Console.WriteLine($"Дубликат не добавлен, возвращен существующий: {duplicateItem}\n");

            // ===== 10. УДАЛЕНИЕ ИЗ ВИШЛИСТА =====
            Console.WriteLine("--- 10. Удаление из вишлиста ---");
            user.RemoveFromWishlistByVariant(wishlist, variant3.Id);
            Console.WriteLine($"Товаров в вишлисте после удаления: {wishlist.Items.Count}\n");

            // ===== 11. ПРОСМОТР ВСЕХ ПРОДУКТОВ БРЕНДА =====
            Console.WriteLine("--- 11. Просмотр всех продуктов бренда ---");
            Console.WriteLine($"Бренд: {brand}");
            foreach (var p in brand.Products)
            {
                Console.WriteLine($"  - {p}");
            }
            Console.WriteLine();

            // ===== 12. ПРОСМОТР ВАРИАНТОВ ПРОДУКТА =====
            Console.WriteLine("--- 12. Просмотр вариантов продукта ---");
            Console.WriteLine($"Продукт: {product}");
            foreach (var v in product.Variants)
            {
                Console.WriteLine($"  - {v}");
            }
            Console.WriteLine();

            // ===== 13. БРОНИРОВАНИЕ ТОВАРА НА СКЛАДЕ =====
            Console.WriteLine("--- 13. Бронирование товара (уменьшение stock) ---");
            Console.WriteLine($"До: {variant1}");
            variant1.RemoveStock(2);
            Console.WriteLine($"После: {variant1}\n");

            // ===== 14. ПРОВЕРКА ДОСТУПНОСТИ ТОВАРА =====
            Console.WriteLine("--- 14. Проверка доступности товара ---");
            Console.WriteLine($"Продукт активен: {product.IsActive}");
            Console.WriteLine($"Вариант {variant1.Sku.Value} в наличии: {variant1.QuantityInStock.Value} шт.\n");

            // ===== 15. ДЕМОНСТРАЦИЯ РАВЕНСТВА VALUE OBJECTS =====
            Console.WriteLine("--- 15. Демонстрация равенства Value Objects ---");
            var size1 = new Size(42f);
            var size2 = new Size(42f);
            var size3 = new Size(43f);
            Console.WriteLine($"size1 == size2: {size1 == size2}");
            Console.WriteLine($"size1 == size3: {size1 == size3}");
            var name1 = new BrandName("Nike");
            var name2 = new BrandName("Nike");
            Console.WriteLine($"name1 == name2: {name1 == name2}\n");

            // ===== 16. ДЕМОНСТРАЦИЯ ИСКЛЮЧЕНИЙ =====
            Console.WriteLine("--- 16. Демонстрация исключений ---\n");

            Console.WriteLine("16.1. Попытка создать Nickname с пустой строкой:");
            try { var _ = new Nickname(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.2. Попытка создать Nickname короче 3 символов:");
            try { var _ = new Nickname("ab"); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.3. Попытка создать Nickname длиннее 30 символов:");
            try { var _ = new Nickname(new string('a', 31)); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.4. Попытка создать BrandName с пустой строкой:");
            try { var _ = new BrandName(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.5. Попытка создать ProductName с пустой строкой:");
            try { var _ = new ProductName(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.6. Попытка создать Price = 0:");
            try { var _ = new Price(0); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.7. Попытка создать Price = -100:");
            try { var _ = new Price(-100); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.8. Попытка создать Size = 0:");
            try { var _ = new Size(0); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.9. Попытка создать Size = 60:");
            try { var _ = new Size(60); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.10. Попытка создать Sku с пустой строкой:");
            try { var _ = new Sku(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.11. Попытка создать LogoUrl с пустой строкой:");
            try { var _ = new LogoUrl(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.12. Попытка создать ImageUrl с пустой строкой:");
            try { var _ = new ImageUrl(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.13. Попытка создать Quantity = 0:");
            try { var _ = new Quantity(0); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.14. Попытка создать Quantity = 100:");
            try { var _ = new Quantity(100); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.15. Попытка создать StockQuantity = -5:");
            try { var _ = new StockQuantity(-5); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.16. Попытка добавить вариант к неактивному продукту:");
            var inactiveProduct = new Product(
                brand,
                new ProductName("Test"),
                new Description("Test"),
                new Price(100),
                Currency.RUB,
                new ImageUrl("test.jpg"),
                false);
            try { inactiveProduct.AddVariant(new Size(42f), new Color("Red"), new Sku("TST-42-RD"), new StockQuantity(10)); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.17. Попытка добавить товар в корзину с превышением остатка:");
            try { user.AddToCart(cart, variant1, new Quantity(100)); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.18. Попытка изменить корзину чужого пользователя:");
            var otherUser = new User(new Nickname("OtherUser"));
            var otherCart = new Cart(otherUser);
            try { otherCart.AddItem(user, variant1, new Quantity(1)); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.19. Попытка удалить несуществующий товар из корзины:");
            try { user.RemoveFromCart(cart, Guid.NewGuid()); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.20. Попытка обновить количество несуществующего товара:");
            try { user.UpdateCartItemQuantity(cart, Guid.NewGuid(), new Quantity(5)); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.21. Попытка изменить вишлист чужого пользователя:");
            var otherWishlist = new Wishlist(otherUser);
            try { otherWishlist.AddItem(user, variant1); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.22. Попытка списать больше, чем есть на складе:");
            try { variant3.RemoveStock(100); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.23. Попытка списать отрицательное количество со склада:");
            try { variant1.RemoveStock(-5); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            // ===== 17. ДЕМОНСТРАЦИЯ TOSTRING() =====
            Console.WriteLine("--- 17. Демонстрация ToString() ---");
            Console.WriteLine($"User: {user}");
            Console.WriteLine($"Brand: {brand}");
            Console.WriteLine($"Product: {product}");
            Console.WriteLine($"Variant1: {variant1}");
            Console.WriteLine($"Variant2: {variant2}");
            Console.WriteLine($"Variant3: {variant3}");
            Console.WriteLine($"Cart: {cart}");
            Console.WriteLine($"CartItem: {cartItem}");
            Console.WriteLine($"Wishlist: {wishlist}");
            Console.WriteLine($"WishlistItem: {wishlistItem}");
        }
    }
}