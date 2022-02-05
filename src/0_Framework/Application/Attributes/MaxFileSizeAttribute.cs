using _0_Framework.Application.Extensions;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    public override bool IsValid(object value)
    {
        return MaxFileSizeHelper.IsValid(_maxFileSize, value);
    }
}

