using Service.AllergyService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AllergyService
{
    public interface IAllergyService
    {
        /// <summary>
        /// Create a new allergy in the database
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<AllergyDTO> CreateAllergyAsync(AllergyDTO allergyObject);

        /// <summary>
        /// Get allergy by parsing in a specific allergy id
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        Task<AllergyDTO> GetAllergyAsync(int allergyId);

        /// <summary>
        /// Get all allergies stored in the database
        /// </summary>
        /// <returns></returns>
        Task<List<AllergyDTO>> GetAllergiesAsync();

        /// <summary>
        /// Get all allergies that a dish contains
        /// </summary>
        /// <returns></returns>
        Task<List<AllergyDTO>> GetDishAllergiesAsync(int dishId);

        /// <summary>
        /// Getting all of the allergies that an ingredient contains
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        Task<List<AllergyDTO>> GetIngredientAllergiesAsync(int ingredientId);

        /// <summary>
        /// Update an allergy based on the DTO parsed in
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<AllergyDTO> UpdateAllergyAsync(AllergyDTO allergyObject);

        /// <summary>
        /// Delete the specified allergy in the database base on its id
        /// </summary>
        /// <param name="allergyObject"></param>
        /// <returns></returns>
        Task<AllergyDTO> DeleteAllergyAsync(int allergyId);

    }
}
