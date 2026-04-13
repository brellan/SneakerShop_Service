using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class Nickname(string value) : ValueObject<string>(new NicknameValidator(), value);