using Canteen.DataAccess.Identity;
using Canteen.Service.DishService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canteen.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Canteen.Service.IngridentsService.Dto;
using Canteen.Service.AllergyService.Dto;

namespace Canteen.Service.DishService
{
    public class DishService : IDishService
    {

        private IdentityContext _identityContext {get; set;}

        public DishService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<FullDishDTO> CreateDishAsync(FullDishDTO dish)
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
                return new FullDishDTO
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

        public async Task<FullDishDTO> DeleteDishAsync(int dishID)
        {
            Dish deleteDish = _identityContext.Dishes.Where(dish => dish.DishId == dishID).FirstOrDefault();
            _identityContext.Remove(deleteDish);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullDishDTO
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

        public async Task<FullDishDTO> GetDishAsync(int dishID)
        {
            Dish returnDish = null;

            await Task.Run(() => {
                //returnDish = _identityContext.Dishes.Where(dish => dish.DishId == dishID).FirstOrDefault();
                returnDish = _identityContext.Dishes.Include(x => x.Ingredients).ThenInclude(x => x.Allergies).FirstOrDefault();
            });

            if (returnDish != null)
            {
                return new FullDishDTO { 
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

        public List<FullDishDTO> GetDishesOfTheDay()
        {
            List<Dish> dbDishes = _identityContext.Dishes.Where(dish => dish.DishOfTheDay == true).ToList();
            List<FullDishDTO> returnDishes = new List<FullDishDTO>();

            foreach (Dish item in dbDishes)
            {
                returnDishes.Add(new FullDishDTO
                {
                    DishId = item.DishId,
                    DishCo2 = item.DishCO2,
                    DishName = item.DishName,
                    DishOfTheDay = item.DishOfTheDay
                });
            }

            return returnDishes;
        }

        public async Task<List<FullDishDTO>> GetAllDishesAsync()
        {
            List<FullDishDTO> dishes = new List<FullDishDTO>();

            await Task.Run(() =>
            {
                foreach (Dish dish in _identityContext.Dishes)
                {
                    dishes.Add(new FullDishDTO
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

        public async Task<FullDishDTO> UpdateDishAsync(FullDishDTO updatedDish)
        {
            Dish updateDishObject = _identityContext.Dishes.Where(dish => dish.DishId == updatedDish.DishId).FirstOrDefault();

            updateDishObject.DishName = updatedDish.DishName;
            updateDishObject.DishCO2 = updatedDish.DishCo2;
            updateDishObject.DishOfTheDay = updatedDish.DishOfTheDay;

            _identityContext.Dishes.Update(updateDishObject);

            if (await _identityContext.SaveChangesAsync() > 0)
            {
                return new FullDishDTO
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
