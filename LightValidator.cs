namespace FluentValidation;

/// <summary>
/// Provides an utility that allows any object to be directly
/// validated, without there being an abstraction or definition for it.
/// </summary>
public static class LightValidator
{
    /// <summary>
    /// Validates any object without being to create an abstraction or definition for it.
    /// </summary>
    /// <typeparam name="T">The type of the object which will be validated.</typeparam>
    /// <param name="val">The instance of <typeparamref name="T"/> which will be validated.</param>
    /// <param name="validationCallback">The handler which will run the rules of the validation.</param>
    /// <returns>If the object is validated, the same object is returned in its same instance.</returns>
    /// <exception cref="ValidationException">Thrown when the object is not validated.</exception>
    public static T Validate<T>(this T val, Action<IRuleBuilder<InlineValidatorContainer<T>, T>> validationCallback)
    {
        InlineValidatorContainer<T> container = new InlineValidatorContainer<T>(val);
        LightInlineValidator<T> validator = new LightInlineValidator<T>();

        var rule = validator.RuleFor(v => v.Value);
        validationCallback(rule);

        var result = validator.Validate(container);

        if (result.IsValid)
        {
            return val;
        }
        else
        {
            throw new ValidationException(result.Errors);
        }
    }
}

/// <summary>
/// Provides an abstraction to house the value of a LightInlineValidator.
/// </summary>
/// <typeparam name="T">The base object to be validated.</typeparam>
public class InlineValidatorContainer<T>
{
    public T Value { get; set; }

    public InlineValidatorContainer(T value)
    {
        Value = value;
    }
}

/// <summary>
/// Abstraction of a validator that box <typeparamref name="T"/> and allows you to
/// call the rules directly on the object.
/// </summary>
/// <typeparam name="T">The base object type to be validated.</typeparam>
public class LightInlineValidator<T> : AbstractValidator<InlineValidatorContainer<T>>
{
}
