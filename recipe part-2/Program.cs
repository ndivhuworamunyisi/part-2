using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeCreator
{
    // Recipe class to store recipe information
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
    }

    // Ingredient class to store ingredient information
    public class Ingredient
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }

    // Delegate to notify user when recipe exceeds 300 calories
    public delegate void RecipeCalorieExceededEventHandler(Recipe recipe);

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Recipe Creator\n");
                Console.WriteLine("1. Enter new recipe");
                Console.WriteLine("2. Display recipes");
                Console.WriteLine("3. Exit\n");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateNewRecipe();
                        break;

                    case 2:
                        DisplayRecipes();
                        break;

                    case 3:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }
        }

        static void CreateNewRecipe()
        {
            Console.WriteLine("Enter recipe name:");
            string recipeName = Console.ReadLine();

            Recipe recipe = new Recipe { Name = recipeName, Ingredients = new List<Ingredient>(), Steps = new List<string>() };

            Console.WriteLine("Enter number of ingredients:");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter name of ingredient {i + 1}:");
                string ingredientName = Console.ReadLine();

                Console.WriteLine($"Enter quantity of {ingredientName}:");
                float quantity = float.Parse(Console.ReadLine());

                Console.WriteLine($"Enter unit of measurement for {ingredientName}:");
                string unit = Console.ReadLine();

                Console.WriteLine($"Enter calories for {ingredientName}:");
                int calories = int.Parse(Console.ReadLine());

                Console.WriteLine($"Enter food group for {ingredientName}:");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient { Name = ingredientName, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup };
                recipe.Ingredients.Add(ingredient);
            }

            Console.WriteLine("Enter number of steps:");
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter description of step {i + 1}:");
                string stepDescription = Console.ReadLine();
                recipe.Steps.Add(stepDescription);
            }

            recipes.Add(recipe);

            // Check if total calories exceed 300
            if (TotalCalories(recipe) > 300)
            {
                OnRecipeCalorieExceeded(recipe);
            }
        }

        static void DisplayRecipes()
        {
            Console.WriteLine("Recipes:\n");

            foreach (Recipe recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }

            Console.WriteLine("Enter recipe number to display:");
            int recipeNumber = int.Parse(Console.ReadLine());

            Recipe selectedRecipe = recipes.ElementAt(recipeNumber - 1);

            Console.WriteLine($"Recipe: {selectedRecipe.Name}\n");

            foreach (Ingredient ingredient in selectedRecipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} - {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }

            Console.WriteLine("\nSteps:\n");

            for (int i = 0; i < selectedRecipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedRecipe.Steps[i]}");
            }
        }

        static int TotalCalories(Recipe recipe)
        {
            return recipe.Ingredients.Sum(i => i.Calories);
        }

        static void OnRecipeCalorieExceeded(Recipe recipe)
        {
            Console.WriteLine($"Recipe {recipe.Name} exceeds 300 calories!");
        }
    }
}
