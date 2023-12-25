using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class StringConventer
{
    public static string ToColor(string text)
    {
        using var hash = MD5.Create();
        var data = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
        var hex = BitConverter.ToString(data).Replace("-", string.Empty.Substring(0, 6));

        return hex;
    }
}
