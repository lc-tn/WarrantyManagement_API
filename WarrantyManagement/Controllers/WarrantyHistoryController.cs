﻿using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyHistoryController
    {
        private readonly IWarrantyHistoryRepository _warrantyHistoryRepository;

        public WarrantyHistoryController(IWarrantyHistoryRepository warrantyHistoryRepository)
        {
            _warrantyHistoryRepository = warrantyHistoryRepository;
        }

        [HttpGet("{warrantyId}/{pageNumber}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyHistory>> GetWarrantyHistoryById(int warrantyId, int pageNumber)
        {
            List<WarrantyHistory> warrantyHistories = await _warrantyHistoryRepository.GetByWarranty(warrantyId, pageNumber * 3);
            return warrantyHistories;
        }

        [HttpGet("total/{warrantyId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalWarranty(int warrantyId)
        {
            int total = await _warrantyHistoryRepository.Total(warrantyId);
            return total;
        }

        [HttpPost]
        [HasPermission("VIEW_WARRANTY")]
        public WarrantyHistory Add(GetWarrantyHistoryModel warrantyHistoryModel)
        {
            WarrantyHistory warrantyHistory = new WarrantyHistory
            {
                WarrantyId = warrantyHistoryModel.Id,
                Description = warrantyHistoryModel.Description,
                WarrantyDate = warrantyHistoryModel.WarrantyDate,
                Status = warrantyHistoryModel.Status,
                Sale = warrantyHistoryModel.Sale,
                Technician = warrantyHistoryModel.Technician,
                Modifier = warrantyHistoryModel.Modifier,
                ModifyDate = DateTime.Now
            };
            if (_warrantyHistoryRepository.Add(warrantyHistory))
                return warrantyHistory;
            return null;
        }
    }
}
