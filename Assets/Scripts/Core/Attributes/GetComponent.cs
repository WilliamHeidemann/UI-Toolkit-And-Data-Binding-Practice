using System;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentAttribute : Attribute
    {
    }
}