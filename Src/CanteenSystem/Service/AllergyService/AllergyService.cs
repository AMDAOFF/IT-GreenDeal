using Canteen.DataAccess.Identity;
using Canteen.DataAccess.Models;
using Canteen.Service.AllergyService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Service.AllergyService
{
    public class AllergyService : IAllergyService
    {

        private IdentityContext _identityContext { get; set; }

        public AllergyService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<FullAllergyDTO> CreateAllergyAsync(FullAllergyDTO allergyObject)
        {
            Allergy newAllergy = new Allergy
            {
                AllergyName = allergyObject.AllergyName
            };

            _identityContext.Allergies.Add(newAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullAllergyDTO
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

        public async Task<FullAllergyDTO> DeleteAllergyAsync(int allergyId)
        {
            Allergy deleteAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyId).FirstOrDefault();

            _identityContext.Allergies.Remove(deleteAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullAllergyDTO
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

        public async Task<List<FullAllergyDTO>> GetAllergiesAsync()
        {
            List<FullAllergyDTO> returnAllergies = new List<FullAllergyDTO>();

            await Task.Run(() => {
                foreach (Allergy allergy in _identityContext.Allergies)
                {
                    returnAllergies.Add(new FullAllergyDTO { 
                        AllergyId = allergy.AllergyId,
                        AllergyName = allergy.AllergyName
                    });
                }
            });

            return returnAllergies;
        }

        public async Task<FullAllergyDTO> GetAllergyAsync(int allergyId)
        {
            Allergy returnAllergy = null;

            await Task.Run(() => {
                returnAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyId).FirstOrDefault();
            });

            if (returnAllergy != null)
            {
                return new FullAllergyDTO
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

        public async Task<List<FullAllergyDTO>> GetDishAllergiesAsync(int dishId)
        {
            //List<FullAllergyDTO> returnDishAllergies = new List<FullAllergyDTO>();


            //await Task.Run(() =>
            //{
            //    foreach (Ingredient ingredient in _identityContext.Ingredients.Where(dish => dish.Dish.DishId == dishId))
            //    {
            //        foreach (Allergy allergy in ingredient.Allergies)
            //        {
            //            returnDishAllergies.Add(new FullAllergyDTO
            //            {
            //                AllergyId = allergy.AllergyId,
            //                AllergyName = allergy.AllergyName
            //            });
            //        }
            //    }
            //});

            //return returnDishAllergies;
            throw new NotImplementedException();
        }

        public async Task<List<FullAllergyDTO>> GetIngredientAllergiesAsync(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public async Task<FullAllergyDTO> UpdateAllergyAsync(FullAllergyDTO allergyObject)
        {
            Allergy updateAllergy = _identityContext.Allergies.Where(allergy => allergy.AllergyId == allergyObject.AllergyId).FirstOrDefault();

            updateAllergy.AllergyName = allergyObject.AllergyName;

            _identityContext.Allergies.Update(updateAllergy);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullAllergyDTO
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
