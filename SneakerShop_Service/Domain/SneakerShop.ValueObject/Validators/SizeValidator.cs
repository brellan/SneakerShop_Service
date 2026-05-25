using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Exceptions;

namespace SneakerShop.ValueObjects.Validators;

public class SizeValidator : IValidator<float>
{
    public static float MIN_SIZE => 1.0f;
    public static float MAX_SIZE => 55.0f;

    public void Validate(float value)
    {
        if (value < MIN_SIZE || value > MAX_SIZE)
            throw new InvalidSizeException(value);
    }
}