﻿using Service.IngridentsService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IngridentsService
{
    public interface IIngredientsService
    {
        /// <summary>
        /// Get all of the ingridients that belong to a dish
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        Task<List<IngridientDTO>> GetDishIngridientsAsync(int dishId);

        /// <summary>
        /// Get a specified ingridient based on its id
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        Task<IngridientDTO> GetIngridientAsync(int ingredientId);

        /// <summary>
        /// Get all ingredients in the database
        /// </summary>
        /// <returns></returns>
        Task<List<IngridientDTO>> GetIngredientsAsync();

        /// <summary>
        /// Create a new ingridient
        /// </summary>
        /// <param name="ingritiendObject"></param>
        /// <returns></returns>
        Task<IngridientDTO> CreateIngridientAsync(IngridientDTO ingritiendObject);

        /// <summary>
        /// Update specific ingridient by parsing in the DTO
        /// </summary>
        /// <param name="ingridientObject"></param>
        /// <returns></returns>
        Task<IngridientDTO> UpdateIngridientAsync(IngridientDTO ingridientObject);

        /// <summary>
        /// Delete a specific ingridient based on its id
        /// </summary>
        /// <param name="ingridientObject"></param>
        /// <returns></returns>
        Task<IngridientDTO> DeleteIngridientAsync(int ingridientId);
    }
}
