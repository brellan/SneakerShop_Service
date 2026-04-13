using SneakerShop.Domain.Domain.Entities;
using SneakerShop.ValueObject;
using SneakerShop.Domain.Domain.Enums;

namespace SneakerShop.DomainApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // ПОДГОТОВКА ТЕСТОВЫХ ДАННЫХ
            Console.WriteLine("--- Подготовка тестовых данных ---\n");

            // Создание брендов
            var nikeBrand = new Brand(new BrandName("Nike"), new LogoUrl("https://example.com/nike.png"), new Description("Just Do It"));
            var adidasBrand = new Brand(new BrandName("Adidas"), new LogoUrl("https://example.com/adidas.png"), new Description("Impossible is Nothing"));

            // Создание товаров
            var jordan1 = new Product(nikeBrand.Id, new ProductName("Air Jordan 1"), new Description("Классические кроссовки"), 18999.99m, Currency.RUB, new MainImageUrl("https://example.com/jordan1.jpg"));
            var dunk = new Product(nikeBrand.Id, new ProductName("Nike Dunk Low"), new Description("Повседневные кроссовки"), 12999.99m, Currency.RUB, new MainImageUrl("https://example.com/dunk.jpg"));
            var yeezy = new Product(adidasBrand.Id, new ProductName("Yeezy 350"), new Description("Кроссовки от Kanye West"), 24999.99m, Currency.RUB, new MainImageUrl("https://example.com/yeezy.jpg"));

            // Добавление вариантов (размеры обуви от 39 до 45)
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size39, Color.Red, new Sku("AJ1391234"), 10, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size40, Color.Red, new Sku("AJ1401234"), 15, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size41, Color.Red, new Sku("AJ1411234"), 8, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size42, Color.Red, new Sku("AJ1421234"), 12, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size43, Color.Red, new Sku("AJ1431234"), 5, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size44, Color.Red, new Sku("AJ1441234"), 3, new AdditionalImages(null)));
            jordan1.AddVariant(new ProductVariant(jordan1.Id, Size.Size45, Color.Red, new Sku("AJ1451234"), 0, new AdditionalImages(null)));

            dunk.AddVariant(new ProductVariant(dunk.Id, Size.Size40, Color.Black, new Sku("DUNK40123"), 20, new AdditionalImages(null)));
            dunk.AddVariant(new ProductVariant(dunk.Id, Size.Size41, Color.Black, new Sku("DUNK41123"), 25, new AdditionalImages(null)));
            dunk.AddVariant(new ProductVariant(dunk.Id, Size.Size42, Color.Black, new Sku("DUNK42123"), 18, new AdditionalImages(null)));
            dunk.AddVariant(new ProductVariant(dunk.Id, Size.Size43, Color.Black, new Sku("DUNK43123"), 8, new AdditionalImages(null)));

            yeezy.AddVariant(new ProductVariant(yeezy.Id, Size.Size41, Color.White, new Sku("YZY411234"), 3, new AdditionalImages(null)));
            yeezy.AddVariant(new ProductVariant(yeezy.Id, Size.Size42, Color.White, new Sku("YZY421234"), 7, new AdditionalImages(null)));
            yeezy.AddVariant(new ProductVariant(yeezy.Id, Size.Size43, Color.White, new Sku("YZY431234"), 5, new AdditionalImages(null)));

            // Создание каталога
            var catalog = new ProductCatalog();
            catalog.AddProduct(jordan1);
            catalog.AddProduct(dunk);
            catalog.AddProduct(yeezy);

            // Создание пользователя
            var user = new User(new Nickname("SneakerHead"));
            user.CreateCart();
            user.CreateWishlist();

            // ===== 1. ПРОСМОТР СПИСКА ВСЕХ КРОССОВОК =====
            Console.WriteLine("1. Просмотр списка всех кроссовок:");
            foreach (var product in catalog.GetAllProducts())
            {
                Console.WriteLine($"- {product.Name.Value} | {product.BasePrice} {product.Currency}");
            }
            Console.WriteLine();

            // ===== 2. ФИЛЬТРАЦИЯ ПО БРЕНДУ =====
            Console.WriteLine("2. Фильтрация по бренду (Nike):");
            var nikeProducts = catalog.GetProductsByBrand(nikeBrand.Id);
            foreach (var product in nikeProducts)
            {
                Console.WriteLine($"- {product.Name.Value} | {product.BasePrice} {product.Currency}");
            }
            Console.WriteLine();

            // ===== 3. ФИЛЬТРАЦИЯ ПО РАЗМЕРУ =====
            Console.WriteLine("3. Фильтрация по размеру (42):");
            var size42Products = catalog.GetProductsBySize(Size.Size42);
            foreach (var product in size42Products)
            {
                var sizes = product.GetAvailableSizes();
                var sizesStr = string.Join(", ", sizes.Select(s => ((int)s).ToString()));
                Console.WriteLine($"- {product.Name.Value} | Доступные размеры: {sizesStr}");
            }
            Console.WriteLine();

            // ===== 4. ФИЛЬТРАЦИЯ ПО ЦЕНОВОМУ ДИАПАЗОНУ =====
            Console.WriteLine("4. Фильтрация по ценовому диапазону (10000 - 20000 руб.):");
            var priceRangeProducts = catalog.GetProductsByPriceRange(10000, 20000);
            foreach (var product in priceRangeProducts)
            {
                Console.WriteLine($"- {product.Name.Value} | {product.BasePrice} {product.Currency}");
            }
            Console.WriteLine();

            // ===== 5. ПРОСМОТР ДЕТАЛЬНОЙ КАРТОЧКИ ТОВАРА =====
            Console.WriteLine("5. Просмотр детальной карточки товара (Air Jordan 1):");
            var detailProduct = catalog.GetProductById(jordan1.Id);
            if (detailProduct != null)
            {
                Console.WriteLine($"Название: {detailProduct.Name.Value}");
                Console.WriteLine($"Описание: {detailProduct.Description.Value}");
                Console.WriteLine($"Цена: {detailProduct.BasePrice} {detailProduct.Currency}");
                Console.WriteLine($"Главное фото: {detailProduct.MainImageUrl.Value}");
                Console.WriteLine("Наличие размеров:");
                foreach (var variant in detailProduct.GetAvailableVariants())
                {
                    Console.WriteLine($"  - Размер {(int)variant.Size} | В наличии: {variant.QuantityInStock} шт.");
                }
            }
            Console.WriteLine();

            // ===== 6. ДОБАВЛЕНИЕ ТОВАРА В КОРЗИНУ =====
            Console.WriteLine("6. Добавление товара в корзину (Air Jordan 1, размер 42):");
            if (detailProduct != null)
            {
                var selectedVariant = detailProduct.GetVariantBySize(Size.Size42);
                if (selectedVariant != null && selectedVariant.IsInStock())
                {
                    var cartItem = new CartItem(
                        user.Cart.Id,
                        detailProduct.Id,
                        selectedVariant.Id,
                        new ProductNameAtCart(detailProduct.Name.Value),
                        selectedVariant.Size,
                        selectedVariant.Color,
                        detailProduct.BasePrice,
                        2
                    );
                    user.Cart.AddItem(cartItem);
                    Console.WriteLine($"Добавлено: {cartItem.ProductName.Value} (Размер {(int)cartItem.Size}) x{cartItem.Quantity}");
                }
            }
            Console.WriteLine();

            // ===== 7. ПРОСМОТР СОДЕРЖИМОГО КОРЗИНЫ =====
            Console.WriteLine("7. Просмотр содержимого корзины:");
            if (user.Cart.Items.Any())
            {
                foreach (var item in user.Cart.Items)
                {
                    Console.WriteLine($"- {item.ProductName.Value} | Размер {(int)item.Size} | {item.Quantity} шт. | {item.GetTotalPrice()} руб.");
                }
                Console.WriteLine($"Общая сумма: {user.Cart.TotalPrice} руб.");
            }
            else
            {
                Console.WriteLine("Корзина пуста");
            }
            Console.WriteLine();

            // ===== 8. УДАЛЕНИЕ ТОВАРА ИЗ КОРЗИНЫ =====
            Console.WriteLine("8. Удаление товара из корзины:");
            var itemToRemove = user.Cart.Items.FirstOrDefault();
            if (itemToRemove != null)
            {
                user.Cart.RemoveItem(itemToRemove.Id);
                Console.WriteLine($"Удален товар: {itemToRemove.ProductName.Value}");
            }
            Console.WriteLine($"Товаров в корзине после удаления: {user.Cart.Items.Count}");
            Console.WriteLine();

            // ===== 9. ДОБАВЛЕНИЕ ТОВАРА В ВИШЛИСТ =====
            Console.WriteLine("9. Добавление товара в вишлист:");
            if (detailProduct != null)
            {
                var selectedVariant = detailProduct.GetVariantBySize(Size.Size42);
                if (selectedVariant != null)
                {
                    var wishlistItem = new WishlistItem(user.Wishlist.Id, detailProduct.Id, selectedVariant.Id);
                    user.Wishlist.AddItem(wishlistItem);
                    Console.WriteLine($"Добавлен в вишлист: {detailProduct.Name.Value} (Размер {(int)selectedVariant.Size})");
                }
            }
            Console.WriteLine();

            // ===== 10. ПРОСМОТР СОДЕРЖИМОГО ВИШЛИСТА =====
            Console.WriteLine("10. Просмотр содержимого вишлиста:");
            if (user.Wishlist.Items.Any())
            {
                foreach (var item in user.Wishlist.Items)
                {
                    var product = catalog.GetProductById(item.ProductId);
                    if (product != null)
                    {
                        Console.WriteLine($"- {product.Name.Value} | ID варианта: {item.ProductVariantId}");
                    }
                    else
                    {
                        Console.WriteLine($"- Товар (удален) | ID варианта: {item.ProductVariantId}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Вишлист пуст");
            }
            Console.WriteLine();

            // ===== 11. УДАЛЕНИЕ ТОВАРА ИЗ ВИШЛИСТА =====
            Console.WriteLine("11. Удаление товара из вишлиста:");
            var wishlistItemToRemove = user.Wishlist.Items.FirstOrDefault();
            if (wishlistItemToRemove != null)
            {
                user.Wishlist.RemoveItem(wishlistItemToRemove.Id);
            }
            Console.WriteLine($"Товаров в вишлисте после удаления: {user.Wishlist.Count}");
            Console.WriteLine();

            // ===== 12. ОЧИСТКА КОРЗИНЫ =====
            Console.WriteLine("12. Очистка корзины:");
            user.Cart.Clear();
            Console.WriteLine($"Корзина пуста: {user.Cart.Items.Count == 0}");
            Console.WriteLine();

            // ===== 13. ОЧИСТКА ВИШЛИСТА =====
            Console.WriteLine("13. Очистка вишлиста:");
            user.Wishlist.Clear();
            Console.WriteLine($"Вишлист пуст: {user.Wishlist.Count == 0}");
            Console.WriteLine();

            // ===== 14. РАСШИРЕННАЯ ФИЛЬТРАЦИЯ =====
            Console.WriteLine("14. Расширенная фильтрация (Nike, размер 41, цена до 20000):");
            var filter = new ProductFilter
            {
                BrandId = nikeBrand.Id,
                Size = Size.Size41,
                MaxPrice = 20000
            };
            var filteredProducts = catalog.FilterProducts(filter);
            foreach (var product in filteredProducts)
            {
                var hasSize41 = product.GetVariantBySize(Size.Size41)?.IsInStock() == true ? "есть" : "нет";
                Console.WriteLine($"- {product.Name.Value} | {product.BasePrice} руб. | Размер 41: {hasSize41}");
            }

            // ===== 15. ДЕМОНСТРАЦИЯ ToString() =====
            Console.WriteLine("15. Демонстрация ToString():");
            Console.WriteLine($"Бренд: {nikeBrand}");
            Console.WriteLine($"Товар: {jordan1}");
            Console.WriteLine($"Вариант: {catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40)}");
            Console.WriteLine($"Пользователь: {user}");
            Console.WriteLine($"Корзина: {user.Cart}");
            Console.WriteLine($"Вишлист: {user.Wishlist}");
            Console.WriteLine();

            // ===== 16. ДЕМОНСТРАЦИЯ ИСКЛЮЧЕНИЙ =====
            Console.WriteLine("16. Демонстрация исключений:");

            Console.WriteLine("16.1. Попытка создать бренд с названием 'A':");
            try { var _ = new BrandName("A"); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.2. Попытка создать URL логотипа 'not-a-url':");
            try { var _ = new LogoUrl("not-a-url"); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.3. Попытка создать SKU 'SHORT':");
            try { var _ = new Sku("SHORT"); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.4. Попытка установить отрицательную цену:");
            try { jordan1.SetBasePrice(-1000); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.5. Попытка списать 1000 единиц (в наличии 10):");
            try { catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40).DecreaseStock(1000); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.6. Попытка добавить в корзину товар с отрицательным количеством:");
            try
            {
                var _ = new CartItem(user.Cart.Id, jordan1.Id, catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40).Id, new ProductNameAtCart(jordan1.Name.Value), catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40).Size, catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40).Color, jordan1.BasePrice, -5);
            }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.7. Попытка создать продукт с пустым названием:");
            try { var _ = new ProductName(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.8. Попытка добавить дубликат в вишлист:");
            var testItem = new WishlistItem(user.Wishlist.Id, jordan1.Id, catalog.GetProductById(jordan1.Id).GetVariantBySize(Size.Size40).Id);
            try
            {
                user.Wishlist.AddItem(testItem);
                user.Wishlist.AddItem(testItem);
            }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }

            Console.WriteLine("\n16.9. Попытка создать пользователя с пустым никнеймом:");
            try { var _ = new Nickname(""); }
            catch (Exception ex) { Console.WriteLine($"  Ошибка: {ex.Message}"); }
        }
    }
}