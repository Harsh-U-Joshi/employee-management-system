﻿namespace Employee.Management.Application.Models;

public class AuthResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
}
