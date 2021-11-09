namespace _0_Framework.Domain;
public class DomainErrorMessage
{
    public const string RequiredMessage = "لطفا {0} را وارد کنید";

    public const string MaxLengthMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد";

    public const string MinLengthMessage = "مقدار {0} نباید کمتر از {1} کاراکتر باشد";

    public const string EmailAddressMessage = "ایمیل وارد شده نامعتبر است";

    public const string MobileMessage = "شماره موبایل وارد شده نامعتبر است";

    public const string CompareMessage = "{0} و {1} با هم مغایرت دارند";

    public const string PasswordMessage = "رمز عبور باید ترکیبی از حروف بزرگ و کوچک و عدد و علامت باشد";
}