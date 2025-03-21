﻿using System.Net;

namespace TechLibrary.Exception;

public class InvalidLoginException : TechLibraryException
{
    public InvalidLoginException() : base("E-mail e/ou senha invalidos") {}

    public override List<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
