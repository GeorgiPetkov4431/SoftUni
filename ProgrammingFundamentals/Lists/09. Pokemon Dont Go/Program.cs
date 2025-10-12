using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._Pokemon_Dont_Go;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> pokemon = Console.ReadLine().Split().Select(int.Parse).ToList();
        int pointSum = 0;

        while (pokemon.Count > 0)
        {
            int targetPokemonIndex = int.Parse(Console.ReadLine());
            int removedPokemon;

            if (targetPokemonIndex < 0)
            {
                removedPokemon = pokemon[0];
                pokemon[0] = pokemon[pokemon.Count - 1];
            }
            else if (targetPokemonIndex >= pokemon.Count)
            {
                removedPokemon = pokemon[pokemon.Count - 1];
                pokemon[pokemon.Count - 1] = pokemon[0];
            }
            else
            {
                removedPokemon = pokemon[targetPokemonIndex];
                pokemon.RemoveAt(targetPokemonIndex);
            }

            pointSum += removedPokemon;

            for (int i = 0; i < pokemon.Count; i++)
            {
                if (pokemon[i] <= removedPokemon) pokemon[i] += removedPokemon;
                else pokemon[i] -= removedPokemon;
            }
        }

        Console.WriteLine(pointSum);
    }
}
