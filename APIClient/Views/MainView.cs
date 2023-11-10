using APIClient.Services.APICalls;
using RecipementAPI.Models.DTO;
using RecipementsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace APIClient.Views
{
    internal class MainView : IMainView
    {
        private readonly IGeneralCalls _generalCalls;
        private readonly IView _innerView;

        public MainView(IGeneralCalls general)
        {
            _generalCalls = general;
        }

        public int RunView()
        {
            string input;
            List<int> Ids;

            do
            {
                Ids = new List<int>();

                Task<(int, IEnumerable<RecipeHeader>)> headers = _generalCalls.GetHeaders();
                (int, IEnumerable<RecipeHeader>) unpack = headers.Result;
                (int, string) unpackVals;

                CommonViews.ResetText();
                CommonViews.MainMenu();

                if (unpack.Item1 == 204)
                {
                    Console.WriteLine("No content in DB");
                    Thread.Sleep(2000);
                    continue;
                }

                foreach (var header in unpack.Item2)
                {
                    Ids.Add(header.Id);
                    Console.WriteLine($"{header.Id}.{header.Title}");
                }

                input = Console.ReadLine().ToLower();

                if(input == "n")
                {
                    CommonViews.ResetText();
                    RecipeDTO recipeDTO;
                    ICollection<IngredientDTO> ingredientList = new List<IngredientDTO>();
                    int statusCode;

                    do
                    {
                        recipeDTO = new();

                        Console.WriteLine("\n Recipe title: ");
                        string title = Console.ReadLine();
                        recipeDTO.Title = title;

                        Console.WriteLine("\n Recipe description");
                        string description = Console.ReadLine();
                        recipeDTO.Description = description;

                        do
                        {
                            CommonViews.ResetText();

                            IngredientDTO ingredient = new();

                            Console.WriteLine("add ingredient name");
                            string name = Console.ReadLine();
                            ingredient.Title = name;

                            Console.WriteLine("add quanity");
                            string quanity = Console.ReadLine();
                            ingredient.Quanity = quanity;

                            ingredientList.Add(ingredient);
                            Console.WriteLine();

                            Console.WriteLine("press any ohter key to add new Ingredient, otherwise 'z'");

                        } while (Console.ReadKey().Key != ConsoleKey.Z);

                        recipeDTO.Ingredients = ingredientList;

                        unpackVals = _generalCalls.Create(recipeDTO).Result;

                        if(unpackVals.Item1 != 200)
                        {
                            Console.WriteLine(unpackVals.Item2);
                            ingredientList = new List<IngredientDTO>();
                        }

                    } while (unpackVals.Item1 != 200);

                }

                if (Ids.Any(x => x == Int32.Parse(input)))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input try again");
                    Thread.Sleep(1000);
                }

            } while (true);

            return Int32.Parse(input);
        }

       

    }
}
