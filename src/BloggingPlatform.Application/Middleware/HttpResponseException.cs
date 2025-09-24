﻿namespace BloggingPlatform.Application.Middleware
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; set; }
        public object? Value { get; set; }   

        public HttpResponseException(int statusCode, string? message, object? value = null) : base(message)
        { 
            StatusCode = statusCode; 
            Value = value; 
        }
    }
}
