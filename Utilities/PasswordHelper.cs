using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public class PasswordHelper
{
    // Método para crear el hash de la contraseña
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convertir la contraseña en bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convertir los bytes a un string hexadecimal
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }

    // Método para verificar si la contraseña ingresada coincide con el hash almacenado
    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Hashear la contraseña ingresada y compararla con el hash almacenado
        string enteredHash = HashPassword(enteredPassword);
        return enteredHash == storedHash;
    }
}

