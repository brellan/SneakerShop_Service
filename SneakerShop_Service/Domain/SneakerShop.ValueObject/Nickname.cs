using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Nickname(string name) : ValueObject<string>(new NicknameValidator(), name);