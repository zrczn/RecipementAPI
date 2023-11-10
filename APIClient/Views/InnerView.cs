using APIClient.Services.APICalls;
using APIClient.Services.Client;
using RecipementAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Views
{
    internal class InnerView : IInnerView
    {
        private readonly IInnerCalls _innerCalls;
        public InnerView(IInnerCalls innerCalls)
        {
            _innerCalls = innerCalls;
        }

        public void RunView(int Id)
        {
            CommonViews.ResetText();
            CommonViews.Details();

            Task<(int, RecipeDTO)> unpack = _innerCalls.GetSingleItem(Id);

            if (unpack.Result.Item1 != 200)
            {
                Console.WriteLine($"Error!, Http response code: {unpack.Result.Item1}\n {unpack.Result.Item2}");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Recipe title: ");
            Console.WriteLine($"{unpack.Result.Item2.Title}\n");

            foreach (var ingredient in unpack.Result.Item2.Ingredients)
            {
                Console.WriteLine(ingredient.Title);
                Console.WriteLine($"Quanity: {ingredient.Quanity} \n");
            }

            Console.WriteLine("Recipe description");
            Console.WriteLine($"{unpack.Result.Item2.Description}\n");

            Console.WriteLine("Choose action");
            Console.WriteLine("1.Delete recipe");
            Console.WriteLine("2.Edit recipe");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    var Delresponse = _innerCalls.DeleteItem(Id).Result;
                    if(Delresponse.Item1 != 200)
                    {
                        Console.WriteLine($"Error!, Http response code: {Delresponse.Item1}\n {Delresponse.Item2}");
                        Console.ReadLine();
                    }
                    break;
                case ConsoleKey.D2:
                    var Modresponse = _innerCalls.ModifyItem(unpack.Result.Item1 ,unpack.Result.Item2).Result;
                    if (Modresponse.Item1 != 200)
                    {
                        Console.WriteLine($"Error!, Http response code: {Modresponse.Item1}\n {Modresponse.Item2}");
                        Console.ReadLine();
                    }
                    break;
            }
        }
    }
}
