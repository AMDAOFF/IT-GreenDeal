using DataAccess.Identity;
using DataAccess.Models;
using Service.AllergyService.Dto;
using Service.IngridentsService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AllergyService
{
    public class AllergyService : IAllergyService
    {

        private IdentityContext _identityContext { get; set; }

        public AllergyService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<AllergyDTO> CreateAllergyAsync(AllergyDTO allergyObject)
        {
            Allergy newAllergy = new Allergy
            {
                AllergyName = allergyObject.AllergyName
            };

            _identityContext.Allergies.Add(newAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new AllergyDTO
                {
                    AllergyId = newAllergy.AllergyId,
                    AllergyName = newAllergy.AllergyName
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<AllergyDTO> DeleteAllergyAsync(int allergyId)
        {
            Allergy deleteAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyId).FirstOrDefault();

            _identityContext.Allergies.Remove(deleteAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new AllergyDTO
                {
                    AllergyId = deleteAllergy.AllergyId,
                    AllergyName = deleteAllergy.AllergyName
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<List<AllergyDTO>> GetAllergiesAsync()
        {
            List<AllergyDTO> returnAllergies = new List<AllergyDTO>();

            await Task.Run(() => {
                foreach (Allergy allergy in _identityContext.Allergies)
                {
                    returnAllergies.Add(new AllergyDTO { 
                        AllergyId = allergy.AllergyId,
                        AllergyName = allergy.AllergyName
                    });
                }
            });

            return returnAllergies;
        }

        public async Task<AllergyDTO> GetAllergyAsync(int allergyId)
        {
            Allergy returnAllergy = null;

            await Task.Run(() => {
                returnAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyId).FirstOrDefault();
            });

            if (returnAllergy != null)
            {
                return new AllergyDTO
                {
                    AllergyId = returnAllergy.AllergyId,
                    AllergyName = returnAllergy.AllergyName
                };
            } 
            
            else
            {
                return null;
            }
        }

        public async Task<List<AllergyDTO>> GetDishAllergiesAsync(int dishId)
        {
            List<AllergyDTO> returnDishAllergies = new List<AllergyDTO>();


            await Task.Run(() =>
            {
                foreach (Ingredient ingredient in _identityContext.Ingredients.Where(dish => dish.Dish.DishId == dishId))
                {
                    foreach (Allergy allergy in ingredient.Allergies)
                    {
                        returnDishAllergies.Add(new AllergyDTO
                        {
                            AllergyId = allergy.AllergyId,
                            AllergyName = allergy.AllergyName
                        });
                    }
                }
            });
         
            return returnDishAllergies;
        }

        public async Task<List<AllergyDTO>> GetIngredientAllergiesAsync(int ingredientId)
        {
            List<AllergyDTO> returnIngredientAllergies = new List<AllergyDTO>();

            await Task.Run(() =>
            {
                foreach (Allergy allergy in _identityContext.Allergies.Where(allergy => allergy.Ingredient.IngredientId == ingredientId).ToList())
                {
                    returnIngredientAllergies.Add(new AllergyDTO
                    {
                        AllergyId = allergy.AllergyId,
                        AllergyName = allergy.AllergyName
                    });
                }
            });

            return returnIngredientAllergies;
        }

        public async Task<AllergyDTO> UpdateAllergyAsync(AllergyDTO allergyObject)
        {
            Allergy updateAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyObject.AllergyId).FirstOrDefault();

            updateAllergy.AllergyName = allergyObject.AllergyName;

            _identityContext.Allergies.Update(updateAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new AllergyDTO
                {
                    AllergyId = updateAllergy.AllergyId,
                    AllergyName = updateAllergy.AllergyName
                };
            }

            else
            {
                return null;
            }
        }
    }
}
