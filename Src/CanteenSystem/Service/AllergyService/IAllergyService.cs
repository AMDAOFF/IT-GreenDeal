using Canteen.Service.AllergyService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.AllergyService
{
    public interface IAllergyService
    {
        /// <summary>
        /// Create a new allergy in the database
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<FullAllergyDTO> CreateAllergyAsync(FullAllergyDTO allergyObject);

        /// <summary>
        /// Get allergy by parsing in a specific allergy id
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        Task<FullAllergyDTO> GetAllergyAsync(int allergyId);

        /// <summary>
        /// Get all allergies stored in the database
        /// </summary>
        /// <returns></returns>
        Task<List<FullAllergyDTO>> GetAllergiesAsync();

        /// <summary>
        /// Get all allergies that a dish contains
        /// </summary>
        /// <returns></returns>
        Task<List<FullAllergyDTO>> GetDishAllergiesAsync(int dishId);

        /// <summary>
        /// Getting all of the allergies that an ingredient contains
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        Task<List<FullAllergyDTO>> GetIngredientAllergiesAsync(int ingredientId);

        /// <summary>
        /// Update an allergy based on the DTO parsed in
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<FullAllergyDTO> UpdateAllergyAsync(FullAllergyDTO allergyObject);

        /// <summary>
        /// Delete the specified allergy in the database base on its id
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<FullAllergyDTO> DeleteAllergyAsync(int allergyId);

    }
}
