using SneakerShop.Domain.Domain.Enums;

namespace SneakerShop.Domain.Domain.Entities
{
    public class ProductFilter
    {
        public Guid? BrandId { get; set; }
        public Size? Size { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; } = true;

        public bool Matches(Product product, List<ProductVariant> variants)
        {
            if (BrandId.HasValue && product.BrandId != BrandId.Value)
                return false;

            if (MinPrice.HasValue && product.BasePrice < MinPrice.Value)
                return false;

            if (MaxPrice.HasValue && product.BasePrice > MaxPrice.Value)
                return false;

            if (IsActive.HasValue && product.IsActive != IsActive.Value)
                return false;

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                if (!product.Name.Value.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) &&
                    !product.Description.Value.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            if (Size.HasValue)
            {
                var hasSize = variants.Any(v => v.Size == Size.Value && v.QuantityInStock > 0);
                if (!hasSize)
                    return false;
            }

            return true;
        }
    }
}