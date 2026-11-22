using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace GeneradorContraseñas.Models
{
    public class GeneradorModel
    {
        [Range(8, 64, ErrorMessage = "La longitud debe estar entre 8 y 64 caracteres.")]
        public int Longitud { get; set; } = 16;

        public bool Mayusculas { get; set; } = true;

        public bool Minusculas { get; set; } = true;

        public bool Numeros { get; set; } = true;

        public bool Simbolos { get; set; } = true;

        public string? Resultado { get; set; }

        public string Generar()
        {
            const string letrasMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numerosDisponibles = "0123456789";
            const string simbolosDisponibles = "!@#$%^&*()-_=+[]{};:,.?";

            string caracteres = "";
            List<char> contraseña = new();

            if (Mayusculas)
            {
                caracteres += letrasMayusculas;
                contraseña.Add(letrasMayusculas[RandomNumberGenerator.GetInt32(letrasMayusculas.Length)]);
            }

            if (Minusculas)
            {
                caracteres += letrasMinusculas;
                contraseña.Add(letrasMinusculas[RandomNumberGenerator.GetInt32(letrasMinusculas.Length)]);
            }

            if (Numeros)
            {
                caracteres += numerosDisponibles;
                contraseña.Add(numerosDisponibles[RandomNumberGenerator.GetInt32(numerosDisponibles.Length)]);
            }

            if (Simbolos)
            {
                caracteres += simbolosDisponibles;
                contraseña.Add(simbolosDisponibles[RandomNumberGenerator.GetInt32(simbolosDisponibles.Length)]);
            }

            while (contraseña.Count < Longitud)
            {
                contraseña.Add(caracteres[RandomNumberGenerator.GetInt32(caracteres.Length)]);
            }

            for (int i = contraseña.Count - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                (contraseña[i], contraseña[j]) = (contraseña[j], contraseña[i]);
            }

            return new string(contraseña.ToArray());
        }
    }
}