# Haitch.Results

A lightweight `Result` type and structured `Error` model for railway-oriented programming in modern .NET. Part of the [Haitch](https://haitch.dev) suite of foundational libraries.

## Install
```shell
dotnet add package Haitch.Results
```

## Quick start

```csharp
using Haitch.Results;

public Result<User> GetUser(int id)
{
    var user = _repo.Find(id);
    if (user is null)
        return Error.NotFound("user.not_found", $"User {id} not found");

    return user;
}

var displayName = GetUser(42)
    .Ensure(u => u.IsActive, Error.Validation("user.inactive", "User is inactive"))
    .Map(u => u.DisplayName)
    .Match(
        onSuccess: name => name,
        onFailure: err => "Anonymous");
```

## Features

- Three result types: `Result`, `Result<TValue>`, `Result<TValue, TError>`
- Structured `Error` records with categorized error types
- Composable combinators: `Match`, `Map`, `MapError`, `Bind`, `Tap`, `TapError`, `Ensure`
- Async combinators support
- Implicit conversions
- Source Link support

## Documentation

Full documentation at [haitch.dev/libraries/results](https://haitch.dev/libraries/results).