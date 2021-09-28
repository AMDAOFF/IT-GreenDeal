using DataAccess.Identity;
using Service.DishService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Service.DishService
{
    public class DishService : IDishService
    {

        private IdentityContext _identityContext {get; set;}

        public DishService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<DishDTO> CreateDishAsync(DishDTO dish)
        {
            Dish newDish = new Dish
            {
                DishName = dish.DishName,
                DishCO2 = dish.DishCo2,
                DishOfTheDay = false
            };

            _identityContext.Dishes.Add(newDish);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new DishDTO
                {
                    DishId = newDish.DishId,
                    DishName = newDish.DishName,
                    DishCo2 = newDish.DishCO2,
                    DishOfTheDay = newDish.DishOfTheDay
                };
            } 
            
            else {
                return null;
            }
        }

        public async Task<DishDTO> DeleteDishAsync(int dishID)
        {
            Dish deleteDish = _identityContext.Dishes.Where(dish => dish.DishId == dishID).FirstOrDefault();
            _identityContext.Remove(deleteDish);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new DishDTO
                {
                    DishId = deleteDish.DishId,
                    DishName = deleteDish.DishName,
                    DishCo2 = deleteDish.DishCO2,
                    DishOfTheDay = deleteDish.DishOfTheDay
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<DishDTO> GetDishAsync(int dishID)
        {
            Dish returnDish = null;

            await Task.Run(() => {
                returnDish = _identityContext.Dishes.Where(dish => dish.DishId == dishID).FirstOrDefault();
            });

            if (returnDish != null)
            {
                return new DishDTO { 
                    DishId = returnDish.DishId,
                    DishName = returnDish.DishName,
                    DishCo2 = returnDish.DishCO2,
                    DishOfTheDay = returnDish.DishOfTheDay
                };
            }

            else
            {
                return null;
            }
        }

        public async Task<List<DishDTO>> GetAllDishesAsync()
        {
            List<DishDTO> dishes = new List<DishDTO>();

            await Task.Run(() =>
            {
                foreach (Dish dish in _identityContext.Dishes)
                {
                    dishes.Add(new DishDTO
                    {
                        DishId = dish.DishId,
                        DishName = dish.DishName,
                        DishCo2 = dish.DishCO2,
                        DishOfTheDay = dish.DishOfTheDay
                    });
                }
            });

            return dishes;
        }

        public async Task<DishDTO> UpdateDishAsync(DishDTO updatedDish)
        {
            Dish updateDishObject = _identityContext.Dishes.Where(dish => dish.DishId == updatedDish.DishId).FirstOrDefault();

            updateDishObject.DishName = updatedDish.DishName;
            updateDishObject.DishCO2 = updatedDish.DishCo2;
            updateDishObject.DishOfTheDay = updatedDish.DishOfTheDay;

            _identityContext.Dishes.Update(updateDishObject);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new DishDTO
                {
                    DishId = updateDishObject.DishId,
                    DishName = updateDishObject.DishName,
                    DishCo2 = updateDishObject.DishCO2,
                    DishOfTheDay = updateDishObject.DishOfTheDay
                };
            }

            else
            {
                return null;
            }
        }
    }
}
