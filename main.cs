// using System;
using System.Text;

namespace Program
{
    class Hangman {
        static string hyphens = "";
        const int hyphen_amount = 12;

        static readonly string[] possible_words = [
            "triradial",
            "overimpressionability",
            "penticle",
            "phytogenetical",
            "foldboater",
            "bellite",
            "relevances",
            "dharnas",
            "infusory",
            "bluetop",
            "pentahedrical",
            "formylated",
            "splinterproof",
            "ed",
            "noncompensable",
            "cabinetmaking",
            "parallels",
            "colville",
            "uppour",
            "electiveness"
        ];

        static readonly string[] possible_wrong =
        [
            "False!",
            "Inaccurate!",
            "Incorrect!",
            "No!",
            "None!",
            "Nothing!",
            "Try Again!",
            "Wrong!"
        ];

        static string chosen_word = "";
        static StringBuilder hangman_word = new();

        static int attempt_left = 10;

        static int Generate_Num_From_Arr(string[] t)
        {
            var rnd = new Random();
            int rnd_number = rnd.Next(0, t.Length);
            return rnd_number;
        }

        static string Generate_Word(string[] t)
        {
            int selected_number = Generate_Num_From_Arr(t);

            try
            {
                return t[selected_number];
            } catch (Exception ex)
            {
                Console.WriteLine($"Couldn't generate word! {ex}");
            }

            return "";
        }

        static void Ask_Exit()
        {
            Console.Write("Exit (y/n)? ");
            char key_choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            try
            {
                switch (char.ToLower(key_choice))
                {
                    case 'y':
                        Console.WriteLine("Exited!");
                        Environment.Exit(0);
                        break;
                    case 'n':
                        break;
                    default:
                        Console.WriteLine("Invalid key! Defaulting");
                        break;
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Not a character! {ex}");
            }
        }

        static void Setup()
        {
            Console.Title = "Hangman";
            Console.Clear();

            for (int i = 0; i < hyphen_amount; i++)
            {
                hyphens += "-";
            }

            Console.WriteLine($"{hyphens} Welcome To Hangman! {hyphens}");
            chosen_word = Generate_Word(possible_words);
            // Console.WriteLine($"Chosen word: {chosen_word}");

            for (int i = 0; i < chosen_word.Length; i++)
            {
                hangman_word.Append('_');
            }

            Ask_Exit();
        }

        static void Handle_Guess(char guess)
        {
            bool found = false;

            for (int i = 0; i < chosen_word.Length; i++)
            {
                if (chosen_word[i] == guess && hangman_word[i] == '_') {
                    found = true;
                    hangman_word[i] = guess;
                    // Console.WriteLine($"Found the letter {guess}!");
                }
            }

            if (found)
            {
                Console.WriteLine($"Found {guess}!");
            } else
            {
                attempt_left -= 1;
                string chosen_wrong = Generate_Word(possible_wrong);
                Console.WriteLine(chosen_wrong);
                Console.WriteLine($"Attempts Left: {attempt_left}");
            }
        }

        static void Attempt()
        {
            Console.WriteLine();
            Console.WriteLine($"Hangman word: {hangman_word}");
            Console.Write("Character: ");

            try {
                char new_guess = Convert.ToChar(Console.ReadLine().ToLower().Trim());
                
                Handle_Guess(new_guess);
            } catch (Exception ex)
            {
                Console.WriteLine($"Not a character! {ex}");
            } finally
            {
                Console.WriteLine();
            }
        }

        static void Hang()
        {
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
        }
        static void Win()
        {
            Console.WriteLine("You Won!");
            Environment.Exit(0);
        }

        static void Handle_State()
        {
            const int game_over = 0;

            if (attempt_left <= game_over & hangman_word.ToString() != chosen_word)
            {
                Hang();
            } else if (hangman_word.ToString() != chosen_word)
            {
                Attempt();
            } else
            {
                Win();
            }
        }

        static void Main(string[] args)
        {
            Setup();

            while (true)
            {
                Handle_State();
            }
        }
    }
}