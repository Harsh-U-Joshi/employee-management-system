﻿namespace Employee.Management.Application.Models;

public class JwtSettings
{
    public string Key { get; set; } 
    public string Issuer { get; set; } 
    public string Audience { get; set; } 
    public int DurationInSeconds { get; set; }
}
