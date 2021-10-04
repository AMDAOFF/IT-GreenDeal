using Canteen.Service.DishService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Canteen.Service.DishService
{
    public interface IDishService
    {
        /// <summary>
        /// Create a dish in the database
        /// </summary>
        /// <returns>DishDTO with the data that has been entered in the database</returns>
        Task<FullDishDTO> CreateDishAsync(FullDishDTO dish);
        
        /// <summary>
        /// Get a dish by its id
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns>DishDTO with the data that has been fetched from the database</returns>
        Task<FullDishDTO> GetDishAsync(int dishID);

        /// <summary>
        /// Get all of the dishes that are on the menu today
        /// </summary>
        /// <returns></returns>
        List<FullDishDTO> GetDishesOfTheDay();

        /// <summary>
        /// Delete a dish defined by its id
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns>DishDTO if the dish were deleted</returns>
        Task<FullDishDTO> DeleteDishAsync(int dishID);

        /// <summary>
        /// Updates a dish based on the DishDTO object parsed in
        /// </summary>
        /// <param name="updatedDish"></param>
        /// <returns>The updated DishDTO object</returns>
        Task<FullDishDTO> UpdateDishAsync(FullDishDTO updatedDish);

        /// <summary>
        /// Getting all of the dishes in the database
        /// </summary>
        /// <returns>List of DishDTO object</returns>
        Task<List<FullDishDTO>> GetAllDishesAsync();
    }
}
