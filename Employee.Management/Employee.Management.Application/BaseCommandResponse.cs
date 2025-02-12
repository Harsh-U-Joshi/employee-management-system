﻿namespace Employee.Management.Application
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public object Data { get; set; } = new();
    }
}
