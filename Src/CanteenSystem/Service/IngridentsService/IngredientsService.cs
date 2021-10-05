using Canteen.DataAccess.Identity;
using Canteen.Service.IngridentsService.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canteen.DataAccess.Models;
using System;

namespace Canteen.Service.IngridentsService
{
    public class IngredientsService : IIngredientsService
    {
        private IdentityContext _identityContext { get; set; }

        public IngredientsService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<FullIngridientDTO> CreateIngridientAsync(FullIngridientDTO ingritiendObject)
        {
            Ingredient newIngredient = new Ingredient {
                IngredientId = ingritiendObject.IngridientId,
                IngredientName = ingritiendObject.IngridientName
            };

            _identityContext.Ingredients.Add(newIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullIngridientDTO
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

        public async Task<FullIngridientDTO> DeleteIngridientAsync(int ingridientId)
        {
            Ingredient deleteIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingridientId).FirstOrDefault();

            _identityContext.Remove(deleteIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullIngridientDTO
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

        public async Task<List<FullIngridientDTO>> GetDishIngridientsAsync(int dishId)
        {
            throw new NotImplementedException();
        }

        public async Task<FullIngridientDTO> GetIngridientAsync(int ingredientId)
        {
            Ingredient getIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingredientId).FirstOrDefault();
            return new FullIngridientDTO
            {
                IngridientId = getIngredient.IngredientId,
                IngridientName = getIngredient.IngredientName
            };
        }

        public async Task<List<FullIngridientDTO>> GetIngredientsAsync()
        {
            List<FullIngridientDTO> returnIngredients = new List<FullIngridientDTO>();

            foreach (Ingredient ingredient in _identityContext.Ingredients.ToList())
            {
                returnIngredients.Add(new FullIngridientDTO
                {
                    IngridientId = ingredient.IngredientId,
                    IngridientName = ingredient.IngredientName
                });
            }

            return returnIngredients;
        }

        public async Task<FullIngridientDTO> UpdateIngridientAsync(FullIngridientDTO ingridientObject)
        {
            Ingredient updateIngredient = _identityContext.Ingredients.Where(ingredient => ingredient.IngredientId == ingridientObject.IngridientId).FirstOrDefault();

            updateIngredient.IngredientName = ingridientObject.IngridientName;

            _identityContext.Ingredients.Update(updateIngredient);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullIngridientDTO
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
