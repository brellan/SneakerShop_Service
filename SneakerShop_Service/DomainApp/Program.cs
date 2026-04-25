using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Enums;
using SneakerShop.ValueObjects;

namespace SneakerStore.DomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===== ПОДГОТОВКА ТЕСТОВЫХ ДАННЫХ =====
            Console.WriteLine("--- Подготовка тестовых данных ---\n");

            // Создание пользователя
            var userId = Guid.NewGuid();
            var cartId = Guid.NewGuid();
            var wishlistId = Guid.NewGuid();
            var user = new User(userId, new Nickname("SneakerHead"), cartId, wishlistId);
            Console.WriteLine($"Создан пользователь: {user.Nickname.Value} (Id: {user.Id})\n");

            // Создание корзины и вишлиста
            var cart = new Cart(cartId, user.Id);
            var wishlist = new Wishlist(wishlistId, user.Id);
            Console.WriteLine($"Создана корзина (Id: {cart.Id})");
            Console.WriteLine($"Создан вишлист (Id: {wishlist.Id})\n");

            // Создание бренда
            var brand = new Brand(
                Guid.NewGuid(),
                new BrandName("Nike"),
                "https://example.com/nike-logo.png",
                new Description("Just Do It - всемирно известный бренд спортивной обуви"));
            Console.WriteLine($"Создан бренд: {brand.Name.Value}\n");

            // Создание продукта
            var product = brand.AddProduct(
                new ProductName("Air Jordan 1"),
                new Description("Культовые кроссовки, выпущенные в 1985 году"),
                new Price(18999.99m),
                Currency.RUB,
                "https://example.com/jordan1.jpg");
            Console.WriteLine($"Создан продукт: {product.ProductName.Value} - {product.BasePrice.Value} {product.Currency}\n");

            // Добавление вариантов
            var variant1 = product.AddVariant(new Size(41f), new Color("Красный"), new Sku("AJ1-41-RED"), 10);
            var variant2 = product.AddVariant(new Size(42f), new Color("Красный"), new Sku("AJ1-42-RED"), 5);
            var variant3 = product.AddVariant(new Size(43f), new Color("Черный"), new Sku("AJ1-43-BLK"), 3);
            Console.WriteLine("Добавлены варианты продукта:");
            Console.WriteLine($"  - Размер {variant1.Size.Value}, цвет {variant1.Color.Value}, SKU: {variant1.Sku.Value}, в наличии: {variant1.QuantityInStock}");
            Console.WriteLine($"  - Размер {variant2.Size.Value}, цвет {variant2.Color.Value}, SKU: {variant2.Sku.Value}, в наличии: {variant2.QuantityInStock}");
            Console.WriteLine($"  - Размер {variant3.Size.Value}, цвет {variant3.Color.Value}, SKU: {variant3.Sku.Value}, в наличии: {variant3.QuantityInStock}\n");

            // ===== 1. ДОБАВЛЕНИЕ ТОВАРА В КОРЗИНУ =====
            Console.WriteLine("--- 1. Добавление товара в корзину ---");
            var cartItem = cart.AddItem(user.Id, variant2, 2);
            Console.WriteLine($"Добавлен товар: {cartItem.ProductName.Value}, размер {cartItem.Size.Value}, количество {cartItem.Quantity}");
            Console.WriteLine($"Цена за единицу: {cartItem.PriceAtAdd.Value} руб.");
            Console.WriteLine($"Общая стоимость позиции: {cartItem.GetTotalPrice()} руб.");
            Console.WriteLine($"Общая стоимость корзины: {cart.TotalPrice} руб.\n");

            // ===== 2. ДОБАВЛЕНИЕ ТОГО ЖЕ ВАРИАНТА (увеличение количества) =====
            Console.WriteLine("--- 2. Добавление того же варианта (увеличение количества) ---");
            var sameItem = cart.AddItem(user.Id, variant2, 3);
            Console.WriteLine($"Теперь количество: {sameItem.Quantity}");
            Console.WriteLine($"Общая стоимость позиции: {sameItem.GetTotalPrice()} руб.\n");

            // ===== 3. ИЗМЕНЕНИЕ КОЛИЧЕСТВА В КОРЗИНЕ =====
            Console.WriteLine("--- 3. Изменение количества в корзине ---");
            cart.UpdateItemQuantity(user.Id, cartItem.Id, 1);
            Console.WriteLine($"Количество изменено на: {cartItem.Quantity}\n");

            // ===== 4. ДОБАВЛЕНИЕ ДРУГОГО ТОВАРА В КОРЗИНУ =====
            Console.WriteLine("--- 4. Добавление другого товара в корзину ---");
            var cartItem2 = cart.AddItem(user.Id, variant1, 1);
            Console.WriteLine($"Добавлен товар: {cartItem2.ProductName.Value}, размер {cartItem2.Size.Value}");
            Console.WriteLine($"Товаров в корзине: {cart.Items.Count}\n");

            // ===== 5. УДАЛЕНИЕ ТОВАРА ИЗ КОРЗИНЫ =====
            Console.WriteLine("--- 5. Удаление товара из корзины ---");
            cart.RemoveItem(user.Id, cartItem2.Id);
            Console.WriteLine($"Товаров в корзине после удаления: {cart.Items.Count}\n");

            // ===== 6. ОЧИСТКА КОРЗИНЫ =====
            Console.WriteLine("--- 6. Очистка корзины ---");
            cart.Clear(user.Id);
            Console.WriteLine($"Товаров в корзине: {cart.Items.Count}");
            Console.WriteLine($"Общая стоимость: {cart.TotalPrice} руб.\n");

            // ===== 7. ДОБАВЛЕНИЕ В ВИШЛИСТ =====
            Console.WriteLine("--- 7. Добавление в вишлист ---");
            var wishlistItem = wishlist.AddItem(user.Id, variant3);
            Console.WriteLine($"Добавлен товар в вишлист: продукт {product.ProductName.Value}, размер {variant3.Size.Value}\n");

            // ===== 8. ПРОВЕРКА НАЛИЧИЯ В ВИШЛИСТЕ =====
            Console.WriteLine("--- 8. Проверка наличия в вишлисте ---");
            bool contains = wishlist.ContainsVariant(user.Id, variant3.Id);
            Console.WriteLine($"Вариант {variant3.Sku.Value} в вишлисте: {contains}");
            bool notContains = wishlist.ContainsVariant(user.Id, variant1.Id);
            Console.WriteLine($"Вариант {variant1.Sku.Value} в вишлисте: {notContains}\n");

            // ===== 9. ДОБАВЛЕНИЕ ДУБЛИКАТА В ВИШЛИСТ =====
            Console.WriteLine("--- 9. Попытка добавить дубликат в вишлист ---");
            var duplicateItem = wishlist.AddItem(user.Id, variant3);
            Console.WriteLine($"Дубликат не добавлен, возвращен существующий элемент (Id: {duplicateItem.Id})\n");

            // ===== 10. УДАЛЕНИЕ ИЗ ВИШЛИСТА ПО ID ВАРИАНТА =====
            Console.WriteLine("--- 10. Удаление из вишлиста по ID варианта ---");
            wishlist.RemoveItemByVariant(user.Id, variant3.Id);
            Console.WriteLine($"Товаров в вишлисте после удаления: {wishlist.Items.Count}\n");

            // ===== 11. ПРОСМОТР ВСЕХ ПРОДУКТОВ БРЕНДА =====
            Console.WriteLine("--- 11. Просмотр всех продуктов бренда ---");
            Console.WriteLine($"Бренд: {brand.Name.Value}");
            Console.WriteLine($"Количество продуктов: {brand.Products.Count}");
            foreach (var p in brand.Products)
            {
                Console.WriteLine($"  - {p.ProductName.Value} ({p.Variants.Count} вариантов)");
            }
            Console.WriteLine();

            // ===== 12. ПРОСМОТР ВАРИАНТОВ ПРОДУКТА =====
            Console.WriteLine("--- 12. Просмотр вариантов продукта ---");
            Console.WriteLine($"Продукт: {product.ProductName.Value}");
            foreach (var v in product.Variants)
            {
                Console.WriteLine($"  - Размер: {v.Size.Value}, Цвет: {v.Color.Value}, SKU: {v.Sku.Value}, В наличии: {v.QuantityInStock}");
            }
            Console.WriteLine();

            // ===== 13. БРОНИРОВАНИЕ ТОВАРА НА СКЛАДЕ =====
            Console.WriteLine("--- 13. Бронирование товара (уменьшение stock) ---");
            variant1.RemoveStock(2);
            Console.WriteLine($"Было 10, забронировано 2, осталось: {variant1.QuantityInStock}\n");

            // ===== 14. ПРОВЕРКА ДОСТУПНОСТИ ТОВАРА =====
            Console.WriteLine("--- 14. Проверка доступности товара ---");
            Console.WriteLine($"Продукт активен: {product.IsActive}");
            Console.WriteLine($"Вариант {variant1.Sku.Value} в наличии: {variant1.QuantityInStock} шт.\n");

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

            Console.WriteLine("16.11. Попытка добавить вариант к неактивному продукту:");
            var inactiveProduct = new Product(Guid.NewGuid(), brand, new ProductName("Test"),
                new Description("Test"), new Price(100), Currency.RUB, "test.jpg", false);
            try { inactiveProduct.AddVariant(new Size(42f), new Color("Red"), new Sku("TST-42-RD"), 10); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.12. Попытка добавить товар с отрицательным количеством:");
            try { cart.AddItem(user.Id, variant1, -5); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.13. Попытка добавить товар с превышением остатка:");
            try { cart.AddItem(user.Id, variant1, 100); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.14. Попытка изменить корзину чужого пользователя:");
            var otherUserId = Guid.NewGuid();
            try { cart.AddItem(otherUserId, variant1, 1); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.15. Попытка удалить несуществующий товар из корзины:");
            try { cart.RemoveItem(user.Id, Guid.NewGuid()); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.16. Попытка обновить количество несуществующего товара:");
            try { cart.UpdateItemQuantity(user.Id, Guid.NewGuid(), 5); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.17. Попытка изменить вишлист чужого пользователя:");
            try { wishlist.AddItem(otherUserId, variant1); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.18. Попытка списать отрицательное количество со склада:");
            try { variant1.RemoveStock(-5); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.19. Попытка списать больше, чем есть на складе:");
            try { variant3.RemoveStock(100); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.20. Попытка создать User с null Nickname:");
            try { var _ = new User(Guid.NewGuid(), null!, Guid.NewGuid(), Guid.NewGuid()); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.21. Попытка создать Brand с null BrandName:");
            try { var _ = new Brand(Guid.NewGuid(), null!, "logo.png", new Description("desc")); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.22. Попытка создать Product с null ProductName:");
            try { var _ = new Product(Guid.NewGuid(), brand, null!, new Description("desc"), new Price(100), Currency.RUB, "img.jpg"); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}\n"); }

            Console.WriteLine("16.23. Попытка создать CartItem с нулевым количеством:");
            try { var _ = new CartItem(Guid.NewGuid(), cart, variant1, new ProductName("Test"), new Size(42f), new Color("Red"), new Price(100), 0); }
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