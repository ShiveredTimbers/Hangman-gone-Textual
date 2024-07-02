using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hangman_Gone_Textual
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 80;

            Console.Write("Welcome to Hangman! Our theme today is: Animals. Good Luck!");

            //array of possible words (word bank)
            string[] words =
            {
                "MONKEY",
                "ELEPHANT",
                "MOOSE",
                "CARIBOU",
                "BENGAL",
                "LABRADOR",
                "RAVEN",
                "PIGEON",
                "BABOON",
                "CAMEL",
                "GRIZZLY",
                "ZEBRA",
                "FERRET",
                "HIPPO",
                "RHINOCEROS",
                "RABBIT",
                "TIGER",
                "HONEYBEE",
                "AXOLOTL",
                "SHEEP",
            };

            //pick random word from word bank
            var chosenWord = words[new Random().Next(0, words.Length - 1)];

            // Regex with valid characters (single character between a and z). !Don't know how this works personally
            var validCharacters = new Regex("^[A-Z]$");

            //how many chances the player has to guess the word
            var attempts = 5;

            // list with the letters the player guesses
            var playerGuesses = new List<string>();


            //while the attempts are less than 7
            while (attempts != 0)
            {

                var lettersLeft = 0;

                //cycle through letters in the chosen word
                foreach (var character in chosenWord)
                {
                    //converts each letter to a string
                    var letter = Convert.ToString(character);

                    //if the letter guessed is correct, write it out
                    if (playerGuesses.Contains(letter))
                    {
                        Console.WriteLine(letter);

                    }
                    //otherwise write out a blank
                    else
                    {
                        Console.WriteLine("_");

                        //counts how many letters are left
                        lettersLeft++;
                    }
                }

                //shorthand for making a break in text
                Console.WriteLine(string.Empty);
                Console.WriteLine("_____________________________");

                //breaks out of loop if all letters are guessed
                if (lettersLeft == 0)
                {
                    break;
                }
                

                //player input processing

                Console.WriteLine($"Take your guess: ");

                //processes player chosen key, makes it a string and makes it lowercase if capitalized
                var key = Console.ReadKey().Key.ToString().ToUpper();
                Console.WriteLine(string.Empty);

                //if guess is wrong, tell mistake and loop back to start
                if (!validCharacters.IsMatch(key))
                {
                    Console.WriteLine($"Uh oh, " + key + " is incorrect. Try again!");
                    continue;
                }

                //if guess is the same as a former one, tell mistake and loop back to start
                if (playerGuesses.Contains(key) && !validCharacters.IsMatch(key))
                {
                    Console.WriteLine($"You've already used " + key + " before. Try again!");
                    continue;
                }

                //if guess is hasn't been used before, add it to list holding player guesses
                playerGuesses.Add(key);
                Console.WriteLine("_____________________________");

                //if the chosen word doesn't contain the player's guess, reduce lives by one
                if (!chosenWord.Contains(key))
                {
                    attempts--;

                    //if the player still has attempts, let them know how many
                    if (attempts > 0)
                    {
                        //speical line that makes the text change based on the amount of lives left
                        Console.WriteLine($"The letter {key} is not in the word. You have {attempts} {(attempts == 1 ? "try" : "tries")} left.");
                    }
                }
            }

            //if the player guesses the word with attempts left, congraulate and inform remaining lives
            if (attempts > 0)
            {
                Console.WriteLine($"WOW, you got it! The word was {chosenWord.ToUpper()} You won with {attempts} {(attempts == 1 ? "life" : "lives")} left! Well done!");
            }
            else if (attempts <= 0)
            {
                Console.WriteLine($"Unlucky, you lost! The word was " + chosenWord.ToUpper() + ". Better luck next time!");
            }

            Console.ReadKey();
        }
    }
}
