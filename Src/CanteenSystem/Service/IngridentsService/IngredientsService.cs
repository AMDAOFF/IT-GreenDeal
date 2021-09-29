using DataAccess.Identity;
using Service.IngridentsService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Service.AllergyService.Dto;

namespace Service.IngridentsService
{
    public class IngredientsService : IIngredientsService
    {
        private IdentityContext _identityContext { get; set; }

        public IngredientsService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<IngridientDTO> CreateIngridientAsync(IngridientDTO ingritiendObject)
        {
            Ingredient newIngredient = new Ingredient {
                IngredientId = ingritiendObject.IngridientId,
                IngredientName = ingritiendObject.IngridientName
            };

            _identityContext.Ingredients.Add(newIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new IngridientDTO
                {
                    IngridientId = newIngredient.IngredientId,
                    IngridientName = newIngredient.IngredientName
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<IngridientDTO> DeleteIngridientAsync(int ingridientId)
        {
            Ingredient deleteIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingridientId).FirstOrDefault();

            _identityContext.Remove(deleteIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new IngridientDTO
                {
                    IngridientId = deleteIngredient.IngredientId,
                    IngridientName = deleteIngredient.IngredientName
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<List<IngridientDTO>> GetDishIngridientsAsync(int dishId)
        {
            List<IngridientDTO> returnDishIngredients = new List<IngridientDTO>();

            foreach (Ingredient ingredient in _identityContext.Ingredients.Where(ingredient => ingredient.Dish.DishId == dishId).ToList())
            {
                returnDishIngredients.Add(new IngridientDTO
                {
                    IngridientId = ingredient.IngredientId,
                    IngridientName = ingredient.IngredientName
                });
            }

            return returnDishIngredients;
        }

        public async Task<IngridientDTO> GetIngridientAsync(int ingredientId)
        {
            Ingredient getIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingredientId).FirstOrDefault();
            return new IngridientDTO
            {
                IngridientId = getIngredient.IngredientId,
                IngridientName = getIngredient.IngredientName
            };
        }

        public async Task<List<IngridientDTO>> GetIngredientsAsync()
        {
            List<IngridientDTO> returnIngredients = new List<IngridientDTO>();

            foreach (Ingredient ingredient in _identityContext.Ingredients.ToList())
            {
                returnIngredients.Add(new IngridientDTO
                {
                    IngridientId = ingredient.IngredientId,
                    IngridientName = ingredient.IngredientName
                });
            }

            return returnIngredients;
        }

        public async Task<IngridientDTO> UpdateIngridientAsync(IngridientDTO ingridientObject)
        {
            Ingredient updateIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingridientObject.IngridientId).FirstOrDefault();

            updateIngredient.IngredientName = ingridientObject.IngridientName;

            _identityContext.Ingredients.Update(updateIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new IngridientDTO
                {
                    IngridientId = updateIngredient.IngredientId,
                    IngridientName = updateIngredient.IngredientName
                };
            }

            else
            {
                return null;
            }
        }
    }
}
