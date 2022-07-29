namespace _0_Framework.Domain;

public class DomainErrorMessage
{
    public const string Required = "لطفا {0} را وارد کنید";

    public const string MaxLength = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد";

    public const string MinLength = "مقدار {0} نباید کمتر از {1} کاراکتر باشد";

    public const string Email = "ایمیل وارد شده نامعتبر است";

    public const string Mobile = "شماره موبایل وارد شده نامعتبر است";

    public const string Compare = "{0} و {1} با هم مغایرت دارند";

    public const string Password = "رمز عبور باید ترکیبی از حروف بزرگ و کوچک و عدد و علامت باشد";

    public const string FileMaxSize = "حجم فایل بیشتر از مقدار مجاز است. لطفا فایل دیگری آپلود کنید";
}