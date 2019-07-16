using System;

namespace Anger_API.API.Attributes
{
    [AttributeUsage(validOn:
    AttributeTargets.Class |
    AttributeTargets.Method |
    AttributeTargets.Interface)]
    public class ApiKeyAuthorize : Attribute
    {
    }
}