# LightFluentValidator 

This repository contains the code necessary to perform inline validations, without creating a validator, property, or abstraction, for any object.

Example:

```cs
string test = "hello".Validate(str => str.MinimumLength(10));
int number = 123
    .Validate(i => i.GreaterThan(10))
    .Validate(i => i.LessThan(150));
```